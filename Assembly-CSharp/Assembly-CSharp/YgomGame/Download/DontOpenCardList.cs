using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Download
{
	// Token: 0x02000F51 RID: 3921
	public class DontOpenCardList : ScriptableObject
	{
		// Token: 0x0600739C RID: 29596 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> GetCardList()
		{
			return null;
		}

		// Token: 0x0400ACA8 RID: 44200
		private static DontOpenCardList m_Instance;

		// Token: 0x0400ACA9 RID: 44201
		private const string path = "Download/ScriptableObject/DontOpenCardList";

		// Token: 0x0400ACAA RID: 44202
		public List<DontOpenCardList.DontOpenInfoList> dontOpenInfoList;

		// Token: 0x0400ACAB RID: 44203
		public List<int> m_avoidList;

		// Token: 0x02000F52 RID: 3922
		[Serializable]
		public class DontOpenInfoList
		{
			// Token: 0x0600739E RID: 29598 RVA: 0x0000216A File Offset: 0x0000036A
			public DontOpenCardList.DontOpenInfoList Copy()
			{
				return null;
			}

			// Token: 0x0400ACAC RID: 44204
			public int id;

			// Token: 0x0400ACAD RID: 44205
			public string Mrk;
		}
	}
}
