using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002BC RID: 700
	public class DebugUIHandlerProgressBar : DebugUIHandlerWidget
	{
		// Token: 0x0600129A RID: 4762 RVA: 0x00046BC5 File Offset: 0x00044DC5
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00046BD2 File Offset: 0x00044DD2
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Value = base.CastWidget<DebugUI.ProgressBarValue>();
			this.nameLabel.text = this.m_Value.displayName;
			this.UpdateValue();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00046C03 File Offset: 0x00044E03
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00046C17 File Offset: 0x00044E17
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00046C2C File Offset: 0x00044E2C
		private void Update()
		{
			if (this.m_Timer >= this.m_Value.refreshRate)
			{
				this.UpdateValue();
				this.m_Timer -= this.m_Value.refreshRate;
			}
			this.m_Timer += Time.deltaTime;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00046C7C File Offset: 0x00044E7C
		private void UpdateValue()
		{
			float value = (float)this.m_Value.GetValue();
			this.valueLabel.text = this.m_Value.FormatString(value);
			Vector3 scale = this.progressBarRect.localScale;
			scale.x = value;
			this.progressBarRect.localScale = scale;
		}

		// Token: 0x04000C63 RID: 3171
		public Text nameLabel;

		// Token: 0x04000C64 RID: 3172
		public Text valueLabel;

		// Token: 0x04000C65 RID: 3173
		public RectTransform progressBarRect;

		// Token: 0x04000C66 RID: 3174
		private DebugUI.ProgressBarValue m_Value;

		// Token: 0x04000C67 RID: 3175
		private float m_Timer;
	}
}
