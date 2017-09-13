(function () {
	'use strict';

	var app = angular.module('mediainventory');

	toastr.options.timeOut = 4000;
	toastr.options.positionClass = 'toast-bottom-right';

	var events = {
		controllerActivateSuccess: 'controller.activateSuccess',
		spinnerToggle: 'spinner.toggle'
	};

	var config = {
		appErrorPrefix: '[MI Error] ',
		docTitle: 'Media Inventory: ',
		events: events,
		remoteServiceName: '',
		version: '2.0.0'
	};

	app.value('config', config);

	app.config(['$logProvider', function ($logProvider) {
		if ($logProvider.debugEnabled) {
			$logProvider.debugEnabled(true);
		}
	}]);

	app.config(['$locationProvider', function ($locationProvider) {
		$locationProvider.html5Mode(true);
	}]);

	app.config(['commonConfigProvider', function (cfg) {
		cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
		cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
	}]);
})();