$("#NotesSendTo").multiselect({
    includeSelectAllOption: true,
    enableFiltering: true
});

function SetNotes() {
    if ($("#standardNotes option:selected").val() != "") {
        var notes = $("#standardNotes option:selected").text();
        $("#notesText").val(notes);
    } else {
        $("#notesText").val("");
    }
}

function GetRequestNotesDataTable() {

    if ($.fn.DataTable.isDataTable("#requestNotesTable")) {
        oTable.draw();
        $('#requestNotesTable').DataTable().ajax.reload();
    }
    else {

        var canDelete = $("#canDeleteNote").val();

        oTable =
            $('#requestNotesTable').DataTable({
                "aaSorting": [],
                "bServerSide": true,
                "sAjaxSource": "/RequestNotes/GetRequestNotesList",
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                    //var colName = oSettings.aoColumns[oSettings.aaSorting[0][0]].mData;
                    //var sDir = oSettings.aaSorting[0][1];

                    var requestId = $("#Request_ID").val();

                    //aoData.push({ "name": "sortCol", "value": colName });
                    //aoData.push({ "name": "sortDir", "value": sDir });

                    aoData.push({ "name": "requestId", "value": requestId });

                    $("#hiddenNotes").hide();

                    $.ajax({
                        type: "get",
                        data: aoData,
                        url: sSource,
                        success: function (resp) {

                            if (resp.hiddenNotesCount > 0) {
                                $("#hiddenNotesCount").text(resp.hiddenNotesCount);
                                $("#hiddenNotes").show();
                            }

                            return fnCallback(resp);
                        }
                    });

                },
                //drawCallback: function () {
                //    var pagination = $(this).closest('.dataTables_wrapper').find('.dataTables_paginate');
                //    pagination.toggle(this.api().page.info().pages > 1);
                //},
                "columns": [
                    {
                        data: "",
                        "render": function (row, type, full) {

                            var deletebtn = '';
                            if (canDelete === "1")
                                deletebtn = ' <a class="note-edit" data-id="' + full.ID + '" href="javascript: deleteNote(' + full.ID +');" class="anchor-design" title="Edit"><u>Delete</u></a>'

                            return '<a class="note-delete" data-id="' + full.ID + '" href="javascript: addNotes(' + full.ID +');" class="anchor-design" title="Edit"><u>Edit</u></a>' +
                                deletebtn;
                        }
                    },
                    {
                        data: "",
                        "render": function (row, type, full) {
                            var icns = '';
                            if (full.flaggedNote === 1)
                                icns = icns + '<i class="fa fa-flag text-primary"></i>';
                            if (full.internalNote === 1)
                                icns = icns + '<i class="fa fa-eye text-danger"></i>';

                            return '<div class="td-with-inline-icons">' +
                                '<div class="custom-control custom-checkbox custom-control-inline"><input type="checkbox" class="custom-control-input" id="noteCb' + full.ID + '" value="'+full.ID+'"><label class="custom-control-label" for="noteCb' + full.ID + '"></label></div>' +
                                icns +
                                '</div>';
                        }
                    },
                    { "data": "NotesDate", "title": "Note Date" },
                    { "data": "FollowupDate", "title": "Follow Up Date" },
                    { "data": "AuthorName", "title": "Author" },
                    { "data": "Notes", "title": "Contact Notes" }
                ],
                "columnDefs": [
                    { "orderable": false, "targets": "_all" }
                ],
                bProcessing: true,
                pageLength: 10,
                "bFilter": false,
                "paging": true,
                //bSearching: false,
                bLengthChange: false,
                lengthMenu: [10, 25, 50],
                "bInfo": true,
                "language": {
                    //"zeroRecords": "no search results",
                    //"info": "Page _PAGE_ of _PAGES_",
                    "emptyTable": "no records found",
                    "processing": "Processing... Please wait"
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    if (aData['flaggedNote'] === 1) {
                        $('td', nRow).css('background-color', '#FFE4B5');
                    }
                }
            });
    }
}

function getSelectedNotes() {
    var selectedNotes = [];
    $("#requestNotesTable").find("input:checked").each(function () {
        selectedNotes.push($(this).val());
    });
    return selectedNotes;
}


function deleteNote(noteid) {
    var requestId = $("#Request_ID").val();
    eModal.confirm({
        message: "Are you sure you want to delete this note?",
        //title: 'Confirm!',
        size: eModal.size.sm,
        subtitle: 'smaller text header',
        label: 'Yes' | 'True' | 'OK'
    })
        .then(function myfunction() {

            $.ajax({
                data: { noteId: noteid, requestId: requestId },
                url: '/RequestNotes/DeleteNote',
                type: 'post',
                dataType: 'json',
                success: function (resp) {
                    if (resp.success === true) {
                        GetRequestNotesDataTable();
                    }
                    else {
                        $.notify("There was an error", "danger");
                    }
                }
            });

        }, function myfunction() { });
}

function addNotes(noteId) {
    var requestId = $("#Request_ID").val();
    $(".se-pre-con").fadeIn();
    var clientId = parseInt($("#ClientId").val());
    if (isNaN(clientId)) { clientId = 0; }
    $.ajax({
        type: "Get",
        url: "/RequestNotes/AddEditNote",
        data: { noteId: noteId, clientId: clientId, requestId: requestId },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".se-pre-con").fadeOut("slow");
            if (response.Success) {
                if (noteId > 0) {
                    $('#addnotesModallLabel').html('Edit Note');
                }
                else {
                    $('#addnotesModallLabel').html('Add Note');
                }

                $('#addNotesDiv').html(response.Html);
                $('#addnotesModal').modal('show');


                $('#noteDate').daterangepicker({
                    autoUpdateInput: true,
                    singleDatePicker: true,
                    showDropdowns: true,
                    locale: { format: 'MM/DD/YYYY' },
                    minYear: 2000,
                    maxYear: parseInt(moment().format('YYYY'), 10)
                });
                $('#followupDate').daterangepicker({
                    autoUpdateInput: false,
                    singleDatePicker: true,
                    showDropdowns: true,
                    locale: { format: 'MM/DD/YYYY' },
                    minYear: 2000,
                    maxYear: parseInt(moment().format('YYYY'), 10)
                });

                $('#followupDate').on('apply.daterangepicker', function (ev, picker) {
                    $(this).val(picker.startDate.format('MM/DD/YYYY'));
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

function addNotesSubmit() {

    var noteDate = $("#noteDate").val();
    var noteAuthor = $("#noteAuthor").val();
    var followupDate = $("#followupDate").val();
    var followupTime = $("#followupTime").val();
    var sendToAuthorsOnly = $("#sendToAuthorsOnly").is(":checked") ? 1 : null;
    var notesText = $("#notesText").val();
    var requestId = $("#Request_ID").val();
    var internalNote = "";
    var noteId = $("#noteId").val();

    if ($("#internalNote").length > 0) {
        internalNote = $("#internalNote").is(":checked") ? 1 : "";
    }

    if ($.trim(followupDate).length && $.trim(followupTime).length > 0) {
        followupDate = followupDate + " " + followupTime;
    }
    //var sendToUsers = $("#NotesSendTo").val();

    $.ajax({
        url: '/RequestNotes/AddNote',
        data: {
            ID: noteId,
            RequestID: requestId,
            Notes: notesText,
            internalNote: internalNote,
            NotesDate: noteDate,
            FollowupDate: followupDate,
            AuthorID: noteAuthor,
            SendToAuthorOnly: sendToAuthorsOnly
        },
        type: 'post',
        dataType: 'json',
        success: function (resp) {
            if (resp.success === true) {

                $("#addnotesModal").modal("hide");
                GetRequestNotesDataTable();

                //$("#noteDate").val($("#notesDefaultDate").val());
                //$("#noteAuthor").val($("#currentUserId").val());
                //$("#followupDate").val("");
                //$("#followupTime").val("");
                //$("#sendToAuthorsOnly").prop("checked", false);
                //$("#internalNote").prop("checked", false);
                //$("#notesText").text("");
                //$("#notesText").val("");

                $.notify("Success.", "success");

            }
            else {
                if (resp.errorMessage.length === 0)
                    $.notify("There was an error", "danger");
                else
                    $.notify(resp.errorMessage, "danger");
            }
        }
    });

}

function flagNotes() {
    var selectedNotes = getSelectedNotes();
    //$("#requestNotesTable td").find("input:checked").each(function () {
    //    selectedNotes.push($(this).val());
    //});

    if (selectedNotes.length === 0) {
        $.notify("Please select some notes", "danger");
    }
    else {
        var requestId = $("#Request_ID").val();
        $.ajax({
            data: { notes: selectedNotes, requestId: requestId },
            traditional: true,
            url: '/RequestNotes/FlagUnflagNotes',
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                if (resp.success === true) {

                    $.notify("Success.", "success");
                    GetRequestNotesDataTable();
                }
                else {
                    $.notify("There was an error", "danger");
                }
            }
        });
    }
}

function hideNotes() {
    var selectedNotes = getSelectedNotes();
    if (selectedNotes.length === 0) {
        $.notify("Please select some notes", "danger");
    }
    else {
        var requestId = $("#Request_ID").val();
        $.ajax({
            data: { notes: selectedNotes, requestId: requestId },
            traditional: true,
            url: '/RequestNotes/HideNotes',
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                if (resp.success === true) {

                    $.notify("Success.", "success");
                    GetRequestNotesDataTable();
                }
                else {
                    $.notify("There was an error", "danger");
                }
            }
        });
    }
}

function showNotes() {
    var requestId = $("#Request_ID").val();
    $.ajax({
        data: { requestId: requestId },
        url: '/RequestNotes/ShowNotes',
        type: 'post',
        dataType: 'json',
        success: function (resp) {
            if (resp.success === true) {
                GetRequestNotesDataTable();
            }
            else {
                $.notify("There was an error", "danger");
            }
        }
    });
}

function sendNotes() {
    var selectedNotes = getSelectedNotes();
    var sendToUsers = $("#NotesSendTo").val();

    if (selectedNotes.length === 0) {
        $.notify("Please select some notes", "danger");
    }
    else if (sendToUsers.length === 0) {
        $.notify("Please select some users", "danger");
    }
    else {
        var requestId = $("#Request_ID").val();
        $.ajax({
            data: { notes: selectedNotes, requestId: requestId, users: sendToUsers },
            traditional: true,
            url: '/RequestNotes/SendNotes',
            type: 'post',
            dataType: 'json',
            success: function (resp) {
                if (resp.success === true) {
                    $.notify("Notes sent to selected users.", "success");
                    //GetRequestNotesDataTable();
                }
                else {
                    $.notify("There was an error", "danger");
                }
            }
        });
    }

}
