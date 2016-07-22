define(["services/datacontext", objectbuilders.config, objectbuilders.app], function (context, config, app) {

    function parseMyKadDate(str) {
        var y = str.substr(0, 2),
            m = str.substr(2, 2) - 1,
            d = str.substr(4, 2);

        // adjust year
        var thisYear = new Date().getFullYear().toString();
        y = (Number(y) <= Number(thisYear.substr(2, 2))) ? "20" + y : "19" + y;

        // gnerate D.O.B
        var D = new Date(y, m, d);

        var today = new Date();
        return (D.getFullYear() == y && D.getMonth() == m && D.getDate() == d) ? D : str;
    }

    function calculateAge(birthday) {
        if (typeof birthday === "object") {
            var ageDifMs = Date.now() - birthday.getTime();
            var ageDate = new Date(ageDifMs);
            return Math.abs(ageDate.getUTCFullYear() - 1970);
        } else {
            app.showMessage("Gagal mendapatkan umur dari 6 digit pertama MyKad " + birthday, "Sistem Ujian Psikometrik", ["OK"]);
            return 0;
        }
    }

    var isPenyelaras = ko.observable(false),
        pengguna = ko.observable(),
        penyelaras = ko.observable(),
        checkMyKad = function (ic) {
            console.log(ic);
            context.getCountAsync("Pengguna", String.format("MyKad eq '{0}'", ic), "MyKad")
            .done(function (count) {
                if (count > 0) {
                    app.showMessage("Pengguna dengan MyKad " + ic + " sudah wujud", "Sistem Ujian Psikometrik", ["OK"])
                      .done(function () {

                      });

                }
                else
                {
                    var param = JSON.stringify({ icno: ic });
                    context.post(param, "hrmis/GetUserDetailsByIcNo").done(function (result) {
                        if (result.data) {
                            pengguna().fillManually(false);
                            pengguna().Pekerjaan('Kerajaan');
                            pengguna().Nama(result.data.Nama);
                            pengguna().Emel(result.data.Emel);
                            pengguna().StatusPerkahwinan(result.data.StatusPerkahwinan);
                            pengguna().Telefon(result.data.Telefon);
                            if (result.data.Jantina === "L" || result.data.Jantina === "P") {
                                pengguna().Jantina(result.data.Jantina === "L" ? "Lelaki" : "Perempuan");
                            } else {
                                pengguna().Jantina(result.data.Jantina);
                            }
                            pengguna().Umur(result.data.Umur);
                            pengguna().JenisPerkhidmatan(result.data.JenisPerkhidmatan);
                            pengguna().KumpulanJawatan(result.data.KumpulanJawatan);
                            pengguna().NamaJabatan(result.data.NamaJabatan);
                            pengguna().Skim(result.data.Skim);
                            pengguna().Bahagian(result.data.Bahagian);
                        }else{

                            pengguna().fillManually(true);
                            pengguna().Umur(calculateAge(parseMyKadDate(ic)));
                        }

                    });
                }
            });

        },
        checkEmail = function (email) {
            console.log(email);
            var query = {
                "filter": {
                    "or": [
                       {
                           "term": {
                               "Emel": email
                           }
                       },
                       {
                           "term": {
                               "Emel2": email
                           }
                       }
                    ]
                }
            };

            $.getJSON("/jpa-management/users/email/" + email, function(x){

                if (x.exist) {
                    app.showMessage("Pengguna dengan emel " + email + " sudah wujud", "Sistem Ujian Psikometrik", ["OK"]);
                }
            });

            context.searchAsync("Pengguna",query)
              .done(function(sr){
                if (sr.rows) {
                    app.showMessage("Pengguna dengan emel " + email + " sudah wujud", "Sistem Ujian Psikometrik", ["OK"]);
                }
              });


        },
        activate = function (entity) {
            entity.fillManually = ko.observable(true);

            pengguna(entity);
            if (ko.unwrap(entity.Id) === "0") {
                entity.MyKad.subscribe(checkMyKad);
                entity.Emel.subscribe(checkEmail);
                entity.Emel2.subscribe(checkEmail);
            }

            return context.loadOneAsync("Pengguna", String.format("MyKad eq '{0}'", config.userName))
                .done(function (user) {

                    if (!user) {
                        return;
                    }
                    console.log(user);
                    if (typeof ko.unwrap(user) === "undefined") {
                        return;
                    }

                    if (typeof user.IsPenyelaras === "undefined") {
                        return;
                    }
                    if (ko.unwrap(user.IsPenyelaras)) {
                        console.log("OK");
                        entity.JenisPerkhidmatan(ko.unwrap(user.JenisPerkhidmatan));
                        entity.NamaKementerian(ko.unwrap(user.NamaKementerian));
                        entity.NamaJabatan(ko.unwrap(user.NamaJabatan));
                        entity.IsResponden(true);
                        isPenyelaras(true);
                        penyelaras(user);
                    }
                });


        },
        attached = function (view) {
            $("select.required").attr("required", "");
        },
        canExecuteSaveCommand = function () {
            if (!pengguna()) return false;
            return pengguna().IsPenyelaras() || pengguna().IsResponden();
        };

    return {
        isPenyelaras: isPenyelaras,
        canExecuteSaveCommand: canExecuteSaveCommand,
        penyelaras: penyelaras,
        activate: activate,
        attached: attached
    };

});
