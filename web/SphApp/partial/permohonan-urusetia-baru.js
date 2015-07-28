define([objectbuilders.config], function(config){
    var activate = function(entity){
            entity.Penyelaras(config.userName);
            entity.StatusPermohonan("LULUS");
            return true;
        },
        attached  = function(view){
        
        };

    return {
        activate : activate,
        attached : attached
    };

});