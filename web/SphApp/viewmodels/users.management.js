//
define([objectbuilders.datacontext, "viewmodels/_users.designation", "viewmodels/_users.department", "services/logger"], function (context, designationvm, departmentvm, logger) {
    var isBusy = ko.observable(false),
        roles = ["administrators", "AmbilUjian", "developers", "JanaLaporan", "PengurusanLookup", "PengurusanPengguna", "PengurusanPenggunaJabatan", "PengurusanPermohonan", "PengurusanPermohonanJabatan", "PengurusanSesiUjian", "PengurusanSoalan"],
        printprofile = ko.observable(new bespoke.sph.domain.Profile()),
        userprofile = ko.observable(new bespoke.sph.domain.Profile()),
        profiles = ko.observableArray(),
        departmentOptions = ko.observableArray(),
        designationOptions = ko.observableArray(),
        selectedUsers = ko.observableArray(),
        userNameValidationStatus = ko.observable(""),
        emailValidationStatus = ko.observable(""),
        importedSecuritySettingStoreId = ko.observable(""),
        password1 = ko.observable(""),
        password2 = ko.observable(""),
        isBusyValidatingUserName = ko.observable(false),
        isBusyValidatingEmail = ko.observable(false),
        loadDetails = function () {
            designationvm.activate(roles);
            departmentvm.activate();
        },
        map = function (item) {
            var p = new bespoke.sph.domain.Profile();
            p.IsNew(false);
            p.FullName(item.FullName());
            p.UserName(item.UserName());
            p.Email(item.Email());
            p.Designation(item.Designation());
            p.Department(item.Department());
            p.Telephone(item.Telephone());
            return p;
        },
        activate = function () {
            var query = String.format("Id ne '0'");
            var tcs = new $.Deferred();
            loadDetails();
            var profileTask = context.loadAsync("UserProfile", query);
            var designationTask = context.getListAsync("Designation", "Id ne '0'", "Name");
            var departmentTask = context.loadOneAsync("Setting", "Key eq 'Departments'");
            $.when(designationTask, profileTask, departmentTask)
             .then(function (s, p, d) {
                 isBusy(false);
                 if (s) {
                     designationOptions(s);
                 }
                 if (d) {
                     var departments = JSON.parse(ko.mapping.toJS(d.Value));;
                     departmentOptions(departments);
                 }
                 var list = _(p.itemCollection).map(map);
                 profiles(list); 
                 tcs.resolve(true);
             });
            return tcs.promise();
        },
        attached = function(view) {
            $(view).on("click", "input.select-user", function() {
                var cb = $(this),
                    user = ko.dataFor(this);
                if (cb.is(":checked")) {
                    selectedUsers.push(user);
                } else {
                    selectedUsers.remove(user);
                }
            });
        },
        userNameChaged = function (userName) {
            isBusyValidatingUserName(true);
            var tcs = new $.Deferred();
            var data = JSON.stringify({ userName: userName });
            isBusy(true);
            context.post(data, "/sph/Admin/ValidateUserName")
                .then(function (result) {
                    isBusy(false);
                    isBusyValidatingUserName(false);
                    if (result.status !== "OK") {
                        userNameValidationStatus(result.message);
                    }

                    tcs.resolve(result);
                });
            return tcs.promise();
        },
        emailChanged = function (email) {
            isBusyValidatingEmail(true);
            var tcs = new $.Deferred();
            var data = JSON.stringify({ email: email });
            isBusy(true);
            context.post(data, "/Sph/Admin/ValidateEmail")
                .then(function (result) {
                    isBusy(false);
                    isBusyValidatingEmail(false);
                    if (result.status !== "OK") {
                        emailValidationStatus(result.message);
                    }

                    tcs.resolve(result);
                });
            return tcs.promise();
        },
        save = function () {
            var data = ko.mapping.toJSON({ profile: userprofile });
            isBusy(true);

            return context.post(data, "/sph/Admin/AddUser")
                .then(function (result) {
                    isBusy(false);
                    var existing = _(profiles()).find(function (v) { 
                        return ko.unwrap(v.UserName) === ko.unwrap(result.profile.UserName); 
                    });
                    if (existing) {
                        profiles.replace(existing, result.profile);
                    } else {
                        profiles.push(result);
                    }
                });
        },
        add = function () {
            userprofile(new bespoke.sph.domain.Profile());
            userprofile().IsNew(true);
            userprofile().UserName("");
            userprofile().Email.subscribe(emailChanged);
            userprofile().UserName.subscribe(userNameChaged);
            require(["viewmodels/user.dialog", "durandal/app"],
                function (dialog, app2) {
                    dialog.profile(ko.unwrap(userprofile));
                    app2.showDialog(dialog).done(function(result) {
                        if (result === "OK") {
                            save(dialog.profile());
                        }
                    });
                });


            $("#user-details-modal").modal();
        },
        removeSelectedUsers = function () {
            var tasks = _(ko.unwrap(selectedUsers))
                .map(function(v) {
                    return context.sendDelete("/jpa-management/users/" + ko.unwrap(v.UserName));
                });
            return $.when(tasks);
        },
        editedProfile = ko.observable(),
        edit = function (user) {
            editedProfile(user);
            var c1 = ko.mapping.fromJSON(ko.mapping.toJSON(user));
            var clone = c1;
            userprofile(clone);
             require(["viewmodels/user.dialog", "durandal/app"],
                function (dialog, app2) {
                    dialog.profile(ko.unwrap(userprofile));
                    app2.showDialog(dialog).done(function(result) {
                        if (result === "OK") {
                            save(dialog.profile());
                        }
                    });
                });

        },
        resetPassword = function (user) {
            password1("");
            password2("");
            var c1 = ko.mapping.fromJSON(ko.mapping.toJSON(user));
            var clone = c1;
            userprofile(clone); 
             require(["viewmodels/user.reset.password.dialog", "durandal/app"],
                function (dialog, app2) {
                    dialog.profile(ko.unwrap(userprofile));
                    app2.showDialog(dialog).done(function(result) {
                        if (result === "OK") {
                            password1(dialog.password1());
                            password2(dialog.password2());
                            savePassword();
                        }
                    });
                });


            $("#user.resetpassword.dialog").modal();
        },
        savePassword = function () {
            if (!password1() && !password2()) {
                return Task.fromResult(false);
            };
            if (password1() !== password2()) {
                logger.logError("Password mismatch", this, this, true);
                return Task.fromResult(false);
            }

            var data = ko.mapping.toJSON({ userName: userprofile().UserName(), password: ko.unwrap(password1) });
            isBusy(true);

            return context.post(data, "/sph/Admin/ResetPassword")
                .then(function (result) {
                    isBusy(false);
                    if (result.OK) {
                        logger.info(result.messages);
                    } else {
                        logger.logError(result.messages, this, this, true);
                    }
                });


        },
        exportCommand = function () {
            window.open("/sph/admin/ExportSecuritySettings");
            return Task.fromResult(true);

        };

    importedSecuritySettingStoreId.subscribe(function (id) {
        context.post(JSON.stringify({ "id": id }), "/sph/admin/import/" + id)
        .done(function () {
            logger.info("All the settings has been imported");
            activate();
        });
    });

    var vm = {
        removeSelectedUsers: removeSelectedUsers,
        selectedUsers : selectedUsers,
        importedSecuritySettingStoreId: importedSecuritySettingStoreId,
        activate: activate,
        attached: attached,
        isBusyValidatingUserName: isBusyValidatingUserName,
        isBusyValidatingEmail: isBusyValidatingEmail,
        profiles: profiles,
        userprofile: userprofile,
        printprofile: printprofile,
        designationOptions: designationOptions,
        departmentOptions: departmentOptions,
        userNameValidationStatus: userNameValidationStatus,
        emailValidationStatus: emailValidationStatus,
        editCommand: edit,
        saveCommand: save,
        add: add,
        password1: password1,
        password2: password2,
        resetPasswordCommand: resetPassword,
        savePasswordCommand: savePassword,
        map: map,
        searchTerm: {
            department: ko.observable(),
            keyword: ko.observable()
        },
        toolbar: ko.observable({
            exportCommand: exportCommand,
            reloadCommand: function () {
                return activate();
            },
            printCommand: ko.observable({
                entity: ko.observable("UserProfile"),
                id: ko.observable(0),
                item: printprofile
            })
        })
    };


    return vm;


});
