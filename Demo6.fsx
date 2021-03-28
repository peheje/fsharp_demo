let divideBy bottom top =
    if bottom = 0
    then None
    else Some(top / bottom)

let divideByWorkflow x y w z =
    let a = x |> divideBy y
    match a with
    | None -> None
    | Some a' ->
        let b = a' |> divideBy w
        match b with
        | None -> None
        | Some b' ->
            let c = b' |> divideBy z
            match c with
            | None -> None
            | Some c' ->
                Some c'

               
let good = divideByWorkflow 12 3 2 1
let bad = divideByWorkflow 12 3 0 1

good |> printfn "%A"
bad |> printfn "%A"

let (>>=) m f = Option.bind f m

let divideByWorkflow2 x y w z =
    x |> divideBy y
    >>= divideBy w
    >>= divideBy z

let good2 = divideByWorkflow2 12 3 2 1
let bad2 = divideByWorkflow2 12 3 0 1

good2 |> printfn "%A"
bad2 |> printfn "%A"