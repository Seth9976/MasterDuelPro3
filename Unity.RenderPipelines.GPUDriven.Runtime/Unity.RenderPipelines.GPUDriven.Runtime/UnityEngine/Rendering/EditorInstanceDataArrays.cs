using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006E RID: 110
	internal struct EditorInstanceDataArrays : IDataArrays
	{
		// Token: 0x06000224 RID: 548 RVA: 0x00004C45 File Offset: 0x00002E45
		public void Initialize(int initCapacity)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00004C45 File Offset: 0x00002E45
		public void Dispose()
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00004C45 File Offset: 0x00002E45
		public void Grow(int newCapacity)
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00004C45 File Offset: 0x00002E45
		public void Remove(int index, int lastIndex)
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00004C45 File Offset: 0x00002E45
		public void SetDefault(int index)
		{
		}

		// Token: 0x0200006F RID: 111
		internal readonly struct ReadOnly
		{
			// Token: 0x06000229 RID: 553 RVA: 0x00004C45 File Offset: 0x00002E45
			public ReadOnly(in CPUInstanceData instanceData)
			{
			}
		}
	}
}
