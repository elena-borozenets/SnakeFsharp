module Snake
    
    open System
    open Point

    type public Direction = Up | Right | Down | Left 

    let getNextPoint (p:Point) (d:Direction) step= 
               match d with
               | Direction.Left -> new Point(p.X - step, p.Y, p.Ch)
               |Direction.Right ->new Point(p.X + step, p.Y, p.Ch)
               | Direction.Up -> new Point(p.X , p.Y - step, p.Ch)
               | Direction.Down ->new Point(p.X , p.Y + step, p.Ch)

    let rotation (rotate:bool) (direction:Direction) (key: ConsoleKey) =
            match rotate with
                | true ->
                    match direction with
                    | Left | Right -> 
                            match key with
                            | ConsoleKey.DownArrow -> (Direction.Down, false)
                            | ConsoleKey.UpArrow -> (Direction.Up, false)
                            | _ -> (direction, false)
                    | Up | Down->
                        match key with
                            | ConsoleKey.LeftArrow -> (Direction.Left, false)
                            | ConsoleKey.RightArrow ->(Direction.Right, false)
                            | _ -> (direction, false)
                | false -> (direction, false)
                | _ -> (direction, false)

    type Snake(x:int, y:int, length: int) as sn = 
      
            let Length = length
            let step = 1
            let mutable tail: Point = new Point()
            let mutable head: Point = new Point()
            let mutable rotate = true
            let mutable snake: Point list = []
            let mutable direction: Direction = Right

            do
                direction <-Right
                snake <- sn.DrawSnake x y length
                printf ""



            member this.DrawSnake x y length=
                let rec draw x y ch arr length action = 
                    match (x,y,length) with 
                    | (x, y, 0) -> arr
                    | (x, y, length) -> 
                        let p = Point(x, y, ch)
                        action p
                        draw (x-1) y ch (p::arr) (length-1) action

                let k = draw x y '*' [] length Point.Draw
                k

            member this.GetHead = List.last snake

            member this.GetNextPoint = getNextPoint (this.GetHead) direction step 

        

            member this.Move = 
                let head = this.GetNextPoint
                snake <- snake @ [head] //change order
                match snake with
                |first::other ->   
                    tail <- first
                    snake <- other
                | _ -> ()
                tail.Clear
                Draw head
                rotate <- true

            member this.Rotation key = 
                let rot = rotation rotate direction key
                direction <- fst rot
                rotate <- snd rot

            member this.IsHit (p: Point) = 
                match snake with
                | head::tail -> 
                            let list = tail |> List.rev |> List.tail
                            List.contains p list
                | _ -> false


            member this.Eat (p:Point) = 
                let head = this.GetNextPoint
                match head with
                | h when h = p -> 
                    snake <- (snake @ [head])
                    Draw head
                    true
                | _ -> false

            member this.CleanSnakePoints = 
                let rec clean (snake:Point list) = 
                    match snake with 
                    | head::tail -> 
                        head.Clear
                        clean tail
                    | _ -> ()
                clean snake