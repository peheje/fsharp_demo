let rec sum list =
    match list with
    | [] -> 0
    | first::rest -> first + sum rest

let average list = sum list / list.Length

printfn "sum %A" (sum [10; 20; 30])
printfn "average %A" (average [10; 20; 30])

printfn "zippy %A" ([1;2;3] |> List.zip [3;2;1])