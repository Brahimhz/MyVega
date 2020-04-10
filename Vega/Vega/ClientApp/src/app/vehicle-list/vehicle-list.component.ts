import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair } from '../models/vehicle';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  queryResult: any = {};
  makes: KeyValuePair[];
  models: KeyValuePair[];
  query: any = {
    pageSize: 3
  };
  columns = [
    { title: 'Id', key: 'id', isAsc: true},
    { title: 'Contact Name', key: 'contactName', isAsc: true },
    { title: 'Make', key: 'make', isAsc: true },
    { title: 'Model', key: 'model', isAsc: true }
  ];


  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {

    this.vehicleService.getMakes().subscribe(makes => this.makes = makes);

    this.populateVehicles(); 
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query )
      .subscribe(result => this.queryResult = result );
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateVehicles();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName)
      this.query.isAsc = !this.query.isAsc;
    else {
      this.query.sortBy = columnName;
      this.query.isAsc = true;
    }

    this.populateVehicles();

  }

  onPageChange(page) {
    this.query.page = page;
    this.populateVehicles();
  }

}
