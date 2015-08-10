$(document).ready(function () {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next,today',
            center: 'title',
            right: 'month, agendaWeek, agendaDay'
        },
        selectable: true,
        editable: true,


        //events: [
        //           {
        //               title: 'event1',
        //               start: '2015-08-10'
        //           },
        //           {
        //               title: 'event2',
        //               start: '2015-10-08',
        //               end: '2016-01-07'
        //           },
        //           {
        //               title: 'event3',
        //               start: '2015-10-08T12:30:00',
        //               allDay: false // will make the time show
        //           }
        //]
    });
});