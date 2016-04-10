angular.module('mediainventory').controller('ArtistEditController', function ($scope, $location, PAGE_URLS, ArtistService, artistId) {
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
				$location.path(PAGE_URLS.ARTIST.NEW);
			else
				$location.path(PAGE_URLS.ARTIST.LIST);
		});
	};
});