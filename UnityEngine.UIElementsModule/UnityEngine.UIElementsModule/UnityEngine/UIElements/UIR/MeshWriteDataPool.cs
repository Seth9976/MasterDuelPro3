using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000543 RID: 1347
	internal class MeshWriteDataPool : ImplicitPool<MeshWriteData>
	{
		// Token: 0x0600251F RID: 9503 RVA: 0x00090E1E File Offset: 0x0008F01E
		public MeshWriteDataPool()
			: base(MeshWriteDataPool.k_CreateAction, null, 100, 1000)
		{
		}

		// Token: 0x04001243 RID: 4675
		private static readonly Func<MeshWriteData> k_CreateAction = () => new MeshWriteData();
	}
}
