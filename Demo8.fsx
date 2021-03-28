let divideBy bottom top =
    if bottom = 0
    then None
    else Some(top/bottom)

let divideByWorkflow init x y z =
    let a = init |> divideBy x
    match a with
    | None -> None  // give up
    | Some a' ->    // keep going
        let b = a' |> divideBy y
        match b with
        | None -> None  // give up
        | Some b' ->    // keep going
            let c = b' |> divideBy z
            match c with
            | None -> None  // give up
            | Some c' ->    // keep going
                //return
                Some c'

let good = divideByWorkflow 12 3 2 1
let bad = divideByWorkflow 12 3 0 1

printfn "%A" good
printfn "%A" bad

// Using computation expression

type MaybeBuilder() =
    member this.Bind(x, f) =
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) =
        Some x

type MaybeBuilder2() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) = Some x

let maybe = new MaybeBuilder()
//let maybe = new MaybeBuilder2()

let divideByWorkflow2 init x y z =
    maybe {
        let! a = init |> divideBy x
        let! b = a |> divideBy y
        let! c = b |> divideBy z
        return c
    }

let good2 = divideByWorkflow2 12 3 2 1
let bad2 = divideByWorkflow2 12 3 0 1

printfn "%A" good2
printfn "%A" bad2