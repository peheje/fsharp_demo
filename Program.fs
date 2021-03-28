namespace someNamespace

module MyModule =

    let strToInt (str:string) =
        match System.Int32.TryParse str with
        | true, value -> Some(value)
        | _ -> None

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

    let strAdd str i =
        match strToInt str with
        | None -> None
        | Some x -> Some(x + i)


    let (>>=) m f =
        match m with
        | None -> None
        | Some x -> f x

    // let (>>=) m f = Option.bind f m


// strToInt result are parsed with >>= which see if it's none and then does not call the next expression, but if it's some it calls it, which is strAdd so strAdd knows that parameter "i" IS an integer, but it does not know if str is parsable so we call strToInt and match on that.


    [<EntryPoint>]
    let main argv =
        let hue = None >>= strAdd "2"

        printfn "done"
        0