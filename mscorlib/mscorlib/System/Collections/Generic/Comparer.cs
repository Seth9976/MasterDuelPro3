using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	/// <summary>Provides a base class for implementations of the <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200076E RID: 1902
	[TypeDependency("System.Collections.Generic.ObjectComparer`1")]
	[Serializable]
	public abstract class Comparer<T> : IComparer, IComparer<T>
	{
		/// <summary>Returns a default sort order comparer for the type specified by the generic argument.</summary>
		/// <returns>An object that inherits <see cref="T:System.Collections.Generic.Comparer`1" /> and serves as a sort order comparer for type <paramref name="T" />.</returns>
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000E9C60 File Offset: 0x000E7E60
		public static Comparer<T> Default
		{
			get
			{
				Comparer<T> comparer = Comparer<T>.defaultComparer;
				if (comparer == null)
				{
					comparer = Comparer<T>.CreateComparer();
					Comparer<T>.defaultComparer = comparer;
				}
				return comparer;
			}
		}

		/// <summary>Creates a comparer by using the specified comparison.</summary>
		/// <returns>The new comparer.</returns>
		/// <param name="comparison">The comparison to use.</param>
		// Token: 0x06003C89 RID: 15497 RVA: 0x000E9C87 File Offset: 0x000E7E87
		public static Comparer<T> Create(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return new ComparisonComparer<T>(comparison);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000E9CA0 File Offset: 0x000E7EA0
		private static Comparer<T> CreateComparer()
		{
			RuntimeType runtimeType = (RuntimeType)typeof(T);
			if (typeof(IComparable<T>).IsAssignableFrom(runtimeType))
			{
				return (Comparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(GenericComparer<>), runtimeType);
			}
			if (runtimeType.IsGenericType && runtimeType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				RuntimeType runtimeType2 = (RuntimeType)runtimeType.GetGenericArguments()[0];
				if (typeof(IComparable<>).MakeGenericType(new Type[] { runtimeType2 }).IsAssignableFrom(runtimeType2))
				{
					return (Comparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(NullableComparer<>), runtimeType2);
				}
			}
			return new ObjectComparer<T>();
		}

		/// <summary>When overridden in a derived class, performs a comparison of two objects of the same type and returns a value indicating whether one object is less than, equal to, or greater than the other.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than <paramref name="y" />.Zero <paramref name="x" /> equals <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than <paramref name="y" />.</returns>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <exception cref="T:System.ArgumentException">Type <paramref name="T" /> does not implement either the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x06003C8B RID: 15499
		public abstract int Compare(T x, T y);

		/// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.</returns>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="x" /> or <paramref name="y" /> is of a type that cannot be cast to type <paramref name="T" />.-or-<paramref name="x" /> and <paramref name="y" /> do not implement either the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x06003C8C RID: 15500 RVA: 0x000E9D4E File Offset: 0x000E7F4E
		int IComparer.Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				if (x is T && y is T)
				{
					return this.Compare((T)((object)x), (T)((object)y));
				}
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
				return 0;
			}
		}

		// Token: 0x04001F53 RID: 8019
		private static volatile Comparer<T> defaultComparer;
	}
}
