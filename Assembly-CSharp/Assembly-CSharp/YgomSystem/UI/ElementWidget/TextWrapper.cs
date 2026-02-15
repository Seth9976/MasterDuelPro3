using System;
using TMPro;
using UnityEngine;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000699 RID: 1689
	public class TextWrapper
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject gameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600352C RID: 13612 RVA: 0x0000216D File Offset: 0x0000036D
		public string text
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000F2C80 File Offset: 0x000F0E80
		// (set) Token: 0x0600352E RID: 13614 RVA: 0x0000216D File Offset: 0x0000036D
		public Color color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x0000216D File Offset: 0x0000036D
		public bool enabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x00002739 File Offset: 0x00000939
		public TextWrapper(MDText text)
		{
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x00002739 File Offset: 0x00000939
		public TextWrapper(TMP_Text text)
		{
		}

		// Token: 0x04003069 RID: 12393
		private TextWrapper.Mode mode;

		// Token: 0x0400306A RID: 12394
		private MDText textComponent;

		// Token: 0x0400306B RID: 12395
		private TMP_Text tmpTextComponent;

		// Token: 0x0200069A RID: 1690
		private enum Mode
		{
			// Token: 0x0400306D RID: 12397
			uGUI,
			// Token: 0x0400306E RID: 12398
			TMP
		}
	}
}
