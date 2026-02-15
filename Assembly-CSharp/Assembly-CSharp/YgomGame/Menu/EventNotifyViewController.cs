using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A6F RID: 2671
	public class EventNotifyViewController : InformDialogViewControllerBase<string[], Action>
	{
		// Token: 0x06004DF7 RID: 19959 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string[] messages, Action callback = null)
		{
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x04008BD8 RID: 35800
		private const string k_PrefPath = "Common/EventNotify/EventNotify";

		// Token: 0x04008BD9 RID: 35801
		private readonly string k_ELabelRootShow;

		// Token: 0x04008BDA RID: 35802
		private readonly string k_ELabelRootHide;

		// Token: 0x04008BDB RID: 35803
		private readonly string k_ELabelTemplate;

		// Token: 0x04008BDC RID: 35804
		private readonly string k_ELabelText;

		// Token: 0x04008BDD RID: 35805
		private readonly string k_ELabelShorcutSkip;

		// Token: 0x04008BDE RID: 35806
		[SerializeField]
		private float m_LifeSecond;

		// Token: 0x04008BDF RID: 35807
		[SerializeField]
		private float m_IntervalShowSecond;

		// Token: 0x04008BE0 RID: 35808
		[SerializeField]
		private float m_IntervalHideSecond;

		// Token: 0x04008BE1 RID: 35809
		[SerializeField]
		private int m_MaxDisp;

		// Token: 0x04008BE2 RID: 35810
		private GameObject m_RootShow;

		// Token: 0x04008BE3 RID: 35811
		private GameObject m_RootHide;

		// Token: 0x04008BE4 RID: 35812
		private Queue<GameObject> m_HideQueue;

		// Token: 0x04008BE5 RID: 35813
		private bool m_IsSkip;
	}
}
