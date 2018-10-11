import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import User from '../models/user';
 
@Injectable()
export default class DataService {
 
    constructor(private http: HttpClient) { }
 
    getUsers(page: number) {
        return this.http.get(`/api/users?id=${page}`);
    }

    getPageCount() {
        return this.http.get('/api/pageCount');
    }

    addUser(user: User) {
        return this.http.post('/api/users', user);
    }
}