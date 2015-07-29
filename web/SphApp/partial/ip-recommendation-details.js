define([], function(){
    var activate = function(entity){
            
            if(entity.Id() === "0"){
                entity.Skor.subscribe(function(skor){
                    entity.Id(skor);
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