define([], function(){
    var activate = function(entity){
            if(entity.Id() === "0"){
                entity.No.subscribe(function(no){
                   entity.Id(no); 
                });
            }
            return true;


        },
        attached  = function(view){
        
        };

    return {
        activate : activate,
        attached : attached
    };

});