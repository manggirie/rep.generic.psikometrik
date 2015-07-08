define([], function(){
    var n = false,
        activate = function(entity){
            n = entity.Id() === "0";
            entity.No.subscribe(function(no){
                if(n){
                    entity.Id(no);
                }
            });
            return true;

        },
        attached  = function(view){

        };

    return {
        activate : activate,
        attached : attached
    };

});
