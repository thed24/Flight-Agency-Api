namespace FlightAgency.Domain

open FlightAgency.Models;

module Parser =
    let ByEmail email = (fun (user: User) -> user.Email.Equals(email)) |> List.tryFind
    let ById id = (fun (user: User) -> user.Id.Equals(id)) |> List.tryFind

module public UserAggregateRoot =
    let Login(email: string, password: string, existingUsers: List<User>): Result<User, string> =
        let user = Parser.ByEmail email existingUsers
        let result = match user with
                        | None -> Error "User not found."
                        | Some user ->
                            match user with
                            | user when user.Password.Equals(password) -> Ok user
                            | _ -> Error "Incorrect password."
        result

    let Register(email: string, name: string, password: string, existingUsers: List<User>): Result<User, string> = 
        let newUser = new User(email, name, password)
        let user = Parser.ByEmail email existingUsers
        let result = match user with
                        | Some _ -> Error "User already exists."
                        | None -> Ok newUser
        result

    let AddTrip(id: int, existingUsers: List<User>, trip: Trip): Result<User, string> =
        let user = Parser.ById id existingUsers
        let result = match user with
                        | None -> Error "User not found."
                        | Some user ->
                            match user with
                            | user when user.Trips.Contains(trip) -> Error "Trip already exists."
                            | _ -> Ok (user.AddTrip trip)
        result

    let GetTrips(id: int, existingUsers: List<User>): seq<Trip> =
        let user = Parser.ById id existingUsers
        let result = match user with
                        | None -> List.empty
                        | Some user -> List.ofSeq(user.Trips)
        result