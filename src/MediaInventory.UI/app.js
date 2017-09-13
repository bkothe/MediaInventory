(function() {
	'use strict';

	var app = angular.module('mediainventory', [
		'ngRoute',
		'ngMaterial',
		'ngMessages'
	]);

	// NOTE: this will not run until all app.configs are run.
	app.constant('API_URLS', {
			'DASHBOARD': '',
			'ARTIST': '/api/artist/',
			'CONCERT': '/api/concert/',
			'VENUE': '/api/venue/',
			'MEDIA': {
				'AUDIO': '/api/media/audio/'
			}
		})
		.run(['$route', function($route) {
			routeMediator.setRoutingHandlers();
		}]);
})();




//.config(function ($routeProvider, ROUTES) {

//		$routeProvider
//			.when(ROUTES.DASHBOARD, {
//				controller: 'DashboardController',
//				templateUrl: 'pages/dashboard.html'
//			})
//			.when(ROUTES.ARTIST.LIST, {
//				controller: 'ArtistListController',
//				controllerAs: 'vm',
//				templateUrl: 'pages/artist/artistList.html'
//			})
//			.when(ROUTES.ARTIST.NEW, {
//				controller: 'ArtistEditController',
//				controllerAs: 'vm',
//				templateUrl: 'pages/artist/artistEdit.html',
//				resolve: {
//					artistId: function() { return null }
//				}
//			})
//			.when(ROUTES.ARTIST.EDIT + '/:id', {
//				controller: 'ArtistEditController',
//				controllerAs: 'vm',
//				templateUrl: 'pages/artist/artistEdit.html',
//				resolve: {
//					artistId: function ($route) {
//						return $route.current.params.id;
//					}
//				}
//			})
//			.when(ROUTES.ARTIST.VIEW + '/:id', {
//				controller: 'ArtistController',
//				templateUrl: 'pages/artist/artist.html',
//				resolve: {
//					artistId: function($route) {
//						return $route.current.params.id;
//					}
//				}
//			})
//			.when(ROUTES.MEDIA.AUDIO.LIST, {
//				controller: 'AudioListController',
//				templateUrl: 'pages/media/audio/audioList.html'
//			})
//			.when(ROUTES.MEDIA.AUDIO.NEW, {
//				controller: 'AudioEditController',
//				controllerAs: 'vm',
//				templateUrl: 'pages/media/audio/audioEdit.html',
//				resolve: {
//					audioId: function () { return null }
//				}
//			})
//			.when(ROUTES.MEDIA.AUDIO.EDIT + '/:id', {
//				controller: 'AudioEditController',
//				controllerAs: 'vm',
//				templateUrl: 'pages/media/audio/audioEdit.html',
//				resolve: {
//					audioId: function ($route) {
//						return $route.current.params.id;
//					}
//				}
//			})
//			.when(ROUTES.MEDIA.AUDIO.VIEW + '/:id', {
//				controller: 'AudioController',
//				templateUrl: 'pages/media/audio/audio.html',
//				resolve: {
//					audioId: function($route) {
//						return $route.current.params.id;
//					}
//				}
//			})
//			.when(ROUTES.CONCERT.LIST, {
//				controller: 'ConcertListController',
//				templateUrl: 'pages/concert/concertList.html'
//			})
//			.when(ROUTES.CONCERT.NEW, {
//				controller: 'ConcertEditController',
//				controllerAs: 'vm',
//				templateUrl: 'pages/concert/concertEdit.html',
//				resolve: {
//					concertId: function () { return null }
//				}
//			})
//			.when(ROUTES.CONCERT.EDIT + '/:id', {
//				controller: 'ConcertEditController',
//				templateUrl: 'pages/concert/concertEdit.html',
//				resolve: {
//					concertId: function ($route) {
//						return $route.current.params.id;
//					}
//				}
//			})
//			.when(ROUTES.CONCERT.VIEW + '/:id', {
//				controller: 'ConcertController',
//				templateUrl: 'pages/concert/concert.html',
//				resolve: {
//					concertId: function ($route) {
//						return $route.current.params.id;
//					}
//				}
//			})
//			.when(ROUTES.VENUE.LIST, {
//				controller: 'VenueListController',
//				templateUrl: 'pages/venue/venueList.html'
//			})
//			.when(ROUTES.VENUE.NEW, {
//				controller: 'VenueEditController',
//				templateUrl: 'pages/venue/venueEdit.html',
//				resolve: {
//					venueId: function () { return null }
//				}
//			})
//			.when(ROUTES.VENUE.EDIT + '/:id', {
//				controller: 'VenueEditController',
//				templateUrl: 'pages/venue/venueEdit.html',
//				resolve: {
//					venueId: function ($route) {
//						return $route.current.params.id;
//					}
//				}
//			})
//			.when(ROUTES.VENUE.VIEW + '/:id', {
//				controller: 'VenueController',
//				templateUrl: 'pages/venue/venue.html',
//				resolve: {
//					venueId: function ($route) {
//						return $route.current.params.id;
//					}
//				}
//			});
//	});