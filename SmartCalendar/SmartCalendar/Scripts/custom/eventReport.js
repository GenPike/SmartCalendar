$(function () {
    $("#inp_datepicker").datepicker({ dateFormat: 'dd-mm-yy' }).val();
    $("#inp_datepicker2").datepicker({ dateFormat: 'dd-mm-yy' }).val();
});

$(function Get() {
    jQuery.support.cors = true;
    var start = new Date();
    var end = new Date();
    end.setDate(end.getDate() + 30);

    $.ajax({
        url: '/api/Event/',
        dataType: 'json',
        data: {
            startISO: start.toISOString(),
            endISO: end.toISOString()
        },
        success: function (data) {
            WriteEvents(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
});

$("#Submitt").click(function () {
    var start = $("#inp_datepicker").val();
    var end = $("#inp_datepicker2").val();

    $.ajax({
        url: '/api/Event/',
        dataType: 'json',
        data: {
            startISO: start,
            endISO: end
        },
        success: function (data) {
            WriteEvents(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
});

function WriteEvents(events) {
    var count = 0;
    $.each(events, function (index, event) {
        count++;
    });

    if (count == 0) $("#tableBlock").html("<h3 style='align-items:center' >No events in chosen period!</h3>");
    else {
        var strResult = "<table class='table table-hover'><thead>" +
            "<th>Title</th><th>Start date</th><th>End date</th>" +
            "<th>Location</th><th>Description</th></thead><tbody>";
        $.each(events, function (index, event) {
            strResult += "<tr><td>" + event.title + "</td><td> " + event.start_date + "</td><td>" + event.end_date +
                "</td><td>" + event.location + "</td><td>" + event.text;
        });
        $("#tableBlock").html(strResult);
    }
}