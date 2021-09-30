import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VehicleService } from '../services/vehicle.service';
import { forkJoin, Observable } from 'rxjs';
import { SaveVehicle, Vehicle } from '../models/vehicle';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    modelId: 0,
    makeId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: null,
      phone: null,
      email: null
    },
  };

  constructor(
    private activateRoute: ActivatedRoute,
    //private route: Route,
    private vehicleService: VehicleService) {

    activateRoute.params.subscribe(params => {
      this.vehicle.id = +params['id'];
    });
  }

  ngOnInit() {
    var source = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()
    ];

    if (this.vehicle.id > 0) {
      source.push(this.vehicleService.getVehicle(this.vehicle.id));
    }

    forkJoin(source).subscribe(result => {
      this.makes = result[0];
      this.features = result[1];

      if (this.vehicle.id > 0) {
        this.setVehicle(result[2]);
        this.populateModels();
      }
    });
  }

  private setVehicle(v) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.contact = v.contact;
    this.vehicle.features = v.features.map(({ id }) => id);
    this.vehicle.isRegistered = v.isRegistered;
  }

  onMakeChange() {
    this.populateModels();

    delete this.vehicle.modelId;
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeaturesToggled(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    }
    else {
      let index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    if (this.vehicle.id > 0) {
      this.vehicleService.update(this.vehicle.id, this.vehicle)
        .subscribe(x => window.location.reload());
    }
    else {
      this.vehicle.id = 0;
      this.vehicleService.create(this.vehicle)
        .subscribe(x => console.log(x));
    }
  }

  delete() {
    this.vehicleService.delete(this.vehicle.id)
      .subscribe(x => console.log(x));
  }
}
