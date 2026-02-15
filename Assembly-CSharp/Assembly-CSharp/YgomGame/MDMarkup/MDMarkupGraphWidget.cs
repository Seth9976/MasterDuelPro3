using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BB2 RID: 2994
	public class MDMarkupGraphWidget : MonoBehaviour
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06005587 RID: 21895 RVA: 0x0000216A File Offset: 0x0000036A
		public List<IMDMarkupWidget> contentWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06005588 RID: 21896 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06005589 RID: 21897 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600558A RID: 21898 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<MDMarkupGraphWidget> onOutputCompleteEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x0000216A File Offset: 0x0000036A
		public static MDMarkupGraphWidget Create(Transform owner, MDMarkupGraphFactory graphFactory = null)
		{
			return null;
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x0000216A File Offset: 0x0000036A
		public static MDMarkupGraphWidget Create(GameObject owner, MDMarkupGraphFactory graphFactory = null)
		{
			return null;
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertContents(IReadOnlyList<IMDMarkupContent> contentDatas)
		{
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertContent(IMDMarkupContent mdMarkupContent)
		{
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x0000216D File Offset: 0x0000036D
		public void Output(Action onComplete = null)
		{
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yOutput(Action onComplete = null)
		{
			return null;
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0000216A File Offset: 0x0000036A
		private MDMarkupIndentWidget ProcessIndent(int targetIndent)
		{
			return null;
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCard(int cardIdx)
		{
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickItem(bool isPeriod, int itemCategory, int itemId)
		{
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickLink(string link)
		{
		}

		// Token: 0x040092BA RID: 37562
		private MDMarkupGraphFactory m_MarkupGraphFactory;

		// Token: 0x040092BB RID: 37563
		private readonly int k_TMPGryphTryAddSpan;

		// Token: 0x040092BC RID: 37564
		private readonly Stack<MDMarkupIndentWidget> m_IndentStack;

		// Token: 0x040092BD RID: 37565
		private List<object> m_CardMrks;

		// Token: 0x040092BE RID: 37566
		private List<object> m_CardPremires;

		// Token: 0x040092BF RID: 37567
		private List<IMDMarkupWidget> m_ContentWidgets;

		// Token: 0x040092C0 RID: 37568
		private Queue<IMDMarkupContent> m_ContentRequestQueue;

		// Token: 0x040092C1 RID: 37569
		private Coroutine m_OutputCoroutine;
	}
}
