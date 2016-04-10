angular.module('mediainventory').controller('AudioEditController', function ($scope, $log, $location, PAGE_URLS, AudioService, ArtistService, audioId) {
	var masterEntity = new AudioService();
	if (audioId) {
		masterEntity = AudioService.get(audioId).then(function (response) {
			$scope.audio = response;
		});
	}
	$scope.audio = masterEntity;

	$scope.ctrl = {};
	$scope.ctrl.availableMediaFormats = getAvailableMediaFormats();
	$scope.ctrl.querySearch = querySearch;
	$scope.ctrl.searchTextChange = searchTextChange;
	$scope.ctrl.selectedItemChange = selectedItemChange;
	$scope.ctrl.save = save;
	$scope.ctrl.cancel = cancel;

	function querySearch(query) {
		return query ? ArtistService.filter(query).then(function (response) {
			$log.info('Response:', response);
			return response;
		}) : [];
	};

	function getAvailableMediaFormats() {
		return ('Cassette,CompactDisc,Vinyl,SHN,FLAC,EightTrack').split(',')
			.map(function (mediaFormat) { return { Name: mediaFormat }; });;
	};

	function save() {
		$scope.audio.save().then(function () {
			$location.path(PAGE_URLS.MEDIA.AUDIO.LIST);
		});
	};

	function cancel() {
		$location.path(PAGE_URLS.MEDIA.AUDIO.LIST);
	};

	function searchTextChange(text) {
		$log.info('Change', text);
		$scope.audio.ArtistName = text;
	};

	function selectedItemChange(item) {
		$log.info('Selected', item);
		searchTextChange(item);
	}
});