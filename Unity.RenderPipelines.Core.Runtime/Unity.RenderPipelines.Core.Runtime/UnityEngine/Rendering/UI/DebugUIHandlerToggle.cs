using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002BE RID: 702
	public class DebugUIHandlerToggle : DebugUIHandlerWidget
	{
		// Token: 0x060012A7 RID: 4775 RVA: 0x00046F3C File Offset: 0x0004513C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BoolField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
			this.valueToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00046F94 File Offset: 0x00045194
		private void OnToggleValueChanged(bool value)
		{
			this.m_Field.SetValue(value);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00046FA2 File Offset: 0x000451A2
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00046FC7 File Offset: 0x000451C7
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00046FEC File Offset: 0x000451EC
		public override void OnAction()
		{
			bool value = !this.m_Field.GetValue();
			this.m_Field.SetValue(value);
			this.UpdateValueLabel();
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0004701A File Offset: 0x0004521A
		protected internal virtual void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.m_Field.GetValue();
			}
		}

		// Token: 0x04000C69 RID: 3177
		public Text nameLabel;

		// Token: 0x04000C6A RID: 3178
		public Toggle valueToggle;

		// Token: 0x04000C6B RID: 3179
		public Image checkmarkImage;

		// Token: 0x04000C6C RID: 3180
		protected internal DebugUI.BoolField m_Field;
	}
}
