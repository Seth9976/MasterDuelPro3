using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame
{
	// Token: 0x020007D8 RID: 2008
	public class ResidentCreator : MonoBehaviour
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003E8F RID: 16015 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool isReady
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddObject(GameObject parent, string objectName, Type type)
		{
		}

		// Token: 0x04003784 RID: 14212
		private const string DownloadDataRevision = "dl_rev";

		// Token: 0x04003785 RID: 14213
		private const string DownloadListIndex = "dllstidx";

		// Token: 0x04003786 RID: 14214
		public static readonly string ResidentTag;

		// Token: 0x04003787 RID: 14215
		private static readonly Type[] ResidentGroups;
	}
}
