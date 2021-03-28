

let strToInt (str:string) =
    match System.Int32.TryParse str with
    | true, value -> Some(value)
    | _ -> None


printfn "valid: %A" (strToInt "10")
printfn "invalid: %A" (strToInt "hue")

type MaybeBuilder() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) = Some x

let maybe = new MaybeBuilder()

let stringAddWorkflow x y z =
    maybe {
        let! a = strToInt x
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
    }

// test
let good = stringAddWorkflow "12" "3" "2"
let bad = stringAddWorkflow "12" "xyz" "2"

printfn "good: %A" good
printfn "bad: %A" bad

let strAdd str i =
    match strToInt str with
    | None -> None
    | Some x -> Some(x + i)

printfn "strAdd '2' + 2 %A" (strAdd "2" 2)
printfn "strAdd 'a' + 2 %A" (strAdd "a" 2)

let (>>=) m f =
    match m with
    | None -> None
    | Some 1 ->
        printfn "matched some 1"
        f 1
    | Some x ->
        printfn "the function is %A" f
        f x

// let (>>=) m f = Option.bind f m

Some(2) >>= strAdd "2"

// let good2 = strToInt "1" >>= strAdd "2" >>= strAdd "3"
// let bad2 = strToInt "1" >>= strAdd "xyz" >>= strAdd "3"

// strToInt result are parsed with >>= which see if it's none and then does not call the next expression, but if it's some it calls it, which is strAdd so strAdd knows that parameter "i" IS an integer, but it does not know if str is parsable so we call strToInt and match on that.

// printfn "good2: %A" good2
// printfn "bad2: %A" bad2