using System;
using System.Runtime.CompilerServices;
using TMPro;

namespace YgomGame.Menu.AgeGate
{
	// Token: 0x02000B67 RID: 2919
	internal abstract class AgeNumber
	{
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06005455 RID: 21589 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005456 RID: 21590 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentIndex
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSelected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00002739 File Offset: 0x00000939
		public AgeNumber()
		{
		}

		// Token: 0x06005459 RID: 21593
		protected abstract int indexToValue(int index);

		// Token: 0x0600545A RID: 21594
		protected abstract string getUnselectText();

		// Token: 0x0600545B RID: 21595 RVA: 0x0000216D File Offset: 0x0000036D
		protected void updateButtonText()
		{
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCurrentValue()
		{
			return 0;
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0000216D File Offset: 0x0000036D
		public void AttachButtonText(TMP_Text textUI)
		{
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeIndex(int index)
		{
		}

		// Token: 0x040091D2 RID: 37330
		public string[] selectList;

		// Token: 0x040091D3 RID: 37331
		protected const int NullIndex = -1;

		// Token: 0x040091D4 RID: 37332
		protected TMP_Text buttonText;
	}
}
