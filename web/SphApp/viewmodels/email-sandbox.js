define([], function () {
    var email = {
        recipient: ko.observable(),
        sender: ko.observable(),
        subject: ko.observable(),
        message: ko.observable()
    },
    isBusy = ko.observable(false),
    emailSendStatus = ko.observable(),
    textType = ko.observable(),
    sendEmail = function () {
        emailSendStatus(null);
        textType(null);
        if ($('#email-sandbox-form').valid()) {
            isBusy(true);
            console.log(
                "\n"
                + "To: " + email.recipient() + "\n"
                + "From: " + email.sender() + "\n"
                + "Subject: " + email.subject() + "\n"
                + "Message: " + email.message() + "\n"
                );

            var json = ko.toJSON({
                recipient: email.recipient(),
                sender: email.sender(),
                subject: email.subject(),
                message: email.message()
            });

            return $.ajax({
                type: "POST",
                data: json,
                url: "/sandbox/email/",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    console.log(result.status);
                    if (result.success) {
                        textType("text-success");
                    } else {
                        textType("text-danger");
                    }
                    emailSendStatus(result.status);
                    isBusy(false);
                },
                error: function (xhr, textStatus, error) {
                    console.log("Status: " + xhr.status + " Status Text: " + xhr.statusText);
                    textType("text-danger");
                    emailSendStatus(xhr.statusText);
                    isBusy(false);
                }
            });
        } else {
            console.log("Form validation fail!")
        }
    },
    activate = function () {
        email.recipient("nazrulhisham@bespoke.com.my");
        email.sender("psikometrik-noreply@jpa.gov.my");
        email.subject("ePsikometrik Email Sandbox");
        email.message("This is a test e-mail from ePsikometrik.\nIf you receive this email, ePsikometrik smtp mail server is up and smtp mail settings are okay!");
    },
    attached = function () {
        $("#email-sandbox-form").validate({
            rules: {
                sandboxRecipient: "required",
                sandboxSender: "required",
                sandboxSubject: "required",
                sandboxMessage: "required",
            },
            messages: {
                sandboxRecipient: "Recipient is required!",
                sandboxSender: "Sender is required!",
                sandboxSubject: "Subject is required!",
                sandboxMessage: "Message is required!",
            }
        });
    };

    return {
        email: email,
        isBusy: isBusy,
        emailSendStatus: emailSendStatus,
        textType: textType,
        sendEmail: sendEmail,
        activate: activate,
        attached: attached
    };
});