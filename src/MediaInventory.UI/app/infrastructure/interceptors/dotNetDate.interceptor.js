angular.module("mediainventory").factory('DotNetDateInterceptor', function ($q) {
	return {
		response: function (promise) {
			var getType = function (obj) { return Object.prototype.toString.apply(obj); };
			var objectType = getType({});
			var arrayType = getType([]);

			var parseValue = function (value) {
				if (typeof value === 'string' && !value.search(/^\/Date\(\d+\)\/$/)) {
					return new Date(parseInt(value.substr(6), 10));
				}
				else { return value; }
			};

			var parseObject = function (obj) {
				if (getType(obj) !== objectType && getType(obj) !== arrayType) { return; }
				for (var key in obj) {
					if (obj.hasOwnProperty(key)) {
						var value = obj[key];
						if (getType(value) === objectType || getType(value) === arrayType) { parseObject(value); }
						else if (typeof value !== 'function') { obj[key] = parseValue(value); }
					}
				}
			};

			return $q.when(promise).then(function (response) {
				parseObject(response.data);
				return response;
			});
		}
	};
});

angular.module('mediainventory').config(['$httpProvider', function ($httpProvider) {
	$httpProvider.interceptors.push('DotNetDateInterceptor');
}]);