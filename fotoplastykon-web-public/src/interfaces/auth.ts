export interface TokenModel
{
    token: string;
    expires: string;
}

export interface RegisterModel
{
    userName: string;
    email: string;
    firstName: string;
    surname: string;
    password: string;
    repeatPassword: string;
}

export interface LoginModel
{
    username: string;
    password: string;
}

export interface PasswordModel
{
    currentPassword: string;
    newPassword: string;
    repeatPassword: string;
}

export interface ResetModel
{
    email: string;
    newPassword: string;
    repeatPassword: string;
}
