using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Download
{
	// Token: 0x02000F53 RID: 3923
	public sealed class DownloadController
	{
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060073A0 RID: 29600 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060073A1 RID: 29601 RVA: 0x0000216D File Offset: 0x0000036D
		public DownloadStep step
		{
			get
			{
				return DownloadStep.None;
			}
			private set
			{
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060073A2 RID: 29602 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float TotalProgress
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060073A3 RID: 29603 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060073A4 RID: 29604 RVA: 0x0000216D File Offset: 0x0000036D
		public int TotalNum
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

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060073A5 RID: 29605 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CompleteNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStepHandler(DownloadController.StepHandler handler)
		{
		}

		// Token: 0x060073A7 RID: 29607 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReleaseStepHandler(DownloadController.StepHandler handler)
		{
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x0000216D File Offset: 0x0000036D
		public void RequestAppQuitAbort(Action callback)
		{
		}

		// Token: 0x060073A9 RID: 29609 RVA: 0x0000216D File Offset: 0x0000036D
		private void DownloadCompleteEvent()
		{
		}

		// Token: 0x060073AA RID: 29610 RVA: 0x0000216D File Offset: 0x0000036D
		private void DownloadErrorEvent()
		{
		}

		// Token: 0x060073AB RID: 29611 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowLoading()
		{
		}

		// Token: 0x060073AC RID: 29612 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideLoading()
		{
		}

		// Token: 0x060073AD RID: 29613 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartDownload(DownloadController.EventHandler complete, DownloadController.EventHandler error)
		{
		}

		// Token: 0x060073AE RID: 29614 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshCache()
		{
		}

		// Token: 0x060073AF RID: 29615 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x060073B0 RID: 29616 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yDownload()
		{
			return null;
		}

		// Token: 0x060073B1 RID: 29617 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yDeleteUnusedData()
		{
			return null;
		}

		// Token: 0x060073B2 RID: 29618 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DownloadItem(string baseUrl, DLCList target, Action<bool> callback)
		{
			return null;
		}

		// Token: 0x060073B3 RID: 29619 RVA: 0x0000216D File Offset: 0x0000036D
		private void SendComplete()
		{
		}

		// Token: 0x060073B4 RID: 29620 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenAlertDialog(string message, Action callbackTitle, Action callbackRetry)
		{
		}

		// Token: 0x0400ACAE RID: 44206
		public DownloadStep m_step;

		// Token: 0x0400ACAF RID: 44207
		private DownloadController.EventHandler downloadCompleteHandler;

		// Token: 0x0400ACB0 RID: 44208
		private DownloadController.EventHandler downloadErrorHandler;

		// Token: 0x0400ACB1 RID: 44209
		private DownloadController.StepHandler stepHandler;

		// Token: 0x0400ACB2 RID: 44210
		private int completeNum;

		// Token: 0x0400ACB3 RID: 44211
		private int requestEndNum;

		// Token: 0x0400ACB4 RID: 44212
		private long m_totalSize;

		// Token: 0x0400ACB5 RID: 44213
		private long m_requestEndSize;

		// Token: 0x0400ACB6 RID: 44214
		private long m_compSize;

		// Token: 0x0400ACB7 RID: 44215
		private List<DownloadController.DownloadData> m_dataList;

		// Token: 0x0400ACB8 RID: 44216
		private List<DLCList> m_targetList;

		// Token: 0x0400ACB9 RID: 44217
		private DLCList m_downloadList;

		// Token: 0x0400ACBA RID: 44218
		private DLCList m_compList;

		// Token: 0x0400ACBB RID: 44219
		private DLCList m_local;

		// Token: 0x0400ACBC RID: 44220
		private IEnumerator downloadCoroutine;

		// Token: 0x0400ACBD RID: 44221
		private bool bAppQuitAbort;

		// Token: 0x0400ACBE RID: 44222
		private Action abortCallback;

		// Token: 0x02000F54 RID: 3924
		private struct DownloadData
		{
			// Token: 0x0400ACBF RID: 44223
			public string path;

			// Token: 0x0400ACC0 RID: 44224
			public string index;
		}

		// Token: 0x02000F55 RID: 3925
		// (Invoke) Token: 0x060073B7 RID: 29623
		public delegate void EventHandler();

		// Token: 0x02000F56 RID: 3926
		// (Invoke) Token: 0x060073BB RID: 29627
		public delegate void StepHandler(DownloadStep step);
	}
}
