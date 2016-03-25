angular.module('mediainventory', ['ngRoute'])
    .config(function ($locationProvider, $routeProvider) {
    	$locationProvider.html5Mode(true);

		$routeProvider
			.when('/', {
				controller: 'DefaultCtrl',
				templateUrl: 'pages/default.html'
			})
			.when('/artist/list', {
				controller: 'ArtistListCtrl',
				templateUrl: 'pages/artist/ArtistList.html'
			})
			.when('/artist/new', {
				controller: 'ArtistNewCtrl',
				templateUrl: 'pages/artist/ArtistEdit.html'
			})
			.when('/artist/:id', {
				controller: 'ArtistCtrl',
				templateUrl: 'pages/artist/Artist.html',
				resolve: {
					artistId: function ($route) {
						return $route.current.params.id;
					}
				}
			});
	});