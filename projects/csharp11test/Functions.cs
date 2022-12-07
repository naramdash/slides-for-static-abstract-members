using System.Numerics;

namespace Functions;

static class Functions
{
  // The type 'T' cannot be used 
  // as type parameter 'TSelf' in the generic type or method 'INumber<TSelf>'. 
  // There is no boxing conversion or type parameter conversion 
  // from 'T' to 'System.Numerics.INumber<T>'. [csharp11test]csharp(CS0314)

  // Operator '+' cannot be applied to operands of type 'INumber<T>' 
  // and 'INumber<T>' [csharp11test]csharp(CS0019)

  // public static INumber<T> Add<T>(INumber<T> left, INumber<T> right) => left + right;
  public static INumber<T> Add<T>(T left, T right) where T : INumber<T> => left + right;
}