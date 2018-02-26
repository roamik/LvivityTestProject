"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var authenticationService_1 = require("../_services/authenticationService");
var router_1 = require("@angular/router");
var LoginPageComponent = /** @class */ (function () {
    function LoginPageComponent(router, authenticationService) {
        this.router = router;
        this.authenticationService = authenticationService;
        this.error = {};
        this.model = {};
    }
    LoginPageComponent.prototype.ngOnInit = function () {
        this.authenticationService.logout();
    };
    LoginPageComponent.prototype.login = function () {
        var _this = this;
        this.authenticationService.login(this.model)
            .subscribe(function (result) {
            _this.router.navigate(['/home']);
        }, function (response) {
            _this.error = response.error;
        });
    };
    LoginPageComponent = __decorate([
        core_1.Component({
            selector: 'app-login-page',
            templateUrl: './login-page.component.html',
            styleUrls: ['./login-page.component.css']
        }),
        __metadata("design:paramtypes", [router_1.Router,
            authenticationService_1.AuthenticationService])
    ], LoginPageComponent);
    return LoginPageComponent;
}());
exports.LoginPageComponent = LoginPageComponent;
//# sourceMappingURL=login-page.component.js.map