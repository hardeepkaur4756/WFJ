    var oTable;
var isFirstTime = true;
$(document).ready(function () {
    GetDataTable();
    $('#associateCounselTable').dataTable({
        "paging": false,
    });
});
function GetLocalCounselData() {
    
    //var clientId = $('#ddlClient option:selected').val();
    //var formTypeId = $('#ddlFormType option:selected').val();
    ////var searchKeyword = $('#txtSearchKeyword').val();
    //if (clientId == -1 && formTypeId == -1 && "@userType" != "@((int)UserType.ClientUser)") {// && searchKeyword == "") {
    //    $('#errormessage').text('Please select atleast one value for search').css("color", "red");
    //}
    //else {
        $('#errormessage').text('');
        GetDataTable();
   /* }*/
}
        

        function GetDataTable() {
            if ($.fn.DataTable.isDataTable("#myDataTable")) {
                oTable.draw();
                $('#myDataTable').DataTable().ajax.reload();
            }
            else {
        oTable =
        $('#myDataTable').DataTable({
            "bServerSide": true,
            "sAjaxSource": "/LocalCounsel/GetLocalCounselList",
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                var colName = oSettings.aoColumns[oSettings.aaSorting[0][0]].mData;
                var sDir = oSettings.aaSorting[0][1];

                var firmName = $('#counselFirmName').val();;
                var attorneyName = $('#counselAttorneyName').val();;
                var contactName = $('#counselContactName').val();;
                var city = $('#counselCity').val();;
                var stateId = $('#ddlLocalCounselState option:selected').val();
                var countryId = $('#ddlLocalCounselCountry option:selected').val();

                aoData.push({ "name": "sortCol", "value": colName });
                aoData.push({ "name": "sortDir", "value": sDir });
                aoData.push({ "name": "firmName", "value": firmName });
                aoData.push({ "name": "attorneyName", "value": attorneyName });
                aoData.push({ "name": "contactName", "value": contactName });
                aoData.push({ "name": "city", "value": city });
                aoData.push({ "name": "stateId", "value": stateId });
                aoData.push({ "name": "countryId", "value": countryId });
                aoData.push({ "name": "isFirstTime", "value": isFirstTime });

                $.ajax({
                    type: "Get",
                    data: aoData,
                    url: sSource,
                    success: fnCallback
                });


                isFirstTime = false;
            },
            drawCallback: function () {
                var pagination = $(this).closest('.dataTables_wrapper').find('.dataTables_paginate');
                pagination.toggle(this.api().page.info().pages > 1);
            },
            "aoColumns": [
                { "mData": "FirmName" },
                { "mData": "ContactName" },
                { "mData": "AttorneyName" },
                { "mData": "City" },
                { "mData": "State" },
                { "mData": "Country" },
                {
                    "render": function (row, type, full) {
                        return '<a href="javascript:void(0)" onclick="editAssociateCounsel(' + full.Id + ')" class="btn btn-sm btn-success" title="View Detail">View Detail</a>';
                    },
                    orderable: false
                }
            ],
            "order": [[0, "asc"]],
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
function GetViewAddNewLocalConsel() {
    $("#FirmId").val("");
    $("#FirmName").val("");
    $("#PhoneNumber").val("");
    $("#AttorneyName").val("");
    $("#DirectLine").val("");
    $("#Address").val("");
    $("#Fax").val("");
    $("#Suite").val("");
    $("#Email").val("");
    $("#City").val("");
    $("#Website").val("");
    $("#State").val("");
    $("#FederalTaxId").val("");
    $("#ZipCode").val("");
    $("#Check").prop('checked', false);
    $("#Country").val("");
    $("#W9").prop('checked', false);
    $("#ALQ").prop('checked', false);
    $("#GeneralBar").prop('checked', false);
    $("#DoNot").prop('checked', false);
    $('#divShowLocalCounsel').addClass("hide");
    $('#divAddLocalCounsel').removeClass("hide");
    $("#Country").val("");
}
function addAssociateCounsel() {
        var isValid = true;
        var saveAddCounselViewModel = { };
        saveAddCounselViewModel.FirmId = $("#FirmId").val();
        saveAddCounselViewModel.FirmName = $("#FirmName").val();
        saveAddCounselViewModel.PhoneNumber = $("#PhoneNumber").val();
        saveAddCounselViewModel.AttorneyName = $("#AttorneyName").val();
        saveAddCounselViewModel.DirectLine = $("#DirectLine").val();
        saveAddCounselViewModel.Address = $("#Address").val();
        saveAddCounselViewModel.Fax = $("#Fax").val();
        saveAddCounselViewModel.Suite = $("#Suite").val();
        saveAddCounselViewModel.Email = $("#Email").val();
        saveAddCounselViewModel.City = $("#City").val();
        saveAddCounselViewModel.Website = $("#Website").val();
        saveAddCounselViewModel.State = $("#State").val();
        saveAddCounselViewModel.FederalTaxId = $("#FederalTaxId").val();
        saveAddCounselViewModel.ZipCode = $("#ZipCode").val();
        saveAddCounselViewModel.Check = $("#Check").val();
        saveAddCounselViewModel.Country = $("#Country").val();
        saveAddCounselViewModel.W9 = $("#W9").val();
        saveAddCounselViewModel.ALQ = $("#ALQ").val();
        saveAddCounselViewModel.GeneralBar = $("#GeneralBar").val();
        saveAddCounselViewModel.DoNot = $("#DoNot").val();
        saveAddCounselViewModel.Notes = $("#Notes").val();
        if (isValid) {
        $.ajax({
            type: "Post",
            url: "/LocalCounsel/AddLocalCounsel",
            data: { 'addLocalCounselViewModel': saveAddCounselViewModel },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#FirmId").val(response.firmId);
                    $('#divAddLocalCounsel').removeClass("hide");
                    $('#divShowLocalCounsel').addClass("hide");
                    //if ($("#FirmId").val() > 0) {
                        
                    //}
                    //else {
                    //    $.notify("Local Counsel Added Succesfully.", "success");
                    //}

                    $('#divShowLocalCounsel').removeClass("hide");
                    $('#divAddLocalCounsel').addClass("hide");
                }
                else {
                }
            },
            error: function (result) {
            }
        });
        } else {
        scrollUp();
        }
    }
function editAssociateCounsel(id) {
        if (id > 0) {
        $.ajax({
            type: "get",
            url: "/LocalCounsel/GetLocalCounselDetail?firmId=" + id,
            dataType: "html",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $('#divhtmlAddLocalCount').empty();
                $('#divhtmlAddLocalCount').html(response);
                //$('#associateCounselTable').dataTable({
                //    "paging": false,
                //});
                $('#divAddLocalCounsel').removeClass("hide");
                $('#divShowLocalCounsel').addClass("hide");
                $('#gridAssociateCounsilFileInfo').removeClass("hide");
            },
            error: function (result) {
            }
        });
        }
    }
function deleteAssociateCounsel() {
        var id = $('#FirmId').val()
        if (confirm("Are you sure you want delete this?")) {
        $.ajax({
            type: "Post",
            url: "/LocalCounsel/DeleteAssociateCounsel",
            data: { 'FirmId': id },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#requestDocumentGrid").html(response.html);
                    $.notify("Associate Counsel Deleted Succesfully.", "success");
                    showLocalCounselGrid();
                }
                else {

                }
            },
            error: function (result) {
            }
        });
        }
        return false;

    }
function showLocalCounselGrid() {
        $('#divShowLocalCounsel').removeClass("hide");
        $('#divAddLocalCounsel').addClass("hide");
    }
function addPersonnelRequests() {
        var isValid = true;
        var reqId = parseInt($("#Request_ID").val());
        if (isNaN(reqId)) {reqId = 0; }
        var savePersonnelRequests = { };
        savePersonnelRequests.FirmId = $("#FirmId").val();
        savePersonnelRequests.RequestID = reqId;


        if (isValid) {
        $.ajax({
            type: "Post",
            url: "/LocalCounsel/AddPersonnelRequests",
            data: { 'personnelRequestModel': savePersonnelRequests },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $.notify("Local Counsel Added Succesfully.", "success");
                }
                else {
                }
            },
            error: function (result) {
            }
        });
        } else {
        scrollUp();
        }
    }
function deletePersonnelRequests() {
        var id = parseInt($("#Request_ID").val());
        if (confirm("Are you sure you want delete this?")) {
        $.ajax({
            type: "Post",
            url: "/LocalCounsel/DeletePersonnelRequests",
            data: { 'requestId': id },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#requestDocumentGrid").empty();
                    $("#requestDocumentGrid").html(response.html);
                   /* $.notify("PersonnelRequests Deleted Succesfully.", "success");*/
                    if ($('#divLocalCounselSearch').hasClass("hide")) {
                        $('#divLocalCounselSearch').removeClass("hide")
                        $('#divAssignedFile').addClass("hide")
                    }
                }
                else {

                }
            },
            error: function (result) {
            }
        });
        }
        return false;

    }
