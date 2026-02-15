using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B3 RID: 691
	public class DebugUIHandlerIntField : DebugUIHandlerWidget
	{
		// Token: 0x06001269 RID: 4713 RVA: 0x00046326 File Offset: 0x00044526
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.IntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00046357 File Offset: 0x00044557
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004637C File Offset: 0x0004457C
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000463A0 File Offset: 0x000445A0
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000463AA File Offset: 0x000445AA
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000463B4 File Offset: 0x000445B4
		private void ChangeValue(bool fast, int multiplier)
		{
			int value = this.m_Field.GetValue();
			value += this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1) * multiplier;
			this.m_Field.SetValue(value);
			this.UpdateValueLabel();
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00046404 File Offset: 0x00044604
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x04000C47 RID: 3143
		public Text nameLabel;

		// Token: 0x04000C48 RID: 3144
		public Text valueLabel;

		// Token: 0x04000C49 RID: 3145
		private DebugUI.IntField m_Field;
	}
}
