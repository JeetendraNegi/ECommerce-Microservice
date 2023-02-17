import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { TypographyComponent } from '../../typography/typography.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { UpgradeComponent } from '../../upgrade/upgrade.component';
import { UserDetailComponent } from 'app/components/users/user-detail/user.detail.component';
import { AddUserComponent } from 'app/components/users/user-detail/adduser/adduser.component';
import { EditUserComponent } from 'app/components/users/user-detail/edituser/edituser.component';
import { ViewUserComponent } from 'app/components/users/user-detail/viewuser/viewuser.component';
import { UserAddressComponent } from 'app/components/users/user-address/useraddress.component';
import { EditUserAddressComponent } from 'app/components/users/user-address/editaddress/editaddress.component';
import { NewAddressComponent } from 'app/components/users/user-address/newaddress/newaddress.component';
import { ViewAddressComponent } from 'app/components/users/user-address/viewaddress/viewaddress.component';
import { UserCardComponent } from 'app/components/users/user-cards/usercard.component';
import { AuthGuardService } from 'app/guards/authguard.Service';

export const AdminLayoutRoutes: Routes = [
    // {
    //   path: '',
    //   children: [ {
    //     path: 'dashboard',
    //     component: DashboardComponent
    // }]}, {
    // path: '',
    // children: [ {
    //   path: 'userprofile',
    //   component: UserProfileComponent
    // }]
    // }, {
    //   path: '',
    //   children: [ {
    //     path: 'icons',
    //     component: IconsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'notifications',
    //         component: NotificationsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'maps',
    //         component: MapsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'typography',
    //         component: TypographyComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'upgrade',
    //         component: UpgradeComponent
    //     }]
    // }
    { path: 'dashboard',      component: DashboardComponent, canActivate:[AuthGuardService] },
    { path: 'user-profile',   component: UserProfileComponent, canActivate:[AuthGuardService] },
    { path: 'table-list',     component: TableListComponent, canActivate:[AuthGuardService] },
    { path: 'typography',     component: TypographyComponent, canActivate:[AuthGuardService] },
    { path: 'maps',           component: MapsComponent, canActivate:[AuthGuardService] },
    { path: 'notifications',  component: NotificationsComponent, canActivate:[AuthGuardService] },
    { path: 'upgrade',        component: UpgradeComponent, canActivate:[AuthGuardService] },
    { path: 'users',          component: UserDetailComponent, canActivate:[AuthGuardService] },
    { path: 'users/adduser',  component: AddUserComponent, canActivate:[AuthGuardService] },
    { path: 'users/edituser', component: EditUserComponent, canActivate:[AuthGuardService] },
    { path: 'users/viewuser', component: ViewUserComponent, canActivate:[AuthGuardService] },
    { path: 'address', component: UserAddressComponent, canActivate:[AuthGuardService] },
    { path: 'address/editaddress', component: EditUserAddressComponent, canActivate:[AuthGuardService] },
    { path: 'address/newaddress', component: NewAddressComponent, canActivate:[AuthGuardService] },
    { path: 'address/viewaddress', component: ViewAddressComponent, canActivate:[AuthGuardService] },
    { path: 'cards', component: UserCardComponent, canActivate:[AuthGuardService] },
];
