import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';

export const appRoutes:Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'artists', loadChildren: 'app/artist/artist.module#ArtistModule' },
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];