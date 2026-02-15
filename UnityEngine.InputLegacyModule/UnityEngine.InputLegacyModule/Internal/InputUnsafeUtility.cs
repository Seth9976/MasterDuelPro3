using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Internal
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Runtime/Input/InputBindings.h")]
	internal static class InputUnsafeUtility
	{
		// Token: 0x06000040 RID: 64
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetKeyString__Unmanaged(byte* name, int nameLen);

		// Token: 0x06000041 RID: 65
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetKeyUpString__Unmanaged(byte* name, int nameLen);

		// Token: 0x06000042 RID: 66
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetKeyDownString__Unmanaged(byte* name, int nameLen);

		// Token: 0x06000043 RID: 67 RVA: 0x000029FC File Offset: 0x00000BFC
		[NativeThrows]
		internal unsafe static float GetAxis(string axisName)
		{
			float axis_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(axisName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = axisName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				axis_Injected = InputUnsafeUtility.GetAxis_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return axis_Injected;
		}

		// Token: 0x06000044 RID: 68
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern float GetAxis__Unmanaged(byte* axisName, int axisNameLen);

		// Token: 0x06000045 RID: 69 RVA: 0x00002A54 File Offset: 0x00000C54
		[NativeThrows]
		internal unsafe static float GetAxisRaw(string axisName)
		{
			float axisRaw_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(axisName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = axisName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				axisRaw_Injected = InputUnsafeUtility.GetAxisRaw_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return axisRaw_Injected;
		}

		// Token: 0x06000046 RID: 70
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern float GetAxisRaw__Unmanaged(byte* axisName, int axisNameLen);

		// Token: 0x06000047 RID: 71
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetButton__Unmanaged(byte* buttonName, int buttonNameLen);

		// Token: 0x06000048 RID: 72 RVA: 0x00002AAC File Offset: 0x00000CAC
		[NativeThrows]
		internal unsafe static bool GetButtonDown(string buttonName)
		{
			bool buttonDown_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(buttonName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = buttonName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				buttonDown_Injected = InputUnsafeUtility.GetButtonDown_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return buttonDown_Injected;
		}

		// Token: 0x06000049 RID: 73
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte GetButtonDown__Unmanaged(byte* buttonName, int buttonNameLen);

		// Token: 0x0600004A RID: 74
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetButtonUp__Unmanaged(byte* buttonName, int buttonNameLen);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAxis_Injected(ref ManagedSpanWrapper axisName);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAxisRaw_Injected(ref ManagedSpanWrapper axisName);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetButtonDown_Injected(ref ManagedSpanWrapper buttonName);
	}
}
