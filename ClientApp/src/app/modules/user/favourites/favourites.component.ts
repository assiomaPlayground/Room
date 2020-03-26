import { Component, OnInit } from '@angular/core';
import { Favourites } from 'src/model/Favourites';
import { FavouritesService } from 'src/service/favourites.service';

@Component({
  selector: 'app-favourites',
  templateUrl: './favourites.component.html',
  styleUrls: ['./favourites.component.css']
})
export class FavouritesComponent implements OnInit {
  favourites: Favourites[];
  constructor(private service: FavouritesService) { }

  ngOnInit() {
    this.list()
  }
  list(){
    this.service.List().subscribe(favourites=> this.favourites= favourites);
  }
  insert(favourites: Favourites){
    this.service.insert(favourites).subscribe(()=> this.list());
  }
  update(favourites: Favourites,Id:String){
    this.service.update(favourites,favourites.Id).subscribe(()=> this.list());
  }
 
}
