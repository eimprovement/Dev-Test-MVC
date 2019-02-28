
var CoreVM = function (baseApiUrl, apiSubscriptionKey) {
    var coreSelf = this;

    coreSelf.basePetsApiUrl = baseApiUrl;
    
    coreSelf.petsApiAjaxHeaders =  {
        "Ocp-Apim-Subscription-Key": apiSubscriptionKey,
        "Content-Type": "application/json"
    };
    
    coreSelf.showMessage = function (type, message) {
        coreSelf.scrollToDiv(0);
        var $messagesSection = $("#messages");

        $.each($messagesSection.children(), function () {
            $(this).addClass("hidden");
            $(this).children().last().text("");

        });

        var $message = $messagesSection.find("#" + type);
        $message.children().last().html(message);
        $message.removeClass("hidden");

    };

    coreSelf.scrollToDiv = function (position) {
        $("html, body").animate({ scrollTop: position }, {
            easing: "swing"
        });
    };

    coreSelf.clearMessages = function () {
        var $messagesSection = $("#messages");

        $.each($messagesSection.children(), function () {
            $(this).addClass("hidden");
            $(this).children().last().text("");
        });
        
    };
    
    coreSelf.formValidationmessages = {
        requiredFieldMessage: "This field is required.",
        numberOnlyMessage: "This field allows numbers only.",
        alphanumericAndSpacesOnlyMessage: "This field allows numbers, letters and spaces only.",
        urlOnlyMessage: "This field allows Urls only."
    };

    coreSelf.commonRegexPatters = {
        alphanumericAndSpacesOnlyRegex: '^[a-zA-Z0-9 ]*$'
    };

    //Knockout validation taken from: https://jes.al/2014/07/url-validation-knockoutjs/
    ko.validation.rules['url'] = {
        validator: function (val, required) {
            if (!val) {
                return !required
            }
            val = val.replace(/^\s+|\s+$/, ''); //Strip whitespace
            //Regex by Diego Perini from: http://mathiasbynens.be/demo/url-regex
            return val.match(/^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.‌​\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[‌​6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1‌​,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00‌​a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u‌​00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i);
        },
        message: "Urls only alowed."
    };
    ko.validation.registerExtenders();
};



