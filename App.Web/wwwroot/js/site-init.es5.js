//pure js
"use strict";

String.prototype.format = function (symbol, strLenght) {
    var args = arguments;

    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] !== 'undefined' ? args[number] : match;
    });
};

//global applicate layer scrope
var app = new function () {
    this.title = {
        success: "Success",
        error: "Error"
    };

    this.storage = {};

    var onImgError = function onImgError(e) {
        e.target.className += " hidden";
    };

    this.onImgError = onImgError;
}();

