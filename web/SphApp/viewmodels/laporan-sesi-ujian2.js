define(["services/datacontext", "services/logger", "plugins/router", objectbuilders.system, objectbuilders.app], function (context, logger, router, system, app) {
    var ujianOptions = ko.observableArray(),
      namaUjian = ko.observable(),
      programOptions = ko.observableArray(),
      tahunOptions = ko.observableArray(),
      namaProgram = ko.observable(),
      bil = ko.observable(),
      siri = ko.observable(),
      tahun = ko.observable(),
      tahunQuery = {
          "query": {
              "filtered": {
                  "filter": {
                      "bool": {
                          "must": [{
                              "term": {
                                  "StatusPermohonan": "LULUS"
                              }
                          }],
                          "must_not": []
                      }
                  }
              }
          },
          "sort": [],
          "aggs": {
              "category": {
                  "terms": {
                      "field": "TahunProgram",
                      "size": 0
                  }
              }
          },
          "from": 0,
          "size": 20
      },
      activate = function () {

          return context.getListAsync("Ujian", "UjianNo ne 'UKBP-A' and UjianNo ne 'UKBP-B'", "UjianNo")
            .then(function (list) {
                ujianOptions(list);
                ujianOptions.push("UKBP");
                return context.loadAsync({ entity: "Permohonan", size: 100}, "Id ne '0'", "NamaProgram");
            })
            .then(function (lo) {
               var items = _(lo.itemCollection).map(function(v){
                  v.NoKod = v.NamaProgram() +"/"+v.BilProgram()+"/"+v.SiriProgram()+"/"+v.TahunProgram();
                  v.Keterangan = v.KodProgram() + "  " + v.NoKod;
                  return v;
                });
                programOptions(items);
                return context.searchAsync("Permohonan", tahunQuery);
            })
            .then(function (result) {
                console.log(result);
                console.log(result.aggregations.category.buckets);
                var years = _(result.aggregations.category.buckets).map(function (v) {
                    return v.key;
                });
                tahunOptions(years);
            });


      },
      attached = function (view) {
      },
      generateLaporan = function () {
          var json = ko.toJSON({
              siri: siri,
              tahun: tahun,
              bil: bil,
              ujian: namaUjian,
              program: namaProgram
          });

          return $.ajax({
              type: "POST",
              data: json,
              url: "/urusetia-report/",
              contentType: "application/json; charset=utf-8",
              success: function (html) {
                  $("#report-panel").html(html);
              }
          });

      },
      generateExcel = function () {
          var json = ko.toJSON({
              siri: siri,
              tahun: tahun,
              bil: bil,
              ujian: namaUjian,
              program: namaProgram
          });

          return $.ajax({
              type: "POST",
              data: json,
              url: "cetak-laporan/xls/sesiujian/",
              contentType: "application/json; charset=utf-8",
              success: function (result) {
                  window.open("cetak-laporan/xls/" + result.path);
              }
          });

      };

    return {
        bil: bil,
        siri: siri,
        tahun: tahun,
        ujianOptions: ujianOptions,
        namaProgram: namaProgram,
        namaUjian: namaUjian,
        programOptions: programOptions,
        tahunOptions: tahunOptions,
        activate: activate,
        attached: attached,
        generateLaporan: generateLaporan,
        generateExcel: generateExcel
    };

});
