import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/model/UserModel';
import { UserService } from 'src/service/user.service';
import { WorkSpace } from 'src/model/WorkSpace';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: UserModel[];
  usersins: UserModel= new UserModel();
  userFilter: any = { Username: '' };
  
  
  constructor(private service : UserService,private router:Router) { }

  ngOnInit() {
    this.List();
  }
  List(){
      this.service.List().subscribe(users=> this.users=users);
  }
  insert(user: UserModel){
      this.service.insert(user).subscribe(()=> this.List());
  }
  update(user: UserModel,Id:String){
    this.service.update(user,user.Id).subscribe(()=> this.List());
  }
  delete(user: UserModel){
    this.service.delete(user.Id).subscribe(()=> this.List());
  }
  
}
