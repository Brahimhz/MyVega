import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class MakeService {

  constructor(private http: Http) { }

  getMakes() {
    return this.http.get('/API/Makes')
      .map(res => res.json());
  }

}
