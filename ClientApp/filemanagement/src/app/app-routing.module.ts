import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FileUploadComponent } from './core/file-upload/file-upload.component';
import { LoginComponent } from './auth/login/login.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { FileDashboardComponent } from './core/file-dashboard/file-dashboard.component';
import { AuthgaurdService } from './authgaurd/authgaurd.service';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'logout',
    component: LogoutComponent
  },
  {
    path: 'upload-files',
    component: FileUploadComponent,
    canActivate: [AuthgaurdService]
  },
  {
    path: 'file-dashboard',
    component: FileDashboardComponent,
    canActivate: [AuthgaurdService]
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: '',
    redirectTo: 'upload-files', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
