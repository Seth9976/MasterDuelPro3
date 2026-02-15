using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005C2 RID: 1474
	internal class StylePropertyValueMatcher : BaseStyleMatcher
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x000A501C File Offset: 0x000A321C
		private StylePropertyValue current
		{
			get
			{
				return base.hasCurrent ? this.m_Values[base.currentIndex] : default(StylePropertyValue);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x000A504D File Offset: 0x000A324D
		public override int valueCount
		{
			get
			{
				return this.m_Values.Count;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		public override bool isCurrentVariable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x000A505C File Offset: 0x000A325C
		public override bool isCurrentComma
		{
			get
			{
				return base.hasCurrent && this.m_Values[base.currentIndex].handle.valueType == StyleValueType.CommaSeparator;
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000A5098 File Offset: 0x000A3298
		public MatchResult Match(Expression exp, List<StylePropertyValue> values)
		{
			MatchResult result = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = values == null || values.Count == 0;
			MatchResult matchResult;
			if (flag)
			{
				result.errorCode = MatchResultErrorCode.EmptyValue;
				matchResult = result;
			}
			else
			{
				base.Initialize();
				this.m_Values = values;
				StyleValueHandle firstHandle = this.m_Values[0].handle;
				bool flag2 = firstHandle.valueType == StyleValueType.Keyword && firstHandle.valueIndex == 1;
				bool match;
				if (flag2)
				{
					base.MoveNext();
					match = true;
				}
				else
				{
					match = base.Match(exp);
				}
				bool flag3 = !match;
				if (flag3)
				{
					StyleSheet sheet = this.current.sheet;
					result.errorCode = MatchResultErrorCode.Syntax;
					result.errorValue = sheet.ReadAsString(this.current.handle);
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						StyleSheet sheet2 = this.current.sheet;
						result.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						result.errorValue = sheet2.ReadAsString(this.current.handle);
					}
				}
				matchResult = result;
			}
			return matchResult;
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000A51B4 File Offset: 0x000A33B4
		protected override bool MatchKeyword(string keyword)
		{
			StylePropertyValue value = this.current;
			bool flag = value.handle.valueType == StyleValueType.Keyword;
			bool flag2;
			if (flag)
			{
				StyleValueKeyword svk = (StyleValueKeyword)value.handle.valueIndex;
				flag2 = svk.ToUssString() == keyword.ToLowerInvariant();
			}
			else
			{
				bool flag3 = value.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					string s = value.sheet.ReadEnum(value.handle);
					flag2 = s == keyword.ToLowerInvariant();
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000A523C File Offset: 0x000A343C
		protected override bool MatchNumber()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000A5264 File Offset: 0x000A3464
		protected override bool MatchInteger()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000A528C File Offset: 0x000A348C
		protected override bool MatchLength()
		{
			StylePropertyValue value = this.current;
			bool flag = value.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = value.sheet.ReadDimension(value.handle);
				flag2 = dimension.unit == Dimension.Unit.Pixel;
			}
			else
			{
				bool flag3 = value.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float f = value.sheet.ReadFloat(value.handle);
					flag2 = Mathf.Approximately(0f, f);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000A5310 File Offset: 0x000A3510
		protected override bool MatchPercentage()
		{
			StylePropertyValue value = this.current;
			bool flag = value.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = value.sheet.ReadDimension(value.handle);
				flag2 = dimension.unit == Dimension.Unit.Percent;
			}
			else
			{
				bool flag3 = value.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float f = value.sheet.ReadFloat(value.handle);
					flag2 = Mathf.Approximately(0f, f);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000A5394 File Offset: 0x000A3594
		protected override bool MatchColor()
		{
			StylePropertyValue value = this.current;
			bool flag = value.handle.valueType == StyleValueType.Color;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = value.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					Color c = Color.clear;
					string colorName = value.sheet.ReadAsString(value.handle);
					bool flag4 = StyleSheetColor.TryGetColor(colorName.ToLowerInvariant(), out c);
					if (flag4)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000A540C File Offset: 0x000A360C
		protected override bool MatchResource()
		{
			return this.current.handle.valueType == StyleValueType.ResourcePath;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000A5434 File Offset: 0x000A3634
		protected override bool MatchUrl()
		{
			StyleValueType valueType = this.current.handle.valueType;
			return valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000A546C File Offset: 0x000A366C
		protected override bool MatchTime()
		{
			StylePropertyValue value = this.current;
			bool flag = value.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = value.sheet.ReadDimension(value.handle);
				flag2 = dimension.unit == Dimension.Unit.Second || dimension.unit == Dimension.Unit.Millisecond;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000A54C4 File Offset: 0x000A36C4
		protected override bool MatchCustomIdent()
		{
			StylePropertyValue value = this.current;
			bool flag = value.handle.valueType == StyleValueType.Enum;
			bool flag2;
			if (flag)
			{
				string ident = value.sheet.ReadAsString(value.handle);
				Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(ident);
				flag2 = match.Success && match.Length == ident.Length;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000A5530 File Offset: 0x000A3730
		protected override bool MatchAngle()
		{
			StylePropertyValue value = this.current;
			bool flag = value.handle.valueType == StyleValueType.Dimension;
			if (flag)
			{
				Dimension dimension = value.sheet.ReadDimension(value.handle);
				Dimension.Unit unit = dimension.unit;
				Dimension.Unit unit2 = unit;
				if (unit2 - Dimension.Unit.Degree <= 3)
				{
					return true;
				}
			}
			bool flag2 = value.handle.valueType == StyleValueType.Float;
			bool flag3;
			if (flag2)
			{
				float f = value.sheet.ReadFloat(value.handle);
				flag3 = Mathf.Approximately(0f, f);
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0400151F RID: 5407
		private List<StylePropertyValue> m_Values;
	}
}
