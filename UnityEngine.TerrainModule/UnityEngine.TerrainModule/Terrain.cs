using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("GetITerrainManager()", StaticAccessorType.Arrow)]
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Runtime/Interfaces/ITerrainManager.h")]
	[NativeHeader("Modules/Terrain/Public/Terrain.h")]
	[UsedByNativeCode]
	public sealed class Terrain : Behaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public TerrainData terrainData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<TerrainData>(Terrain.get_terrainData_Injected(intPtr));
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public bool allowAutoConnect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_allowAutoConnect_Injected(intPtr);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000209C File Offset: 0x0000029C
		public int groupingID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_groupingID_Injected(intPtr);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.SetNeighbors_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Terrain>(left), Object.MarshalledUnityObject.Marshal<Terrain>(top), Object.MarshalledUnityObject.Marshal<Terrain>(right), Object.MarshalledUnityObject.Marshal<Terrain>(bottom));
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5
		[NativeProperty("ActiveTerrainsScriptingArray")]
		public static extern Terrain[] activeTerrains
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: Unmarshalled]
			get;
		}

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_terrainData_Injected(IntPtr _unity_self);

		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowAutoConnect_Injected(IntPtr _unity_self);

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_groupingID_Injected(IntPtr _unity_self);

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNeighbors_Injected(IntPtr _unity_self, IntPtr left, IntPtr top, IntPtr right, IntPtr bottom);
	}
}
