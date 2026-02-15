using System;
using System.Collections.Generic;

namespace UnityEngine.AddressableAssets.ResourceLocators
{
	// Token: 0x02000049 RID: 73
	public class ContentCatalogDataEntry
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000830F File Offset: 0x0000650F
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00008317 File Offset: 0x00006517
		public string InternalId { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008320 File Offset: 0x00006520
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00008328 File Offset: 0x00006528
		public string Provider { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008331 File Offset: 0x00006531
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00008339 File Offset: 0x00006539
		public List<object> Keys { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008342 File Offset: 0x00006542
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000834A File Offset: 0x0000654A
		public List<object> Dependencies { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008353 File Offset: 0x00006553
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000835B File Offset: 0x0000655B
		public object Data { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008364 File Offset: 0x00006564
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000836C File Offset: 0x0000656C
		public Type ResourceType { get; private set; }

		// Token: 0x060001E3 RID: 483 RVA: 0x00008378 File Offset: 0x00006578
		public ContentCatalogDataEntry(Type type, string internalId, string provider, IEnumerable<object> keys, IEnumerable<object> dependencies = null, object extraData = null)
		{
			this.InternalId = internalId;
			this.Provider = provider;
			this.ResourceType = type;
			this.Keys = new List<object>(keys);
			this.Dependencies = ((dependencies == null) ? new List<object>() : new List<object>(dependencies));
			this.Data = extraData;
		}
	}
}
