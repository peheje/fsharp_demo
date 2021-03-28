
let rec quicksort list =
    match list with
    | [] -> []
    | first::rest ->
        let smaller, larger = rest |> List.partition (fun v -> v > first)
        List.concat [smaller; [first]; larger]
        
let xs = [42; 3; -2; 5; 35; 59]

printfn "%A" (xs |> quicksort)