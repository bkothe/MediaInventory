angular.module('mediainventory').controller('ArtistCtrl', function ($scope, $rootScope, artistId, ArtistService) {
	ArtistService.get(artistId).then(function (response) {
		$scope.artist = response;
	});
});