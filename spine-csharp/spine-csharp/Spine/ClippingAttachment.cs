using System;

namespace Spine
{
	// Token: 0x0200004E RID: 78
	public class ClippingAttachment : VertexAttachment
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A315 File Offset: 0x00008515
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000A31D File Offset: 0x0000851D
		public SlotData EndSlot
		{
			get
			{
				return this.endSlot;
			}
			set
			{
				this.endSlot = value;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A2FB File Offset: 0x000084FB
		public ClippingAttachment(string name)
			: base(name)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A326 File Offset: 0x00008526
		protected ClippingAttachment(ClippingAttachment other)
			: base(other)
		{
			this.endSlot = other.endSlot;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A33B File Offset: 0x0000853B
		public override Attachment Copy()
		{
			return new ClippingAttachment(this);
		}

		// Token: 0x04000135 RID: 309
		internal SlotData endSlot;
	}
}
