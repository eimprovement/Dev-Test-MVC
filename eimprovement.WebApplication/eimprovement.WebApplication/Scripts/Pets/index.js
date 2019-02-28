
var PetsVM = function () {
    var self = this;

    self.petManageMentModalReference = $('#petManagementModal');

    self.petData = ko.validatedObservable(new function(){
        var petSelf = this;
        
        petSelf.id = ko.observable().extend({
            required: { message: core.formValidationmessages.requiredFieldMessage, params: true },
            number: { message: core.formValidationmessages.numberOnlyMessage, params: true }
        });

        petSelf.isIdValid = ko.computed(function () {
            if (petSelf.id() >= Number.MAX_SAFE_INTEGER) {
                core.showMessage('warning', "This pet can't be updated or deleted at the time.");
                return false;
            }
            return true;
        }, this);

        petSelf.category = new function () {
            var catSelf = this;

            catSelf.id = ko.observable().extend({
                number: { message: core.formValidationmessages.numberOnlyMessage, params: true }
            });

            catSelf.name = ko.observable().extend({
                pattern: { message: core.formValidationmessages.alphanumericAndSpacesOnlyMessage, params: core.commonRegexPatters.alphanumericAndSpacesOnlyRegex }
            });

            catSelf.update = function (cat) {
                catSelf.id(cat.id);
                catSelf.name(cat.name);
            };

            catSelf.reset = function () {
                catSelf.update({});
                catSelf.id.isModified(false);
                catSelf.name.isModified(false);
            };
        };
        
        petSelf.name = ko.observable().extend({
            required: { message: core.formValidationmessages.requiredFieldMessage, params: true },
            pattern: { message: core.formValidationmessages.alphanumericAndSpacesOnlyMessage, params: core.commonRegexPatters.alphanumericAndSpacesOnlyRegex }
        });

        petSelf.photoUrls = ko.observableArray();

        petSelf.tags = ko.observableArray();
        
        petSelf.status = ko.observable().extend({
            required: { message: core.formValidationmessages.requiredFieldMessage, params: true }
        });
        
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

        petSelf.reset = function () {
            petSelf.update({});
            petSelf.id.isModified(false);
            petSelf.category.reset();
            petSelf.name.isModified(false);
            petSelf.status.isModified(false);
            petSelf.newTag.reset();
            petSelf.newPhotoUrl(null);
            petSelf.newPhotoUrl.isModified(false);
            petSelf.addingNewPhotoUrl(false);
            petSelf.addingNewTag(false);
        };

        petSelf.removePhotoUrl = function (item) {
            if (petSelf.photoUrls() == null || petSelf.photoUrls().length <= 0) {
                return;
            }

            petSelf.photoUrls.remove(item);
        };

        petSelf.addingNewPhotoUrl = ko.observable(false);

        petSelf.newPhotoUrl = ko.observable("").extend({
            url: { message: core.formValidationmessages.urlOnlyMessage, onlyIf: function () { return petSelf.addingNewPhotoUrl() == true; } }
        });
        
        petSelf.toggleNewPhotoUrlFlag = function () {
            petSelf.addingNewPhotoUrl(!petSelf.addingNewPhotoUrl());
        };

        petSelf.addNewPhotoUrl = function () {
            if (petSelf.newPhotoUrl() == null) {
                return;
            } else if (petSelf.newPhotoUrl.isValid() == false) {
                return;
            }
            petSelf.photoUrls.push(petSelf.newPhotoUrl());
            petSelf.newPhotoUrl(null);
            petSelf.newPhotoUrl.isModified(false);
        };

        petSelf.removeTag = function (item) {
            if (petSelf.tags() == null || petSelf.tags().length <= 0) {
                return;
            }

            petSelf.tags.remove(item);
        };

        petSelf.addingNewTag = ko.observable(false);

        petSelf.newTag = new function () {
            var tagSelf = this;

            tagSelf.id = ko.observable().extend({
                number: { message: core.formValidationmessages.numberOnlyMessage, onlyIf: function () { return petSelf.addingNewTag() == true; } }
            });

            tagSelf.name = ko.observable().extend({
                pattern: { message: core.formValidationmessages.alphanumericAndSpacesOnlyMessage, params: core.commonRegexPatters.alphanumericAndSpacesOnlyRegex, onlyIf: function() { return petSelf.addingNewTag() == true; } }
            });

            tagSelf.update = function (tag) {
                tagSelf.id(tag.id);
                tagSelf.name(tag.name);
            };

            tagSelf.reset = function () {
                tagSelf.update({});
                tagSelf.id.isModified(false);
                tagSelf.name.isModified(false);
            };
        };
        
        petSelf.toggleNewTagFlag = function () {
            petSelf.addingNewTag(!petSelf.addingNewTag());
        };

        petSelf.addNewTag = function () {
            if (petSelf.newTag.id() == null || petSelf.newTag.name() == null) {
                return;
            } else if (petSelf.newTag.id.isValid() == false || petSelf.newTag.name.isValid() == false) {
                return;
            }
            petSelf.tags.push({ id: petSelf.newTag.id(), name: petSelf.newTag.name() });
            petSelf.newTag.reset();
        };
    });
    
    self.currentAction = ko.observable().extend({ notify: 'always' });

    self.currentAction.subscribe(function (newValue) {
        core.clearMessages();
        self.petData().reset();
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

        if (self.statusFilter() == undefined || self.statusFilter().length == 0) {
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
        self.petData().update(item);
        if (self.petData().isIdValid() == true) {
            self.petManageMentModalReference.modal('show');
        }
    };

    self.updatePetData = function () {
        self.actionExecuting(true);
        var url = core.basePetsApiUrl + "/pet";
        var formData = ko.toJSON(self.petData());
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
        if (self.statusFilter.indexOf(self.petData().status()) == -1) {
            self.statusFilter.push(self.petData().status());
        } else {
            self.reFilter();
        }
    };

    self.petUpdateErrorHandler = function () {
        self.petManageMentModalReference.modal('hide');
        core.showMessage('alert', "There was an error updating the pet information on the system.");
        self.actionExecuting(false);
    };
    
    self.deletePetData = function () {
        self.actionExecuting(true);
        var url = core.basePetsApiUrl + "/pet/" + self.petData().id();
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
        var formData = ko.toJSON(self.petData());
        $.ajax({
            url: url,
            type: "POST",
            data: formData,
            contentType: "application/json; charset=utf-8",
            timeout: 20000,
            headers: core.petsApiAjaxHeaders,
            success: self.petAddSuccessHandler,
            error: self.petAddErrorHandler
        });
    };

    self.petAddSuccessHandler = function () {
        self.petManageMentModalReference.modal('hide');
        core.showMessage('success', "Pet added successfully.");
        self.actionExecuting(false);
        if (self.statusFilter.indexOf(self.petData().status()) == -1) {
            self.statusFilter.push(self.petData().status());
        } else {
            self.reFilter();
        }
    };

    self.petAddErrorHandler = function (error) {
        self.petManageMentModalReference.modal('hide');
        core.showMessage('alert', "There was an error addind the pet from the system.");
        self.actionExecuting(false);
    };
};
