using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class ProfileIconFrameFileNameList : ScriptableObject
{
	// Token: 0x060000F6 RID: 246 RVA: 0x0000216A File Offset: 0x0000036A
	public List<string> getFileNameList()
	{
		return null;
	}

	// Token: 0x04000181 RID: 385
	public List<ProfileIconFrameFileNameList.ProfileIconFrameFileNameInfoList> profileIconFrameFileNameInfoList;

	// Token: 0x0200003E RID: 62
	[Serializable]
	public class ProfileIconFrameFileNameInfoList
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000216A File Offset: 0x0000036A
		public ProfileIconFrameFileNameList.ProfileIconFrameFileNameInfoList Copy()
		{
			return null;
		}

		// Token: 0x04000182 RID: 386
		public int id;

		// Token: 0x04000183 RID: 387
		public string fileName;
	}
}
