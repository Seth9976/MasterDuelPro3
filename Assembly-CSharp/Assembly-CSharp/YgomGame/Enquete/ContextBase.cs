using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Enquete
{
	// Token: 0x02000C17 RID: 3095
	public abstract class ContextBase : IContext
	{
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600584B RID: 22603 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600584C RID: 22604 RVA: 0x0000216D File Offset: 0x0000036D
		public string label
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x00002739 File Offset: 0x00000939
		public ContextBase()
		{
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x00002739 File Offset: 0x00000939
		public ContextBase(object jsonData)
		{
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Import(object jsonData)
		{
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void CopyTo(ContextBase target)
		{
		}

		// Token: 0x06005851 RID: 22609
		public abstract void SearchDependencieTextGroups(List<string> resultList);
	}
}
