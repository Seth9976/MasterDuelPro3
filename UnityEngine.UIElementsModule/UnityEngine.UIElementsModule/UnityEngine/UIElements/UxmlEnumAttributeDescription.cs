using System;
using System.Collections.Generic;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000493 RID: 1171
	public class UxmlEnumAttributeDescription<T> : TypedUxmlAttributeDescription<T> where T : struct, IConvertible
	{
		// Token: 0x060021E6 RID: 8678 RVA: 0x0007C364 File Offset: 0x0007A564
		public UxmlEnumAttributeDescription()
		{
			bool flag = !typeof(T).IsEnum;
			if (flag)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = new T();
			UxmlEnumeration enumRestriction = new UxmlEnumeration();
			List<string> values = new List<string>();
			foreach (object obj in Enum.GetValues(typeof(T)))
			{
				T item = (T)((object)obj);
				values.Add(item.ToString(CultureInfo.InvariantCulture));
			}
			enumRestriction.values = values;
			base.restriction = enumRestriction;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0007C44C File Offset: 0x0007A64C
		public override T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0007C48C File Offset: 0x0007A68C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref T value)
		{
			return base.TryGetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue, ref value);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0007C4CC File Offset: 0x0007A6CC
		private static U ConvertValueToEnum<U>(string v, U defaultValue) where U : struct
		{
			try
			{
				bool flag = string.IsNullOrEmpty(v);
				if (flag)
				{
					return defaultValue;
				}
				return (U)((object)Enum.Parse(typeof(U), v, true));
			}
			catch (ArgumentException)
			{
				Debug.LogError(UxmlEnumAttributeDescription<T>.GetEnumNameErrorMessage(v, typeof(U)));
			}
			catch (OverflowException)
			{
				Debug.LogError(UxmlEnumAttributeDescription<T>.GetEnumRangeErrorMessage(v, typeof(U)));
			}
			return defaultValue;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0007C55C File Offset: 0x0007A75C
		private static string GetEnumNameErrorMessage(string v, Type enumType)
		{
			return string.Concat(new string[]
			{
				"The ",
				enumType.Name,
				" enum does not contain the value `",
				v,
				"`. Value must be in range [",
				string.Join(" | ", Enum.GetNames(enumType)),
				"]."
			});
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0007C5BC File Offset: 0x0007A7BC
		private static string GetEnumRangeErrorMessage(string v, Type enumType)
		{
			return v + " is outside of the range of possible values for the " + enumType.Name + " enum.";
		}
	}
}
