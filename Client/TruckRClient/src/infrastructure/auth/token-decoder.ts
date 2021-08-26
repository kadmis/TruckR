import jwt_decode from "jwt-decode";

export class TokenDecoder {
    public static getTokenData(token: string): TokenData {
        let data = new TokenData();
        let decoded = jwt_decode(token);

        data.userId = decoded['nameid'];
        data.username = decoded['name'];
        data.email = decoded['email'];
        data.role = decoded['role'];
        data.expired = (Math.floor((new Date).getTime() / 1000)) >= decoded['exp'];
        data.msToExpire = (decoded['exp'] - (Math.floor((new Date).getTime() / 1000))) * 1000;

        return data;
    }
}

export class TokenData {
    userId: string;
    username: string;
    email: string;
    role: string;
    msToExpire: number;
    expired: boolean;
}
