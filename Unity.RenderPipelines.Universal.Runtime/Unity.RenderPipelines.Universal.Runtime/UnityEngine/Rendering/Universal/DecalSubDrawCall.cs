using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000083 RID: 131
	internal struct DecalSubDrawCall
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000A0A1 File Offset: 0x000082A1
		public int count
		{
			get
			{
				return this.end - this.start;
			}
		}

		// Token: 0x0400025D RID: 605
		public int start;

		// Token: 0x0400025E RID: 606
		public int end;
	}
}
