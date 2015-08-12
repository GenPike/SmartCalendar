$(document).ready(function () {
    //the scheduler is a static object and can be instantiate on the page once
    scheduler.init('scheduler_here', new Date(), "month");

    // init our calendar
    var start = new Date();
    var end = new Date();
    start.setDate(end.getDate() - 30);
    end.setDate(end.getDate() + 30);

    $.ajax({
        url: '/api/Event/',
        type: 'GET',
        dataType: 'json',
        data: {
            startISO: start.toISOString(),
            endISO: end.toISOString()
        },
        success: function (data) {
            scheduler.parse(data, "json");
        },
        error: function (x, y, z) {
            console.log(x + '\n' + y + '\n' + z);
        }
    });

    scheduler.config.lightbox.sections = [
    { name: "description", height: 200, map_to: "text", type: "textarea", focus: true },
    { name: "time", height: 72, type: "time", map_to: "auto" }
    ];

    scheduler.attachEvent("onEventChanged", function (id, ev) {
        UpdateEvent(ev);
        return true;
    });

    scheduler.attachEvent("onEventDeleted", function (id) {
        DeleteEvent(id);
        return true;
    });

    scheduler.attachEvent("onEventAdded", function (id, ev) {
        AddEvent(ev);
        return true;
    });
});

function AddEvent(ev) {
    ev.Location = "new location";
    ev.title = "new title";

    $.ajax({
        url: '/api/Event/',
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(ev),
        contentType: "application/json;charset=utf-8",
        success: console.log("Object added!"),
        error: function (x, y, z) {
            console.log(x + '\n' + y + '\n' + z);
        }
    });
}

function UpdateEvent(ev) {
    ev.Location = "new location";
    ev.title = "new title";

    $.ajax({
        url: '/api/Event/',
        type: 'PUT',
        dataType: 'json',
        data: JSON.stringify(ev),
        contentType: "application/json;charset=utf-8",
        success: console.log(ev.id + " Object changed!"),
        error: function (x, y, z) {
            console.log(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteEvent(id) {
    $.ajax({
        url: '/api/Event/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: console.log(id + " Object deleted!"),
        error: function (x, y, z) {
            console.log(x + '\n' + y + '\n' + z);
        }
    });
}