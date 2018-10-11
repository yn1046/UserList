import { Component } from '@angular/core';
import User from '../models/user';
import DataService from '../services/data.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'add-user',
    templateUrl: 'addUser.html',
    providers: [DataService]
})
export class AddUserComponent {
    userForm = this.fb.group({
        name: ['', [Validators.required, Validators.pattern(/^[a-zA-Zа-яА-Я ]*$/)]],
        age: ['', [Validators.required, Validators.pattern(/^\d*$/), Validators.min(10), Validators.max(150)]]
    });

    constructor(private fb: FormBuilder, private dataService: DataService) { }

    onSubmit() {
        
    }

    get name() { return this.userForm.get('name'); }

    get age() { return this.userForm.get('age'); }

}