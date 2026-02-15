using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200055C RID: 1372
	internal struct TextCoreSettings : IEquatable<TextCoreSettings>
	{
		// Token: 0x060025CB RID: 9675 RVA: 0x000962F8 File Offset: 0x000944F8
		public override bool Equals(object obj)
		{
			return obj is TextCoreSettings && this.Equals((TextCoreSettings)obj);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00096324 File Offset: 0x00094524
		public bool Equals(TextCoreSettings other)
		{
			return other.faceColor == this.faceColor && other.outlineColor == this.outlineColor && other.outlineWidth == this.outlineWidth && other.underlayColor == this.underlayColor && other.underlayOffset == this.underlayOffset && other.underlaySoftness == this.underlaySoftness;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000963A4 File Offset: 0x000945A4
		public override int GetHashCode()
		{
			int hashCode = 75905159;
			hashCode = hashCode * -1521134295 + this.faceColor.GetHashCode();
			hashCode = hashCode * -1521134295 + this.outlineColor.GetHashCode();
			hashCode = hashCode * -1521134295 + this.outlineWidth.GetHashCode();
			hashCode = hashCode * -1521134295 + this.underlayColor.GetHashCode();
			hashCode = hashCode * -1521134295 + this.underlayOffset.x.GetHashCode();
			hashCode = hashCode * -1521134295 + this.underlayOffset.y.GetHashCode();
			return hashCode * -1521134295 + this.underlaySoftness.GetHashCode();
		}

		// Token: 0x040012F7 RID: 4855
		public Color faceColor;

		// Token: 0x040012F8 RID: 4856
		public Color outlineColor;

		// Token: 0x040012F9 RID: 4857
		public float outlineWidth;

		// Token: 0x040012FA RID: 4858
		public Color underlayColor;

		// Token: 0x040012FB RID: 4859
		public Vector2 underlayOffset;

		// Token: 0x040012FC RID: 4860
		public float underlaySoftness;
	}
}
