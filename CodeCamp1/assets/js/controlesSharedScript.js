(function ($) {
    $.fn.GetGradeDropDownDbOptions = function (options) {

        var proxyUrl = "https://cors-anywhere.herokuapp.com/";

        var urlGrad = proxyUrl + 'http://schoolapi.thiqatech.com/api/Grade/ReadGradesJson/';

        var settings = $.extend({
            // These are the defaults.
            url: urlGrad,
            parametres: "",
        }, options);

        var element = $(this);

        var url = settings.url;
        var parametres = settings.parametres;

        var PageNoValue = 0;
        var RowCountPerPageValue = 0;
        var IsPagingValue = 0;
        var GradeIdValue = 0;
        var langValue = "en";

        $.ajax({
            url: url,
            data: JSON.stringify(
                {
                    PageNo: PageNoValue,
                    RowCountPerPage: RowCountPerPageValue,
                    IsPaging: IsPagingValue,
                    GradeId: GradeIdValue,
                    lang: langValue,
                }),
            //dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                console.log(result);

                $("<label class='clear-top-margin'><i class='fa fa-book'></i>TIMESLOT</label>").appendTo(element);
                $("<select>").appendTo(element);
                $("<option value=''>-- Select --</option>").appendTo(element.children("select"));
                for (var x = 0; x < result.length; x++) {
                    $('<option value="' + result[x].Guid + '">' + result[x].Code + ' - ' + result[x].Name + '</option>').appendTo(element.children("select"));
                }
                $("</select>").appendTo(element);
            },
            error: function (result) {
                console.log(result);
                alert("Error loading data! Please try again.");
            }
        }).done(function () {
        });

    };

    $.fn.GetSectionByGradeDropDownDbOptions = function (options) {

        var proxyUrl = "https://cors-anywhere.herokuapp.com/";

        var urlGrad = proxyUrl + 'http://schoolapi.thiqatech.com/api/Section/ReadSectionsByGrade/';

        var settings = $.extend({
            // These are the defaults.
            url: urlGrad,
            parametres: "",
        }, options);

        var element = $(this);

        var url = settings.url;
        var parametres = settings.parametres;

        console.log(parametres);

        $.ajax({
            url: url,
            data: parametres,
            //dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                console.log(result);

                $("<label class='clear-top-margin'><i class='fa fa-users'></i>SECTION</label>").appendTo(element);
                $("<select>").appendTo(element);
                $("<option value=''>-- Select --</option>").appendTo(element.children("select"));
                for (var x = 0; x < result.length; x++) {
                    $('<option value="' + result[x].Guid + '">' + result[x].Code + ' - ' + result[x].Name + '</option>').appendTo(element.children("select"));
                }
                $("</select>").appendTo(element);
            },
            error: function (result) {
                console.log(result);
                alert("Error loading data! Please try again.");
            }
        }).done(function () {
        });

    };

    $.fn.GetTimeSlotDropDownDbOptions = function (options) {

        var proxyUrl = "https://cors-anywhere.herokuapp.com/";

        var urlGrad = proxyUrl + 'http://schoolapi.thiqatech.com/api/TimeSlot/ReadTimeSlotsJson/';

        var settings = $.extend({
            // These are the defaults.
            url: urlGrad,
            parametres: "",
        }, options);

        var element = $(this);

        var url = settings.url;
        var parametres = settings.parametres;

        var PageNoValue = 0;
        var RowCountPerPageValue = 0;
        var IsPagingValue = 0;
        var TimeSlotIdValue = 0;
        var langValue = "en";

        $.ajax({
            url: url,
            data: JSON.stringify(
                {
                    PageNo: PageNoValue,
                    RowCountPerPage: RowCountPerPageValue,
                    IsPaging: IsPagingValue,
                    TimeSlotId: TimeSlotIdValue,
                    lang: langValue,
                }),
            //dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                console.log(result);

                $("<label class='clear-top-margin'><i class='fa fa-clock-o'></i>GRADE</label>").appendTo(element);
                $("<select>").appendTo(element);
                $("<option value=''>-- Select --</option>").appendTo(element.children("select"));
                for (var x = 0; x < result.length; x++) {
                    $('<option value="' + result[x].Id + '">' + result[x].Title + ' - ' + result[x].StartTime + ' - ' + result[x].EndTime + '</option>').appendTo(element.children("select"));
                }
                $("</select>").appendTo(element);
            },
            error: function (result) {
                console.log(result);
                alert("Error loading data! Please try again.");
            }
        }).done(function () {
        });

    };

    $.fn.GetCoursesByGradeSemesterDropDownDbOptions = function (options) {

        var proxyUrl = "https://cors-anywhere.herokuapp.com/";

        var urlGrad = proxyUrl + 'http://schoolapi.thiqatech.com/api/Course/ReadCoursesByGradeSemester/';

        var settings = $.extend({
            // These are the defaults.
            url: urlGrad,
            parametres: "",
        }, options);

        var element = $(this);

        var url = settings.url;
        var parametres = settings.parametres;

        console.log(parametres);

        $.ajax({
            url: url,
            data: parametres,
            //dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                console.log(result);

                $("<label class='clear-top-margin'><i class='fa fa-code'></i>COURSE</label>").appendTo(element);
                $("<select>").appendTo(element);
                $("<option value=''>-- Select --</option>").appendTo(element.children("select"));
                for (var x = 0; x < result.length; x++) {
                    $('<option value="' + result[x].Guid + '">' + result[x].Code + ' - ' + result[x].Name + '</option>').appendTo(element.children("select"));
                }
                $("</select>").appendTo(element);
            },
            error: function (result) {
                console.log(result);
                alert("Error loading data! Please try again.");
            }
        }).done(function () {
        });

    };
}(jQuery));