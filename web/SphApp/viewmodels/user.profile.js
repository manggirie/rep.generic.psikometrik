

    define([objectbuilders.datacontext],
        function (context) {
            var isBusy = ko.observable(false),
            userName = ko.observable("urusetia"),
            userProfile = ko.observable(new bespoke.sph.domain.UserProfile()),
            startModule = ko.observable("responden-home"),
            startModuleOptions = ko.observableArray(["laporan-sesi-ujian","solution.diagnostics","ujian-all","kementerians-all","jabatans-all","programlookups-all","permohonans-all","pengguna-all","sesiujians-all","senarai_program_pengguna","soalan-indeks-bimbingan-kerjaya","indikator-perwatakan-unggul","inventori-stres-organisasi","listprogram","penyelaras","viewpermohonanpenyelaras","permohonan_penyelaras_lulus","responden","senarai-jabatan-mengikut-kementerian","soalandetail","testviewpenyelaras","urusetia","jabatan","kementerian","pendaftaranprogram","pengguna","permohonan","programlookup","sesiujian","soalan","ujian"]),
            languageOptions = ko.observableArray(),
            activate = function () {
                var query = String.format("UserName eq '{0}'", userName()),
                    tcs = new $.Deferred(),
                    loadTask = context.loadOneAsync("UserProfile", query),
                    languageOptionsTask = $.getJSON("/i18n/options");
                $.when(loadTask, languageOptionsTask).done(function (b, langs) {
                    if (b)
                        userProfile(b);
                    else
                        userProfile(new bespoke.sph.domain.UserProfile());
                    var lang = langs[0],
                        options = [];
                    for (var code in lang) {
                        if (lang.hasOwnProperty(code)) {
                            options.push({ code: code, display: lang[code] });
                        }
                    }
                    languageOptions(options);
                    tcs.resolve(true);
                });

                return tcs.promise();
            },
            saveAsync = function () {
                var json = ko.toJSON(userProfile);
                return context.post(json, "/jpa-account/profile");
            };

            var vm = {
                activate: activate,
                userProfile: userProfile,
                startModule: startModule,
                startModuleOptions: startModuleOptions,
                languageOptions: languageOptions,
                toolbar: {
                    saveCommand: saveAsync
                },
                isBusy: isBusy,
                title: "User profile Details"
            };

            return vm;
        });
