let pipeInto (someExpression, lambda) =
    printfn "expression is %A" someExpression
    someExpression |> lambda

pipeInto (42, fun x ->
pipeInto (43, fun y ->
pipeInto (x + y, fun z ->
z)))
