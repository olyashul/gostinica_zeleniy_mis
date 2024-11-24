// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    let datePickerЗаезд, datePickerВыезд;

    function updateCalendars() {
        const nomerId = $('#NomerId').val();
        fetch(`/GetOccupiedDates?nomerId=${nomerId}`)
            .then(response => response.json())
            .then(occupiedDates => {
                console.log("Occupied dates:", occupiedDates);
                if (datePickerЗаезд) {
                    datePickerЗаезд.set('disable', occupiedDates);
                }
                if (datePickerВыезд) {
                    datePickerВыезд.set('disable', occupiedDates);
                }
            })
            .catch(error => console.error("Error fetching occupied dates:", error));
    }

    datePickerЗаезд = flatpickr("#data_zaezd", {
        dateFormat: "Y-m-d",
        minDate: "today",
        disable: [],
        onChange: function (selectedDates, dateStr, instance) {
            if (datePickerВыезд) {
                datePickerВыезд.set('minDate', dateStr);
            }
            updateCalendars();
        }
    });

    datePickerВыезд = flatpickr("#data_viezd", {
        dateFormat: "Y-m-d",
        minDate: "today",
        disable: [],
    });

    $('#NomerId').change(updateCalendars);
    updateCalendars();
});
