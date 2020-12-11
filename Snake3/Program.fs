// Learn more about F# at https://fsharp.org
// See the 'F# Tutorial' project for more help.
open System;
open System.Collections.Generic;
open System.Linq;
open Walls;
open Point;
open FoodFactory;

let x = 80
let y = 26

let getRandomCoordinates = 
    let r = new Random()
    (r.Next(2, x - 2), r.Next(2, y - 2))
    

[<EntryPoint>]
let main argv =
    Console.SetWindowSize(x + 1, y + 1)
    Console.SetBufferSize(x + 1, y + 1)
    Console.CursorVisible = false
    let walls = new Walls(x, y, '#')
    let foodFactory = new FoodFactory('o')
    let k = getRandomCoordinates
    foodFactory.CreateFood (fst k) (snd k)
    //printfn "%A" argv
    0 // return an integer exit code
