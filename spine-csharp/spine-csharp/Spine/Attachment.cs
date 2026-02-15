using System;

namespace Spine
{
	// Token: 0x0200004A RID: 74
	public abstract class Attachment
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000A2B5 File Offset: 0x000084B5
		public string Name { get; }

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A2BD File Offset: 0x000084BD
		protected Attachment(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null");
			}
			this.Name = name;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A2DF File Offset: 0x000084DF
		protected Attachment(Attachment other)
		{
			this.Name = other.Name;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A2F3 File Offset: 0x000084F3
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060001C4 RID: 452
		public abstract Attachment Copy();
	}
}
