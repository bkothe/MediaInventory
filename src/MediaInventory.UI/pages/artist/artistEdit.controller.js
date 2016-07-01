(function () {
	angular.module('mediainventory').controller('ArtistEditController', ArtistEdit);

	ArtistEdit.$inject = ['$location', '$log', 'ROUTES', 'ArtistsFactory', 'artistId'];

	function ArtistEdit($location, $log, ROUTES, ArtistsFactory, artistId) {
		var vm = this;

		if (artistId) {
			ArtistsFactory.get(artistId).then(function (artist) {
				vm.artist = artist;
			});
		} else {
			ArtistsFactory.new().then(function (artist) {
				vm.artist = artist;
			});
		}

		vm.saveAddNew = saveAndAddNew;
		vm.save = save;

		function saveAndAddNew() {
			save(true);
		};

		function save(addAnother) {
			$log.info(vm.artist);
			ArtistsFactory.save(vm.artist).then(function () {
				if (addAnother)
					$location.path(ROUTES.ARTIST.NEW);
				else
					$location.path(ROUTES.ARTIST.LIST);
			});
		};
	}
})();