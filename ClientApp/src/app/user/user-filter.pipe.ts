import{ PipeTransform,Pipe} from '@angular/core';
import { User } from 'src/model/user';

@Pipe({
    name : 'userFilter'
})
export class UserFilterPipe implements PipeTransform{
    transform(users: User[], search: String): User[]{
        if(!users|| !search){
            return users;
        }
        return users.filter(user=> user.Username.toLocaleLowerCase().indexOf(search.toLocaleLowerCase())!==1)
    }
}