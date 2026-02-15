using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B3 RID: 947
	internal struct InheritedData : IStyleDataGroup<InheritedData>, IEquatable<InheritedData>
	{
		// Token: 0x06001BFA RID: 7162 RVA: 0x0006899C File Offset: 0x00066B9C
		public InheritedData Copy()
		{
			return this;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000689B4 File Offset: 0x00066BB4
		public void CopyFrom(ref InheritedData other)
		{
			this = other;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000689C4 File Offset: 0x00066BC4
		public static bool operator ==(InheritedData lhs, InheritedData rhs)
		{
			return lhs.color == rhs.color && lhs.fontSize == rhs.fontSize && lhs.letterSpacing == rhs.letterSpacing && lhs.textShadow == rhs.textShadow && lhs.unityEditorTextRenderingMode == rhs.unityEditorTextRenderingMode && lhs.unityFont == rhs.unityFont && lhs.unityFontDefinition == rhs.unityFontDefinition && lhs.unityFontStyleAndWeight == rhs.unityFontStyleAndWeight && lhs.unityParagraphSpacing == rhs.unityParagraphSpacing && lhs.unityTextAlign == rhs.unityTextAlign && lhs.unityTextGenerator == rhs.unityTextGenerator && lhs.unityTextOutlineColor == rhs.unityTextOutlineColor && lhs.unityTextOutlineWidth == rhs.unityTextOutlineWidth && lhs.visibility == rhs.visibility && lhs.whiteSpace == rhs.whiteSpace && lhs.wordSpacing == rhs.wordSpacing;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00068AFC File Offset: 0x00066CFC
		public bool Equals(InheritedData other)
		{
			return other == this;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00068B1C File Offset: 0x00066D1C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is InheritedData && this.Equals((InheritedData)obj);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00068B54 File Offset: 0x00066D54
		public override int GetHashCode()
		{
			int hashCode = this.color.GetHashCode();
			hashCode = (hashCode * 397) ^ this.fontSize.GetHashCode();
			hashCode = (hashCode * 397) ^ this.letterSpacing.GetHashCode();
			hashCode = (hashCode * 397) ^ this.textShadow.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.unityEditorTextRenderingMode;
			hashCode = (hashCode * 397) ^ ((this.unityFont == null) ? 0 : this.unityFont.GetHashCode());
			hashCode = (hashCode * 397) ^ this.unityFontDefinition.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.unityFontStyleAndWeight;
			hashCode = (hashCode * 397) ^ this.unityParagraphSpacing.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.unityTextAlign;
			hashCode = (hashCode * 397) ^ (int)this.unityTextGenerator;
			hashCode = (hashCode * 397) ^ this.unityTextOutlineColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.unityTextOutlineWidth.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.visibility;
			hashCode = (hashCode * 397) ^ (int)this.whiteSpace;
			return (hashCode * 397) ^ this.wordSpacing.GetHashCode();
		}

		// Token: 0x04000C12 RID: 3090
		public Color color;

		// Token: 0x04000C13 RID: 3091
		public Length fontSize;

		// Token: 0x04000C14 RID: 3092
		public Length letterSpacing;

		// Token: 0x04000C15 RID: 3093
		public TextShadow textShadow;

		// Token: 0x04000C16 RID: 3094
		public EditorTextRenderingMode unityEditorTextRenderingMode;

		// Token: 0x04000C17 RID: 3095
		public Font unityFont;

		// Token: 0x04000C18 RID: 3096
		public FontDefinition unityFontDefinition;

		// Token: 0x04000C19 RID: 3097
		public FontStyle unityFontStyleAndWeight;

		// Token: 0x04000C1A RID: 3098
		public Length unityParagraphSpacing;

		// Token: 0x04000C1B RID: 3099
		public TextAnchor unityTextAlign;

		// Token: 0x04000C1C RID: 3100
		public TextGeneratorType unityTextGenerator;

		// Token: 0x04000C1D RID: 3101
		public Color unityTextOutlineColor;

		// Token: 0x04000C1E RID: 3102
		public float unityTextOutlineWidth;

		// Token: 0x04000C1F RID: 3103
		public Visibility visibility;

		// Token: 0x04000C20 RID: 3104
		public WhiteSpace whiteSpace;

		// Token: 0x04000C21 RID: 3105
		public Length wordSpacing;
	}
}
