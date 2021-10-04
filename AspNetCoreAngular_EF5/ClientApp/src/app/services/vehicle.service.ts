import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from "rxjs/operators";
import { SaveVehicle, Vehicle } from '../models/vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly vehicleEndpoint = '/api/vehicles';

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<any[]>('/api/makes')
      .pipe(map(res => {
        return res;
      }));
  }

  getFeatures() {
    return this.http.get<any[]>('/api/features')
      .pipe(map(res => res));
  }

  create(vehicle: SaveVehicle) {
    return this.http.post<SaveVehicle>(this.vehicleEndpoint, vehicle)
      .pipe(map(res => res));
  }

  getVehicle(id: number) {
    return this.http.get<any>(this.vehicleEndpoint + '/' + id)
      .pipe(map(res => res));
  }

  update(id: number, vehicle: SaveVehicle) {
    return this.http.put<SaveVehicle>(this.vehicleEndpoint + '/' + id, vehicle)
      .pipe(map(res => res));
  }

  delete(id: number) {
    return this.http.delete<SaveVehicle>(this.vehicleEndpoint + '/' + id)
      .pipe(map(res => res));
  }

  getVehicles(filter: any) {
    return this.http.get<Vehicle[]>(this.vehicleEndpoint + '?' + this.toQueryString(filter))
      .pipe(map(res => res));
  }

  private toQueryString(obj) {
    var parts = [];

    for (var property in obj) {
      var value = obj[property];

      parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }
}
