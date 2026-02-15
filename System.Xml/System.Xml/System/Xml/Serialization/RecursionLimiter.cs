using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001BB RID: 443
	internal class RecursionLimiter
	{
		// Token: 0x0600154A RID: 5450 RVA: 0x00069151 File Offset: 0x00067351
		internal RecursionLimiter()
		{
			this.depth = 0;
			this.maxDepth = (DiagnosticsSwitches.NonRecursiveTypeLoading.Enabled ? 1 : int.MaxValue);
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0006917A File Offset: 0x0006737A
		internal bool IsExceededLimit
		{
			get
			{
				return this.depth > this.maxDepth;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0006918A File Offset: 0x0006738A
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x00069192 File Offset: 0x00067392
		internal int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0006919B File Offset: 0x0006739B
		internal WorkItems DeferredWorkItems
		{
			get
			{
				if (this.deferredWorkItems == null)
				{
					this.deferredWorkItems = new WorkItems();
				}
				return this.deferredWorkItems;
			}
		}

		// Token: 0x04000998 RID: 2456
		private int maxDepth;

		// Token: 0x04000999 RID: 2457
		private int depth;

		// Token: 0x0400099A RID: 2458
		private WorkItems deferredWorkItems;
	}
}
