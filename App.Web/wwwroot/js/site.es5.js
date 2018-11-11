//logout action
"use strict";

$(document).on("click.app.logout", ".app-logout", function (e) {
    e.preventDefault();
    $('#logoutForm').submit();
});

//remove popup content on close
$('body').on('hidden.bs.modal', '.modal', function (e) {
    $(this).removeData('bs.modal');
    $('.modal-content', this).empty();
});
window.onerror = function (e) {
    app.error = {
        'event': e
    };

    swal({
        title: "Error",
        text: "Unexpected error occur.",
        icon: "error"
    });
};

//global ajax event
$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    app.error = {
        'event': event,
        'jqxhr': jqxhr,
        'settings': settings,
        'thrownError': thrownError
    };

    if (jqxhr && jqxhr.status === 0) {
        return true;
    }

    swal({
        title: "Error",
        text: "Unexpected error occur.",
        icon: "error"
    });
}).ajaxSend(function (event, jqxhr, settings) {}).ajaxStart(function () {}).ajaxStop(function () {}).ajaxComplete(function (event, jqxhr, settings) {});

$(document).ready(function () {
    if (location.pathname === '/') {
        $('#btnBack').addClass('hidden');
    } else {
        $('#btnBack').removeClass('hidden');
    }
});

var loader = new function () {
    this.show = function () {
        $('.loader').addClass('show');
    };

    this.hide = function () {
        $('.loader').removeClass('show');
    };
}();

//jquery dom to post data in json
$.fn.serializeJSON = function () {
    var $inputs = $(this).add(this.find('[name]'));

    var model = {};

    $inputs.each(function (i, item) {
        var propertyName = this.name;
        var propertyValue = this.value;

        if (typeof propertyName === "undefined" || propertyName === "" || propertyName === null) {
            return true;
        }

        if (propertyName.indexOf('.') < 0 && propertyName.indexOf('[') < 0 && propertyName.indexOf(']') < 0) {
            model[propertyName] = this.value;
        } else {
            var propertyModel = model;
            var propertyKeys = propertyName.split('.');

            $.each(propertyKeys, function (iName, key) {
                var arrayKeys = key.match(/\[\d\]/g);
                if (arrayKeys && arrayKeys.length > 0) {
                    var propertyKey = propertyName.substring(0, propertyName.indexOf('['));

                    if (typeof propertyModel[propertyKey] === "undefined") {
                        propertyModel[propertyKey] = [];
                    }
                    propertyModel = propertyModel[propertyKey];

                    $.each(arrayKeys, function (iArray, arrayIndex) {
                        var intArrayIndex = parseInt(arrayIndex.replace(/[\[|\]]/g, ''));

                        if (iArray === arrayKeys.length - 1) {
                            if (iName === propertyKeys.length - 1) {
                                propertyModel[intArrayIndex] = propertyValue;
                            } else {
                                if (typeof propertyModel[intArrayIndex] === "undefined") {
                                    propertyModel[intArrayIndex] = {};
                                }

                                propertyModel = propertyModel[intArrayIndex];
                            }
                        } else {
                            if (typeof propertyModel[intArrayIndex] === "undefined") {
                                propertyModel[intArrayIndex] = [];
                            }

                            propertyModel = propertyModel[intArrayIndex];
                        }
                    });
                } else {
                    if (iName === propertyKeys.length - 1) {
                        propertyModel[key] = propertyValue;
                    } else {

                        if (typeof propertyModel[key] === "undefined") {
                            propertyModel[key] = {};
                        }

                        propertyModel = propertyModel[key];
                    }
                }
            });
        }
    });

    return model;
};

