using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryProject
{
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            var isLeftNull = left is null;
            var isRightNull = right is null;

            if (isLeftNull && isRightNull)
            {
                return true;
            }

            if (isLeftNull || isRightNull)
            {
                return false;
            }

            return left!.Equals(right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

        public override bool Equals(object? obj)
        {
            var isObjNullOrDifferentType = GetType() != obj?.GetType();

            if (isObjNullOrDifferentType)
            {
                return false;
            }

            var valueObject = (ValueObject)obj!;
            var valueObjectEqualityComponents = valueObject.GetEqualityComponents();

            return GetEqualityComponents().SequenceEqual(valueObjectEqualityComponents);
        }

        public override int GetHashCode()
        {
            var hash = default(HashCode);

            foreach (var component in GetEqualityComponents())
            {
                hash.Add(component);
            }

            return hash.ToHashCode();
        }

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
