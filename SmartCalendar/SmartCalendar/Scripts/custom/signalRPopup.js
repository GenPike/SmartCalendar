$(function () {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.alarmHub;
    // Create a function that the hub can call back to display messages.
    chat.client.addNewMessageToPage = function (name, message) {
            
        $.ajaxSetup({ cache: false });
        $("<div>Event: <strong>"+htmlEncode(name)+"</strong><br/>Starts in 5 min.<br/>Start date:<strong>"+htmlEncode(message)+"</strong></div>")
            .addClass("dialog")
           .appendTo("body")
           .dialog({
               title: $(this).attr("data-dialog-title"),
               close: function () { $(this).remove() },
               modal: true
           })
            .load(this.href);

        $(".close").on("click", function (e) {
            e.preventDefault();
            $(this).closest(".dialog").dialog("close");
        });
    };
   
    $.connection.hub.start().done(function () {
    });
});

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}