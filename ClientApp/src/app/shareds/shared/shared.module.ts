import { NgModule } from '@angular/core';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { MomentModule } from 'ngx-moment';
import { OrderModule } from 'ngx-order-pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FilterPipeModule } from 'ngx-filter-pipe';
import 'moment/locale/it';
import { CommonModule } from '@angular/common';
import { IconMenuComponent } from '../icon-menu/icon-menu.component';

@NgModule({
  declarations: [IconMenuComponent],
  imports: [
    CommonModule,
    ButtonsModule.forRoot(),
    TimepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MomentModule,
    OrderModule,
    FilterPipeModule,
    ButtonsModule,
    TimepickerModule,
    BsDropdownModule,
    BsDatepickerModule
  ]
})
export class SharedModule { }
