using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class ProtectorFileNameList : ScriptableObject
{
	// Token: 0x060000FA RID: 250 RVA: 0x0000216A File Offset: 0x0000036A
	public List<string> getFileNameList()
	{
		return null;
	}

	// Token: 0x04000184 RID: 388
	public List<ProtectorFileNameList.ProtectorFileNameInfoList> protectorInfoList;

	// Token: 0x02000040 RID: 64
	[Serializable]
	public class ProtectorFileNameInfoList
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000216A File Offset: 0x0000036A
		public ProtectorFileNameList.ProtectorFileNameInfoList Copy()
		{
			return null;
		}

		// Token: 0x04000185 RID: 389
		public int id;

		// Token: 0x04000186 RID: 390
		public string fileName;
	}
}
