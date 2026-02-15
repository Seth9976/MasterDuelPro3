using System;
using UnityEngine;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomSystem.Utility;

namespace YgomSystem.UI
{
	// Token: 0x0200064B RID: 1611
	public class UISystem : MonoBehaviour
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x0000216D File Offset: 0x0000036D
		public static ResourceBindingPathSetting resourceBindingPathSetting
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x0000216A File Offset: 0x0000036A
		public static InputBlocker inputBlocker
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x0000216A File Offset: 0x0000036A
		public static UINetworkHandler uiNetworkHandler
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x0000216A File Offset: 0x0000036A
		public static SystemProgress systemProgress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x0000216A File Offset: 0x0000036A
		public static SystemDialog systemDialog
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x0000216A File Offset: 0x0000036A
		public static HoldIndicator holdIndicator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite invisibleSprite
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnDownloadedAssets()
		{
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04002EF3 RID: 12019
		private static UISystem s_Instance;

		// Token: 0x04002EF4 RID: 12020
		[SerializeField]
		private InputBlocker m_InputBlocker;

		// Token: 0x04002EF5 RID: 12021
		[SerializeField]
		private UINetworkHandler m_UINetworkHandler;

		// Token: 0x04002EF6 RID: 12022
		[SerializeField]
		private SystemProgress m_SystemProgress;

		// Token: 0x04002EF7 RID: 12023
		[SerializeField]
		private SystemDialog m_SystemDialog;

		// Token: 0x04002EF8 RID: 12024
		[SerializeField]
		private HoldIndicator m_HoldIndicator;

		// Token: 0x04002EF9 RID: 12025
		[SerializeField]
		private AssetReferer m_AssetReferer;

		// Token: 0x04002EFA RID: 12026
		[SerializeField]
		private ResourceBindingPathSetting m_ResourceBindingPathSetting;

		// Token: 0x04002EFB RID: 12027
		private ResourceBindingPathSetting m_CurrentResourceBindingPathSetting;
	}
}
