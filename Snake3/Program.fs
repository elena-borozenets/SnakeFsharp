// Learn more about F# at https://fsharp.org
// See the 'F# Tutorial' project for more help.
open System;
open System.Collections.Generic;
open System.Linq;
open Walls;
open Point;
open FoodFactory;
open Snake;
open System.Threading
open System.Linq.Expressions
open System.Threading.Tasks

let x = 80
let y = 26


let r = new Random()
let walls = new Walls(x, y, '#')
let foodFactory = new FoodFactory('o')
let mutable snake2 = new Snake(x/2, y/2, 3)
let mutable key:ConsoleKeyInfo = new ConsoleKeyInfo()
let defaultKey:ConsoleKeyInfo = new ConsoleKeyInfo()

let getRandomCoordinates =    
                let newX = r.Next(2, x - 2)
                let newY = r.Next(2, y - 2)
                (newX, newY)


 
let Loop (obj: Object) = 
    let rec game (snake:Snake) =
           let head = snake.GetHead
           match head with
           | p when walls.IsHit p  ->  
                   snake.CleanSnakePoints
                   Draw head
                   game (new Snake(x / 2, y / 2, 3));
                  // Thread.Sleep(1000)
           | p when   snake.IsHit p ->  
                        snake.CleanSnakePoints
                        game (new Snake(x / 2, y / 2, 3));
                       // Thread.Sleep(1000)
           | _ ->
                   let food = foodFactory.Food
                   let newSnakeEat = snake.Eat food 
                   let newSnake = fst newSnakeEat
                   let foodIsEaten = snd newSnakeEat
                   match foodIsEaten with 
                   | true -> 
                        let newX = r.Next(2, x - 2)
                        let newY = r.Next(2, y - 2)
                        foodFactory.CreateFood newX newY
                   
                        //Thread.Sleep(1000)
                        game newSnake
                    
                   | _ -> 
                            Draw foodFactory.Food
                            Thread.Sleep(1000)
                            //snake.DrawSnake    
                            match key.Key with
                            | k when k = defaultKey.Key -> game (snake.Move)                                
                            | _ ->  
                                    let newSnake = snake.Rotation key.Key
                                    key<- new ConsoleKeyInfo()
                                    let dir = newSnake.GetCurrentSnake.Direction
                                    game (newSnake.Move)    
                        
    game (new Snake(x/2, y/2, 3))
    printf ""

let waitForAnyKey =   Async.FromContinuations (fun (cont, _, _) ->   cont (Console.ReadKey ())                                                                   )

let rec f = 
     async{
            //let! t = Task.Delay(200) |> Async.AwaitTask
            //printfn "f"
            match Console.KeyAvailable with
                                | true -> 
                                    let! key1 = waitForAnyKey
                                    key <- key1
                                | _ -> ()
            return! f 
            }



let rec g t= 
    let k = Console.ReadKey()
    match k.Key with
    | ConsoleKey.K -> ()
    | _ ->   g t


[<EntryPoint>]
let main argv =
   
    //Console.SetWindowSize(x + 1, y + 1)
    //Console.SetBufferSize(x + 1, y + 1)
    Console.CursorVisible <- false
    
    let k = getRandomCoordinates
    foodFactory.CreateFood (fst k) (snd k)

    let time = new Timer (Loop, null, 0, 500)

    f |> Async.StartImmediate


    g 1
    Console.ReadKey()
    Console.ReadKey()
    //let f = async {}

    //let f =  
    //    async {
    //            match Console.KeyAvailable with
    //                    | true -> 
    //                        let key1 = Console.ReadKey ();
    //                        key <- key1
    //                    | _ -> ()    
    //              }
     //Async.StartImmediate f
     //Async.StartImmediate f

    //while true do
    //    match Console.KeyAvailable with
    //    | true -> 
    //        let key = Console.ReadKey ();
    //        let newSnake = snake.Rotation key.Key
    //        newSnake
    //    | false -> snake
        
    //printfn "%A" argv
    0 // return an integer exit code


