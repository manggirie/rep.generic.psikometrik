define([objectbuilders.config], function(config){
    var     permohonan = ko.observable(),
            activate = function(entity){
                permohonan(entity);
                entity.NamaJabatan(config.namaJabatan);
                entity.NamaKementerian(config.namaKementerian);
                entity.Penyelaras(config.userName);
                if(!ko.unwrap(entity.StatusPermohonan)){
                    entity.StatusPermohonan("BARU");
                }
                
                return true;
    
    
            },
            attached  = function(view){
                var tarikhMulaPicker = $("#tarikhMulaPicker").data("kendoDatePicker"),
                    tarikhTamatPicker = $("#tarikhTamatPicker").data("kendoDatePicker");
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