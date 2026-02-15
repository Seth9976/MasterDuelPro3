using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B6 RID: 950
	internal struct TransformData : IStyleDataGroup<TransformData>, IEquatable<TransformData>
	{
		// Token: 0x06001C0C RID: 7180 RVA: 0x000694B0 File Offset: 0x000676B0
		public TransformData Copy()
		{
			return this;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000694C8 File Offset: 0x000676C8
		public void CopyFrom(ref TransformData other)
		{
			this = other;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000694D8 File Offset: 0x000676D8
		public static bool operator ==(TransformData lhs, TransformData rhs)
		{
			return lhs.rotate == rhs.rotate && lhs.scale == rhs.scale && lhs.transformOrigin == rhs.transformOrigin && lhs.translate == rhs.translate;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00069538 File Offset: 0x00067738
		public bool Equals(TransformData other)
		{
			return other == this;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00069558 File Offset: 0x00067758
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransformData && this.Equals((TransformData)obj);
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00069590 File Offset: 0x00067790
		public override int GetHashCode()
		{
			int hashCode = this.rotate.GetHashCode();
			hashCode = (hashCode * 397) ^ this.scale.GetHashCode();
			hashCode = (hashCode * 397) ^ this.transformOrigin.GetHashCode();
			return (hashCode * 397) ^ this.translate.GetHashCode();
		}

		// Token: 0x04000C4D RID: 3149
		public Rotate rotate;

		// Token: 0x04000C4E RID: 3150
		public Scale scale;

		// Token: 0x04000C4F RID: 3151
		public TransformOrigin transformOrigin;

		// Token: 0x04000C50 RID: 3152
		public Translate translate;
	}
}
