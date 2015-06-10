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
		        section =_(sections()).find(function(s){return s.section === ko.unwrap(v.SeksyenSoalan);}) ;
			if(section){				
			    questions = section.questions;
			}else{
				sections.push({
							section : ko.unwrap(v.SeksyenSoalan), 
							questions: questions,
						    answered : ko.observable(0)
				});	
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
            
           /* $('#test-panel').affix({
              offset: {
                top: 100,
                bottom: 5
              }
            });*/
            
            
        	$(view).on("click", "input[type=radio]", function(){
				var panel = $(this).parents("li.soalan-panel"),
					soalan = ko.dataFor(panel[0]),
					answer = ko.dataFor(this);
				
				var modify = ko.unwrap(soalan.JawapanPilihan);
				
				soalan.JawapanPilihan(ko.unwrap(answer.Teks));
				soalan.Nilai(ko.unwrap(answer.Nilai));
				
				if(!modify){
					totalAnswered(ko.unwrap(totalAnswered) + 1);				
					var section =_(sections()).find(function(s){return s.section === ko.unwrap(soalan.SeksyenSoalan);});
					section.answered(ko.unwrap(section.answered) + 1);
				}
			
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
			var tcs = new $.Deferred();
			if(totalAnswered() < questionsCount()){
				  app.showMessage("Adaka and ingin meninggalkan sesi ujian ini", "Tinggal Sesi Ujian", ["Ya", "Tidak"])
                    .done(function (dialogResult) {
                        tcs.resolve(dialogResult === "Ya");                        
                    });
			}else{
				return true;
			}
			return tcs.promise();
		},
		findPos = function (obj) {
			var curtop = 0;
			if (obj.offsetParent) {
				do {
					curtop += obj.offsetTop;
				} while (obj = obj.offsetParent);
			return [curtop];
			}
		},
		goToSection = function(sc){
			window.scroll(0,findPos(document.getElementById(sc.section)));
		
		},
		submitSesiUjian = function(){
		
			return context.post(ko.toJSON(sesiUjian), "/sesiujian/submitsesiujian");
		};

    return {
        submitSesiUjian : submitSesiUjian,
        goToSection : goToSection,
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