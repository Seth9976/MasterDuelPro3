using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000698 RID: 1688
	public class StructureBoxWidget : ElementWidgetBase
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x0000216D File Offset: 0x0000036D
		public int structureId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public StructureBoxWidget(ElementObjectManager eom, Sprite deckSprite, Sprite openedDeckSprite, Sprite[] monsterSprites)
			: base(null)
		{
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x0000216A File Offset: 0x0000036A
		public StructureBoxWidget Binding(int structureId)
		{
			return null;
		}

		// Token: 0x04003060 RID: 12384
		public readonly Image deckImage;

		// Token: 0x04003061 RID: 12385
		public readonly Image deckOpenedImage;

		// Token: 0x04003062 RID: 12386
		public readonly RawImage[] cardImages;

		// Token: 0x04003063 RID: 12387
		public readonly ExtendedTextMeshProUGUI nameText;

		// Token: 0x04003064 RID: 12388
		public readonly SelectionButton button;

		// Token: 0x04003065 RID: 12389
		private Image[] _monsterImages;

		// Token: 0x04003066 RID: 12390
		public Action<StructureBoxWidget> onClickEvent;

		// Token: 0x04003067 RID: 12391
		private bool _useNewDeckImage;

		// Token: 0x04003068 RID: 12392
		private bool _useNewOpenedDeckImage;
	}
}
