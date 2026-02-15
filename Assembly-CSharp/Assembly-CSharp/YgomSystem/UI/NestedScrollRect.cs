using System;
using UnityEngine.EventSystems;

namespace YgomSystem.UI
{
	// Token: 0x020005A7 RID: 1447
	public class NestedScrollRect : ExtendedScrollRect
	{
		// Token: 0x06002DD7 RID: 11735 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnInitializePotentialDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnScroll(PointerEventData eventData)
		{
		}

		// Token: 0x04002B93 RID: 11155
		public bool alwaysRouteToParent;

		// Token: 0x04002B94 RID: 11156
		private bool routeToParent;
	}
}
