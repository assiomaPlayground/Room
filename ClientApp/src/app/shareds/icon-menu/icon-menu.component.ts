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

  toHide : string[] = ['/home', '/register', '/login']

  constructor(private route : Router) { }

  ngOnInit() {
    this.route.events.subscribe( evnt => {
      if(evnt['routerEvent']) if(evnt['routerEvent'].url)
      {
        this.path = evnt['routerEvent'].url;
        this.ValidateVisible();
      }
    })
  }

  private ValidateVisible(){
    if(this.toHide.includes(this.path))
      this.IsVisible = false;
    else
      this.IsVisible = true;
  }

}
