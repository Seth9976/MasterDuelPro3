using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.GemShop;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B17 RID: 2839
	[DisallowMultipleComponent]
	public class BindingGemShopIcon : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600525E RID: 21086 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600525F RID: 21087 RVA: 0x0000216D File Offset: 0x0000036D
		public int iconId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06005260 RID: 21088 RVA: 0x0000216A File Offset: 0x0000036A
		public Image iconImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06005261 RID: 21089 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject effectRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06005262 RID: 21090 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005263 RID: 21091 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06005264 RID: 21092 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool validIconId
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06005265 RID: 21093 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005266 RID: 21094 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005267 RID: 21095 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x0000216D File Offset: 0x0000036D
		private void VisibleRefresh()
		{
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x0000216D File Offset: 0x0000036D
		public void SourceChanged()
		{
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRebind()
		{
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yOnBindingRoutine()
		{
			return null;
		}

		// Token: 0x040090AA RID: 37034
		private const string k_ELabelIconImage = "IconImage";

		// Token: 0x040090AB RID: 37035
		private const string k_ELabelEffectRoot = "EffectRoot";

		// Token: 0x040090AC RID: 37036
		private const string k_ELabelFxpPrefix = "fxp_UI_Gem";

		// Token: 0x040090AD RID: 37037
		private const string k_ELabelFxpFormat = "fxp_UI_Gem_{0:D2}";

		// Token: 0x040090AE RID: 37038
		[SerializeField]
		private int m_IconId;

		// Token: 0x040090AF RID: 37039
		[SerializeField]
		private bool m_UseEffect;

		// Token: 0x040090B0 RID: 37040
		[SerializeField]
		private GemShopIconSetting m_Setting;

		// Token: 0x040090B1 RID: 37041
		private Image m_IconImageCache;

		// Token: 0x040090B2 RID: 37042
		private GameObject m_EffectRootCache;

		// Token: 0x040090B3 RID: 37043
		private bool m_Visible;

		// Token: 0x040090B4 RID: 37044
		private IEnumerator m_OnBindingRoutine;
	}
}
