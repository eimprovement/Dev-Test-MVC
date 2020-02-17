
var app = angular.module("PetManagement", ['ui.bootstrap']);

// Controller Part
app.controller("PetController", function ($scope, $http) {

    $scope.pets = [];
    $scope.petForm = {
        id: -1,
        name: "",
        status: "available",
        category : {
            id: 1,
            name: "animal"
        }
    };
    $scope.baseUrl = window._env.baseUrl;
    $scope.apiKey = window._env.apiKey;
    $scope.Status = ['available', 'unavailable'];
    $scope.Category = [{
        "id": 1,
        "name": "animal"
    }, {
        "id": 2,
        "name": "bird"
    }, {
        "id": 3,
        "name": "fish"
    }];

    $scope.viewby = 10;
    $scope.currentPage = 1;
    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize = 5; //Number of pager buttons to show

    _refreshPetData(); 
 
    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage = 1; //reset to first page
    }
  
    // HTTP POST/PUT methods for add/edit pet  
    // Call: https://dev-test.azure-api.net/petstore/pet
    $scope.submitPet = function () {

        var method = "";
        var url = $scope.baseUrl;

        if ($scope.petForm.id == null || $scope.petForm.id < 0)
        {
            $scope.petForm.id = null;
            method = "POST";           
        }
        else
        {
            method = "PUT";            
        }

        $http({
            method: method,
            url: url,
            data: angular.toJson($scope.petForm),
            headers: {
                'Content-Type': 'application/json',
                'Ocp-Apim-Subscription-Key': $scope.apiKey
            }
        }).then(_success, _error);
    };

    $scope.createPet = function () {
        _clearFormData();
    }

    // HTTP DELETE- delete pet by Id
    // Call: https://dev-test.azure-api.net/petstore/pet/{petid}
    $scope.deletePet = function (pet) {
        var method = "";
        var url = "";

        method = "DELETE";
        url = $scope.baseUrl + '/' + pet.id;

        $http({
            method: method,
            url: url,            
            headers: {
                'Content-Type': 'application/json',
                'Ocp-Apim-Subscription-Key': $scope.apiKey
            }
        }).then(_success, _error);
    };

    // In case of edit
    $scope.editPet = function (pet) {
        $scope.petForm.id = pet.id;
        $scope.petForm.name = pet.name;
        $scope.petForm.status = pet.status;
        $scope.petForm.category.id = pet.category.id;
        $scope.petForm.category.name = pet.category.name;
    };

    // Private Method  
    // HTTP GET- get all pets collection
    // Call: https://dev-test.azure-api.net/petstore/pet/findByStatus?status=available
    function _refreshPetData() {
        $http({
            method: 'GET',
            url: $scope.baseUrl + '/findByStatus?status=available',            
            headers: {
                'Ocp-Apim-Subscription-Key': $scope.apiKey
            }
        }).then(
            function (res) { // success               
                
                $scope.pets = res.data;
                $scope.totalItems = $scope.pets.length;
            },
            function (res) { // error
                console.log("Error: " + res.status + " : " + res.data);
            }
        );
    }

    function _success(res) {
        _refreshPetData();
        _clearFormData();
    }

    function _error(res) {
        var data = res.data;
        var status = res.status;
        var header = res.header;
        var config = res.config;
        alert("Error: " + status + ":" + data);
    }

    // Clear the form
    function _clearFormData() {
        $scope.petForm.id = -1;
        $scope.petForm.name = "";
        $scope.petForm.status = "available";
        $scope.petForm.category.id = 1;
        $scope.petForm.category.name = "animal";
    };
});