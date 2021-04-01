type Person = {first:string; last:string}

type MixedType =
  | Tup of int * int
  | P of Person

let doIt x =
    match x with
    | P {first=f; last=l} -> (f, l)