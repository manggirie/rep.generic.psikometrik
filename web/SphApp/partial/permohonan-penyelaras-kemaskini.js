define([objectbuilders.config], function(config){
    var     permohonan = ko.observable(),
            activate = function(entity){
                permohonan(entity);
                return true;

            },
            attached  = function(view){
                var tarikhMulaPicker = $(view).find("#tarikhMulaPicker").data("kendoDateTimePicker"),
                    tarikhTamatPicker = $(view).find("#tarikhTamatPicker").data("kendoDateTimePicker");
                tarikhMulaPicker.min(new Date());
                tarikhTamatPicker.min(new Date());

                tarikhMulaPicker.enable(permohonan().StatusPermohonan() === 'BARU');
                tarikhTamatPicker.enable(permohonan().StatusPermohonan() === 'BARU');
            };

    return {
        activate : activate,
        attached : attached
    };

});
