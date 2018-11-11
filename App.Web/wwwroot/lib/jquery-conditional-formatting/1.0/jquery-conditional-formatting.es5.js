'use strict';

$.fn.conditionalFormatting = function (opts) {
    var $this = $(this);

    var executeFormatting = function executeFormatting() {
        var $elem = $(this);
        var value = this.value;

        $.each(opts, function (i, opt) {
            var conditionText = opt.condition.replace(/\{\{value\}\}/, value);
            var inCondition = eval(conditionText);

            if (opt.className) {
                if (inCondition) {
                    $elem.addClass(opt.className);
                } else {
                    $elem.removeClass(opt.className);
                }
            }
        });
    };

    //on init
    $this.each(function () {
        executeFormatting.bind(this).apply();
    });

    //on change
    $this.on('change', executeFormatting);

    //on key up
    $this.on('keyup', executeFormatting);

    //on key down
    $this.on('keydown', executeFormatting);
};

