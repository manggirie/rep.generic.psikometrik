define(["services/datacontext", objectbuilders.app],
    function (context, app) {

        var activate = function (options) {
            var currentSection = options.currentSection || {},
                sesiUjian = options.sesiUjian,
                interval = options.interval,
                ujian = options.ujian,
                timer = options.timer,
                sections = options.sections,
                questionsCount = options.questionsCount,
                totalAnswered = options.totalAnswered;

            var runTimer = function (v) {
                if (interval) {
                    clearInterval(interval);
                }

                var hours = ko.unwrap(v.DurationHour),
                    minutes = ko.unwrap(v.DurationMinutes),
                    start = moment.duration(hours, "hours"),
                    tcs = new $.Deferred(),
                    sct = _(sections()).find(function (x) {
                        return x.section === v.Name();
                    });

                if (minutes === 1) {
                    start.add(40, "s");
                } else {
                    start.add(minutes * 60, "s");
                }

                // hide other sections
                currentSection(v.Name());

                interval = window.setInterval(function () {
                    start.subtract(1000);
                    timer(start.hours() + " jam " + start.minutes() + " minit   " + start.seconds() + " saat");
                    if (sct.questions().length <= sct.answered()) {

                        tcs.resolve();
                        return;
                    }

                    if (start.as("seconds") <= 0) {
                        clearInterval(interval);

                        context.post(ko.toJSON({}), "/sesi-ujian/timeout/" + ko.unwrap(sesiUjian().Id));
                        app.showMessage("Anda sudah kehabisan masa, anda akan di log keluar", "JPA ePsikometrik", ["OK"])
                            .done(function () {
                                totalAnswered(questionsCount());
                                router.navigate("responden-home");
                            });
                        tcs.reject();
                    }
                }, 1000);

                return tcs.promise();
            };

            var index = 0;
            var run = function () {
                var sct = ujian().SectionCollection()[index];
                if (typeof sct === "undefined" || !sct) {
                    return;
                }

                if (sct.Name() === "2" && ujian().UjianNo() === "UKBP-B") {
                    $.get("/arahan/ukbp-b.section-2.html")
                    .done(function (html) {
                        app.showMessage(html, "JPA ePsikometrik", ["OK"]).done(function () {
                            $("div.soalan-panel").each(function() {
                                var soalan = ko.dataFor(this);
                                if (ko.unwrap(soalan.SoalanNo) === "UKBP-B-00091") {
                                    $(this).before("<br/><strong>Sekiranya diberi peluang, saya ingin ...</strong>");
                                }
                                if (ko.unwrap(soalan.SoalanNo) === "UKBP-B-00090") {
                                    $(this).before("<br/><strong>Saya boleh ...</strong>");
                                }
                                if (ko.unwrap(soalan.SoalanNo) === "UKBP-B-00211") {
                                    $(this).before("<br/><strong>Saya meminati kerjaya sebagai seorang  ...</strong>");
                                }
                            });

                        });

                    });
                }
                runTimer(sct)
                        .fail()
                        .done(function () {
                            if (typeof ujian().SectionCollection()[index + 1] === "undefined") {
                                return;
                            }
                            app.showMessage("Adakah anda bersedia untuk menjawab soalan seksyen seterusnya?", "JPA ePsikometrik", ["Ya", "Tidak"])
                                  .done(function (dr) {
                                      if (dr === "Ya") {
                                          index++;
                                          run();
                                      } else {
                                          context.post(ko.toJSON(sesiUjian), "/sesi-ujian")
                                          .done(function () {
                                              router.navigate("responden-home");
                                          });
                                      }
                                  });
                        });
            };

            // get saved items
            $.getJSON("/sesi-ujian").done(function (ur) {
                if (!ur.success) {
                    run();
                    return;
                }
                if (!ur.sesi) {
                    run();
                    return;
                }
                if (ur.sesi.Id !== ko.unwrap(sesiUjian().Id)) {
                    run();
                    return;
                }
                // move to the section which was not yet answered
                var idx = 0;
                console.log("we got a hit here");
                _(ur.sesi.JawapanCollection).each(function (a) {
                    if (!a.JawapanPilihan && idx <= 0) {
                        // find the index of the question which has not been answered
                        var temp = -1;
                        _(sections()).each(function (v, i) {
                            if (a.SeksyenSoalan === ko.unwrap(v.section)) {
                                temp = i;
                            }
                        });
                        idx = temp;
                        return;
                    }
                    if (!a.JawapanPilihan) {
                        return;
                    }
                    var q = _(sesiUjian().JawapanCollection()).find(function (v) {
                        return a.SoalanNo === ko.unwrap(v.SoalanNo);
                    });
                    if (q) {
                        q.JawapanPilihan(a.JawapanPilihan);
                        q.Nilai(a.Nilai);
                    }
                });
                // mark the sections that has been answered
                for (var j = 0; j < idx; j++) {
                    var scj = sections()[j];
                    scj.answered(scj.questions().length);

                    totalAnswered(totalAnswered() + scj.questions().length);
                }
                index = idx;
                run();

            });
            return true;

        },
        attached = function (view) {

        };

        var vm = {
            activate: activate,
            attached: attached
        };


        return vm;

    });