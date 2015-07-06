define(["services/datacontext"], function(context){
    var activate = function(list){
            
            _(list).each(function(v){
            
                v.namaPenyelaras  = ko.observable();
                getNamaPenyelaras(v);
            });
            var tcs = new $.Deferred();
            setTimeout(function(){
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        attached  = function(view){
        
        },
        map = function(p){
            
            p.namaPenyelaras  = ko.observable("...");
            context.getScalarAsync("Pengguna", "MyKad eq '" + p.Penyelaras + "'", "Nama")
                    .done(function(nama){
                        p.namaPenyelaras(nama);
                    });
            
            return p;
        };

    return {
        activate : activate,
        map : map,
        attached : attached
    };

}); 