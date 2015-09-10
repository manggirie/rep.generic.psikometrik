define([objectbuilders.config, "partial/permohonan-urusetia"], function(config, formPartial){
    var activate = function(entity){
            entity.Penyelaras(config.userName);
            entity.StatusPermohonan("LULUS");
            return formPartial.activate(entity);
        },
        attached  = function(view){
             var tarikhMulaPicker = $("#tarikhMulaPicker").data("kendoDatePicker"),
                    tarikhTamatPicker = $("#tarikhTamatPicker").data("kendoDatePicker");
                tarikhMulaPicker.min(new Date());
                tarikhTamatPicker.min(new Date());

                return formPartial.attached(view);

        };

    return {
        activate : activate,
        attached : attached
    };

});