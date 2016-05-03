angular.module('mediainventory').controller('VenueController', function ($scope, $rootScope, VenueService, venueId) {
	VenueService.get(venueId).then(function (response) {
		$scope.venue = response;
	});
});