
var oTable;
var isFirstTime = true;
$(document).ready(function () {
    GetDataGrid();
});

function SearchByDocumentFilters()
{
            var clientId = $('#ddlDocumentClient option:selected').val();
            var documentTypeId = $('#ddlDocumentType option:selected').val();
             var practiceAreaId = $('#ddlPracticeArea option:selected').val();
             var categoryId = $('#ddlCategory option:selected').val();
            var formTypeId = $('#ddlFormType option:selected').val();
            var searchKeyword = $('#txtSearchKeyword').val();
            if (clientId == -1 && documentTypeId == -1 && practiceAreaId == -1 && categoryId == -1 && formTypeId == -1 && searchKeyword == "") {
            $('#errormessage').text('Please select atleast one value for search').css("color", "red");
            }
            else
            {
            $('#errormessage').text('');
            GetDataGrid();
            }

}

function GetDataGrid() {
     
    if ($.fn.DataTable.isDataTable("#tblDocumentSearch"))
    {
        oTable.draw();
            }
   else
   {

        oTable =
        $('#tblDocumentSearch').DataTable({
            "bServerSide": true,
            "sAjaxSource": "/DocumentCenter/GetDocumentList",
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                var colName = oSettings.aoColumns[oSettings.aaSorting[0][0]].mData;
                var sDir = oSettings.aaSorting[0][1];
                var clientId = $('#ddlDocumentClient option:selected').val();
                var documentTypeId = $('#ddlDocumentType option:selected').val();
                var practiceAreaId = $('#ddlPracticeArea option:selected').val();
                var categoryId = $('#ddlCategory option:selected').val();
                var formTypeId = $('#ddlFormType option:selected').val();
                var searchKeyword = $('#txtSearchKeyword').val();
                aoData.push({ "name": "sortCol", "value": colName });
                aoData.push({ "name": "sortDir", "value": sDir });
                aoData.push({ "name": "clientId", "value": clientId });
                aoData.push({ "name": "documentTypeId", "value": documentTypeId });
                aoData.push({ "name": "practiceAreaId", "value": practiceAreaId });
                aoData.push({ "name": "categoryId", "value": categoryId });
                aoData.push({ "name": "formTypeId", "value": formTypeId });
                aoData.push({ "name": "searchKeyword", "value": searchKeyword });

                console.log(fnCallback);
                $.ajax({
                    type: "Get",
                    data: aoData,
                    url: sSource,
                    success: fnCallback
                });
            },
            drawCallback: function () {
                var pagination = $(this).closest('.dataTables_wrapper').find('.dataTables_paginate');
                pagination.toggle(this.api().page.info().pages > 1);
            },
            "aoColumns": [
                {
                    "mData": null,
                    "render": function (row, type, full) {
                        if (full.CurrentUserType == 1 || full.CurrentUserType == 6 || full.CurrentUserType == 8) {
                            var buttons = "<a class='anchor-design' href='#' id='' data-Id='" + full.ID + "' data-toggle='modal' data-target='' onclick='return EditDocument(this)'>Edit</a>";
                            return buttons;
                        }
                        else {
                            return "";
                        }
                        //var buttons = "<a class='anchor-design' href='#' id=''  data-toggle='modal' data-target='#editdocument' onclick='return EditDocument()'>Edit</a>";
                    }
                },
                { "mData": "ClientName" },
                //{ "mData": "StateCodeID" },
                //{ "mData": "StateCode" },
                { "mData": "State" },
                //{ "mData": "DocumentName" },
                {
                    "mData": "DocumentName",
                    "render": function (row, type, full) {
                        if (full.DocumentFullPath !="") {
                            var downloadlink = "<a class='anchor-design' href='" + full.DocumentFullPath + "' id='documentlink' Download>" + full.DocumentName + "</a>";
                            //var downloadlink = "<a class='anchor-design' href='DocumentCenter/DownloadFile?filename=" + full.FileName + "' id='documentlink'>" + full.DocumentName + "</a>";
                            return downloadlink;
                        }
                        else {
                            return full.DocumentName;
                        }
                    }
                },
                //{ "mData": "DocumentTypeID" },
                { "mData": "DocumentType" },
                { "mData": "PracticeAreaName" }
            ],
            //"aoColumnDefs": [
            //    { "bSortable": false, "aTargets": [1] }
            //],
            "order": [[2, "asc"]],
            bProcessing: true,
            pageLength: 10,
            "bFilter": false,
            "paging": true,
            //bSearching: false,
            bLengthChange: true,
            lengthMenu: [10, 25, 50],
            "bInfo": true,
            "language": {
                "zeroRecords": "no search results (too much data)",
                //"info": "Page _PAGE_ of _PAGES_",
                "emptyTable": "no records found",
                "processing": "Processing... Please wait",
            }
        });
    }

}

function EditDocument(event) {
    var Id = parseInt(event.getAttribute("data-Id"));
    if (Id == null || Id == undefined) {
        notificationHelper.ShowError("Some thing went wrong!");
    }
    else {

        $.ajax({
            type: "Get",
            url: "/DocumentCenter/EditDocument",
            data: { id: Id },
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.Success) {
                    if (Id > 0) {
                        $('#newdocument').find('#exampleModalLabel').html('Edit Document');
                    }
                    else {
                        $('#newdocument').find('#exampleModalLabel').html('Add User');
                    }

                    $('#newdocument').find('.modal-body').html(response.Html);
                    $('#newdocument').modal('show');
                    var dataarray = response.ClientId;
                    // Set the value
                    $("#boot-document-multiselect").val(dataarray);
                    $("#boot-document-multiselect").multiselect("refresh");
                    $("#boot-document-multiselect").multiselect({
                        includeSelectAllOption: true,
                        enableFiltering: true,
                    });
                   
                   
                    $(".custom-file-input").on("change", function () {
                        var fileName = $(this).val().split("\\").pop();
                        $(this)
                            .siblings(".custom-file-label")
                            .addClass("selected")
                            .html(fileName);
                    });
                }
                else {
                    //notificationhelper.showerror('sorry an error occured.')
                }
            },
            error: function (result) {
                //notificationHelper.ShowError(result.Message);
            }
        });
    }

};

function AddDocument(event) {
    $.ajax({
        type: "Get",
        url: "/DocumentCenter/AddDocument",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.Success) {
                $('#newdocument').find('#exampleModalLabel').html('Add Document');
                $('#newdocument').find('.modal-body').html(response.Html);
                $('#newdocument').modal({ backdrop: "static", show: true });
                $("#boot-document-multiselect").multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                });
                $(".custom-file-input").on("change", function () {
                    var fileName = $(this).val().split("\\").pop();
                    $(this)
                        .siblings(".custom-file-label")
                        .addClass("selected")
                        .html(fileName);
                });
            }
            else {
                //notificationhelper.showerror('sorry an error occured.')
            }
        },
        error: function (result) {
            //notificationHelper.ShowError(result.Message);
        }
    });
}


