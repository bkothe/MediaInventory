angular.module('mediainventory').controller('AudioController', function ($scope, AudioService, audioId) {
	AudioService.get(audioId).then(function (response) {
		$scope.audio = response;
	});
});