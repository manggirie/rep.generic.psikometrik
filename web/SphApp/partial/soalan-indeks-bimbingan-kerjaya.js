define(["services/datacontext", "services/logger"], function(context, logger){
    var activate = function(list){
            
           return true;


        },
        attached  = function(view){
            $(view).on("click", "a", function(e){
                var link = $(this),
                soalan = ko.dataFor(this);
                if(link.children("i.glyphicon-sort-by-order").length == 1){
                    e.preventDefault();
                    
                    require(['viewmodels/susunan-soalan' , 'durandal/app'], function (dialog, app2) {
                        dialog.item(soalan);
                        
                        app2.showDialog(dialog)
                            .done(function (result) {
                                if (!result) return;
                                if (result === "OK") {
                                    context.post(ko.toJSON(soalan), "/soalan/save")
                                    .done(function(){
                                        logger.info("Susunan saved!!")
                                    });
                                
                                }
                        });
                    });
                }
                
                
            })
        };

    return {
        activate : activate,
        attached : attached
    };

});