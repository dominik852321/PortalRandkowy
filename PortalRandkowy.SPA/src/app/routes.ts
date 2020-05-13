import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent},
    { path: 'użytkownicy', component: UserListComponent, canActivate: [AuthGuard]},
    { path: 'polubienia', component: LikesComponent, canActivate: [AuthGuard]},
    { path: 'wiadomości', component: MessagesComponent, canActivate: [AuthGuard]},
    { path: '**', redirectTo: 'home', pathMatch: 'full'},
];

