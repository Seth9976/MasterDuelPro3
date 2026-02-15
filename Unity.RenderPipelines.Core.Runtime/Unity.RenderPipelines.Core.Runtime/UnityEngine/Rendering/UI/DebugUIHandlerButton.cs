using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002A1 RID: 673
	public class DebugUIHandlerButton : DebugUIHandlerWidget
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x00044798 File Offset: 0x00042998
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Button>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000447C3 File Offset: 0x000429C3
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000447D7 File Offset: 0x000429D7
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000447EA File Offset: 0x000429EA
		public override void OnAction()
		{
			if (this.m_Field.action != null)
			{
				this.m_Field.action();
			}
		}

		// Token: 0x04000C07 RID: 3079
		public Text nameLabel;

		// Token: 0x04000C08 RID: 3080
		private DebugUI.Button m_Field;
	}
}
