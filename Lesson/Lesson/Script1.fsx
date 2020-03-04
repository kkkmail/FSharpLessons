let x = 1234567890L

printfn "x = %s" (x.ToString().PadLeft(6, '0'))


type Attack =
    | Piss
    | Fart



type Dino =
    | Jack
    | Shit
    | Dumb


    member j.attack a =
        match a with
        | Piss -> 
            match j with
            | Jack -> printfn "Jack pissed."
            | Shit -> printfn "GFYS"
            | Dumb -> printfn ""
        | Fart -> 
            match j with
            | Jack -> printfn "Jack farted."
            | Shit -> printfn "Shit happended."
            | Dumb -> printfn "Dubmd farted."


let bot = Shit

bot.attack Fart

