﻿ var oTable;
var isFirstTime = true;
$(document).ready(function () {
    GetDataGrid();
});

function SearchByDocumentFilters()
{
            var clientId = $('#ddlDocumentClient option:selected').val();
            var documentTypeId = $('#ddlDocumentType option:selected').val();
            var projectTypeId = $('#ddlProjectType option:selected').val();
             var practiceAreaId = $('#ddlPracticeArea option:selected').val();
             var categoryId = $('#ddlCategory option:selected').val();
            var formTypeId = $('#ddlFormType option:selected').val();
            var searchKeyword = $('#txtSearchKeyword').val();
            if (clientId == -1 && documentTypeId == -1 && projectTypeId == -1 && practiceAreaId == -1 && categoryId == -1 && formTypeId == -1 && searchKeyword == "") {
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
                var projectTypeId = $('#ddlProjectType option:selected').val();
                var practiceAreaId = $('#ddlPracticeArea option:selected').val();
                var categoryId = $('#ddlCategory option:selected').val();
                var formTypeId = $('#ddlFormType option:selected').val();
                var searchKeyword = $('#txtSearchKeyword').val();

                aoData.push({ "name": "sortCol", "value": colName });
                aoData.push({ "name": "sortDir", "value": sDir });
                aoData.push({ "name": "clientId", "value": clientId });
                aoData.push({ "name": "documentTypeId", "value": documentTypeId });
                aoData.push({ "name": "projectTypeId", "value": projectTypeId });
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
            "aoColumns": [
                {
                    "mData": null,
                    "render": function (row, type, full) {
                        //var buttons = "<a class='anchor-design' href='#' id=''  data-toggle='modal' data-target='#editdocument' onclick='return EditDocument()'>Edit</a>";
                        var buttons = "<a class='anchor-design' href='#' id='' data-Id='" + full.ID + "' data-toggle='modal' data-target='' onclick='return EditDocument(this)'>Edit</a>";
                        return buttons;
                    }
                },
                { "mData": "ClientName" },
                { "mData": "StateCodeID" },
                { "mData": "DocumentName" },
                { "mData": "DocumentTypeID" },
                { "mData": "PracticeAreaName" }




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
                "zeroRecords": "no record found",
                "info": "Page _PAGE_ of _PAGES_",
                //"infoEmpty": "No records",
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
            //data: { id: Id, "viewType": "Display" },
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
                    //$('#newdocument').find('.btn-primary').addclass('savebid');
                    $('#newdocument').modal('show');
                    $(".boot-multiselect222").multiselect({
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
                    //removeloader();
                }
                else {
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
                //$('#newdocument').find('.btn-primary').addclass('savebid');
                $('#newdocument').modal({ backdrop: "static", show: true });
                $(".boot-multiselect222").multiselect({
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
                //removeloader();
            }
            else {
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


