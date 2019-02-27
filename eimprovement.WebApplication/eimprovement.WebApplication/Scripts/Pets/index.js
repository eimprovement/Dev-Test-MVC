
var PetsVM = function () {
    var self = this;

    self.petManageMentModalReference = $('#petManagementModal');

    self.petData = new function () {
        var petSelf = this;
        
        petSelf.id = ko.observable();
        petSelf.category = new function () {
            catSelf = this;

            catSelf.id = ko.observable();
            catSelf.name = ko.observable();

            catSelf.update = function (cat) {
                catSelf.id(cat.id);
                catSelf.name(cat.name);
            };
        };

        petSelf.name = ko.observable();
        petSelf.photoUrls = ko.observableArray();
        petSelf.tags = ko.observableArray();
        petSelf.status = ko.observable();
        
        petSelf.update = function (data) {
            petSelf.id(data.id);

            petSelf.category.id(undefined);
            petSelf.category.name(undefined);

            if (data.category != null) {
                petSelf.category.id(data.category.id);
                petSelf.category.name(data.category.name);
            }
            petSelf.name(data.name);
            petSelf.photoUrls([]);

            if (data.photoUrls != null) {
                $.each(data.photoUrls, function (index, item) {
                    petSelf.photoUrls.push(item);
                });
            }

            petSelf.tags([]);

            if (data.tags != null) {
                $.each(data.tags, function (index, item) {
                    petSelf.tags.push(item);
                });
            }

            petSelf.status(data.status);
        };

        petSelf.removePhotoUrl = function (item) {
            if (petSelf.photoUrls() == null || petSelf.photoUrls().length <= 0) {
                return;
            }

            petSelf.photoUrls.remove(item);
        }

        petSelf.removeTag = function (item) {
            if (petSelf.tags() == null || petSelf.tags().length <= 0) {
                return;
            }

            petSelf.tags.remove(item);
        }
        
    };

    self.currentAction = ko.observable();

    self.currentAction.subscribe(function (newValue) {
        core.clearMessages();
        if (newValue == "add") {
            self.petData.update({});
        }
    });

    self.actionExecuting = ko.observable(false);

    self.petsFoundByStatus = ko.observableArray([]);

    self.totalPetsFound = ko.computed(function () {
        return this.petsFoundByStatus().length;
    }, this);

    self.arePetsLoading = ko.observable(false);

    self.statusFilter = ko.observableArray();

    self.statusFilter.subscribe(function () {
        core.clearMessages();
        self.reFilter();
    });

    self.reFilter = function () {
        self.findPetsByStatus();
    };

    self.findPetsByStatus = function () {

        if (self.statusFilter() == undefined) {
            return;
        }

        self.arePetsLoading(true);
        self.petsFoundByStatus([]);
        var url = core.basePetsApiUrl + "/pet/findByStatus";
        var urlParameters = "?status=" + self.statusFilter().join();
        $.ajax({
            url: url + urlParameters,
            type: "GET",
            timeout: 20000,
            headers: core.petsApiAjaxHeaders,
            success: self.petsRetrieveSuccessHandler,
            error: self.petsRetrieveErrorHandler
        });
    };

    self.petsRetrieveSuccessHandler = function (items) {
        if (items != null) {
            if (items.length > 0) {
                $.each(items, function (index, item) {
                    self.petsFoundByStatus.push(item);
                });
            }
        } else {
            self.petsRetrieveError();
        }
        self.arePetsLoading(false);
    };

    self.petsRetrieveErrorHandler = function (error) {
        self.arePetsLoading(false);
        core.showMessage('alert', "There has been an error retrieving the pets.");
    };
    
    self.openPetManagementModal = function (action, item) {
        self.currentAction(action);
        self.petData.update(item);
        self.petManageMentModalReference.modal('show');
    };

    self.updatePetData = function () {
        self.actionExecuting(true);
        var url = core.basePetsApiUrl + "/pet";
        var formData = ko.toJSON(self.petData);
        $.ajax({
            url: url,
            type: "PUT",
            data: formData,
            contentType: "application/json; charset=utf-8",
            timeout: 20000,
            headers: core.petsApiAjaxHeaders,
            success: self.petUpdateSuccessHandler,
            error: self.petUpdateErrorHandler
        });
    };

    self.petUpdateSuccessHandler = function () {
        self.petManageMentModalReference.modal('hide');
        core.showMessage('success', "Pet updated successfully.");
        self.actionExecuting(false);
        self.reFilter();
    };

    self.petUpdateErrorHandler = function () {
        self.petManageMentModalReference.modal('hide');
        alert("There was an error updating the pet information on the system.");
        self.actionExecuting(false);
    };

    self.deletePetData = function () {
        self.actionExecuting(true);
        var url = core.basePetsApiUrl + "/pet/" + self.petData.id();
        $.ajax({
            url: url,
            type: "DELETE",
            timeout: 20000,
            headers: core.petsApiAjaxHeaders,
            success: self.petDeleteSuccessHandler,
            error: self.petDeleteErrorHandler
        });
    };

    self.petDeleteSuccessHandler = function () {
        self.petManageMentModalReference.modal('hide');
        core.showMessage('success', "Pet deleted successfully.");
        self.actionExecuting(false);
        self.reFilter();
    };

    self.petDeleteErrorHandler = function (error) {
        if (error.status == 200) {
            self.petDeleteSuccessHandler();
        } else {
            self.petManageMentModalReference.modal('hide');
            core.showMessage('alert', "There was an error deleting the pet from the system.");
            self.actionExecuting(false);
        }
    };

    self.addPetData = function () {
        self.actionExecuting(true);
        var url = core.basePetsApiUrl + "/pet";
        var formData = ko.toJSON(self.petData);
        $.ajax({
            url: url,
            type: "POST",
            data: formData,
            contentType: "application/json; charset=utf-8",
            timeout: 20000,
            headers: core.petsApiAjaxHeaders,
            success: self.petUpdateSuccessHandler,
            error: self.petUpdateErrorHandler
        });
    };

    self.petAddSuccessHandler = function () {
        self.petManageMentModalReference.modal('hide');
        core.showMessage('success', "Pet added successfully.");
        self.actionExecuting(false);
        self.reFilter();
    };

    self.petAddErrorHandler = function (error) {
        self.petManageMentModalReference.modal('hide');
        core.showMessage('alert', "There was an error addind the pet from the system.");
        self.actionExecuting(false);
    };
};
