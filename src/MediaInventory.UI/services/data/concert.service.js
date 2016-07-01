angular.module('mediainventory').factory('ConcertService', function ($http, API_URLS) {
	var path = API_URLS.CONCERT;

	var ConcertService = function (data) {
		angular.extend(this, data);

		this.save = function () {
			if (this.Id) {
				return $http.put(path + this.Id, this)
					.then(function (response) { return new ConcertService(response.data); });
			}
			else {
				return $http.post(path, this)
					.then(function (response) { return new ConcertService(response.data); });
			}
		};

		this.delete = function () {
			return $http.delete(path + this.Id, this);
		};
	};

	ConcertService.get = function (id) {
		return $http.get(path + id).then(function (response) {
			return new ConcertService(response.data);
		});
	};

	ConcertService.enumerate = function () {
		return $http.get(path).then(function (response) {
			return _.map(response.data, function (d) {
				return new ConcertService(d);
			});
		});
	};

	return ConcertService;
});