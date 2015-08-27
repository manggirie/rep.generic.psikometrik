define(["services/datacontext", objectbuilders.config, objectbuilders.app], function(context, config, app){
    var isPenyelaras = ko.observable(false),
        penyelaras = ko.observable(),
        checkMyKad = function(ic){
            console.log(ic);
            context.getCountAsync("Pengguna", String.format("MyKad eq '{0}'", ic), "MyKad")
            .done(function(count){
                if(count > 0){
                    app.showMessage("Pengguna dengan MyKad " + ic + " sudah wujud", "Sistem Ujian Psikometrik", ["OK"])
                      .done(function () {
                        
                    });
                    
                }
            });
            
        },
        activate = function(entity){
            if(ko.unwrap(entity.Id) === "0"){
                entity.MyKad.subscribe(checkMyKad);
            }
            
            return context.loadOneAsync("Pengguna", String.format("MyKad eq '{0}'", config.userName))
                .done(function(user){

                        if(!user){
                            return;
                        }
                        console.log(user);
                        if(typeof ko.unwrap(user) === "undefined"){
                            return;
                        }
        
                        if(typeof user.IsPenyelaras === "undefined"){
                            return;
                        }
                        if( ko.unwrap(user.IsPenyelaras)){
                            console.log("OK");
                            entity.JenisPerkhidmatan(ko.unwrap(user.JenisPerkhidmatan));
                            entity.NamaKementerian(ko.unwrap(user.NamaKementerian));
                            entity.NamaJabatan(ko.unwrap(user.NamaJabatan));
                            entity.IsResponden(true);
                            isPenyelaras(true);
                            penyelaras(user);
                        }
                });


        },
        attached  = function(view){
            
            $("select.required").attr("required", "");
        };

    return {
        isPenyelaras : isPenyelaras,
        penyelaras : penyelaras,
        activate : activate,
        attached : attached
    };

});
