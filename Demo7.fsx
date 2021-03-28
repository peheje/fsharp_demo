let log p = printfn "expression is %A" p

let loggedWorkflow1 =
    let x = 42
    log x
    let y = 43
    log y
    let z = x + y
    log z
    //return
    z

// With computation expression

type LoggingBuilder() =
    let log p = printfn "expression is %A" p

    member __.Bind(x, f) =
        log x
        f x

    member __.Return(x) = 
        x

let logger = new LoggingBuilder()

let loggedWorkflow2 =
    logger {
        let! x = 42
        let! y = 43
        let! z = x + y
        return z
    }