open System

// So the objective is to call do1 and if that has a value pass it to do2 and if that has a value to do3 and then return it. But the do functions can fail and instead return None, so they return Option

let random = Random()
let heads () = random.NextDouble() < 0.5
let do1 () = if heads () then Some(1) else None
let do2 x = if heads () then Some(x) else None
let do3 x = if heads () then Some(x) else None

// This leads to some tragic code with some pyramid of doom
let flow1 () =
    let d1 = do1 ()

    if d1.IsSome then
        let d2 = do2 d1.Value

        if d2.IsSome then
            let d3 = do3 d2.Value

            if d3.IsSome then
                Some(d3.Value)
            else None
        else
            None
    else
        None

printfn "flow1: %A" (flow1 ())

// Let's use bind, it only calls the next function if the value passed in is Some, not None

let bind nextFunction optionValue =
    match optionValue with
    | None -> None
    | Some v -> nextFunction v

let flow2 () =
    let d1 = do1 ()
    let d2 = bind do2 d1
    let d3 = bind do3 d2
    d3

printfn "flow2: %A" (flow2 ())

// Let's make it a bit prettier with the |> which passes the result from the previous statement

let flow3 () =
    let d3 = do1 () |> bind do2 |> bind do3
    d3

printfn "flow3: %A" (flow3 ())

// We try to make it even prettier, still the same thing happening though
// In F# we can make infix operator but for those we can only name them symbols. Here it is named
// "Dash, questionmark, bigger than"
// -?>
// As in "try to put this into the next function if it has a value"
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/symbol-and-operator-reference/

let (-?>) optionValue nextFunction = bind nextFunction optionValue

// Notice how we switch optionValue and nextFunction around, as the infix applies the result from previous statement as the first argument

let flow4 () =
    let d3 = do1 () -?> do2 -?> do3
    d3

printfn "flow4: %A" (flow4 ())

// So the option of do1 becomes the first argument of -?> and the function do2 is the second
// The ouput of providing that to -?> is another option which is then passed to do3

