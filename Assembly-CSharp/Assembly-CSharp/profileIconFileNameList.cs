using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class profileIconFileNameList : ScriptableObject
{
	// Token: 0x060001B3 RID: 435 RVA: 0x0000216A File Offset: 0x0000036A
	public List<string> getFileNameList()
	{
		return null;
	}

	// Token: 0x04000222 RID: 546
	public List<profileIconFileNameList.ProfileIconFileNameInfoList> profileIconFileNameInfoList;

	// Token: 0x0200005F RID: 95
	[Serializable]
	public class ProfileIconFileNameInfoList
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000216A File Offset: 0x0000036A
		public profileIconFileNameList.ProfileIconFileNameInfoList Copy()
		{
			return null;
		}

		// Token: 0x04000223 RID: 547
		public int id;

		// Token: 0x04000224 RID: 548
		public string fileName;
	}
}
