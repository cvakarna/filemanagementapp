export class User {
    constructor(public userName: string, public email: string, public userId: string, public token: string) {

    }
}

export class UserLoginModel {
    constructor(public email: string, public password: string) {

    }
}