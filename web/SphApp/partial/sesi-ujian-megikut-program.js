define([], function(){
    var activate = function(entity){
            
            var tcs = new $.Deferred();
            setTimeout(function(){
                tcs.resolve(true);
            }, 500);

            return tcs.promise();


        },
        map = function(item){
            item.Jantina = "L";
            
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