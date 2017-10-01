"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var router_1 = require("@angular/router");
var material_module_1 = require("../infrastructure/material/material.module");
var artist_list_component_1 = require("./artist-list.component");
var artist_edit_component_1 = require("./artist-edit.component");
var routes = [
    { path: '', component: artist_list_component_1.ArtistListComponent },
    { path: 'new', component: artist_edit_component_1.ArtistEditComponent },
    { path: 'edit/:id', component: artist_edit_component_1.ArtistEditComponent }
];
var ArtistModule = /** @class */ (function () {
    function ArtistModule() {
    }
    ArtistModule = __decorate([
        core_1.NgModule({
            imports: [common_1.CommonModule,
                material_module_1.MaterialModule,
                router_1.RouterModule.forChild(routes),
                material_module_1.MaterialModule
            ],
            exports: [],
            declarations: [
                artist_list_component_1.ArtistListComponent,
                artist_edit_component_1.ArtistEditComponent
            ],
            providers: []
        })
    ], ArtistModule);
    return ArtistModule;
}());
exports.ArtistModule = ArtistModule;
//# sourceMappingURL=artist.module.js.map