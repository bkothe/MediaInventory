(function () {
	angular.module('mediainventory').controller('ArtistListController', ArtistList);

	ArtistList.$inject = ['ArtistsFactory'];

	function ArtistList(ArtistsFactory) {
		var vm = this;
		ArtistsFactory.enumerate().then(function (artists) {
			vm.artists = artists;
		});
	}
})();