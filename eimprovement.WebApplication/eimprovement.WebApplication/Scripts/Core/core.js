
var CoreVM = function (baseApiUrl, apiSubscriptionKey) {
    var coreSelf = this;

    coreSelf.basePetsApiUrl = baseApiUrl;
    
    coreSelf.petsApiAjaxHeaders =  {
        "Ocp-Apim-Subscription-Key": apiSubscriptionKey
    };
    
    if (!String.format) {
        String.format = function (format) {
            var args = Array.prototype.slice.call(arguments, 1);
            return format.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                    ? args[number]
                    : match
                    ;
            });
        };
    };

    coreSelf.setCookie = function (cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    coreSelf.getCookie = function (name) {
        var dc = document.cookie;
        var prefix = name + "=";
        var begin = dc.indexOf("; " + prefix);
        if (begin == -1) {
            begin = dc.indexOf(prefix);
            if (begin != 0) return null;
        }
        else {
            begin += 2;
            var end = document.cookie.indexOf(";", begin);
            if (end == -1) {
                end = dc.length;
            }
        }

        return decodeURI(dc.substring(begin + prefix.length, end));
    };

    coreSelf.copyContentToClipboard = function (target, content, message) {
        try {
            const el = document.createElement('textarea');
            el.value = content;
            el.setAttribute('readonly', '');
            el.style.position = 'absolute';
            el.style.left = '-9999px';
            if (target == null) {
                document.body.appendChild(el);
            } else {
                $(target.parentElement).append(el);
            }

            //el.focus();
            el.select();
            var success = document.execCommand('copy');
            if (success) {
                if (target == null) {
                    coreSelf.showMessage('success', message);
                } else {
                    var $target = $(target);
                    $target.addClass('animated bounceIn');
                    setTimeout(function () {
                        $target.removeClass('animated bounceIn');
                        var original = $target.data('originalTitle');
                        $target.attr('title', message)
                            .tooltip('fixTitle')
                            .tooltip('show');
                        setTimeout(function () {
                            $target.attr('title', original)
                                .tooltip('fixTitle');
                            if ($target.data('bs.tooltip').tip().hasClass('in')) {
                                $target.tooltip('show');
                            }
                        }, 1500);
                    }, 300);
                }
            } else {
                coreSelf.showUnexpectedErrorMessage();
            }

            if (target == null) {
                document.body.removeChild(el);
            } else {
                $(el).remove();
            }

        } catch (e) {
            coreSelf.showUnexpectedErrorMessage();
        }
    };
    
};



