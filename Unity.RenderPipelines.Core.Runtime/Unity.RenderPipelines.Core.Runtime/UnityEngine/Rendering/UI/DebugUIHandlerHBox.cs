using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B0 RID: 688
	public class DebugUIHandlerHBox : DebugUIHandlerWidget
	{
		// Token: 0x06001256 RID: 4694 RVA: 0x00046097 File Offset: 0x00044297
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000460AC File Offset: 0x000442AC
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

		// Token: 0x06001258 RID: 4696 RVA: 0x000460E8 File Offset: 0x000442E8
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

		// Token: 0x04000C39 RID: 3129
		private DebugUIHandlerContainer m_Container;
	}
}
