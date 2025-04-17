$(function () {
    var Calendar = FullCalendar.Calendar;
    var checkbox = document.getElementById('drop-remove');
    var calendarEl = document.getElementById('calendar');

    var calendar = new Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        themeSystem: 'bootstrap',
        editable: false, 
        droppable: false,
        selectable: false,
        events: function (info, successCallback, failureCallback) {
            $.ajax({
                url: `${urls.company_works}/calendar-events`,
                method: "GET",
                data: {
                    start: info.startStr,
                    end: info.endStr
                },
                success: function (data) {
                    console.log("Loaded Calendar Data", data);
                    successCallback(data); 
                },
                error: function (xhr) {
                    console.error("Failed to load calendar data", xhr);
                    failureCallback(xhr);
                }
            });
        },
        eventClick: function (info) {
            info.jsEvent.preventDefault();
            if (info.event.url) {
                window.open(info.event.url, "_blank");
            }
        },
        drop: function (info) {
            if (checkbox.checked) {
                info.draggedEl.parentNode.removeChild(info.draggedEl);
            }
        }
    });

    calendar.render();

})
