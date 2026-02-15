using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B1 RID: 689
	public class DebugUIHandlerIndirectFloatField : DebugUIHandlerWidget
	{
		// Token: 0x0600125A RID: 4698 RVA: 0x00046127 File Offset: 0x00044327
		public void Init()
		{
			this.UpdateValueLabel();
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004612F File Offset: 0x0004432F
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00046154 File Offset: 0x00044354
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00046178 File Offset: 0x00044378
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00046186 File Offset: 0x00044386
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00046194 File Offset: 0x00044394
		private void ChangeValue(bool fast, float multiplier)
		{
			float value = this.getter();
			value += this.incStepGetter() * (fast ? this.incStepMultGetter() : 1f) * multiplier;
			this.setter(value);
			this.UpdateValueLabel();
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000461E8 File Offset: 0x000443E8
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.getter().ToString("N" + this.decimalsGetter().ToString());
			}
		}

		// Token: 0x04000C3A RID: 3130
		public Text nameLabel;

		// Token: 0x04000C3B RID: 3131
		public Text valueLabel;

		// Token: 0x04000C3C RID: 3132
		public Func<float> getter;

		// Token: 0x04000C3D RID: 3133
		public Action<float> setter;

		// Token: 0x04000C3E RID: 3134
		public Func<float> incStepGetter;

		// Token: 0x04000C3F RID: 3135
		public Func<float> incStepMultGetter;

		// Token: 0x04000C40 RID: 3136
		public Func<float> decimalsGetter;
	}
}
