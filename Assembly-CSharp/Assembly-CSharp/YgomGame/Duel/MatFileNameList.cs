using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000ED0 RID: 3792
	public class MatFileNameList : ScriptableObject
	{
		// Token: 0x06006E83 RID: 28291 RVA: 0x0000216A File Offset: 0x0000036A
		public List<string> getFileNameList()
		{
			return null;
		}

		// Token: 0x0400A968 RID: 43368
		public List<MatFileNameList.MatFileNameInfoList> matInfoList;

		// Token: 0x02000ED1 RID: 3793
		[Serializable]
		public class MatFileNameInfoList
		{
			// Token: 0x06006E85 RID: 28293 RVA: 0x0000216A File Offset: 0x0000036A
			public MatFileNameList.MatFileNameInfoList Copy()
			{
				return null;
			}

			// Token: 0x0400A969 RID: 43369
			public int id;

			// Token: 0x0400A96A RID: 43370
			public string fileName;
		}
	}
}
