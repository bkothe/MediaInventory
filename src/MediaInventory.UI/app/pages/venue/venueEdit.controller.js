angular.module('mediainventory').controller('VenueEditController', function ($scope, $location, PAGE_URLS, VenueService, venueId) {
	var masterEntity = new VenueService();
	if (venueId) {
		masterEntity = VenueService.get(venueId).then(function (response) {
			$scope.venue = response;
		});
	}
	$scope.venue = masterEntity;

	$scope.saveAddNew = function () {
		$scope.save(true);
	};

	$scope.save = function (addAnother) {
		$scope.venue.save().then(function () {
			if (addAnother)
				$location.path(PAGE_URLS.VENUE.NEW);
			else
				$location.path(PAGE_URLS.VENUE.LIST);
		});
	};
});