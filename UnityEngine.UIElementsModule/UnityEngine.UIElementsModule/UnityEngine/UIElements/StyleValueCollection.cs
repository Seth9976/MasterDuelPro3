using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B9 RID: 953
	internal class StyleValueCollection
	{
		// Token: 0x06001C1E RID: 7198 RVA: 0x00069BC0 File Offset: 0x00067DC0
		public StyleLength GetStyleLength(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleLength styleLength;
			if (flag)
			{
				styleLength = new StyleLength(inline.length, inline.keyword);
			}
			else
			{
				styleLength = StyleKeyword.Null;
			}
			return styleLength;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00069C04 File Offset: 0x00067E04
		public StyleFloat GetStyleFloat(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleFloat styleFloat;
			if (flag)
			{
				styleFloat = new StyleFloat(inline.number, inline.keyword);
			}
			else
			{
				styleFloat = StyleKeyword.Null;
			}
			return styleFloat;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00069C48 File Offset: 0x00067E48
		public StyleInt GetStyleInt(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleInt styleInt;
			if (flag)
			{
				styleInt = new StyleInt((int)inline.number, inline.keyword);
			}
			else
			{
				styleInt = StyleKeyword.Null;
			}
			return styleInt;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00069C8C File Offset: 0x00067E8C
		public StyleColor GetStyleColor(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleColor styleColor;
			if (flag)
			{
				styleColor = new StyleColor(inline.color, inline.keyword);
			}
			else
			{
				styleColor = StyleKeyword.Null;
			}
			return styleColor;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00069CD0 File Offset: 0x00067ED0
		public StyleBackground GetStyleBackground(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			if (flag)
			{
				Texture2D texture = (inline.resource.IsAllocated ? (inline.resource.Target as Texture2D) : null);
				bool flag2 = texture != null;
				if (flag2)
				{
					return new StyleBackground(texture, inline.keyword);
				}
				Sprite sprite = (inline.resource.IsAllocated ? (inline.resource.Target as Sprite) : null);
				bool flag3 = sprite != null;
				if (flag3)
				{
					return new StyleBackground(sprite, inline.keyword);
				}
				VectorImage vectorImage = (inline.resource.IsAllocated ? (inline.resource.Target as VectorImage) : null);
				bool flag4 = vectorImage != null;
				if (flag4)
				{
					return new StyleBackground(vectorImage, inline.keyword);
				}
			}
			return StyleKeyword.Null;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00069DCC File Offset: 0x00067FCC
		public StyleBackgroundPosition GetStyleBackgroundPosition(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleBackgroundPosition styleBackgroundPosition;
			if (flag)
			{
				styleBackgroundPosition = new StyleBackgroundPosition(inline.position);
			}
			else
			{
				styleBackgroundPosition = StyleKeyword.Null;
			}
			return styleBackgroundPosition;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00069E08 File Offset: 0x00068008
		public StyleBackgroundRepeat GetStyleBackgroundRepeat(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleBackgroundRepeat styleBackgroundRepeat;
			if (flag)
			{
				styleBackgroundRepeat = new StyleBackgroundRepeat(inline.repeat);
			}
			else
			{
				styleBackgroundRepeat = StyleKeyword.Null;
			}
			return styleBackgroundRepeat;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00069E44 File Offset: 0x00068044
		public StyleFont GetStyleFont(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleFont styleFont;
			if (flag)
			{
				Font font = (inline.resource.IsAllocated ? (inline.resource.Target as Font) : null);
				styleFont = new StyleFont(font, inline.keyword);
			}
			else
			{
				styleFont = StyleKeyword.Null;
			}
			return styleFont;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00069EA4 File Offset: 0x000680A4
		public StyleFontDefinition GetStyleFontDefinition(StylePropertyId id)
		{
			StyleValue inline = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref inline);
			StyleFontDefinition styleFontDefinition;
			if (flag)
			{
				object font = (inline.resource.IsAllocated ? inline.resource.Target : null);
				styleFontDefinition = new StyleFontDefinition(font, inline.keyword);
			}
			else
			{
				styleFontDefinition = StyleKeyword.Null;
			}
			return styleFontDefinition;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00069F00 File Offset: 0x00068100
		public bool TryGetStyleValue(StylePropertyId id, ref StyleValue value)
		{
			value.id = StylePropertyId.Unknown;
			foreach (StyleValue inlineStyle in this.m_Values)
			{
				bool flag = inlineStyle.id == id;
				if (flag)
				{
					value = inlineStyle;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00069F78 File Offset: 0x00068178
		public void SetStyleValue(StyleValue value)
		{
			for (int i = 0; i < this.m_Values.Count; i++)
			{
				bool flag = this.m_Values[i].id == value.id;
				if (flag)
				{
					bool flag2 = value.keyword == StyleKeyword.Null;
					if (flag2)
					{
						this.m_Values.RemoveAt(i);
					}
					else
					{
						this.m_Values[i] = value;
					}
					return;
				}
			}
			this.m_Values.Add(value);
		}

		// Token: 0x04000C65 RID: 3173
		internal List<StyleValue> m_Values = new List<StyleValue>();
	}
}
