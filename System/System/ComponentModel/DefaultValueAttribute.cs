using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Specifies the default value for a property.</summary>
	// Token: 0x02000237 RID: 567
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class, converting the specified value to the specified type, and using an invariant culture as the translation context.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to convert the value to. </param>
		/// <param name="value">A <see cref="T:System.String" /> that can be converted to the type using the <see cref="T:System.ComponentModel.TypeConverter" /> for the type and the U.S. English culture. </param>
		// Token: 0x06000D9E RID: 3486 RVA: 0x0003DC28 File Offset: 0x0003BE28
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				object obj;
				if (DefaultValueAttribute.<.ctor>g__TryConvertFromInvariantString|2_0(type, value, out obj))
				{
					this._value = obj;
				}
				else if (type.IsSubclassOf(typeof(Enum)))
				{
					this._value = Enum.Parse(type, value, true);
				}
				else if (type == typeof(TimeSpan))
				{
					this._value = TimeSpan.Parse(value);
				}
				else
				{
					this._value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a Unicode character.</summary>
		/// <param name="value">A Unicode character that is the default value. </param>
		// Token: 0x06000D9F RID: 3487 RVA: 0x0003DCC0 File Offset: 0x0003BEC0
		public DefaultValueAttribute(char value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that is the default value. </param>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003DCD4 File Offset: 0x0003BED4
		public DefaultValueAttribute(int value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 64-bit signed integer.</summary>
		/// <param name="value">A 64-bit signed integer that is the default value. </param>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003DCE8 File Offset: 0x0003BEE8
		public DefaultValueAttribute(long value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a double-precision floating point number.</summary>
		/// <param name="value">A double-precision floating point number that is the default value. </param>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003DCFC File Offset: 0x0003BEFC
		public DefaultValueAttribute(double value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> that is the default value. </param>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003DD10 File Offset: 0x0003BF10
		public DefaultValueAttribute(bool value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.String" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that is the default value. </param>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003DD24 File Offset: 0x0003BF24
		public DefaultValueAttribute(string value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that represents the default value. </param>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003DD24 File Offset: 0x0003BF24
		public DefaultValueAttribute(object value)
		{
			this._value = value;
		}

		/// <summary>Gets the default value of the property this attribute is bound to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of the property this attribute is bound to.</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0003DD33 File Offset: 0x0003BF33
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultValueAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003DD3C File Offset: 0x0003BF3C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(defaultValueAttribute.Value);
			}
			return defaultValueAttribute.Value == null;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003DD88 File Offset: 0x0003BF88
		[CompilerGenerated]
		internal static bool <.ctor>g__TryConvertFromInvariantString|2_0(Type typeToConvert, string stringValue, out object conversionResult)
		{
			conversionResult = null;
			if (DefaultValueAttribute.s_convertFromInvariantString == null)
			{
				Type type = Type.GetType("System.ComponentModel.TypeDescriptor, System.ComponentModel.TypeConverter", false);
				Volatile.Write<object>(ref DefaultValueAttribute.s_convertFromInvariantString, (type == null) ? new object() : Delegate.CreateDelegate(typeof(Func<Type, string, object>), type, "ConvertFromInvariantString", false));
			}
			Func<Type, string, object> func = DefaultValueAttribute.s_convertFromInvariantString as Func<Type, string, object>;
			if (func == null)
			{
				return false;
			}
			conversionResult = func(typeToConvert, stringValue);
			return true;
		}

		// Token: 0x0400096F RID: 2415
		private object _value;

		// Token: 0x04000970 RID: 2416
		private static object s_convertFromInvariantString;
	}
}
