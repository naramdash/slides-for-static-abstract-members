case class Centimeters(value: Double) extends AnyVal
case class Meters(value: Double) extends AnyVal
case class Kilometers(value: Double) extends AnyVal

implicit def meters2centimeters(meters: Meters): Centimeters =
  Centimeters(meters.value * 100)

implicit def kiloToMeter(kilo: Kilometers): Meters =
  Meters(kilo.value / 100)

@main 
def hello: Unit = 
  println("Hello world!")
  println(msg)
  
  val centimeters: Centimeters = Meters(2.5)
  println(centimeters)

  val meters: Meters = Kilometers(2.5)
  println(meters)


def msg = "I was compiled by Scala 3. :)"
