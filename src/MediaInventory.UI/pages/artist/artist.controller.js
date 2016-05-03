angular.module('mediainventory').controller('ArtistController', function ($scope, $rootScope, ArtistService, artistId) {
	ArtistService.get(artistId).then(function (response) {
		$scope.artist = response;
	});
});