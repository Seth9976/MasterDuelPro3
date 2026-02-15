using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000003 RID: 3
	[NativeType(Header = "Modules/VFX/Public/VFXEventAttribute.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXEventAttribute : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private VFXEventAttribute(IntPtr ptr, bool owner, VisualEffectAsset vfxAsset)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
			this.m_VfxAsset = vfxAsset;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
		internal static VFXEventAttribute CreateEventAttributeWrapper()
		{
			return new VFXEventAttribute(IntPtr.Zero, false, null);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		internal void SetWrapValue(IntPtr ptrToEventAttribute)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			this.m_Ptr = ptrToEventAttribute;
		}

		// Token: 0x06000004 RID: 4
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		// Token: 0x06000005 RID: 5 RVA: 0x000020BC File Offset: 0x000002BC
		internal static VFXEventAttribute Internal_InstanciateVFXEventAttribute(VisualEffectAsset vfxAsset)
		{
			VFXEventAttribute eventAttribute = new VFXEventAttribute(VFXEventAttribute.Internal_Create(), true, vfxAsset);
			eventAttribute.Internal_InitFromAsset(vfxAsset);
			return eventAttribute;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E4 File Offset: 0x000002E4
		internal void Internal_InitFromAsset(VisualEffectAsset vfxAsset)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.Internal_InitFromAsset_Injected(intPtr, Object.MarshalledUnityObject.Marshal<VisualEffectAsset>(vfxAsset));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210C File Offset: 0x0000030C
		private void Release()
		{
			bool flag = this.m_Owner && this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VFXEventAttribute.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_VfxAsset = null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215C File Offset: 0x0000035C
		~VFXEventAttribute()
		{
			this.Release();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000A RID: 10
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_InitFromAsset_Injected(IntPtr _unity_self, IntPtr vfxAsset);

		// Token: 0x04000006 RID: 6
		private IntPtr m_Ptr;

		// Token: 0x04000007 RID: 7
		private bool m_Owner;

		// Token: 0x04000008 RID: 8
		private VisualEffectAsset m_VfxAsset;

		// Token: 0x02000004 RID: 4
		internal static class BindingsMarshaller
		{
			// Token: 0x0600000C RID: 12 RVA: 0x0000219D File Offset: 0x0000039D
			public static IntPtr ConvertToNative(VFXEventAttribute eventAttibute)
			{
				return eventAttibute.m_Ptr;
			}
		}
	}
}
