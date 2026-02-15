using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace System.Reflection
{
	/// <summary>Represents an argument of a custom attribute in the reflection-only context, or an element of an array argument.</summary>
	// Token: 0x0200062B RID: 1579
	public struct CustomAttributeTypedArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified value.</summary>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06002E70 RID: 11888 RVA: 0x000B342C File Offset: 0x000B162C
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = CustomAttributeTypedArgument.CanonicalizeValue(value);
			this.ArgumentType = value.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified type and value.</summary>
		/// <param name="argumentType">The type of the custom attribute argument.</param>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="argumentType" /> is null.</exception>
		// Token: 0x06002E71 RID: 11889 RVA: 0x000B3454 File Offset: 0x000B1654
		public CustomAttributeTypedArgument(Type argumentType, object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.Value = ((value == null) ? null : CustomAttributeTypedArgument.CanonicalizeValue(value));
			this.ArgumentType = argumentType;
			Array array = value as Array;
			if (array != null)
			{
				Type elementType = array.GetType().GetElementType();
				CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[array.GetLength(0)];
				for (int i = 0; i < array2.Length; i++)
				{
					object value2 = array.GetValue(i);
					Type type = ((elementType == typeof(object) && value2 != null) ? value2.GetType() : elementType);
					array2[i] = new CustomAttributeTypedArgument(type, value2);
				}
				this.Value = new ReadOnlyCollection<CustomAttributeTypedArgument>(array2);
			}
		}

		/// <summary>Gets the type of the argument or of the array argument element.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type of the argument or of the array element.</returns>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000B3506 File Offset: 0x000B1706
		public readonly Type ArgumentType { get; }

		/// <summary>Gets the value of the argument for a simple argument or for an element of an array argument; gets a collection of values for an array argument.</summary>
		/// <returns>An object that represents the value of the argument or element, or a generic <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> objects that represent the values of an array-type argument.</returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000B350E File Offset: 0x000B170E
		public readonly object Value { get; }

		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
		/// <param name="obj">The object to compare with the current instance. </param>
		// Token: 0x06002E74 RID: 11892 RVA: 0x000B3516 File Offset: 0x000B1716
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000B3526 File Offset: 0x000B1726
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the equality operator.</param>
		// Token: 0x06002E76 RID: 11894 RVA: 0x000B3538 File Offset: 0x000B1738
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the inequality operator.</param>
		// Token: 0x06002E77 RID: 11895 RVA: 0x000B354D File Offset: 0x000B174D
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a string consisting of the argument name, the equal sign, and a string representation of the argument value.</summary>
		/// <returns>A string consisting of the argument name, the equal sign, and a string representation of the argument value.</returns>
		// Token: 0x06002E78 RID: 11896 RVA: 0x000B3565 File Offset: 0x000B1765
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000B3570 File Offset: 0x000B1770
		internal string ToString(bool typed)
		{
			if (this.ArgumentType == null)
			{
				return base.ToString();
			}
			string text;
			try
			{
				if (this.ArgumentType.IsEnum)
				{
					text = string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.FullNameOrDefault);
				}
				else if (this.Value == null)
				{
					text = string.Format(CultureInfo.CurrentCulture, typed ? "null" : "({0})null", this.ArgumentType.NameOrDefault);
				}
				else if (this.ArgumentType == typeof(string))
				{
					text = string.Format(CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
				}
				else if (this.ArgumentType == typeof(char))
				{
					text = string.Format(CultureInfo.CurrentCulture, "'{0}'", this.Value);
				}
				else if (this.ArgumentType == typeof(Type))
				{
					text = string.Format(CultureInfo.CurrentCulture, "typeof({0})", ((Type)this.Value).FullNameOrDefault);
				}
				else if (this.ArgumentType.IsArray)
				{
					IList<CustomAttributeTypedArgument> list = this.Value as IList<CustomAttributeTypedArgument>;
					Type elementType = this.ArgumentType.GetElementType();
					string text2 = string.Format(CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? elementType.FullNameOrDefault : elementType.NameOrDefault, list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						text2 += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", list[i].ToString(elementType != typeof(object)));
					}
					text = text2 + " }";
				}
				else
				{
					text = string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.NameOrDefault);
				}
			}
			catch (MissingMetadataException)
			{
				text = base.ToString();
			}
			return text;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000B37CC File Offset: 0x000B19CC
		private static object CanonicalizeValue(object value)
		{
			if (value.GetType().IsEnum)
			{
				return ((Enum)value).GetValue();
			}
			return value;
		}
	}
}
