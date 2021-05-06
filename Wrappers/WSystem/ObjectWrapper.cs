using System;

namespace CustomLevelProgression.Wrappers.WSystem
{
    public class ObjectWrapper
    {
        protected object obj;
        public object WrappedObj => obj;

        public ObjectWrapper(object obj)
        {
            this.obj = obj;
            this.obj.ToString();
        }

        /// <summary>
        /// Returns the type of the object this wrapper wraps.
        /// </summary>
        /// <returns>The type of object this wrapper wraps.</returns>
        public virtual Type GetWrappedType() => this.obj.GetType();
        /// <summary>
        /// Returns whether the given object equals the wrapped object.
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>whether the given object equals the wrapped object.</returns>
        public bool WrappedEquals(object obj)
        {
            return this.obj.Equals(obj);
        }

        /// <summary>
        /// Returns the hash code of the wrapped object.
        /// </summary>
        /// <returns>The hash code of the wrapped object.</returns>
        public int GetWrappedHashCode()
        {
            return this.obj.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of the wrapped object.
        /// </summary>
        /// <returns>The string representation of the wrapped object.</returns>
        public string WrappedToString()
        {
            return this.obj.ToString();
        }
    }
}