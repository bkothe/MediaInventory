angular.module('mediainventory').controller('AudioController', function ($scope, $rootScope, AudioService, audioId) {
	AudioService.get(audioId).then(function (response) {
		$scope.audio = response;
	});
});