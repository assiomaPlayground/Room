import { Component, OnInit } from '@angular/core';
import { Building } from 'src/model/Building';
import { Builder } from 'protractor';
import { BuildingService } from 'src/service/building.service';

@Component({
  selector: 'app-building',
  templateUrl: './building.component.html',
  styleUrls: ['./building.component.css']
})
export class BuildingComponent implements OnInit {
  building: Building[];
  constructor(private service: BuildingService) { }

  ngOnInit() {
    this.list()
  }
  list(){
    this.service.List().subscribe(building=> this.building=building);
  }
  insert(building: Building){
    this.service.insert(building).subscribe(()=>this.list());
  }
  update(building: Building,Id : String){
    this.service.update(building,building.Id).subscribe(()=> this.list());
  }
  delete(building: Building){
    this.service.delete(building.Id).subscribe(()=> this.list());
  }
}
