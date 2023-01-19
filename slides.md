---
theme: default
class: text-right
highlighter: shiki
lineNumbers: true
favicon: "#"
fonts:
  sans: "Nanum Gothic"
  mono: "Fira Code"
info: |
  ## C#11 static abstract members <br/> 이해와 대비

  written by [naram.dash](https://github.com/naramdash)

  [F# |> I ❤️](https://fsharp.org)
drawings:
  persist: false
css: unocss
title: C#11 static abstract members 이해와 대비
layout: image
image: ./Intro.jpg
---

<h1 style="color: black; position: absolute; bottom: 3em"> C#11 static abstract members <br /> 이해와 대비</h1>

<!--
안녕하세요

지금부터 C#11 스태틱 어브스트랙트 멤버에 대한 이해와 대비 세션을 시작하겠습니다

저는 발표를 맡게된 김주호입니다.

(10초)

-->

---

## <fluent-emoji-nerd-face /> 발표자

김주호

## <fluent-emoji-link /> 발표 자료

[**http**://slides.juho.kim](http://slides.juho.kim)

---

## <fluent-emoji-bullseye /> 목표

static abstract members 기능의 장점과 주의사항을 함께 파악

## <fluent-emoji-books /> 구성

- F# interfaces-with-static-abstract-members RFC 문서를 중심으로 내용 작성됨
- C# static abstract interface 기능 이해하기
- 다른 언어의 비슷한 기능 둘러보기
- 도입과정에서 나타난 우려 및 주의사항 살펴보기

<div style="margin-top: 14em; color: aqua;">
틀린 내용이 있다면 봐주십시오... 😿
</div>

<!--
C#11은 Generic Math을 지원하기 위해 static abstract members라는 새로운 언어 기능을 도입하였습니다.

이 기능은 C#의 표현력을 더욱 증가시켜주었지만 도입과정에서 우려사항이 있었습니다.

해당 내용이 자세히 표현된 F# RFC 문서와 함께 이 기능의 장점과 주의사항을 함께 파악해보겠습니다.

발표 내용는 interface with static abstract member 기능에 대한 F#의 RFC 문서를 중심으로 작성되었습니다.

먼저 C# static abstract interface 기능을 이해한 후,

다른 언어들의 비슷한 기능들을 알아보고

이 기능에 대한 우려 및 주의사항에 대해서 알아보겠습니다.
-->

---

# <fluent-emoji-magnifying-glass-tilted-left/> 왜 조사를 시작했는가?

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
이번 발표를 준비하게 된 이유는

F# Korea 슬랙에서 다음과 같은 메시지를 봤기 때문입니다.

C#이야 업데이트 된다고 몇달전부터 글이 많이 올라온 것 같은데

F#이 7으로 업데이트 되었다고?

근데 그게 매력적이면서 위험하면서 아름답다고?

그래서 알아봤습니다.

다만 이번 발표에서는 C# 위주로 구성되었으니 안심하셔도 됩니다.
-->

---

# <fluent-emoji-warning /> F#7에서의 경고

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

<!--
C#에서 도입된 기능들도 F#의 상호운용성에 영향을 끼치기 때문에

F# 관련기사에서도 그 기능의 여파를 확인할 수 있었는데요

관련된 내용들이 굉장히 경고에 가까운 내용들이었습니다.

(RFC읽기) 하거나 (경고읽기)한다고 했는데요.
-->

---

# <fluent-emoji-thinking-face /> 이때의 감상

- 왜 이번에 별 예고도 없이 F#이 6 -> 7 버전업이 되었는가
  - [심지어 별로 추가된 기능도 없음](https://github.com/fsharp/fslang-design/tree/main/FSharp-7.0)
- static abstract members가 무엇인가
  - 어디에 쓰는 건데
- 왜 이렇게 부정적인가
  - [null checking operator `!!`](https://github.com/dotnet/runtime/pull/64720) 같은 기능이 또 나왔나?

<!--
이때 제가 든 생각들입니다.

왜 버전업이 된 것이지? 네 이건 당연히 F#이 C#과 상호운영할 수 있어야 되기 때문에 끌려올라갔다라고 볼 수 있겠고요

static abstract members 흠 이건 C# OOP에서 본 키워드들인데 뭔가 3중 합체가 되어있네?

근데 왜 이렇게 부정적이지? 못 넣을 기능이라도 넣은건가?

그래서 더 알아보고 싶어졌습니다.
-->

---

# <codicon-symbol-interface /> static abstract members란?

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

<!--
영어로 말하기 좀 익숙치 않아서 앞으로는 정적추상멤버라고 말하도록 하겠습니다.

이 기능의 유래에 대해서 먼저 알아보자면요, 이 기능은 일반 수학이라는 새로운 코드 패턴을 도입하기 위해 만들어진 4가지 기능 중 하나입니다.

여러 기능이 있지만 정적추상멤버에 대해서 집중적으로 알아보기로 하고, 다른 기능에 대해서는 슬라이드에 달아놓은 링크로 확인하시면 되겠습니다.
-->

---

# <icon-park-file-code /> static abstract members 단순 예제 (1)

```csharp
interface IAnimal
{
  static abstract string Favorite { get; }
  static virtual int SizeAtAge(int age) => age;
}

class Dog : IAnimal
{
  public static string Favorite { get => "Bones"; }
  public static int SizeAtAge(int age) => age * 2;
}
class Cat : IAnimal
{
  public static string Favorite { get => "Fish"; }
}
class Tiger : Cat, IAnimal
{
  new public static string Favorite { get => "Human"; }
  public static int SizeAtAge(int age) => age * 4;
}

```

<!--
바로 코드로 들어가겠습니다.

IAnimal라는 인터페이스를 선언하고

페이버리트는 추상으로, 사이즈엣에이지는 버추얼로 구현과 함께 작성했습니다.

그리고 각 독 캣 타이거에서 각 구현을 하고 타이거의 경우 캣에서 온 구현을 숨기면서 Favorite를 구현했습니다.
-->

---

# <icon-park-file-code /> static abstract members 단순 예제 (2)

```csharp
void describeAnimal<T>(T animal) where T : IAnimal
{
  Console.WriteLine($"{animal.GetType().Name}'s favorite is {T.Favorite}");
  Console.WriteLine($"{animal.GetType().Name}'s size at age 5: {T.SizeAtAge(5)}");
}

describeAnimal(new Dog());
// Dog's favorite is Bones
// Dog': size at age 5: 10

describeAnimal(new Cat());
// Cat's favorite is Fish
// Cat's size at age 5: 5

describeAnimal(new Tiger());
// Tiger's favorite is Human
// Tiger's size at age 5: 20
```

<!--
자 그럼 IAnimal로 파라미터 타입에 제약을 건 함수를 만든 뒤에

타입파라미터를 통해 각 정적 프로퍼티와 메소드를 불러올 수 있는 것을 확인하실 수 있습니다.
-->

---

# <fluent-emoji-input-numbers /> [Generic Math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math)란?

<span />

```csharp
static T Add<T>(T left, T right) where T : INumber<T>
{
    return left + right;
}
```

- 제네릭 타입 혹은 메소드의 타입 파라미터에 "수학적 연산을 구현하도록" 제약
- 위 제약 안에서 연산자를 통한 표현식 지원

<!--
이번에 도입된 일반 수학이란,

파라미터 타입 제약으로 수학적 연산을 지원하는 타입을 제네릭하게 지원하고

이 제약 안에서 연산자를 통한 수학적 표현을 지원하는 것입니다.
-->

---

# <fluent-emoji-red-question-mark /> static abstract members 필요 이유

<span />

> ... 연산자는 반드시 `static`으로 선언되어야 했기 때문에 ...

[Operator overloading (C# reference): _연산자 선언은 public와 static 한정자를 가져야한다_](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading)

<br />

> ... 이 기능(static abstract member)을 통해 연산자가 number-like 인터페이스 안에 (abstract로) 선언될 수 있었다 ...

_Generic Math 기능이 원하는 표현 방식_

1. 연산자를 통한 수학적 표현
2. 피연산자는 현재 스코프에서는 인터페이스를 구현한 오브젝트이며, 실제 타입은 매우 다양할 수 있음

_을 위해 인터페이스에 연산자를 선언하고 싶었구나!_

<!--
그렇다면 왜 정적추상멤버가 필요했을까요?

C#에서 연산자 오버로딩은 반드시 public와 static 한정자와 함께 작성되어야하고요

일반 수학은 연산자들을 공통점으로 많은 타입을 지원하기 위해 인터페이스 제약을 활용했다고 봅니다.

-->

---

# <fluent-emoji-red-question-mark /> 연산자와 관계없는 Generic Math에도...

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

<!--
이번에는 연산자와 관계없는 코드를 보실까요

위의 코드조각은 T1, T2 제네릭 타입을 통해 One Zero 속성을 호출하고 있습니다.

하지만 이 기능을 정적 추상 멤버가 없을때에는 아래와 같은 리플렉션을 통해 찾아와야하겠죠.

게다가 One의 경우에는, NegativeOne을 가져올 가능성도 있기 때문에 안전하지 않을 수 있습니다.
-->

---

# <fluent-emoji-soon-arrow /> Generic Math가 가져올 변화

<span />

> 이러한 인터페이스를 사용할 수 있다는 것은 제네릭 형식 또는 메서드의 **형식 매개 변수를 "숫자와 유사(number-like)"하도록** 제한할 수 있음

- 예를 들자면 숫자 인자 타입을 `int`, `float`이 아니라 `INumber`(number-like)로 선언할 수 있음

<br />

> 이러한 혁신을 통해 수학적 연산을 일반적으로, 즉 작업 중인 **정확한 유형을 알 필요 없이** 수행할 수 있습니다.
>
> 라이브러리 작성자는 "중복" 오버로드를 제거하여 코드 베이스를 단순화할 수 있기 때문에...
>
> 다른 개발자는 사용하는 API가 더 많은 형식을 지원하기 시작할 수 있으므로 간접적으로 이점을 얻을 수 있습니다.

- 작업자는 최대 공통 인터페이스로 작업하여 코드량을 줄이면서도
- 사용자는 해당 인터페이스를 지원하는 타입들을 더 많이 사용할 수 있음

<!--
일반 수학을 정리해보자면,

어떤 기능이 받아들일 수 있는 매개변수의 폭이 특정 타입이 아닌 인터페이스를 통해  도메인에서 통용될 수 있는 범위로 확장할 수 있게 되었고

이러한 기능은 앞으로 코드 작성자로 하여금 더 적은 코드로 같은 기능 범위를 커버할 수 있게 될 것이고

사용자로 하여금 해당인터페이스만 구현하면 되기에 더 많은 타입을 사용할 수 있게 될거라는 기대를 가질 수 있습니다.
-->

---

# <logos-java /> Java: Static Method in Interface

근데 이름만 같고 목적과 사용법이 매우 다르다

- Java 8부터 인터페이스에 기본 메소드와 정적 메소드를 작성할 수 있음
- 인터페이스 내의 정적 메소드는 반드시 구현을 가지고 있어야 함
- 제네릭 메소드의 타입 파라미터로 클래스의 정적 메소드를 호출할 수는 없음
- 기능을 제공하되, 인스턴스화 될 수 없게끔 하는 제약을 제공 (기존에는 final + private constructor)
- Java에는 연산자 오버로딩이 없음

<MyLinks>

[Static method in Interface in Java](https://www.geeksforgeeks.org/static-method-in-interface-in-java)

[Static and Default Methods in Interfaces in Java](https://www.baeldung.com/java-static-default-methods)

[What is the purpose of a static method in interface from Java 8?](https://stackoverflow.com/questions/45780952/what-is-the-purpose-of-a-static-method-in-interface-from-java-8)

</MyLinks>

<!--
다른 언어에서 정적추상멤버 혹은 일반수학과 같은 개념을 찾아보았습니다.

자바는 자바8에서 기본메소드와 정적메소드를 모두 도입했는데요

이름만 비슷할뿐 매우 달랐습니다.

자바의 정적메소드는 C#에서 유틸 함수들을 정적 클래스에 담는 목적과 같습니다.

항상 인터페이스 내에서 구현을 포함해야하고 인터페이스 타입을 통해서만 호출할 수 있습니다.

그리고 자바는 연산자 오버로딩이 없습니다.
-->

---

# <logos-scala /> Scala: method, implicit, trait

연산자가 정적 메소드일 필요가 없다는 것이 큰 차이점, 그리고 implicit이 scala에서 코드 축약의 핵심?

- Scala는 인스턴스 메소드가 연산자로서 동작
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

<!--
스칼라의 경우 일반 수학과 비슷한 기능을 타입클래스 패턴이라는 것을 통해 제공하고 있습니다.

C#과의 차이점으로는 스칼라는 인스턴스의 메소드가 연산자로서 동작하고 있어 C#이 정적 메소드인것과 차이가 있고요

공통점으로는 암시적 타입변환을 제공하고 있습니다. 다만 스칼라의 경우 implicit 키워드가 타입변환뿐 아니라 함수 인자의 기본값 제공도 포함하고 있어 처음보면 혼란스러운 경우가 있고요

이러한 기능들을 혼합해서 만든것이 타입클래스 패턴인데, 코드만 보자면, 해당 타입의 코드는 전혀 확장하지 않고, 확장부 코드를 따로 작성하는 것이 특징입니다.

관심있으시면 링크에 더 많은 내용이 있으니 확인바랍니다.
-->

---

# <logos-fsharp /> F#: Statically Resolved Type Parameters

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

fsi> add "123" "abc" ;;
val it: string = "123abc"
```

- 타입 파라미터가 특정 멤버를 가지게 제약할 수 있음
- C++의 Function Template과 비슷하지만 [인라인 함수](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/inline-functions)에만 쓸 수 있음

<MyLinks>

[Statically Resolved Type Parameters](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/generics/statically-resolved-type-parameters)

[[ C++ ] 함수 템플릿(Function Template)과 템플릿 함수(Template Function)](https://musket-ade.tistory.com/entry/C-%ED%95%A8%EC%88%98-%ED%85%9C%ED%94%8C%EB%A6%BFFunction-Template%EA%B3%BC-%ED%85%9C%ED%94%8C%EB%A6%BF-%ED%95%A8%EC%88%98Template-Function)

</MyLinks>

<!--
닷넷의 또다른 언어인 F#의 경우 스태티컬리 리졸브드 타입 파라미터, 해석해보면 정적으로 결정되는 타입 파라미터 기능을 가지고 있는데요.

간단히만 말씀드리면 인자 파라미터의 제약을 타입이 아니라, 특정 멤버를 가지는 여부에 따라 제약하고, 컴파일 시점에는 제네릭이 아니라 사용된 타입에 따라 실제 타입이 정해지는 기능입니다.

인라인 함수라고 하는, 실제로는 함수를 호출하는게 아닌 코드로 대치되는 기능 안에서만 쓸 수 있기 때문에 가해진 제약이라고도 볼 수 있겠네요.
-->

---

# `double` in .net6.0 <fluent-emoji-vs-button /> .net7.0

<div class="flex flex-row w-full gap-4" >

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

<v-click>
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
</v-click>

</div>

<!--
네 다른 언어의 비슷한 기능을 알아 봤고, 다음부터는 F# RFC 문서에서 지적한 우려사항들을 알아보겠습니다.

그전에, 닷넷6와 닷넷7 사이에 double 타입의 달라진점을 확인해보겠습니다.

왼쪽에 있는 것이 닷넷6의 더블이고 네 오른쪽에 나타난 것이 닷넷7의 더블입니다.

오, 이게 뭐지. 네 이 차이점을 한번 보시고 우려사항들을 보시면 좀 마음에 와닿으실 것 같습니다.
-->

---

# <fluent-emoji-fire /> 1. 최대 추상화 충동을 유발

- static abstract members와 일반 수학은 공통적으로
  - 더 많은 추상화를 통해
  - 더 적은 코드로 더 넓은 범위를 커버하여
  - 재사용성의 증가

<br />

- 재사용성의 증가는
  - 매우 매력있고
  - 합당해보이나

<br />

- 실제로는
  - 재사용되는 양은 극히 적으며
  - 굉장한 시간의 낭비이며
  - 라이브러리 사용자로 하여금 학습과 사용에 복잡성을 높인다

<!--
(읽기)
-->

---

# <fluent-emoji-fire /> 2. 마이크로 인터페이스의 확산과 후속 요구

- 일반 수학의 표현법과 활용을 다른 분야에서 적용하길 바랄 것이고
  - 그 분야(라이브러리, 프레임워크)들은 최대 추상화를 구현하기 위한 리소스를 소모해야할 것

<br />

- 인터페이스는 점점 세분화될 것이며
  - 나누어진 인터페이스를 이해하는 것은 사용자 개개인의 시간과 노력으로 지불됨

<!--
(읽기)
-->

---

# <fluent-emoji-fire /> 3. 끊나지 않는 **적합한** 일반화 지점 찾기

- 추상화의 정도는 절대로 적합한 지점을 찾을 수 없으며
- 항상 비생산적인 논쟁을 불러일으킬 것이며
- 소프트웨어 엔지니어링의 다른 합리적인 목표가 무시될 위험이 있다

<!--
(읽기)
-->

---

# <fluent-emoji-no-entry /> A. 타입 제약이 아닌 타입으로 사용

하면 안됨

```csharp{1-3,8}
// The type 'T' cannot be used as type parameter 'TSelf' in the generic type or method 'INumber<TSelf>'.
// There is no boxing conversion or type parameter conversion from 'T' to 'System.Numerics.INumber<T>'.
// csharp(CS0314)

// Operator '+' cannot be applied to operands of type 'INumber<T>' and 'INumber<T>'
// csharp(CS0019)

public static INumber<T> Add<T>(INumber<T> left, INumber<T> right) => left + right; // 💥error
```

```csharp{1,4}
interface INumber<TSelf> : ... IAdditionOperators<TSelf, TSelf, TSelf> ... where TSelf : INumber<TSelf>
{}

interface IAdditionOperators<TSelf, TOther, TResult> where TSelf : IAdditionOperators<TSelf, TOther, TResult>?
{
  static abstract TResult operator +(TSelf left, TOther right);
}

```

- `INumber`의 `T`가 가져야하는 타입 제약 `TSelf: INumber<TSelf>` 을 만족하지 못하기 때문
- 모르면 맞는 규칙 추가 _(`TSelf`는 어디서 나온건데)_

<!--
앞서 일반적인 우려사항을 알아봤고,여기서부터는 코드 상에서의 우려사항을 알아보겠습니다.

오케이 각 기능별로 인터페이스를 나누고 그 타입을 구현한 것끼리 계산할 수 있단 말이지? 라고 해서 위와 같이 코드를 작성하면 에러가 납니다.

자 조금 머리가 아파지는데요, INumber의 타입 파라미터 T는 where TSelf: INumber<TSelf> 라는 타입 제약을 지켜야하는데, T 자체는 제약이 없으니 그걸 만족하지 못하는겁니다.

라고 말해야지 이해가 될까말까하죠. 실제 오류 메시지를 보면 내 코드에는 TSelf에도 없는데 뭐라는거야 싶을 겁니다.

그래서 실제로는 다음과 같이 (페이지 넘김)
-->

---

# <fluent-emoji-no-entry /> A. 타입 제약이 아닌 타입으로 사용하면 안됨

```csharp
public static INumber<T> Add<T>(T left, T right) where T : INumber<T> => left + right; // ⭕ work!
```

<br />

```csharp
void getSizeAtAge(IAnimal t1, int age)
{
  // A static virtual or abstract interface member can be accessed only on a type parameter.
  // csharp(CS8926)
  var sizeAtAge = IAnimal.SizeAtAge(age); // 💥error
}

void getSizeAtAge<T>(T t1, int age) where T : IAnimal
{
  var sizeatage = T.SizeAtAge(age); // ⭕ work!
}
```

- 복잡한 인터페이스 선언이 아니더라도 타입 파라미터를 통해서만 접근이 가능하도록 설정되어 있음

<!--
위와 같이 타입 제약으로서 활용을 하셔야합니다.

그리고 위와같이 타입 파라미터를 받는 제네릭 인터페이스가 아니더라도 타입 자체로 활용하면 아래와 같은 에러가 발생하므로, 타입 제약으로써만 사용하셔야합니다.

이러한 미묘한 사용법의 제약이 초보자나, 해당 라이브러리에 익숙하지 않은 초보자로 하여금 장벽을 만든다는 우려사항이 있었습니다.
-->

---

# <fluent-emoji-no-entry /> B. 고차함수가 더 간단하고 일반적일 수 있음

```fsharp
type ISomeFunctionality<'T when 'T :> ISomeFunctionality<'T>> =
    static abstract DoSomething: 'T -> 'T

let SomeGenericThing<'T when 'T :> ISomeFunctionality<'T>> (arg: 'T) =
    //...
    'T.DoSomething(arg)
    //...

type MyType1() =
    interface ISomeFunctionality<MyType1> with
        static member DoSomething(x) = ...

type MyType2() =
    static member DoSomethingElseDifference(x) = ...

SomeGenericThing<MyType1> arg1
SomeGenericThing<MyType2> arg2 // oh no, MyType2 doesn't have the interface! Stuck!
```

- 정적멤버인터페이스를 구현한 타입 안에서의 일반화

<!--
다음은 고차함수를 활용하는 것이 더 일반적일 수 있다라는 우려사항입니다.

어 이게 뭐지? 네 F# 코드인데요, 간단해서 그냥 가져왔습니다.

일반수학이 가져오는 적은 코드와 넓어진 적용 범위는 해당 인터페이스를 구현한 타입들을 통해서 지원되는 것입니다.

만약 인터페이스를 지원하지 않은 타입이 있다면 그 타입은 지원되지 않겠죠.
-->

---

# <fluent-emoji-no-entry /> B. 고차함수가 더 간단하고 일반적일 수 있음

```fsharp
fsi> let SomeGenericThing doSomething arg =
       doSomething arg

val SomeGenericThing: doSomeThing: ('a -> 'b) -> arg: 'a -> 'b
```

```fsharp
type MyType1 =
    static member DoSomething(x) = ...

type MyType2 =
    static member DoSomethingElse(x) = ...

SomeGenericThing MyType1.DoSomething arg1
SomeGenericThing MyType2.DoSomethingElse arg2
```

- 함수 전달이 더 짧은 구현이며 더 일반적임
- 추상화 메소드를 구현한 수가 10개 미만이라면, 고차함수를 사용하라

<!--
타입 파라미터 a를 넣으면 b가 나오는, 사실상 타입 관계없이 인풋하나 아웃풋 하나 나오는 함수 인자와 함수의 인풋타입과 같은 인자만 넣으면 서로 적용시켜주는 함수를 만들고

각 타입은 인터페이스를 구현하지 않고 정적메소드로 가지고 있다가

고계함수로 넘겨주기만 하라는 겁니다.

이 구현이 더 짧으면서도 더 적용범위가 넓다는 것이죠.

문서에는 추상메소드를 구현한 수가 많지 않다면 명시적인 함수와 고차함수를 사용하는 것이 더 좋을것이라고 설명하고 있습니다.
-->

---

# <fluent-emoji-no-entry /> C. 닫힌 연산에만 사용하세요

- 정적 메소드가 가지는 한계
  - 계산에 영향을 주는 정보는 파라미터 안에만 존재
    - 암시적인 컨텍스트 (인스턴스 멤버)
    - 전역 상태를 쉽게 사용할 수 없음
  - 특정 정보와 함께 인스턴스화 될 수 없음 (`ParserV1`, `ParserV2`)

<br />

- 한계로 인한 협소한 적용 범위
  - [연산이 내부적으로 닫혀 있으며](http://wanochoi.com/?p=5061)
  - 구현 내용에 반박의 여지가 _(미래에도)_ 없는
  - 영역에서만 사용해야한다

<br />

**요구사항의 변경이 존재하거나 컨텍스트에 의존해야하는 도메인 영역에서는 절대 사용하지 말 것**

<!--
일반 수학의 장점은 연산자 오버로딩을 통한 수학적 연산의 표현에도 있는데요

C#, 닷넷에서 연산자는 정적메소드이기 때문에 가지는 태생적인 한계가 있습니다.

정적 메소드가 연산에 사용할 정보들은 대부분 파라미터 안에만 존재하며,

컨텍스트를 활용하기에는 인스턴스 멤버의 필드는 당연히 사용할 수 없고, 전역 상태인 static 필드를 활용하기에는 대단히 위험하며, 각 조건별로 상속도 할 수 없다는 것입니다.

그로 인해서 일반 수학은 시간과 상태의 조건에 따른 변경에 대해서 무방비적일수밖에 없는데, 그로 인해 적용 범위를 한계적으로 가져야한다고 문서에서 설명합니다.

내부적으로 갇혀있고 구현 내용의 변경의 여지가 없어야하는 영역에서만 활용해야한다고 말이죠.
-->

---

# <fluent-emoji-check-mark /> [F# RFC의 지침](https://github.com/fsharp/fslang-design/blob/main/FSharp-7.0/FS-1124-interfaces-with-static-abstract-members.md#guidance)

- static abstract members가 가지는 고유의 제약을 이해하세요
- 유형 분류 & 최대 추상화 충동에 지지마세요
- 논쟁과 변경의 여지가 없는 형식에만 사용하세요
- 정적 인터페이스와 비정적 인터페이스를 섞어쓰지마세요

<!--
그래서 문서의 내용을 마무리하자면 아래와 같습니다.

(읽기)
-->

---

# <fluent-emoji-thought-balloon /> 들은 생각

<span />

<ul>

- 작성하기도, 쓰기도, 쉽지 않은 기능
- 함께 일하기

  1. 이견이 없는 일반 수학 라이브러리를 만들 수 있을까?
  2. 만들어진 라이브러리(복잡성의 증가)를 팀이 이해하고 활용해줄 수 있을까?

- .NET7은 LTS가 아닌데 .NET8가 나올 시점에는 커뮤니티에서 어떻게 사용되고 있을까

- Java랑 C# 볼수록 안 비슷하다...

<br />

<details>
<summary>이젠 <a target="_blank" href="https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/">AOT 컴파일</a>인가?  </summary>

> .NET은 역사적으로 이 공간을 피했습니다. Anders Hejlsberg가 C#2.0에서 의도적으로 결정한 것으로, 복잡성 대비 이점을 이유로 제안을 거부했습니다.이 RFC(Don Syme)의 초기 작성자가 참여하고 동의한 결정입니다.
>
> 모든 리플렉션 코드가 이제 정적 컴파일 시나리오에서 더 비싼 것으로 간주된 상황에서, 제한된 제네릭이 없는 경우 리플렉션이 자주 사용되었기 때문에 C#11 및 .NET 6/7은 이 결정을 수정했습니다.

</details>

<br />

<details>
<summary>닷넷에도 타입 논쟁이? </summary>

> 특히 추상 수학에 입문한 사람들이 그러한 수학적 계층 구조를 좋아하는 것처럼 보이며, 불나방처럼 이끌립니다.

</details>

</ul>

<!--
(읽기)
-->

---

# <fluent-emoji-white-question-mark /> 남은 의문

- IWSAM implementations are static
  - 제네릭을 사용하지만 정적인 구현?

<!--
(읽기)
-->
