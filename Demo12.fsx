
let mutable (a, b) = (10, 5)

printfn $"a: {a} b: {b}"

let swap (a : 'a byref) (b : 'a byref) =
    let c = a
    a <- b
    b <- c

swap &a &b

printfn $"a: {a} b: {b}"