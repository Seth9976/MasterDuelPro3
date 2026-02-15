using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005E6 RID: 1510
	internal class MultiColumnHeaderColumnMovePreview : VisualElement
	{
		// Token: 0x060028EE RID: 10478 RVA: 0x000A8751 File Offset: 0x000A6951
		public MultiColumnHeaderColumnMovePreview()
		{
			base.AddToClassList(MultiColumnHeaderColumnMovePreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
		}

		// Token: 0x04001594 RID: 5524
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__move-preview";
	}
}
