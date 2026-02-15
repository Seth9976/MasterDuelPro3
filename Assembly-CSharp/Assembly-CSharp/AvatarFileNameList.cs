using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class AvatarFileNameList : ScriptableObject
{
	// Token: 0x06000035 RID: 53 RVA: 0x0000216A File Offset: 0x0000036A
	public List<string> getFileNameList()
	{
		return null;
	}

	// Token: 0x04000028 RID: 40
	public List<AvatarFileNameList.AvatarFileNameInfoList> avatarInfoList;

	// Token: 0x02000012 RID: 18
	[Serializable]
	public class AvatarFileNameInfoList
	{
		// Token: 0x06000037 RID: 55 RVA: 0x0000216A File Offset: 0x0000036A
		public AvatarFileNameList.AvatarFileNameInfoList Copy()
		{
			return null;
		}

		// Token: 0x04000029 RID: 41
		public int id;

		// Token: 0x0400002A RID: 42
		public string fileName;
	}
}
