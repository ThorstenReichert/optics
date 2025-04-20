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

The simplest form of source-generation is provided via the `[GenerateLenses]` attribute, applicable
to record types themselves. The source-generator will then generate a nested class inside the attributed type
containing lenses for all directly defined properties on the type, such as
```csharp
public partial record A(int Prop1, string Prop2);

// Generated
partial record A
{
	public static class Lenses
	{
		public static Lens<A, int> Prop1 { get; } = ...;
		public static Lens<A, string> Prop2 { get; } = ...;
	}
}
```
With this generator, the aforementioned example could be rewritten via
```csharp
var lensAB = A.Lenses.PropB;
var lensBC = B.Lenses.PropC;
var lensCS = C.Lenses.Prop;

A instance = ...;
A updatedA = lensAB.Compose(lensBC).Compose(lensCS).Set("my new value", instance);
```

Another option for generating deeply nested lenses directly is via the `LensExtensions.Focus()` and `Lens<>.Focus()` methods.
Both accept a lambda-expression that is intended to determine the path through the nested properties, such as
```csharp
var lens = Lens<A>.Focus(a => a.PropB.PropC.Prop);
```
The source generator will intercept the call to `Focus()` and return the appropriate `Lens<A, string>`.
For more direct updates of single instances, the `LensExtensions.Focus()` method can similarly be invoced on any record type instance
```csharp
A instance = ...;
A updatedA = instance.Focus(a => a.PropB.PropC.Prop).Set("my new value");
```
Notice that the `Set` method did not reqire to specify the instance argument. Instead, the instance `Focus()` was invoked
on is propagated through `Focus()` and inserted into the `Set` call of the underlying lens directly.
