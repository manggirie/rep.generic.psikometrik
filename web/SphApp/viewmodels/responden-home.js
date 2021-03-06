define(["services/datacontext", objectbuilders.config, objectbuilders.app, objectbuilders.router], function (context, config, app, router) {
    var testList = ko.observableArray(),
        ujianTertangguh = ko.observableArray(),
        ujianList = ko.observableArray(),
        registrationList = ko.observableArray(),
        getNamaUjian = function (kod) {
            var ujian = _(ujianList()).find(function (v) {
                return ko.unwrap(v.UjianNo) === ko.unwrap(kod);
            });

            return ko.unwrap(ujian.NamaUjian);
        },
        activate = function () {
            var date = moment().format("YYYY-MM-DD HH:mm:ss"),
            query = String.format("MyKad eq '{0}' and Status eq 'Belum Ambil' and TarikhMulaProgram le DateTime'{1}' and TarikhTamatProgram ge DateTime'{1}' ", config.userName, date);
            console.log(query);
            return context.loadAsync("SesiUjian", query)
                .then(function (lo) {
                    testList(lo.itemCollection);
                    return context.loadAsync("Ujian", "Id ne '0'");
                })
                .then(function (lo1) {
                    ujianList(lo1.itemCollection);
                    return context.loadAsync("Setting", "UserName eq '" + config.userName + "'");
                })
                .then(function () {
                    _(testList()).each(function (v) {
                        v.Lock = ko.observable();
                        context.loadOneAsync("PercubaanSesi", "MyKad eq '" + config.userName + "' and SesiUjianId eq '" + ko.unwrap(v.Id) + "'")
                        .done(function (lock) {
                            if (lock) {
                                v.Lock(ko.unwrap(lock.Bilangan));
                            }
                        });
                    });

                    return context.loadAsync("PendaftaranProgram", "MyKad eq '" + config.userName + "'");
                })
                .then(function (lo) {
                    var daftar = _(lo.itemCollection).map(function (v) {
                        v.tarikhMula = ko.observable();
                        v.tarikhTamat = ko.observable();
                        v.exams = ko.observableArray();
                        return v;
                    });
                    registrationList(daftar);
                    // just load the name, status and date for each sesi ujian in the program
                    _(daftar).each(function (v) {
                        var sq = String.format("MyKad eq '{0}' and NamaProgram eq '{1}'", config.userName, v.NoPermohonan());
                        context.getTuplesAsync("SesiUjian", sq, "NamaUjian", "Status", "TarikhUjian", "TarikhMulaProgram", "TarikhTamatProgram")
                         .done(function (list) {
                             var sessions = _(list).map(function (c) {
                                 c.namaUjian = getNamaUjian(c.Item1);
                                 return c;
                             });
                             v.exams(sessions);
                             v.tarikhMula(list[0].Item4);
                             v.tarikhTamat(list[0].Item5);
                         });

                    });

                });
        },
        attached = function (view) {

            $.getJSON("/sesi-ujian")
            .done(function (ut) {
                if (ut.success) {
                    ujianTertangguh.removeAll();
                    ujianTertangguh.push(ut.sesi);
                    $("button[data-id=" + ut.sesi.Id + "]").text("Sambung Sesi Ujian");
                }
            });
        },
        getTileClass = function (color) {
            switch (ko.unwrap(color)) {
                case "bred": return "red-intense";
                case "bgreen": return "green-haze";
                case "bviolet": return "purple-plum";
                default: return "blue-madison";

            }
        },

        getTempohUjian = function (kod) {
            var ujian = _(ujianList()).find(function (v) {
                return ko.unwrap(v.UjianNo) === ko.unwrap(kod);
            });
            var hours = ko.unwrap(ujian.DurationHour),
                minutes = ko.unwrap(ujian.DurationMinutes);

            return hours + " jam " + minutes + " minit";
        },
        canDeactivate = function () {
            var locked = _(testList()).some(function (v) {
                return parseInt(ko.unwrap(v.Lock)) >= 3;
            });
            if (locked) {
                app.showMessage("Akaun anda sudah di halang sebab lebih 3 kali time out, Sila hubungi pihak urusetia untuk tindakan", "Sesi Ujian", ["OK"]);
                return false;
            }
            return true;
        },
        mulaSesiUjian = function (sesi) {
            var tcs = new $.Deferred();
            require(["viewmodels/arahan-mula-sesi-ujian-dialog", "durandal/app"], function (dialog, app2) {
                dialog.item(sesi);
                app2.showDialog(dialog)
                    .done(function (result) {
                        tcs.resolve(true);
                        if (result === "OK") {
                            router.navigate("#mula-sesi-ujian/" + ko.unwrap(sesi.Id));
                        }
                    });
            });
            return tcs.promise();
        },
        sambungSesiUjian = function (sesi) {
            var tcs = new $.Deferred();
            require(["viewmodels/arahan-mula-sesi-ujian-dialog", "durandal/app"], function (dialog, app2) {
                dialog.item(sesi);
                app2.showDialog(dialog)
                    .done(function (result) {
                        tcs.resolve(true);
                        if (result === "OK") {
                            router.navigate("#mula-sesi-ujian/" + ko.unwrap(sesi.Id));
                        }
                    });
            });
            return tcs.promise();
        };

    return {
        ujianTertangguh: ujianTertangguh,
        getTempohUjian: getTempohUjian,
        registrationList: registrationList,
        testList: testList,
        activate: activate,
        attached: attached,
        mulaSesiUjian: mulaSesiUjian,
        sambungSesiUjian: sambungSesiUjian,
        canDeactivate: canDeactivate,
        getNamaUjian: getNamaUjian,
        getTileClass: getTileClass
    };

});
