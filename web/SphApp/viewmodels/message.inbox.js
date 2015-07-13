﻿/// <reference path="../../Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="../../Scripts/knockout-2.2.1.debug.js" />
/// <reference path="../../Scripts/knockout.mapping-latest.debug.js" />
/// <reference path="../../Scripts/require.js" />
/// <reference path="../../Scripts/underscore.js" />
/// <reference path="../../Scripts/moment.js" />
/// <reference path="../services/datacontext.js" />
/// <reference path="../services/domain.g.js" />
/// <reference path="../objectbuilders.js" />
/// <reference path="../../Scripts/bootstrap.js" />


define([objectbuilders.datacontext, objectbuilders.config, objectbuilders.router],
    function (context, config, router) {

        var isBusy = ko.observable(false),
            includeRead = ko.observable(true),
            messages = ko.observableArray(),
            activate = function () {
                var query = String.format("UserName eq '{0}'", config.userName);

                return context.loadAsync({
                    entity: "Message",
                    orderby: "CreatedDate desc"
                }, query)
                .done(function (lo) {
                    isBusy(false);
                    var sorted = lo.itemCollection.sort(function (x, y) { return x.CreatedDate() > y.CreatedDate(); });
                    messages(sorted);
                });


            },
            resetFilter = function (ev) {
                $("ul#filter-messages>li").removeClass("active");
                if (ev.target) {
                    $(ev.target.parentNode).addClass("active");
                }
            },
            filter = function (options) {
                options = options || {};
                options.read = includeRead();

                var query = String.format("UserName eq '{0}'", config.userName);
                if (options.start) {
                    query += " and CreatedDate ge DateTime'" + options.start + "'";
                }
                if (options.end) {
                    query += " and CreatedDate le DateTime'" + options.end + "'";
                }
                if (!options.read) {
                    query += " and IsRead eq 0";
                }

                return context.loadAsync({
                    entity: "Message",
                    orderby: "CreatedDate desc"
                }, query)
                     .then(function (lo) {
                         isBusy(false);

                         messages(lo.itemCollection);
                     });

            },
            attached = function (view) {
              $(view).on("click", "tr", function(e){
                var message = ko.dataFor(this);
                if(typeof message.Id === "function"){
                  router.navigate("#message.detail/" + ko.unwrap(message.Id));
                }
              });
            },
            thisWeek = function (d, ev) {
                resetFilter(ev);
                filter({ start: moment().day("Sunday").format("YYYY-MM-DD") });
            },
            thisMonth = function (d, ev) {
                resetFilter(ev);
                filter({ start: moment().startOf("month").format("YYYY-MM-DD") });
            },
            older = function (d, ev) {
                resetFilter(ev);
                filter({ end: moment().startOf("month").format("YYYY-MM-DD") });
            };

        includeRead.subscribe(function() {
            return filter();
        });

        var vm = {
            isBusy: isBusy,
            activate: activate,
            includeRead: includeRead,
            thisWeek: thisWeek,
            thisMonth: thisMonth,
            older: older,
            attached: attached,
            messages: messages
        };

        return vm;

    });
