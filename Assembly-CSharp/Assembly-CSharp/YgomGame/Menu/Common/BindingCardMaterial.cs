using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B12 RID: 2834
	public class BindingCardMaterial : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06005238 RID: 21048 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005239 RID: 21049 RVA: 0x0000216D File Offset: 0x0000036D
		public int cardId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600523A RID: 21050 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600523B RID: 21051 RVA: 0x0000216D File Offset: 0x0000036D
		public int pRareType
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600523D RID: 21053 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isMonochrome
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x0600523E RID: 21054 RVA: 0x0000216A File Offset: 0x0000036A
		private RawImage target
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600523F RID: 21055 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06005240 RID: 21056 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005241 RID: 21057 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005242 RID: 21058 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMatMonochrome(bool isMonochrome)
		{
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingCardMaterial Binding(RawImage target, int cardId, int pRareType = 1)
		{
			return null;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x0000216D File Offset: 0x0000036D
		public void ExecuteBinding()
		{
		}

		// Token: 0x04009098 RID: 37016
		[SerializeField]
		private int m_CardId;

		// Token: 0x04009099 RID: 37017
		[SerializeField]
		private int m_RareId;

		// Token: 0x0400909A RID: 37018
		private bool m_IsMonochrome;

		// Token: 0x0400909B RID: 37019
		private RawImage m_TargetCache;

		// Token: 0x0400909C RID: 37020
		private bool m_LoadOnStart;
	}
}
