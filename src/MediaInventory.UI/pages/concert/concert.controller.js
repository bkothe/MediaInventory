(function () {
	angular.module('mediainventory').controller('ConcertController', Concert);

	Concert.$inject = ['ConcertService', 'concertId'];

	function Concert(ConcertService, concertId) {
		var vm = this;
		ConcertService.get(concertId).then(function (response) {
			vm.concert = response;
		});
	}
})();