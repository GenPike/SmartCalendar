$(document).ready(function () {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next,today',
            center: 'title',
            right: 'month, agendaWeek, agendaDay'
        },
        selectable: true,
        editable: true
    });
});