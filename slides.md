---
# try also 'default' to start simple
theme: seriph
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

# C#11 _static abstract members_ <br/> 그리고 <br/> F# _SRTP_

---

# 목표

- C#11의 새로운 기능인 static abstract members 이해하기
- 다른 언어들은 어떻게 제공하는지 살펴보기
- 어떤 용도에 사용하는지 살펴보기
- 이 기능에 반발하는 이유 알아보기
- F# SRTP 기능 알아보기

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

---

# F# 7에서의 경고

<br />

[F# RFC FS-1124 - Interfaces with static abstract members (IWSAMs)](https://github.com/fsharp/fslang-design/blob/main/FSharp-7.0/FS-1124-interfaces-with-static-abstract-members.md#guidance)

> 지침
>
> ---
>
> - 유형 분류 충동에 굴복하지 마십시오
> - 최대 추상화 충동에 굴복하지 마십시오
> - 애플리케이션 코드에서 IWSAM을 사용하면 귀하 또는 귀하의 팀이 나중에 해당 사용을 제거할 때 큰 위험이 따릅니다
> - 구현이 안정적이고 폐쇄형이며 논쟁의 여지가 없는 유형에서만 IWSAM을 구현하십시오
> - 구성 프레임워크의 기초로 IWSAM을 사용하지 마십시오

[Announcing F# 7 | Static abstract members support in interfaces](https://devblogs.microsoft.com/dotnet/announcing-fsharp-7/#static-abstract-members-support-in-interfaces)

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

# static abstract members의 역사

<br/>

.NET 6에서 미리보기 기능으로 들어갔으며 .NET 7에서 정식 기능으로 편입

`static virtual members in interface` 기능은 **Generic Math Support**를 위해 추가된 언어 기능 중 하나

- interface
  - **static virtual members in interfaces**
- operator
  - [checked user defined operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/checked-user-defined-operators) `checked(~~~)`
  - relaxed shift operators _이제 shift류 연산자의 두번째 인수가 정수(혹은 정수로 암시적 변환)가 아니어도 됨_
  - [unsigned right-shift operator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#unsigned-right-shift-operator-)

<br />

- [.NET 6 | Static abstract members declared in interfaces](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/static-abstract-interface-methods)
- [.NET 7 | Tutorial: Explore C# 11 feature - static virtual members in interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members)
- [C# 11 | Generic Math Support](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#generic-math-support)

---

# static abstract members 단순 예제 (1)

```csharp
interface IFavorite
{
  static abstract string Favorite { get; }
}

class Dog : IFavorite
{
  public static string Favorite { get => "Bones"; }
}
class Cat : IFavorite
{
  public static string Favorite { get => "Fish"; }
}
class Tiger : Cat, IFavorite
{
  public static string Favorite { get => "Human"; }
}

```

---

# static abstract members 단순 예제 (2)

```csharp
void whatIsYourFavorite<T>(T iHaveAFavorite) where T : IFavorite
{
  Console.WriteLine($"{iHaveAFavorite.GetType().Name}'s favorite is {T.Favorite}");
}

whatIsYourFavorite(new Dog()); // Dog's favorite is Bones
whatIsYourFavorite(new Cat()); // Cat's favorite is Fish
whatIsYourFavorite(new Tiger()); // Tiger's favorite is Human
```

---

# Generic Math 예제

<span />

with `static abstract members`

```csharp
void doNumericThings<T1, T2>(T1 t1, T2 t2)
  where T1 : INumber<T1>
  where T2 : INumber<T2>
{
  var t1sZero = T1.Zero;
  var t2sZero = T2.Zero;
}
```

without `static abstract members` with `reflection`

```csharp
void doNumericThings<T1, T2>(T1 t1, T2 t2)
  where T1 : INumber<T1>
  where T2 : INumber<T2>
{
  // System.Numerics.INumberBase<System.Int32>.Zero
  var t1sZero = t1.GetType().GetRuntimeProperties().Where(property => property.Name.Contains("Zero")).First().GetValue(null);
  // System.Numerics.INumberBase<System.Int16>.Zero
  var t2sZero = t2.GetType().GetRuntimeProperties().Where(property => property.Name.Contains("Zero")).First().GetValue(null);
}
```

---

# Generic Math 공식 문서 해석 (1)

[Generic Math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math)

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

# Generic Math 공식 문서 해석 (2)

[Generic Math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math)
<br />

> 이러한 인터페이스를 사용할 수 있다는 것은 제네릭 형식 또는 메서드의 **형식 매개 변수를 "숫자와 유사(number-like)"하도록** 제한할 수 있음

- 예를 들자면 숫자 인자 타입을 `int`, `float이` 아니라 `INumber`(number-like)로 선언할 수 있음

<br />

> 이러한 혁신을 통해 **수학적 연산**을 일반적으로, 즉 작업 중인 **정확한 유형을 알 필요 없이** 수행할 수 있습니다.
>
> 라이브러리 작성자는 "중복" 오버로드를 제거하여 코드 베이스를 단순화할 수 있기 때문에...
>
> 다른 개발자는 사용하는 API가 더 많은 형식을 지원하기 시작할 수 있으므로 간접적으로 이점을 얻을 수 있습니다.

- 작업자는 최대 공통 인터페이스로 작업하여 코드량을 줄이면서도
- 사용자는 해당 인터페이스를 지원하는 타입들을 더 많이 사용할 수 있음

---

# 다른 언어에서는 이 기능을 뭐라고 할까?

## General drawbacks

Statically-constrained qualified genericity is strongly distortive of the practical experience of using a programming language, whether in personal, framework-building, team or community situations. The effect of these distortions are well known from:

- Standard ML in the 1990s (e.g. SML functors and "fully functorized programming")
- C++ templates
- Haskell (type classes and their many technical extensions, abstract uses, generalizations and intensely intricate community discussions)
- Scala (implicits, their uses and abuses)
- Swift (traits, their uses and abuses)
