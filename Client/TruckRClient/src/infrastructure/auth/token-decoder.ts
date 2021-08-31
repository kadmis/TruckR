import jwt_decode from "jwt-decode";

export function getTokenData(token: string): TokenData {
    if(!token || token.length == 0)
        return new TokenData();
        
    let data = new TokenData();
    let decoded = jwt_decode(token);

    data.userId = decoded['nameid'];
    data.username = decoded['name'];
    data.email = decoded['email'];
    data.role = decoded['role'];
    data.fullName = decoded['given_name'];
    data.authenticationId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid'];
    data.expired = (Math.floor((new Date).getTime() / 1000)) >= decoded['exp'];
    data.msToExpire = (decoded['exp'] - (Math.floor((new Date).getTime() / 1000))) * 1000;

    return data;
}

export class TokenData {
    userId: string;
    username: string;
    email: string;
    role: string;
    fullName: string;
    authenticationId: string;
    msToExpire: number;
    expired: boolean;
}
