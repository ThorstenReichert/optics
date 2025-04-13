﻿using System.Linq.Expressions;

namespace ThorSoft.Optics
{
    /// <summary>
    ///     A general lens allowing non-destructive mutations of immutable data structures.
    /// </summary>
    /// <typeparam name="T">The base type of the lens.</typeparam>
    /// <typeparam name="U">The property type of the lens.</typeparam>
    public readonly struct Lens<T, U>
    {
        /// <summary>
        ///     Creates a new <see cref="Lens{T, U}"/> instances with specified getter and setter.
        /// </summary>
        /// <param name="get">The getter of the lens.</param>
        /// <param name="set">The setter of the lens.</param>
        public Lens(Func<T, U> get, Func<U, T, T> set)
        {
            Get = get;
            Set = set;
        }

        /// <summary>
        ///     Returns the value of the property focused by the lens.
        /// </summary>
        public Func<T, U> Get { get; }

        /// <summary>
        ///     Creates a copy with the value of the focused property updated.
        /// </summary>
        public Func<U, T, T> Set { get; }
    }

    /// <summary>
    ///     Factory methods for <see cref="Lens{T, U}"/> instances of base type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The base type of the constructed lenses.</typeparam>
    public static class Lens<T>
    {
        /// <summary>
        ///     Generate a <see cref="Lens{T, U}"/> that focuses in on a nested property of <typeparamref name="T"/> 
        ///     through the path <paramref name="propertySelector"/>.
        /// </summary>
        /// <typeparam name="U">The type of the property selected through <paramref name="propertySelector"/>.</typeparam>
        /// <param name="propertySelector">The expression selecting the nested property to focus on.</param>
        /// <returns>
        ///     A <see cref="Lens{T, U}"/> focusing on the nested property selected by <paramref name="propertySelector"/>.
        /// </returns>
        public static Lens<T, U> Focus<U>(Expression<Func<T, U>> propertySelector)
        {
            throw new NotImplementedException("Method is only a marker to be intercepted via the accompanying source-generator");
        }
    }
}
