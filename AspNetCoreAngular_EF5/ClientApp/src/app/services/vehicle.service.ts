import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from "rxjs/operators";

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

  create(vehicle) {
    return this.http.post<any>('/api/vehicles', vehicle)
      .pipe(map(res => res));
  }
}
