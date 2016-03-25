angular.module('mediainventory').controller('ArtistNewCtrl', function ($scope, $location, ArtistService) {
	$scope.artist = new ArtistService();

	$scope.saveAddNew = function () {
		$scope.save(true);
	};

	$scope.save = function (addNew) {
		$scope.artist.save().then(function () {
			console.log('Saved');
			if (addNew)
				$scope.artist = new ArtistService();
			else
				$location.path("/artist/list/");
		});
	};
});