define(["services/datacontext"], function(context){
    var activate = function(list){
            return true;

        },
        attached  = function(view){

        },
        map = function(p){

            var ujian = "";
            if(p.isIBK)ujian += "IBK, ";
            if(p.isIP)ujian += "IP, ";
            if(p.isIPU)ujian += "IPU, ";
            if(p.isISO)ujian += "ISO, ";
            if(p.isPPKP)ujian += "PPKP, ";
            if(p.isUKBP)ujian += "UKBP, ";
            if(p.isUKHLP)ujian += "HLP, ";

            p.senaraiUjian = ujian;
            p.sudahAmbil = ko.observable(",....");
            p.belumAmbil = ko.observable("...");
            context.getCountAsync("SesiUjian", "NamaProgram eq '" + p.PermohonanNo + "' and Status eq 'Diambil'").done(p.sudahAmbil);
            context.getCountAsync("SesiUjian", "NamaProgram eq '" + p.PermohonanNo + "' and Status eq 'Belum Ambil'").done(p.belumAmbil);
            return p;
        };
    return {
        activate : activate,
        attached : attached,
        map : map

    };

});
