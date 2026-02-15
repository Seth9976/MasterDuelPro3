using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000E98 RID: 3736
	public class GraveFileNameList : ScriptableObject
	{
		// Token: 0x06006CA6 RID: 27814 RVA: 0x0000216A File Offset: 0x0000036A
		public List<string> getFileNameList()
		{
			return null;
		}

		// Token: 0x0400A7FA RID: 43002
		public List<GraveFileNameList.GraveFileNameInfoList> graveInfoList;

		// Token: 0x02000E99 RID: 3737
		[Serializable]
		public class GraveFileNameInfoList
		{
			// Token: 0x06006CA8 RID: 27816 RVA: 0x0000216A File Offset: 0x0000036A
			public GraveFileNameList.GraveFileNameInfoList Copy()
			{
				return null;
			}

			// Token: 0x0400A7FB RID: 43003
			public int id;

			// Token: 0x0400A7FC RID: 43004
			public string fileName;
		}
	}
}
