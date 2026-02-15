using System;

namespace Spine
{
	// Token: 0x0200005C RID: 92
	public abstract class ConstraintData
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000CAFD File Offset: 0x0000ACFD
		public ConstraintData(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			this.name = name;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CB1F File Offset: 0x0000AD1F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000CB27 File Offset: 0x0000AD27
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000CB2F File Offset: 0x0000AD2F
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000CB38 File Offset: 0x0000AD38
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000CB40 File Offset: 0x0000AD40
		public bool SkinRequired
		{
			get
			{
				return this.skinRequired;
			}
			set
			{
				this.skinRequired = value;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000CB1F File Offset: 0x0000AD1F
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x040001AE RID: 430
		internal readonly string name;

		// Token: 0x040001AF RID: 431
		internal int order;

		// Token: 0x040001B0 RID: 432
		internal bool skinRequired;
	}
}
