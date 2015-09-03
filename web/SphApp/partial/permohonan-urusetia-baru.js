define([objectbuilders.config], function(config){
    var activate = function(entity){
            entity.Penyelaras(config.userName);
            entity.StatusPermohonan("LULUS");
            return true;
        },
        attached  = function(view){
             var tarikhMulaPicker = $("#tarikhMulaPicker").data("kendoDatePicker"),
                    tarikhTamatPicker = $("#tarikhTamatPicker").data("kendoDatePicker");
                tarikhMulaPicker.min(new Date());
                tarikhTamatPicker.min(new Date());
                
        };

    return {
        activate : activate,
        attached : attached
    };

});