define(["services/datacontext", objectbuilders.config, objectbuilders.app], function (context, config, app) {
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
                            pengguna().Jantina(result.data.Jantina);
                            pengguna().Umur(result.data.Umur);
                            pengguna().JenisPerkhidmatan(result.data.JenisPerkhidmatan);
                            pengguna().KumpulanJawatan(result.data.KumpulanJawatan);
                            pengguna().NamaJabatan(result.data.NamaJabatan);
                            pengguna().Skim(result.data.Skim);
                            pengguna().Bahagian(result.data.Bahagian);
                        }else{
                            
                            pengguna().fillManually(true);
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
            }
            context.searchAsync("Pengguna",query)
            .done(function (result) {
                if (result.rows > 0) {
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
