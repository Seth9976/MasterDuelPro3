using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000749 RID: 1865
	public class LocalFileManager : MonoBehaviour
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000F1669 File Offset: 0x000EF869
		public ulong frameCounter
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(string env, bool enableLog)
		{
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLoadTaskLimit(int num)
		{
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetLoadTaskLimit()
		{
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0000216A File Offset: 0x0000036A
		public LocalFileAssetBundleLoadRequest RequestAssetBundleLoad(string nativePath)
		{
			return null;
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAssetBundleRequestQueueCount()
		{
			return 0;
		}

		// Token: 0x04003434 RID: 13364
		private bool m_initialized;

		// Token: 0x04003435 RID: 13365
		private ulong m_frameCounter;

		// Token: 0x04003436 RID: 13366
		private Queue<LocalFileAssetBundleLoadRequest> m_assetBundleLoadQueue;

		// Token: 0x04003437 RID: 13367
		private List<LocalFileAssetBundleLoadRequest> m_assetBundleLoadTask;

		// Token: 0x04003438 RID: 13368
		private const int defaultLoadTaskLimit = 10;

		// Token: 0x04003439 RID: 13369
		private int m_loadTaskLimit;
	}
}
