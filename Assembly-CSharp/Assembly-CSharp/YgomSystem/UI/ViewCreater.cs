using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI
{
	// Token: 0x02000656 RID: 1622
	[Serializable]
	public class ViewCreater
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060032EA RID: 13034 RVA: 0x0000216D File Offset: 0x0000036D
		public ViewCreater.CreateType createType
		{
			get
			{
				return ViewCreater.CreateType.None;
			}
			set
			{
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032EC RID: 13036 RVA: 0x0000216D File Offset: 0x0000036D
		public ElementObjectManager prefRef
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032EE RID: 13038 RVA: 0x0000216D File Offset: 0x0000036D
		public string prefPath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032F0 RID: 13040 RVA: 0x0000216D File Offset: 0x0000036D
		public string prefLinkerLabel
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x0000216D File Offset: 0x0000036D
		public ElementObjectManager manualInstance
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x0000216D File Offset: 0x0000036D
		public Transform parent
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isManualParent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x0000216D File Offset: 0x0000036D
		public void Create(GameObject woner, Action<ElementObjectManager> onComplete)
		{
		}

		// Token: 0x04002F3C RID: 12092
		[SerializeField]
		private ViewCreater.CreateType m_CreateType;

		// Token: 0x04002F3D RID: 12093
		[SerializeField]
		private ElementObjectManager m_PrefRef;

		// Token: 0x04002F3E RID: 12094
		[SerializeField]
		private string m_PrefPath;

		// Token: 0x04002F3F RID: 12095
		[SerializeField]
		private string m_PrefLinkerLabel;

		// Token: 0x04002F40 RID: 12096
		private ElementObjectManager m_ManualInstance;

		// Token: 0x04002F41 RID: 12097
		private Transform m_Parent;

		// Token: 0x04002F42 RID: 12098
		private bool m_IsManualParent;

		// Token: 0x02000657 RID: 1623
		public enum CreateType
		{
			// Token: 0x04002F44 RID: 12100
			None,
			// Token: 0x04002F45 RID: 12101
			PrefRef,
			// Token: 0x04002F46 RID: 12102
			PrefPath,
			// Token: 0x04002F47 RID: 12103
			PrefLinker = 4,
			// Token: 0x04002F48 RID: 12104
			ManualInstance = 3
		}
	}
}
