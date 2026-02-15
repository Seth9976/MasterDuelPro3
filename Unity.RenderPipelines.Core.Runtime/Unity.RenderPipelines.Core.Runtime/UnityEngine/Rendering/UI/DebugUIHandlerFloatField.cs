using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002AD RID: 685
	public class DebugUIHandlerFloatField : DebugUIHandlerWidget
	{
		// Token: 0x06001241 RID: 4673 RVA: 0x00045BBF File Offset: 0x00043DBF
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.FloatField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00045BF0 File Offset: 0x00043DF0
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00045C15 File Offset: 0x00043E15
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00045C39 File Offset: 0x00043E39
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00045C47 File Offset: 0x00043E47
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00045C58 File Offset: 0x00043E58
		private void ChangeValue(bool fast, float multiplier)
		{
			float value = this.m_Field.GetValue();
			value += this.m_Field.incStep * (fast ? this.m_Field.incStepMult : 1f) * multiplier;
			this.m_Field.SetValue(value);
			this.UpdateValueLabel();
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00045CAC File Offset: 0x00043EAC
		private void UpdateValueLabel()
		{
			this.valueLabel.text = this.m_Field.GetValue().ToString("N" + this.m_Field.decimals.ToString());
		}

		// Token: 0x04000C2C RID: 3116
		public Text nameLabel;

		// Token: 0x04000C2D RID: 3117
		public Text valueLabel;

		// Token: 0x04000C2E RID: 3118
		private DebugUI.FloatField m_Field;
	}
}
