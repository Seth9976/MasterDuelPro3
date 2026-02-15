using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200003D RID: 61
	public interface ICinemachineTargetGroup
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000169 RID: 361
		Transform Transform { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016A RID: 362
		Bounds BoundingBox { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016B RID: 363
		BoundingSphere Sphere { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016C RID: 364
		bool IsEmpty { get; }

		// Token: 0x0600016D RID: 365
		Bounds GetViewSpaceBoundingBox(Matrix4x4 observer);

		// Token: 0x0600016E RID: 366
		void GetViewSpaceAngularBounds(Matrix4x4 observer, out Vector2 minAngles, out Vector2 maxAngles, out Vector2 zRange);
	}
}
