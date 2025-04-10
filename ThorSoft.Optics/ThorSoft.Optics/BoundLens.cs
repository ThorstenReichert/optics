namespace ThorSoft.Optics
{
    public readonly struct BoundLens<T, U>
    {
        public BoundLens(Lens<T, U> lens, T instance)
        {
            Lens = lens;
            Instance = instance;
        }

        public Lens<T, U> Lens { get; }
        public T Instance { get; }
        public U Get() => Lens.Get(Instance);
        public T Set(U value) => Lens.Set(value, Instance);
    }
}
