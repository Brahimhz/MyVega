import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map'; 
import { SaveVehicle } from '../models/vehicle';


@Injectable()
export class VehicleService {

  private readonly vehiclesApi = '/api/vehicles/';

  constructor(private http: Http) { }

  getModels() {
    return this.http.get('/api/models')
      .map(res => res.json());
  }

  getMakes() {
    return this.http.get('/api/makes')
      .map(res => res.json());
  }

  getFeatures() {
    return this.http.get('/api/features')
      .map(res => res.json());
  }

  create(vehicle) {
    return this.http.post(this.vehiclesApi, vehicle)
      .map(res => res.json());
  }

  getVehicle(id) {
    return this.http.get(this.vehiclesApi + id)
      .map(res => res.json());
  }

  update(vehicle: SaveVehicle) {
    return this.http.put(this.vehiclesApi + vehicle.id, vehicle)
      .map(res => res.json());
  }
  delete(id: number) {
    return this.http.delete(this.vehiclesApi + id)
      .map(res => res.json());
  }

  getVehicles(filter) {
    return this.http.get(this.vehiclesApi + '?' + this.toQueryString(filter))
      .map(res => res.json());
  }

  toQueryString(obj) {
    var parts = [];

    for (var property in obj) {
      var value = obj[property];  // obj.property <==>  obj[property]
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }
}
