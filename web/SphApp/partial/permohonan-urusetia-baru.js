define([objectbuilders.config, "partial/permohonan-urusetia", objectbuilders.datacontext], function(config, formPartial, context){
    var activate = function(entity){
        
          
            entity.Penyelaras(config.userName);
            entity.StatusPermohonan("LULUS");
            
            return context.getCountAsync("Permohonan", "", "Id")
                .then(function(count){
                    var no = (count + 1).toString();
                    while(no.length < 5){
                        no = "0" + no;
                    }
                    entity.KodProgram(moment().format("YYYY") +"-" + no)
                    return formPartial.activate(entity);
                })
                
            
        },
        attached  = function(view){
             var tarikhMulaPicker = $("#tarikhMulaPicker").data("kendoDatePicker"),
                    tarikhTamatPicker = $("#tarikhTamatPicker").data("kendoDatePicker");
                tarikhMulaPicker.min(new Date());
                tarikhTamatPicker.min(new Date());

                return formPartial.attached(view);

        };

    return {
        activate : activate,
        attached : attached
    };

});