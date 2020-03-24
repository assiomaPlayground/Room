import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/service/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FoundUserWorkSpaceDTO } from 'src/model/DTO/FoundUserWorkSpaceDTO';
import { UserModel } from 'src/model/UserModel';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  foundUserWorkSpace: FoundUserWorkSpaceDTO
  user:UserModel
  constructor(private service:UserService,private router:ActivatedRoute) { }

  ngOnInit() {  
    this.router.paramMap.subscribe(params=>{
      this.service.read(params.get('id')).subscribe(user=>{
        this.user=user
          this.service.wUser(user.Id).subscribe(result=>{
            this.foundUserWorkSpace=result;
            
            
      })
    })
  })}
}