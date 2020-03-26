import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/model/UserModel';
import { UserService } from 'src/service/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
users: UserModel[];
userIns: UserModel= new UserModel();
  constructor(private service:UserService) { }
  ngOnInit() {
    this.List();
  }
  List(){
      this.service.List().subscribe(users=> this.users=users);
  }
  insert(user: UserModel){
      this.service.insert(user).subscribe(()=> this.List());
  }
  update(user: UserModel){
    this.service.update(user,user.Id).subscribe(()=> this.List());
  }
  delete(user: UserModel){
    this.service.delete(user.Id).subscribe(()=> this.List());
  }
}
