$(document).ready(function() {
    //the scheduler is a static object and can be instantiate on the page once
    scheduler.init('scheduler_here', new Date(), "month");

    // init our calendar
    var start = new Date();
    var end = new Date();
    start.setDate(end.getDate() - 1825);
    end.setDate(end.getDate() + 1825);

    $.ajax({
        url: '/api/Event/',
        type: 'GET',
        dataType: 'json',
        data: {
            startISO: start.toISOString(),
            endISO: end.toISOString()
        },
        success: function(data) {
            scheduler.parse(data, "json");
        },
        error: function(x, y, z) {
            console.log(x + '\n' + y + '\n' + z);
        }
    });

    scheduler.locale.labels.section_title = 'Title';
    scheduler.locale.labels.section_address = 'Location';
    scheduler.locale.labels.section_type = 'Category';

    scheduler.config.lightbox.sections = [
        { name: "title", height: 30, map_to: "title", type: "textarea" },
        { name: "description", height: 100, map_to: "text", type: "textarea", focus: true },
        { name: "address", height: 30, map_to: "location", type: "textarea" },
        {
            name: "type",
            height: 40,
            map_to: "category",
            type: "select",
            options: [
                { key: 1, label: 'Home' },
                { key: 2, label: 'Business' },
                { key: 3, label: 'Study' },
                { key: 4, label: 'Fun' },
                { key: 5, label: 'Other' }
            ]
        },
        { name: "time", height: 72, type: "calendar_time", map_to: "auto" }
    ];

    scheduler.templates.event_class = function(start, end, event) {
        if (event.category == '1') return "home_event";
        if (event.category == '2') return "business_event";
        if (event.category == '3') return "study_event";
        if (event.category == '4') return "fun_event";
        return "other_event";
    };

    scheduler.attachEvent("onEventChanged", function(id, ev) {
        UpdateEvent(ev);
        return true;
    });

    scheduler.attachEvent("onEventDeleted", function(id) {
        DeleteEvent(id);
        return true;
    });

    scheduler.attachEvent("onEventAdded", function(id, ev) {
        AddEvent(ev);
        return true;
    });
});

function AddEvent(ev) {
    $.ajax({
        url: '/api/Event/',
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(ev),
        contentType: "application/json;charset=utf-8",
        success: console.log(ev.id + " Object added!"),
        error: function (x, y, z) {
            console.log(x + '\n' + y + '\n' + z);
        }
    });
}

function UpdateEvent(ev) {
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