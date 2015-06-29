define([objectbuilders.datacontext, objectbuilders.config], function(context, config){
    var namaJabatan = ko.observable(),
        activate = function(list){
            
            return context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaJabatan")
            .done(namaJabatan);

        },
        attached  = function(view){
        
        };

    return {
        namaJabatan : namaJabatan,
        activate : activate,
        attached : attached
    };

});