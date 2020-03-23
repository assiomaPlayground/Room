import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/service/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserModel } from 'src/model/UserModel';
import { WorkSpace } from 'src/model/WorkSpace';
import { FoundUserWorkSpaceDTO } from 'src/model/DTO/FoundUserWorkSpaceDTO';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
user: UserModel
workSpace: FoundUserWorkSpaceDTO
  constructor(private service:UserService,private router:ActivatedRoute) { }

  ngOnInit() {
    
    this.router.paramMap.subscribe(params=>{
      this.service.read(params.get('id')).subscribe(user=>{
        this.user=user;
        this.service.wUser(user.Id).subscribe(workspace=>{
          this.workSpace=workspace
        console.log(this.workSpace)})
      })})
    
  }

  
}


