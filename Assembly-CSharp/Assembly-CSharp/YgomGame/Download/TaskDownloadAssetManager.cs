using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Download
{
	// Token: 0x02000F5C RID: 3932
	public class TaskDownloadAssetManager
	{
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060073D3 RID: 29651 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060073D4 RID: 29652 RVA: 0x0000216D File Offset: 0x0000036D
		public DownloadErrorCode errorCode
		{
			[CompilerGenerated]
			get
			{
				return DownloadErrorCode.None;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060073D5 RID: 29653 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060073D6 RID: 29654 RVA: 0x0000216D File Offset: 0x0000036D
		public int completeNum
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

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060073D7 RID: 29655 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x060073D8 RID: 29656 RVA: 0x0000216D File Offset: 0x0000036D
		public long completeSize
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060073D9 RID: 29657 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x060073DA RID: 29658 RVA: 0x0000216D File Offset: 0x0000036D
		public long totalSize
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060073DB RID: 29659 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060073DC RID: 29660 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRequest(string baseUrl, DLCList dlcList, Action<DLCList.DLCInfo> updateHandler, Action completeHandler, Action errorHandler, Action cancelHandler)
		{
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateProgress()
		{
		}

		// Token: 0x060073DF RID: 29663 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateError()
		{
		}

		// Token: 0x060073E0 RID: 29664 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCancel()
		{
		}

		// Token: 0x060073E1 RID: 29665 RVA: 0x0000216D File Offset: 0x0000036D
		public void Retry()
		{
		}

		// Token: 0x060073E2 RID: 29666 RVA: 0x0000216D File Offset: 0x0000036D
		public void Cancel()
		{
		}

		// Token: 0x0400ACCD RID: 44237
		private const int kEnableTaskMax = 4;

		// Token: 0x0400ACCE RID: 44238
		private TaskDownloadAssetManager.State m_state;

		// Token: 0x0400ACCF RID: 44239
		private string m_baseUrl;

		// Token: 0x0400ACD0 RID: 44240
		private List<TaskDownloadAsset> m_tasks;

		// Token: 0x0400ACD1 RID: 44241
		private Queue<DLCList.DLCInfo> m_requestTask;

		// Token: 0x0400ACD2 RID: 44242
		private Action<DLCList.DLCInfo> m_updateHandler;

		// Token: 0x0400ACD3 RID: 44243
		private Action m_completeHandler;

		// Token: 0x0400ACD4 RID: 44244
		private Action m_errorHandler;

		// Token: 0x0400ACD5 RID: 44245
		private Action m_cancelHandler;

		// Token: 0x02000F5D RID: 3933
		private enum State
		{
			// Token: 0x0400ACD7 RID: 44247
			None,
			// Token: 0x0400ACD8 RID: 44248
			Progress,
			// Token: 0x0400ACD9 RID: 44249
			Success,
			// Token: 0x0400ACDA RID: 44250
			Cancel,
			// Token: 0x0400ACDB RID: 44251
			Error,
			// Token: 0x0400ACDC RID: 44252
			Wait
		}
	}
}
