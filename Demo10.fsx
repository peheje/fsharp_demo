let pipeInto (someExpression, lambda) =
    match someExpression with
    | None -> None
    | Some x -> lambda x

let divideBy bottom top =
    if bottom = 0
    then None
    else Some(top/bottom)

let divideByWorkflow x y w z =
    pipeInto(x |> divideBy y, fun a ->
    pipeInto(a |> divideBy w, fun b ->
    pipeInto(b |> divideBy z, fun c ->
    Some c
    )))

let good = divideByWorkflow 12 3 2 1
let bad = divideByWorkflow 12 3 0 1

printfn "%A" good
printfn "%A" bad

// Make our pipeInto an infix
let (>>=) m f = pipeInto(m, f)

let divideByWorkflow2 x y w z =
    x |> divideBy y >>= divideBy w >>= divideBy z

let good2 = divideByWorkflow2 12 3 2 1
let bad2 = divideByWorkflow2 12 3 0 1

printfn "%A" good2
printfn "%A" bad2