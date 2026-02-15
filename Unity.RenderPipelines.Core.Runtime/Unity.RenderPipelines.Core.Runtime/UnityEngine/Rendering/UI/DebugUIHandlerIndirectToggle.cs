using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B2 RID: 690
	public class DebugUIHandlerIndirectToggle : DebugUIHandlerWidget
	{
		// Token: 0x06001262 RID: 4706 RVA: 0x0004623E File Offset: 0x0004443E
		public void Init()
		{
			this.UpdateValueLabel();
			this.valueToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00046262 File Offset: 0x00044462
		private void OnToggleValueChanged(bool value)
		{
			this.setter(this.index, value);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00046276 File Offset: 0x00044476
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004629B File Offset: 0x0004449B
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000462C0 File Offset: 0x000444C0
		public override void OnAction()
		{
			bool value = !this.getter(this.index);
			this.setter(this.index, value);
			this.UpdateValueLabel();
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000462FA File Offset: 0x000444FA
		internal void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.getter(this.index);
			}
		}

		// Token: 0x04000C41 RID: 3137
		public Text nameLabel;

		// Token: 0x04000C42 RID: 3138
		public Toggle valueToggle;

		// Token: 0x04000C43 RID: 3139
		public Image checkmarkImage;

		// Token: 0x04000C44 RID: 3140
		public Func<int, bool> getter;

		// Token: 0x04000C45 RID: 3141
		public Action<int, bool> setter;

		// Token: 0x04000C46 RID: 3142
		internal int index;
	}
}
