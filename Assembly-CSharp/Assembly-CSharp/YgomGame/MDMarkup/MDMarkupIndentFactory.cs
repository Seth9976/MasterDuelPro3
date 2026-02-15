using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BBB RID: 3003
	public class MDMarkupIndentFactory
	{
		// Token: 0x060055B4 RID: 21940 RVA: 0x00002739 File Offset: 0x00000939
		public MDMarkupIndentFactory(Transform[] indentTemplates)
		{
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupIndentWidget Create(int indent)
		{
			return null;
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupIndentWidget Create(int indent, MDMarkupIndentWidget parent)
		{
			return null;
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupIndentWidget Create(int indent, Transform parent)
		{
			return null;
		}

		// Token: 0x040092C7 RID: 37575
		internal const int k_EmptyIndent = -2;

		// Token: 0x040092C8 RID: 37576
		private readonly Transform[] m_IndentTemplates;
	}
}
