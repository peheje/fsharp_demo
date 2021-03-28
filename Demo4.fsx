type State = New | Draft | Published | Inactive

let handleState state =
    match state with
    | New -> printfn "handled new"
    | Draft -> printfn "handled draft"
    | Published -> printfn "handled published"
    | Inactive -> printfn "handled inactive"

let myState = Inactive
handleState myState