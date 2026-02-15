using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002AF RID: 687
	public class DebugUIHandlerGroup : DebugUIHandlerWidget
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x00045FB8 File Offset: 0x000441B8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Container>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			if (this.m_Field.hideDisplayName)
			{
				this.header.gameObject.SetActive(false);
				return;
			}
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004601C File Offset: 0x0004421C
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (!fromNext && !this.m_Container.IsDirectChild(previous))
			{
				DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
				DebugManager.instance.ChangeSelection(lastItem, false);
				return true;
			}
			return false;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00046058 File Offset: 0x00044258
		public override DebugUIHandlerWidget Next()
		{
			if (this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstChild = this.m_Container.GetFirstItem();
			if (firstChild == null)
			{
				return base.Next();
			}
			return firstChild;
		}

		// Token: 0x04000C35 RID: 3125
		public Text nameLabel;

		// Token: 0x04000C36 RID: 3126
		public Transform header;

		// Token: 0x04000C37 RID: 3127
		private DebugUI.Container m_Field;

		// Token: 0x04000C38 RID: 3128
		private DebugUIHandlerContainer m_Container;
	}
}
