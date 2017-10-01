import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { MaterialModule } from '../infrastructure/material/material.module';

import { ArtistListComponent } from './artist-list.component';
import { ArtistEditComponent } from './artist-edit.component';

const routes: Routes = [
    { path: '', component: ArtistListComponent },
    { path: 'new', component: ArtistEditComponent },
    { path: 'edit/:id', component: ArtistEditComponent }
];

@NgModule({
    imports: [CommonModule,
        MaterialModule,
        RouterModule.forChild(routes),
        MaterialModule
    ],
    exports: [],
    declarations: [
        ArtistListComponent,
        ArtistEditComponent
    ],
    providers: []
})
export class ArtistModule { }