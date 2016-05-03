angular.module('mediainventory').controller('ArtistEditController', function ($scope, $location, ROUTES, ArtistService, artistId) {
	var masterEntity = new ArtistService();
	if (artistId) {
		masterEntity = ArtistService.get(artistId).then(function (response) {
			$scope.artist = response;
		});
	}
	$scope.artist = masterEntity;

	$scope.saveAddNew = function () {
		$scope.save(true);
	};

	$scope.save = function (addAnother) {
		$scope.artist.save().then(function () {
			if (addAnother)
				$location.path(ROUTES.ARTIST.NEW);
			else
				$location.path(ROUTES.ARTIST.LIST);
		});
	};
});