using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
	[UsedByNativeCode]
	public sealed class TerrainData : Object
	{
		// Token: 0x06000011 RID: 17
		[ThreadSafe]
		[StaticAccessor("TerrainDataScriptingInterface", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBoundaryValue(TerrainData.BoundaryValueType type);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002190 File Offset: 0x00000390
		public Vector3 size
		{
			[NativeName("GetHeightmap().GetSize")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				TerrainData.get_size_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021B8 File Offset: 0x000003B8
		[RequiredByNativeCode]
		[NativeName("GetSplatDatabase().GetAlphamapResolution")]
		internal float GetAlphamapResolutionInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.GetAlphamapResolutionInternal_Injected(intPtr);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021DC File Offset: 0x000003DC
		internal Terrain[] users
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_users_Injected(intPtr);
			}
		}

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAlphamapResolutionInternal_Injected(IntPtr _unity_self);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Terrain[] get_users_Injected(IntPtr _unity_self);

		// Token: 0x04000003 RID: 3
		internal static readonly int k_MaximumResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxHeightmapRes);

		// Token: 0x04000004 RID: 4
		internal static readonly int k_MinimumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinDetailResPerPatch);

		// Token: 0x04000005 RID: 5
		internal static readonly int k_MaximumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailResPerPatch);

		// Token: 0x04000006 RID: 6
		internal static readonly int k_MaximumDetailPatchCount = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailPatchCount);

		// Token: 0x04000007 RID: 7
		internal static readonly int k_MinimumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinAlphamapRes);

		// Token: 0x04000008 RID: 8
		internal static readonly int k_MaximumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxAlphamapRes);

		// Token: 0x04000009 RID: 9
		internal static readonly int k_MinimumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinBaseMapRes);

		// Token: 0x0400000A RID: 10
		internal static readonly int k_MaximumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxBaseMapRes);

		// Token: 0x02000007 RID: 7
		private enum BoundaryValueType
		{
			// Token: 0x0400000C RID: 12
			MaxHeightmapRes,
			// Token: 0x0400000D RID: 13
			MinDetailResPerPatch,
			// Token: 0x0400000E RID: 14
			MaxDetailResPerPatch,
			// Token: 0x0400000F RID: 15
			MaxDetailPatchCount,
			// Token: 0x04000010 RID: 16
			MaxCoveragePerRes,
			// Token: 0x04000011 RID: 17
			MinAlphamapRes,
			// Token: 0x04000012 RID: 18
			MaxAlphamapRes,
			// Token: 0x04000013 RID: 19
			MinBaseMapRes,
			// Token: 0x04000014 RID: 20
			MaxBaseMapRes
		}
	}
}
