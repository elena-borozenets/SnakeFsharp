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


    let rec draw x y ch arr length action = 
        match (x,y,length) with 
        | (x, y, 0) -> arr
        | (x, y, length) -> 
            let p = Point(x, y, ch)
            action p
            draw (x-1) y ch (p::arr) (length-1) action

    type SnakeStucture =
        {
            Tail: Point
            Head: Point
            Rotate: bool
            SnakePoints: Point list
            Direction: Direction
            Length: int
        }

        

    type Snake(snakeRecord:SnakeStucture) as sn = 
      
            let snake:SnakeStucture = snakeRecord
            //let mutable Length = snakeRecord.Length
            let step = 1
            //let mutable tail: Point = new Point()
            //let mutable head: Point = new Point()
            //let mutable rotate = true
            ////let mutable snake: Point list = []
            //let mutable direction: Direction = Right

            do
                let head = List.last snake.SnakePoints
                let k = sn.DrawSnake head.X head.Y snake.Length
                //snake = {Tail = new Point(); Head = new Point(); Rotate=true; Snake= sn.DrawSnake x y length; Length = length; Direction = Right}
                //direction <-Right
                //snake <- sn.DrawSnake x y Length
                printf ""

            new(x:int, y:int, length: int) as this1 = 
                        let snakeArr = draw x y '*' [] length Point.Draw;
                        let newSnake = {
                                 Head = new Point(); 
                                 Rotate=true; 
                                 SnakePoints= snakeArr; 
                                 SnakeStucture.Tail = List.last snakeArr;
                                 Length = length; 
                                 Direction = Right }
                        Snake(newSnake)
                        
         

            member this.DrawSnake x y length=
                let k = draw x y '*' [] length Point.Draw
                k

            member this.GetHead = List.last snake.SnakePoints

            member this.GetNextPoint = 
                        getNextPoint (this.GetHead) snake.Direction step 

            member this.GetCurrentSnake = snake


            member this.Move = 
                let head = this.GetNextPoint
                let newS = {snake with SnakePoints = (snake.SnakePoints@[head])}
                //snake <- snake @ [head] //change order
                match newS.SnakePoints with
                |first::other ->   
                    let newnewS = {newS with 
                                    Tail = first;
                                    SnakePoints = other;
                                    Rotate = true}
                    newnewS.Tail.Clear
                    Draw head
                    new Snake(newnewS)
                | _ -> 
                    snake.Tail.Clear
                    Draw head
                    this

            member this.Rotation key = 
                let rot = rotation snake.Rotate snake.Direction key
                let newS = {snake with 
                                  Direction = fst rot;
                                  Rotate = snd rot}
                new Snake(newS)

            member this.IsHit (p: Point) = 
                match snake.SnakePoints with
                | head::tail -> 
                            let list = tail |> List.rev |> List.tail
                            List.contains p list
                | _ -> false


            member this.Eat (p:Point) = 
                let head = this.GetNextPoint
                match head with
                | h when h = p -> 
                    
                    let newS = {snake with SnakePoints = (snake.SnakePoints @ [head]);
                                        Length = (snake.Length+1)}
                    Draw head
                    new Snake(newS), true
                | _ -> this, false

            member this.CleanSnakePoints = 
                let rec clean (snake:Point list) = 
                    match snake with 
                    | head::tail -> 
                        head.Clear
                        clean tail
                    | _ -> ()
                clean snake.SnakePoints


