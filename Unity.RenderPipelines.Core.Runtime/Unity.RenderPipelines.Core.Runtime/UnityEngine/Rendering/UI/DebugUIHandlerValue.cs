using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C3 RID: 707
	public class DebugUIHandlerValue : DebugUIHandlerWidget
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x0004742B File Offset: 0x0004562B
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00047438 File Offset: 0x00045638
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Value>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00047463 File Offset: 0x00045663
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00047488 File Offset: 0x00045688
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000474AC File Offset: 0x000456AC
		private void Update()
		{
			if (this.m_Timer >= this.m_Field.refreshRate)
			{
				object value = this.m_Field.GetValue();
				this.valueLabel.text = this.m_Field.FormatString(value);
				if (value is float)
				{
					this.valueLabel.color = (((float)value == 0f) ? DebugUIHandlerValue.k_ZeroColor : this.colorDefault);
				}
				this.m_Timer -= this.m_Field.refreshRate;
			}
			this.m_Timer += Time.deltaTime;
		}

		// Token: 0x04000C76 RID: 3190
		public Text nameLabel;

		// Token: 0x04000C77 RID: 3191
		public Text valueLabel;

		// Token: 0x04000C78 RID: 3192
		private DebugUI.Value m_Field;

		// Token: 0x04000C79 RID: 3193
		protected internal float m_Timer;

		// Token: 0x04000C7A RID: 3194
		private static readonly Color k_ZeroColor = Color.gray;
	}
}
