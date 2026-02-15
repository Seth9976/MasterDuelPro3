using System;
using System.Reflection;

namespace UnityEngine.UI
{
	// Token: 0x0200007A RID: 122
	internal class ReflectionMethodsCache
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x000164D8 File Offset: 0x000146D8
		public ReflectionMethodsCache()
		{
			MethodInfo raycast3DMethodInfo = typeof(Physics).GetMethod("Raycast", new Type[]
			{
				typeof(Ray),
				typeof(RaycastHit).MakeByRefType(),
				typeof(float),
				typeof(int)
			});
			if (raycast3DMethodInfo != null)
			{
				this.raycast3D = (ReflectionMethodsCache.Raycast3DCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.Raycast3DCallback), raycast3DMethodInfo);
			}
			MethodInfo raycastAllMethodInfo = typeof(Physics).GetMethod("RaycastAll", new Type[]
			{
				typeof(Ray),
				typeof(float),
				typeof(int)
			});
			if (raycastAllMethodInfo != null)
			{
				this.raycast3DAll = (ReflectionMethodsCache.RaycastAllCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.RaycastAllCallback), raycastAllMethodInfo);
			}
			MethodInfo getRaycastAllNonAllocMethodInfo = typeof(Physics).GetMethod("RaycastNonAlloc", new Type[]
			{
				typeof(Ray),
				typeof(RaycastHit[]),
				typeof(float),
				typeof(int)
			});
			if (getRaycastAllNonAllocMethodInfo != null)
			{
				this.getRaycastNonAlloc = (ReflectionMethodsCache.GetRaycastNonAllocCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.GetRaycastNonAllocCallback), getRaycastAllNonAllocMethodInfo);
			}
			MethodInfo raycast2DMethodInfo = typeof(Physics2D).GetMethod("Raycast", new Type[]
			{
				typeof(Vector2),
				typeof(Vector2),
				typeof(float),
				typeof(int)
			});
			if (raycast2DMethodInfo != null)
			{
				this.raycast2D = (ReflectionMethodsCache.Raycast2DCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.Raycast2DCallback), raycast2DMethodInfo);
			}
			MethodInfo getRayIntersectionAllMethodInfo = typeof(Physics2D).GetMethod("GetRayIntersectionAll", new Type[]
			{
				typeof(Ray),
				typeof(float),
				typeof(int)
			});
			if (getRayIntersectionAllMethodInfo != null)
			{
				this.getRayIntersectionAll = (ReflectionMethodsCache.GetRayIntersectionAllCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.GetRayIntersectionAllCallback), getRayIntersectionAllMethodInfo);
			}
			MethodInfo getRayIntersectionAllNonAllocMethodInfo = typeof(Physics2D).GetMethod("GetRayIntersectionNonAlloc", new Type[]
			{
				typeof(Ray),
				typeof(RaycastHit2D[]),
				typeof(float),
				typeof(int)
			});
			if (getRayIntersectionAllNonAllocMethodInfo != null)
			{
				this.getRayIntersectionAllNonAlloc = (ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback), getRayIntersectionAllNonAllocMethodInfo);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001678E File Offset: 0x0001498E
		public static ReflectionMethodsCache Singleton
		{
			get
			{
				if (ReflectionMethodsCache.s_ReflectionMethodsCache == null)
				{
					ReflectionMethodsCache.s_ReflectionMethodsCache = new ReflectionMethodsCache();
				}
				return ReflectionMethodsCache.s_ReflectionMethodsCache;
			}
		}

		// Token: 0x0400025B RID: 603
		public ReflectionMethodsCache.Raycast3DCallback raycast3D;

		// Token: 0x0400025C RID: 604
		public ReflectionMethodsCache.RaycastAllCallback raycast3DAll;

		// Token: 0x0400025D RID: 605
		public ReflectionMethodsCache.GetRaycastNonAllocCallback getRaycastNonAlloc;

		// Token: 0x0400025E RID: 606
		public ReflectionMethodsCache.Raycast2DCallback raycast2D;

		// Token: 0x0400025F RID: 607
		public ReflectionMethodsCache.GetRayIntersectionAllCallback getRayIntersectionAll;

		// Token: 0x04000260 RID: 608
		public ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback getRayIntersectionAllNonAlloc;

		// Token: 0x04000261 RID: 609
		private static ReflectionMethodsCache s_ReflectionMethodsCache;

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x060004FF RID: 1279
		public delegate bool Raycast3DCallback(Ray r, out RaycastHit hit, float f, int i);

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x06000503 RID: 1283
		public delegate RaycastHit[] RaycastAllCallback(Ray r, float f, int i);

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x06000507 RID: 1287
		public delegate int GetRaycastNonAllocCallback(Ray r, RaycastHit[] results, float f, int i);

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x0600050B RID: 1291
		public delegate RaycastHit2D Raycast2DCallback(Vector2 p1, Vector2 p2, float f, int i);

		// Token: 0x0200007F RID: 127
		// (Invoke) Token: 0x0600050F RID: 1295
		public delegate RaycastHit2D[] GetRayIntersectionAllCallback(Ray r, float f, int i);

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x06000513 RID: 1299
		public delegate int GetRayIntersectionAllNonAllocCallback(Ray r, RaycastHit2D[] results, float f, int i);
	}
}
