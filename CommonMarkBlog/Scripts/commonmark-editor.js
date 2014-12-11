'use strict';

var writer = new commonmark.HtmlRenderer();
var reader = new commonmark.DocParser();

$(document).ready(function () {
    var timer;
    var x;
    var parsed;
    var render = function () {
        if (parsed === undefined) {
            return;
        }
        var startTime = new Date().getTime();
        var result = writer.renderBlock(parsed);
        var endTime = new Date().getTime();
        var renderTime = endTime - startTime;
        $("#Preview").html(result);
        $("#rendertime").text(renderTime);
    };

    var parseAndRender = function () {
        if (x) {
            x.abort()
        } // If there is an existing XHR, abort it.
        clearTimeout(timer); // Clear the timer so we don't end up with dupes.
        timer = setTimeout(function () { // assign timer a new timeout
            var startTime = new Date().getTime();
            parsed = reader.parse($("#Content").val());
            var endTime = new Date().getTime();
            var parseTime = endTime - startTime;
            $("#parsetime").text(parseTime);
            $(".timing").css('visibility', 'visible');
            render();
        }, 0); // ms delay
    };

    parseAndRender();

    $("#Content").bind('keyup paste cut mouseup', parseAndRender);
    $(".option").change(render);
});