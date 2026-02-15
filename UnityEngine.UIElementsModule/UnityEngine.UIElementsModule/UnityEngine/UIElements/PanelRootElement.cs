using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000291 RID: 657
	internal class PanelRootElement : VisualElement
	{
		// Token: 0x060011CA RID: 4554 RVA: 0x0004A705 File Offset: 0x00048905
		public PanelRootElement()
		{
			base.name = VisualElementUtils.GetUniqueName("unity-panel-container");
			base.viewDataKey = "PanelContainer";
			base.pickingMode = PickingMode.Ignore;
			base.SetAsNextParentWithEventInterests();
		}
	}
}
