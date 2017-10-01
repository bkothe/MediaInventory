"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var dashboard_component_1 = require("./dashboard.component");
exports.appRoutes = [
    { path: 'dashboard', component: dashboard_component_1.DashboardComponent },
    { path: 'artists', loadChildren: 'app/artist/artist.module#ArtistModule' },
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];
//# sourceMappingURL=routes.js.map