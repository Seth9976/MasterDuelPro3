using System;
using System.Collections;

namespace YgomGame.Download
{
	// Token: 0x02000F5B RID: 3931
	public class TaskDownloadAsset : TaskWebRequest
	{
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060073CB RID: 29643 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsEnableTask
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060073CC RID: 29644 RVA: 0x000F1669 File Offset: 0x000EF869
		public long size
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x000F6526 File Offset: 0x000F4726
		public TaskDownloadAsset(string _baseUrl, string _path)
			: base(null, null, TaskWebRequest.RequestType.Bytes)
		{
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x000F6526 File Offset: 0x000F4726
		public TaskDownloadAsset(string _baseUrl, DLCList.DLCInfo _info)
			: base(null, null, TaskWebRequest.RequestType.Bytes)
		{
		}

		// Token: 0x060073CF RID: 29647 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCompleteHandler(Action<DLCList.DLCInfo> callback)
		{
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x0000216A File Offset: 0x0000036A
		protected override IEnumerator yProgress()
		{
			return null;
		}

		// Token: 0x060073D1 RID: 29649 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InstallDataAsync(string path, byte[] data)
		{
			return null;
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x0000216D File Offset: 0x0000036D
		private void InstallData(string path, byte[] data)
		{
		}

		// Token: 0x0400ACCB RID: 44235
		private DLCList.DLCInfo m_info;

		// Token: 0x0400ACCC RID: 44236
		private Action<DLCList.DLCInfo> m_completeAction;
	}
}
