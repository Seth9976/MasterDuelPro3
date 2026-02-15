using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005B2 RID: 1458
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StylePropertyReader
	{
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x000A0F47 File Offset: 0x0009F147
		// (set) Token: 0x0600278B RID: 10123 RVA: 0x000A0F4F File Offset: 0x0009F14F
		public StyleProperty property { get; private set; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x000A0F58 File Offset: 0x0009F158
		// (set) Token: 0x0600278D RID: 10125 RVA: 0x000A0F60 File Offset: 0x0009F160
		public StylePropertyId propertyId { get; private set; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x000A0F69 File Offset: 0x0009F169
		// (set) Token: 0x0600278F RID: 10127 RVA: 0x000A0F71 File Offset: 0x0009F171
		public int valueCount { get; private set; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x000A0F7A File Offset: 0x0009F17A
		// (set) Token: 0x06002791 RID: 10129 RVA: 0x000A0F82 File Offset: 0x0009F182
		public float dpiScaling { get; private set; }

		// Token: 0x06002792 RID: 10130 RVA: 0x000A0F8C File Offset: 0x0009F18C
		public void SetContext(StyleSheet sheet, StyleComplexSelector selector, StyleVariableContext varContext, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = selector.rule.properties;
			this.m_PropertyIds = StyleSheetCache.GetPropertyIds(sheet, selector.ruleIndex);
			this.m_Resolver.variableContext = varContext;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000A0FE1 File Offset: 0x0009F1E1
		public void SetInlineContext(StyleSheet sheet, StyleProperty[] properties, StylePropertyId[] propertyIds, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = properties;
			this.m_PropertyIds = propertyIds;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000A100C File Offset: 0x0009F20C
		public StylePropertyId MoveNextProperty()
		{
			this.m_CurrentPropertyIndex++;
			this.m_CurrentValueIndex += this.valueCount;
			this.SetCurrentProperty();
			return this.propertyId;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000A104C File Offset: 0x0009F24C
		public StylePropertyValue GetValue(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index];
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000A1074 File Offset: 0x0009F274
		public StyleValueType GetValueType(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000A10A8 File Offset: 0x0009F2A8
		public bool IsValueType(int index, StyleValueType type)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == type;
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000A10E0 File Offset: 0x0009F2E0
		public bool IsKeyword(int index, StyleValueKeyword keyword)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			return value.handle.valueType == StyleValueType.Keyword && value.handle.valueIndex == (int)keyword;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000A1128 File Offset: 0x0009F328
		public string ReadAsString(int index)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			return value.sheet.ReadAsString(value.handle);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000A1160 File Offset: 0x0009F360
		public Length ReadLength(int index)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = value.handle.valueType == StyleValueType.Keyword;
			Length length;
			if (flag)
			{
				StyleValueKeyword keyword = (StyleValueKeyword)value.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = keyword;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.Auto)
				{
					if (styleValueKeyword2 != StyleValueKeyword.None)
					{
						length = default(Length);
					}
					else
					{
						length = Length.None();
					}
				}
				else
				{
					length = Length.Auto();
				}
			}
			else
			{
				length = value.sheet.ReadDimension(value.handle).ToLength();
			}
			return length;
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000A11F8 File Offset: 0x0009F3F8
		public TimeValue ReadTimeValue(int index)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			return value.sheet.ReadDimension(value.handle).ToTime();
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000A1238 File Offset: 0x0009F438
		public Translate ReadTranslate(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue val3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadTranslate(this.valueCount, val, val2, val3);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000A12C4 File Offset: 0x0009F4C4
		public TransformOrigin ReadTransformOrigin(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue val3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadTransformOrigin(this.valueCount, val, val2, val3);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000A1350 File Offset: 0x0009F550
		public Rotate ReadRotate(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue val3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			StylePropertyValue val4 = ((this.valueCount > 3) ? this.m_Values[this.m_CurrentValueIndex + index + 3] : default(StylePropertyValue));
			return StylePropertyReader.ReadRotate(this.valueCount, val, val2, val3, val4);
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000A1408 File Offset: 0x0009F608
		public Scale ReadScale(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue val3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadScale(this.valueCount, val, val2, val3);
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000A1494 File Offset: 0x0009F694
		public float ReadFloat(int index)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			return value.sheet.ReadFloat(value.handle);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000A14CC File Offset: 0x0009F6CC
		public int ReadInt(int index)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			return (int)value.sheet.ReadFloat(value.handle);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000A1504 File Offset: 0x0009F704
		public Color ReadColor(int index)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			Color c = Color.clear;
			bool flag = value.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				string colorName = value.sheet.ReadAsString(value.handle);
				StyleSheetColor.TryGetColor(colorName.ToLowerInvariant(), out c);
			}
			else
			{
				c = value.sheet.ReadColor(value.handle);
			}
			return c;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000A1580 File Offset: 0x0009F780
		public int ReadEnum(StyleEnumType enumType, int index)
		{
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueHandle handle = value.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string enumString;
			if (flag)
			{
				StyleValueKeyword keyword = value.sheet.ReadKeyword(handle);
				enumString = keyword.ToUssString();
			}
			else
			{
				enumString = value.sheet.ReadEnum(handle);
			}
			int intValue;
			StylePropertyUtil.TryGetEnumIntValue(enumType, enumString, out intValue);
			return intValue;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000A15F8 File Offset: 0x0009F7F8
		public FontDefinition ReadFontDefinition(int index)
		{
			FontAsset fontAsset = null;
			Font font = null;
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = value.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + value.handle.valueType.ToString());
					}
					else
					{
						font = value.sheet.ReadAssetReference(value.handle) as Font;
						bool flag = font == null;
						if (flag)
						{
							fontAsset = value.sheet.ReadAssetReference(value.handle) as FontAsset;
						}
					}
				}
				else
				{
					string path = value.sheet.ReadResourcePath(value.handle);
					bool flag2 = !string.IsNullOrEmpty(path);
					if (flag2)
					{
						font = Panel.LoadResource(path, typeof(Font), this.dpiScaling) as Font;
						bool flag3 = font == null;
						if (flag3)
						{
							fontAsset = Panel.LoadResource(path, typeof(FontAsset), this.dpiScaling) as FontAsset;
						}
					}
					bool flag4 = fontAsset == null && font == null;
					if (flag4)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", path));
					}
				}
			}
			else
			{
				bool flag5 = value.handle.valueIndex != 6;
				if (flag5)
				{
					string text = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)value.handle.valueIndex;
					Debug.LogWarning(text + valueIndex.ToString());
				}
			}
			bool flag6 = font != null;
			FontDefinition sfd;
			if (flag6)
			{
				sfd = FontDefinition.FromFont(font);
			}
			else
			{
				bool flag7 = fontAsset != null;
				if (flag7)
				{
					sfd = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					sfd = default(FontDefinition);
				}
			}
			return sfd;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000A17D8 File Offset: 0x0009F9D8
		public Font ReadFont(int index)
		{
			Font font = null;
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = value.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + value.handle.valueType.ToString());
					}
					else
					{
						font = value.sheet.ReadAssetReference(value.handle) as Font;
					}
				}
				else
				{
					string path = value.sheet.ReadResourcePath(value.handle);
					bool flag = !string.IsNullOrEmpty(path);
					if (flag)
					{
						font = Panel.LoadResource(path, typeof(Font), this.dpiScaling) as Font;
					}
					bool flag2 = font == null;
					if (flag2)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", path));
					}
				}
			}
			else
			{
				bool flag3 = value.handle.valueIndex != 6;
				if (flag3)
				{
					string text = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)value.handle.valueIndex;
					Debug.LogWarning(text + valueIndex.ToString());
				}
			}
			return font;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000A191C File Offset: 0x0009FB1C
		public Background ReadBackground(int index)
		{
			ImageSource source = default(ImageSource);
			StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = value.handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				bool flag2 = value.handle.valueIndex != 6;
				if (flag2)
				{
					string text = "Invalid keyword for image source ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)value.handle.valueIndex;
					Debug.LogWarning(text + valueIndex.ToString());
				}
			}
			else
			{
				bool flag3 = !StylePropertyReader.TryGetImageSourceFromValue(value, this.dpiScaling, out source);
				if (flag3)
				{
				}
			}
			bool flag4 = source.texture != null;
			Background background;
			if (flag4)
			{
				background = Background.FromTexture2D(source.texture);
			}
			else
			{
				bool flag5 = source.sprite != null;
				if (flag5)
				{
					background = Background.FromSprite(source.sprite);
				}
				else
				{
					bool flag6 = source.vectorImage != null;
					if (flag6)
					{
						background = Background.FromVectorImage(source.vectorImage);
					}
					else
					{
						bool flag7 = source.renderTexture != null;
						if (flag7)
						{
							background = Background.FromRenderTexture(source.renderTexture);
						}
						else
						{
							background = default(Background);
						}
					}
				}
			}
			return background;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000A1A4C File Offset: 0x0009FC4C
		public Cursor ReadCursor(int index)
		{
			float hotspotX = 0f;
			float hotspotY = 0f;
			int cursorId = 0;
			Texture2D texture = null;
			StyleValueType valueType = this.GetValueType(index);
			bool isCustom = valueType == StyleValueType.ResourcePath || valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage || valueType == StyleValueType.MissingAssetReference;
			bool flag = isCustom;
			if (flag)
			{
				bool flag2 = this.valueCount < 1;
				if (flag2)
				{
					Debug.LogWarning(string.Format("USS 'cursor' has invalid value at {0}.", index));
				}
				else
				{
					ImageSource source = default(ImageSource);
					StylePropertyValue value = this.GetValue(index);
					bool flag3 = StylePropertyReader.TryGetImageSourceFromValue(value, this.dpiScaling, out source);
					if (flag3)
					{
						texture = source.texture;
						bool flag4 = this.valueCount >= 3;
						if (flag4)
						{
							StylePropertyValue valueX = this.GetValue(index + 1);
							StylePropertyValue valueY = this.GetValue(index + 2);
							bool flag5 = valueX.handle.valueType != StyleValueType.Float || valueY.handle.valueType != StyleValueType.Float;
							if (flag5)
							{
								Debug.LogWarning("USS 'cursor' property requires two integers for the hot spot value.");
							}
							else
							{
								hotspotX = valueX.sheet.ReadFloat(valueX.handle);
								hotspotY = valueY.sheet.ReadFloat(valueY.handle);
							}
						}
					}
				}
			}
			else
			{
				bool flag6 = StylePropertyReader.getCursorIdFunc != null;
				if (flag6)
				{
					StylePropertyValue value2 = this.GetValue(index);
					cursorId = StylePropertyReader.getCursorIdFunc(value2.sheet, value2.handle);
				}
			}
			return new Cursor
			{
				texture = texture,
				hotspot = new Vector2(hotspotX, hotspotY),
				defaultCursorId = cursorId
			};
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000A1BF0 File Offset: 0x0009FDF0
		public TextShadow ReadTextShadow(int index)
		{
			float offsetX = 0f;
			float offsetY = 0f;
			float blurRadius = 0f;
			Color color = Color.clear;
			bool flag = this.valueCount >= 2;
			if (flag)
			{
				int i = index;
				StyleValueType valueType = this.GetValueType(i);
				bool isColorRead = false;
				bool flag2 = valueType == StyleValueType.Color || valueType == StyleValueType.Enum;
				if (flag2)
				{
					color = this.ReadColor(i++);
					isColorRead = true;
				}
				bool flag3 = i + 1 < this.valueCount;
				if (flag3)
				{
					valueType = this.GetValueType(i);
					StyleValueType valueType2 = this.GetValueType(i + 1);
					bool flag4 = (valueType == StyleValueType.Dimension || valueType == StyleValueType.Float) && (valueType2 == StyleValueType.Dimension || valueType2 == StyleValueType.Float);
					if (flag4)
					{
						StylePropertyValue valueX = this.GetValue(i++);
						StylePropertyValue valueY = this.GetValue(i++);
						offsetX = valueX.sheet.ReadDimension(valueX.handle).value;
						offsetY = valueY.sheet.ReadDimension(valueY.handle).value;
					}
				}
				bool flag5 = i < this.valueCount;
				if (flag5)
				{
					valueType = this.GetValueType(i);
					bool flag6 = valueType == StyleValueType.Dimension || valueType == StyleValueType.Float;
					if (flag6)
					{
						StylePropertyValue valueBlur = this.GetValue(i++);
						blurRadius = valueBlur.sheet.ReadDimension(valueBlur.handle).value;
					}
					else
					{
						bool flag7 = valueType == StyleValueType.Color || valueType == StyleValueType.Enum;
						if (flag7)
						{
							bool flag8 = !isColorRead;
							if (flag8)
							{
								color = this.ReadColor(i);
							}
						}
					}
				}
				bool flag9 = i < this.valueCount;
				if (flag9)
				{
					valueType = this.GetValueType(i);
					bool flag10 = valueType == StyleValueType.Color || valueType == StyleValueType.Enum;
					if (flag10)
					{
						bool flag11 = !isColorRead;
						if (flag11)
						{
							color = this.ReadColor(i);
						}
					}
				}
			}
			return new TextShadow
			{
				offset = new Vector2(offsetX, offsetY),
				blurRadius = blurRadius,
				color = color
			};
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000A1E00 File Offset: 0x000A0000
		public BackgroundPosition ReadBackgroundPositionX(int index)
		{
			return this.ReadBackgroundPosition(index, BackgroundPositionKeyword.Left);
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000A1E1C File Offset: 0x000A001C
		public BackgroundPosition ReadBackgroundPositionY(int index)
		{
			return this.ReadBackgroundPosition(index, BackgroundPositionKeyword.Top);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000A1E38 File Offset: 0x000A0038
		private BackgroundPosition ReadBackgroundPosition(int index, BackgroundPositionKeyword keyword)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			return StylePropertyReader.ReadBackgroundPosition(this.valueCount, val, val2, keyword);
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000A1E98 File Offset: 0x000A0098
		public BackgroundRepeat ReadBackgroundRepeat(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			return StylePropertyReader.ReadBackgroundRepeat(this.valueCount, val, val2);
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000A1EF8 File Offset: 0x000A00F8
		public BackgroundSize ReadBackgroundSize(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			return StylePropertyReader.ReadBackgroundSize(this.valueCount, val, val2);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000A1F58 File Offset: 0x000A0158
		public void ReadListEasingFunction(List<EasingFunction> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
				StyleValueHandle handle = value.handle;
				bool flag = handle.valueType == StyleValueType.Enum;
				if (flag)
				{
					string enumString = value.sheet.ReadEnum(handle);
					int intValue;
					StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.EasingMode, enumString, out intValue);
					list.Add(new EasingFunction((EasingMode)intValue));
					index++;
				}
				bool flag2 = index < this.valueCount;
				if (flag2)
				{
					bool flag3 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag3)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000A2018 File Offset: 0x000A0218
		public void ReadListTimeValue(List<TimeValue> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
				TimeValue time = value.sheet.ReadDimension(value.handle).ToTime();
				list.Add(time);
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000A20BC File Offset: 0x000A02BC
		public void ReadListStylePropertyName(List<StylePropertyName> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue value = this.m_Values[this.m_CurrentValueIndex + index];
				string str = value.sheet.ReadAsString(value.handle);
				list.Add(new StylePropertyName(str));
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000A215C File Offset: 0x000A035C
		private void LoadProperties()
		{
			this.m_CurrentPropertyIndex = 0;
			this.m_CurrentValueIndex = 0;
			this.m_Values.Clear();
			this.m_ValueCount.Clear();
			foreach (StyleProperty sp in this.m_Properties)
			{
				int count = 0;
				bool valid = true;
				bool requireVariableResolve = sp.requireVariableResolve;
				if (requireVariableResolve)
				{
					this.m_Resolver.Init(sp, this.m_Sheet, sp.values);
					int i = 0;
					while (i < sp.values.Length && valid)
					{
						StyleValueHandle handle = sp.values[i];
						bool flag = handle.IsVarFunction();
						if (flag)
						{
							valid = this.m_Resolver.ResolveVarFunction(ref i);
						}
						else
						{
							this.m_Resolver.AddValue(handle);
						}
						i++;
					}
					bool flag2 = valid && this.m_Resolver.ValidateResolvedValues();
					if (flag2)
					{
						this.m_Values.AddRange(this.m_Resolver.resolvedValues);
						count += this.m_Resolver.resolvedValues.Count;
					}
					else
					{
						StyleValueHandle unsetHandle = new StyleValueHandle
						{
							valueType = StyleValueType.Keyword,
							valueIndex = 3
						};
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = unsetHandle
						});
						count++;
					}
				}
				else
				{
					count = sp.values.Length;
					for (int j = 0; j < count; j++)
					{
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = sp.values[j]
						});
					}
				}
				this.m_ValueCount.Add(count);
			}
			this.SetCurrentProperty();
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000A2348 File Offset: 0x000A0548
		private void SetCurrentProperty()
		{
			bool flag = this.m_CurrentPropertyIndex < this.m_PropertyIds.Length;
			if (flag)
			{
				this.property = this.m_Properties[this.m_CurrentPropertyIndex];
				this.propertyId = this.m_PropertyIds[this.m_CurrentPropertyIndex];
				this.valueCount = this.m_ValueCount[this.m_CurrentPropertyIndex];
			}
			else
			{
				this.property = null;
				this.propertyId = StylePropertyId.Unknown;
				this.valueCount = 0;
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000A23C8 File Offset: 0x000A05C8
		public static TransformOrigin ReadTransformOrigin(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue zVvalue)
		{
			Length x = Length.Percent(50f);
			Length y = Length.Percent(50f);
			float z = 0f;
			switch (valCount)
			{
			case 1:
			{
				bool isVertical;
				bool isHorizontal;
				Length val3 = StylePropertyReader.ReadTransformOriginEnum(val1, out isVertical, out isHorizontal);
				bool flag = isHorizontal;
				if (flag)
				{
					x = val3;
				}
				else
				{
					y = val3;
				}
				goto IL_00F3;
			}
			case 2:
				break;
			case 3:
			{
				bool flag2 = zVvalue.handle.valueType == StyleValueType.Dimension || zVvalue.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					Dimension dimension = zVvalue.sheet.ReadDimension(zVvalue.handle);
					z = dimension.value;
				}
				break;
			}
			default:
				goto IL_00F3;
			}
			bool isVertical2;
			bool isHorizontal2;
			Length len = StylePropertyReader.ReadTransformOriginEnum(val1, out isVertical2, out isHorizontal2);
			bool isVertical3;
			bool isHorizontal3;
			Length len2 = StylePropertyReader.ReadTransformOriginEnum(val2, out isVertical3, out isHorizontal3);
			bool flag3 = !isHorizontal2 || !isVertical3;
			if (flag3)
			{
				bool flag4 = isHorizontal3 && isVertical2;
				if (flag4)
				{
					x = len2;
					y = len;
				}
			}
			else
			{
				x = len;
				y = len2;
			}
			IL_00F3:
			return new TransformOrigin(x, y, z);
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000A24D8 File Offset: 0x000A06D8
		private static Length ReadTransformOriginEnum(StylePropertyValue value, out bool isVertical, out bool isHorizontal)
		{
			bool flag = value.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				switch (StylePropertyReader.ReadEnum(StyleEnumType.TransformOriginOffset, value))
				{
				case 1:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(0f);
				case 2:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(100f);
				case 3:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(0f);
				case 4:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(100f);
				case 5:
					isVertical = true;
					isHorizontal = true;
					return Length.Percent(50f);
				}
			}
			else
			{
				bool flag2 = value.handle.valueType == StyleValueType.Dimension || value.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					isVertical = true;
					isHorizontal = true;
					return value.sheet.ReadDimension(value.handle).ToLength();
				}
			}
			isVertical = false;
			isHorizontal = false;
			return Length.Percent(50f);
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000A25FC File Offset: 0x000A07FC
		public static Translate ReadTranslate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Translate translate;
			if (flag)
			{
				translate = Translate.None();
			}
			else
			{
				Length x = 0f;
				Length y = 0f;
				float z = 0f;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						x = val1.sheet.ReadDimension(val1.handle).ToLength();
						y = val1.sheet.ReadDimension(val1.handle).ToLength();
					}
					goto IL_01A6;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						Dimension dimension = val3.sheet.ReadDimension(val3.handle);
						z = dimension.value;
					}
					break;
				}
				default:
					goto IL_01A6;
				}
				bool flag4 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					x = val1.sheet.ReadDimension(val1.handle).ToLength();
				}
				bool flag5 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					y = val2.sheet.ReadDimension(val2.handle).ToLength();
				}
				IL_01A6:
				translate = new Translate(x, y, z);
			}
			return translate;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000A27C0 File Offset: 0x000A09C0
		public static Scale ReadScale(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Scale scale2;
			if (flag)
			{
				scale2 = Scale.None();
			}
			else
			{
				Vector3 scale = Vector3.one;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						scale.x = val1.sheet.ReadFloat(val1.handle);
						scale.y = scale.x;
					}
					goto IL_0173;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						scale.z = val3.sheet.ReadFloat(val3.handle);
					}
					break;
				}
				default:
					goto IL_0173;
				}
				bool flag4 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					scale.x = val1.sheet.ReadFloat(val1.handle);
				}
				bool flag5 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					scale.y = val2.sheet.ReadFloat(val2.handle);
				}
				IL_0173:
				scale2 = new Scale(scale);
			}
			return scale2;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000A294C File Offset: 0x000A0B4C
		public static Rotate ReadRotate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3, StylePropertyValue val4)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Rotate rotate;
			if (flag)
			{
				rotate = Rotate.None();
			}
			else
			{
				Rotate rot = Rotate.Initial();
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension;
					if (flag2)
					{
						rot.angle = StylePropertyReader.ReadAngle(val1);
					}
					break;
				}
				case 2:
					rot.angle = StylePropertyReader.ReadAngle(val2);
					switch (StylePropertyReader.ReadEnum(StyleEnumType.Axis, val1))
					{
					case 0:
						rot.axis = new Vector3(1f, 0f, 0f);
						break;
					case 1:
						rot.axis = new Vector3(0f, 1f, 0f);
						break;
					case 2:
						rot.axis = new Vector3(0f, 0f, 1f);
						break;
					}
					break;
				case 4:
					rot.angle = StylePropertyReader.ReadAngle(val4);
					rot.axis = new Vector3(val1.sheet.ReadFloat(val1.handle), val1.sheet.ReadFloat(val2.handle), val1.sheet.ReadFloat(val3.handle));
					break;
				}
				rotate = rot;
			}
			return rotate;
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000A2AC4 File Offset: 0x000A0CC4
		private static bool TryReadEnum(StyleEnumType enumType, StylePropertyValue value, out int intValue)
		{
			StyleValueHandle handle = value.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string enumString;
			if (flag)
			{
				StyleValueKeyword keyword = value.sheet.ReadKeyword(handle);
				enumString = keyword.ToUssString();
			}
			else
			{
				enumString = value.sheet.ReadEnum(handle);
			}
			return StylePropertyUtil.TryGetEnumIntValue(enumType, enumString, out intValue);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000A2B20 File Offset: 0x000A0D20
		private static int ReadEnum(StyleEnumType enumType, StylePropertyValue value)
		{
			StyleValueHandle handle = value.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string enumString;
			if (flag)
			{
				StyleValueKeyword keyword = value.sheet.ReadKeyword(handle);
				enumString = keyword.ToUssString();
			}
			else
			{
				enumString = value.sheet.ReadEnum(handle);
			}
			int intValue;
			StylePropertyUtil.TryGetEnumIntValue(enumType, enumString, out intValue);
			return intValue;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000A2B80 File Offset: 0x000A0D80
		public static Angle ReadAngle(StylePropertyValue value)
		{
			bool flag = value.handle.valueType == StyleValueType.Keyword;
			Angle angle;
			if (flag)
			{
				StyleValueKeyword keyword = (StyleValueKeyword)value.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = keyword;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.None)
				{
					angle = default(Angle);
				}
				else
				{
					angle = Angle.None();
				}
			}
			else
			{
				angle = value.sheet.ReadDimension(value.handle).ToAngle();
			}
			return angle;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000A2BF0 File Offset: 0x000A0DF0
		public static BackgroundPosition ReadBackgroundPosition(int valCount, StylePropertyValue val1, StylePropertyValue val2, BackgroundPositionKeyword keyword)
		{
			bool flag = valCount == 1;
			if (flag)
			{
				bool flag2 = val1.handle.valueType == StyleValueType.Enum;
				if (flag2)
				{
					return new BackgroundPosition((BackgroundPositionKeyword)StylePropertyReader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, val1));
				}
				bool flag3 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					return new BackgroundPosition(keyword, val1.sheet.ReadDimension(val1.handle).ToLength());
				}
			}
			else
			{
				bool flag4 = valCount == 2;
				if (flag4)
				{
					bool flag5 = val1.handle.valueType == StyleValueType.Enum && (val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float);
					if (flag5)
					{
						return new BackgroundPosition((BackgroundPositionKeyword)StylePropertyReader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, val1), val1.sheet.ReadDimension(val2.handle).ToLength());
					}
				}
			}
			return default(BackgroundPosition);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000A2CF8 File Offset: 0x000A0EF8
		public static BackgroundRepeat ReadBackgroundRepeat(int valCount, StylePropertyValue val1, StylePropertyValue val2)
		{
			BackgroundRepeat backgroundRepeat = default(BackgroundRepeat);
			bool flag = valCount == 1;
			if (flag)
			{
				int enumValue;
				bool flag2 = StylePropertyReader.TryReadEnum(StyleEnumType.RepeatXY, val1, out enumValue);
				if (flag2)
				{
					bool flag3 = enumValue == 0;
					if (flag3)
					{
						backgroundRepeat.x = Repeat.Repeat;
						backgroundRepeat.y = Repeat.NoRepeat;
					}
					else
					{
						bool flag4 = enumValue == 1;
						if (flag4)
						{
							backgroundRepeat.x = Repeat.NoRepeat;
							backgroundRepeat.y = Repeat.Repeat;
						}
					}
				}
				else
				{
					backgroundRepeat.x = (Repeat)StylePropertyReader.ReadEnum(StyleEnumType.Repeat, val1);
					backgroundRepeat.y = backgroundRepeat.x;
				}
			}
			else
			{
				backgroundRepeat.x = (Repeat)StylePropertyReader.ReadEnum(StyleEnumType.Repeat, val1);
				backgroundRepeat.y = (Repeat)StylePropertyReader.ReadEnum(StyleEnumType.Repeat, val2);
			}
			return backgroundRepeat;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000A2DAC File Offset: 0x000A0FAC
		public static BackgroundSize ReadBackgroundSize(int valCount, StylePropertyValue val1, StylePropertyValue val2)
		{
			BackgroundSize BackgroundSize = default(BackgroundSize);
			bool flag = valCount == 1;
			if (flag)
			{
				bool flag2 = val1.handle.valueType == StyleValueType.Keyword;
				if (flag2)
				{
					bool flag3 = val1.handle.valueIndex == 2;
					if (flag3)
					{
						BackgroundSize.x = Length.Auto();
						BackgroundSize.y = Length.Auto();
					}
					else
					{
						bool flag4 = val1.handle.valueIndex == 7;
						if (flag4)
						{
							BackgroundSize.sizeType = BackgroundSizeType.Cover;
						}
						else
						{
							bool flag5 = val1.handle.valueIndex == 8;
							if (flag5)
							{
								BackgroundSize.sizeType = BackgroundSizeType.Contain;
							}
						}
					}
				}
				else
				{
					bool flag6 = val1.handle.valueType == StyleValueType.Enum;
					if (flag6)
					{
						BackgroundSize.sizeType = (BackgroundSizeType)StylePropertyReader.ReadEnum(StyleEnumType.BackgroundSizeType, val1);
					}
					else
					{
						bool flag7 = val1.handle.valueType == StyleValueType.Dimension;
						if (flag7)
						{
							BackgroundSize.x = val1.sheet.ReadDimension(val1.handle).ToLength();
							BackgroundSize.y = Length.Auto();
						}
					}
				}
			}
			else
			{
				bool flag8 = valCount == 2;
				if (flag8)
				{
					bool flag9 = val1.handle.valueType == StyleValueType.Keyword;
					if (flag9)
					{
						bool flag10 = val1.handle.valueIndex == 2;
						if (flag10)
						{
							BackgroundSize.x = Length.Auto();
						}
					}
					else
					{
						bool flag11 = val1.handle.valueType == StyleValueType.Dimension;
						if (flag11)
						{
							BackgroundSize.x = val1.sheet.ReadDimension(val1.handle).ToLength();
						}
					}
					bool flag12 = val2.handle.valueType == StyleValueType.Keyword;
					if (flag12)
					{
						bool flag13 = val2.handle.valueIndex == 2;
						if (flag13)
						{
							BackgroundSize.y = Length.Auto();
						}
					}
					else
					{
						bool flag14 = val2.handle.valueType == StyleValueType.Dimension;
						if (flag14)
						{
							BackgroundSize.y = val2.sheet.ReadDimension(val2.handle).ToLength();
						}
					}
				}
			}
			return BackgroundSize;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000A2FCC File Offset: 0x000A11CC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static bool TryGetImageSourceFromValue(StylePropertyValue propertyValue, float dpiScaling, out ImageSource source)
		{
			source = default(ImageSource);
			StyleValueType valueType = propertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType <= StyleValueType.AssetReference)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType == StyleValueType.AssetReference)
					{
						Object o = propertyValue.sheet.ReadAssetReference(propertyValue.handle);
						source.texture = o as Texture2D;
						source.sprite = o as Sprite;
						source.vectorImage = o as VectorImage;
						source.renderTexture = o as RenderTexture;
						bool flag = source.IsNull();
						if (flag)
						{
							Debug.LogWarning("Invalid image specified");
							return false;
						}
						goto IL_0254;
					}
				}
				else
				{
					string path = propertyValue.sheet.ReadResourcePath(propertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(path);
					if (flag2)
					{
						source.sprite = Panel.LoadResource(path, typeof(Sprite), dpiScaling) as Sprite;
						bool flag3 = source.IsNull();
						if (flag3)
						{
							source.texture = Panel.LoadResource(path, typeof(Texture2D), dpiScaling) as Texture2D;
						}
						bool flag4 = source.IsNull();
						if (flag4)
						{
							source.vectorImage = Panel.LoadResource(path, typeof(VectorImage), dpiScaling) as VectorImage;
						}
						bool flag5 = source.IsNull();
						if (flag5)
						{
							source.renderTexture = Panel.LoadResource(path, typeof(RenderTexture), dpiScaling) as RenderTexture;
						}
					}
					bool flag6 = source.IsNull();
					if (flag6)
					{
						Debug.LogWarning(string.Format("Image not found for path: {0}", path));
						return false;
					}
					goto IL_0254;
				}
			}
			else if (styleValueType != StyleValueType.ScalableImage)
			{
				if (styleValueType == StyleValueType.MissingAssetReference)
				{
					return false;
				}
			}
			else
			{
				ScalableImage img = propertyValue.sheet.ReadScalableImage(propertyValue.handle);
				bool flag7 = img.normalImage == null && img.highResolutionImage == null;
				if (flag7)
				{
					Debug.LogWarning("Invalid scalable image specified");
					return false;
				}
				source.texture = img.normalImage;
				bool flag8 = !Mathf.Approximately(dpiScaling % 1f, 0f);
				if (flag8)
				{
					source.texture.filterMode = FilterMode.Bilinear;
				}
				goto IL_0254;
			}
			Debug.LogWarning("Invalid value for image texture " + propertyValue.handle.valueType.ToString());
			return false;
			IL_0254:
			return true;
		}

		// Token: 0x040014F0 RID: 5360
		internal static StylePropertyReader.GetCursorIdFunction getCursorIdFunc;

		// Token: 0x040014F1 RID: 5361
		private List<StylePropertyValue> m_Values = new List<StylePropertyValue>();

		// Token: 0x040014F2 RID: 5362
		private List<int> m_ValueCount = new List<int>();

		// Token: 0x040014F3 RID: 5363
		private StyleVariableResolver m_Resolver = new StyleVariableResolver();

		// Token: 0x040014F4 RID: 5364
		private StyleSheet m_Sheet;

		// Token: 0x040014F5 RID: 5365
		private StyleProperty[] m_Properties;

		// Token: 0x040014F6 RID: 5366
		private StylePropertyId[] m_PropertyIds;

		// Token: 0x040014F7 RID: 5367
		private int m_CurrentValueIndex;

		// Token: 0x040014F8 RID: 5368
		private int m_CurrentPropertyIndex;

		// Token: 0x020005B3 RID: 1459
		// (Invoke) Token: 0x060027C1 RID: 10177
		internal delegate int GetCursorIdFunction(StyleSheet sheet, StyleValueHandle handle);
	}
}
