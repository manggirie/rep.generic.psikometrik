define(["services/datacontext", objectbuilders.config ], function(context, config){
    var testList = ko.observableArray(),
        activate = function(){
            
            return context.loadAsync("SesiUjian", String.format("MyKad eq '{0}'", config.userName))
                .done(function(lo){
                    testList(lo.itemCollection);
                });
        },
        attached  = function(view){
        
        };

    return {
        testList : testList,
        activate : activate,
        attached : attached
    };

});