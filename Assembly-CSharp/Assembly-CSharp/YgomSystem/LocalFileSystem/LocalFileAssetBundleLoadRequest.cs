using System;
using UnityEngine;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000746 RID: 1862
	public class LocalFileAssetBundleLoadRequest
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDone
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSuccess
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCanceled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x0000216D File Offset: 0x0000036D
		private void clear()
		{
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStart(string nativePath)
		{
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCancel()
		{
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x04003421 RID: 13345
		private LocalFileAssetBundleLoadRequest.Step m_step;

		// Token: 0x04003422 RID: 13346
		private bool m_reqCancel;

		// Token: 0x04003423 RID: 13347
		private LocalFileStream m_fileStream;

		// Token: 0x04003424 RID: 13348
		private AssetBundleCreateRequest m_assetBundleRequest;

		// Token: 0x04003425 RID: 13349
		private ReadRequest m_fileReadRequest;

		// Token: 0x04003426 RID: 13350
		public string nativePath;

		// Token: 0x04003427 RID: 13351
		public AssetBundle assetBundle;

		// Token: 0x04003428 RID: 13352
		public LocalFileAssetBundleLoadRequest.Result result;

		// Token: 0x02000747 RID: 1863
		private enum Step
		{
			// Token: 0x0400342A RID: 13354
			Idle,
			// Token: 0x0400342B RID: 13355
			Start,
			// Token: 0x0400342C RID: 13356
			ReadFile,
			// Token: 0x0400342D RID: 13357
			LoadAssetBundle,
			// Token: 0x0400342E RID: 13358
			End
		}

		// Token: 0x02000748 RID: 1864
		public enum Result
		{
			// Token: 0x04003430 RID: 13360
			None,
			// Token: 0x04003431 RID: 13361
			Success,
			// Token: 0x04003432 RID: 13362
			Failed,
			// Token: 0x04003433 RID: 13363
			Canceled
		}
	}
}
