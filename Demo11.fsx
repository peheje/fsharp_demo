
let rec quicksort list =
    match list with
    | [] -> []
    | first::rest ->
        let smaller = rest |> List.filter (fun v -> v < first) |> quicksort
        let larger = rest |> List.filter (fun v -> v > first) |> quicksort
        List.concat [smaller; [first]; larger]
        
let xs = [42; 3; -2; 5; 35; 59]

printfn "%A" (xs |> quicksort)