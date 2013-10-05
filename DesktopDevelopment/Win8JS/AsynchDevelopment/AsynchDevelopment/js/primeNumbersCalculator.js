/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />

var PrimeNumbersCalculator = WinJS.Class.define(function() {
    this._workerCount = 0;
    this._workerCalculatePrimeNumberTo = Worker("/js/workers/calculatePrimeNumberTo.js");
    this._workerCalculateFirstNumbers = Worker("/js/workers/calculateFirstNumbers.js");
    this._workerCalculateFromRange = Worker("/js/workers/calculateFromRange.js");
}, {
    calculatePrimeNumberTo: function(number) {
        var self = this;
        return new WinJS.Promise(function(complete, error) {
            if (self._workerCount < 3) {
                self._workerCount++;
                self._workerCalculatePrimeNumberTo.onmessage = function (event) {
                    self._workerCount--;
                    var primesList = event.data;
                    complete(primesList);
                };
                self._workerCalculatePrimeNumberTo.postMessage({
                    toNumber: number
                });
            } else {
                error("only 3 workers is allowed at some time");
            }
        });
    },
    calculateFirstNumbers: function(toNumber, stopNumber) {
        var self = this;
        return new WinJS.Promise(function(complete, error) {
            if (self._workerCount < 3) {
                self._workerCount++;
                self._workerCalculateFirstNumbers.onmessage = function (event) {
                    self._workerCount--;
                    var primesList = event.data;
                    complete(primesList);
                };
                self._workerCalculateFirstNumbers.postMessage({
                    toNumber: toNumber,
                    stopNumber: stopNumber
                });
            } else {
                error("only 3 workers is allowed at some time");
            }
        });
    },
    calculateFromRange: function(startNumber, endNumber) {
        var self = this;
        return new WinJS.Promise(function(complete, error) {
            if (self._workerCount < 3) {
                self._workerCount++;
                self._workerCalculateFromRange.onmessage = function (event) {
                    self._workerCount--;
                    var primesList = event.data;
                    complete(primesList);
                };
                self._workerCalculateFromRange.postMessage({
                    startNumber: startNumber,
                    endNumber: endNumber
                });
            } else {
                error("only 3 workers is allowed at some time");
            }
        });
    }
});