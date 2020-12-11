module FoodFactory

    open Point

    type FoodFactory (ch: char) = 
        let Ch = ch
        member this.CreateFood x y = 
            let p = new Point(x, y, Ch)
            Draw p