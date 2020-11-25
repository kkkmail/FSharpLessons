namespace MathProblems

module EnchantedForest =

    type Animal =
        | Vampire
        | Spider
        | Unicorn


    type EatingRule =
        | VampireEatsSpider
        | VampireEatsUnicorn
        | SpiderEatsUnicorn

        static member getRule g =
            match g() % 3 with
            | 0 -> VampireEatsSpider
            | 1 -> VampireEatsUnicorn
            | _ -> SpiderEatsUnicorn


    type Forest =
        {
            vampires : int
            spiders : int
            unicorns : int
        }

        member f.canEvolve =
            match f.vampires, f.spiders, f.unicorns with
            | 0, 0, _ -> false
            | 0, _, 0 -> false
            | _, 0, 0 -> false
            | _ -> true


    let tryEat g f =
        let r = EatingRule.getRule g

        match r with
        | VampireEatsSpider when f.vampires > 0 && f.spiders > 0 ->
            { f with vampires = f.vampires - 1; spiders = f.spiders - 1; unicorns = f.unicorns + 1 }
        | VampireEatsUnicorn when f.vampires > 0 && f.unicorns > 0 ->
            { f with vampires = f.vampires - 1; spiders = f.spiders + 1; unicorns = f.unicorns - 1 }
        | SpiderEatsUnicorn when f.spiders > 0 && f.unicorns > 0 ->
            { f with vampires = f.vampires + 1; spiders = f.spiders - 1; unicorns = f.unicorns - 1 }
        | _ -> f


    let evolve g f =
        let tryEat = tryEat g

        let rec doEvolve (x : Forest) =
            match x.canEvolve with
            | true -> x |> tryEat |> doEvolve
            | false -> x

        doEvolve f
