define([], function(){
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
        addResponden = function(){
            require(['viewmodels/tambah-responden-dialog' , 'durandal/app'], function (dialog, app2) {
                app2.showDialog(dialog)
                    .done(function (result) {
                        if (!result) return;
                        if (result === "OK") {
                            
                            
                        
                        }
                });
            });
        };

    return {
        addResponden : addResponden,
        activate : activate,
        attached : attached
    };

});