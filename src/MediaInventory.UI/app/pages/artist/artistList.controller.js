angular.module('mediainventory').controller('ArtistListController', function ($scope, $rootScope, ArtistService) {
	ArtistService.enumerate().then(function (response) {
		$scope.artists = response;
	});
});