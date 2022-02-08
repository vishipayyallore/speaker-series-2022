import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorAlertComponent } from './alerts/error-alert/error-alert.component';
import { LoaderComponent } from './loader/loader.component';



@NgModule({
  declarations: [
    ErrorAlertComponent,
    LoaderComponent
  ],
  exports: [
    ErrorAlertComponent,
    LoaderComponent,
  ],
  imports: [
    CommonModule
  ]
})
export class CoreComponentsModule { }
