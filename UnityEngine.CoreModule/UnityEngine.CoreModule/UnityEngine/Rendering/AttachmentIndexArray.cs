using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039C RID: 924
	public struct AttachmentIndexArray
	{
		// Token: 0x0600193A RID: 6458 RVA: 0x00036230 File Offset: 0x00034430
		public AttachmentIndexArray(int numAttachments)
		{
			bool flag = numAttachments < 0 || numAttachments > 8;
			if (flag)
			{
				throw new ArgumentException(string.Format("AttachmentIndexArray - numAttachments must be in range of [0, {0}[", 8));
			}
			this.a0 = (this.a1 = (this.a2 = (this.a3 = (this.a4 = (this.a5 = (this.a6 = (this.a7 = -1)))))));
			this.activeAttachments = numAttachments;
		}

		// Token: 0x1700039E RID: 926
		public unsafe int this[int index]
		{
			get
			{
				bool flag = index >= 8;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", 8));
				}
				bool flag2 = (ulong)index >= (ulong)((long)this.activeAttachments);
				if (flag2)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", this.activeAttachments));
				}
				fixed (AttachmentIndexArray* ptr = &this)
				{
					AttachmentIndexArray* self = ptr;
					int* array = (int*)self;
					return array[index];
				}
			}
			set
			{
				bool flag = index >= 8;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", 8));
				}
				bool flag2 = (ulong)index >= (ulong)((long)this.activeAttachments);
				if (flag2)
				{
					throw new IndexOutOfRangeException(string.Format("AttachmentIndexArray - index must be in range of [0, {0}[", this.activeAttachments));
				}
				fixed (AttachmentIndexArray* ptr = &this)
				{
					AttachmentIndexArray* self = ptr;
					int* array = (int*)self;
					array[index] = value;
				}
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x0003639C File Offset: 0x0003459C
		public int Length
		{
			get
			{
				return this.activeAttachments;
			}
		}

		// Token: 0x04000B75 RID: 2933
		public static AttachmentIndexArray Emtpy = new AttachmentIndexArray(0);

		// Token: 0x04000B76 RID: 2934
		private int a0;

		// Token: 0x04000B77 RID: 2935
		private int a1;

		// Token: 0x04000B78 RID: 2936
		private int a2;

		// Token: 0x04000B79 RID: 2937
		private int a3;

		// Token: 0x04000B7A RID: 2938
		private int a4;

		// Token: 0x04000B7B RID: 2939
		private int a5;

		// Token: 0x04000B7C RID: 2940
		private int a6;

		// Token: 0x04000B7D RID: 2941
		private int a7;

		// Token: 0x04000B7E RID: 2942
		private int activeAttachments;
	}
}
