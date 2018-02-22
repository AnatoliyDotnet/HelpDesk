var MainApp = angular.module('App', []);

MainApp.controller('LoginController', [
    '$scope', 'factory', function($scope, factory) {

        $scope.login = "";
        $scope.password = "";
        $scope.HideSpinner = true;

        $scope.SetLoginButtonEnable = function() {
            if (!isNullOrWhitespace($scope.login) && !isNullOrWhitespace($scope.password)) {
                $scope.loginpasswordAnswer = "";
                document.getElementById('loginbutton').disabled = false;
            }
            else {
                document.getElementById('loginbutton').disabled = true;
            }
        }

        function isNullOrWhitespace(input) {
            if (typeof input === 'undefined' || input == null) return true;

            return input.replace(/\s/g, '').length < 1;
        }

        $scope.LoginUser = function () {

            $scope.HideSpinner = false;

            var credentials = {
                "login": $scope.login.toString(),
                "password": $scope.password.toString()
            }

        factory.SendCredentials(credentials).then(function(result) {
            $scope.HideSpinner = true;

            if (result.data.IsSuccess) {
                $scope.loginpasswordAnswer = result.data.ResponseObject;
                window.location.pathname = 'Client';
            } else {
                $scope.loginpasswordAnswer = result.data.ResponseObject;
            }

        });

        }
    }
]);

MainApp.factory('factory', [
    '$http', function($http) {
        var service = {
            SendCredentials: function(credentials) {
                return $http.post('/Home/Login', credentials);
            }
        };
        return service;
    }
]);

