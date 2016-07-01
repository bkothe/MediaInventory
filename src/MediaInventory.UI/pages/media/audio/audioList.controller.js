angular.module('mediainventory').controller('AudioListController', function ($scope, $rootScope, AudioService) {
	AudioService.enumerate().then(function () {
		$scope.audios = AudioService.audio;
	});
});