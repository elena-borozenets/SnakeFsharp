﻿module Walls

    open Point 

    type Walls (xp:int, yp:int, chp:char) as wall =
            let Ch:char = chp
            //let mutable wal: Point list
            do
                let (m: Point list) = wall.DrawHorizontal(xp, 0)
                let (m: Point list) = wall.DrawHorizontal(xp, yp)
                let (m: Point list) = wall.DrawVertical(0, yp)
                let (m: Point list) = wall.DrawVertical(xp, yp)
                printfn ""

            member this.DrawHorizontal(x : int, y : int) = 
                let rec draw x y ch arr count action = 
                    match (x,y) with 
                    | (0, y) -> arr
                    | (x, y) -> 
                        let p = Point(x, y, ch)
                        action p
                        draw (x-1) y ch (p::arr) (count+1) action

                draw x y Ch [] 0 Point.Draw
                //let my = fun f -> Point.Draw f
                //List.map my result
                   
                   
            member this.DrawVertical (x : int, y : int) = 
                let rec draw x y ch arr count action= 
                    match (x,y) with 
                    | (x, 0) -> arr
                    | (x, y) -> 
                        let p = Point(x, y, ch)
                        action p
                        draw x (y - 1) ch (p::arr) (count+1) action

                draw x y Ch [] 0 Point.Draw
                //let result = (draw x y Ch [] 0 )
                //let my = fun f -> Point.Draw f
                //List.map my result
