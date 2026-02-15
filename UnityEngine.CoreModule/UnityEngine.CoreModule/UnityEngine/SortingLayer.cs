using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200008B RID: 139
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct SortingLayer
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00005874 File Offset: 0x00003A74
		public int id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000588C File Offset: 0x00003A8C
		public int value
		{
			get
			{
				return SortingLayer.GetLayerValueFromID(this.m_Id);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000058AC File Offset: 0x00003AAC
		public static SortingLayer[] layers
		{
			get
			{
				int[] ids = SortingLayer.GetSortingLayerIDsInternal();
				SortingLayer[] layers = new SortingLayer[ids.Length];
				for (int i = 0; i < ids.Length; i++)
				{
					layers[i].m_Id = ids[i];
				}
				return layers;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000058F4 File Offset: 0x00003AF4
		[FreeFunction("GetTagManager().GetSortingLayerIDs")]
		private static int[] GetSortingLayerIDsInternal()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				SortingLayer.GetSortingLayerIDsInternal_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600021C RID: 540
		[FreeFunction("GetTagManager().GetSortingLayerValueFromUniqueID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLayerValueFromID(int id);

		// Token: 0x0600021D RID: 541 RVA: 0x00005928 File Offset: 0x00003B28
		[FreeFunction("GetTagManager().GetSortingLayerNameFromUniqueID")]
		public static string IDToName(int id)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SortingLayer.IDToName_Injected(id, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600021E RID: 542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSortingLayerIDsInternal_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600021F RID: 543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IDToName_Injected(int id, out ManagedSpanWrapper ret);

		// Token: 0x0400013B RID: 315
		private int m_Id;
	}
}
