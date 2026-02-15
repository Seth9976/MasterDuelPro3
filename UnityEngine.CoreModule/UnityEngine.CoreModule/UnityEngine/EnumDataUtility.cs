using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200019C RID: 412
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static class EnumDataUtility
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x0002209C File Offset: 0x0002029C
		public static EnumData GetCachedEnumData(Type enumType, EnumDataUtility.CachedType cachedType = EnumDataUtility.CachedType.IncludeObsoleteExceptErrors, Func<string, string> nicifyName = null)
		{
			EnumData enumData;
			bool flag = EnumDataUtility.s_EnumData.TryGetValue(new ValueTuple<EnumDataUtility.CachedType, Type>(cachedType, enumType), out enumData);
			EnumData enumData2;
			if (flag)
			{
				enumData2 = enumData;
			}
			else
			{
				enumData = new EnumData
				{
					underlyingType = Enum.GetUnderlyingType(enumType)
				};
				enumData.unsigned = enumData.underlyingType == typeof(byte) || enumData.underlyingType == typeof(ushort) || enumData.underlyingType == typeof(uint) || enumData.underlyingType == typeof(ulong);
				FieldInfo[] enumFields = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);
				List<FieldInfo> enumfieldlist = new List<FieldInfo>();
				int enumFieldslen = enumFields.Length;
				for (int i = 0; i < enumFieldslen; i++)
				{
					bool flag2 = EnumDataUtility.CheckObsoleteAddition(enumFields[i], cachedType);
					if (flag2)
					{
						enumfieldlist.Add(enumFields[i]);
					}
				}
				bool flag3 = !enumfieldlist.Any<FieldInfo>();
				if (flag3)
				{
					string[] defaultstr = new string[] { "" };
					Enum[] defaultenum = new Enum[0];
					int[] defaultarr = new int[1];
					enumData.values = defaultenum;
					enumData.flagValues = defaultarr;
					enumData.displayNames = defaultstr;
					enumData.names = defaultstr;
					enumData.tooltip = defaultstr;
					enumData.flags = true;
					enumData.serializable = true;
					enumData2 = enumData;
				}
				else
				{
					try
					{
						string location = enumfieldlist.First<FieldInfo>().Module.Assembly.Location;
						bool flag4 = !string.IsNullOrEmpty(location);
						if (flag4)
						{
							enumfieldlist = enumfieldlist.OrderBy((FieldInfo f) => f.MetadataToken).ToList<FieldInfo>();
						}
					}
					catch
					{
					}
					enumData.displayNames = enumfieldlist.Select((FieldInfo f) => EnumDataUtility.EnumNameFromEnumField(f, nicifyName)).ToArray<string>();
					bool flag5 = enumData.displayNames.Distinct<string>().Count<string>() != enumData.displayNames.Length;
					if (flag5)
					{
						Debug.LogWarning("Enum " + enumType.Name + " has multiple entries with the same display name, this prevents selection in EnumPopup.");
					}
					enumData.tooltip = enumfieldlist.Select((FieldInfo f) => EnumDataUtility.EnumTooltipFromEnumField(f)).ToArray<string>();
					enumData.values = enumfieldlist.Select((FieldInfo f) => (Enum)f.GetValue(null)).ToArray<Enum>();
					int[] array;
					if (!enumData.unsigned)
					{
						array = enumData.values.Select((Enum v) => (int)Convert.ToInt64(v)).ToArray<int>();
					}
					else
					{
						array = enumData.values.Select((Enum v) => (int)Convert.ToUInt64(v)).ToArray<int>();
					}
					enumData.flagValues = array;
					enumData.names = new string[enumData.values.Length];
					for (int j = 0; j < enumfieldlist.Count; j++)
					{
						enumData.names[j] = enumfieldlist[j].Name;
					}
					bool flag6 = enumData.underlyingType == typeof(ushort);
					if (flag6)
					{
						int k = 0;
						int length = enumData.flagValues.Length;
						while (k < length)
						{
							bool flag7 = (long)enumData.flagValues[k] == 65535L;
							if (flag7)
							{
								enumData.flagValues[k] = -1;
							}
							k++;
						}
					}
					else
					{
						bool flag8 = enumData.underlyingType == typeof(byte);
						if (flag8)
						{
							int l = 0;
							int length2 = enumData.flagValues.Length;
							while (l < length2)
							{
								bool flag9 = (long)enumData.flagValues[l] == 255L;
								if (flag9)
								{
									enumData.flagValues[l] = -1;
								}
								l++;
							}
						}
					}
					enumData.flags = enumType.IsDefined(typeof(FlagsAttribute), false);
					enumData.serializable = enumData.underlyingType != typeof(long) && enumData.underlyingType != typeof(ulong);
					EnumDataUtility.HandleInspectorOrderAttribute(enumType, ref enumData);
					EnumDataUtility.s_EnumData[new ValueTuple<EnumDataUtility.CachedType, Type>(cachedType, enumType)] = enumData;
					enumData2 = enumData;
				}
			}
			return enumData2;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00022530 File Offset: 0x00020730
		public static void HandleInspectorOrderAttribute(Type enumType, ref EnumData enumData)
		{
			InspectorOrderAttribute attribute = Attribute.GetCustomAttribute(enumType, typeof(InspectorOrderAttribute)) as InspectorOrderAttribute;
			bool flag = attribute == null;
			if (!flag)
			{
				int size = enumData.displayNames.Length;
				int[] indexes = new int[size];
				for (int i = 0; i < size; i++)
				{
					indexes[i] = i;
				}
				InspectorSort inspectorSort = attribute.m_inspectorSort;
				InspectorSort inspectorSort2 = inspectorSort;
				if (inspectorSort2 != InspectorSort.ByValue)
				{
					string[] sortData = new string[size];
					Array.Copy(enumData.displayNames, sortData, size);
					Array.Sort<string, int>(sortData, indexes, StringComparer.Ordinal);
				}
				else
				{
					int[] data = new int[size];
					Array.Copy(enumData.flagValues, data, size);
					Array.Sort<int, int>(data, indexes);
				}
				bool flag2 = attribute.m_sortDirection == InspectorSortDirection.Descending;
				if (flag2)
				{
					Array.Reverse<int>(indexes);
				}
				Enum[] values = new Enum[size];
				int[] flagValues = new int[size];
				string[] displayNames = new string[size];
				string[] names = new string[size];
				string[] tooltip = new string[size];
				for (int j = 0; j < size; j++)
				{
					int index = indexes[j];
					values[j] = enumData.values[index];
					flagValues[j] = enumData.flagValues[index];
					displayNames[j] = enumData.displayNames[index];
					names[j] = enumData.names[index];
					tooltip[j] = enumData.tooltip[index];
				}
				enumData.values = values;
				enumData.flagValues = flagValues;
				enumData.displayNames = displayNames;
				enumData.names = names;
				enumData.tooltip = tooltip;
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000226B4 File Offset: 0x000208B4
		private static bool CheckObsoleteAddition(FieldInfo field, EnumDataUtility.CachedType cachedType)
		{
			object[] obsolete = field.GetCustomAttributes(typeof(ObsoleteAttribute), false);
			bool flag = obsolete.Length != 0;
			bool flag3;
			if (flag)
			{
				bool flag2 = cachedType == EnumDataUtility.CachedType.ExcludeObsolete;
				if (flag2)
				{
					flag3 = false;
				}
				else
				{
					bool flag4 = cachedType == EnumDataUtility.CachedType.IncludeAllObsolete;
					flag3 = flag4 || !((ObsoleteAttribute)obsolete.First<object>()).IsError;
				}
			}
			else
			{
				flag3 = true;
			}
			return flag3;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00022714 File Offset: 0x00020914
		private static string EnumTooltipFromEnumField(FieldInfo field)
		{
			object[] tooltip = field.GetCustomAttributes(typeof(TooltipAttribute), false);
			bool flag = tooltip.Length != 0;
			string text;
			if (flag)
			{
				text = ((TooltipAttribute)tooltip.First<object>()).tooltip;
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0002275C File Offset: 0x0002095C
		private static string EnumNameFromEnumField(FieldInfo field, Func<string, string> nicifyName)
		{
			EnumDataUtility.<>c__DisplayClass8_0 CS$<>8__locals1;
			CS$<>8__locals1.nicifyName = nicifyName;
			CS$<>8__locals1.field = field;
			object[] description = CS$<>8__locals1.field.GetCustomAttributes(typeof(InspectorNameAttribute), false);
			bool flag = description.Length != 0;
			string text;
			if (flag)
			{
				text = ((InspectorNameAttribute)description.First<object>()).displayName;
			}
			else
			{
				bool flag2 = CS$<>8__locals1.field.IsDefined(typeof(ObsoleteAttribute), false);
				if (flag2)
				{
					text = EnumDataUtility.<EnumNameFromEnumField>g__NicifyName|8_0(ref CS$<>8__locals1) + " (Obsolete)";
				}
				else
				{
					text = EnumDataUtility.<EnumNameFromEnumField>g__NicifyName|8_0(ref CS$<>8__locals1);
				}
			}
			return text;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000227F8 File Offset: 0x000209F8
		[CompilerGenerated]
		internal static string <EnumNameFromEnumField>g__NicifyName|8_0(ref EnumDataUtility.<>c__DisplayClass8_0 A_0)
		{
			return (A_0.nicifyName == null) ? A_0.field.Name : A_0.nicifyName(A_0.field.Name);
		}

		// Token: 0x0400065C RID: 1628
		private static readonly Dictionary<ValueTuple<EnumDataUtility.CachedType, Type>, EnumData> s_EnumData = new Dictionary<ValueTuple<EnumDataUtility.CachedType, Type>, EnumData>();

		// Token: 0x0200019D RID: 413
		public enum CachedType
		{
			// Token: 0x0400065E RID: 1630
			ExcludeObsolete,
			// Token: 0x0400065F RID: 1631
			IncludeObsoleteExceptErrors,
			// Token: 0x04000660 RID: 1632
			IncludeAllObsolete
		}
	}
}
