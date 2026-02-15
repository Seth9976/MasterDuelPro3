using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C8E RID: 3214
	public class AvatarStandFileNameList : ScriptableObject
	{
		// Token: 0x06005C13 RID: 23571 RVA: 0x0000216A File Offset: 0x0000036A
		public List<string> getFileNameList()
		{
			return null;
		}

		// Token: 0x04009745 RID: 38725
		public List<AvatarStandFileNameList.AvatarStandFileNameInfoList> avatarStandInfoList;

		// Token: 0x02000C8F RID: 3215
		[Serializable]
		public class AvatarStandFileNameInfoList
		{
			// Token: 0x06005C15 RID: 23573 RVA: 0x0000216A File Offset: 0x0000036A
			public AvatarStandFileNameList.AvatarStandFileNameInfoList Copy()
			{
				return null;
			}

			// Token: 0x04009746 RID: 38726
			public int id;

			// Token: 0x04009747 RID: 38727
			public string fileName;
		}
	}
}
