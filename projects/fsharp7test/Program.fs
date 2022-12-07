open System

type OOOOOPtion<'a> =
    | Some of 'a
    | None
// Declaration of an enumeration.
type Color =
    | Red = 0
    | Green = 1
    | Blue = 2

let bbb: Double = 1

type ISomeFunctionality<'T when 'T :> ISomeFunctionality<'T>> =
    static abstract DoSomething: 'T -> 'T

type Vector =
    { x: float
      y: float }

    static member (~-)(v: Vector) = { x = -1.0 * v.x; y = -1.0 * v.y }
    static member (*)(v: Vector, a) = { x = a * v.x; y = a * v.y }
    static member (*)(a, v: Vector) = { x = a * v.x; y = a * v.y }
    static member (+?||<-)(v: Vector, a) = 4
    static member (+?||<-)(a, v: Vector) = 6

    override this.ToString() =
        this.x.ToString() + " " + this.y.ToString()


let v1 = { x = 3; y = 5 }

let v2 = v1 * 2.0
let v3 = 2.0 * v1
let v4 = -v1

Console.WriteLine v4

v2 +?||<- 1 |> Console.WriteLine
4 +?||<- v3 |> Console.WriteLine
