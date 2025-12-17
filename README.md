[![publish](https://github.com/ThorstenReichert/optics/actions/workflows/pipeline.yml/badge.svg)](https://github.com/ThorstenReichert/optics/actions/workflows/pipeline.yml)

# Optics

Optics refers to a class of utilities aimed at simplifying handling of immutable data structures,
such as record types.

# Lenses

## Motivation

A lens is a particular type of optic, that is designed for manipulating nested, immutable data structures.
Conceptually, a lens is just a pair of a "getter" and "setter" methods.
```csharp
public readonly record struct Lens<T,U>(Func<T,U> Get, Func<U,T,T> Set);
```
Typically, these methods refer to a property
on a record type, in which case the accompanying lens can be defined thus:
```csharp
record A(int Prop1, string Prop2);

var lens = new Lens<A, string>(
	static x => x.Prop2,
	static (v,x) => x with { Prop2 = v });
```
wherein the first lambda method returns the value of `Prop2` on `A` and the second lambda creates a copy of an instance 
of `A` with the value of `Prop2` updated to `v`.

What makes lenses particularly useful is their composability. That is given a lens `Lens<A,B>` and another lens `Lens<B,C>`,
the two lenses can be composed into a lens `Lens<A,C>` as such
```csharp
var lensAB = ...;
var lensBC = ...;

var lensAC = new Lens<A,C>(
	x => lensBC.Get(lensAB.Get(x)),
	(v,x) => lensAB.Set(lensBC.Set(v, lensAB.Get(x)), x);
```
The value of this kind of composition becomes apparent when dealing with deeply nested, immutable record types, 
especially when updating a deeply nested property, such as
```csharp
record A(B PropB);
record B(C PropC);
record C(string Prop);

A instance = ...;
A updatedA = instance with 
{
	PropB = instance.PropB with 
	{
		PropC = instance.PropB.PropC with 
		{
			Prop = "my new value"
		}
	}
};
```
Given instead the three appropriate lenses, the update can be performed via
```csharp
var lensAB = ...;
var lensBC = ...;
var lensCS = ...;

A instance = ...;
A updatedA = lensAB.Compose(lensBC).Compose(lensCS).Set("my new value", instance);
```

## Source Generation

While this package does provide the base lens types and extension methods to operate on them,
the more interesting part are the accompanying source-generators. While composing lenses is straightforward,
creating the component lenses in the first part requires a fair bit of boilerplate code, which can be 
mitigated by using source generation.

The simplest form of source-generation is provided via the `[FocusProperties]` attribute, applicable
to record types themselves. 
```csharp
[FocusProperties]
public partial record A(int Prop1, string Prop2);
```

The source generator will then generate code, to enable using the types properties
from within a special `Focus` method to generate lenses:
```csharp
Lens<A, int> lensProp1 = Optics.Focus<A>(a => a.Prop1);
```

The `Focus` method also allows to directly generate nested properties, as long as all participating types
are annotated with `[FocusProperties]`, such as
```csharp
[FocusProperties]
record A(B PropB);

[FocusProperties]
record B(C PropC);

[FocusProperties]
record C(string Prop);
```

The lens focusing from `PropB` of `A` all the way down to `Prop` of `C` can then be generated via
```csharp
Lens<A, string> lens = Optics.Focus<A>(a => a.PropB.PropC.Prop);
```
