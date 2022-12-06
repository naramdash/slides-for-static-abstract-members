# double in 10 and 11

```csharp
// # c#10
struct Double :
  IComparable,
  IComparable<Double>,
  IConvertible,
  IEquatable<Double>,
  ISpanFormattable,
  IFormattable
{}

// # c#11
struct Double :
  IComparable,
  IComparable<Double>,
  IConvertible,
  IEquatable<Double>,
  ISpanFormattable,
  IFormattable,

  IParsable<Double>,
  ISpanParsable<Double>,
  IAdditionOperators<Double, Double, Double>,
  IAdditiveIdentity<Double, Double>,
  IBinaryFloatingPointIeee754<Double>,
  IBinaryNumber<Double>,
  IBitwiseOperators<Double, Double, Double>,
  IComparisonOperators<Double, Double, bool>,
  IEqualityOperators<Double, Double, bool>,
  IDecrementOperators<Double>,
  IDivisionOperators<Double, Double, Double>,
  IIncrementOperators<Double>,
  IModulusOperators<Double, Double, Double>,
  IMultiplicativeIdentity<Double, Double>,
  IMultiplyOperators<Double, Double, Double>,
  INumber<Double>, INumberBase<Double>,
  ISubtractionOperators<Double, Double, Double>,
  IUnaryNegationOperators<Double, Double>,
  IUnaryPlusOperators<Double, Double>,
  IExponentialFunctions<Double>,
  IFloatingPointConstants<Double>,
  IFloatingPoint<Double>,
  ISignedNumber<Double>,
  IFloatingPointIeee754<Double>,
  IHyperbolicFunctions<Double>,
  ILogarithmicFunctions<Double>,
  IPowerFunctions<Double>,
  IRootFunctions<Double>,
  ITrigonometricFunctions<Double>,
  IMinMaxValue<Double>
{}
```
