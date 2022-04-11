namespace FlightAgency.Domain

open FlightAgency.Models

module public AuthorizationRoot =
    let Login(email: string, password: string, existingUsers: List<User>): Result<User, string> =
        let validateUserExists = List.tryFind (fun (user: User) -> user.Email.Equals(email)) existingUsers
        let result = match validateUserExists with
            | None -> Error "User not found."
            | Some userExists ->
                match userExists with
                | existingUser when existingUser.Password.Equals(password) -> Ok existingUser
                | _ -> Error "Password is incorrect."
        result

    let Register(email: string, name: string, password: string, existingUsers: List<User>): Result<User, string> = 
        let newUser = new User(email, name, password)
        let validateUserExists = List.tryFind (fun (user: User) -> user.Email.Equals(email)) existingUsers
        let result = match validateUserExists with
            | Some userExists -> Error "User already exists."
            | None -> Ok newUser
        result
