
    define([objectbuilders.datacontext, objectbuilders.logger, objectbuilders.router,
        objectbuilders.system, objectbuilders.validation, objectbuilders.eximp,
        objectbuilders.dialog, objectbuilders.watcher, objectbuilders.config,
        objectbuilders.app ],
        function (context, logger, router, system, validation, eximp, dialog, watcher,config,app
            ) {

            var pendaftaran = ko.observable(new bespoke.epsikologi_pendaftaranprogram.domain.PendaftaranProgram({WebId:system.guid()}))
                errors = ko.observableArray(),
                activate = function (programId) {
                   var query = String.format("Id eq '{0}'", programId),
                        permohonanTask = context.loadOneAsync("permohonan", query);

                    return $.when(permohonanTask).done(function(b) {
                        if (b) {
                            pendaftaran().NamaProgram(ko.unwrap(b.NamaProgram));
                            pendaftaran().NoPermohonan(ko.unwrap(b.PermohonanNo));
                        }
                        
                    });

                },
                attached = function (view) {
                    pendaftaran().MyKad.subscribe(function(ic){
                        context.getScalarAsync("Pengguna", "MyKad eq '" + ic + "' and IsResponden eq 1", "Nama")
                        .done(function(nama){
                            pendaftaran().NamaPengguna(nama);
                        });

                    });
                },
                save = function() {

                    var data = ko.mapping.toJSON(pendaftaran);

                    return context.post(data, "/PendaftaranProgram/TambahResponden")
                        .then(function(result) {
                            app.showMessage("Your PendaftaranProgram has been successfully saved", "epsikologi", ["ok"]);

                        });
                    
                };

            var vm = {
                activate: activate,
                config: config,
                attached: attached,
                pendaftaran: pendaftaran,
                errors: errors,
                save : save,
                toolbar : {
                    saveCommand : save
                }
            };

            return vm;
        });
