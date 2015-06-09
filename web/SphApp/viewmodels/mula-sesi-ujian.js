define(["services/datacontext", objectbuilders.app], function(context, app){
    var sesiUjian = ko.observable(),
        ujian = ko.observable(),
        pendaftaran = ko.observable(),
        permohonan = ko.observable(),
		totalAnswered = ko.observable(0),
		questionsCount = ko.observable(0),
		interval = null,
		timer = ko.observable(),
		sections = ko.observableArray(),
		mapJawapan = function(v){
		    
		    var questions = ko.observableArray(),
		        section ={section : ko.unwrap(v.SeksyenSoalan), questions: questions} ;
			if(sections().indexOf(ko.unwrap(v.SeksyenSoalan)) < 0){
				sections().push(section);	
			}else{
			    section = _(sections()).find(function(v){return v.section === v.SeksyenSoalan;});
			    questions =section.questions
			}
			
			var answer = {
				"$type":"Bespoke.epsikologi_sesiujian.Domain.Jawapan, epsikologi.SesiUjian",
				"JawapanPilihan" : ko.observable(),
				"SeksyenSoalan" : ko.unwrap(v.SeksyenSoalan),
				"SoalanNo": ko.unwrap(v.SoalanNo),
				"Trait" : ko.unwrap(v.Trait),
				"Text" : ko.unwrap(v.TeksSoalan),
				"Nilai" : ko.observable(),
				"PilihanJawapanCollection" : v.PilihanJawapanCollection
			};
			questions.push(answer);
			
			return answer;
		},
        activate = function(id){
            return context.loadOneAsync("SesiUjian", String.format("Id eq '{0}'", id))
                    .then(function(a){
                        sesiUjian(a);
                        return context.loadOneAsync("Ujian", String.format("UjianNo eq '{0}'",ko.unwrap(sesiUjian().NamaUjian)));
                    })
                    .then(function(b){
                        ujian(b);
						return context.loadAsync({entity:"Soalan", size: 200,includeTotal: true}, String.format("NamaUjian eq '{0}'",ko.unwrap(ujian().NamaUjian)));
                    })
					.then(function(qLo){
						sesiUjian().JawapanCollection.removeAll();
						var questions = _(qLo.itemCollection).map(mapJawapan);
						sesiUjian().JawapanCollection(questions);
						questionsCount(qLo.rows);
				
						if(qLo.rows > qLo.size){							
							return context.loadAsync({entity:"Soalan", size: 200,includeTotal: true, page: 2 }, String.format("NamaUjian eq '{0}'",ko.unwrap(ujian().NamaUjian)));
						}
						return Task.fromResult(0); 
					})
					.then(function(qLo){
						if(!qLo){
							return;
						}
						var questions = _(qLo.itemCollection).map(mapJawapan);
						_(questions).each(function(v){
							sesiUjian().JawapanCollection.push(v);						
						});
						// assume it's only 400 max
					});


        },
        attached  = function(view){
        	$(view).on("click", "input[type=radio]", function(){
				var panel = $(this).parents("div.soalan-panel"),
					soalan = ko.dataFor(panel[0]),
					answer = ko.dataFor(this);
				
				soalan.JawapanPilihan(ko.unwrap(answer.Teks));
				soalan.Nilai(ko.unwrap(answer.Nilai));
				totalAnswered(ko.unwrap(totalAnswered) + 1);
			
			});
			
			var start = moment.duration();
			interval = window.setInterval(function(){
				start.add(1000);
				timer(start.minutes() + " minutes and " + start.seconds() + " seconds");
			},1000);
        },
		detached = function(){
			interval = null;
			timer("");
		},
		canDeactivate = function(){
			
			if(totalAnswered() < questionsCount()){
				  app.showMessage("Are you sure you want to remove this custom route permanently", "Remove Route", ["Yes", "No"])
                    .done(function (dialogResult) {
                        if (dialogResult === "Yes") {
                          
							

                        }
                    });
				return false;
			}
			return true;
		};

    return {
        sections : sections,
        detached : detached,
        canDeactivate : canDeactivate,
        timer : timer,
        questionsCount : questionsCount,
        totalAnswered : totalAnswered,
        sesiUjian : sesiUjian,
        ujian : ujian,
        pendaftaran : pendaftaran,
        permohonan : permohonan,
        activate : activate,
        attached : attached
    };

});