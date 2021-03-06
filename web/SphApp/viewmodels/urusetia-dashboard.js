define(["services/datacontext", "services/logger", "plugins/router", objectbuilders.system, objectbuilders.app], function(context, logger, router, system, app){
    var permohonanBaruCount = ko.observable("Please wait...."),
        soalanChart = ko.observable(),
        ujianBelumAmbil = ko.observable(),
        ujianBelumAmbilQuery = {
            "query": {
                "filtered": {
                    "filter": {
                        "bool": {
                            "must": [{
                                "term": {
                                    "StatusPermohonan": "LULUS"
                                }
                            }, {
                                "range": {
                                    "TarikhTamat": {
                                        "from": moment().format("YYYY-MM-DDTHH:mm:ss")
                                    }
                                }
                            }

                            ],
                            "must_not": [

                            ]
                        }
                    }
                }
            }
        },
        programSemasa = ko.observable(),
        respondenCount = ko.observable(),
        programSemasaQuery = {
            "query": {
                "filtered": {
                    "filter": {
                        "bool": {
                            "must": [{
                                "term": {
                                    "StatusPermohonan": "LULUS"
                                }
                            }, {
                                "range": {
                                    "TarikhTamat": {
                                        "to": moment().format("YYYY-MM-DDTHH:mm:ss")
                                    }
                                }
                            }

                            ],
                            "must_not": [

                            ]
                        }
                    }
                }
            },
            "fields": ["ProgramNo"]
        },
        activate = function(){

            return context.loadOneAsync("EntityChart", "Id eq 'soalandetail-ujian-2'")
            .done(soalanChart);
        },
        attached  = function(view){
            //
            $.get("Sph/EntityView/Count/permohonan-penyelaras").done(function(result){
              permohonanBaruCount(result.hits.total);
            });
            //
            context.searchAsync("Permohonan",ujianBelumAmbilQuery).done(function(result) {
                console.log("ujianBelumAmbil", result);
                ujianBelumAmbil(result.rows);
            });
            //
            context.getCountAsync("Pengguna","IsResponden eq 1").done(respondenCount);
            //

            context.searchAsync("Permohonan",programSemasaQuery).done(function(result) {
                console.log("programSemasa", result);
                programSemasa(result.rows);
            });
        };

    return {
        activate : activate,
        permohonanBaruCount : permohonanBaruCount,
        soalanChart : soalanChart,
        ujianBelumAmbil:ujianBelumAmbil,
        programSemasa : programSemasa,
        respondenCount  : respondenCount,
        attached : attached
    };

});
