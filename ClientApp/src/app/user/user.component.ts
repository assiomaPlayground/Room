import { Component, OnInit } from '@angular/core';
import { User } from 'src/model/user';
import { UserService } from 'src/service/user.service';
import { Room } from 'src/model/room';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: User[];
  usersins: User= new User();
  userFilter: any = { Username: '' };
  
  
  constructor(private service : UserService) { }

  ngOnInit() {
    this.List();
  }
  List(){
      this.service.List().subscribe(users=> this.users=users);
  }
  insert(user: User){
      this.service.insert(user).subscribe(()=> this.List());
  }
  update(user: User){
    this.service.update(user).subscribe(()=> this.List());
  }
  delete(user: User){
    this.service.delete(user.Id).subscribe(()=> this.List());
  }
  
}
