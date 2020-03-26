import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-icon-menu',
  templateUrl: './icon-menu.component.html',
  styleUrls: ['./icon-menu.component.css']
})
export class IconMenuComponent implements OnInit {

  IsVisible : boolean;
  path : string;

  toHide : string[] = ['home', 'register', 'login', 'admin']

  constructor(private route : Router) { }

  ngOnInit() {
    this.route.events.subscribe( evnt => {
      if(evnt['routerEvent']) if(evnt['routerEvent'].url)
      {
        this.path = this.route.url;
        this.ValidateVisible();
      }
    })
  }

  private ValidateVisible(){
    for (let i = 0; i < this.toHide.length; i++) {
      if (this.path.includes(this.toHide[i])) {
        this.IsVisible = false;
        return;
      }
    }
    this.IsVisible = true;
  }

}
