import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DailyActivityComponent } from './daily-activity/daily-activity.component';
import { ShowDailyActivityComponent } from './daily-activity/show-daily-activity/show-daily-activity.component';
import { AddEditDailyActivityComponent } from './daily-activity/add-edit-daily-activity/add-edit-daily-activity.component';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DailyActivityApiService } from './daily-activity-api.service';

@NgModule({
  declarations: [
    AppComponent,
    DailyActivityComponent,
    ShowDailyActivityComponent,
    AddEditDailyActivityComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [DailyActivityApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
