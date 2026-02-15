using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B8F RID: 2959
	[Serializable]
	public abstract class MDMarkupContainerBase
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060054E9 RID: 21737
		public abstract MDMarkupDef.ContainerType containerType { get; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060054EA RID: 21738 RVA: 0x0000216A File Offset: 0x0000036A
		private GlobalTextData YgomGame_002EMDMarkup_002EIMDMarkupContainer_002Etitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x0000216A File Offset: 0x0000036A
		private List<IMDMarkupContent> YgomGame_002EMDMarkup_002EIMDMarkupContainer_002Econtents
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Clear()
		{
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJson(string json)
		{
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> SearchUseTextGruops()
		{
			return null;
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void InnerSearchUseTextGroups(List<string> useTextGroups)
		{
		}

		// Token: 0x0400922A RID: 37418
		public GlobalTextData title;

		// Token: 0x0400922B RID: 37419
		public List<IMDMarkupContent> contents;
	}
}
