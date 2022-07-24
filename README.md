# MicroMonads

MicroMonads provides simple Monads for use in .Net projects.

## Featured Monads

1. Maybe
2. Carry (also known as Log)

## Installation

Nuget Gallery: https://www.nuget.org/packages/RhoMicro.MicroMonads

.Net CLI: `dotnet add package RhoMicro.MicroMonads --version 1.0.0`

Package Manager: `Install-Package RhoMicro.MicroMonads -Version 1.0.0`

## How To Use `Maybe<T>`

`Maybe<T>` encapsulates the notion of a value possibly existing (*maybe* it's there). It can be either just a value or nothing. This means that operations *using* `Maybe<T>` should also return `Maybe<T>` in case one of their operands is nothing. This differs from the notion of `null`, as operations on `Maybe<T>` will not throw exceptions, but instead return `Maybe<T>`.

### Unit Functions

Instantiate a new `Maybe<T>` using the unit functions `Maybe<T>.Unit`, `Maybe<T>.Just` or `Maybe<T>.Nothing`

```cs
Maybe<Int32> a = Maybe<Int32>.Unit(valueFactory: () => 5, hasValue: true);
Maybe<Int32> b = Maybe<Int32>.Unit(valueFactory:() => 3, hasValue: false);
```
*which is equivalent to*
```cs
Maybe<Int32> a = Maybe<Int32>.Just(value: 5);
Maybe<Int32> b = Maybe<Int32>.Nothing();
```

Note that `Maybe<T>.Nothing` will not initialize `Maybe<T>.Value`.

### Usage

Construct a function that returns a `Maybe<T>`:

```cs
Func<Int32, Int32, Maybe<Int32>> safeDivision = (x, y) => Maybe<Int32>.Unit(() => x/y, y != 0);

Maybe<Int32> result = safeDivision.Invoke(12, 3);
Console.WriteLine(result); //Output: "{Value="Just 4"}"

result = safeDivision(12, 0);
Console.WriteLine(result); //Output: "{Value="Nothing"}"
```

### Bind Function

Use the `Maybe<T>.Bind` function to conditionally operate on a wrapped value:

```cs
Function<Int32, Maybe<Int32>> addTen = v => Maybe<Int32>.Just(v + 10);

Maybe<Int32> just12 = Maybe<Int32>.Just(12);

Maybe<Int32> sum = just12.Bind(addTen);
Console.WriteLine(sum); //Output: "{Value="Just 22"}"

Maybe<Int32> nothing = Maybe<Int32>.Nothing();

sum = nothing.Bind(addTen);
Console.WriteLine(sum); //Output: "{Value="Nothing"}"
```
### Helper Class

Using the helper class `Maybe`, you can reduce the verbosity of your code.

```cs
using static MicroMonads.Maybe;

Maybe<Int32> just12 = Just<Int32>(12);
Maybe<Int32> nothing = Nothing<Int32>();
```
