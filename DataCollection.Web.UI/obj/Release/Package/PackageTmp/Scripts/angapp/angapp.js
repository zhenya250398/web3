var angapp = angular.module("angapp", ['ngMaterial']);

angapp.config(function ($mdIconProvider) {
    $mdIconProvider.iconSet('actions', '/Content/Images/material-icons/action-icons.svg', 24)
});