/*const da = require("../../wwwroot/fullcalendar/core/locales/da");*/

    var routeURL = location.protocol + "//" + location.host;
    $(document).ready(function () {
        $("#appointmentDate").kendoDateTimePicker({
            value: new Date(),
            dateInput: false
        });

    InitializeCalendar();
    });
    var calendar;
    function InitializeCalendar() {
        try {
            var calendarEl = document.getElementById('calendar');
    if (calendarEl != null) {
        calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            headerToolbar: {
                left: 'prev,next,today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            selectable: true,
            editable: false,
            select: function (event) {
                onShowModal(event, null)
            },
            //oncancel: function ()
            //{
            //},
            eventDisplay: 'block',
            events: function (fetchInfo, successCallback, falier) {
                $.ajax({
                    url: routeURL + '/api/GetClanderDate?doctorId=' + $("#doctorId").val(),
                    type: 'GET',
                    contentType: 'JSON',
                    success: function (response) {
                        var events = [];
                        if (response.length > 0) {
                            $.each(response, function (i, data) {
                                events.push({
                                    title: data.title,
                                    description: data.description,
                                    start: data.startDate,
                                    end: data.endDate,
                                    backgroundColor: data.isDoctorApproved ? "#28a785" : "#dc3545",
                                    bordercolor: "#162466",
                                    textcolor: "white",
                                    id: data.id
                                });
                            })
                        }
                        successCallback(events);
                        console.log(response);
                    },
                    error: function (xhr) {
                        console.log(xhr);
                    }
                });
            }

        });
    calendar.render();
            }

        }
    catch (e) {
        alert(e);
        }

    }

    function onShowModal(obj, isEventEdit) {
        $("#appInput").modal('show');
    }

    function onModelClose() {
        $("#appInput").modal('hide');

    }
    //function onModalSumit() {

        //    var requestData =
        //    {
        //        Id: parseInt($("#hdId").val()),
        //        Title: $("#txtTitle").val(),
        //        Description: $("#txtDescription").val(),
        //        StartDate: $("#txtStartDate").val(),
        //        Duration: $("#ddlDurations").val(),
        //        DoctorId: $('#ddlDoctor').val(),
        //        PatientId: $('#ddlPatinetID').val(),
        //    }
        //    //var framData = new FormData();
        //    //framData.append(Id, parseInt($("#hdId").val()));
        //    //framData.append(Title, $("#txtTitle").val());
        //    //framData.append(Description, $("#txtDescription").val());
        //    //framData.append(StartDate, $("#txtStartDate").val());
        //    //framData.append(Duration, $("#ddlDurations").val());
        //    //framData.append(DoctorId, $('#ddlDoctor').val());
        //    //framData.append(PatientId, $('#ddlPatinetID').val());

        //    //$.post(`${routeURL}/api/Appointment/SaveData`, `mdl:${requestData}`, function (res) {

        //    //    console.log(res)
        //    //});
        //    $.ajax({
        //        url: routeURL + "/api/Appointment/SaveData",
        //        type: 'POST',
        //        data: JSON.stringify(requestData),
        //        contentType: 'application/json',
        //        success: function (res) {
        //            console.log(res);
        //        },
        //        error: function (xhr) {
        //            console.log(xhr);
        //        }
        //    });
        //    console.log(requestData);
        //}
function onModalSumit() {
            if (checkValidation()) {
                var requestData = {
                    Id: parseInt($("#hdID").val()),
                    Title: $("#title").val(),
                    Description: $("#description").val(),
                    StartDate: $("#appointmentDate").val(),
                    Duration: $("#duration").val(),
                    patientId: $("#patientId").val(),
                    DoctorId: $("#doctorId").val()
                };
                console.log(requestData);
                $.ajax({
                    url: routeURL + '/api/SaveCalendarData',
                    type: 'POST',
                    data: JSON.stringify(requestData),
                    contentType: 'application/json',
                    success: function (response) {
                        response.resulteEnum == 1 ? $.notify(response.message, "success") : $.notify(response.message, "error");
                        onModelClose();
                        console.log(response);
                    },
                    error: function (xhr) {
                        $.notify("Error", "error");
                        console.log(xhr);
                    }
                });
            }

        }
    function checkValidation() {
        var isValid = true;
    if ($("#title").val() === undefined || $("#title").val() === "") {
        isValid = false;
    $("#title").addClass("error");
        }
    else {
        $("#title").removeClass("error");

        }
    if ($("#appointmentDate").val() === undefined || $("#appointmentDate").val() === "") {
        isValid = false;
    $("#appointmentDate").addClass("error");
        }
    else {
        $("#appointmentDate").removeClass("error");

        }
        return isValid;
    }












































//function onShowModal(obj, isEventDetail) {
//    if (isEventDetail != null) {

//        $("#title").val(obj.title);
//        $("#description").val(obj.description);
//        $("#appointmentDate").val(obj.startDate);
//        $("#duration").val(obj.duration);
//        $("#doctorId").val(obj.doctorId);
//        $("#patientId").val(obj.patientId);
//        $("#id").val(obj.id);
//        $("#lblPatientName").html(obj.patientName);
//        $("#lblDoctorName").html(obj.doctorName);
//        if (obj.isDoctorApproved) {
//            $("#lblStatus").html('Approved');
//            $("#btnConfirm").addClass("d-none");
//            $("#btnSubmit").addClass("d-none");
//        }
//        else {
//            $("#lblStatus").html('Pending');
//            $("#btnConfirm").removeClass("d-none");
//            $("#btnSubmit").removeClass("d-none");
//        }
//        $("#btnDelete").removeClass("d-none");
//    }
//    else {
//        $("#appointmentDate").val(obj.startStr + " " + new moment().format("hh:mm A"));
//        $("#id").val(0);
//        $("#btnDelete").addClass("d-none");
//        $("#btnSubmit").removeClass("d-none");
//    }
//    $("#appointmentInput").modal("show");
//}

//function onCloseModal() {
//    $("#apointmentForm")[0].reset();
//    $("#id").val(0);
//    $("#title").val('');
//    $("#description").val('');
//    $("#appointmentDate").val('');

//    $("#appointmentInput").modal("hide");
//}

//function onSubmitForm() {
//    if (checkValidation()) {
//        var requestData = {
//            Id: parseInt($("#id").val()),
//            Title: $("#title").val(),
//            Description: $("#description").val(),
//            StartDate: $("#appointmentDate").val(),
//            Duration: $("#duration").val(),
//            DoctorId: $("#doctorId").val(),
//            PatientId: $("#patientId").val(),
//        };

//        $.ajax({
//            url: routeURL + '/api/Appointment/SaveCalendarData',
//            type: 'POST',
//            data: JSON.stringify(requestData),
//            contentType: 'application/json',
//            success: function (response) {
//                if (response.status === 1 || response.status === 2) {
//                    calendar.refetchEvents();
//                    $.notify(response.message, "success");
//                    onCloseModal();
//                }
//                else {
//                    $.notify(response.message, "error");
//                }
//            },
//            error: function (xhr) {
//                $.notify("Error", "error");
//            }
//        });
//    }
//}

//function checkValidation() {
//    var isValid = true;
//    if ($("#title").val() === undefined || $("#title").val() === "") {
//        isValid = false;
//        $("#title").addClass('error');
//    }
//    else {
//        $("#title").removeClass('error');
//    }

//    if ($("#appointmentDate").val() === undefined || $("#appointmentDate").val() === "") {
//        isValid = false;
//        $("#appointmentDate").addClass('error');
//    }
//    else {
//        $("#appointmentDate").removeClass('error');
//    }

//    return isValid;

//}

//function getEventDetailsByEventId(info) {
//    $.ajax({
//        url: routeURL + '/api/Appointment/GetCalendarDataById/' + info.id,
//        type: 'GET',
//        dataType: 'JSON',
//        success: function (response) {

//            if (response.status === 1 && response.dataenum !== undefined) {
//                onShowModal(response.dataenum, true);
//            }
//            successCallback(events);
//        },
//        error: function (xhr) {
//            $.notify("Error", "error");
//        }
//    });
//}

//function onDoctorChange() {
//    calendar.refetchEvents();
//}

//function onDeleteAppointment() {
//    var id = parseInt($("#id").val());
//    $.ajax({
//        url: routeURL + '/api/Appointment/DeleteAppoinment/' + id,
//        type: 'GET',
//        dataType: 'JSON',
//        success: function (response) {

//            if (response.status === 1) {
//                $.notify(response.message, "success");
//                calendar.refetchEvents();
//                onCloseModal();
//            }
//            else {

//                $.notify(response.message, "error");
//            }
//        },
//        error: function (xhr) {
//            $.notify("Error", "error");
//        }
//    });
//}

//function onConfirm() {
//    var id = parseInt($("#id").val());
//    $.ajax({
//        url: routeURL + '/api/Appointment/ConfirmEvent/' + id,
//        type: 'GET',
//        dataType: 'JSON',
//        success: function (response) {

//            if (response.status === 1) {
//                $.notify(response.message, "success");
//                calendar.refetchEvents();
//                onCloseModal();
//            }
//            else {

//                $.notify(response.message, "error");
//            }
//        },
//        error: function (xhr) {
//            $.notify("Error", "error");
//        }
//    });
//}


