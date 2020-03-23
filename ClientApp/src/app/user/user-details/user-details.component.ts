import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/service/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserModel } from 'src/model/UserModel';
import { WorkSpace } from 'src/model/WorkSpace';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
user: UserModel
workSpace: WorkSpace
  constructor(private service:UserService,private router:ActivatedRoute) { }

  ngOnInit() {
    this.router.paramMap.subscribe(params=>{
      this.service.read(params.get('id')).subscribe(user=>this.user=user)})
    
  }
  WUser(user:UserModel){
    this.service.wUser(user.Id).subscribe(workSpace=>this.workSpace=workSpace);
  }
  
}


