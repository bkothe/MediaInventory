angular.module('mediainventory').controller('AudioEditController', function ($location, ROUTES, AudioService, ArtistsFactory, audioId)
{
	var vm = this;

	vm.audio = AudioService.audio;
	if (audioId) {
		AudioService.get(audioId).then(function () {
			vm.audio = AudioService.audio;
		});
	}

	vm.availableMediaFormats = getAvailableMediaFormats();
	vm.querySearch = querySearch;
	vm.searchTextChange = searchTextChange;
	vm.selectedItemChange = selectedItemChange;
	vm.save = save;
	vm.cancel = cancel;

	function querySearch(query) {
		return query ? ArtistsFactory.filter(query).then(function (artists) {
			return artists;
		}) : [];
	};

	function getAvailableMediaFormats() {
		return ('Cassette,CompactDisc,Vinyl,SHN,FLAC,EightTrack').split(',')
			.map(function (mediaFormat) { return { Name: mediaFormat }; });;
	};

	function save() {
		AudioService.save(vm.audio).then(function () {
			$location.path(ROUTES.MEDIA.AUDIO.LIST);
		});
	};

	function cancel() {
		$location.path(ROUTES.MEDIA.AUDIO.LIST);
	};

	function searchTextChange(text) {
		vm.audio.ArtistName = text;
	};

	function selectedItemChange(item) {
		searchTextChange(item);
	}
});