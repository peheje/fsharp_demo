namespace someNamespace

module MyModule =
    open System

    let booth (xs: float array) =
        let x0 = xs.[0]
        let x1 = xs.[1]
        let t1 = Math.Pow(x0 + 2.0 * x1 - 7.0, 2.0)
        let t2 = Math.Pow(2.0 * x0 + x1 - 5.0, 2.0)
        t1 + t2

    let value = booth [| -2.12; -4.39 |]
    printfn "value: %f" value

    let logCsv = false
    let print = 1000
    let optimizer = booth
    let parameters = 2
    let (boundMin, boundMax) = (-10.0, 10.0)
    let generations = 1000
    let popsize = 50
    let (mutateMin, mutateMax) = (0.2, 0.95)
    let (crossoverMin, crossoverMax) = (0.1, 1.0)

    let random = Random(42)

    let rand min max () = random.NextDouble() * (max - min) + min
    let rand01 = rand 0.0 1.0
    let randBounds = rand boundMin boundMax
    let randCrossover = rand crossoverMin crossoverMax
    let randMutate = rand mutateMin mutateMax

    let sample (array: _ array) = array.[random.Next(0, array.Length)]
    let clamp value = Math.Clamp(value, boundMin, boundMax)

    let mutable crossover = 0.9
    let mutable mutate = 0.4

    let mutable trial = Array.zeroCreate<float> parameters
    let mutable pop =
        Array.init popsize (fun _ -> Array.init parameters (fun _ -> randBounds ()))
    let mutable scores = pop |> Array.map optimizer

    [<EntryPoint>]
    let main argv =
        for g in 0 .. generations - 1 do
            crossover <- randCrossover ()
            mutate <- randMutate ()

            for i in 0 .. popsize - 1 do
                let x0 = sample pop
                let x1 = sample pop
                let x2 = sample pop
                let xt = pop.[i]

                for j in 0 .. parameters - 1 do
                    if rand01 () < crossover then
                        trial.[j] <- clamp (x0.[j] + (x1.[j] - x2.[j]) * mutate)
                    else
                        trial.[j] <- xt.[j]

                let scoreTrial = optimizer trial

                if scoreTrial < scores.[i] then
                    pop.[i] <- trial |> Array.copy
                    scores.[i] <- scoreTrial

                if g % print = 0 || g = generations - 1 then
                    let bestIndex =
                        scores |> Array.indexed |> Array.minBy snd |> fst

                    printfn "generation %i" g
                    printfn "generation best %A" pop.[bestIndex]
                    printfn "generation best score %f" scores.[bestIndex]

        0
