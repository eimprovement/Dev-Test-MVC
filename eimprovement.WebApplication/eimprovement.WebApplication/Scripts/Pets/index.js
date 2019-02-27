
var PetsVM = function () {
    var self = this;

    self.petManageMentModalReference = $('#petManagementModal');

    self.petData = new function () {
        var petSelf = this;

        petSelf.id = ko.observable();
        petSelf.category = ko.observableArray();
        petSelf.name = ko.observable();
        petSelf.photoUrls = ko.observableArray();
        petSelf.tags = ko.observableArray();
        petSelf.status = ko.observable();

        petSelf.update = function (data) {
            petSelf.id(data.id);
            petSelf.category(data.category);
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

    self.petsFoundByStatus = ko.observableArray([]);

    self.arePetsLoading = ko.observable(false);

    self.statusFilter = ko.observable();

    self.statusFilter.subscribe(function () {
        self.reFilter();
    });

    self.reFilter = function () {
        self.findPetsByStatus();
    };

    self.findPetsByStatus = function () {
        self.arePetsLoading(true);
        self.petsFoundByStatus([]);
        var url = core.basePetsApiUrl + "/pet/findByStatus";
        var urlParameters = "?status=" + self.statusFilter();
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
        alert("There has been an error retrieving the pets.");
    };

    self.serializeTags = function (tags) {
        var serializedTags = "--";
        if (tags == null) {
            return serializedTags;
        }

        if (tags.length == 0) {
            return serializedTags;
        }

        serializedTags = tags.map(function (tag) {
            return tag.name;
        }).join();

        return serializedTags
    };

    self.openPetManagementModal = function (item) {
        self.petData.update(item);
        self.petManageMentModalReference.modal('show');
    };

    self.updatePetData = function () {
        self.petManageMentModalReference.modal('hide');
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

    self.petUpdateSuccessHandler = function () {
        self.reFilter();
    };

    self.petUpdateErrorHandler = function () {
        alert("There was an error updating the pet information.");
    };
};
