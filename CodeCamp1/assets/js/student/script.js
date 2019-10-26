$('#gender').on('change', function () {
    // alert($('#gender option:selected').val());
});

$().ready(function () {
    //alert('ddeerr');
    $("#studentFullInfo").validate({
        // errorLabelContainer: $("#studentFullInfo div.error"),
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side


            StudentNum: "required",
            StudentName: "required",
            CourseId: "required",
            SemesterId: "required",
            ExamFaculty: "required",
            //StudentFirstName: {
            //    required: true,
            //    minlength: 6,
            //},
            ExamDate: {
                required: true,
                dateITA: true
            },
            MiddleExamMark: {
                required: true,
                number: true
            },
            ActivityMark: {
                required: true,
                number: true
            },
            FinalExamMark: {
                required: true,
                number: true
            },
            TotalMark: {
                required: true,
                number: true
            },
            FinalEstimation: {
                required: true
            }
           
        },
        // Specify validation error messages
        messages: {
            //StudentFirstName: "Please enter StudentFirstName",
            StudentNum: "Please enter StudentNum",
            StudentName: "Please enter Student Name",
            CourseId: "Please enter CourseCode",
            SemesterId: "Please enter ExamSemester",
            ExamFaculty: "Please enter ExamFaculty",
            ExamDate: "Please enter valid date dd/mm/yyyy",
            MiddleExamMark: {
                required: "Please provide a MiddleExamMark"
            },
            ActivityMark: {
                required: "Please provide a ActivityMark",
            },
            FinalExamMark: {
                required: "Please provide a ActivityMark",
            },
            TotalMark: {
                required: "Please provide a ActivityMark",
            },
            FinalEstimation: {
                required: "Please provide a ActivityMark",
            }
            
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        //submitHandler: function (form) {
        //    form.submit();
        //}
    });
});


function saveFunction() {
    //some code
    //alert('start');

    var validator = $("#studentFullInfo").valid();
    //$("#studentFullInfo").validate();


    console.log(validator);
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"

    if (validator === true) {
        $('#addStudentModal').modal('show');
    }

    else {
        alert('there are errors');
    }

}