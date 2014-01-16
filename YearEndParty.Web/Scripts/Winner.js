var winner = {
    execSearch: function () {
        var query = $('#query').val();

        if (query.length < 1) {
            $('#query').focus();
            $("ul#list_winner").find("li").show();
            return;
        } else {
            var hasResult = false;

            $("#no_result").hide();
            $("ul#list_winner").find("li").hide();

            var liWinner = $("ul#list_winner li.winner");

            query = query.toLowerCase();

            $.each(liWinner, function (i, elm) {
                var nick = $(elm).attr("nick").toString().toLowerCase();
                var code = $(elm).attr("code").toString().toLowerCase();
                var full = $(elm).attr("full").toString().toLowerCase();
                var unsign = $(elm).attr("unsign").toString().toLowerCase();
                var type = $(elm).attr("type").toString();

                if (full.indexOf(query) != -1 || unsign.indexOf(query) != -1 || nick.indexOf(query) != -1 || code.indexOf(query) != -1) {
                    $("ul#list_winner").find("li.head." + type).show();
                    $(elm).show();
                    hasResult = true;
                }
            });

            if (hasResult == false) {
                $("#no_result").show();
            }
        }
    }
};

$(document).ready(function () {
    $('#query').focus().live('keypress', function (e) {
        if (e.keyCode == 13) {
            winner.execSearch();
        }
    });
});