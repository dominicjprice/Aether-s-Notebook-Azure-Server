//jQuery OpenID Plugin 1.1 Copyright 2009 Jarrett Vance http://jvance.com/pages/jQueryOpenIdPlugin.xhtml
// Modified for Aether's Notebook Azure Server 2011 Dominic Price http://www.horizon.ac.uk

$.fn.openid = function () {

    var $this = $(this);
    var $opendiv = $this.find('div.openid');
    var $id = $opendiv.find('input[name=OpenID]');
    var $front = $opendiv.find('div:has(input[name=OpenID])>span:eq(0)');
    var $end = $opendiv.find('div:has(input[name=OpenID])>span:eq(1)');
    var $idfs = $opendiv.find('div:has(input[name=OpenID])');

    var direct = function () {
        var $li = $(this);
        $li.parent().find('li').removeClass('highlight');
        $li.addClass('highlight');
        $idfs.fadeOut();
        return false;
    };

    var openid = function () {
        var $li = $(this);
        $li.parent().find('li').removeClass('highlight');
        $li.addClass('highlight');
        $idfs.show();
        $id.focus();
        return false;
    };

    var username = function () {
        var $li = $(this);
        $li.parent().find('li').removeClass('highlight');
        $li.addClass('highlight');
        $idfs.show();
        $front.text($li.find("span").text().split("username")[0]);
        $end.text("").text($li.find("span").text().split("username")[1]);
        $id.focus();
        return false;
    };

    function validateInput() {
        var $selectedli = $opendiv.find('li.direct,li.username,li.openid').filter('.highlight');
        if ($selectedli.hasClass("direct")) {
            $id.val($selectedli.find("span").text());
            return true;
        }
        else {
            if ($id.val().length < 1) {
                $id.focus();
                return false;
            }
            if ($selectedli.hasClass("username"))
                $id.val($front.text() + $id.val() + $end.text());
            return true;
        }
    };

    $id.keypress(function (e) {
        if ((e.which && e.which == 13)
                || (e.keyCode && e.keyCode == 13)) {
            $("input[type='submit']").click();
        }
    });

    $opendiv.find('li.direct').click(direct);
    $opendiv.find('li.openid').click(openid);
    $opendiv.find('li.username').click(username);
    $opendiv.find('li span').hide();
    $opendiv.find('li').css('line-height', 0).css('cursor', 'pointer');
    $opendiv.find('li:eq(0)').click();
    $("input[type='submit']").click(validateInput);

    return this;
};