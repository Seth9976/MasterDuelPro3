using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C2 RID: 706
	public class DebugUIHandlerVBox : DebugUIHandlerWidget
	{
		// Token: 0x060012C0 RID: 4800 RVA: 0x0004739A File Offset: 0x0004559A
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000473B0 File Offset: 0x000455B0
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

		// Token: 0x060012C2 RID: 4802 RVA: 0x000473EC File Offset: 0x000455EC
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

		// Token: 0x04000C75 RID: 3189
		private DebugUIHandlerContainer m_Container;
	}
}
