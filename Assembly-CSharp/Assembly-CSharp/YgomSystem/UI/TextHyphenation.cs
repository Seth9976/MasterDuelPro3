using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000606 RID: 1542
	public class TextHyphenation : MonoBehaviour
	{
		// Token: 0x0600314D RID: 12621 RVA: 0x0000216D File Offset: 0x0000036D
		private void DirtyLayoutCallback()
		{
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x0000216A File Offset: 0x0000036A
		private List<string> SplitWrap(string txt)
		{
			return null;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x0000216A File Offset: 0x0000036A
		private string WrapText(float rectwidth)
		{
			return null;
		}

		// Token: 0x04002DB3 RID: 11699
		private bool doCallbackProc;

		// Token: 0x04002DB4 RID: 11700
		private bool doFirstWrap;

		// Token: 0x04002DB5 RID: 11701
		private MDText _Text;

		// Token: 0x04002DB6 RID: 11702
		private string proctext;

		// Token: 0x04002DB7 RID: 11703
		private string orgtext;

		// Token: 0x04002DB8 RID: 11704
		private static SortedDictionary<char, bool> frontmap;

		// Token: 0x04002DB9 RID: 11705
		private static SortedDictionary<char, bool> backmap;

		// Token: 0x04002DBA RID: 11706
		private static readonly string RITCH_TEXT_REPLACE;

		// Token: 0x04002DBB RID: 11707
		private static readonly char[] HYP_FRONT;

		// Token: 0x04002DBC RID: 11708
		private static readonly char[] HYP_BACK;
	}
}
