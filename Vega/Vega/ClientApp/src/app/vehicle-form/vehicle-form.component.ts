import { Component, OnInit } from '@angular/core';
import { VehiculeService } from '../services/vehicule.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  vehicle: any = {};
  models: any[];
  features: any[];

  constructor(
    private vehiculeService: VehiculeService
    ) { }

  ngOnInit() {
    this.vehiculeService.getMakes().subscribe(makes =>
      this.makes = makes
    );

    this.vehiculeService.getFeatures().subscribe(features => this.features = features);
  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
    this.models = selectedMake ? selectedMake.models : [];
  }
}

