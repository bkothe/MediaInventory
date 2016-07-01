angular.module('mediainventory').controller('ConcertListController', function ($scope, $rootScope, ConcertService) {
	ConcertService.enumerate().then(function (response) {
		$scope.concerts = response;
	});
});