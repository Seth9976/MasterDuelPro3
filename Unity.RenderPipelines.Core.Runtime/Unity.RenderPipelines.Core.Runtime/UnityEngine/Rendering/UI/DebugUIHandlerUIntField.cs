using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C1 RID: 705
	public class DebugUIHandlerUIntField : DebugUIHandlerWidget
	{
		// Token: 0x060012B8 RID: 4792 RVA: 0x00047272 File Offset: 0x00045472
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.UIntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x000472A3 File Offset: 0x000454A3
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x000472C8 File Offset: 0x000454C8
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000472EC File Offset: 0x000454EC
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000472F6 File Offset: 0x000454F6
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00047300 File Offset: 0x00045500
		private void ChangeValue(bool fast, int multiplier)
		{
			long value = (long)((ulong)this.m_Field.GetValue());
			if (value == 0L && multiplier < 0)
			{
				return;
			}
			value += (long)((ulong)(this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1U)) * (ulong)((long)multiplier));
			this.m_Field.SetValue((uint)value);
			this.UpdateValueLabel();
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004735C File Offset: 0x0004555C
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x04000C72 RID: 3186
		public Text nameLabel;

		// Token: 0x04000C73 RID: 3187
		public Text valueLabel;

		// Token: 0x04000C74 RID: 3188
		private DebugUI.UIntField m_Field;
	}
}
