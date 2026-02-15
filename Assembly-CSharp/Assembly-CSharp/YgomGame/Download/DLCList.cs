using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Download
{
	// Token: 0x02000F4E RID: 3918
	public class DLCList
	{
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600737F RID: 29567 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007380 RID: 29568 RVA: 0x0000216D File Offset: 0x0000036D
		public Dictionary<string, DLCList.DLCInfo> fileDict
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06007381 RID: 29569 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007382 RID: 29570 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06007383 RID: 29571 RVA: 0x0000216D File Offset: 0x0000036D
		private void Clear()
		{
		}

		// Token: 0x06007384 RID: 29572 RVA: 0x0000216A File Offset: 0x0000036A
		public List<string> GetAssetNames()
		{
			return null;
		}

		// Token: 0x06007385 RID: 29573 RVA: 0x000F1669 File Offset: 0x000EF869
		public long GetBytes()
		{
			return 0L;
		}

		// Token: 0x06007386 RID: 29574 RVA: 0x0000216D File Offset: 0x0000036D
		public void Set(DLCList.DLCInfo info)
		{
		}

		// Token: 0x06007387 RID: 29575 RVA: 0x0000216D File Offset: 0x0000036D
		public void Remove(DLCList.DLCInfo info)
		{
		}

		// Token: 0x06007388 RID: 29576 RVA: 0x0000216D File Offset: 0x0000036D
		public void Merge(DLCList merge)
		{
		}

		// Token: 0x06007389 RID: 29577 RVA: 0x0000216A File Offset: 0x0000036A
		public DLCList GetDiffList(DLCList target, bool versionCheck = true)
		{
			return null;
		}

		// Token: 0x0600738A RID: 29578 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetText()
		{
			return null;
		}

		// Token: 0x0600738B RID: 29579 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Exists(string path, bool convertHash = true)
		{
			return false;
		}

		// Token: 0x0600738C RID: 29580 RVA: 0x0000216A File Offset: 0x0000036A
		private static DLCList GetLocalInstance()
		{
			return null;
		}

		// Token: 0x0600738D RID: 29581 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsLocalData(string path)
		{
			return false;
		}

		// Token: 0x0600738E RID: 29582 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearLocalInstance()
		{
		}

		// Token: 0x0600738F RID: 29583 RVA: 0x0000216A File Offset: 0x0000036A
		public static DLCList Load()
		{
			return null;
		}

		// Token: 0x06007390 RID: 29584 RVA: 0x0000216A File Offset: 0x0000036A
		public static DLCList LoadStreaming()
		{
			return null;
		}

		// Token: 0x06007391 RID: 29585 RVA: 0x0000216A File Offset: 0x0000036A
		public static DLCList LoadFromJson(string json)
		{
			return null;
		}

		// Token: 0x06007392 RID: 29586 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Save(DLCList target)
		{
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Delete()
		{
		}

		// Token: 0x0400AC9E RID: 44190
		public const string kFileName = "dlcList.json";

		// Token: 0x0400AC9F RID: 44191
		[SerializeField]
		private List<DLCList.DLCInfo> informations;

		// Token: 0x0400ACA0 RID: 44192
		private static DLCList s_localList;

		// Token: 0x02000F4F RID: 3919
		[Serializable]
		public struct DLCInfo
		{
			// Token: 0x17000DCA RID: 3530
			// (get) Token: 0x06007395 RID: 29589 RVA: 0x0000216A File Offset: 0x0000036A
			public string path
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0400ACA1 RID: 44193
			public string assetName;

			// Token: 0x0400ACA2 RID: 44194
			public string version;

			// Token: 0x0400ACA3 RID: 44195
			public long bytes;

			// Token: 0x0400ACA4 RID: 44196
			public string crc;
		}
	}
}
