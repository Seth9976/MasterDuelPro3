using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Download;
using YgomGame.Menu;

namespace YgomSample.Download
{
	// Token: 0x0200079E RID: 1950
	public class DownloadTest : BaseMenuViewController
	{
		// Token: 0x06003CA3 RID: 15523 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClick()
		{
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCancelClick()
		{
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnResetClick()
		{
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yLoadRequset()
		{
			return null;
		}

		// Token: 0x0400351D RID: 13597
		[SerializeField]
		private List<DownloadTest.DownloadData> m_dataList;

		// Token: 0x0400351E RID: 13598
		[SerializeField]
		private MDText m_text;

		// Token: 0x0400351F RID: 13599
		[SerializeField]
		private Button m_button;

		// Token: 0x04003520 RID: 13600
		[SerializeField]
		private Button m_resetButton;

		// Token: 0x04003521 RID: 13601
		[SerializeField]
		private Button m_cancelButton;

		// Token: 0x04003522 RID: 13602
		private DLCList m_compList;

		// Token: 0x04003523 RID: 13603
		private long m_nowSize;

		// Token: 0x04003524 RID: 13604
		private long m_totalSize;

		// Token: 0x04003525 RID: 13605
		private int m_compNum;

		// Token: 0x04003526 RID: 13606
		private int m_totalNum;

		// Token: 0x04003527 RID: 13607
		private List<DLCList> m_targets;

		// Token: 0x04003528 RID: 13608
		private Coroutine loadRequestCoroutine;

		// Token: 0x0200079F RID: 1951
		[Serializable]
		public struct DownloadData
		{
			// Token: 0x04003529 RID: 13609
			public string m_path;

			// Token: 0x0400352A RID: 13610
			public string m_index;
		}
	}
}
