export class AuthDTO {

    Username: string;

    Password: string;

    rememberMe: boolean;

    constructor(username: string, password: string) {
        this.Username = username;
        this.Password = password;
    }
}