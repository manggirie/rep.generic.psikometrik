define([], function () {
    var map = function (v) {
        if (v.NamaUjian === "UKBP-A") {
            v.NamaUjian = "UKBP";
        }
        if (v.NamaUjian === "UKBP-B") {
            v.NamaUjian = "UKBP";
        }
        return v;
    }, activate = function (list) {
        var items = list(),
            mapping = false;

        list.subscribe(function (changes) {

            if (mapping) {
                return;
            }



            items = list();
            mapping = true;

            var baskets = [],
                filterdItems = [];

            setTimeout(function () {

                _(items).each(function (v) {
                    var key = v.NamaUjian + v.NamaProgram + v.MyKad;
                    if (baskets.indexOf(key) === -1) {
                        baskets.push(key);
                        filterdItems.push(v);
                    }
                });
                list(filterdItems);
                mapping = false;
            }, 250);

        }, null, "arrayChange");

        return true;


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
        map: map,
        attached: attached
    };

});
