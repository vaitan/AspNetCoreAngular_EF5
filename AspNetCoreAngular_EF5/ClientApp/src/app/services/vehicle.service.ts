import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from "rxjs/operators";
import { SaveVehicle, Vehicle } from '../models/vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

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
    return this.http.post<SaveVehicle>('/api/vehicles', vehicle)
      .pipe(map(res => res));
  }

  getVehicle(id: number) {
    return this.http.get<any>('/api/vehicles/' + id)
      .pipe(map(res => res));
  }

  update(id: number, vehicle: SaveVehicle) {
    return this.http.put<SaveVehicle>('/api/vehicles/' + id, vehicle)
      .pipe(map(res => res));
  }

  delete(id: number) {
    return this.http.delete<SaveVehicle>('/api/vehicles/' + id)
      .pipe(map(res => res));
  }

  getVehicles() {
    return this.http.get<Vehicle[]>('/api/vehicles')
      .pipe(map(res => res));
  }
}
