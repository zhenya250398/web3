angapp.provider("appProvider", function () {

    var getAllEmployees = function ($http) {
        return $http({
            url: "/Home/GetEmployees",
            method: "GET"
        });
    };

    var createEmployee = function ($http, employee) {
        return $http({
            url: "/Home/CreateEmployee",
            method: "POST",
            data: { employee }
        });
    }

    var deleteEmployee = function ($http, id) {
        return $http({
            url: "/Home/DeleteEmployee",
            method: "POST",
            data: { id }
        });
    }

    var deleteReward = function ($http, id) {
        return $http({
            url: "/Home/DeleteReward",
            method: "POST",
            data: { id }
        });
    }

    var getDepartments = function ($http) {
        return $http({
            url: "/Home/GetDepartments",
            method: "GET"
        });
    }

    var createReward = function ($http, reward) {
        return $http({
            url: "/Home/CreateReward",
            method: "POST",
            data: { reward }
        });
    }
    var changeEmployee = function ($http, employee) {
        return $http({
            url: "/Home/ChangeEmployee",
            method: "POST",
            data: { employee }
        });
    }

    var changeReward = function ($http, reward) {
        return $http({
            url: "/Home/ChangeReward",
            method: "POST",
            data: { reward }
        });
    }

    var addDepartment = function ($http, department) {
        return $http({
            url: "/Home/AddDepartment",
            method: "POST",
            data: { department }
        });
    }

    return {
        $get: ["$http", function ($http) {
            var service = {};
            service.getAllEmployees = function () {
                return getAllEmployees($http);
            }

            service.getDepartments = function () {
                return getDepartments($http);
            }

            service.createEmployee = function (employee) {
                return createEmployee($http, employee);
            }

            service.deleteEmployee = function (id) {
                return deleteEmployee($http, id);
            }

            service.deleteReward = function (id) {
                return deleteReward($http, id);
            }

            service.createReward = function (reward) {
                return createReward($http, reward);
            }

            service.changeEmployee = function (employee) {
                return changeEmployee($http, employee);
            }

            service.changeReward = function (reward) {
                return changeReward($http, reward);
            }

            service.addDepartment = function (newDepartment) {
                return addDepartment($http, newDepartment);
            }

            return service;
        }]
    };
});