﻿// Learn more about F# at https://fsharp.org
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

let x = 80
let y = 26

let getRandomCoordinates = 
    let r = new Random()
    (r.Next(2, x - 2), r.Next(2, y - 2))

let walls = new Walls(x, y, '#')
let foodFactory = new FoodFactory('o')
let mutable snake = new Snake(x/2, y/2, 3)

 
let Loop (obj: Object) = 
       match snake.GetHead with
       | p when walls.IsHit p  ->  
               snake.CleanSnakePoints
               let p = snake.GetHead;
               Draw p;
               snake <- new Snake(x / 2, y / 2, 3);
       | p when   snake.IsHit p ->  
                    snake.CleanSnakePoints
                    snake <- new Snake(x / 2, y / 2, 3);
       | _ ->
           match foodFactory.Food with 
           | f when snake.Eat f -> 
                let k = getRandomCoordinates
                foodFactory.CreateFood (fst k) (snd k)
           | _ -> snake.Move    

    

[<EntryPoint>]
let main argv =
   
    Console.SetWindowSize(x + 1, y + 1)
    Console.SetBufferSize(x + 1, y + 1)
    Console.CursorVisible <- false
    
    let Timer time = new Timer (Loop, null, 0, 200)
    let k = getRandomCoordinates
    foodFactory.CreateFood (fst k) (snd k)
    

    while true do
        match Console.KeyAvailable with
        | true -> 
            let key = Console.ReadKey ();
            snake.Rotation key.Key
        | false -> ()
        
    //printfn "%A" argv
    0 // return an integer exit code


