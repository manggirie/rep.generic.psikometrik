define([objectbuilders.datacontext, objectbuilders.config], function (context, config) {
    var namaJabatan = ko.observable(),
        namaKementerian = ko.observable(),
        activate = function (list) {

            return context.getTuplesAsync("Pengguna", "MyKad eq '" + config.userName + "'", "NamaJabatan", "NamaKementerian")
                .done(function (lo) {
                    var x = lo[0];
                    config.namaJabatan = x.Item1;
                    config.namaKementerian = x.Item2;
                    namaJabatan(x.Item1);
                    namaKementerian(x.Item2);
                });

        },
        attached = function () {

        };

    return {
        namaJabatan: namaJabatan,
        namaKementerian: namaKementerian,
        activate: activate,
        attached: attached
    };

});