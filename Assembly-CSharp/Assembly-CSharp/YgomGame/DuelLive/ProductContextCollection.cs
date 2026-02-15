using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C66 RID: 3174
	public class ProductContextCollection<T> : List<T>
	{
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AA6 RID: 23206 RVA: 0x0000216D File Offset: 0x0000036D
		public int filterSubId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06005AA7 RID: 23207 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDisplayEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06005AA8 RID: 23208 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> subCategories
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06005AA9 RID: 23209 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IProductContext> importedContexts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06005AAA RID: 23210 RVA: 0x0000216A File Offset: 0x0000036A
		private List<IDuelLiveProductGruopData> YgomGame_002EDuelLive_002EIProductContextCollection_002ElockedGroups
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06005AAB RID: 23211 RVA: 0x0000216A File Offset: 0x0000036A
		private IProductContext YgomGame_002EDuelLive_002EIProductContextCollection_002EItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06005AAC RID: 23212 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005AAD RID: 23213 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onUpdatedEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEmpty()
		{
			return false;
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(Dictionary<string, object> productDatas)
		{
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x0000216D File Offset: 0x0000036D
		public void Filter()
		{
		}

		// Token: 0x040095E6 RID: 38374
		public readonly List<T> m_ImportedContexts;

		// Token: 0x040095E7 RID: 38375
		private readonly List<int> m_ImportedSubCategories;

		// Token: 0x040095E8 RID: 38376
		public readonly List<IDuelLiveProductGruopData> lockedGroups;
	}
}
