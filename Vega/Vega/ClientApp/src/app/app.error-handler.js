"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppErrorHandler = /** @class */ (function () {
    function AppErrorHandler(toastyService) {
        this.toastyService = toastyService;
    }
    AppErrorHandler.prototype.handleError = function (error) {
        this.toastyService.error({
            title: 'Error',
            msg: 'An unexpected error happened.',
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
        });
    };
    return AppErrorHandler;
}());
exports.AppErrorHandler = AppErrorHandler;
//# sourceMappingURL=app.error-handler.js.map