using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;
using YgomSystem.YGomTMPro;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F95 RID: 3989
	public class PickupCursorWidget : ElementWidgetBehaviourBase<PickupCursorWidget>
	{
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06007563 RID: 30051 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06007564 RID: 30052 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007565 RID: 30053 RVA: 0x0000216D File Offset: 0x0000036D
		public int pickupId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x06007566 RID: 30054 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ElementObjectManager eom, int pickupId = -1)
		{
		}

		// Token: 0x06007567 RID: 30055 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<PickupCursorWidget> onCreated, int pickupId = -1)
		{
		}

		// Token: 0x06007568 RID: 30056 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPickupId(int pickupId)
		{
		}

		// Token: 0x0400AEA7 RID: 44711
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/PickupCursor";

		// Token: 0x0400AEA8 RID: 44712
		private readonly string k_ELabelText;

		// Token: 0x0400AEA9 RID: 44713
		private bool m_IsActive;

		// Token: 0x0400AEAA RID: 44714
		private GameObject m_GameObject;

		// Token: 0x0400AEAB RID: 44715
		private int m_PickupId;

		// Token: 0x0400AEAC RID: 44716
		private ExtendedTextMeshProUGUI m_Text;

		// Token: 0x0400AEAD RID: 44717
		public Action onClickedCallback;

		// Token: 0x0400AEAE RID: 44718
		public Action onCreated;
	}
}
