import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/service/authenticationservice.service';

@Component({
  selector: 'app-layout-selector',
  templateUrl: './layout-selector.component.html',
  styleUrls: ['./layout-selector.component.css']
})
export class LayoutSelectorComponent implements OnInit {

  userType : number = 0;

  constructor(private autService : AuthenticationService) { }

  ngOnInit() {
    this.autService.currentUser.subscribe(user => {
      if(user != null)
        this.userType = this.autService.currentUserValue.Usertype;
    })
  }
}