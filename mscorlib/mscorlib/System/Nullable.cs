using System;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a value type that can be assigned null.</summary>
	/// <typeparam name="T">The underlying value type of the <see cref="T:System.Nullable`1" /> generic type.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000129 RID: 297
	[NonVersionable]
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Nullable`1" /> structure to the specified value. </summary>
		/// <param name="value">A value type.</param>
		// Token: 0x060009C0 RID: 2496 RVA: 0x00029928 File Offset: 0x00027B28
		[NonVersionable]
		public Nullable(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Nullable`1" /> object has a value.</summary>
		/// <returns>true if the current <see cref="T:System.Nullable`1" /> object has a value; false if the current <see cref="T:System.Nullable`1" /> object has no value.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00029938 File Offset: 0x00027B38
		public bool HasValue
		{
			[NonVersionable]
			get
			{
				return this.hasValue;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Nullable`1" /> value.</summary>
		/// <returns>The value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is true. An exception is thrown if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Nullable`1.HasValue" /> property is false.</exception>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00029940 File Offset: 0x00027B40
		public T Value
		{
			get
			{
				if (!this.hasValue)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_NoValue();
				}
				return this.value;
			}
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the object's default value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the  <see cref="P:System.Nullable`1.HasValue" /> property is true; otherwise, the default value of the current <see cref="T:System.Nullable`1" /> object. The type of the default value is the type argument of the current <see cref="T:System.Nullable`1" /> object, and the value of the default value consists solely of binary zeroes.</returns>
		// Token: 0x060009C3 RID: 2499 RVA: 0x00029955 File Offset: 0x00027B55
		[NonVersionable]
		public T GetValueOrDefault()
		{
			return this.value;
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the specified default value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is true; otherwise, the <paramref name="defaultValue" /> parameter.</returns>
		/// <param name="defaultValue">A value to return if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</param>
		// Token: 0x060009C4 RID: 2500 RVA: 0x0002995D File Offset: 0x00027B5D
		[NonVersionable]
		public T GetValueOrDefault(T defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		/// <summary>Indicates whether the current <see cref="T:System.Nullable`1" /> object is equal to a specified object.</summary>
		/// <returns>true if the <paramref name="other" /> parameter is equal to the current <see cref="T:System.Nullable`1" /> object; otherwise, false. This table describes how equality is defined for the compared values: Return ValueDescriptiontrueThe <see cref="P:System.Nullable`1.HasValue" /> property is false, and the <paramref name="other" /> parameter is null. That is, two null values are equal by definition.-or-The <see cref="P:System.Nullable`1.HasValue" /> property is true, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is equal to the <paramref name="other" /> parameter.falseThe <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is true, and the <paramref name="other" /> parameter is null.-or-The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is false, and the <paramref name="other" /> parameter is not null.-or-The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is true, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is not equal to the <paramref name="other" /> parameter.</returns>
		/// <param name="other">An object.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009C5 RID: 2501 RVA: 0x0002996F File Offset: 0x00027B6F
		public override bool Equals(object other)
		{
			if (!this.hasValue)
			{
				return other == null;
			}
			return other != null && this.value.Equals(other);
		}

		/// <summary>Retrieves the hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property.</summary>
		/// <returns>The hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is true, or zero if the <see cref="P:System.Nullable`1.HasValue" /> property is false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009C6 RID: 2502 RVA: 0x00029995 File Offset: 0x00027B95
		public override int GetHashCode()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		/// <summary>Returns the text representation of the value of the current <see cref="T:System.Nullable`1" /> object.</summary>
		/// <returns>The text representation of the value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is true, or an empty string ("") if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009C7 RID: 2503 RVA: 0x000299B2 File Offset: 0x00027BB2
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		/// <summary>Creates a new <see cref="T:System.Nullable`1" /> object initialized to a specified value. </summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> object whose <see cref="P:System.Nullable`1.Value" /> property is initialized with the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A value type.</param>
		// Token: 0x060009C8 RID: 2504 RVA: 0x000299D3 File Offset: 0x00027BD3
		[NonVersionable]
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		/// <summary>Returns the value of a specified <see cref="T:System.Nullable`1" /> value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property for the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A <see cref="T:System.Nullable`1" /> value.</param>
		// Token: 0x060009C9 RID: 2505 RVA: 0x000299DB File Offset: 0x00027BDB
		[NonVersionable]
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000299E4 File Offset: 0x00027BE4
		private static object Box(T? o)
		{
			if (!o.hasValue)
			{
				return null;
			}
			return o.value;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000299FC File Offset: 0x00027BFC
		private static T? Unbox(object o)
		{
			if (o == null)
			{
				return null;
			}
			return new T?((T)((object)o));
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00029A24 File Offset: 0x00027C24
		private static T? UnboxExact(object o)
		{
			if (o == null)
			{
				return null;
			}
			if (o.GetType() != typeof(T))
			{
				throw new InvalidCastException();
			}
			return new T?((T)((object)o));
		}

		// Token: 0x04000451 RID: 1105
		private readonly bool hasValue;

		// Token: 0x04000452 RID: 1106
		internal T value;
	}
}
