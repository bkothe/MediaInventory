angular.module('mediainventory').controller('DashboardController', function ($scope) {
	$scope.customer = {
		name: 'Naomi',
		address: '1600 Amphitheatre'
	};
}).controller('myCustomer', function () {
	return {
		template: 'Name: {{customer.name}} Address: {{customer.address}}'
	};
});