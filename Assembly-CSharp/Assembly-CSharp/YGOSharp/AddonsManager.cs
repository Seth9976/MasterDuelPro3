using System;
using System.Collections.Generic;
using YGOSharp.Addons;

namespace YGOSharp
{
	// Token: 0x020001AE RID: 430
	public class AddonsManager
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001FA0E File Offset: 0x0001DC0E
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0001FA16 File Offset: 0x0001DC16
		public List<AddonBase> Addons { get; private set; }

		// Token: 0x06000663 RID: 1635 RVA: 0x0001FA1F File Offset: 0x0001DC1F
		public AddonsManager()
		{
			this.Addons = new List<AddonBase>();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001FA32 File Offset: 0x0001DC32
		public void Init(Game game)
		{
			this.Addons.Add(new StandardStreamProtocol(game));
		}
	}
}
