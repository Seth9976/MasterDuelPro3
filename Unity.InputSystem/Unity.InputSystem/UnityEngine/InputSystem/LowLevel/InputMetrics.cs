using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public struct InputMetrics
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x000513D1 File Offset: 0x0004F5D1
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x000513D9 File Offset: 0x0004F5D9
		public int maxNumDevices { readonly get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x000513E2 File Offset: 0x0004F5E2
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x000513EA File Offset: 0x0004F5EA
		public int currentNumDevices { readonly get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x000513F3 File Offset: 0x0004F5F3
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x000513FB File Offset: 0x0004F5FB
		public int maxStateSizeInBytes { readonly get; set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00051404 File Offset: 0x0004F604
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0005140C File Offset: 0x0004F60C
		public int currentStateSizeInBytes { readonly get; set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00051415 File Offset: 0x0004F615
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x0005141D File Offset: 0x0004F61D
		public int currentControlCount { readonly get; set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00051426 File Offset: 0x0004F626
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x0005142E File Offset: 0x0004F62E
		public int currentLayoutCount { readonly get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00051437 File Offset: 0x0004F637
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x0005143F File Offset: 0x0004F63F
		public int totalEventBytes { readonly get; set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00051448 File Offset: 0x0004F648
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00051450 File Offset: 0x0004F650
		public int totalEventCount { readonly get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00051459 File Offset: 0x0004F659
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00051461 File Offset: 0x0004F661
		public int totalUpdateCount { readonly get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0005146A File Offset: 0x0004F66A
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00051472 File Offset: 0x0004F672
		public double totalEventProcessingTime { readonly get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0005147B File Offset: 0x0004F67B
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00051483 File Offset: 0x0004F683
		public double totalEventLagTime { readonly get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0005148C File Offset: 0x0004F68C
		public float averageEventBytesPerFrame
		{
			get
			{
				return (float)this.totalEventBytes / (float)this.totalUpdateCount;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0005149D File Offset: 0x0004F69D
		public double averageProcessingTimePerEvent
		{
			get
			{
				return this.totalEventProcessingTime / (double)this.totalEventCount;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x000514AD File Offset: 0x0004F6AD
		public double averageLagTimePerEvent
		{
			get
			{
				return this.totalEventLagTime / (double)this.totalEventCount;
			}
		}
	}
}
