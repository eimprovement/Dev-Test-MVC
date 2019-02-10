$(function () {
    FindByStatus();
});

function ConfirmDelete(trigger) {
    var id = $(trigger).attr("id").split('-')[1];
    $.confirm({
        title: 'Message',
        content: 'Are you sure you want to delete this record?',
        type: 'orange',
        closeIcon: true,
        buttons: {
            Yes: function () {
                var antiForgeryToken = $("[name='__RequestVerificationToken']").val();
                $.ajax({
                    url: DeletePetURL,
                    type: 'POST',
                    data: { __RequestVerificationToken: antiForgeryToken, "id": id },
                    success: function (data) {
                        window.location.replace(IndexURL);
                    }
                });
            }
        }
    });
}

function FindByStatus() {
    $("#PetStatusDropdown").change(function () {
        $.ajax({
            url: IndexURL,
            type: 'GET',
            data: { "status": $(this).val(), "renderView": false },
            success: function (data) {
                $("#PetsTable").html(data);
            }
        });
    });
}