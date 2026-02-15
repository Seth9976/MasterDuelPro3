using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D16 RID: 3350
	internal struct ChainIndo
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060060AD RID: 24749 RVA: 0x000F5424 File Offset: 0x000F3624
		public Vector3 currentpos
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float currentlength
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x04009C07 RID: 39943
		public Transform chainhead;

		// Token: 0x04009C08 RID: 39944
		public float unitlength;

		// Token: 0x04009C09 RID: 39945
		public Stack<Transform> chainstack;

		// Token: 0x04009C0A RID: 39946
		public Vector3 srcpos;

		// Token: 0x04009C0B RID: 39947
		public Vector3 dstpos;
	}
}
