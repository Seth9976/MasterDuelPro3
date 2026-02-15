using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000582 RID: 1410
	public class ColorContainerImage : ColorContainerGraphic
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002CAD RID: 11437 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite spriteUnselected
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002CAF RID: 11439 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite spriteSelected
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite spriteButtonDown
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite spriteButtonEnter
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite spriteInactive
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x0000216A File Offset: 0x0000036A
		public Image targetImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode, bool is_active = true)
		{
		}

		// Token: 0x04002AFA RID: 11002
		[SerializeField]
		private Sprite _spriteUnselected;

		// Token: 0x04002AFB RID: 11003
		[SerializeField]
		private Sprite _spriteSelected;

		// Token: 0x04002AFC RID: 11004
		[SerializeField]
		private Sprite _spriteButtonDown;

		// Token: 0x04002AFD RID: 11005
		[SerializeField]
		private Sprite _spriteButtonEnter;

		// Token: 0x04002AFE RID: 11006
		[SerializeField]
		private Sprite _spriteInactive;

		// Token: 0x04002AFF RID: 11007
		private Image _targetImage;
	}
}
