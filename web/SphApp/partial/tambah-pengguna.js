define(["services/datacontext", objectbuilders.config, objectbuilders.logger], function(context, config, logger){
    var isPenyelaras = ko.observable(false),
        penyelaras = ko.observable(),
        activate = function(entity){

            entity.MyKad.subscribe(function(e){
                if(ko.unwrap(entity.Id) === "0"){
                    console.log("MyKad = " + e)
                    context.getCountAsync("Pengguna", String.format("MyKad eq '{0}'", e)).done(function(c){
                        if(c > 0){
                            logger.error("User with MyKad no " + e +" already exist");
                            $("#mykad-input").addClass("input-validation-error");
                        }else{
                            
                            $("#mykad-input").removeClass("input-validation-error");
                        }
                    });
                }
            });
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

        };

    return {
        isPenyelaras : isPenyelaras,
        penyelaras : penyelaras,
        activate : activate,
        attached : attached
    };

});
