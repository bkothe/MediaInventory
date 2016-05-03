angular.module('mediainventory').factory('AudioService', function ($http, API_URLS) {
	var path = API_URLS.MEDIA.AUDIO;

	var AudioService = function (data) {
		angular.extend(this, data);

		this.save = function () {
			if (this.Id) {
				return $http.put(path + this.Id, this)
					.then(function (response) { return new AudioService(response.data); });
			}
			else {
				return $http.post(path, this)
					.then(function (response) { return new AudioService(response.data); });
			}
		};

		this.delete = function () {
			return $http.delete(path + this.Id, this);
		};
	};

	AudioService.get = function (id) {
		return $http.get(path + id).then(function (response) {
			return new AudioService(response.data);
		});
	};

	AudioService.enumerate = function () {
		return $http.get(path).then(function (response) {
			return _.map(response.data, function (d) {
				return new AudioService(d);
			});
		});
	};

	return AudioService;
});