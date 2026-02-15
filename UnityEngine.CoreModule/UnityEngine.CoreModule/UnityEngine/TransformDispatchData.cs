using System;
using Unity.Collections;

namespace UnityEngine
{
	// Token: 0x02000154 RID: 340
	internal struct TransformDispatchData : IDisposable
	{
		// Token: 0x06000EE3 RID: 3811 RVA: 0x0001F5E8 File Offset: 0x0001D7E8
		public void Dispose()
		{
			this.transformedID.Dispose();
			this.parentID.Dispose();
			this.localToWorldMatrices.Dispose();
			this.positions.Dispose();
			this.rotations.Dispose();
			this.scales.Dispose();
		}

		// Token: 0x040005D9 RID: 1497
		public NativeArray<int> transformedID;

		// Token: 0x040005DA RID: 1498
		public NativeArray<int> parentID;

		// Token: 0x040005DB RID: 1499
		public NativeArray<Matrix4x4> localToWorldMatrices;

		// Token: 0x040005DC RID: 1500
		public NativeArray<Vector3> positions;

		// Token: 0x040005DD RID: 1501
		public NativeArray<Quaternion> rotations;

		// Token: 0x040005DE RID: 1502
		public NativeArray<Vector3> scales;
	}
}
