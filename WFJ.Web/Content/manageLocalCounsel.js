

    var oTable;
var isFirstTime = true;
$(document).ready(function () {
    GetDataTable();
    $('#associateCounselTable').dataTable({
        "paging": false,
    });
        var isChecked = $("#inHouseCounsel").is(':checked');
    if (isChecked) {
        $(".divhouseCounselOther").css("display", "none");
        $(".divhouseCounsel").css("display", "block");
       
    }
    else {
        $(".divhouseCounselOther").css("display", "block");
        $(".divhouseCounsel").css("display", "none");
    }
});

$(function () {
    $('#inHouseCounsel').change(function () {
        var isChecked = $(this).is(':checked');
        if (isChecked) {
            $(".divhouseCounselOther").css("display", "none");
            $(".divhouseCounsel").css("display", "block");
            $('#counselFirmName').val("");
            $('#counselAttorneyName').val("");
            $('#counselContactName').val("");
            $('#counselCity').val();
            $('#ddlLocalCounselState').val("-1");
            $('#ddlLocalCounselCountry').val("-1");
        }
        else {
            $(".divhouseCounselOther").css("display", "block");
            $(".divhouseCounsel").css("display", "none");
            $('#ddlLocalCounselWFJAttorneys').val("-1");
        }
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

                var firmName = $('#counselFirmName').val();
                var attorneyName = $('#counselAttorneyName').val();
                var contactName = $('#counselContactName').val();
                var city = $('#counselCity').val();
                var stateId = $('#ddlLocalCounselState option:selected').val();
                var countryId = $('#ddlLocalCounselCountry option:selected').val();
                var wfjAttorneyId = $('#ddlLocalCounselWFJAttorneys option:selected').val();

                aoData.push({ "name": "sortCol", "value": colName });
                aoData.push({ "name": "sortDir", "value": sDir });
                aoData.push({ "name": "firmName", "value": firmName });
                aoData.push({ "name": "attorneyName", "value": attorneyName });
                aoData.push({ "name": "contactName", "value": contactName });
                aoData.push({ "name": "city", "value": city });
                aoData.push({ "name": "stateId", "value": stateId });
                aoData.push({ "name": "countryId", "value": countryId });
                aoData.push({ "name": "isFirstTime", "value": isFirstTime });
                aoData.push({ "name": "wfjAttorneyId", "value": wfjAttorneyId });
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
                var rowCount = this.fnSettings().fnRecordsDisplay();
                if (rowCount <= 10 || isNaN(rowCount)) {
                    $('.dataTables_length').hide();
                }
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

                        return '<div class="d-flex"><a href="javascript:void(0)" onclick="editAssociateCounsel(' + full.Id + ')" class="btn btn-sm btn-primary" title="View Detail"><i class="fa fa-eye"></i></i></a><a href="javascript:void(0)" onclick="addPersonnelRequests(' + full.Id + ')" class="btn btn-sm btn-primary" ' + (full.DoNotUse === 1 ? 'style="Display:none;margin-left:2px;"' :'style="margin-left: 2px;"')+' title="Select Firm"><i class="fa fa-plus"></i></i></a></div>';
                        
                    },
                    orderable: false
                }
            ],
            "autoWidth": false,
            "columnDefs": [{width:'125px',targets:0}],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.DoNotUse === 1) {
                    $('td', nRow).css('color', 'var(--sidebarcolor)');
                } 
            },
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
    $("#WrightHolmess").prop('checked', false);
    //$("#GB").prop('checked', false);
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
        saveAddCounselViewModel.Country = $("#Country").val();
        saveAddCounselViewModel.Check = $("#Check").is(":checked") ? "true" : "false";
        saveAddCounselViewModel.W9 = $("#W9").is(":checked") ? "true" : "false";
        saveAddCounselViewModel.ALQ = $("#ALQ").is(":checked") ? "true" : "false";
        saveAddCounselViewModel.GB = $("#GB").is(":checked") ? "true" : "false";
        saveAddCounselViewModel.GeneralBar = $("#GeneralBar").is(":checked") ? "true" : "false";
        saveAddCounselViewModel.WrightHolmess = $("#WrightHolmess").is(":checked") ? "true" : "false";
        saveAddCounselViewModel.DoNotUse = $("#DoNotUse").is(":checked") ? "true" : "false";
        saveAddCounselViewModel.Notes = $("#Notes").val();
    if ($("#FirmName").val() == "") {
        isValid = false;
    }
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
                    GetDataTable();
                }
                else {
                }
            },
            error: function (result) {
            }
        });
        } else {
            $.notify("Please enter firmname.", "danger");
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
                GetDataTable();
                window.scrollTo(0, 0);
            },
            error: function (result) {
            }
        });
        }
    }
function deleteAssociateCounsel() {
    var id = $('#FirmId').val()
    eModal.confirm({
        message: "Are you sure you want delete this?",
        //title: 'Confirm!',
        size: eModal.size.sm,
        subtitle: 'smaller text header',
        label: 'Yes' | 'True' | 'OK'
    })
        .then(function myfunction() {
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

        }, function myfunction() { });
    }
function showLocalCounselGrid() {
        $('#divShowLocalCounsel').removeClass("hide");
        $('#divAddLocalCounsel').addClass("hide");
    }
function addPersonnelRequests(id) {
        var isValid = true;
        var reqId = parseInt($("#Request_ID").val());
        if (isNaN(reqId)) {reqId = 0; }
    var savePersonnelRequests = {};
    if (id > 0) {
        savePersonnelRequests.FirmId = id
    } else {
        savePersonnelRequests.FirmId = $("#FirmId").val();
    }
        //savePersonnelRequests.FirmId = $("#FirmId").val();
        savePersonnelRequests.RequestID = reqId;


        if (isValid) {
        $.ajax({
            type: "Post",
            url: "/LocalCounsel/AddPersonnelRequests",
            data: { 'personnelRequestModel': savePersonnelRequests },
            dataType: "html",
            success: function (response) {
                $('#divAssignedFileHtml').empty();
                $('#divAssignedFileHtml').html(response);
                $('#divAssignedFile').removeClass("hide");
                $('#divLocalCounselSearch').addClass("hide");
                window.scrollTo(0, 0);
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
    eModal.confirm({
        message: "Are you sure you want to delete this Personnel Request?",
        //title: 'Confirm!',
        size: eModal.size.sm,
        subtitle: 'smaller text header',
        label: 'Yes' | 'True' | 'OK'
    })
        .then(function myfunction() {
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
                            $('#divAssignedFile').addClass("hide");
                            $('#divAddLocalCounsel').addClass("hide")
                        }
                        if ($('#divShowLocalCounsel').hasClass("hide")) {
                            $('#divShowLocalCounsel').removeClass("hide")
                        }
                    }
                    else {

                    }
                },
                error: function (result) {
                }
            });

        }, function myfunction() { });
    }
