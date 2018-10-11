import { Component, Output, EventEmitter } from '@angular/core';
import User from '../models/user';
import DataService from '../services/data.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'add-user',
    templateUrl: 'addUser.html',
    providers: [DataService]
})
export class AddUserComponent {
    @Output() submitted = new EventEmitter();
    
    userForm = this.fb.group({
        name: ['', [Validators.required, Validators.pattern(/^[a-zA-Zа-яА-Я ]*$/)]],
        age: ['', [Validators.required, Validators.pattern(/^\d*$/), Validators.min(10), Validators.max(150)]]
    });

    constructor(private fb: FormBuilder, private dataService: DataService) { }

    async onSubmit() {
        if (this.name.valid && this.age.valid) await this.dataService.addUser({
            userId: null,
            name: this.name.value,
            age: this.age.value
        }).toPromise().then((response: User) => console.log(response));
        this.submitted.next();
    }

    get name() { return this.userForm.get('name'); }

    get age() { return this.userForm.get('age'); }

}