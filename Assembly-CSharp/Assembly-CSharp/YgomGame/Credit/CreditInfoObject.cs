using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Credit
{
	// Token: 0x02001012 RID: 4114
	public class CreditInfoObject : ScriptableObject
	{
		// Token: 0x0400B38B RID: 45963
		[SerializeField]
		public float scrollSpeed;

		// Token: 0x0400B38C RID: 45964
		[SerializeField]
		public List<CreditInfo> creditInfoList;
	}
}
