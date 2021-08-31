export class RefreshTokenModel {
    constructor(public refreshToken: string, public userId: string, public authenticationId: string){}
}
