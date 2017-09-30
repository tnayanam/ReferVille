// toggle of myaccount in navb bar
$(function () {
    $('[data-admin-menu]').hover(function () {
        $('[data-admin-menu]').toggleClass('open')
    });
});

// show coverletter for  a particular company selected.

$(function () {
    var coverLetterSelect = $('#CoverLetterId');
    $('#CompanyId').change(function () {
        var companyId = $(this).val();
        var url = $(this).data('url');
        coverLetterSelect.empty();
        if (!companyId) {
            return;
        }
        // If others option is selected in the dropdown, then no need to make an ajax call to find coverletters
        if (companyId == 0) {
            $('.js-div').show();
            coverLetterSelect.append($('<option></option>').val('').text('None'));
        }
        else {
            $('.js-div').hide(); $.ajax({
                url: url,
                type: 'POST',
                data: { companyId: companyId },
                success: function (response) {
                    coverLetterSelect.append($('<option></option>').val('').text('None'));
                    $.each(response, function (i, data) {
                        coverLetterSelect.append($('<option></option>').val(data.Value).text(data.Text));
                    });
                },
                error: function () { }
            });
        }
    });
})

// check whether referral already exist
$(function () {
    var canSubmit = false;
    $('#referralform').submit(function (e) {
        // if Model state is not valid then return
        if (!$(this).valid()) {
            return // exit
        }
        if (!canSubmit) {
            e.preventDefault();
            var data = $('#referralform').serialize();
            var url = $(this).data('url');
            $.ajax({
                url: url,
                type: 'POST',
                data: data,
                success: function (response) {
                    if (response.hasPreviousRequest) {
                        if (confirm("You've already applied for this job. Apply again?")) {
                            canSubmit = true;
                            $('#referralform').submit();
                        }
                    }
                    else {
                        canSubmit = true;
                        $('#referralform').submit();
                    }
                },
                error: function () {
                    alert("error");
                }
            });
        }
    });
});

$(function () {
    var uploadButton = $('.js-toggle')
    $('#ReferralStatusId').change(function () {
        var referralStatusId = $(this).val();
        if (referralStatusId == 1)
            uploadButton.hide();
        else
            uploadButton.show();
    });
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

// Delete Application by referrer
$(".js-cancel-application").click(function (e) {
    var link = $(e.target);
    var referralId = $(this).data('referral-id');
    var url = $(this).data('url');
    debugger;
    bootbox.dialog({
        message: "Cancel Application?",
        title: "Confirm",
        buttons: {
            no: {
                label: "No",
                className: "btn-default",
                callback: function () {
                    bootbox.hideAll();
                }
            },
            yes: {
                label: "Yes",
                className: "btn-danger",
                callback: function () {
                    $.ajax({
                        type: 'POST',
                        //url: '/Referrer/Cancel',
                        url:url,
                        data: { referralId: referralId },
                    })
                    .done(function (result) {
                        debugger;
                        link.parents("tr").fadeOut(function () {
                            $(this).remove();
                        })
                    })
                    .fail(function () {
                        alert("failed");
                    });
                }
            }
        }
    });
});

