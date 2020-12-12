module FoodFactory

    open Point

    type FoodFactory (ch: char) = 
        let mutable food: Point = new Point()
        let Ch = ch
        member this.CreateFood x y = 
            let p = new Point(x, y, Ch)
            food <- p
            Draw p

        member this.Food = food