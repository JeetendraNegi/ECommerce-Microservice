import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { TypographyComponent } from '../../typography/typography.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { UpgradeComponent } from '../../upgrade/upgrade.component';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSelectModule} from '@angular/material/select';
import { UserDetailComponent } from 'app/components/users/user-detail/user.detail.component';
import { AddUserComponent } from 'app/components/users/user-detail/adduser/adduser.component';
import { EditUserComponent } from 'app/components/users/user-detail/edituser/edituser.component';
import { ViewUserComponent } from 'app/components/users/user-detail/viewuser/viewuser.component';
import { UserAddressComponent } from 'app/components/users/user-address/useraddress.component';
import { EditUserAddressComponent } from 'app/components/users/user-address/editaddress/editaddress.component';
import { NewAddressComponent } from 'app/components/users/user-address/newaddress/newaddress.component';
import { ViewAddressComponent } from 'app/components/users/user-address/viewaddress/viewaddress.component';
import { UserCardComponent } from 'app/components/users/user-cards/usercard.component';
import { MyTestPipePipe } from 'app/pipe/my-test-pipe.pipe';
import { CountArrayItemPipe } from 'app/pipe/count-array-item.pipe';
import { TestDirective } from 'app/directives/test.directive';
import { TableHeaderDirective } from 'app/directives/tablHeader.directive';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    TableListComponent,
    TypographyComponent,
    MapsComponent,
    NotificationsComponent,
    UpgradeComponent,
    UserDetailComponent, AddUserComponent, EditUserComponent,
    ViewUserComponent, NewAddressComponent, UserAddressComponent, EditUserAddressComponent,
    ViewAddressComponent, UserCardComponent, MyTestPipePipe, CountArrayItemPipe,
    TestDirective, TableHeaderDirective
  ]
})

export class AdminLayoutModule {}
