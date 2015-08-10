define(["services/datacontext", objectbuilders.app, objectbuilders.config, objectbuilders.logger], function (context, app, config, logger) {
    "use strict";
    var permohonan = ko.observable(),
        filePelajar = ko.observable(),
        senaraiPendaftaran = ko.observableArray(),
        activate = function (entity) {
            permohonan(entity);
            var tcs = new $.Deferred();
            setTimeout(function () {
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        attached = function () {

        },
        save = function (pendaftaran) {

            var data = ko.mapping.toJSON(pendaftaran),
                sixMonthsAgo = moment().subtract(6, "M").format("YYYY-MM-DD"),
				pm = permohonan();

            var ujian = "";
            if (ko.unwrap(pm.isIBK)) {
                ujian = "IBK";
            }
            if (ko.unwrap(pm.isUKBP)) {
                ujian = "UKBP";
            }
            if (ko.unwrap(pm.isIP)) {
                ujian = "IP";
            }
            if (ko.unwrap(pm.isHLP)) {
                ujian = "HLP";
            }
            if (ko.unwrap(pm.isIOC)) {
                ujian = "IOC";
            }
            if (ko.unwrap(pm.isIPE)) {
                ujian = "IPE";
            }
            if (ko.unwrap(pm.isIPU)) {
                ujian = "IPU";
            }
            if (ko.unwrap(pm.isISO)) {
                ujian = "ISO";
            }
            if (ko.unwrap(pm.isISP)) {
                ujian = "ISP";
            }
            if (ko.unwrap(pm.isPTD)) {
                ujian = "PTD";
            }

            return context.loadOneAsync("Permohonan", "Id eq '" + ko.unwrap(pendaftaran.PermohonanId) + "'")
                .then(function (pm) {
                    // TODO : what it has more than 1 Ujian
                    var query = String.format("Status eq 'Diambil' and TarikhUjian ge DateTime'{0}' and MyKad eq '{1}' and NamaUjian eq '{2}'", sixMonthsAgo, pendaftaran.MyKad(), ujian);
                    return context.getCountAsync("SesiUjian", query, "Id")
                })
                .then(function (count) {
                    if (count > 0) {
                        logger.error(String.format("{0} sudah menduduki ujian {1} dan perlu menunngu 6 bulan", pendaftaran.MyKad(), ujian), ko.toJS(pendaftaran));
                        return Task.fromResult("Sudah daftar");
                    }
                    return context.post(data, "/PendaftaranProgram/TambahResponden");
                })
                .then(function (result) {
                    if (result === "Sudah daftar") {
                        return;
                    }
                    senaraiPendaftaran.push(pendaftaran);
                });

        },
        addResponden = function () {

            var tcs = new $.Deferred();
            require(["viewmodels/tambah-responden-dialog", "durandal/app"], function (dialog, app2) {
                dialog.maxCount(permohonan().BilRespondan() - senaraiPendaftaran().length);
                dialog.senaraiPendaftaran(senaraiPendaftaran());
                app2.showDialog(dialog)
                    .done(function (result) {
                        if (result === "OK") {
                            console.log(dialog.selectedRespondens());
                            var registrations = _(dialog.selectedRespondens()).map(function (v) {
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

                        } else {
                            tcs.resolve();
                        }
                    });
            });

            return tcs.promise();
        },
        sendReminderEmail = function (pendaftaran) {
            return function () {

                var to = "";
                return context.loadOneAsync("Pengguna", String.format("MyKad eq '{0}'", ko.unwrap(pendaftaran.MyKad)))
                    .then(function (user) {
                        to = ko.unwrap(user.Emel);
                        return $.get("/email-template/generate/Permohonan/" + ko.unwrap(permohonan().Id) + "/peringatan-kepada-responden");
                    })
                    .then(function (mail) {
                        var message = {
                            subject: mail.subject,
                            body: mail.body,
                            to: to
                        };
                        return context.post(JSON.stringify(message), "/email-template/send");
                    }).then(function () {
                        app.showMessage("Emel peringatan sudah dihantar kepada " + to, config.applicationFullName, ["OK"]);
                    });

            };
        };

    // process the list of students
    filePelajar.subscribe(function (w) {
        context.post(JSON.stringify({ id: w, permohonanId: ko.unwrap(permohonan().Id) }), "/pelajar/process-file")
        .done(function (r) {
            if (r.success) {
                _(r.list).each(function (v) {
                    senaraiPendaftaran.push(v);
                });
            }
        });
    });

    return {
        addResponden: addResponden,
        senaraiPendaftaran: senaraiPendaftaran,
        sendReminderEmail: sendReminderEmail,
        filePelajar: filePelajar,
        activate: activate,
        attached: attached
    };

});
