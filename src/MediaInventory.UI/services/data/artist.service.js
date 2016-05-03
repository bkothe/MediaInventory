angular.module('mediainventory').factory('ArtistService', function ($http, API_URLS) {
	var path = API_URLS.ARTIST;
	var filerPath = path + '/filter/';

	var ArtistService = function (data) {
		angular.extend(this, data);

		this.save = function () {
			if (this.Id) {
				return $http.put(path + this.Id, this)
					.then(function(response) { return new ArtistService(response.data); });
			}
			else {
				return $http.post(path, this)
					.then(function (response) { return new ArtistService(response.data); });
			}
		};

		this.delete = function () {
			return $http.delete(path + this.Id, this);
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

	ArtistService.filter = function (value) {
		return $http.get(filerPath + value).then(function (response) {
			return _.map(response.data, function (d) {
				return d.Name;
			});
		});
	};

	return ArtistService;
});