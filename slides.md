---
# try also 'default' to start simple
theme: default
# random image from a curated Unsplash collection by Anthony
# like them? see https://unsplash.com/collections/94734566/slidev
background: black
# apply any windi css classes to the current slide
class: "text-center"
# https://sli.dev/custom/highlighters.html
highlighter: shiki
# show line numbers in code blocks
lineNumbers: false
# some information about the slides, markdown enabled
info: |
  ## Slidev Starter Template
  Presentation slides for developers.

  Learn more at [Sli.dev](https://sli.dev)
# persist drawings in exports and build
drawings:
  persist: false
# use UnoCSS
css: unocss
---

# C#11 static abstract members <br/> 이해와 대비

---

# 자기 소개

## 현 직장

- 군집 물류 관제 솔루션을 개발하는 DaimResearch
- 웹 기반 프론트엔드
  - 실시간 모니터링
  - 리플레이 기능을 개발중입니다

## 관심 있는 것

- TypeScript & 웹 브라우저 기반 기술
- 닷넷 & F#
- 덜 일하고 많이 받는 기술

---

# 발표 구성 및 목표

- F# interfaces-with-static-abstract-members RFC 문서를 중심으로 내용 작성됨
- C# static abstract interface 기능 이해하기
- 다른 언어의 비슷한 기능 둘러보기
- 우려되는 점을 알아보기

<div style="margin-top: 14em; color: aqua;">
틀린 내용이 있다면 봐주십시오... 😿
</div>

---

# 왜 조사를 시작했는가?

> Sang Kil Cha | F# Korea Slack
>
> <br />
>
> 이번 .NET7에서 상당히 큰 언어적인 변화가 있었습니다. static abstract member를 선언할 수 있게 된 점인데,
>
> F#7에서도 지원하게 되었습니다. 이게 상당히 매력적이면서도 위험한 언어적인 요소라 F#개발자들이 엄청 고심한 흔적이 엿보입니다.
>
> 이 부분을 읽어보시길 추천드립니다.
>
> <br />
>
> 개인적으로는 F#이 언어적인 아름다움을 더 갖게 되어 좋네요..

<br />

- 매력적?
- 위험한?
- 아름다움?

<!--
123123
-->

---

# F#7에서의 경고

## [F# RFC FS-1124 - Interfaces with static abstract members](https://github.com/fsharp/fslang-design/blob/main/FSharp-7.0/FS-1124-interfaces-with-static-abstract-members.md#guidance)

> 지침
>
> ---
>
> - 유형 분류 충동에 굴복하지 마십시오
> - 최대 추상화 충동에 굴복하지 마십시오
> - 애플리케이션 코드에서 IWSAM을 사용하면 귀하 또는 귀하의 팀이 나중에 해당 사용을 제거할 때 큰 위험이 따릅니다
> - 구현이 안정적이고 폐쇄형이며 논쟁의 여지가 없는 유형에서만 IWSAM을 구현하십시오
> - 구성 프레임워크의 기초로 IWSAM을 사용하지 마십시오

<br />

## [Announcing F# 7 | Static abstract members support in interfaces](https://devblogs.microsoft.com/dotnet/announcing-fsharp-7/#static-abstract-members-support-in-interfaces)

> 이러한 단점들 때문에, F#에서
>
> _[warning FS3535]_ static abstract method를 포함한 interface를 선언하거나
>
> _[warning FS3536]_ 제네릭 형식 매개변수에 대한 제약 조건 상황 밖에서, 타입으로 사용되면
>
> 경고를 표시합니다.

---

# 이때의 감상

- 왜 이번에 별 예고도 없이 F#이 6 -> 7 버전업이 되었는가
  - [심지어 별로 추가된 기능도 없음](https://github.com/fsharp/fslang-design/tree/main/FSharp-7.0)
- static abstract members가 뭔가
  - 어디에 쓰는 건데
- 왜 이렇게 부정적인가
  - [null checking operator `!!`](https://github.com/dotnet/runtime/pull/64720) 같은 기능이 또 나왔나?

---

# static abstract members란?

.NET 6에서 미리보기 기능으로 들어갔으며 .NET 7에서 정식 기능으로 편입

`static virtual members in interface` 기능은 **Generic Math Support**를 위해 추가된 언어 기능 중 하나

- interface
  - **static virtual members in interfaces**
- operator
  - [checked user defined operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/checked-user-defined-operators) `checked(~~~)`
  - relaxed shift operators _이제 shift류 연산자의 두번째 인수가 정수(혹은 정수로 암시적 변환)가 아니어도 됨_
  - [unsigned right-shift operator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#unsigned-right-shift-operator-)

<br />

<MyLinks>

[.NET 6 | Static abstract members declared in interfaces](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/static-abstract-interface-methods)

[.NET 7 | Tutorial: Explore C# 11 feature - static virtual members in interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members)

[C# 11 | Generic Math Support](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#generic-math-support)

</MyLinks>

---

# static abstract members 단순 예제 (1)

```csharp
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

```

---

# static abstract members 단순 예제 (2)

```csharp
void whatIsYourFavorite<T>(T iHaveAFavorite) where T : IFavorite
{
  Console.WriteLine($"{iHaveAFavorite.GetType().Name}'s favorite is {T.Favorite}");
  Console.WriteLine($"{iHaveAFavorite.GetType().Name}': size at age 5 :  {T.SizeAtAge(5)}");
}

whatIsYourFavorite(new Dog());
// Dog's favorite is Bones
// Dog': size at age 5 :  10

whatIsYourFavorite(new Cat());
// Cat's favorite is Fish
// Cat': size at age 5 :  5

whatIsYourFavorite(new Tiger());
// Tiger's favorite is Human
// Tiger': size at age 5 :  20
```

---

# Generic Math란?

[Generic Math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math)

```csharp
static T Add<T>(T left, T right) where T : INumber<T>
{
    return left + right;
}
```

- 수학적 연산을 지원하는 파라미터의 타입을 제네릭하게 선언하고
- 위 제약 안에서 연산자를 통한 표현식 지원

---

# Generic Math의 static abstract members 필요성

<br />

> ... 연산자는 반드시 `static`으로 선언되어야 했기 때문에 ...

[Operator overloading (C# reference): _연산자 선언은 public와 static 한정자를 가져야한다_](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading)

<br />

> ... 이 기능(static abstract member)을 통해 연산자가 number-like 인터페이스 안에 (abstract로) 선언될 수 있었다 ...

_Generic Math 기능이 원하는 표현 방식_

1. 연산자를 통한 수학적 표현
2. 피연산자는 현재 스코프에서는 일반 인터페이스로 타입 제약된 상태로 형식이 매우 자유로움

_을 위해 인터페이스에 연산자를 선언하고 싶었구나!_

---

# 연산자와 관계없는 Generic Math

<span />

with `static abstract members`

```csharp
void doNumericThings<T1, T2>(T1 t1, T2 t2)
  where T1 : INumber<T1>
  where T2 : INumber<T2>
{
  var t1sOne = T1.One;
  var t2sZero = T2.Zero;
}
```

without `static abstract members` with `reflection`

```csharp
void doNumericThings<T1, T2>(T1 t1, T2 t2)
  where T1 : INumber<T1>
  where T2 : INumber<T2>
{
  // System.Numerics.INumberBase<System.Int32>.One
  var t1sOne = t1.GetType().GetRuntimeProperties().Where(property => property.Name.Contains("One")).First().GetValue(null);
  // System.Numerics.INumberBase<System.Int16>.Zero
  var t2sZero = t2.GetType().GetRuntimeProperties().Where(property => property.Name.Contains("Zero")).First().GetValue(null);
}
```

---

# Generic Math가 가져올 변화

<br />

> 이러한 인터페이스를 사용할 수 있다는 것은 제네릭 형식 또는 메서드의 **형식 매개 변수를 "숫자와 유사(number-like)"하도록** 제한할 수 있음

- 예를 들자면 숫자 인자 타입을 `int`, `float이` 아니라 `INumber`(number-like)로 선언할 수 있음

<br />

> 이러한 혁신을 통해 수학적 연산을 일반적으로, 즉 작업 중인 **정확한 유형을 알 필요 없이** 수행할 수 있습니다.
>
> 라이브러리 작성자는 "중복" 오버로드를 제거하여 코드 베이스를 단순화할 수 있기 때문에...
>
> 다른 개발자는 사용하는 API가 더 많은 형식을 지원하기 시작할 수 있으므로 간접적으로 이점을 얻을 수 있습니다.

- 작업자는 최대 공통 인터페이스로 작업하여 코드량을 줄이면서도
- 사용자는 해당 인터페이스를 지원하는 타입들을 더 많이 사용할 수 있음

---

# Java: Static Method in Interface

근데 이름만 같고 목적과 사용법이 매우 다르다

- Java 8에서 인터페이스에 기본 메소드와 정적 메소드 추가됨
- 인터페이스 내의 정적 메소드는 반드시 구현을 가지고 있어야 함
- 제네릭 메소드의 타입 파라미터로 클래스의 정적 메소드를 호출할 수는 없음
- 기능을 제공하되, 인스턴스화 될 수 없게끔 하는 제약을 제공 (기존에는 final + private constructor)
- Java에는 연산자 오버로딩이 없음

<MyLinks>

[Static method in Interface in Java](https://www.geeksforgeeks.org/static-method-in-interface-in-java)

[Static and Default Methods in Interfaces in Java](https://www.baeldung.com/java-static-default-methods)

[What is the purpose of a static method in interface from Java 8?](https://stackoverflow.com/questions/45780952/what-is-the-purpose-of-a-static-method-in-interface-from-java-8)

</MyLinks>

---

# Scala: method, implicit, trait

연산자가 정적 메소드일 필요가 없다는 것이 큰 차이점, 그리고 implicit이 scala에서 코드 축약의 핵심?

- Scala는 인스턴스 메소드가 메소드로서 동작
- 타입별로 암시적 변환 코드를 구현하여 인자의 타입을 맞출 수도 있고 [(C#도 마찬가지)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators)
- 제네릭, 트레이트, 암시적 파라미터를 통해
  - 기존 타입에 대한 서브 타이핑 없이 기능을 확장하고
  - 깔끔하고 제네릭한 코드로 타입별 기능을 사용할 수 있다.
  - 타입 클래스 패턴이라 부르며, Haskell의 TypeClass에서 파생

<MyLinks>

[TOUR OF SCALA | Operator](https://docs.scala-lang.org/tour/operators.html#inner-main)

[Implicit Conversions in Scala](https://www.geeksforgeeks.org/implicit-conversions-in-scala/)

[SCALA 3 — BOOK | Type Class](https://docs.scala-lang.org/scala3/book/types-type-classes.html)

[Type Classes. Scala의 Implicit 마법의 결정체](https://signal9.co.kr/2019/10/09/scala_type_class/)

[Type Classes in Scala and Haskell](https://www.slideshare.net/hermannhueck/type-classes-in-scala-and-haskell)

</MyLinks>

---

# F#: Statically Resolved Type Parameters

컴파일 시간에 실제 타입이 정해지는 타입 파라미터, [제네릭은 런타임 시점에서 결정](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generics-in-the-run-time)

```fsharp
fsi> let inline add a b = a + b ;;
val inline add:
  a: ^a -> b: ^b -> 'c when (^a or ^b) : (static member (+) : ^a * ^b -> 'c)
```

```fsharp
fsi> add 4 2 ;;
val it: int = 6

fsi> add -2.0  4.7 ;;
val it: float = 2.7

fsi> add "123" "가나다" ;;
val it: string = "123가나다"
```

- 타입 파라미터가 특정 멤버를 가지게 제약할 수 있음
- C++의 Function Template과 비슷하지만 [인라인 함수](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/inline-functions)에만 쓸 수 있음

<MyLinks>

[Statically Resolved Type Parameters](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/generics/statically-resolved-type-parameters)

[[ C++ ] 함수 템플릿(Function Template)과 템플릿 함수(Template Function)](https://musket-ade.tistory.com/entry/C-%ED%95%A8%EC%88%98-%ED%85%9C%ED%94%8C%EB%A6%BFFunction-Template%EA%B3%BC-%ED%85%9C%ED%94%8C%EB%A6%BF-%ED%95%A8%EC%88%98Template-Function)

</MyLinks>

---

# `double` in .net6.0 vs .net7.0

<div class="flex flex-row w-full gap-4" style="">
<v-clicks>
<div class='flex-1'>

## .net6.0

```csharp
struct Double :
  IComparable,
  IComparable<Double>,
  IConvertible,
  IEquatable<Double>,
  ISpanFormattable,
  IFormattable
{
  ...
}
```

</div>

<div class='flex-1'>

## .net7.0

<div class='overflow-scroll h-23em'>

```csharp
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
{
  ...
}
```

</div>

</div>
</v-clicks>
</div>

---

# 왜 문제인가? - 일반적인 단점

- 최대 추상화 충동을 유발
- 더 많은 형식 수준 계산에 대한 후속 요구
- 형식 수준 디버깅, 프로파일링 등에 대한 컴파일러 지원에 대한 후속 요구
- 마이크로 인터페이스의 확산
- 라이브러리 디자인에 안정적인 지점이 없음
- 올바른 정도의 일반성에 대해 논쟁의 여지가 있습니다
- 컴파일러 및 툴링은 큰 인터페이스 목록에서 느려짐

---

# 왜 문제인가? - 타입제약이 아닌 타입으로 사용

---

# 왜 문제인가? - 제네릭 타입 코드가 함수 패스 코드보다 덜 제네릭함

---

# 왜 문제인가? - 정적 추상 멤버의 구현은 매개변수화 되지 않으며 어느것에도 닫히지 않는다.

---

# 왜 문제인가? - F#의 구현 다향성의 증가

---

# RFC의 결론

- 안써도 문제없었다는 언어들~ 문장 발췌
- 그리고 도입했으니 어떻게하라는 그 맨 첨 리스트 다시 상기

---

# 나의 평가

- 또 하나 기능이 늘었다
- 일반화 프로그래밍의 도입
- 커뮤니티의 고인물화?
- 함께 일하기
- 복잡성의 팽창
- 자바랑 C# 볼수록 안비슷하다

```

```
