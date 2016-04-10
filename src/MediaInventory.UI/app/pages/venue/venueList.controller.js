angular.module('mediainventory').controller('VenueListController', function ($scope, $rootScope, VenueService) {
	VenueService.enumerate().then(function (response) {
		$scope.venues = response;
	});
});