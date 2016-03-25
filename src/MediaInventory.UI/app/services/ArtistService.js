angular.module('mediainventory').factory('ArtistService', function ($http) {
	var path = '/api/artist/';

	var ArtistService = function (data) {
		angular.extend(this, data);

		this.save = function () {
			if (this.id) {
				return $http.put(path + this.id, this)
					.then(function(response) { return new ArtistService(response.data); });
			}
			else {
				return $http.post(path, this)
					.then(function (response) { return new ArtistService(response.data); });
			}
		};

		this.delete = function () {
			return $http.delete(path + this.id, this);
		};
	};

	ArtistService.get = function (id) {
		return $http.get(path + id).then(function (response) {
			return new ArtistService(response.data);
		});
	};

	ArtistService.enumerate = function () {
		return $http.get(path).then(function (response) {
			return _.map(response.data, function (d) {
				return new ArtistService(d);
			});
		});
	};

	return ArtistService;
});