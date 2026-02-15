using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C9 RID: 713
	[ExecuteAlways]
	public class UIFoldout : Toggle
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x00048389 File Offset: 0x00046589
		protected override void Start()
		{
			base.Start();
			this.onValueChanged.AddListener(new UnityAction<bool>(this.SetState));
			this.SetState(base.isOn);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000483B4 File Offset: 0x000465B4
		private void OnValidate()
		{
			this.SetState(base.isOn, false);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000483C3 File Offset: 0x000465C3
		public void SetState(bool state)
		{
			this.SetState(state, true);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000483D0 File Offset: 0x000465D0
		public void SetState(bool state, bool rebuildLayout)
		{
			if (this.arrowOpened == null || this.arrowClosed == null || this.content == null)
			{
				return;
			}
			if (this.arrowOpened.activeSelf != state)
			{
				this.arrowOpened.SetActive(state);
			}
			if (this.arrowClosed.activeSelf == state)
			{
				this.arrowClosed.SetActive(!state);
			}
			if (this.content.activeSelf != state)
			{
				this.content.SetActive(state);
			}
			if (rebuildLayout)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.parent as RectTransform);
			}
		}

		// Token: 0x04000C9D RID: 3229
		public GameObject content;

		// Token: 0x04000C9E RID: 3230
		public GameObject arrowOpened;

		// Token: 0x04000C9F RID: 3231
		public GameObject arrowClosed;
	}
}
