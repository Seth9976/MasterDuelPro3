using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x02001429 RID: 5161
	public class DeckViewMobile : DeckView
	{
		// Token: 0x060095E0 RID: 38368 RVA: 0x0015A75C File Offset: 0x0015895C
		protected override void Awake()
		{
			this.contentWidth = 802f;
			this.defaultColumns = 5;
			this.defaultMainDeckRows = int.MaxValue;
			this.defaultExtraDeckRows = int.MaxValue;
			this.defaultSideDeckRows = int.MaxValue;
			this.defaultSpacing = new Vector2(8f, 8f);
			this.dragCardScale = new Vector3(1.3f, 1.3f, 1f);
			base.Awake();
		}

		// Token: 0x060095E1 RID: 38369 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ChangeGridSpacing(DeckView.DeckLocation location)
		{
		}
	}
}
