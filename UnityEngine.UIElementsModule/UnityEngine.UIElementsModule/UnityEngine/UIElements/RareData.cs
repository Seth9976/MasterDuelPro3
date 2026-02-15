using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B5 RID: 949
	internal struct RareData : IStyleDataGroup<RareData>, IEquatable<RareData>
	{
		// Token: 0x06001C06 RID: 7174 RVA: 0x000692C0 File Offset: 0x000674C0
		public RareData Copy()
		{
			return this;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000692D8 File Offset: 0x000674D8
		public void CopyFrom(ref RareData other)
		{
			this = other;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000692E8 File Offset: 0x000674E8
		public static bool operator ==(RareData lhs, RareData rhs)
		{
			return lhs.cursor == rhs.cursor && lhs.textOverflow == rhs.textOverflow && lhs.unityBackgroundImageTintColor == rhs.unityBackgroundImageTintColor && lhs.unityOverflowClipBox == rhs.unityOverflowClipBox && lhs.unitySliceBottom == rhs.unitySliceBottom && lhs.unitySliceLeft == rhs.unitySliceLeft && lhs.unitySliceRight == rhs.unitySliceRight && lhs.unitySliceScale == rhs.unitySliceScale && lhs.unitySliceTop == rhs.unitySliceTop && lhs.unityTextOverflowPosition == rhs.unityTextOverflowPosition;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00069398 File Offset: 0x00067598
		public bool Equals(RareData other)
		{
			return other == this;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000693B8 File Offset: 0x000675B8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RareData && this.Equals((RareData)obj);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000693F0 File Offset: 0x000675F0
		public override int GetHashCode()
		{
			int hashCode = this.cursor.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.textOverflow;
			hashCode = (hashCode * 397) ^ this.unityBackgroundImageTintColor.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.unityOverflowClipBox;
			hashCode = (hashCode * 397) ^ this.unitySliceBottom;
			hashCode = (hashCode * 397) ^ this.unitySliceLeft;
			hashCode = (hashCode * 397) ^ this.unitySliceRight;
			hashCode = (hashCode * 397) ^ this.unitySliceScale.GetHashCode();
			hashCode = (hashCode * 397) ^ this.unitySliceTop;
			return (hashCode * 397) ^ (int)this.unityTextOverflowPosition;
		}

		// Token: 0x04000C43 RID: 3139
		public Cursor cursor;

		// Token: 0x04000C44 RID: 3140
		public TextOverflow textOverflow;

		// Token: 0x04000C45 RID: 3141
		public Color unityBackgroundImageTintColor;

		// Token: 0x04000C46 RID: 3142
		public OverflowClipBox unityOverflowClipBox;

		// Token: 0x04000C47 RID: 3143
		public int unitySliceBottom;

		// Token: 0x04000C48 RID: 3144
		public int unitySliceLeft;

		// Token: 0x04000C49 RID: 3145
		public int unitySliceRight;

		// Token: 0x04000C4A RID: 3146
		public float unitySliceScale;

		// Token: 0x04000C4B RID: 3147
		public int unitySliceTop;

		// Token: 0x04000C4C RID: 3148
		public TextOverflowPosition unityTextOverflowPosition;
	}
}
