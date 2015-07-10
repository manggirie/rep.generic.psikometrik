define(["services/datacontext"], function(context){
    var mykad = ko.observable(),
        results = ko.observableArray(),
        activate = function(){


        },
        attached  = function(view){
            $(view).on("submit", "form", function(e){
              e.preventDefault();
              context.loadAsync("SesiUjian", "MyKad eq '" + ko.unwrap(mykad) + "'")
                .done(function(lo){
                  results(lo.itemCollection);
                });
            });
        };

    return {
        activate : activate,
        mykad : mykad,
        results : results,
        attached : attached
    };

});
