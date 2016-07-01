(function() {
	angular.module('mediainventory').controller('ConcertEditController', ConcertEdit);

	ConcertEdit.$inject = ['$location', 'ROUTES', 'ConcertService', 'ArtistService', 'concertId'];

	function ConcertEdit($location, ROUTES, ConcertService, ArtistService, concertId) {
		var vm = this;
		var masterEntity = new ConcertService();
		if (concertId) {
			masterEntity = ConcertService.get(concertId).then(function(response) {
				vm.concert = response;
			});
		}

		vm.concert = masterEntity;
		vm.save = save;
		vm.querySearch = querySearch;
		vm.searchTextChange = searchTextChange;
		vm.selectedItemChange = selectedItemChange;

		function save() {
			vm.concert.save().then(function() {
				$location.path(ROUTES.CONCERT.LIST);
			});
		};

		function querySearch(query) {
			return query ? ArtistService.filter(query).then(function(response) {
				return response;
			}) : [];
		};

		function searchTextChange(text) {
			vm.concert.ArtistName = text;
		};

		function selectedItemChange(item) {
			searchTextChange(item);
		}
	}
})();