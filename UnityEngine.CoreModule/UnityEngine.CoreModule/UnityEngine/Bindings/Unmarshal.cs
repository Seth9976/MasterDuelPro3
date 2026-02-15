using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Bindings
{
	// Token: 0x0200023C RID: 572
	[VisibleToOtherModules]
	internal struct Unmarshal
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x0002B7F8 File Offset: 0x000299F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T UnmarshalUnityObject<T>(IntPtr gcHandlePtr) where T : Object
		{
			bool flag = gcHandlePtr == IntPtr.Zero;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				T target = (T)((object)Unmarshal.FromIntPtrUnsafe(gcHandlePtr).Target);
				t = target;
			}
			return t;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0002B840 File Offset: 0x00029A40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static GCHandle FromIntPtrUnsafe(IntPtr gcHandle)
		{
			return *UnsafeUtility.As<IntPtr, GCHandle>(ref gcHandle);
		}
	}
}
