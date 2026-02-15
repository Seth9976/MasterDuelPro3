using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B1A RID: 2842
	public class BindingItemThumb : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06005295 RID: 21141 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPeriod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x000029CC File Offset: 0x00000BCC
		public int itemCategory
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06005297 RID: 21143 RVA: 0x000029CC File Offset: 0x00000BCC
		public int itemId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLargeIcon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool iconScaleEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x0600529B RID: 21147 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600529C RID: 21148 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x0600529D RID: 21149 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingItemThumb Binding(GameObject target, bool isPeriod, int itemCategory, int itemId, bool isLargeIcon = false, BindingItemThumb.DxBadgeMode dxBadgeMode = BindingItemThumb.DxBadgeMode.None)
		{
			return null;
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateData(bool isPeriod, int itemCategory, int itemId, bool isLargeIcon, BindingItemThumb.DxBadgeMode dxBadgeMode = BindingItemThumb.DxBadgeMode.None, bool isReverse = false)
		{
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadItemThumb()
		{
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool YgomSystem_002EUI_002EILoadingIconHandler_002EIsDone()
		{
			return false;
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x040090BC RID: 37052
		[SerializeField]
		private bool m_IsPeriod;

		// Token: 0x040090BD RID: 37053
		[SerializeField]
		private int m_ItemCategory;

		// Token: 0x040090BE RID: 37054
		[SerializeField]
		private int m_ItemId;

		// Token: 0x040090BF RID: 37055
		[SerializeField]
		private bool m_IsLargeIcon;

		// Token: 0x040090C0 RID: 37056
		[SerializeField]
		private bool m_IconScaleEnabled;

		// Token: 0x040090C1 RID: 37057
		[SerializeField]
		private BindingItemThumb.DxBadgeMode m_DxBadgeMode;

		// Token: 0x040090C2 RID: 37058
		[SerializeField]
		private bool m_IsReverse;

		// Token: 0x040090C3 RID: 37059
		private bool m_LoadOnStart;

		// Token: 0x040090C4 RID: 37060
		private bool m_DirtyChild;

		// Token: 0x040090C5 RID: 37061
		private GameObject m_BindChild;

		// Token: 0x040090C6 RID: 37062
		private GameObject m_DXBadge;

		// Token: 0x040090C7 RID: 37063
		private IAsyncProgressContent m_BindProgress;

		// Token: 0x02000B1B RID: 2843
		public enum DxBadgeMode
		{
			// Token: 0x040090C9 RID: 37065
			None,
			// Token: 0x040090CA RID: 37066
			Floating,
			// Token: 0x040090CB RID: 37067
			Based
		}
	}
}
