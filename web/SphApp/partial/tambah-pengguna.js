define(["services/datacontext",objectbuilders.config], function(context, config){
    var isPenyelaras = ko.observable(),
        penyelaras = ko.observable(),
        activate = function(entity){
            
            return context.loadOneAsync("Pengguna", String.format("MyKad eq '{0}'", config.userName))
            .done(function(user){
                console.log(user);
                if(ko.unwrap(user.IsPenyelaras)){
                    console.log("OK");
                    entity.JenisPerkhidmatan(ko.unwrap(user.JenisPerkhidmatan));
                    entity.NamaKementerian(ko.unwrap(user.NamaKementerian));
                    entity.NamaJabatan(ko.unwrap(user.NamaJabatan));
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