define([objectbuilders.config], function (config) {
    var permohonan = ko.observable({
        TarikTamat: ko.observable(moment().format("YYYY-MM-DD"))
    }),
            saveTrigger = ko.observable(false),
            activate = function (entity) {
                permohonan(entity);
                entity.NamaJabatan(config.namaJabatan);
                entity.NamaKementerian(config.namaKementerian);
                entity.Penyelaras(config.userName);
                if (!ko.unwrap(entity.StatusPermohonan)) {
                    entity.StatusPermohonan("BARU");
                }

                return true;


            },
            attached = function (view) {
                var tarikhMulaPicker = $("#tarikhMulaPicker").data("kendoDatePicker"),
                    tarikhTamatPicker = $("#tarikhTamatPicker").data("kendoDatePicker");
                tarikhMulaPicker.min(new Date());
                tarikhTamatPicker.min(new Date());

                tarikhMulaPicker.enable(permohonan().StatusPermohonan() === "BARU");
                tarikhTamatPicker.enable(permohonan().StatusPermohonan() === "BARU" || config.roles.indexOf("PengurusanPermohonan") > -1);

                saveTrigger(true);// fire this to re-eval

                if (config.profile.Designation === "Urusetia") {
                    $(view).find("h1").text("Program oleh Urusetia");
                }

            },
            canExecuteSaveCommand = ko.computed(function () {
                if (!ko.unwrap(saveTrigger)) {
                    return false;
                }
                var date = moment(),
                  tarikhTamat = moment(ko.unwrap(permohonan().TarikhTamat));

                if (date > tarikhTamat) {
                    return false;
                }

                return true;
            });

    return {
        activate: activate,
        attached: attached,
        canExecuteSaveCommand: canExecuteSaveCommand
    };

});