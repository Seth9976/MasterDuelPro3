using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B4 RID: 948
	internal struct LayoutData : IStyleDataGroup<LayoutData>, IEquatable<LayoutData>
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x00068CC4 File Offset: 0x00066EC4
		public LayoutData Copy()
		{
			return this;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00068CDC File Offset: 0x00066EDC
		public void CopyFrom(ref LayoutData other)
		{
			this = other;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00068CEC File Offset: 0x00066EEC
		public static bool operator ==(LayoutData lhs, LayoutData rhs)
		{
			return lhs.alignContent == rhs.alignContent && lhs.alignItems == rhs.alignItems && lhs.alignSelf == rhs.alignSelf && lhs.borderBottomWidth == rhs.borderBottomWidth && lhs.borderLeftWidth == rhs.borderLeftWidth && lhs.borderRightWidth == rhs.borderRightWidth && lhs.borderTopWidth == rhs.borderTopWidth && lhs.bottom == rhs.bottom && lhs.display == rhs.display && lhs.flexBasis == rhs.flexBasis && lhs.flexDirection == rhs.flexDirection && lhs.flexGrow == rhs.flexGrow && lhs.flexShrink == rhs.flexShrink && lhs.flexWrap == rhs.flexWrap && lhs.height == rhs.height && lhs.justifyContent == rhs.justifyContent && lhs.left == rhs.left && lhs.marginBottom == rhs.marginBottom && lhs.marginLeft == rhs.marginLeft && lhs.marginRight == rhs.marginRight && lhs.marginTop == rhs.marginTop && lhs.maxHeight == rhs.maxHeight && lhs.maxWidth == rhs.maxWidth && lhs.minHeight == rhs.minHeight && lhs.minWidth == rhs.minWidth && lhs.paddingBottom == rhs.paddingBottom && lhs.paddingLeft == rhs.paddingLeft && lhs.paddingRight == rhs.paddingRight && lhs.paddingTop == rhs.paddingTop && lhs.position == rhs.position && lhs.right == rhs.right && lhs.top == rhs.top && lhs.width == rhs.width;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00068F7C File Offset: 0x0006717C
		public bool Equals(LayoutData other)
		{
			return other == this;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00068F9C File Offset: 0x0006719C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LayoutData && this.Equals((LayoutData)obj);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00068FD4 File Offset: 0x000671D4
		public override int GetHashCode()
		{
			int hashCode = (int)this.alignContent;
			hashCode = (hashCode * 397) ^ (int)this.alignItems;
			hashCode = (hashCode * 397) ^ (int)this.alignSelf;
			hashCode = (hashCode * 397) ^ this.borderBottomWidth.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderLeftWidth.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderRightWidth.GetHashCode();
			hashCode = (hashCode * 397) ^ this.borderTopWidth.GetHashCode();
			hashCode = (hashCode * 397) ^ this.bottom.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.display;
			hashCode = (hashCode * 397) ^ this.flexBasis.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.flexDirection;
			hashCode = (hashCode * 397) ^ this.flexGrow.GetHashCode();
			hashCode = (hashCode * 397) ^ this.flexShrink.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.flexWrap;
			hashCode = (hashCode * 397) ^ this.height.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.justifyContent;
			hashCode = (hashCode * 397) ^ this.left.GetHashCode();
			hashCode = (hashCode * 397) ^ this.marginBottom.GetHashCode();
			hashCode = (hashCode * 397) ^ this.marginLeft.GetHashCode();
			hashCode = (hashCode * 397) ^ this.marginRight.GetHashCode();
			hashCode = (hashCode * 397) ^ this.marginTop.GetHashCode();
			hashCode = (hashCode * 397) ^ this.maxHeight.GetHashCode();
			hashCode = (hashCode * 397) ^ this.maxWidth.GetHashCode();
			hashCode = (hashCode * 397) ^ this.minHeight.GetHashCode();
			hashCode = (hashCode * 397) ^ this.minWidth.GetHashCode();
			hashCode = (hashCode * 397) ^ this.paddingBottom.GetHashCode();
			hashCode = (hashCode * 397) ^ this.paddingLeft.GetHashCode();
			hashCode = (hashCode * 397) ^ this.paddingRight.GetHashCode();
			hashCode = (hashCode * 397) ^ this.paddingTop.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.position;
			hashCode = (hashCode * 397) ^ this.right.GetHashCode();
			hashCode = (hashCode * 397) ^ this.top.GetHashCode();
			return (hashCode * 397) ^ this.width.GetHashCode();
		}

		// Token: 0x04000C22 RID: 3106
		public Align alignContent;

		// Token: 0x04000C23 RID: 3107
		public Align alignItems;

		// Token: 0x04000C24 RID: 3108
		public Align alignSelf;

		// Token: 0x04000C25 RID: 3109
		public float borderBottomWidth;

		// Token: 0x04000C26 RID: 3110
		public float borderLeftWidth;

		// Token: 0x04000C27 RID: 3111
		public float borderRightWidth;

		// Token: 0x04000C28 RID: 3112
		public float borderTopWidth;

		// Token: 0x04000C29 RID: 3113
		public Length bottom;

		// Token: 0x04000C2A RID: 3114
		public DisplayStyle display;

		// Token: 0x04000C2B RID: 3115
		public Length flexBasis;

		// Token: 0x04000C2C RID: 3116
		public FlexDirection flexDirection;

		// Token: 0x04000C2D RID: 3117
		public float flexGrow;

		// Token: 0x04000C2E RID: 3118
		public float flexShrink;

		// Token: 0x04000C2F RID: 3119
		public Wrap flexWrap;

		// Token: 0x04000C30 RID: 3120
		public Length height;

		// Token: 0x04000C31 RID: 3121
		public Justify justifyContent;

		// Token: 0x04000C32 RID: 3122
		public Length left;

		// Token: 0x04000C33 RID: 3123
		public Length marginBottom;

		// Token: 0x04000C34 RID: 3124
		public Length marginLeft;

		// Token: 0x04000C35 RID: 3125
		public Length marginRight;

		// Token: 0x04000C36 RID: 3126
		public Length marginTop;

		// Token: 0x04000C37 RID: 3127
		public Length maxHeight;

		// Token: 0x04000C38 RID: 3128
		public Length maxWidth;

		// Token: 0x04000C39 RID: 3129
		public Length minHeight;

		// Token: 0x04000C3A RID: 3130
		public Length minWidth;

		// Token: 0x04000C3B RID: 3131
		public Length paddingBottom;

		// Token: 0x04000C3C RID: 3132
		public Length paddingLeft;

		// Token: 0x04000C3D RID: 3133
		public Length paddingRight;

		// Token: 0x04000C3E RID: 3134
		public Length paddingTop;

		// Token: 0x04000C3F RID: 3135
		public Position position;

		// Token: 0x04000C40 RID: 3136
		public Length right;

		// Token: 0x04000C41 RID: 3137
		public Length top;

		// Token: 0x04000C42 RID: 3138
		public Length width;
	}
}
