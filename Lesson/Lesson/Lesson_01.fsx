open System.Numerics

// Primitive types
let xInt = 1
let xFloat = 1.0
let xBool = true
let xString = "abc"
let xChar = 'A'

/// Record
type MyRecord =
    {
        a : int
        b : string
        c : bool
        d : int -> string
    }

let dd i = i.ToString()

let x1 = dd 2.0
let x2 = dd 2


let xRecord =
    {
        a = 1
        b = "abcd"
        c = false
        d = fun i -> i.ToString()
    }


/// Discriminated Union
type MyDicriminatedUnion =
    | A
    | B of MyRecord
    |DumbDumb of bool

let x3 = DumbDumb true


let x = 1
//printfn "x = %A" x

let myPurse = Some 11
let bum = None

let y = 2
let z = x + y
//printfn "z = %A" z

let square x x1 x2 x3 =
    x * x + x1 * x1 * x1 + x2 * x2 * x2 * x2 + x3

let z2 = square z
let z3 = z2 y
let z4 = z3 x
let z5 = z4 y

//printfn "z2 = %A" z2

let mult x y = x * y
let a = mult 4 5
//printfn "a = %A" a

let mult7 = mult 7
let b = mult7 8
//printfn "b = %A" b
//printfn "mult7 = %A" mult7

let takeUnit (_ : unit) = 1
let takeUnit1 () = 1

let aa = takeUnit
let bb = aa()


/// Function
let buyCakes cakePrice maybePurse =
    match maybePurse with
    | Some money ->
        let numberOfCakes = money / cakePrice

        if numberOfCakes <= 0
        then printfn "Sorry, you don't have enough money."
        else printfn "Cool! I bought %A cakes." numberOfCakes
    | None -> printfn "Go away you bum!!!"


let cakePrice = 2
//buyCakes cakePrice myPurse
//buyCakes cakePrice bum

let buyCrappyCakes = buyCakes (cakePrice / 2)
//buyCrappyCakes myPurse
//buyCrappyCakes bum

let rec factorial (n : bigint) =
    if n = BigInteger.One
    then BigInteger.One
    else n * (factorial (n - BigInteger.One))


type Alien =
    {
        lives : int
        healthPoints : int
        immortal : bool
        getSpeed : unit -> int
        hornLength : bigint option
    }


type Animal =
    | Bird
    | Cat of int
    | Dog of int * int
    | Alien of Alien


let rec kill animal =
    match animal with
    | Bird -> printfn "I killed the bird."
    | Cat lives -> printfn "I wagged the cat %A times." lives
    | Dog (hp, strength) ->
        if strength < 1000
        then printfn "I kicked the dog %A with %A strength." hp strength
        else printfn "Oops, this dog has %A strength. It cannot be killed." strength
    | Alien a ->
        match a.immortal with 
        | true -> printfn "Immortal alien cannot be killed."
        | false -> 
            Bird |> kill
            Cat a.lives |> kill
            Dog (a.lives, a.healthPoints) |> kill

let myPet =
    Alien
        {
            lives = 10
            healthPoints = 100
            immortal = false
            getSpeed = aa
            hornLength = 
                52 |> BigInteger |> factorial |> Some
                //Some (factorial (BigInteger 52))
        }
        |> Some

let tryKill() =
    match myPet with
    | Some p -> kill p
    | None -> printfn "You don't have a pet. Go away bum!!!"

tryKill()

//type Alien =
//    {
//        lives : int
//        healthPoints : int
//        immortal : bool
//        magicArmor : bool
//    }
//    static member defaultValue = 
//        {
//            lives = 1
//            healthPoints = 100
//            immortal = false
//            magicArmor = false
//        }
//    member this.puke () = 
//        printfn "Oops, I puked %A times." this.lives


//type Animal = 
//    | Cat of int
//    | Dog
//    | Rat
//    | Alien of Alien

//let kill animal = 
//    match animal with
//    | Cat lives -> printfn "Drown the cat %A times." lives
//    | Dog -> printfn "Kick the dog."
//    | Rat -> printfn "Poison the rat."
//    | Alien a ->
//        a.puke ()
//        if a.immortal then printfn "You cannot kill immportant alien!"
//        else
//            printfn "Stab the alien %A times with %A force." a.lives a.healthPoints

//let myPet = Animal.Alien { Alien.defaultValue with magicArmor = true }

//kill myPet

