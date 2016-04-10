angular.module('mediainventory').factory('VenueService', function ($http, API_URLS) {
	var path = API_URLS.VENUE;
	var filerPath = path + '/filter/';

	var VenueService = function (data) {
		angular.extend(this, data);

		this.save = function () {
			if (this.Id) {
				return $http.put(path + this.Id, this)
					.then(function (response) { return new VenueService(response.data); });
			}
			else {
				return $http.post(path, this)
					.then(function (response) { return new VenueService(response.data); });
			}
		};

		this.delete = function () {
			return $http.delete(path + this.Id, this);
		};
	};

	VenueService.get = function (id) {
		return $http.get(path + id).then(function (response) {
			return new VenueService(response.data);
		});
	};

	VenueService.enumerate = function () {
		return $http.get(path).then(function (response) {
			return _.map(response.data, function (d) {
				return new VenueService(d);
			});
		});
	};

	VenueService.filter = function (value) {
		return $http.get(filerPath + value).then(function (response) {
			return _.map(response.data, function (d) {
				return d.Name;
			});
		});
	};

	return VenueService;
});