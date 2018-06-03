angapp.controller("mainController", ["$scope", "appProvider", "$mdDialog", function ($scope, appProvider, $mdDialog) {
    moment.locale('ru');
    $scope.selectedEmployee = null;
    $scope.departments = [];
    $scope.gender = [
        {
            Description: "Муж.", Value: 0
        },
        {
            Description: "Жен.", Value: 1
        }
    ];

    function getEmployees() {
        appProvider.getAllEmployees().then(function (response) {
            $scope.selectedEmployee = null;
            $scope.employees = response.data;

            for (var i = 0; i < $scope.employees.length; i++) {
                $scope.employees[i].BirthDate = getFormatedDate($scope.employees[i].BirthDate);
            }
        }).catch(function (e) {
            console.log(e);
        });
    }

    function getFormatedDate(date, format) {
        var result = '';
        format = format ? format : "L";
        if (date) {
            var m = moment(date);
            result = m.format(format);
        }
        return result;
    }

    function getDepartments() {
        appProvider.getDepartments().then(function (response) {
            $scope.departments = response.data;
        });
    }

    getEmployees();
    getDepartments();

    $scope.selectEmployee = function (employee) {
        $scope.selectedEmployee = {};
        $scope.selectedEmployee = employee;
    }

    $scope.deleteEmployee = function (employee) {
        var confirm = $mdDialog.confirm()
          .title('Подтверждение')
          .textContent('Удалить пользователя ' + employee.Name)
          .ariaLabel('Delete user')
          .ok('Удалить')
          .cancel('Отмена');
        $mdDialog.show(confirm).then(function () {
            appProvider.deleteEmployee(employee.Id).then(function (content) {
                getEmployees();
            })
        }, function () {
        });
    }

    $scope.deleteReward = function (reward) {
        var confirm = $mdDialog.confirm()
          .title('Подтверждение')
          .textContent('Удалить награду?')
          .ariaLabel('Delete reward')
          .ok('Удалить')
          .cancel('Отмена');
        $mdDialog.show(confirm).then(function () {
            appProvider.deleteReward(reward.Id).then(function (content) {
                getEmployees();
            })
        }, function () {
        });
    }

    $scope.prepareEmployee = function () {
        $scope.newEmployee =
            {
                Name: "", Address: "", Phone: "", Department: {}, Gender: {}, BirthDate: null,
                valid: function () {
                    var obj = this;
                    return obj.Name && obj.Address && obj.Phone && obj.Department && obj.Gender && obj.BirthDate;
                }
            }
    }

    $scope.prepareReward = function () {
        $scope.newReward = {
            Name: "", Description: "", Year: "", EmployeeId: $scope.selectedEmployee.Id,
            valid: function () {
                var obj = this;
                return obj.Name && obj.Description && obj.Year;
            }
        }
    }

    $scope.addNewEmployee = function (newEmployee) {
        appProvider.createEmployee(newEmployee).then(function (content) {
            getEmployees();
        })
    }

    $scope.addNewReward = function (newReward) {
        appProvider.createReward(newReward).then(function (content) {
            getEmployees();
        })
    }

    $scope.prepareToChange = function (employee) {
        $scope.changedEmployee = angular.copy(employee);
    }

    $scope.changeEmployee = function () {
        appProvider.changeEmployee($scope.changedEmployee).then(function (content) {
            getEmployees();
            $scope.changedEmployee = {};
        })
    }

    $scope.editReward = function (reward) {
        $scope.changedReward = angular.copy(reward);
    }

    $scope.changeReward = function () {
        appProvider.changeReward($scope.changedReward).then(function (content) {
            getEmployees();
            $scope.changedReward = {};
        });
    }

    $scope.addPrepareDepartment = function () {
        $scope.newDepartment = { Name: "" };
    }

    $scope.addDepartment = function () {
        appProvider.addDepartment($scope.newDepartment).then(function (response) {
            getDepartments();
        });
    }
}]);