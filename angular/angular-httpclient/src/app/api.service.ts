import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//const localUrl = '/assets/data/smartphone.json';
const localUrl = 'http://localhost:4200/assets/data/smartphone.json';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  getSmartphone() {
    return this.http.get(localUrl);
  }
}
