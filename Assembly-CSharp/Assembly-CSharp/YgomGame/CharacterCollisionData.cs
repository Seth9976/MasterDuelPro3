using System;
using UnityEngine;

namespace YgomGame
{
	// Token: 0x020007B3 RID: 1971
	[Serializable]
	public class CharacterCollisionData
	{
		// Token: 0x06003D3B RID: 15675 RVA: 0x00002739 File Offset: 0x00000939
		public CharacterCollisionData(GameObject target, Vector3 center, Vector3 size)
		{
		}

		// Token: 0x040035B9 RID: 13753
		public GameObject target;

		// Token: 0x040035BA RID: 13754
		public Vector3 center;

		// Token: 0x040035BB RID: 13755
		public Vector3 size;
	}
}
