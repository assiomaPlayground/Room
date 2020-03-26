import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {

  IsDashboard : boolean = false;
  constructor(private route : Router) 
  { 
    
  }

  ngOnInit() {
    this.route.events.subscribe( evnt => {
      if(evnt['routerEvent']) if(evnt['routerEvent'].url)
      this.IsDashboard = this.route.url.includes('admin');
    })
  }

}
