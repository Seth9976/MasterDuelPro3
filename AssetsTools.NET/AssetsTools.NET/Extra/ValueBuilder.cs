using System;
using System.Collections.Generic;

namespace AssetsTools.NET.Extra
{
	// Token: 0x0200008A RID: 138
	public class ValueBuilder
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0001C004 File Offset: 0x0001A204
		public static AssetTypeValueField DefaultValueFieldFromArrayTemplate(AssetTypeValueField arrayField)
		{
			return ValueBuilder.DefaultValueFieldFromArrayTemplate(arrayField.TemplateField);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001C024 File Offset: 0x0001A224
		public static AssetTypeValueField DefaultValueFieldFromArrayTemplate(AssetTypeTemplateField arrayField)
		{
			bool flag = !arrayField.IsArray;
			AssetTypeValueField assetTypeValueField;
			if (flag)
			{
				assetTypeValueField = null;
			}
			else
			{
				AssetTypeTemplateField assetTypeTemplateField = arrayField.Children[1];
				assetTypeValueField = ValueBuilder.DefaultValueFieldFromTemplate(assetTypeTemplateField);
			}
			return assetTypeValueField;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001C05C File Offset: 0x0001A25C
		public static AssetTypeValueField DefaultValueFieldFromTemplate(AssetTypeTemplateField templateField)
		{
			List<AssetTypeTemplateField> children = templateField.Children;
			bool flag = templateField.IsArray || templateField.ValueType == AssetValueType.String;
			List<AssetTypeValueField> list;
			if (flag)
			{
				list = new List<AssetTypeValueField>(0);
			}
			else
			{
				list = new List<AssetTypeValueField>(children.Count);
				for (int i = 0; i < children.Count; i++)
				{
					list.Add(ValueBuilder.DefaultValueFieldFromTemplate(children[i]));
				}
			}
			AssetTypeValue assetTypeValue = ValueBuilder.DefaultValueFromTemplate(templateField);
			return new AssetTypeValueField
			{
				Children = list,
				TemplateField = templateField,
				Value = assetTypeValue
			};
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001C104 File Offset: 0x0001A304
		public static AssetTypeValue DefaultValueFromTemplate(AssetTypeTemplateField templateField)
		{
			object obj;
			switch (templateField.ValueType)
			{
			case AssetValueType.Bool:
				obj = false;
				break;
			case AssetValueType.Int8:
				obj = 0;
				break;
			case AssetValueType.UInt8:
				obj = 0;
				break;
			case AssetValueType.Int16:
				obj = 0;
				break;
			case AssetValueType.UInt16:
				obj = 0;
				break;
			case AssetValueType.Int32:
				obj = 0;
				break;
			case AssetValueType.UInt32:
				obj = 0U;
				break;
			case AssetValueType.Int64:
				obj = 0L;
				break;
			case AssetValueType.UInt64:
				obj = 0UL;
				break;
			case AssetValueType.Float:
				obj = 0f;
				break;
			case AssetValueType.Double:
				obj = 0.0;
				break;
			case AssetValueType.String:
			case AssetValueType.ByteArray:
				obj = new byte[0];
				break;
			case AssetValueType.Array:
				obj = default(AssetTypeArrayInfo);
				break;
			case AssetValueType.ManagedReferencesRegistry:
				obj = new ManagedReferencesRegistry();
				break;
			default:
				obj = null;
				break;
			}
			bool flag = obj == null && templateField.IsArray;
			AssetTypeValue assetTypeValue;
			if (flag)
			{
				obj = default(AssetTypeArrayInfo);
				assetTypeValue = new AssetTypeValue(AssetValueType.Array, obj);
			}
			else
			{
				assetTypeValue = new AssetTypeValue(templateField.ValueType, obj);
			}
			return assetTypeValue;
		}
	}
}
