define(["services/datacontext", "services/logger", "plugins/router", objectbuilders.system, objectbuilders.app], function(context, logger, router, system, app){
    var permohonanBaruCount = ko.observable("Please wait...."),
        activate = function(){

            return true;
        },
        attached  = function(view){
            // http://localhost:50230/Sph/EntityView/Count/permohonan-penyelaras
            $.get("Sph/EntityView/Count/permohonan-penyelaras").done(function(result){
              permohonanBaruCount(result.hits.total);
            });
        };

    return {
        activate : activate,
        permohonanBaruCount : permohonanBaruCount,
        attached : attached
    };

});
