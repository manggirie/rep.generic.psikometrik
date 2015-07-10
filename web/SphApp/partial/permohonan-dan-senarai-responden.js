define(["services/datacontext", objectbuilders.app, objectbuilders.config], function(context, app, config){
    var permohonan = ko.observable(),
        senaraiPendaftaran = ko.observableArray(),
        activate = function(entity){
            permohonan(entity);
            var tcs = new $.Deferred();
            setTimeout(function(){
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        attached  = function(view){

        },
        save = function(pendaftaran) {

            var data = ko.mapping.toJSON(pendaftaran);
            return context.post(data, "/PendaftaranProgram/TambahResponden")
                .then(function(result) {
                    senaraiPendaftaran.push(pendaftaran);
                });

        },
        addResponden = function(){

            var tcs = new $.Deferred();
            require(['viewmodels/tambah-responden-dialog' , 'durandal/app'], function (dialog, app2) {
                dialog.maxCount ( permohonan().BilRespondan() - senaraiPendaftaran().length);
                app2.showDialog(dialog)
                    .done(function (result) {
                        if (result === "OK") {
                            console.log(dialog.selectedRespondens());
                            var registrations = _(dialog.selectedRespondens()).map(function(v){
                                var r = new bespoke.epsikologi_pendaftaranprogram.domain.PendaftaranProgram(v);
                                r.NamaProgram(ko.unwrap(permohonan().NamaProgram));
                                r.NoPermohonan(ko.unwrap(permohonan().PermohonanNo));
                                r.PermohonanId(ko.unwrap(permohonan().Id));
                                r.NamaPengguna(v.Nama);
                                r.TarikhDaftar(moment().format("YYYY-MM-DD"));
                                return r;
                            });
                            console.log("registrations", ko.toJS(registrations));
                            var tasks = _(registrations).map(save);
                            $.when(tasks).done(tcs.resolve);

                        }else{
                            tcs.resolve();
                        }
                });
            });

            return tcs.promise();
        },
        sendReminderEmail = function(pendaftaran){
            return function(){

            var to = "";
            return context.loadOneAsync("Pengguna", String.format("MyKad eq '{0}'", ko.unwrap(pendaftaran.MyKad)))
                .then(function(user){
                    to = ko.unwrap(user.Emel);
                    return $.get("/email-template/generate/Permohonan/" + ko.unwrap(permohonan().Id) + "/peringatan-kepada-responden") ;
                })
                .then(function(mail){
                    var message = {
                        subject : mail.subject,
                        body : mail.body,
                        to : to
                    };
                   return context.post(JSON.stringify(message), "/email-template/send");
                }).then(function(){
                    app.showMessage("Emel peringatan sudah dihantar kepada " + to, config.applicationFullName, ["OK"]);
                });

            };
        };

    return {
        addResponden : addResponden,
        senaraiPendaftaran : senaraiPendaftaran,
        sendReminderEmail : sendReminderEmail,
        activate : activate,
        attached : attached
    };

});
