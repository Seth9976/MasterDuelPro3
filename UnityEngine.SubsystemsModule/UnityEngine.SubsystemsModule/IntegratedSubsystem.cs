using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[UsedByNativeCode]
	[NativeHeader("Modules/Subsystems/Subsystem.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class IntegratedSubsystem : ISubsystem
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal void SetHandle([Unmarshalled] IntegratedSubsystem subsystem)
		{
			IntPtr intPtr = IntegratedSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntegratedSubsystem.SetHandle_Injected(intPtr, subsystem);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002073 File Offset: 0x00000273
		public bool running
		{
			get
			{
				return this.valid && this.IsRunning();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002086 File Offset: 0x00000286
		internal bool valid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		internal bool IsRunning()
		{
			IntPtr intPtr = IntegratedSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return IntegratedSubsystem.IsRunning_Injected(intPtr);
		}

		// Token: 0x06000006 RID: 6
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHandle_Injected(IntPtr _unity_self, IntegratedSubsystem subsystem);

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRunning_Injected(IntPtr _unity_self);

		// Token: 0x04000001 RID: 1
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x04000002 RID: 2
		internal ISubsystemDescriptor m_SubsystemDescriptor;

		// Token: 0x02000003 RID: 3
		internal static class BindingsMarshaller
		{
			// Token: 0x06000008 RID: 8 RVA: 0x000020C3 File Offset: 0x000002C3
			public static IntPtr ConvertToNative(IntegratedSubsystem integratedSubsystem)
			{
				return integratedSubsystem.m_Ptr;
			}
		}
	}
}
