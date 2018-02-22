var MainApp = angular.module('App', []);

MainApp.controller('RegistrationController', [
    '$scope','factory', function($scope, factory) {

        $scope.login = "";
        $scope.password = "";
        $scope.isCompany = false;
        $scope.isEmployee = false;
        $scope.subscription = "";
        $scope.HideSpinner = true;

        $scope.showCompany = function () {
            if (!$scope.isCompany && $scope.isEmployee)
                $scope.showCompanyCredentials = true;
            else {
                $scope.isEmployee = false;
                $scope.showCompanyCredentials = false;
            }
        };

        $scope.subscriptionSelection = function() {
            if ($scope.isCompany)
                $scope.subscription = "2";
        };


        $scope.RegisterUser = function() {

            $scope.HideSpinner = false;

            var client = {
                "email": $scope.email.toString(),
                "password": $scope.password.toString(),
                "firstName": $scope.firstName.toString(),
                "lastName": $scope.lastName.toString(),
                "isCompany": $scope.isCompany.toString(),
                "subscription": $scope.subscription.toString(),
                "city": $scope.city.toString(),
                "street": $scope.street.toString(),
                "house": $scope.house.toString(),
                "flat": $scope.flat.toString()
            };
            /*var company = {
                "companyEmail": $scope.companyEmail.toString(),
                "companyPassword": $scope.companyPassword.toString()
            }*/
            factory.SendInfo(client).then(function(result) {
                $scope.HideSpinner = true;

                if (result.data.IsSuccess) {
                    $scope.loginpasswordAnswer = result.data.ResponseObject;
                    $scope.rega = 'Ура, мы зарегались!';

                } else {
                    $scope.loginpasswordAnswer = result.data.ResponseObject;
                }

            });
        }
    }
]);

MainApp.factory('factory', [
    '$http', function ($http) {
        var service = {
            SendInfo: function (client) {
                return $http.post('/Home/RegisterClient', client);
            }
        };
        return service;
    }
]);

