define([objectbuilders.config], function(config){
    var activate = function(entity){
            
            entity.NamaJabatan(config.namaJabatan);
            entity.NamaKementerian(config.namaKementerian);
            entity.Penyelaras(config.userName);
            
            return true;


        },
        attached  = function(view){
        
        };

    return {
        activate : activate,
        attached : attached
    };

});