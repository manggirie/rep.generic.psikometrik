define(["services/datacontext", "services/logger", "plugins/router", objectbuilders.system, objectbuilders.app], function (context, logger, router, system, app) {
    var ujianOptions = ko.observableArray(),
      namaUjian = ko.observable(),
      namaProgram = ko.observable(),
      tahun = ko.observable(),
      activate = function (PermohonanNo) {
          namaProgram(PermohonanNo);
          var tahunQuery = String.format("PermohonanNo eq '{0}'", PermohonanNo);
          context.loadOneAsync("Permohonan", tahunQuery)
              .done(function (permohonan) {
                  tahun(permohonan.TahunProgram())
              })

          return context.getListAsync("Ujian", "UjianNo ne 'UKBP-A' and UjianNo ne 'UKBP-B'", "UjianNo")
            .then(function (list) {
                ujianOptions(list);
                ujianOptions.push("UKBP");
                return context.loadAsync({ entity: "Permohonan", size: 100 }, "Id ne '0'", "NamaProgram");
            });

      },
      attached = function (view) {
      },
      generateLaporan = function () {
          var json = ko.toJSON({             
              tahun: tahun,
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
              tahun: tahun,
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
        tahun: tahun,
        ujianOptions: ujianOptions,
        namaProgram: namaProgram,
        namaUjian: namaUjian,
        activate: activate,
        attached: attached,
        generateLaporan: generateLaporan,
        generateExcel: generateExcel
    };

});
