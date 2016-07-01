(function () {
	angular.module('mediainventory').controller('ArtistController', Artist);

	Artist.$inject = ['ArtistService', 'artistId'];

	function Artist(ArtistService, artistId) {
		var vm = this;
		ArtistService.get(artistId).then(function() {
			vm.artist = ArtistService.artist;
		});
	}
})();