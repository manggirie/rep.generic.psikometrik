define([], function () {
    var activate = function (list) {

        var tcs = new $.Deferred();
        setTimeout(function () {
            tcs.resolve(true);
        }, 500);

        return tcs.promise();


    },
        attached = function (view) {
            $(view).on("click", "a", function (e) {
                var data = ko.dataFor(this);
                if (typeof data.NamaUjian !== "string") {
                    return;
                }

                e.preventDefault();
                var url = "/cetak-laporan/trait/" + data.NamaUjian + "/" + data.Id;
                switch (data.NamaUjian) {
                    case "PPKP":
                        url = "/cetak-laporan/ppkp/profile/" + data.Id;
                        break;
                    case "UKBP-A":
                    case "UKBP-B":
                        url = "/cetak-laporan/indikator/ukbp/" + data.Id;
                        break;
                    default:
                }
                window.open(url);
            });
        };

    return {
        activate: activate,
        attached: attached
    };

});
