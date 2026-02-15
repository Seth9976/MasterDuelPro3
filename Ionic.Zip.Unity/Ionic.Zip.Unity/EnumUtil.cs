using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Ionic
{
	// Token: 0x02000022 RID: 34
	internal sealed class EnumUtil
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00002086 File Offset: 0x00000286
		private EnumUtil()
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003934 File Offset: 0x00001B34
		internal static string GetDescription(Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length > 0)
			{
				return array[0].Description;
			}
			return value.ToString();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000397F File Offset: 0x00001B7F
		internal static object Parse(Type enumType, string stringRepresentation)
		{
			return EnumUtil.Parse(enumType, stringRepresentation, false);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000398C File Offset: 0x00001B8C
		public static Enum[] GetEnumValues(Type type)
		{
			if (!type.IsEnum)
			{
				throw new ArgumentException("not an enum");
			}
			List<Enum> list = new List<Enum>();
			foreach (FieldInfo fieldInfo in type.GetFields(24))
			{
				if (fieldInfo.IsLiteral)
				{
					list.Add((Enum)fieldInfo.GetValue(null));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039F0 File Offset: 0x00001BF0
		public static string[] GetEnumStrings<T>()
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsEnum)
			{
				throw new ArgumentException("not an enum");
			}
			List<string> list = new List<string>();
			foreach (FieldInfo fieldInfo in typeFromHandle.GetFields(24))
			{
				if (fieldInfo.IsLiteral)
				{
					list.Add(fieldInfo.Name);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A5C File Offset: 0x00001C5C
		internal static object Parse(Type enumType, string stringRepresentation, bool ignoreCase)
		{
			if (ignoreCase)
			{
				stringRepresentation = stringRepresentation.ToLower();
			}
			foreach (Enum @enum in EnumUtil.GetEnumValues(enumType))
			{
				string text = EnumUtil.GetDescription(@enum);
				if (ignoreCase)
				{
					text = text.ToLower();
				}
				if (text == stringRepresentation)
				{
					return @enum;
				}
			}
			return Enum.Parse(enumType, stringRepresentation, ignoreCase);
		}
	}
}
