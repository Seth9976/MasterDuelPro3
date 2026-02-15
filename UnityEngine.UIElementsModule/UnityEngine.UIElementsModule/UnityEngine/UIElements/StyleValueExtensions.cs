using System;
using System.Collections.Generic;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x020003DD RID: 989
	internal static class StyleValueExtensions
	{
		// Token: 0x06001D82 RID: 7554 RVA: 0x0006C938 File Offset: 0x0006AB38
		internal static string DebugString<T>(this IStyleValue<T> styleValue)
		{
			return (styleValue.keyword != StyleKeyword.Undefined) ? string.Format("{0}", styleValue.keyword) : string.Format("{0}", styleValue.value);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0006C980 File Offset: 0x0006AB80
		internal static LayoutValue ToLayoutValue(this Length length)
		{
			bool flag = length.IsAuto();
			LayoutValue layoutValue;
			if (flag)
			{
				layoutValue = LayoutValue.Auto();
			}
			else
			{
				bool flag2 = length.IsNone();
				if (flag2)
				{
					layoutValue = float.NaN;
				}
				else
				{
					LengthUnit unit = length.unit;
					LengthUnit lengthUnit = unit;
					if (lengthUnit != LengthUnit.Pixel)
					{
						if (lengthUnit != LengthUnit.Percent)
						{
							Debug.LogAssertion(string.Format("Unexpected unit '{0}'", length.unit));
							layoutValue = float.NaN;
						}
						else
						{
							layoutValue = LayoutValue.Percent(length.value);
						}
					}
					else
					{
						layoutValue = LayoutValue.Point(length.value);
					}
				}
			}
			return layoutValue;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0006CA18 File Offset: 0x0006AC18
		internal static Length ToLength(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Length length;
			if (styleKeyword2 != StyleKeyword.Auto)
			{
				if (styleKeyword2 != StyleKeyword.None)
				{
					Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
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
			return length;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0006CA78 File Offset: 0x0006AC78
		internal static Rotate ToRotate(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Rotate rotate;
			if (styleKeyword2 != StyleKeyword.None)
			{
				Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
				rotate = default(Rotate);
			}
			else
			{
				rotate = Rotate.None();
			}
			return rotate;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0006CAC8 File Offset: 0x0006ACC8
		internal static Scale ToScale(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Scale scale;
			if (styleKeyword2 != StyleKeyword.None)
			{
				Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
				scale = default(Scale);
			}
			else
			{
				scale = Scale.None();
			}
			return scale;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0006CB18 File Offset: 0x0006AD18
		internal static Translate ToTranslate(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Translate translate;
			if (styleKeyword2 != StyleKeyword.None)
			{
				Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
				translate = default(Translate);
			}
			else
			{
				translate = Translate.None();
			}
			return translate;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0006CB68 File Offset: 0x0006AD68
		internal static Length ToLength(this StyleLength styleLength)
		{
			StyleKeyword keyword = styleLength.keyword;
			StyleKeyword styleKeyword = keyword;
			Length length;
			if (styleKeyword - StyleKeyword.Auto > 1)
			{
				length = styleLength.value;
			}
			else
			{
				length = styleLength.keyword.ToLength();
			}
			return length;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0006CBA2 File Offset: 0x0006ADA2
		internal static void CopyFrom<T>(this List<T> list, List<T> other)
		{
			list.Clear();
			list.AddRange(other);
		}
	}
}
