open System.Net.Http

let client = new HttpClient()

async {
    let! response = client.GetAsync("https://www.google.dk") |> Async.AwaitTask
    let! text = response.Content.ReadAsStringAsync() |> Async.AwaitTask

    printfn "reponse is %s" text
} |> Async.RunSynchronously
