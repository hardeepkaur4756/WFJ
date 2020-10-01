var oTable;
var isFirstTime = true;
$(document).ready(function () {
    GetDatafortable();
});
  function SearchBy() {

    var clientid = $('#ddlClient option:selected').val();
    var activeid = $('#ddlActive option:selected').val();
    var name = $('#txtname').val();
      if (clientid == -1 && activeid == -1 && name == "") {
          alert('error');
          $('#errormessage').text('Please Select atleast one value for searching').css("color", "red");

      }
      else {
          GetDatafortable();
      }
     
}

           function GetDatafortable () {
                if ($.fn.DataTable.isDataTable("#manageMyUsers")) {
        oTable.draw();
            }
                else {
        oTable =
        $('#manageMyUsers').DataTable({
            "bServerSide": true,
            "sAjaxSource": "/Home/GetUsersList",
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                var colName = oSettings.aoColumns[oSettings.aaSorting[0][0]].mData;
                var sDir = oSettings.aaSorting[0][1];
                var clientid = $('#ddlClient option:selected').val();
                var activeid = $('#ddlActive option:selected').val();
                var name = $('#txtname').val();

                aoData.push({ "name": "sortCol", "value": colName });
                aoData.push({ "name": "sortDir", "value": sDir });
                aoData.push({ "name": "clientId", "value": clientid });
                aoData.push({ "name": "active", "value": activeid });
                aoData.push({ "name": "name", "value": name });
                $.ajax({
                    type: "Get",
                    data: aoData,
                    url: sSource,
                    success: fnCallback
                })
            },
            "aoColumns": [
                {
                    "mData": null,
                    "render": function (row, type, full) {
                        var buttons = "<a class='anchor-design' href='#' id=''  data-toggle='modal' data-target='#edituser' onclick='return AddOrEdit(this)'>Edit</a>";
                        return buttons;
                    }
                },
               { "mData": "ClientName" },
                { "mData": "UserName" },
                { "mData": "ManagerName" },
                { "mData": "LevelName" },
                { "mData": "AccessLevel" },
                { "mData": "Active" }



            ],
            "aoColumnDefs": [
                { "bSortable": false, "aTargets": [4] }
            ],
            "order": [[2, "desc"]],
            bProcessing: true,
            pageLength: 5,
            "bFilter": false,
            "paging": true,
            //bSearching: false,
            bLengthChange: false,
            "language": {
                "zeroRecords": "Not records found",
                "info": "Page _PAGE_ of _PAGES_",
                "infoEmpty": "No records",
                "processing": "Processing... Please wait",
            }
        });
    }

}


 