let divide ifZero ifSuccess top bottom =
    if bottom = 0
    then ifZero()
    else ifSuccess(top / bottom)

// Divide with printf as handlers
let ifZero1 () = printfn "you can't divide by zero"
let ifSuccess1 x = printfn "division: %i" x
let divide1 = divide ifZero1 ifSuccess1

divide1 10 4
divide1 100 0

// Helper printer
let log x = printfn "%A" x

// Divide using option pattern
let ifZero2 () = None
let ifSuccess2 x = Some x
let divide2 = divide ifZero2 ifSuccess2

divide2 100 4 |> log
divide2 100 0 |> log

// Divide with failure
let ifZero3 () = failwith "div by 0"
let ifSuccess3 x = x
let divide3 = divide ifZero3 ifSuccess3

divide3 100 4 |> log
divide3 100 0 |> log