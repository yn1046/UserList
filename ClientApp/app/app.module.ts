import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AddUserComponent } from './addUser/addUser.component';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, ReactiveFormsModule],
    declarations: [AppComponent, AddUserComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }