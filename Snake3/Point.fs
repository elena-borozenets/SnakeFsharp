module Point
    open System;

    [<CustomComparison; CustomEquality>]
    type Point= 
       struct  
          val X:int
          val Y:int
          val Ch:char
          new(x:int, y:int, ch:char) = { X = x; Y = y; Ch = ch }

          //member public this.Draw =
          //    this.DrawPoint this.Ch

          member public this.Clear =
                  this.DrawPoint ' '

          member public this.DrawPoint (ch: char) =
              Console.SetCursorPosition(this.X, this.Y);
              Console.Write(ch);

          static member (==) (p1: Point, p2: Point) = 
                match (p1,p2) with
                | p1, p2 when p1.X = p2.X && p1.Y=p2.Y -> true
                | _ -> false

          static member (!=) (p1: Point, p2: Point) = 
                match (p1,p2) with
                | p1, p2 when not (p1.X = p2.X) || not (p1.Y=p2.Y) -> true
                | _ -> false

            member private this.Ident = this.X, this.Y
            interface IComparable<Point> with
                member this.CompareTo other =
                    compare this.Ident other.Ident

            interface IComparable with
                member this.CompareTo obj =
                    match obj with
                      | null                 -> 1
                      | :? Point as other -> (this :> IComparable<_>).CompareTo other
                      | _                    -> invalidArg "obj" "not a Category"
            interface IEquatable<Point> with
                member this.Equals other =
                    this.Ident = other.Ident
            override this.Equals obj =
                    match obj with
                      | :? Point as other -> (this :> IEquatable<_>).Equals other
                      | _                    -> false
             override this.GetHashCode () =
                hash this.Ident

       end

       
     let public Draw (p:Point) = 
                p.DrawPoint p.Ch
        
