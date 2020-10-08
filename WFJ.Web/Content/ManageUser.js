﻿var oTable;
var isFirstTime = true;
$(document).ready(function () {
    GetDatafortable();
});
  function SearchBy() {

    var clientid = $('#ddlClient option:selected').val();
    var activeid = $('#ddlActive option:selected').val();
    var name = $('#txtname').val();
      if (clientid == -1 && activeid == -1 && name == "") {
          $('#errormessage').text('Please select atleast one value for search').css("color", "red");

      }
      else {
          $('#errormessage').text('');
          GetDatafortable();
      }
     
}

function GetDatafortable() {
    var zeroRecordsMessage = "";
    if (isFirstTime) {
        zeroRecordsMessage = "no search results (too much data)";
        isFirstTime = false;
    }
    else {
        zeroRecordsMessage = "no records found";
    }
      if ($.fn.DataTable.isDataTable("#manageMyUsers"))
      {
                    oTable.draw();
      }
      else
      {
          

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
                console.log("fnCallback " + fnCallback);

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
                        var buttons = "<a class='anchor-design' href='#' id='' data-id='" + full.UserID +"' data-toggle='modal' data-target='' onclick='return EditUser(this)'>Edit</a>";
                        return buttons;
                    }
                },
               { "mData": "ClientName" },
                { "mData": "Fullname" },
                { "mData": "ManagerName" },
                { "mData": "LevelName" },
                { "mData": "AccessLevelName" },
                { "mData": "ActiveStatus" }



            ],
            "aoColumnDefs": [
                { "bSortable": false, "aTargets": [1] }
            ],
            "order": [[2, "asc"]],
            bProcessing: true,
            pageLength: 10,
            "bFilter": false,
            "paging": true,
            //bSearching: false,
            bLengthChange: false,
            "language": {
                "zeroRecords": zeroRecordsMessage,
                "info": "Page _PAGE_ of _PAGES_",
                //"infoEmpty": "No records",
                "processing": "Processing... Please wait",
            }
        });
    }

}

function EditUser(event) {
    var Id = parseInt(event.getAttribute("data-Id"));
    if (Id == null || Id == undefined) {
        notificationHelper.ShowError("Some thing went wrong!");
    }
    else {

        $.ajax({
            type: "Get",
            url: "/Home/EditUser",
            //data: { id: Id, "viewType": "Display" },
            data: { id: Id},
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.Success) {
                    console.log(JSON.stringify(response));
                    if (Id > 0) {
                        $('#newdocument').find('#exampleModalLabel').html('Edit User');
                    }
                    else {
                        $('#newdocument').find('#exampleModalLabel').html('Add User');
                    }
                    
                    $('#newdocument').find('.modal-body').html(response.Html);
                    //$('#newdocument').find('.btn-primary').addclass('savebid');
                    $('#newdocument').modal('show');
                    //removeloader();
                }
                else {
                    $('#AddEditErrorMsg').text(response.Message);
                    //notificationhelper.showerror('sorry an error occured.')
                    //removeloader();
                }
            },
            error: function (result) {
                //notificationHelper.ShowError(result.Message);
                //removeLoader();
            }
        });
    }

};

function AddUser(event) {
  
        $.ajax({
            type: "Get",
            url: "/Home/AddUser",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.Success) {
                    $('#newdocument').find('#exampleModalLabel').html('Add User');
                    $('#newdocument').find('.modal-body').html(response.Html);
                    //$('#newdocument').find('.btn-primary').addclass('savebid');
                    $('#newdocument').modal({ backdrop: "static", show: true });
                    //removeloader();
                }
                else {
                    $('#requestError').text(response.Message);
                    //notificationhelper.showerror('sorry an error occured.')
                    //removeloader();
                }
            },
            error: function (result) {
                //notificationHelper.ShowError(result.Message);
                //removeLoader();
            }
        });
};






 