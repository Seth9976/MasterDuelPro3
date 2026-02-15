using System;
using System.Globalization;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005BB RID: 1467
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleSheetExtensions
	{
		// Token: 0x060027D4 RID: 10196 RVA: 0x000A472C File Offset: 0x000A292C
		public static string ReadAsString(this StyleSheet sheet, StyleValueHandle handle)
		{
			string value = string.Empty;
			switch (handle.valueType)
			{
			case StyleValueType.Keyword:
				value = sheet.ReadKeyword(handle).ToUssString();
				break;
			case StyleValueType.Float:
				value = sheet.ReadFloat(handle).ToString(CultureInfo.InvariantCulture.NumberFormat);
				break;
			case StyleValueType.Dimension:
				value = sheet.ReadDimension(handle).ToString();
				break;
			case StyleValueType.Color:
				value = sheet.ReadColor(handle).ToString();
				break;
			case StyleValueType.ResourcePath:
				value = sheet.ReadResourcePath(handle);
				break;
			case StyleValueType.AssetReference:
				value = sheet.ReadAssetReference(handle).ToString();
				break;
			case StyleValueType.Enum:
				value = sheet.ReadEnum(handle);
				break;
			case StyleValueType.Variable:
				value = sheet.ReadVariable(handle);
				break;
			case StyleValueType.String:
				value = sheet.ReadString(handle);
				break;
			case StyleValueType.Function:
				value = sheet.ReadFunctionName(handle);
				break;
			case StyleValueType.CommaSeparator:
				value = ",";
				break;
			case StyleValueType.ScalableImage:
				value = sheet.ReadScalableImage(handle).ToString();
				break;
			default:
				value = "Error reading value type (" + handle.valueType.ToString() + ") at index " + handle.valueIndex.ToString();
				break;
			}
			return value;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000A4894 File Offset: 0x000A2A94
		public static bool IsVarFunction(this StyleValueHandle handle)
		{
			return handle.valueType == StyleValueType.Function && handle.valueIndex == 1;
		}
	}
}
