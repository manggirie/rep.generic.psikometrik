define(["services/datacontext", "services/logger", "plugins/router", objectbuilders.system, objectbuilders.app], function(context, logger, router, system, app){
    var ujianOptions = ko.observableArray(),
        namaUjian = ko.observable(),
        programOptions = ko.observableArray(),
        namaProgram = ko.observable(),
        bil = ko.observable(),
        siri = ko.observable(),
        tahun = ko.observable(),
        activate = function(){
            
            return context.getListAsync("Ujian", "Id ne '0'", "UjianNo")
            .then(function(list){
                ujianOptions(list);
                return context.getListAsync("ProgramLookup", "Id ne '0'", "NamaProgram");
            })
            .then(programOptions);


        },
        attached  = function(view){
        
        },
        generateLaporan = function(){
                var json = ko.toJSON({
                siri : siri,
                tahun : tahun,
                bil : bil,
                ujian : namaUjian,
                program : namaProgram
            });

        return $.ajax({
                type: "POST",
                data: json,
                url: "/urusetia-report/",
                contentType: "application/json; charset=utf-8",
                success: function(html){
                   $("#report-panel").html(html); 
                }
            });

        };

    return {
        bil :bil,
        siri : siri,
        tahun : tahun,
        ujianOptions : ujianOptions,
        namaProgram : namaProgram,
        namaUjian : namaUjian,
        programOptions : programOptions,
        activate : activate,
        attached : attached,
        generateLaporan : generateLaporan
    };

});