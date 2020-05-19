import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    { path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'użytkownicy', component: UserListComponent, resolve: {users : UserListResolver}},
        { path: 'użytkownicy/:id', component: UserDetailComponent, resolve: {user: UserDetailResolver}},
        { path: 'profil', component: UserEditComponent, resolve: {user: UserEditResolver}, canDeactivate: [PreventUnsavedChanges]},
        { path: 'polubienia', component: LikesComponent},
        { path: 'wiadomości', component: MessagesComponent},
        
      ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'},
];


