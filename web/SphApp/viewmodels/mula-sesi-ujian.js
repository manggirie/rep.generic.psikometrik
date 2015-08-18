define(["services/datacontext", objectbuilders.app, objectbuilders.config, objectbuilders.router, "services/duration-section", "services/duration-bahagian"],
    function (context, app, config, router, sectionDuration, bahagianDuration) {
        var sesiUjian = ko.observable(),
            ujian = ko.observable(),
            pendaftaran = ko.observable(),
            hideAnswered = ko.observable(),
            permohonan = ko.observable(),
            totalAnswered = ko.observable(0),
            questionsCount = ko.observable(0),
            interval = null,
            timer = ko.observable(),
            sections = ko.observableArray(),
            currentSection = ko.observable(),
            mapJawapan = function (v) {
                var questions = ko.observableArray(),
                    section = _(sections()).find(function (s) {
                        return s.section === ko.unwrap(v.SeksyenSoalan);
                    });
                if (section) {
                    questions = section.questions;
                } else {
                    sections.push({
                        section: ko.unwrap(v.SeksyenSoalan),
                        questions: questions,
                        answered: ko.observable(0),
                        header: ko.observable()
                    });
                }

                var answer = {
                    "$type": "Bespoke.epsikologi_sesiujian.Domain.Jawapan, epsikologi.SesiUjian",
                    "JawapanPilihan": ko.observable(),
                    "SeksyenSoalan": ko.unwrap(v.SeksyenSoalan),
                    "SoalanNo": ko.unwrap(v.SoalanNo),
                    "Trait": ko.unwrap(v.Trait),
                    "Text": ko.unwrap(v.TeksSoalan),
                    "Nilai": ko.observable(),
                    "PilihanJawapanCollection": v.PilihanJawapanCollection
                };
                answer.Visible = ko.computed(function () {
                    if (currentSection()) {
                        if (currentSection() !== ko.unwrap(this.SeksyenSoalan)) {
                            return false;
                        }
                    }
                    if (!this.JawapanPilihan()) {
                        return true;
                    }
                    if (hideAnswered()) {
                        return false;
                    }
                    return true;
                }, answer);
                questions.push(answer);

                return answer;
            },
            activate = function (id) {

                sections.removeAll();
                totalAnswered(0);
                questionsCount(0);

                var $f = "",
                    soalanOption = {
                        entity: "Soalan",
                        size: 200,
                        includeTotal: true,
                        orderby: "Susunan",
                        page: 1
                    };

                return context.loadOneAsync("SesiUjian", String.format("Id eq '{0}'", id))
                    .then(function (a) {
                        sesiUjian(a);
                        return context.loadOneAsync("Ujian", String.format("UjianNo eq '{0}'", ko.unwrap(sesiUjian().NamaUjian)));
                    })
                    .then(function (b) {
                        ujian(b);
                        var $q = String.format("NamaUjian eq '{0}'", ko.unwrap(ujian().Id));
                        $f = encodeURIComponent($q);
                        return context.loadAsync(soalanOption, $f);
                    })
                    .then(function (qLo) {
                        sesiUjian().JawapanCollection.removeAll();
                        var questions = _(qLo.itemCollection).map(mapJawapan);
                        sesiUjian().JawapanCollection(questions);
                        questionsCount(qLo.rows);

                        if (qLo.rows > qLo.size) {
                            soalanOption.page = 2;
                            return context.loadAsync(soalanOption, $f);
                        }
                        return Task.fromResult(0);
                    })
                    .then(function (qLo) {
                        if (!qLo) {
                            return;
                        }
                        var questions = _(qLo.itemCollection).map(mapJawapan);
                        _(questions).each(function (v) {
                            sesiUjian().JawapanCollection.push(v);
                        });
                        // TODO: assume it's only 400 max, well not for UKBP which is more than 500 questions
                    });


            },
            durationForSection = function () {
                sectionDuration.activate({
                    currentSection: currentSection,
                    sesiUjian: sesiUjian,
                    interval: interval,
                    ujian: ujian,
                    sections: sections,
                    totalAnswered: totalAnswered,
                    questionsCount: questionsCount,
                    timer: timer
                });
            },
            attached = function (view) {
                $(view).on("click", "input[type=radio]", function () {
                    var panel = $(this).parents("div.soalan-panel"),
                        soalan = ko.dataFor(panel[0]),
                        answer = ko.dataFor(this);

                    var modify = ko.unwrap(soalan.JawapanPilihan);

                    soalan.JawapanPilihan(ko.unwrap(answer.Teks));
                    soalan.Nilai(ko.unwrap(answer.Nilai));

                    if (!modify) {
                        totalAnswered(ko.unwrap(totalAnswered) + 1);
                        var section = _(sections()).find(function (s) {
                            return s.section === ko.unwrap(soalan.SeksyenSoalan);
                        });
                        section.answered(ko.unwrap(section.answered) + 1);
                    }

                });

                $(view).on("contextmenu", function (e) {
                    e.preventDefault();
                });

                $(view).attr("unselectable", "on")
                     .css({
                         '-moz-user-select': "none",
                         '-o-user-select': "none",
                         '-khtml-user-select': "none",
                         '-webkit-user-select': "none",
                         '-ms-user-select': "none",
                         'user-select': "none"
                     }).bind("selectstart", function () { return false; });

                if (ko.unwrap(ujian().DurationHour) === null) {
                    durationForSection();
                    return;
                }

                var hours = ko.unwrap(ujian().DurationHour),
                    minutes = ko.unwrap(ujian().DurationMinutes),
                    start = moment.duration(hours, "hours");
                start.add(minutes * 60, "s");

                interval = window.setInterval(function () {
                    start.subtract(1000);
                    timer(start.hours() + " jam " + start.minutes() + " minit   " + start.seconds() + " saat");
                    if (start.as("seconds") <= 0) {
                        clearInterval(interval);

                        context.post(ko.toJSON({}), "/sesi-ujian/timeout/" + ko.unwrap(sesiUjian().Id));
                        app.showMessage("Anda sudah kehabisan masa, anda akan di log keluar", "JPA ePsikometrik", ["OK"])
                            .done(function () {
                                totalAnswered(questionsCount());
                                router.navigate("responden-home");
                            });
                    }
                }, 1000);

                // section header
                _(ujian().SectionCollection()).each(function (v) {
                    if (!ko.unwrap(v.Header)) {
                        return;
                    }
                    var sct = _(sections()).find(function (k) {
                        return k.section === ko.unwrap(v.Name);
                    });
                    if (sct) {
                        sct.header(ko.unwrap(v.Header));
                    }
                });

            },
            detached = function () {
                clearInterval(interval);
                timer("");
            },
            canDeactivate = function () {
                var tcs = new $.Deferred();
                if (totalAnswered() < questionsCount()) {
                    app.showMessage("Adakah and ingin meninggalkan sesi ujian ini", "Tinggal Sesi Ujian", ["Ya", "Tidak"])
                        .done(function (dialogResult) {
                            tcs.resolve(dialogResult === "Ya");
                        });
                } else {
                    return true;
                }
                return tcs.promise();
            },
            findPos = function (obj) {
                var curtop = 0;
                if (obj.offsetParent) {
                    do {
                        curtop += obj.offsetTop;
                    } while (obj = obj.offsetParent);
                    return [curtop];
                }
            },
            goToSection = function (sc) {
                window.scroll(0, findPos(document.getElementById(sc.section)));

            },
            submitSesiUjian = function () {

                return context.post(ko.toJSON(sesiUjian), "/sesiujian/submitsesiujian")
                    .done(function () {

                        clearInterval(interval);
                        app.showMessage("Sesi Ujian anda sudah berjaya di hantar", "JPA ePsikometrik", ["OK"])
                            .done(function () {
                                router.navigate("responden-home");
                            });

                    });
            };

        return {
            hideAnswered: hideAnswered,
            submitSesiUjian: submitSesiUjian,
            goToSection: goToSection,
            sections: sections,
            detached: detached,
            canDeactivate: canDeactivate,
            timer: timer,
            currentSection: currentSection,
            questionsCount: questionsCount,
            totalAnswered: totalAnswered,
            sesiUjian: sesiUjian,
            ujian: ujian,
            pendaftaran: pendaftaran,
            permohonan: permohonan,
            activate: activate,
            attached: attached,
            bahagianDuration: bahagianDuration,
            sectionDuration: sectionDuration
        };

    });
