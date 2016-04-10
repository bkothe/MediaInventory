angular.module('mediainventory', ['ngRoute', 'ngMaterial', 'ngMessages'])
.constant('PAGE_URLS', {
	'DASHBOARD': '/',
	'ARTIST': {
		'LIST': '/artist/list',
		'NEW': '/artist/new',
		'EDIT': '/artist/edit',
		'VIEW': '/artist'
	},
	'VENUE': {
		'LIST': '/venue/list',
		'NEW': '/venue/new',
		'EDIT': '/venue/edit',
		'VIEW': '/venue'
	},
	'MEDIA': {
		'AUDIO': {
			'LIST': '/media/audio/list',
			'NEW': '/media/audio/new',
			'EDIT': '/media/audio/edit',
			'VIEW': '/media/audio/'
		}
	}
})
.constant('API_URLS', {
	'DASHBOARD': '',
	'ARTIST': '/api/artist/',
	'VENUE': '',
	'MEDIA': {
		'AUDIO': '/api/media/audio/'
	}
})
.config(function ($locationProvider, $routeProvider, PAGE_URLS) {
		$locationProvider.html5Mode(true);

		$routeProvider
			.when(PAGE_URLS.DASHBOARD, {
				controller: 'DashboardController',
				templateUrl: 'pages/dashboard.html'
			})
			.when(PAGE_URLS.ARTIST.LIST, {
				controller: 'ArtistListController',
				templateUrl: 'pages/artist/artistList.html'
			})
			.when(PAGE_URLS.ARTIST.NEW, {
				controller: 'ArtistEditController',
				templateUrl: 'pages/artist/artistEdit.html',
				resolve: {
					artistId: function() { return null }
				}
			})
			.when(PAGE_URLS.ARTIST.EDIT + '/:id', {
				controller: 'ArtistEditController',
				templateUrl: 'pages/artist/artistEdit.html',
				resolve: {
					artistId: function ($route) {
						return $route.current.params.id;
					}
				}
			})
			.when(PAGE_URLS.ARTIST.VIEW + '/:id', {
				controller: 'ArtistController',
				templateUrl: 'pages/artist/artist.html',
				resolve: {
					artistId: function($route) {
						return $route.current.params.id;
					}
				}
			})
			.when(PAGE_URLS.MEDIA.AUDIO.LIST, {
				controller: 'AudioListController',
				templateUrl: 'pages/media/audio/audioList.html'
			})
			.when(PAGE_URLS.MEDIA.AUDIO.NEW, {
				controller: 'AudioEditController',
				templateUrl: 'pages/media/audio/audioEdit.html',
				resolve: {
					audioId: function () { return null }
				}
			})
			.when(PAGE_URLS.MEDIA.AUDIO.EDIT + '/:id', {
				controller: 'AudioEditController',
				templateUrl: 'pages/media/audio/audioEdit.html',
				resolve: {
					audioId: function ($route) {
						return $route.current.params.id;
					}
				}
			})
			.when(PAGE_URLS.MEDIA.AUDIO.VIEW + '/:id', {
				controller: 'AudioController',
				templateUrl: 'pages/media/audio/audio.html',
				resolve: {
					audioId: function($route) {
						return $route.current.params.id;
					}
				}
			})
			.when(PAGE_URLS.VENUE.LIST, {
				controller: 'VenueListController',
				templateUrl: 'pages/venue/venueList.html'
			})
			.when(PAGE_URLS.VENUE.NEW, {
				controller: 'VenueEditController',
				templateUrl: 'pages/venue/venueEdit.html',
				resolve: {
					venueId: function () { return null }
				}
			})
			.when(PAGE_URLS.VENUE.EDIT + '/:id', {
				controller: 'VenueEditController',
				templateUrl: 'pages/venue/venueEdit.html',
				resolve: {
					venueId: function ($route) {
						return $route.current.params.id;
					}
				}
			})
			.when(PAGE_URLS.VENUE.VIEW + '/:id', {
				controller: 'VenueController',
				templateUrl: 'pages/venue/venue.html',
				resolve: {
					venueId: function ($route) {
						return $route.current.params.id;
					}
				}
			})
	});