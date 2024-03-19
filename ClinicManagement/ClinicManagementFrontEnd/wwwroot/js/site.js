// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    var columns;
    var _gender = '';
    var _name= '';
    $(document).ready(function () {
      FormatNumber = function (input) {
        return $.fn.dataTable.render.number(',', '.', 0).display(input);
      }
      columns = [
          { "title": "Full Name", "data": "name", "searchable": true },
          { "title": "Birth Date", "data": "dob", "searchable": true },
        { "title": "Age", "data": "age", "searchable": true },
          { "title": "Gender", "data": "gender", "searchable": true },
          { "title": "Disease", "data": "disease", "searchable": true },
          { "title": "Doctor", "data": "doctor", "searchable": true },
      ];
                columns.push({
                    "title": "Action", "data": "id", "class": "center", "render": function (id) {
                        return '<a class="btn btn-outline-info" href="/PatientManagement/Update?PatientId=' + id + '" type="button" >Update</a> ';
                    }
                });
            console.log(columns);

            

            var table = $('#tblReport').DataTable({
                "ordering": false,
            "pageLength": 25,
            "processing": true,
            "serverSide": true,
            "dom": "tipr",
            "columnDefs": [{

            },

            ],
            "ajax": {
                "url": "/PatientManagement/GetPatientReport",
            "type": "POST",
            "dataType": "json",
            "data": function (d) {
                d.AdditionalParameters = JSON.stringify({
                    Name: _name,
                    gender: _gender
                });
          }, "error": function (xhMessage, error, thrown) {

            }

        },
            "columns": columns
      });

            $("#SearchBtn").on('click', function () {
                _gender = $("#gender").val();
            _name = $("#Name").val();
            table.draw();
      })
    });
