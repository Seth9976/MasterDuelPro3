using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002AC RID: 684
	public abstract class DebugUIHandlerField<T> : DebugUIHandlerWidget where T : DebugUI.Widget
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00045A75 File Offset: 0x00043C75
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<T>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00045AAC File Offset: 0x00043CAC
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (this.nextButtonText != null)
			{
				this.nextButtonText.color = this.colorSelected;
			}
			if (this.previousButtonText != null)
			{
				this.previousButtonText.color = this.colorSelected;
			}
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00045B1C File Offset: 0x00043D1C
		public override void OnDeselection()
		{
			if (this.nextButtonText != null)
			{
				this.nextButtonText.color = this.colorDefault;
			}
			if (this.previousButtonText != null)
			{
				this.previousButtonText.color = this.colorDefault;
			}
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00045B89 File Offset: 0x00043D89
		public override void OnAction()
		{
			this.OnIncrement(false);
		}

		// Token: 0x0600123E RID: 4670
		public abstract void UpdateValueLabel();

		// Token: 0x0600123F RID: 4671 RVA: 0x00045B92 File Offset: 0x00043D92
		protected void SetLabelText(string text)
		{
			if (text.Length > 26)
			{
				text = text.Substring(0, 23) + "...";
			}
			this.valueLabel.text = text;
		}

		// Token: 0x04000C27 RID: 3111
		public Text nextButtonText;

		// Token: 0x04000C28 RID: 3112
		public Text previousButtonText;

		// Token: 0x04000C29 RID: 3113
		public Text nameLabel;

		// Token: 0x04000C2A RID: 3114
		public Text valueLabel;

		// Token: 0x04000C2B RID: 3115
		protected internal T m_Field;
	}
}
