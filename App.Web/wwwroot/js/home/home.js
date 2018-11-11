var PageHome = function (opts) {
    var $form = $('#formOccupancy');
    var $adultInput = $('#input-adult');
    var $childrenInput = $('#input-children');
    var $infantInput = $('#input-infant');
    var $summary = $form.find(".validation-summary > ul");
    var $submit = $("#btn-submit");
    var $resultSpan = $('#span-result');
    var _minimumAdultRequiredPerRoom = opts._minimumAdultRequiredPerRoom;
    var _maximumNumberOfAdultAndChildren = opts._maximumNumberOfAdultAndChildren;
    var _maximumNumberOfRoomPerBooking = opts._maximumNumberOfRoomPerBooking;
    var _maximumNumberOfAdultPerRoom = opts._maximumNumberOfAdultPerRoom;
    var _maximumNumberOfChildrenPerRoom = opts._maximumNumberOfChildrenPerRoom;
    var _maximumNumberOfInfantPerRoom = opts._maximumNumberOfInfantPerRoom;
    var adultCount = opts.adultCount;
    var childrenCount = opts.childrenCount;
    var infantCount = opts.infantCount;

    $.validator.unobtrusive.parse($form[0]);

    //process response from server
    var onServerResponse = function (response) {
        if (response
            && response.status
            && response.status.code === 0) {
            $resultSpan.empty();
            if (response.data.roomNeeded.length > 0) {
                var i = 1;

                $resultSpan.append('<h3>' + response.data.roomNeeded.length + ' room is needed.</h3>');
                $.each(response.data.roomNeeded, function (key, value) {
                    $resultSpan.append('Room ' + i + ': ' + value.adultCount + ' adults, ' + value.childrenCount + ' children, ' + value.infantCount + ' infants' + '<br/>');
                    i++;
                });
            }
        } else {
            $summary.empty();
            $.each(response.data, function (key, value) {
                $summary.append('<li>' + value + '</li>');
            });
        }
    };

    var validationForm = function () {
        var valid = true;

        valid = $form.valid() && valid;

        return valid;
    };

    var init = function () {
        $form.find("button[type=submit]").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            if (validationForm()) {
                $("#divLoader").show();
                $.ajax({
                    'method': 'POST',
                    'url': $form[0].action,
                    'data': $form.serialize(),
                    'complete': function (response, status, xhr) {
                        $("#divLoader").hide();
                    },
                    'success': function (response, status, xhr) {
                        onServerResponse(response);
                    }
                });
            }
        });

        var validation = function () {
            var errors = [];
            if (adultCount < _minimumAdultRequiredPerRoom) {
                errors.push("Oops, minimum " + _minimumAdultRequiredPerRoom + " of adult is required per room.");
            }
            if ((adultCount + childrenCount) > _maximumNumberOfAdultAndChildren) {
                errors.push("Oops, maximum number of guest allowed for a booking is " + _maximumNumberOfAdultAndChildren + ", excluding infants.");
            }
            if ((childrenCount / _maximumNumberOfChildrenPerRoom) > adultCount || (infantCount / _maximumNumberOfInfantPerRoom) > adultCount) {
                errors.push("Oops, every " + _maximumNumberOfChildrenPerRoom + " children or " + _maximumNumberOfInfantPerRoom + " infants required 1 adult supervision.");
            }
            if (adultCount > (_maximumNumberOfRoomPerBooking * _maximumNumberOfAdultPerRoom)
                || childrenCount > (_maximumNumberOfRoomPerBooking * _maximumNumberOfChildrenPerRoom)
                || infantCount > (_maximumNumberOfRoomPerBooking * _maximumNumberOfInfantPerRoom)) {
                errors.push("Oops, maximum number of room allowed per booking is " + _maximumNumberOfRoomPerBooking + ".");
            }

            $summary.empty();
            if (errors.length > 0) {
                $.each(errors, function (key, value) {
                    $summary.append('<li>' + value + '</li>');
                });
                $submit.attr('disabled', 'disabled');
            } else {
                $submit.removeAttr('disabled');
            }
        };

        $adultInput.on('input', function (e) {
            adultCount = parseInt($adultInput.val() || 0);
            validation();
        });
        $childrenInput.on('input', function (e) {
            childrenCount = parseInt($childrenInput.val() || 0);
            validation();
        });
        $infantInput.on('input', function (e) {
            infantCount = parseInt($infantInput.val() || 0);
            validation();
        });
    };

    init();
};