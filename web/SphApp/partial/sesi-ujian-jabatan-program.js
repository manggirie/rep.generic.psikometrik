define([objectbuilders.app, "services/datacontext", objectbuilders.config], function (app, context, config) {
    var map = function (v) {
        if (v.NamaUjian === "UKBP-A") {
            v.NamaUjian = "UKBP";
        }
        if (v.NamaUjian === "UKBP-B") {
            v.NamaUjian = "UKBP";
        }
        return v;
    },
        activate = function (list) {

            var items = list(),
                mapping = false;
            list.subscribe(function (changes) {

                if (mapping) {
                    return;
                }



                items = list();
                mapping = true;

                var baskets = [],
                    filterdItems = [];

                setTimeout(function () {

                    _(items).each(function (v) {
                        var key = v.NamaUjian + v.NamaProgram + v.MyKad;
                        if (baskets.indexOf(key) === -1) {
                            baskets.push(key);
                            filterdItems.push(v);
                        }
                    });
                    list(filterdItems);
                    mapping = false;
                }, 250);

            }, null, "arrayChange");

            return true;

        },
        sendReminderEmail = function (sesi, permohonanId) {
            var to = "";
            return context.loadOneAsync("Pengguna", String.format("MyKad eq '{0}'", ko.unwrap(sesi.MyKad)))
                .then(function (user) {
                    to = ko.unwrap(user.Emel);
                    return $.get("/email-template/generate/Permohonan/" + ko.unwrap(permohonanId) + "/peringatan-kepada-responden");
                })
                .then(function (mail) {
                    var message = {
                        subject: mail.subject,
                        body: mail.body,
                        to: to
                    };
                    return context.post(JSON.stringify(message), "/email-template/send");
                }).then(function () {
                    app.showMessage("Emel peringatan sudah dihantar kepada " + to, config.applicationFullName, ["OK"]);
                });


        },
        attached = function (view) {
            $(view).on("click", "a", function (e) {
                var data = ko.dataFor(this);
                if (typeof data.MyKad !== "string") {
                    return 0;
                }
                e.preventDefault();
                // do the email
                return context.getScalarAsync("Permohonan", "PermohonanNo eq '" + data.NamaProgram + "'", "Id")
                .then(function (permohonanId) {
                    return sendReminderEmail(data, permohonanId);
                });
            });
        };

    return {
        activate: activate,
        attached: attached,
        map: map
    };

});
