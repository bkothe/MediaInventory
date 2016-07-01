angular.module('mediainventory').factory('AudioService', function AudioService($http, API_URLS) {
	var path = API_URLS.MEDIA.AUDIO;

	AudioService.audio = {};

	AudioService.save = function (audio) {
		if (audio.Id) {
			return $http.put(path + audio.Id, audio);
		}
		else {
			return $http.post(path, audio)
				.then(function (response) { AudioService.audio = response.data; });
		}
	}

	AudioService.delete = function (audio) {
		return $http.delete(path + audio.Id, audio);
	}

	AudioService.get = function (id) {
		return $http.get(path + id).then(function (response) {
			AudioService.audio = response.data;
		});
	};

	AudioService.enumerate = function () {
		return $http.get(path).then(function (response) {
			AudioService.audio = response.data;
		});
	};

	return AudioService;
});