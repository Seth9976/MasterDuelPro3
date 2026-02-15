using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200031E RID: 798
	[RequireComponent(typeof(Transform))]
	[NativeType(Header = "Runtime/2D/Sorting/SortingGroup.h")]
	public sealed class SortingGroup : Behaviour
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600161D RID: 5661
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		internal static extern int invalidSortingGroupID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0002E7B4 File Offset: 0x0002C9B4
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		internal static SortingGroup GetSortingGroupByIndex(int index)
		{
			return Unmarshal.UnmarshalUnityObject<SortingGroup>(SortingGroup.GetSortingGroupByIndex_Injected(index));
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0002E7CC File Offset: 0x0002C9CC
		public int sortingLayerID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortingLayerID_Injected(intPtr);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
		public int sortingOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortingOrder_Injected(intPtr);
			}
		}

		// Token: 0x06001621 RID: 5665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSortingGroupByIndex_Injected(int index);

		// Token: 0x06001622 RID: 5666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingLayerID_Injected(IntPtr _unity_self);

		// Token: 0x06001623 RID: 5667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingOrder_Injected(IntPtr _unity_self);
	}
}
