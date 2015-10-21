define(["services/datacontext", objectbuilders.app], function (context, app) {

    function toTitleCase(str) {
        return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
    }

    var permohonan = ko.observable(),
        activate = function (entity) {
            permohonan = entity;

            ko.extenders.toTitleCase = function (target, option) {
                target.subscribe(function (newValue) {
                    target(toTitleCase(newValue));
                });
                return target;
            };

            entity.NamaProgram.extend({ toTitleCase: true });

            return true;
        },
        assignNo = function () {
            var no = String.format("{0}/{1}/{2}/{3}",
                ko.unwrap(permohonan.NamaProgram),
                ko.unwrap(permohonan.BilProgram),
                ko.unwrap(permohonan.SiriProgram),
                ko.unwrap(permohonan.TahunProgram));

            context.getCountAsync("Permohonan", String.format("Id ne '{0}' and PermohonanNo eq '{1}'", ko.unwrap(permohonan.Id), no))
            .done(function (c) {
                if (c > 0) {
                    app.showMessage("Program ini sudah wujud <br/>" + no, "Warning", ["OK"])
                      .done(function () {

                      });

                }
                console.log(c);
            });
        },
        attached = function (view) {
            permohonan.StatusPermohonan.subscribe(assignNo);
            permohonan.NamaProgram.subscribe(assignNo);
            permohonan.BilProgram.subscribe(assignNo);
            permohonan.SiriProgram.subscribe(assignNo);
            permohonan.TahunProgram.subscribe(assignNo);

            $("NamaProgram").css("text-transform", "capitalize");
        };

    return {
        activate: activate,
        attached: attached
    };

});