using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x02000362 RID: 866
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public sealed class RayTracingAccelerationStructure : IDisposable
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x0002F6F9 File Offset: 0x0002D8F9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0002F70C File Offset: 0x0002D90C
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				RayTracingAccelerationStructure.Destroy(this);
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0002F734 File Offset: 0x0002D934
		[FreeFunction("RayTracingAccelerationStructure_Bindings::Destroy")]
		private static void Destroy(RayTracingAccelerationStructure accelStruct)
		{
			RayTracingAccelerationStructure.Destroy_Injected((accelStruct == null) ? ((IntPtr)0) : RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelStruct));
		}

		// Token: 0x06001696 RID: 5782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy_Injected(IntPtr accelStruct);

		// Token: 0x04000A2E RID: 2606
		internal IntPtr m_Ptr;

		// Token: 0x02000363 RID: 867
		public struct BuildSettings
		{
			// Token: 0x1700037A RID: 890
			// (set) Token: 0x06001697 RID: 5783 RVA: 0x0002F755 File Offset: 0x0002D955
			public RayTracingAccelerationStructureBuildFlags buildFlags
			{
				[CompilerGenerated]
				set
				{
					this.<buildFlags>k__BackingField = value;
				}
			}

			// Token: 0x1700037B RID: 891
			// (set) Token: 0x06001698 RID: 5784 RVA: 0x0002F75E File Offset: 0x0002D95E
			public Vector3 relativeOrigin
			{
				[CompilerGenerated]
				set
				{
					this.<relativeOrigin>k__BackingField = value;
				}
			}

			// Token: 0x06001699 RID: 5785 RVA: 0x0002F767 File Offset: 0x0002D967
			public BuildSettings()
			{
				this.buildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
				this.relativeOrigin = Vector3.zero;
			}
		}

		// Token: 0x02000364 RID: 868
		internal static class BindingsMarshaller
		{
			// Token: 0x0600169A RID: 5786 RVA: 0x0002F77E File Offset: 0x0002D97E
			public static IntPtr ConvertToNative(RayTracingAccelerationStructure rayTracingAccelerationStructure)
			{
				return rayTracingAccelerationStructure.m_Ptr;
			}
		}
	}
}
