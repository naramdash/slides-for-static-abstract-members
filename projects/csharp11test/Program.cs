﻿// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection;
using System.Numerics;
using System.Linq;

using Types;
using static Utils.ConsoleUtil;

#region 1. Very simple example
{
  printHeader("1. Very simple example");

  void whatIsYourFavorite<T>(T iHaveAFavorite) where T : IFavorite
  {
    Console.WriteLine($"{iHaveAFavorite.GetType().Name}'s favorite is {T.Favorite}");
    Console.WriteLine($"{iHaveAFavorite.GetType().Name}': size at age 5 :  {T.SizeAtAge(5)}");
  }

  whatIsYourFavorite(new Dog());
  whatIsYourFavorite(new Cat());
  whatIsYourFavorite(new Tiger());
}
#endregion

#region 2. Generic Math
{
  printHeader("2. Generic Math");

  void doNumericThings<T1, T2>(T1 t1, T2 t2)
    where T1 : INumber<T1>
    where T2 : INumber<T2>
  {
    var t1sOne = T1.One;
    Console.WriteLine($"{nameof(t1sOne)}({t1sOne})'s type: {t1sOne.GetType().Name}");

    var t2sZero = T2.Zero;
    Console.WriteLine($"{nameof(t2sZero)}({t2sZero})'s type: {t2sZero.GetType().Name}");
  }

  Int32 i = 1;
  Int16 j = 4;
  doNumericThings(i, j);
}
#endregion

#region 3. Generic Math without static abstract members
{
  printHeader("3. Generic Math without static abstract members");

  void doNumericThingsWithReflection<T1, T2>(T1 t1, T2 t2)
    where T1 : INumber<T1>
    where T2 : INumber<T2>
  {
    var t1sOne =
      t1
        .GetType()
        .GetRuntimeProperties()
        .Where(property => property.Name.Contains("One"))
        .First()
        .GetValue(null);

    Console.WriteLine($"{nameof(t1sOne)}({t1sOne})'s type: {t1sOne.GetType().Name}");

    var t2sZero =
      t2
        .GetType()
        .GetRuntimeProperties()
        .Where(property => property.Name.Contains("Zero"))
        .First()
        .GetValue(null);

    Console.WriteLine($"{nameof(t2sZero)}({t2sZero})'s type: {t2sZero.GetType().Name}");
  }

  int i = 1;
  Int16 j = 4;
  doNumericThingsWithReflection(i, j);
}
#endregion

#region 4. BAD CALLING
{
  // A static virtual or abstract interface member 
  // can be accessed only on a type parameter. 
  // [csharp11test]csharp(CS8926)
  // IInterface.GetYourNumber();

  // void doNumericThings(IFavorite t1, IFavorite t2)
  // {
  //   // A static virtual or abstract interface member 
  //   // can be accessed only on a type parameter. 
  //   // [csharp11test]csharp(CS8926)
  //   var sizeatage = IFavorite.SizeAtAge(2);
  // }
  void doNumericThings<T>(T t1, T t2) where T : IFavorite
  {
    var sizeatage = T.SizeAtAge(2);
  }
}
#endregion