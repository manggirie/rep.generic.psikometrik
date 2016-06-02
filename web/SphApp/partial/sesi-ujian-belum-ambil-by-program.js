define(["services/datacontext"], function (context) {
    var activate = function(list){
            
            var tcs = new $.Deferred();
            setTimeout(function(){
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        attached  = function(view){
        
        },
        map =function(item){
            item.Jantina = ko.observable("...");
            context.getScalarAsync("Pengguna", "MyKad eq '" + item.MyKad + "'", "Jantina")
                .done(function (jantina) {
                    item.Jantina(jantina);
                });
            return item;
        };

    return {
        activate : activate,
        attached : attached,
        map:map
    };

});