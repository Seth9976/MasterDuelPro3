using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	/// <summary>Provides the base class for enumerations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019F RID: 415
	[ComVisible(true)]
	[Serializable]
	public abstract class Enum : ValueType, IComparable, IFormattable, IConvertible
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x0003FDE0 File Offset: 0x0003DFE0
		private static Enum.ValuesAndNames GetCachedValuesAndNames(RuntimeType enumType, bool getNames)
		{
			Enum.ValuesAndNames valuesAndNames = enumType.GenericCache as Enum.ValuesAndNames;
			if (valuesAndNames == null || (getNames && valuesAndNames.Names == null))
			{
				ulong[] array = null;
				string[] array2 = null;
				if (!Enum.GetEnumValuesAndNames(enumType, out array, out array2))
				{
					Array.Sort<ulong, string>(array, array2, Comparer<ulong>.Default);
				}
				valuesAndNames = new Enum.ValuesAndNames(array, array2);
				enumType.GenericCache = valuesAndNames;
			}
			return valuesAndNames;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003FE34 File Offset: 0x0003E034
		private static string InternalFormattedHexString(object value)
		{
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				return Convert.ToByte((bool)value).ToString("X2", null);
			case TypeCode.Char:
				return ((ushort)((char)value)).ToString("X4", null);
			case TypeCode.SByte:
				return ((byte)((sbyte)value)).ToString("X2", null);
			case TypeCode.Byte:
				return ((byte)value).ToString("X2", null);
			case TypeCode.Int16:
				return ((ushort)((short)value)).ToString("X4", null);
			case TypeCode.UInt16:
				return ((ushort)value).ToString("X4", null);
			case TypeCode.Int32:
				return ((uint)((int)value)).ToString("X8", null);
			case TypeCode.UInt32:
				return ((uint)value).ToString("X8", null);
			case TypeCode.Int64:
				return ((ulong)((long)value)).ToString("X16", null);
			case TypeCode.UInt64:
				return ((ulong)value).ToString("X16", null);
			default:
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003FF6C File Offset: 0x0003E16C
		private static string InternalFormat(RuntimeType eT, object value)
		{
			if (eT.IsDefined(typeof(FlagsAttribute), false))
			{
				return Enum.InternalFlagsFormat(eT, value);
			}
			string name = Enum.GetName(eT, value);
			if (name == null)
			{
				return value.ToString();
			}
			return name;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003FFA8 File Offset: 0x0003E1A8
		private static string InternalFlagsFormat(RuntimeType eT, object value)
		{
			ulong num = Enum.ToUInt64(value);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(eT, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			int num2 = values.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			ulong num3 = num;
			while (num2 >= 0 && (num2 != 0 || values[num2] != 0UL))
			{
				if ((num & values[num2]) == values[num2])
				{
					num -= values[num2];
					if (!flag)
					{
						stringBuilder.Insert(0, ", ");
					}
					stringBuilder.Insert(0, names[num2]);
					flag = false;
				}
				num2--;
			}
			if (num != 0UL)
			{
				return value.ToString();
			}
			if (num3 != 0UL)
			{
				return stringBuilder.ToString();
			}
			if (values.Length != 0 && values[0] == 0UL)
			{
				return names[0];
			}
			return "0";
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00040050 File Offset: 0x0003E250
		internal static ulong ToUInt64(object value)
		{
			ulong num;
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				num = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
				break;
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				num = (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
				break;
			default:
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
			return num;
		}

		// Token: 0x06000F22 RID: 3874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalCompareTo(object o1, object o2);

		// Token: 0x06000F23 RID: 3875
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType InternalGetUnderlyingType(RuntimeType enumType);

		// Token: 0x06000F24 RID: 3876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnumValuesAndNames(RuntimeType enumType, out ulong[] values, out string[] names);

		// Token: 0x06000F25 RID: 3877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalBoxEnum(RuntimeType enumType, long value);

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.</summary>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">A string containing the name or value to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is either an empty string or only contains white space.-or- <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F26 RID: 3878 RVA: 0x000400C3 File Offset: 0x0003E2C3
		[ComVisible(true)]
		public static object Parse(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, false);
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-insensitive.</summary>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">A string containing the name or value to convert. </param>
		/// <param name="ignoreCase">true to ignore case; false to regard case. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is either an empty string ("") or only contains white space.-or- <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F27 RID: 3879 RVA: 0x000400D0 File Offset: 0x0003E2D0
		[ComVisible(true)]
		public static object Parse(Type enumType, string value, bool ignoreCase)
		{
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			enumResult.Init(true);
			if (Enum.TryParseEnum(enumType, value, ignoreCase, ref enumResult))
			{
				return enumResult.parsedEnum;
			}
			throw enumResult.GetEnumParseException();
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00040108 File Offset: 0x0003E308
		private static bool TryParseEnum(Type enumType, string value, bool ignoreCase, ref Enum.EnumResult parseResult)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			if (value == null)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.ArgumentNull, "value");
				return false;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.Argument, "Must specify valid information for parsing in the string.", null);
				return false;
			}
			ulong num = 0UL;
			if (char.IsDigit(value[0]) || value[0] == '-' || value[0] == '+')
			{
				Type underlyingType = Enum.GetUnderlyingType(enumType);
				try
				{
					object obj = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
					parseResult.parsedEnum = Enum.ToObject(enumType, obj);
					return true;
				}
				catch (FormatException)
				{
				}
				catch (Exception ex)
				{
					if (parseResult.canThrow)
					{
						throw;
					}
					parseResult.SetFailure(ex);
					return false;
				}
			}
			string[] array = value.Split(Enum.enumSeperatorCharArray);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(runtimeType, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				bool flag = false;
				int j = 0;
				while (j < names.Length)
				{
					if (ignoreCase)
					{
						if (string.Compare(names[j], array[i], StringComparison.OrdinalIgnoreCase) == 0)
						{
							goto IL_0158;
						}
					}
					else if (names[j].Equals(array[i]))
					{
						goto IL_0158;
					}
					j++;
					continue;
					IL_0158:
					ulong num2 = values[j];
					num |= num2;
					flag = true;
					break;
				}
				if (!flag)
				{
					parseResult.SetFailure(Enum.ParseFailureKind.ArgumentWithParameter, "Requested value '{0}' was not found.", value);
					return false;
				}
			}
			bool flag2;
			try
			{
				parseResult.parsedEnum = Enum.ToObject(enumType, num);
				flag2 = true;
			}
			catch (Exception ex2)
			{
				if (parseResult.canThrow)
				{
					throw;
				}
				parseResult.SetFailure(ex2);
				flag2 = false;
			}
			return flag2;
		}

		/// <summary>Returns the underlying type of the specified enumeration.</summary>
		/// <returns>The underlying type of <paramref name="enumType" />.</returns>
		/// <param name="enumType">The enumeration whose underlying type will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F29 RID: 3881 RVA: 0x00040304 File Offset: 0x0003E504
		[ComVisible(true)]
		public static Type GetUnderlyingType(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumUnderlyingType();
		}

		/// <summary>Retrieves an array of the values of the constants in a specified enumeration.</summary>
		/// <returns>An array that contains the values of the constants in <paramref name="enumType" />. </returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked by reflection in a reflection-only context, -or-<paramref name="enumType" /> is a type from an assembly loaded in a reflection-only context.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2A RID: 3882 RVA: 0x00040320 File Offset: 0x0003E520
		[ComVisible(true)]
		public static Array GetValues(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumValues();
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0004033C File Offset: 0x0003E53C
		internal static ulong[] InternalGetValues(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, false).Values;
		}

		/// <summary>Retrieves the name of the constant in the specified enumeration that has the specified value.</summary>
		/// <returns>A string containing the name of the enumerated constant in <paramref name="enumType" /> whose value is <paramref name="value" />; or null if no such constant is found.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">The value of a particular enumerated constant in terms of its underlying type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is neither of type <paramref name="enumType" /> nor does it have the same underlying type as <paramref name="enumType" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2C RID: 3884 RVA: 0x0004034A File Offset: 0x0003E54A
		[ComVisible(true)]
		public static string GetName(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumName(value);
		}

		/// <summary>Retrieves an array of the names of the constants in a specified enumeration.</summary>
		/// <returns>A string array of the names of the constants in <paramref name="enumType" />. </returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2D RID: 3885 RVA: 0x00040367 File Offset: 0x0003E567
		[ComVisible(true)]
		public static string[] GetNames(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumNames();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00040383 File Offset: 0x0003E583
		internal static string[] InternalGetNames(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, true).Names;
		}

		/// <summary>Converts the specified object with an integer value to an enumeration member.</summary>
		/// <returns>An enumeration object whose value is <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2F RID: 3887 RVA: 0x00040394 File Offset: 0x0003E594
		[ComVisible(true)]
		public static object ToObject(Type enumType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				return Enum.ToObject(enumType, (bool)value);
			case TypeCode.Char:
				return Enum.ToObject(enumType, (char)value);
			case TypeCode.SByte:
				return Enum.ToObject(enumType, (sbyte)value);
			case TypeCode.Byte:
				return Enum.ToObject(enumType, (byte)value);
			case TypeCode.Int16:
				return Enum.ToObject(enumType, (short)value);
			case TypeCode.UInt16:
				return Enum.ToObject(enumType, (ushort)value);
			case TypeCode.Int32:
				return Enum.ToObject(enumType, (int)value);
			case TypeCode.UInt32:
				return Enum.ToObject(enumType, (uint)value);
			case TypeCode.Int64:
				return Enum.ToObject(enumType, (long)value);
			case TypeCode.UInt64:
				return Enum.ToObject(enumType, (ulong)value);
			default:
				throw new ArgumentException(Environment.GetResourceString("The value passed in must be an enum base or an underlying type for an enum, such as an Int32."), "value");
			}
		}

		/// <summary>Returns an indication whether a constant with a specified value exists in a specified enumeration.</summary>
		/// <returns>true if a constant in <paramref name="enumType" /> has a value equal to <paramref name="value" />; otherwise, false.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">The value or name of a constant in <paramref name="enumType" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an Enum.-or- The type of <paramref name="value" /> is an enumeration, but it is not an enumeration of type <paramref name="enumType" />.-or- The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />, or <see cref="T:System.String" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F30 RID: 3888 RVA: 0x00040481 File Offset: 0x0003E681
		[ComVisible(true)]
		public static bool IsDefined(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.IsEnumDefined(value);
		}

		/// <summary>Converts the specified value of a specified enumerated type to its equivalent string representation according to the specified format.</summary>
		/// <returns>A string representation of <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type of the value to convert. </param>
		/// <param name="value">The value to convert. </param>
		/// <param name="format">The output format to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="enumType" />, <paramref name="value" />, or <paramref name="format" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" /> type.-or- The <paramref name="value" /> is from an enumeration that differs in type from <paramref name="enumType" />.-or- The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />. </exception>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter contains an invalid value. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F31 RID: 3889 RVA: 0x000404A0 File Offset: 0x0003E6A0
		[ComVisible(true)]
		public static string Format(Type enumType, object value, string format)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			Type type = value.GetType();
			Type underlyingType = Enum.GetUnderlyingType(enumType);
			if (type.IsEnum)
			{
				Type underlyingType2 = Enum.GetUnderlyingType(type);
				if (!type.IsEquivalentTo(enumType))
				{
					throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
					{
						type.ToString(),
						enumType.ToString()
					}));
				}
				value = ((Enum)value).GetValue();
			}
			else if (type != underlyingType)
			{
				throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
				{
					type.ToString(),
					underlyingType.ToString()
				}));
			}
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
			}
			char c = format[0];
			if (c == 'D' || c == 'd')
			{
				return value.ToString();
			}
			if (c == 'X' || c == 'x')
			{
				return Enum.InternalFormattedHexString(value);
			}
			if (c == 'G' || c == 'g')
			{
				return Enum.InternalFormat(runtimeType, value);
			}
			if (c == 'F' || c == 'f')
			{
				return Enum.InternalFlagsFormat(runtimeType, value);
			}
			throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
		}

		// Token: 0x06000F32 RID: 3890
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object get_value();

		// Token: 0x06000F33 RID: 3891 RVA: 0x00040627 File Offset: 0x0003E827
		internal object GetValue()
		{
			return this.get_value();
		}

		// Token: 0x06000F34 RID: 3892
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool InternalHasFlag(Enum flags);

		// Token: 0x06000F35 RID: 3893
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int get_hashcode();

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an enumeration value of the same type and with the same underlying value as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F36 RID: 3894 RVA: 0x0004062F File Offset: 0x0003E82F
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		/// <summary>Returns the hash code for the value of this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F37 RID: 3895 RVA: 0x00040638 File Offset: 0x0003E838
		public override int GetHashCode()
		{
			return this.get_hashcode();
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F38 RID: 3896 RVA: 0x00040640 File Offset: 0x0003E840
		public override string ToString()
		{
			return Enum.InternalFormat((RuntimeType)base.GetType(), this.GetValue());
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString(System.String)" />.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A format specification. </param>
		/// <param name="provider">(Obsolete.)</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> does not contain a valid format specification. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F39 RID: 3897 RVA: 0x00040658 File Offset: 0x0003E858
		[Obsolete("The provider argument is not used. Please use ToString(String).")]
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format);
		}

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of this instance and <paramref name="target" />.Value Meaning Less than zero The value of this instance is less than the value of <paramref name="target" />. Zero The value of this instance is equal to the value of <paramref name="target" />. Greater than zero The value of this instance is greater than the value of <paramref name="target" />.-or- <paramref name="target" /> is null. </returns>
		/// <param name="target">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> and this instance are not the same type. </exception>
		/// <exception cref="T:System.InvalidOperationException">This instance is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F3A RID: 3898 RVA: 0x00040664 File Offset: 0x0003E864
		public int CompareTo(object target)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			int num = Enum.InternalCompareTo(this, target);
			if (num < 2)
			{
				return num;
			}
			if (num == 2)
			{
				Type type = base.GetType();
				Type type2 = target.GetType();
				throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
				{
					type2.ToString(),
					type.ToString()
				}));
			}
			throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A format string. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> contains an invalid specification. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F3B RID: 3899 RVA: 0x000406D4 File Offset: 0x0003E8D4
		public string ToString(string format)
		{
			if (format == null || format.Length == 0)
			{
				format = "G";
			}
			if (string.Compare(format, "G", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.ToString();
			}
			if (string.Compare(format, "D", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.GetValue().ToString();
			}
			if (string.Compare(format, "X", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFormattedHexString(this.GetValue());
			}
			if (string.Compare(format, "F", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFlagsFormat((RuntimeType)base.GetType(), this.GetValue());
			}
			throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString" />.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <param name="provider">(obsolete) </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F3C RID: 3900 RVA: 0x00040770 File Offset: 0x0003E970
		[Obsolete("The provider argument is not used. Please use ToString().")]
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		/// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
		/// <returns>true if the bit field or bit fields that are set in <paramref name="flag" /> are also set in the current instance; otherwise, false.</returns>
		/// <param name="flag">An enumeration value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is a different type than the current instance.</exception>
		// Token: 0x06000F3D RID: 3901 RVA: 0x00040778 File Offset: 0x0003E978
		public bool HasFlag(Enum flag)
		{
			if (flag == null)
			{
				throw new ArgumentNullException("flag");
			}
			if (!base.GetType().IsEquivalentTo(flag.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("The argument type, '{0}', is not the same as the enum type '{1}'.", new object[]
				{
					flag.GetType(),
					base.GetType()
				}));
			}
			return this.InternalHasFlag(flag);
		}

		/// <summary>Returns the underlying <see cref="T:System.TypeCode" /> for this instance.</summary>
		/// <returns>The type for this instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration type is unknown.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F3E RID: 3902 RVA: 0x000407D8 File Offset: 0x0003E9D8
		public TypeCode GetTypeCode()
		{
			Type underlyingType = Enum.GetUnderlyingType(base.GetType());
			if (underlyingType == typeof(int))
			{
				return TypeCode.Int32;
			}
			if (underlyingType == typeof(sbyte))
			{
				return TypeCode.SByte;
			}
			if (underlyingType == typeof(short))
			{
				return TypeCode.Int16;
			}
			if (underlyingType == typeof(long))
			{
				return TypeCode.Int64;
			}
			if (underlyingType == typeof(uint))
			{
				return TypeCode.UInt32;
			}
			if (underlyingType == typeof(byte))
			{
				return TypeCode.Byte;
			}
			if (underlyingType == typeof(ushort))
			{
				return TypeCode.UInt16;
			}
			if (underlyingType == typeof(ulong))
			{
				return TypeCode.UInt64;
			}
			if (underlyingType == typeof(bool))
			{
				return TypeCode.Boolean;
			}
			if (underlyingType == typeof(char))
			{
				return TypeCode.Char;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
		}

		/// <summary>Converts the current value to a Boolean value based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000F3F RID: 3903 RVA: 0x000408CC File Offset: 0x0003EACC
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a Unicode character based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000F40 RID: 3904 RVA: 0x000408DE File Offset: 0x0003EADE
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F41 RID: 3905 RVA: 0x000408F0 File Offset: 0x0003EAF0
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F42 RID: 3906 RVA: 0x00040902 File Offset: 0x0003EB02
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F43 RID: 3907 RVA: 0x00040914 File Offset: 0x0003EB14
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F44 RID: 3908 RVA: 0x00040926 File Offset: 0x0003EB26
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F45 RID: 3909 RVA: 0x00040938 File Offset: 0x0003EB38
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F46 RID: 3910 RVA: 0x0004094A File Offset: 0x0003EB4A
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F47 RID: 3911 RVA: 0x0004095C File Offset: 0x0003EB5C
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F48 RID: 3912 RVA: 0x0004096E File Offset: 0x0003EB6E
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a single-precision floating-point number based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000F49 RID: 3913 RVA: 0x00040980 File Offset: 0x0003EB80
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a double-precision floating point number based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000F4A RID: 3914 RVA: 0x00040992 File Offset: 0x0003EB92
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.Decimal" /> based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000F4B RID: 3915 RVA: 0x000409A4 File Offset: 0x0003EBA4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.DateTime" /> based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000F4C RID: 3916 RVA: 0x000409B6 File Offset: 0x0003EBB6
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Enum", "DateTime" }));
		}

		/// <summary>Converts the current value to a specified type based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="type">The type to convert to. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x06000F4D RID: 3917 RVA: 0x00011F42 File Offset: 0x00010142
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Converts the specified 8-bit signed integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4E RID: 3918 RVA: 0x000409E0 File Offset: 0x0003EBE0
		[ComVisible(true)]
		[CLSCompliant(false)]
		public static object ToObject(Type enumType, sbyte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 16-bit signed integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4F RID: 3919 RVA: 0x00040A4C File Offset: 0x0003EC4C
		[ComVisible(true)]
		public static object ToObject(Type enumType, short value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 32-bit signed integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F50 RID: 3920 RVA: 0x00040AB8 File Offset: 0x0003ECB8
		[ComVisible(true)]
		public static object ToObject(Type enumType, int value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 8-bit unsigned integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F51 RID: 3921 RVA: 0x00040B24 File Offset: 0x0003ED24
		[ComVisible(true)]
		public static object ToObject(Type enumType, byte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 16-bit unsigned integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F52 RID: 3922 RVA: 0x00040B90 File Offset: 0x0003ED90
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, ushort value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 32-bit unsigned integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F53 RID: 3923 RVA: 0x00040BFC File Offset: 0x0003EDFC
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, uint value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 64-bit signed integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F54 RID: 3924 RVA: 0x00040C68 File Offset: 0x0003EE68
		[ComVisible(true)]
		public static object ToObject(Type enumType, long value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value);
		}

		/// <summary>Converts the specified 64-bit unsigned integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F55 RID: 3925 RVA: 0x00040CD0 File Offset: 0x0003EED0
		[ComVisible(true)]
		[CLSCompliant(false)]
		public static object ToObject(Type enumType, ulong value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00040D38 File Offset: 0x0003EF38
		private static object ToObject(Type enumType, char value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00040DA4 File Offset: 0x0003EFA4
		private static object ToObject(Type enumType, bool value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value ? 1L : 0L);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00040E14 File Offset: 0x0003F014
		public static bool TryParse(Type enumType, string value, bool ignoreCase, out object result)
		{
			result = null;
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			bool flag = Enum.TryParseEnum(enumType, value, ignoreCase, ref enumResult);
			if (flag)
			{
				result = enumResult.parsedEnum;
			}
			return flag;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00040E41 File Offset: 0x0003F041
		public static bool TryParse(Type enumType, string value, out object result)
		{
			return Enum.TryParse(enumType, value, false, out result);
		}

		// Token: 0x040005EA RID: 1514
		private static readonly char[] enumSeperatorCharArray = new char[] { ',' };

		// Token: 0x040005EB RID: 1515
		private const string enumSeperator = ", ";

		// Token: 0x020001A0 RID: 416
		private enum ParseFailureKind
		{
			// Token: 0x040005ED RID: 1517
			None,
			// Token: 0x040005EE RID: 1518
			Argument,
			// Token: 0x040005EF RID: 1519
			ArgumentNull,
			// Token: 0x040005F0 RID: 1520
			ArgumentWithParameter,
			// Token: 0x040005F1 RID: 1521
			UnhandledException
		}

		// Token: 0x020001A1 RID: 417
		private struct EnumResult
		{
			// Token: 0x06000F5C RID: 3932 RVA: 0x00040E66 File Offset: 0x0003F066
			internal void Init(bool canMethodThrow)
			{
				this.parsedEnum = 0;
				this.canThrow = canMethodThrow;
			}

			// Token: 0x06000F5D RID: 3933 RVA: 0x00040E7B File Offset: 0x0003F07B
			internal void SetFailure(Exception unhandledException)
			{
				this.m_failure = Enum.ParseFailureKind.UnhandledException;
				this.m_innerException = unhandledException;
			}

			// Token: 0x06000F5E RID: 3934 RVA: 0x00040E8B File Offset: 0x0003F08B
			internal void SetFailure(Enum.ParseFailureKind failure, string failureParameter)
			{
				this.m_failure = failure;
				this.m_failureParameter = failureParameter;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x06000F5F RID: 3935 RVA: 0x00040EAA File Offset: 0x0003F0AA
			internal void SetFailure(Enum.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x06000F60 RID: 3936 RVA: 0x00040ED0 File Offset: 0x0003F0D0
			internal Exception GetEnumParseException()
			{
				switch (this.m_failure)
				{
				case Enum.ParseFailureKind.Argument:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID));
				case Enum.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureParameter);
				case Enum.ParseFailureKind.ArgumentWithParameter:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID, new object[] { this.m_failureMessageFormatArgument }));
				case Enum.ParseFailureKind.UnhandledException:
					return this.m_innerException;
				default:
					return new ArgumentException(Environment.GetResourceString("Requested value '{0}' was not found."));
				}
			}

			// Token: 0x040005F2 RID: 1522
			internal object parsedEnum;

			// Token: 0x040005F3 RID: 1523
			internal bool canThrow;

			// Token: 0x040005F4 RID: 1524
			internal Enum.ParseFailureKind m_failure;

			// Token: 0x040005F5 RID: 1525
			internal string m_failureMessageID;

			// Token: 0x040005F6 RID: 1526
			internal string m_failureParameter;

			// Token: 0x040005F7 RID: 1527
			internal object m_failureMessageFormatArgument;

			// Token: 0x040005F8 RID: 1528
			internal Exception m_innerException;
		}

		// Token: 0x020001A2 RID: 418
		private class ValuesAndNames
		{
			// Token: 0x06000F61 RID: 3937 RVA: 0x00040F51 File Offset: 0x0003F151
			public ValuesAndNames(ulong[] values, string[] names)
			{
				this.Values = values;
				this.Names = names;
			}

			// Token: 0x040005F9 RID: 1529
			public ulong[] Values;

			// Token: 0x040005FA RID: 1530
			public string[] Names;
		}
	}
}
