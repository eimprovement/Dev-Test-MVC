
var app = angular.module("PetManagement", []);

// Controller Part
app.controller("PetController", function ($scope, $http) {


    $scope.pets = [];
    $scope.petForm = {
        id: null,
        name: "",
        status: ""
    };
    $scope.baseUrl = window._env.baseUrl;
    $scope.apiKey = window._env.apiKey;
 
    _refreshPetData();       
  
    // HTTP POST/PUT methods for add/edit pet  
    // Call: https://dev-test.azure-api.net/petstore/pet
    $scope.submitPet = function () {

        var method = "";
        var url = $scope.baseUrl;

        if ($scope.petForm.id == null)
        {
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
        $scope.petForm.id = null;
        $scope.petForm.name = "";
        $scope.petForm.status = "";
    };
});