using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B5 RID: 693
	public class DebugUIHandlerObject : DebugUIHandlerWidget
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0004653C File Offset: 0x0004473C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			DebugUI.ObjectField field = base.CastWidget<DebugUI.ObjectField>();
			this.nameLabel.text = field.displayName;
			this.valueLabel.text = field.GetValue().name;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004657E File Offset: 0x0004477E
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000465A3 File Offset: 0x000447A3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x04000C50 RID: 3152
		public Text nameLabel;

		// Token: 0x04000C51 RID: 3153
		public Text valueLabel;
	}
}
