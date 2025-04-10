using System.Linq.Expressions;

namespace ThorSoft.Optics
{
    public readonly struct Lens<T, U>
    {
        public Lens(Func<T, U> get, Func<U, T, T> set)
        {
            Get = get;
            Set = set;
        }

        public Func<T, U> Get { get; }
        public Func<U, T, T> Set { get; }
    }

    public static class Lens<T>
    {
        public static Lens<T, U> Focus<U>(Expression<Func<T, U>> propertySelector)
        {
            throw new NotImplementedException("Method is only a marker to be intercepted via the accompanying source-generator");
        }
    }
}
