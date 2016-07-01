(function () {

	angular.module('mediainventory').factory('ArtistsFactory', ArtistsFactory);

	ArtistsFactory.$inject = ['$http', '$q', 'API_URLS'];

	function ArtistsFactory($http, $q, API_URLS) {
		var path = API_URLS.ARTIST;
		var filterPath = path + '/filter/';
		var factory = {};

		factory.new = function () {
			return $q.when({ Id: null });
		};

		factory.get = function (id) {
			return $http.get(path + id).then(function (response) {
				return response.data;
			});
		};

		factory.save = function (artist) {
			if (artist.Id) {
				return $http.put(path + artist.Id, artist).then(function (status) {
					return status.data;
				});
			}
			else {
				return $http.post(path, artist).then(function (response) {
					artist.Id = response.data.Id;
					return response.data;
				});
			}
		};

		factory.delete = function (artist) {
			return $http.delete(path + artist.Id, artist).then(function (status) {
				return status.data;
			});
		};

		factory.enumerate = function () {
			return $http.get(path).then(function (response) {
				return response.data;
			});
		};

		factory.filter = function (value) {
			return $http.get(filterPath + value).then(function (response) {
				return _.map(response.data, function (d) {
					return d.Name;
				});
			});
		};

		return factory;
	}
})();