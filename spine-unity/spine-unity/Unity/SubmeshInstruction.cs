using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000066 RID: 102
	public struct SubmeshInstruction
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00012A4B File Offset: 0x00010C4B
		public int SlotCount
		{
			get
			{
				return this.endSlot - this.startSlot;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00012A5C File Offset: 0x00010C5C
		public override string ToString()
		{
			return string.Format("[SubmeshInstruction: slots {0} to {1}. (Material){2}. preActiveClippingSlotSource:{3}]", new object[]
			{
				this.startSlot,
				this.endSlot - 1,
				(this.material == null) ? "<none>" : this.material.name,
				this.preActiveClippingSlotSource
			});
		}

		// Token: 0x04000205 RID: 517
		public Skeleton skeleton;

		// Token: 0x04000206 RID: 518
		public int startSlot;

		// Token: 0x04000207 RID: 519
		public int endSlot;

		// Token: 0x04000208 RID: 520
		public Material material;

		// Token: 0x04000209 RID: 521
		public bool forceSeparate;

		// Token: 0x0400020A RID: 522
		public int preActiveClippingSlotSource;

		// Token: 0x0400020B RID: 523
		public int rawTriangleCount;

		// Token: 0x0400020C RID: 524
		public int rawVertexCount;

		// Token: 0x0400020D RID: 525
		public int rawFirstVertexIndex;

		// Token: 0x0400020E RID: 526
		public bool hasClipping;

		// Token: 0x0400020F RID: 527
		public bool hasPMAAdditiveSlot;
	}
}
