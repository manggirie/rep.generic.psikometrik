define(["services/datacontext"], function(context){
    var permohonan = ko.observable()
        activate = function(entity){
            permohonan(entity);
            var tcs = new $.Deferred();
            setTimeout(function(){
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        attached  = function(view){
        
        },
        save = function(pendaftaran) {
    
            var data = ko.mapping.toJSON(pendaftaran);
    
            return context.post(data, "/PendaftaranProgram/TambahResponden")
                .then(function(result) {
                    app.showMessage("Your PendaftaranProgram has been successfully saved", "epsikologi", ["ok"]);
    
                });
            
        },
        addResponden = function(){
            
            var tcs = new $.Deferred();
            require(['viewmodels/tambah-responden-dialog' , 'durandal/app'], function (dialog, app2) {
                app2.showDialog(dialog)
                    .done(function (result) {
                        if (result === "OK") {
                            console.log(dialog.selectedRespondens());
                            var registrations = _(dialog.selectedRespondens()).map(function(v){
                                var r = new bespoke.epsikologi_pendaftaranprogram.domain.PendaftaranProgram(v);
                                r.NamaProgram(ko.unwrap(permohonan().NamaProgram));
                                r.NoPermohonan(ko.unwrap(permohonan().PermohonanNo));
                                r.PermohonanId(ko.unwrap(permohonan().Id));
                                r.NamaPengguna(v.Nama);
                                r.TarikhDaftar(moment().format("YYYY-MM-DD"));
                                return r;
                            });
                            console.log("registrations", ko.toJS(registrations));
                            var tasks = _(registrations).map(save);
                            $.when(tasks).done(tcs.resolve);
                            
                        }else{
                            tsc.resolve();
                        }
                });
            });
            
            return tcs.promise();
        };

    return {
        addResponden : addResponden,
        activate : activate,
        attached : attached
    };

});