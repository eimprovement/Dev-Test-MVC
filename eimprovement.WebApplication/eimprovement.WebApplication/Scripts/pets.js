
function showAlertModal(modalName) {
    $('#' + modalName).modal('show');
    setTimeout(function () { $('#' + modalName).modal('hide'); }, 2000);
}


function showEditPetModal(id) {
    $('#waitingModal').modal('show');
    $.ajax({
        url: "/Pets/EditPetModal",
        type: "get",
        data: { id: id },
        success: function (data) {
            $('#waitingModal').modal('hide');
            $("#editPetModalContent").html(data);
            $('#editPetModal').modal('show');
        },
        error: function () {
            $('#waitingModal').modal('hide');
            showAlertModal("errorModal");
        }
    });
}

function ShowAddPetModal() {
    $.ajax({
        url: "/Pets/AddPetModal",
        type: "get",
        success: function (data) {
            $("#addPetModalContent").html(data);
            $('#addPetModal').modal('show');
        }
    });
}

function addPet() {
    var name = $("#petName").val();
    var status = $("#petStatus").val();

    $.ajax({
        url: "/Pets/AddPet",
        type: "post",
        data: { name: name, status: status },
        success: function (data) {
            $('#addPetModal').modal('hide');
            $("#addPetModalContent").html("");
            showAlertModal("successModal");
            location.reload(true);
        }
    });
}

function updatePet() {
    var id = $("#petId").val();
    var name = $("#petName").val();
    var status = $("#petStatus").val();

    $.ajax({
        url: "/Pets/SavePet",
        type: "put",
        data: { id: id, name: name, status: status },
        success: function (data) {
            $('#editPetModal').modal('hide');
            $("#editPetModalContent").html("");
            showAlertModal("successModal");
            location.reload(true);
        }
    });
}

function deletePet(id) {
    var id = $("#petId").val();
    var name = $("#petName").val();
    var status = $("#petStatus").val();

    $.ajax({
        url: "/Pets/DeletePet",
        type: "delete",
        data: { id: id, name: name, status: status },
        success: function (data) {
            $('#editPetModal').modal('hide');
            $("#editPetModalContent").html("");
            showAlertModal("successModal");
            location.reload(true);
        }
    });
}