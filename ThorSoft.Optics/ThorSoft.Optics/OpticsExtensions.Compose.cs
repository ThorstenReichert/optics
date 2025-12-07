namespace ThorSoft.Optics
{
    public partial class OpticsExtensions
    {
        /// <summary>
        ///     Composes a lens with an iso to create a new lens.
        /// </summary>
        /// <typeparam name="T">The source type of the lens.</typeparam>
        /// <typeparam name="U">The source type of the lens and the target type of the iso.</typeparam>
        /// <typeparam name="V">The target type of the iso.</typeparam>
        /// <param name="source">The lens instance.</param>
        /// <param name="other">The iso instance.</param>
        /// <returns>
        ///     A lens with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that first focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Lens<T, V> Compose<T, U, V>(this Lens<T, U> source, Iso<U, V> other) =>
            new(
                instance => other.DirectMap(source.Get(instance)),
                (x, instance) => source.Set(other.InverseMap(x), instance));

        /// <summary>
        ///     Composes two compatible lenses to create a new lens.
        /// </summary>
        /// <typeparam name="T">The source type of the first lens.</typeparam>
        /// <typeparam name="U">The source type of the first lens and the target type of the second lens.</typeparam>
        /// <typeparam name="V">The target type of the second lens.</typeparam>
        /// <param name="source">The first lens instance.</param>
        /// <param name="other">The second lens instance.</param>
        /// <returns>
        ///     A lens with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that first focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Lens<T, V> Compose<T, U, V>(this Lens<T, U> source, Lens<U, V> other)
        {
            return new Lens<T, V>(
                instance => other.Get(source.Get(instance)),
                (value, instance) => source.Set(other.Set(value, source.Get(instance)), instance));
        }

        /// <summary>
        ///     Composes a lens with an optional to create a new optional.
        /// </summary>
        /// <typeparam name="T">The source type of the lens.</typeparam>
        /// <typeparam name="U">The source type of the optional and the target type of the lens.</typeparam>
        /// <typeparam name="V">The target type of the optional.</typeparam>
        /// <param name="source">The lens instance.</param>
        /// <param name="other">The optional instance.</param>
        /// <returns>
        ///     An optional with source <typeparamref name="T"/> and target <typeparamref name="V"/> 
        ///     that first focuses first through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Optional<T, V> Compose<T, U, V>(this Lens<T, U> source, Optional<U, V> other) =>
            new(
                instance => other.Get(source.Get(instance)),
                (x, instance) =>
                {
                    var outerValue = source.Get(instance);
                    if (other.Get(outerValue).TryGetValue(out var innerValue))
                    {
                        return source.Set(other.Set(x, outerValue), instance);
                    }
                    else
                    {
                        return instance;
                    }
                });

        /// <summary>
        ///     Composes two compatible optionals to create a new optional.
        /// </summary>
        /// <typeparam name="T">The source type of the first optional.</typeparam>
        /// <typeparam name="U">The target type of the first optional and the target type of the second optional.</typeparam>
        /// <typeparam name="V">The target type of the second optional.</typeparam>
        /// <param name="source">The first optional instance.</param>
        /// <param name="other">The second optional instance.</param>
        /// <returns>
        ///     An optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that first focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Optional<T, V> Compose<T, U, V>(this Optional<T, U> source, Optional<U, V> other) =>
            new(
                instance => source.Get(instance).TryGetValue(out var inner)
                    ? other.Get(inner)
                    : Maybe.None<V>(),
                (x, instance) =>
                {
                    if (source.Get(instance).TryGetValue(out var outerValue)
                        && other.Get(outerValue).TryGetValue(out var innerValue))
                    {
                        return source.Set(other.Set(x, outerValue), instance);
                    }
                    else
                    {
                        return instance;
                    }
                });

        /// <summary>
        ///     Composes an optional with a lens to create a new optional.
        /// </summary>
        /// <typeparam name="T">The source type of the optional.</typeparam>
        /// <typeparam name="U">The target type of the optional and the target type of the lens.</typeparam>
        /// <typeparam name="V">The target type of the lens.</typeparam>
        /// <param name="source">The optional instance.</param>
        /// <param name="other">The lens instance.</param>
        /// <returns>
        ///     An optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that first focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Optional<T, V> Compose<T, U, V>(this Optional<T, U> source, Lens<U, V> other) =>
            new(
                instance => source.Get(instance).TryGetValue(out var inner)
                    ? Maybe.Some(other.Get(inner))
                    : Maybe.None<V>(),
                (x, instance) =>
                {
                    if (source.Get(instance).TryGetValue(out var outerValue))
                    {
                        return source.Set(other.Set(x, outerValue), instance);
                    }
                    else
                    {
                        return instance;
                    }
                });

        /// <summary>
        ///     Composes an optional with an iso to create a new optional.
        /// </summary>
        /// <typeparam name="T">The source type of the optional.</typeparam>
        /// <typeparam name="U">The target type of the optional and the target type of the iso.</typeparam>
        /// <typeparam name="V">The target type of the iso.</typeparam>
        /// <param name="source">The optional instance.</param>
        /// <param name="other">The iso instance.</param>
        /// <returns>
        ///     An optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Optional<T, V> Compose<T, U, V>(this Optional<T, U> source, Iso<U, V> other) =>
            new(
                instance => source.Get(instance).TryGetValue(out var inner)
                    ? Maybe.Some(other.DirectMap(inner))
                    : Maybe.None<V>(),
                (x, instance) => source.Set(other.InverseMap(x), instance));

        /// <summary>
        ///     Composes two compatible isos to create a new iso.
        /// </summary>
        /// <typeparam name="T">The source type of the first iso.</typeparam>
        /// <typeparam name="U">The target type of the first iso and the target type of the second iso.</typeparam>
        /// <typeparam name="V">The target type of the second iso.</typeparam>
        /// <param name="source">The first iso instance.</param>
        /// <param name="other">The second iso instance.</param>
        /// <returns>
        ///     An iso with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that first focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Iso<T, V> Compose<T, U, V>(this Iso<T, U> source, Iso<U, V> other) =>
            new(
                x => other.DirectMap(source.DirectMap(x)),
                x => source.InverseMap(other.InverseMap(x)));

        /// <summary>
        ///     Composes an iso with a lens to create a new lens.
        /// </summary>
        /// <typeparam name="T">The source type of the iso.</typeparam>
        /// <typeparam name="U">The target type of the iso and the target type of the lens.</typeparam>
        /// <typeparam name="V">The target type of the lens.</typeparam>
        /// <param name="source">The iso instance.</param>
        /// <param name="other">The lens instance.</param>
        /// <returns>
        ///     A lens with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Lens<T, V> Compose<T, U, V>(this Iso<T, U> source, Lens<U, V> other) =>
            new(
                instance => other.Get(source.DirectMap(instance)),
                (x, instance) => source.InverseMap(other.Set(x, source.DirectMap(instance))));

        /// <summary>
        ///     Composes an iso with an optional to create a new lens.
        /// </summary>
        /// <typeparam name="T">The source type of the iso.</typeparam>
        /// <typeparam name="U">The target type of the iso and the target type of the optional.</typeparam>
        /// <typeparam name="V">The target type of the optional.</typeparam>
        /// <param name="source">The iso instance.</param>
        /// <param name="other">The optional instance.</param>
        /// <returns>
        ///     An optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static Optional<T, V> Compose<T, U, V>(this Iso<T, U> source, Optional<U, V> other) =>
            new(
                instance => other.Get(source.DirectMap(instance)),
                (x, instance) => source.InverseMap(other.Set(x, source.DirectMap(instance))));

        /// <summary>
        ///     Composes a bound lens with an iso to create a new bound lens.
        /// </summary>
        /// <typeparam name="T">The source type of the bound lens.</typeparam>
        /// <typeparam name="U">The target type of the bound lens and the target type of the iso.</typeparam>
        /// <typeparam name="V">The target type of the iso.</typeparam>
        /// <param name="source">The bound lens instance.</param>
        /// <param name="other">The iso instance.</param>
        /// <returns>
        ///     A bound lens with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundLens<T, V> Compose<T, U, V>(this BoundLens<T, U> source, Iso<U, V> other) =>
            new(
                source.Lens.Compose(other),
                source.Instance);

        /// <summary>
        ///     Composes a bound lens with another lens to create a new bound lens.
        /// </summary>
        /// <typeparam name="T">The source type of the bound lens.</typeparam>
        /// <typeparam name="U">The target type of the bound lens and the target type of the other lens.</typeparam>
        /// <typeparam name="V">The target type of the other lens.</typeparam>
        /// <param name="source">The bound lens instance.</param>
        /// <param name="other">The other lens instance.</param>
        /// <returns>
        ///     A bound lens with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundLens<T, V> Compose<T, U, V>(this BoundLens<T, U> source, Lens<U, V> other)
        {
            return new BoundLens<T, V>(
                source.Lens.Compose(other),
                source.Instance);
        }

        /// <summary>
        ///     Composes a bound lens with an optional to create a new bound optional.
        /// </summary>
        /// <typeparam name="T">The source type of the bound lens.</typeparam>
        /// <typeparam name="U">The target type of the bound lens and the target type of the optional.</typeparam>
        /// <typeparam name="V">The target type of the optional.</typeparam>
        /// <param name="source">The bound lens instance.</param>
        /// <param name="other">The optional instance.</param>
        /// <returns>
        ///     A bound optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundOptional<T, V> Compose<T, U, V>(this BoundLens<T, U> source, Optional<U, V> other) =>
            new(
                source.Lens.Compose(other),
                source.Instance);

        /// <summary>
        ///     Composes a bound optional with an iso to create a new bound optional.
        /// </summary>
        /// <typeparam name="T">The source type of the bound optional.</typeparam>
        /// <typeparam name="U">The target type of the bound optional and the target type of the iso.</typeparam>
        /// <typeparam name="V">The target type of the iso.</typeparam>
        /// <param name="source">The bound optional instance.</param>
        /// <param name="other">The iso instance.</param>
        /// <returns>
        ///     A bound optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundOptional<T, V> Compose<T, U, V>(this BoundOptional<T, U> source, Iso<U, V> other) =>
            new(
                source.Optional.Compose(other),
                source.Instance);

        /// <summary>
        ///     Composes a bound optional with a lens to create a new bound optional.
        /// </summary>
        /// <typeparam name="T">The source type of the bound optional.</typeparam>
        /// <typeparam name="U">The target type of the bound optional and the target type of the lens.</typeparam>
        /// <typeparam name="V">The target type of the lens.</typeparam>
        /// <param name="source">The bound optional instance.</param>
        /// <param name="other">The lens instance.</param>
        /// <returns>
        ///     A bound optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundOptional<T, V> Compose<T, U, V>(this BoundOptional<T, U> source, Lens<U, V> other) =>
            new(
                source.Optional.Compose(other),
                source.Instance);

        /// <summary>
        ///     Composes a bound optional with another optional to create a new bound optional.
        /// </summary>
        /// <typeparam name="T">The source type of the bound optional.</typeparam>
        /// <typeparam name="U">The target type of the bound optional and the target type of the other optional.</typeparam>
        /// <typeparam name="V">The target type of the other optional.</typeparam>
        /// <param name="source">The bound optional instance.</param>
        /// <param name="other">The other optional instance.</param>
        /// <returns>
        ///     A bound optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundOptional<T, V> Compose<T, U, V>(this BoundOptional<T, U> source, Optional<U, V> other) =>
            new(
                source.Optional.Compose(other),
                source.Instance);

        /// <summary>
        ///     Composes a bound iso with another iso to create a new bound iso.
        /// </summary>
        /// <typeparam name="T">The source type of the bound iso.</typeparam>
        /// <typeparam name="U">The target type of the bound iso and the target type of the other iso.</typeparam>
        /// <typeparam name="V">The target type of the other iso.</typeparam>
        /// <param name="source">The bound iso instance.</param>
        /// <param name="other">The other iso instance.</param>
        /// <returns>
        ///     A bound iso with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundIso<T, V> Compose<T, U, V>(this BoundIso<T, U> source, Iso<U, V> other) =>
            new(
                source.Iso.Compose(other),
                source.Instance);

        /// <summary>
        ///     Composes a bound iso with a lens to create a new bound lens.
        /// </summary>
        /// <typeparam name="T">The source type of the bound iso.</typeparam>
        /// <typeparam name="U">The target type of the bound iso and the target type of the lens.</typeparam>
        /// <typeparam name="V">The target type of the lens.</typeparam>
        /// <param name="source">The bound iso instance.</param>
        /// <param name="other">The lens instance.</param>
        /// <returns>
        ///     A bound lens with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundLens<T, V> Compose<T, U, V>(this BoundIso<T, U> source, Lens<U, V> other) =>
            new(
                source.Iso.Compose(other),
                source.Instance);

        /// <summary>
        ///     Composes a bound iso with an optional to create a new bound optional.
        /// </summary>
        /// <typeparam name="T">The source type of the bound iso.</typeparam>
        /// <typeparam name="U">The target type of the bound iso and the target type of the optional.</typeparam>
        /// <typeparam name="V">The target type of the optional.</typeparam>
        /// <param name="source">The bound iso instance.</param>
        /// <param name="other">The optional instance.</param>
        /// <returns>
        ///     A bound optional with source <typeparamref name="T"/> and target <typeparamref name="V"/>
        ///     that focuses through <paramref name="source"/> then through <paramref name="other"/>.
        /// </returns>
        public static BoundOptional<T, V> Compose<T, U, V>(this BoundIso<T, U> source, Optional<U, V> other) =>
            new(
                source.Iso.Compose(other),
                source.Instance);
    }
}
