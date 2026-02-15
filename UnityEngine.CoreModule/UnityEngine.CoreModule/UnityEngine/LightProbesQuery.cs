using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000D3 RID: 211
	[StaticAccessor("LightProbeContextWrapper", StaticAccessorType.DoubleColon)]
	[NativeContainer]
	[NativeHeader("Runtime/Camera/RenderLoops/LightProbeContext.h")]
	public struct LightProbesQuery : IDisposable
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0000BF92 File Offset: 0x0000A192
		public LightProbesQuery(Allocator allocator)
		{
			this.m_LightProbeContextWrapper = LightProbesQuery.Create();
			this.m_AllocatorLabel = allocator;
			UnsafeUtility.LeakRecord(this.m_LightProbeContextWrapper, LeakCategory.LightProbesQuery, 0);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		public void Dispose()
		{
			bool flag = this.m_LightProbeContextWrapper == IntPtr.Zero;
			if (flag)
			{
				throw new ObjectDisposedException("The LightProbesQuery is already disposed.");
			}
			bool flag2 = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag2)
			{
				throw new InvalidOperationException("The LightProbesQuery can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			if (flag3)
			{
				UnsafeUtility.LeakErase(this.m_LightProbeContextWrapper, LeakCategory.LightProbesQuery);
				LightProbesQuery.Destroy(this.m_LightProbeContextWrapper);
				this.m_AllocatorLabel = Allocator.Invalid;
			}
			this.m_LightProbeContextWrapper = IntPtr.Zero;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000C03C File Offset: 0x0000A23C
		public JobHandle Dispose(JobHandle inputDeps)
		{
			bool flag = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag)
			{
				throw new InvalidOperationException("The LightProbesQuery can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag2 = this.m_LightProbeContextWrapper == IntPtr.Zero;
			if (flag2)
			{
				throw new InvalidOperationException("The LightProbesQuery is already disposed.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			JobHandle jobHandle2;
			if (flag3)
			{
				JobHandle jobHandle = new LightProbesQuery.LightProbesQueryDisposeJob
				{
					Data = new LightProbesQuery.LightProbesQueryDispose
					{
						m_LightProbeContextWrapper = this.m_LightProbeContextWrapper
					}
				}.Schedule(inputDeps);
				this.m_AllocatorLabel = Allocator.Invalid;
				this.m_LightProbeContextWrapper = IntPtr.Zero;
				jobHandle2 = jobHandle;
			}
			else
			{
				this.m_LightProbeContextWrapper = IntPtr.Zero;
				jobHandle2 = inputDeps;
			}
			return jobHandle2;
		}

		// Token: 0x06000570 RID: 1392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create();

		// Token: 0x06000571 RID: 1393
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr lightProbeContextWrapper);

		// Token: 0x06000572 RID: 1394 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public void CalculateInterpolatedLightAndOcclusionProbes(NativeArray<Vector3> positions, NativeArray<int> tetrahedronIndices, NativeArray<SphericalHarmonicsL2> lightProbes, NativeArray<Vector4> occlusionProbes)
		{
			bool flag = tetrahedronIndices.Length < positions.Length;
			if (flag)
			{
				throw new ArgumentException("tetrahedronIndices", "Argument tetrahedronIndices is null or has fewer elements than positions.");
			}
			bool flag2 = lightProbes.Length < positions.Length;
			if (flag2)
			{
				throw new ArgumentException("lightProbes", "Argument lightProbes is null or has fewer elements than positions.");
			}
			bool flag3 = occlusionProbes.Length < positions.Length;
			if (flag3)
			{
				throw new ArgumentException("occlusionProbes", "Argument occlusionProbes is null or has fewer elements than positions.");
			}
			LightProbesQuery.CalculateInterpolatedLightAndOcclusionProbes(this.m_LightProbeContextWrapper, (IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), (IntPtr)tetrahedronIndices.GetUnsafeReadOnlyPtr<int>(), (IntPtr)lightProbes.GetUnsafePtr<SphericalHarmonicsL2>(), (IntPtr)occlusionProbes.GetUnsafePtr<Vector4>(), positions.Length);
		}

		// Token: 0x06000573 RID: 1395
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateInterpolatedLightAndOcclusionProbes(IntPtr lightProbeContextWrapper, IntPtr positions, IntPtr tetrahedronIndices, IntPtr lightProbes, IntPtr occlusionProbes, int count);

		// Token: 0x04000280 RID: 640
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr m_LightProbeContextWrapper;

		// Token: 0x04000281 RID: 641
		internal Allocator m_AllocatorLabel;

		// Token: 0x020000D4 RID: 212
		[NativeContainer]
		internal struct LightProbesQueryDispose
		{
			// Token: 0x06000574 RID: 1396 RVA: 0x0000C1AC File Offset: 0x0000A3AC
			public void Dispose()
			{
				UnsafeUtility.LeakErase(this.m_LightProbeContextWrapper, LeakCategory.LightProbesQuery);
				LightProbesQuery.Destroy(this.m_LightProbeContextWrapper);
			}

			// Token: 0x04000282 RID: 642
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr m_LightProbeContextWrapper;
		}

		// Token: 0x020000D5 RID: 213
		internal struct LightProbesQueryDisposeJob : IJob
		{
			// Token: 0x06000575 RID: 1397 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
			public void Execute()
			{
				this.Data.Dispose();
			}

			// Token: 0x04000283 RID: 643
			internal LightProbesQuery.LightProbesQueryDispose Data;
		}
	}
}
