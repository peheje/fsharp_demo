let max a b = if a > b then a else b
let m1 = max 1 2
let m2 = max 1.0 2.0

let inline square x = x * x

let xs = [ 1.0 .. 10.0 ]
let ys = [ 1 .. 10 ]

let xxs = xs |> List.map square
printfn "xss looks like this: %A" xxs

let yys = ys |> List.map square
printfn "yys looks like this: %A " yys
