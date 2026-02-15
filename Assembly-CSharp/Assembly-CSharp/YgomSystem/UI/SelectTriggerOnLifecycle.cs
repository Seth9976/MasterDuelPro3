using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005C6 RID: 1478
	public class SelectTriggerOnLifecycle : MonoBehaviour
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002E87 RID: 11911 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem selectedCheckItem
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x0000216A File Offset: 0x0000036A
		public List<SelectionItem> onEnableTargets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x0000216A File Offset: 0x0000036A
		public List<SelectionItem> onDisableTargets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x04002C22 RID: 11298
		[SerializeField]
		private SelectionItem m_SelectedCheckItem;

		// Token: 0x04002C23 RID: 11299
		[SerializeField]
		private List<SelectionItem> m_OnEnableTargets;

		// Token: 0x04002C24 RID: 11300
		[SerializeField]
		private List<SelectionItem> m_OnDisableTargets;
	}
}
