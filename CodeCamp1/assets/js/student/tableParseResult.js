(function ($) {

    $.fn.BasicTableParseResult = function (options) {

        var settings = $.extend({
            // These are the defaults.
            masterData: "",
            masterColumns: [],
        }, options);

        var masterData = settings.masterData;
        var masterColumns = settings.masterColumns;


        if ($.fn.DataTable.isDataTable(".display")) {
            $('.display').DataTable().clear().destroy();
        }

        $(".display").html("");

        var masterHeader = '<thead><tr><th data-field="" data-align="left" data-sortable="true">Num</th>';

        for (var c = 0; c < masterColumns.length; c++) {
            masterHeader = masterHeader + '<th data-field=' + masterColumns[c][0] + 'data-align="left" data-sortable="true">' + '<i class="' + masterColumns[c][2] + '"></i>' + masterColumns[c][1] + '</th>'
        }

        masterHeader = masterHeader + '<th data-field=""><i class="fa fa-tasks"></i>ACTION</th></tr></thead><tbody></tbody>';

        $(".display").append(masterHeader);

        for (var i = 0; i < masterData.length; i++) {

            var tr = "<tr>";
            //tr += '<td>' + '</td>';
            tr += '<td>' + i + '</td>';

            for (var m = 0; m < masterColumns.length; m++) {                
                var colType = masterColumns[m][2];
                var value = masterData[i][masterColumns[m][0]];

                if (colType == 'amount')
                    tr += '<td class="amountCell" > ' + masterData[i][masterColumns[m][0]] + '</td > ';
                else
                    tr += '<td class="textCell" > ' + masterData[i][masterColumns[m][0]] + '</td > ';
                //alert(masterData[i][masterColumns[m][0]]);
            }

            //tr += '<td>' + subtable + '</td>';
            tr += '<td  class="action-link">' 
                +"<a class=\"profile\" href =\"#\" title=\"Profile\" data-toggle=\"modal\" data-target=\"#viewProfileModal11\"><i class=\"fa fa-user\"></i></a>"
                +"<a class=\"edit\" href =\"#\" title=\"Edit\" data-toggle=\"modal\" data-target=\"#editDetailModal11\"><i class=\"fa fa-edit\"></i></a>"
                +"<a class=\"delete\" href =\"#\" title=\"Delete\" data-toggle=\"modal\" data-target=\"#deleteDetailModal11\"><i class=\"fa fa-remove\"></i></a>"
                + '</td>';

            tr += '</tr>';

            $(".display > tbody").append(tr);           

        } // End For



        var size = $(".display > thead > tr >th").length;
        

        var oTable = $(".display").DataTable({
            "initComplete": function (settings, json) {
            },
            "createdRow": function (row, masterData, index) {
            },
            "lengthMenu": [[50, 100, 250, 500, 1000, -1], [50, 100, 250, 500, 1000, "All"]],
            "columnDefs": [
            //    {
            //    "targets": 0,
            //    "data": null,
            //    "className": 'details-control',
            //    "orderable": false,
            //    "ordering": false,
            //    "data": null,
            //    "defaultContent": '',
            //},
            {
                "targets": size -1,
                "orderable": true,
                    "ordering": true,
                "visible": true
                }
            ],
            "order": [[1, 'asc']],
            'colReorder': true
        });

        //alert(size);

    };

}(jQuery));