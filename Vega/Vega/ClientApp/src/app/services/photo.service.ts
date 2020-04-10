import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class PhotoService {

  constructor(private http: Http) { }


  upload(vehicleId, photo) {

    var formeData = new FormData();
    formeData.append('file', photo);
    return this.http.post(`/api/vehicles/${vehicleId}/photos`, formeData)
      .map(res => res.json());
  }

  getPhotos(vehicleId) {
    return this.http.get(`/api/vehicles/${vehicleId}/photos`)
      .map(res => res.json());
  }

}
