using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013DB RID: 5083
	public class SuperScrollViewItem : MonoBehaviour
	{
		// Token: 0x06009345 RID: 37701 RVA: 0x0014C768 File Offset: 0x0014A968
		private void Start()
		{
			Button button;
			if (base.TryGetComponent<Button>(out button))
			{
				button.onClick.AddListener(new UnityAction(this.OnClick));
			}
		}

		// Token: 0x06009346 RID: 37702 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Refresh()
		{
		}

		// Token: 0x06009347 RID: 37703 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnClick()
		{
		}

		// Token: 0x0400D197 RID: 53655
		public int id;

		// Token: 0x0400D198 RID: 53656
		public SuperScrollView handler;
	}
}
