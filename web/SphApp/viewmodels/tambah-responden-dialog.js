define(["plugins/dialog", "services/datacontext", "services/config"],
    function (dialog, context, config) {
        var item = ko.observable(),
            searchText = ko.observable(),
            results = ko.observableArray(),
            maxCount = ko.observable(),
            selectedRespondens = ko.observableArray(),
            senaraiPendaftaran = ko.observableArray(),
            searchAsync = function () {
                var query = {
                    "query": {
                        "filtered": {
                            "filter": {
                                "bool": {
                                    "must": []
                                }
                            }
                        }
                    },
                    "from": 0,
                    "size": 20
                };
                if (config.namaJabatan) {
                    if (config.namaJabatan) {
                        query.query.filtered.filter.bool.must.push({
                            term: { "NamaJabatan": config.namaJabatan }
                        });
                    }
                }

                if (ko.unwrap(searchText)) {
                    query.query = {
                        "query_string": {
                            "default_field": "_all",
                            "query": ko.unwrap(searchText)
                        }
                    };
                    query.filter = {
                        "bool": {
                            "must": []
                        }
                    };
                    if (config.namaJabatan) {
                        if (config.namaJabatan) {
                            query.filter.bool.must.push({
                                term: { "NamaJabatan": config.namaJabatan }
                            });
                        }
                    }
                }

                if (config.profile.Designation === "Urusetia") {
                    query.filter.bool.must = [{
                        term: { "IsResponden": true}
                    }];
                }
                return context.searchAsync({ entity: "Pengguna" }, query)
                    .done(function (lo) {
                        var list = _(lo.itemCollection).filter(function (v) {
                            var exist = _(senaraiPendaftaran()).find(function (k) {
                                return ko.unwrap(k.MyKad) === v.MyKad;
                            });
                            if (exist) {
                                return false;
                            }
                            return v.IsResponden === true;
                        });
                        results(list);
                        selectedRespondens.removeAll();
                    });


            },
            activate = function () {

                if (config.profile.Designation === "Urusetia") {

                    searchText("*");
                    return searchAsync();

                }
                return context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaKementerian")
                    .then(function (ministry) {
                        config.namaKementerian = ministry;
                        return context.getScalarAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaJabatan");
                    })
                    .then(function (jabatan) {
                        config.namaJabatan = jabatan;
                        return searchAsync();
                    });
            },
            attached = function (view) {
                $(view).on("click", "input[type=checkbox]", function (e) {
                    var pengguna = ko.dataFor(this);
                    if ($(this).is(":checked")) {
                        selectedRespondens.push(pengguna);
                    } else {
                        selectedRespondens.remove(pengguna);
                    }
                });
            },
            okClick = function (data, ev) {
                if (bespoke.utils.form.checkValidity(ev.target)) {
                    dialog.close(this, "OK");
                }
            },
            cancelClick = function () {
                dialog.close(this, "Cancel");
            },
            canExecuteSave = ko.computed(function () {
                return selectedRespondens().length > 0 && selectedRespondens().length <= maxCount();
            }),
            lebihBilangan = ko.computed(function () {
                return selectedRespondens().length > maxCount();
            });

        var vm = {
            selectedRespondens: selectedRespondens,
            senaraiPendaftaran: senaraiPendaftaran,
            maxCount: maxCount,
            canExecuteSave: canExecuteSave,
            lebihBilangan: lebihBilangan,
            searchText: searchText,
            searchAsync: searchAsync,
            results: results,
            activate: activate,
            attached: attached,
            permohonan: item,
            okClick: okClick,
            cancelClick: cancelClick
        };


        return vm;

    });
