define([objectbuilders.config], function (config) {

    function toTitleCase(str) {
        return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
    }

    var permohonan = ko.observable(),
            activate = function (entity) {
                permohonan(entity);
                entity.NamaJabatan(config.namaJabatan);
                entity.NamaKementerian(config.namaKementerian);
                entity.Penyelaras(config.userName);
                if (!ko.unwrap(entity.StatusPermohonan)) {
                    entity.StatusPermohonan("BARU");
                }

                ko.extenders.toTitleCase = function (target, option) {
                    target.subscribe(function (newValue) {
                        target(toTitleCase(newValue));
                    });
                    return target;
                };

                entity.NamaProgram.extend({ toTitleCase: true });

                return true;
            },


            attached = function (view) {
                var tarikhMulaPicker = $("#tarikhMulaPicker").data("kendoDatePicker"),
                    tarikhTamatPicker = $("#tarikhTamatPicker").data("kendoDatePicker");
                tarikhMulaPicker.min(new Date());
                tarikhTamatPicker.min(new Date());

                tarikhMulaPicker.enable(permohonan().StatusPermohonan() === 'BARU');
                tarikhTamatPicker.enable(permohonan().StatusPermohonan() === 'BARU');

                $("NamaProgram").css("text-transform", "capitalize");
            };

    return {
        activate: activate,
        attached: attached
    };

});