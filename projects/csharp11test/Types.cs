namespace Types;

interface IFavorite
{
  static abstract string Favorite { get; }

  static virtual int SizeAtAge(int age) => age;
}

class Dog : IFavorite
{
  public static string Favorite { get => "Bones"; }
  public static int SizeAtAge(int age) => age * 2;
}
class Cat : IFavorite
{
  public static string Favorite { get => "Fish"; }
}
class Tiger : Cat, IFavorite
{
  new public static string Favorite { get => "Human"; }
  public static int SizeAtAge(int age) => age * 4;
}

interface IInterface
{
  static virtual int GetYourNumber() { return 5; }
}