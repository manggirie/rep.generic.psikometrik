define(["services/datacontext", "services/logger", "plugins/router", objectbuilders.system, objectbuilders.app], function(context, logger, router, system, app){
    var activate = function(){
            
            return true;


        },
        attached  = function(view){
        
        };

    return {
        activate : activate,
        attached : attached
    };

});