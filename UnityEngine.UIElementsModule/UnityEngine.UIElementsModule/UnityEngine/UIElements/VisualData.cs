using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B8 RID: 952
	internal struct VisualData : IStyleDataGroup<VisualData>, IEquatable<VisualData>
	{
		// Token: 0x06001C18 RID: 7192 RVA: 0x00069850 File Offset: 0x00067A50
		public VisualData Copy()
		{
			return this;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00069868 File Offset: 0x00067A68
		public void CopyFrom(ref VisualData other)
		{
			this = other;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00069878 File Offset: 0x00067A78
		public static bool operator ==(VisualData lhs, VisualData rhs)
		{
			return lhs.backgroundColor == rhs.backgroundColor && lhs.backgroundImage == rhs.backgroundImage && lhs.backgroundPositionX == rhs.backgroundPositionX && lhs.backgroundPositionY == rhs.backgroundPositionY && lhs.backgroundRepeat == rhs.backgroundRepeat && lhs.backgroundSize == rhs.backgroundSize && lhs.borderBottomColor == rhs.borderBottomColor && lhs.borderBottomLeftRadius == rhs.borderBottomLeftRadius && lhs.borderBottomRightRadius == rhs.borderBottomRightRadius && lhs.borderLeftColor == rhs.borderLeftColor && lhs.borderRightColor == rhs.borderRightColor && lhs.borderTopColor == rhs.borderTopColor && lhs.borderTopLeftRadius == rhs.borderTopLeftRadius && lhs.borderTopRightRadius == rhs.borderTopRightRadius && lhs.opacity == rhs.opacity && lhs.overflow == rhs.overflow;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000699CC File Offset: 0x00067BCC
		public bool Equals(VisualData other)
		{
			return other == this;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000699EC File Offset: 0x00067BEC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualData && this.Equals((VisualData)obj);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00069A24 File Offset: 0x00067C24
		public override int GetHashCode()
		{
			int hashCode = this.backgroundColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.backgroundImage.GetHashCode();
			hashCode = (hashCode * 397) ^ this.backgroundPositionX.GetHashCode();
			hashCode = (hashCode * 397) ^ this.backgroundPositionY.GetHashCode();
			hashCode = (hashCode * 397) ^ this.backgroundRepeat.GetHashCode();
			hashCode = (hashCode * 397) ^ this.backgroundSize.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderBottomColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderBottomLeftRadius.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderBottomRightRadius.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderLeftColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderRightColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderTopColor.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderTopLeftRadius.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderTopRightRadius.GetHashCode();
			hashCode = (hashCode * 397) ^ this.opacity.GetHashCode();
			return (hashCode * 397) ^ (int)this.overflow;
		}

		// Token: 0x04000C55 RID: 3157
		public Color backgroundColor;

		// Token: 0x04000C56 RID: 3158
		public Background backgroundImage;

		// Token: 0x04000C57 RID: 3159
		public BackgroundPosition backgroundPositionX;

		// Token: 0x04000C58 RID: 3160
		public BackgroundPosition backgroundPositionY;

		// Token: 0x04000C59 RID: 3161
		public BackgroundRepeat backgroundRepeat;

		// Token: 0x04000C5A RID: 3162
		public BackgroundSize backgroundSize;

		// Token: 0x04000C5B RID: 3163
		public Color borderBottomColor;

		// Token: 0x04000C5C RID: 3164
		public Length borderBottomLeftRadius;

		// Token: 0x04000C5D RID: 3165
		public Length borderBottomRightRadius;

		// Token: 0x04000C5E RID: 3166
		public Color borderLeftColor;

		// Token: 0x04000C5F RID: 3167
		public Color borderRightColor;

		// Token: 0x04000C60 RID: 3168
		public Color borderTopColor;

		// Token: 0x04000C61 RID: 3169
		public Length borderTopLeftRadius;

		// Token: 0x04000C62 RID: 3170
		public Length borderTopRightRadius;

		// Token: 0x04000C63 RID: 3171
		public float opacity;

		// Token: 0x04000C64 RID: 3172
		public OverflowInternal overflow;
	}
}
