﻿var Settings = function () {

    var disableDefaultOnClickAction = function () {
        $(document).on("click", "a.disable-default", function () {
            event.preventDefault();
        });
    }

    var showSettingsModal = function () {
        $(document).off("click.add-settings-modal").on("click.add-settings-modal", "a.add-settings-item", function () {
            LoadingIndicator.show();
            $.get((this).href + "?klass=" + $(this).data("klass"), function(html) {
                LoadingIndicator.hide();
                $("div#modal-container").html(html);
                $("div.new-settings-modal").modal("show");           
            });
        });
    }

    var showSettingsEditModal = function () {
        $(document).off("click.edit-settings-modal").on("click.edit-settings-modal", "a.edit-link", function () {
            LoadingIndicator.show();
            var klass = $(this).data("klass");
            $.get((this).href+ "?klass="+klass, function (html) {
                LoadingIndicator.hide();
                $("div#modal-container").html(html);
                $("div.edit-settings-modal").modal("show");                              
            });
        });
    }

    var goToTab = function () {
        var hash = document.location.hash;
        var prefix = "";
        if (hash) {
            $('.nav-tabs a[href="' + hash.replace(prefix, "") + '"]').tab('show');
        }

        $('.nav-tabs a:not([data-parent-tab])').on('shown.bs.tab', function (e) {
            window.location.hash = e.target.hash.replace("#", "#" + prefix);
            if (window.location.query !== undefined) {
                $("a[data-parent-tab]#" + window.location.query).click();
                window.location.query = undefined;
            }
        });

        $('.nav-tabs a[data-parent-tab]').on('shown.bs.tab', function (e) {
            window.location.query = $(this).attr("href").replace("#", "")
        });
    }

    var onSettingsItemSubmit = function () {
        $(document).off("click.save-settings-item").on("click.save-settings-item", "button.submit-settings-item", function () {
            LoadingIndicator.show();
            var loadTab = $(this).data("tab");
            var klass = $(this).data("klass");
            var requestData = function () {
                if (klass === undefined) {
                    return $("form.settings-form").serialize();
                } else {
                    return $("form.settings-form").serialize() + "&klass=" + klass;
                }
            }();
            $("label.text-danger").remove();
            $.ajax({
                url: $("form.settings-form").attr("action"),
                type: "POST",
                data: requestData,
                contentType: "application/x-www-form-urlencoded",
                dataType: 'json'
            }).done(function (data, textStatus) {
                LoadingIndicator.hide();
                if (textStatus === "success") {
                    if (data.errors) {
                        displayErrors(data.errors);
                    } else {
                        $("form.settings-form")[0].reset();
                        $("div.new-settings-modal").modal("hide");
                        window.location.hash = loadTab;
                        window.location.reload();
                    }
                }
            }).fail(function (data) {
                LoadingIndicator.hide();
                $("form.settings-form")[0].reset();
                $("div.new-settings-modal").modal("hide");
                alert("There was a problem saving this information. Please contact administrator");
            });
        });
    }

    var loadRadiologySubitem = function () {

    }

    var displayErrors = function (errors) {
        for (var i = 0; i < Object.keys(errors).length; i++) {
            var field = Object.keys(errors)[i];
            var htmlCode = "<label for='" + field + "' class='text-danger'></label>";
            var fieldError = errors[Object.keys(errors)[i]];
            var fieldInput = $("input[name='" + capitalizeFirstLetter(field) + "'], select[name='" + capitalizeFirstLetter(field) + "'], input[name='RadiologyViewModel.Name']");
            $(htmlCode).html(fieldError).appendTo(fieldInput.parent());
        }
    }

    var capitalizeFirstLetter = function(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    var bindOnDeleteSettingsItemClick = function () {
        $(document).off("click.delete-link").on("click.delete-link", "a.delete-link", function () {
            LoadingIndicator.show();
            var deleteUrl = $(this).attr("href");
            var whatToDelete = $(this).data("what");
            var warningMessage = $(this).data("warning");
            var klass = $(this).data("klass");
            window.openedTab = $(this).data("tab");
            var question = 'Are you sure you want to irreversibly delete this ' + whatToDelete +' and all related data?<br><br><div class=\'alert alert-danger\' style=\'padding: 10px\'>'+warningMessage+ '</div>';
            var requestData = function () {
                if (klass !== undefined) {
                    return deleteUrl = deleteUrl + "?klass=" + klass;
                } else {
                    return deleteUrl;
                }
            }();
            BootstrapDialog.confirm(question, function (result, dialog) {
                LoadingIndicator.hide();

                if (result) {
                    $.ajax({
                        url: requestData,
                        type: "POST",
                        contentType: "application/x-www-form-urlencoded",
                        dataType: 'json'
                    }).done(function (data, textStatus) {
                        if (textStatus === "success") {
                            window.location.hash = window.openedTab;
                            window.location.reload();
                            if (window.diagnosisTypesDataTable !== undefined) {
                                window.diagnosisTypesDataTable.reload();
                            }
                        }
                    }).always(function () {
                        LoadingIndicator.hide();
                    });
                }
            });
        });
    }
         
    return {
        init: function () {
            disableDefaultOnClickAction();
            showSettingsModal();
            goToTab();
            onSettingsItemSubmit();
            showSettingsEditModal();
            bindOnDeleteSettingsItemClick();
        }
    }

}();