angular.module("mediainventory").factory('HttpStatusInterceptor', function ($q) {
	return {
		'responseError': function (rejection) {
			if (rejection.status === 500 || rejection.status === 403 || rejection.status === 400) {
				alert(rejection.statusText);
			}
			return $q.reject(rejection);
		}
	};
});

angular.module('mediainventory').config(['$httpProvider', function ($httpProvider) {
	$httpProvider.interceptors.push('HttpStatusInterceptor');
}]);