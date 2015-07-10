define(["services/datacontext", "services/logger", "plugins/router", objectbuilders.system, objectbuilders.app], function(context, logger, router, system, app){
    var permohonanBaruCount = ko.observable("Please wait...."),
        soalanChart = ko.observable(),
        ujianBelumAmbil = ko.observable(),
        programSemsa = ko.observable(),
        respondenCount = ko.observable(),
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
            context.getCountAsync("SesiUjian","Status eq 'Belum Ambil'").done(function(result){
              ujianBelumAmbil(result);
            });
            //
            context.getCountAsync("Pengguna","IsResponden eq 1").done(respondenCount);
            //

            var date = moment().format('YYYY-MM-DD HH:mm:ss');
            context.getCountAsync("Permohonan","TarikhTamat ge '" + date +"'").done(function(result){
              programSemsa(result);
            });
        };

    return {
        activate : activate,
        permohonanBaruCount : permohonanBaruCount,
        soalanChart : soalanChart,
        ujianBelumAmbil:ujianBelumAmbil,
        programSemsa : programSemsa,
        respondenCount  : respondenCount,
        attached : attached
    };

});
