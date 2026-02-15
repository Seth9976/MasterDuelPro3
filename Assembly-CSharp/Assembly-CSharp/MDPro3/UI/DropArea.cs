using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013E5 RID: 5093
	public class DropArea : MonoBehaviour
	{
		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06009375 RID: 37749 RVA: 0x0014DA18 File Offset: 0x0014BC18
		private ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponentInParent<ElementObjectManager>());
			}
		}

		// Token: 0x06009376 RID: 37750 RVA: 0x0014DA4A File Offset: 0x0014BC4A
		protected void Awake()
		{
			UserInput.OnDragStart += this.Show;
			UserInput.OnDragEnd += this.Hide;
		}

		// Token: 0x06009377 RID: 37751 RVA: 0x0014DA6E File Offset: 0x0014BC6E
		protected void OnDestroy()
		{
			UserInput.OnDragStart -= this.Show;
			UserInput.OnDragEnd -= this.Hide;
		}

		// Token: 0x06009378 RID: 37752 RVA: 0x0014DA92 File Offset: 0x0014BC92
		public void SetShowLabel(string label)
		{
			this.showLabels.Add(label);
		}

		// Token: 0x06009379 RID: 37753 RVA: 0x0014DAA0 File Offset: 0x0014BCA0
		public void ClearLabels()
		{
			this.showLabels.Clear();
		}

		// Token: 0x0600937A RID: 37754 RVA: 0x0014DAB0 File Offset: 0x0014BCB0
		private void Show()
		{
			if (!this.active || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			foreach (string label in this.showLabels)
			{
				GameObject part = this.Manager.GetElement(label);
				if (part != null)
				{
					part.SetActive(true);
				}
			}
		}

		// Token: 0x0600937B RID: 37755 RVA: 0x0014DB30 File Offset: 0x0014BD30
		private void Hide()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			ElementObject[] serializedElements = this.Manager.serializedElements;
			for (int i = 0; i < serializedElements.Length; i++)
			{
				serializedElements[i].gameObject.SetActive(false);
			}
		}

		// Token: 0x0400D1E1 RID: 53729
		private ElementObjectManager m_Manager;

		// Token: 0x0400D1E2 RID: 53730
		public bool active = true;

		// Token: 0x0400D1E3 RID: 53731
		private List<string> showLabels = new List<string>();
	}
}
