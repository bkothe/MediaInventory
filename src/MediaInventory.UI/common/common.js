(function () {
	'use strict';

	var commonModule = angular.module('common', []);

	commonModule.provider('commonConfig', function () {
		this.config = {
			// These are the properties we need to set
			//controllerActivateSuccessEvent: '',
			//spinnerToggleEvent: ''
		};

		this.$get = function () {
			return {
				config: this.config
			};
		};
	});

	commonModule.factory('common', ['$q', '$rootScope', '$timeout', 'commonConfig', 'logger', common]);

	function common($q, $rootScope, $timeout, commonConfig, logger) {
		var service = {
			$broadcast: $broadcast,
			$q: $q,
			$timeout: $timeout,
			activateController: activateController,
			isNumber: isNumber,
			logger: logger,
			textContains: textContains
		};

		return service;

		function activateController(promises, controllerId) {
			return $q.all(promises).then(function (eventArgs) {
				var data = { controllerId: controllerId };
				$broadcast(commonConfig.config.controllerActivateSuccessEvent, data);
			});
		}

		function $broadcast() {
			return $rootScope.$broadcast.apply($rootScope, arguments);
		}

		function isNumber(val) {
			return /^[-]?\d+$/.test(val);
		}

		function textContains(text, searchText) {
			return text && -1 !== text.toLowerCase().indexOf(searchText.toLowerCase());
		}
	}
})();