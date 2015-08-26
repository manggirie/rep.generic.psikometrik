define(["services/datacontext"], function(context){
    var mykad = ko.observable(),
        results = ko.observableArray(),
        activate = function(){


        },
        attached  = function(view){
            $(view).on("submit", "form", function(e){
              e.preventDefault();
              context.loadAsync("SesiUjian", "MyKad eq '" + ko.unwrap(mykad) + "' and Status eq 'Diambil'")
                .done(function (lo) {

                    var sesi = [];
                    _(lo.itemCollection).each(function (v) {
                        var ujian = ko.unwrap(v.NamaUjian),
                            indikator = !(ujian === "HLP" || ujian === "IBK" || ujian === "IP"),
                            trait = !(ujian === "UKBP-A" || ujian === "UKBP-B"|| ujian === "UKBP");
                        v.traitButton = ko.observable(trait);
                        v.indikatorButton = ko.observable(indikator);
                        if (ujian === "UKBP-A" || ujian === "UKBP-B") {

                        } else {
                            sesi.push(v);
                        }
                        if (ujian === "UKBP-A") {
                            v.NamaUjian("UKBP");
                            sesi.push(v);
                        }
                    });
                  results(sesi);
                });
            });
        };

    return {
        activate : activate,
        mykad : mykad,
        results : results,
        attached : attached
    };

});
