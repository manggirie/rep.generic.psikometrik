define([objectbuilders.router], function(router){
    var activate = function(list){
            return true;


        },
        attached  = function(view){
            $(view).on("click", "a", function(e){
                var permohonan = ko.dataFor(this);
                if(typeof permohonan.TarikhTamat !== "string"){
                    return;
                }
                
                var tarikhTamat = moment(ko.unwrap(permohonan.TarikhTamat), "YYYY-MM-DDTHH:mm:ss");
                if(tarikhTamat < moment()){
                    e.preventDefault();
                    router.navigate("permohonan-penyelaras-readonly/" + ko.unwrap(permohonan.Id));
                }
                
            });
        };

    return {
        activate : activate,
        attached : attached
    };

});