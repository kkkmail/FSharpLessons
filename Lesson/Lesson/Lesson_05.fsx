#r "System.Numerics.dll"
#r @"C:\Users\kkkma\.nuget\packages\fsharp.collections.parallelseq\1.1.2\lib\net45\FSharp.Collections.ParallelSeq.dll"

open System.Numerics
open FSharp.Collections.ParallelSeq

//let f (x : BigInteger) (y : BigInteger) = x * y

//let x = 1 |> BigInteger
//printfn "x = %A" x

//let rec factorial (x : BigInteger) = 
//    if x = BigInteger.One
//    then BigInteger.One
//    else x * (factorial (x - BigInteger.One))

let factorial x =
    match x with
    | 0 -> bigint.One
    | n when n > 0 ->
        [ for i in 1..n -> i ]
        |> List.fold (fun acc r -> acc * (BigInteger r)) BigInteger.One

    | _ -> bigint.MinusOne


let bigintToStrings n (x : bigint) = 
    let s = sprintf "%A" x
    let x = s.ToCharArray()
    let y = x |> Array.chunkBySize n |> Array.map (fun e -> System.String.Concat(e))
    y


let printBigInt n (x : bigint) = 
    bigintToStrings n x
    |> Array.map (fun e -> printfn "%s" e)


#time
factorial 1000000
|> printBigInt 100
#time

//let rec bigPower (x : BigInteger) k =
//    match k with
//    | 0 -> BigInteger.One
//    | p when p > 0 -> x * (bigPower x (k - 1))
//    | _ -> BigInteger.Zero

//999
//|> BigInteger
//|> fun x -> bigPower x 999
//|> printfn "%A"
