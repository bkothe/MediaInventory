(function () {

	var controllerId = 'ArtistListController';

	angular.module('mediainventory').controller(controllerId, artistList);

	artistList.$inject = ['common', 'ArtistsFactory'];

	function artistList(common, ArtistsFactory) {
		var getLogFn = common.logger.getLogFn;
		var log = getLogFn(controllerId);
		var vm = this;

		vm.artists = [];

		activate();

		function activate() {
			var promises = [getArtists()];
			common.activateController(promises, controllerId)
				.then(function () { log('Activated artist list view') });
		}

		function getArtists() {
			return ArtistsFactory.enumerate().then(function (artists) {
				vm.artists = artists;
			});
		}
	}
})();