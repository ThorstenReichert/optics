using System.Linq.Expressions;

namespace ThorSoft.Optics
{
    public static class LensExtensions
    {
        public static BoundLens<T, U> Focus<T, U>(this T instance, Expression<Func<T, U>> propertySelector)
        {
            throw new NotImplementedException("Method is only a marker to be intercepted via the accompanying source-generator");
        }

        public static Lens<T, V> Compose<T, U, V>(this Lens<T, U> source, Lens<U, V> nested)
        {
            return new Lens<T, V>(
                instance => nested.Get(source.Get(instance)),
                (value, instance) => source.Set(nested.Set(value, source.Get(instance)), instance));
        }

        public static BoundLens<T, V> Compose<T, U, V>(this BoundLens<T, U> source, Lens<U, V> nested)
        {
            return new BoundLens<T, V>(
                source.Lens.Compose(nested),
                source.Instance);
        }

        public static Lens<T, V> Compose<T, U, V>(this Lens<T, U> source, BoundLens<U, V> nested)
        {
            return source.Compose(nested.Lens);
        }

        public static BoundLens<T, V> Compose<T, U, V>(this BoundLens<T, U> source, BoundLens<U, V> nested)
        {
            return source.Compose(nested.Lens);
        }

        public static T Modify<T, U>(this Lens<T, U> source, Func<U, U> update, T instance)
        {
            return source.Set(update(source.Get(instance)), instance);
        }

        public static T Modify<T, U>(this BoundLens<T, U> source, Func<U, U> update)
        {
            return source.Set(update(source.Get()));
        }
    }
}
