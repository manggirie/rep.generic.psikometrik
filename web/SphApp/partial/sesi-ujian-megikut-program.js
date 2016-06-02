define(["services/datacontext"], function (context) {
    var activate = function(entity){
            
            var tcs = new $.Deferred();
            setTimeout(function(){
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        map = function(item){
            item.Jantina = ko.observable("...");
            context.getScalarAsync("Pengguna", "MyKad eq '" + item.MyKad + "'", "Jantina")
                .done(function (jantina) {
                    item.Jantina(jantina);
                });
            return item;
        },
        attached  = function(view){
        
        };

    return {
        activate : activate,
        attached : attached,
        map : map
    };

});