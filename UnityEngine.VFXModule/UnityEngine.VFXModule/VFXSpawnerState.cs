using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000B RID: 11
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/VFX/Public/VFXSpawnerState.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXSpawnerState : IDisposable
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000022E4 File Offset: 0x000004E4
		internal VFXSpawnerState(IntPtr ptr, bool owner)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022FC File Offset: 0x000004FC
		[RequiredByNativeCode]
		internal static VFXSpawnerState CreateSpawnerStateWrapper()
		{
			VFXSpawnerState spawnerState = new VFXSpawnerState(IntPtr.Zero, false);
			spawnerState.PrepareWrapper();
			return spawnerState;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002324 File Offset: 0x00000524
		private void PrepareWrapper()
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			bool flag = this.m_WrapEventAttribute != null;
			if (flag)
			{
				throw new Exception("VFXSpawnerState : Unexpected calling twice prepare wrapper");
			}
			this.m_WrapEventAttribute = VFXEventAttribute.CreateEventAttributeWrapper();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000236C File Offset: 0x0000056C
		[RequiredByNativeCode]
		internal void SetWrapValue(IntPtr ptrToSpawnerState, IntPtr ptrToEventAttribute)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			bool flag = this.m_WrapEventAttribute == null;
			if (flag)
			{
				throw new Exception("VFXSpawnerState : Missing PrepareWrapper");
			}
			this.m_Ptr = ptrToSpawnerState;
			this.m_WrapEventAttribute.SetWrapValue(ptrToEventAttribute);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023BC File Offset: 0x000005BC
		private void Release()
		{
			bool flag = this.m_Ptr != IntPtr.Zero && this.m_Owner;
			if (flag)
			{
				VFXSpawnerState.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_WrapEventAttribute = null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000240C File Offset: 0x0000060C
		~VFXSpawnerState()
		{
			this.Release();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000243C File Offset: 0x0000063C
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000022 RID: 34
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x04000019 RID: 25
		private IntPtr m_Ptr;

		// Token: 0x0400001A RID: 26
		private bool m_Owner;

		// Token: 0x0400001B RID: 27
		private VFXEventAttribute m_WrapEventAttribute;
	}
}
