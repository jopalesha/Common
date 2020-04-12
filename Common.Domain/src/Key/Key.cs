﻿using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Domain
{
    public class Key<T> : IKey<T>
    {
        public Key(T value) : this()
        {
            Value = Check.NotNull(value);
        }

        protected Key() { }

        public static Key<T> Generated => new AutogeneratedKey<T>();

        public T Value { get; }
    }
}