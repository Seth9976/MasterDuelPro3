using System;

namespace Spine
{
	// Token: 0x0200004D RID: 77
	public class BoundingBoxAttachment : VertexAttachment
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000A2FB File Offset: 0x000084FB
		public BoundingBoxAttachment(string name)
			: base(name)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A304 File Offset: 0x00008504
		protected BoundingBoxAttachment(BoundingBoxAttachment other)
			: base(other)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A30D File Offset: 0x0000850D
		public override Attachment Copy()
		{
			return new BoundingBoxAttachment(this);
		}
	}
}
