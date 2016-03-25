angular.module('mediainventory').controller('ArtistListCtrl', function ($scope, $rootScope, ArtistService) {
	ArtistService.enumerate().then(function (response) {
		$scope.artists = response;
	});
});