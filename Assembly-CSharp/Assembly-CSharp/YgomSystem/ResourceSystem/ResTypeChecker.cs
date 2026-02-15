using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006E5 RID: 1765
	public class ResTypeChecker : IResTypeChecker
	{
		// Token: 0x06003702 RID: 14082 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearCache()
		{
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x0000216A File Offset: 0x0000036A
		public ResTypeData GetResType(string path)
		{
			return null;
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x0000216A File Offset: 0x0000036A
		public ResTypeData GetResTypeSimpleCheck(string path)
		{
			return null;
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateResType(Resource res)
		{
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator CheckFileTypeAsync(Resource res, ResourceManager.ReqType queueId)
		{
			return null;
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator checkFileAssetBundleAsync(int id, string path, bool isStreamingAssets = false, Action<bool> callback = null)
		{
			return null;
		}

		// Token: 0x04003148 RID: 12616
		private ResTypeChecker.CheckFileTypeParam[] threadParam;

		// Token: 0x04003149 RID: 12617
		private ResTypeChecker.CheckFileTypeThread[] threads;

		// Token: 0x0400314A RID: 12618
		private List<IResTypeChecker> m_checkList;

		// Token: 0x020006E6 RID: 1766
		private class CheckFileTypeParam
		{
			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x0600370B RID: 14091 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x0600370C RID: 14092 RVA: 0x0000216D File Offset: 0x0000036D
			public int Result
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x0600370D RID: 14093 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600370E RID: 14094 RVA: 0x0000216D File Offset: 0x0000036D
			public string Path
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x0600370F RID: 14095 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003710 RID: 14096 RVA: 0x0000216D File Offset: 0x0000036D
			public bool isStreaming
			{
				[CompilerGenerated]
				get
				{
					return false;
				}
				[CompilerGenerated]
				set
				{
				}
			}
		}

		// Token: 0x020006E7 RID: 1767
		private class CheckFileTypeThread : PooledThreadBase
		{
			// Token: 0x06003712 RID: 14098 RVA: 0x000029CC File Offset: 0x00000BCC
			protected override bool ExecThread(object parameter)
			{
				return false;
			}

			// Token: 0x06003713 RID: 14099 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool CheckFromLocalFileSystem(ResTypeChecker.CheckFileTypeParam param)
			{
				return false;
			}

			// Token: 0x06003714 RID: 14100 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool CheckFromFileStream(ResTypeChecker.CheckFileTypeParam param)
			{
				return false;
			}
		}
	}
}
