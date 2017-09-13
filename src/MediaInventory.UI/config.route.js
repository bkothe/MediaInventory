(function () {
	'use strict';

	var app = angular.module('mediainventory');

	app.constant('ROUTES', {
		'DASHBOARD': '/',
		'ARTIST': {
			'LIST': '/artist/list',
			'NEW': '/artist/new',
			'EDIT': '/artist/edit',
			'VIEW': '/artist'
		},
		'CONCERT': {
			'LIST': '/concert/list',
			'NEW': '/concert/new',
			'EDIT': '/concert/edit',
			'VIEW': '/concert'
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
	});

	app.constant('routes', getRoutes());

	app.config(['$routeProvider', 'routes', routeConfigurator]);

	function routeConfigurator($routeProvider, routes) {
		routes.forEach(function (r) {
			$routeProvider.when(r.url, r.config);
		});
		$routeProvider.otherwise({ redirectTo: '/' });
	}

	function getRoutes() {
		var blah = {
			'DASHBOARD': '/',
			'ARTIST': {
				'LIST': '/artist/list',
				'NEW': '/artist/new',
				'EDIT': '/artist/edit',
				'VIEW': '/artist'
			},
			'CONCERT': {
				'LIST': '/concert/list',
				'NEW': '/concert/new',
				'EDIT': '/concert/edit',
				'VIEW': '/concert'
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
		};

		return [
			{
				url: blah.ARTIST.LIST,
				config: {
					templateUrl: '',
					title: '',
					settings: {

					}
				}
			}
		];
	}
})();