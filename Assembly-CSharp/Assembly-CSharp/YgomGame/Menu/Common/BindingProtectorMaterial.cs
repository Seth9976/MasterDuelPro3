using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B20 RID: 2848
	public class BindingProtectorMaterial : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060052F5 RID: 21237 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052F6 RID: 21238 RVA: 0x0000216D File Offset: 0x0000036D
		public int itemid
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060052F7 RID: 21239 RVA: 0x0000216A File Offset: 0x0000036A
		private RawImage target
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060052F8 RID: 21240 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x060052F9 RID: 21241 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060052FA RID: 21242 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
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

		// Token: 0x060052FB RID: 21243 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingProtectorMaterial Binding(RawImage target, int itemid)
		{
			return null;
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x0000216D File Offset: 0x0000036D
		public void ExecuteBinding()
		{
		}

		// Token: 0x040090FE RID: 37118
		[SerializeField]
		private int m_ItemId;

		// Token: 0x040090FF RID: 37119
		private RawImage m_TargetCache;

		// Token: 0x04009100 RID: 37120
		private bool m_LoadOnStart;
	}
}
