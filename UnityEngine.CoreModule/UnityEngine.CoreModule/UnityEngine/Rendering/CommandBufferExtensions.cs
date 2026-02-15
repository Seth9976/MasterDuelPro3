using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000367 RID: 871
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Graphics/RenderingCommandBufferExtensions.bindings.h")]
	public static class CommandBufferExtensions
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x00034564 File Offset: 0x00032764
		[FreeFunction("RenderingCommandBufferExtensions_Bindings::Internal_SwitchIntoFastMemory")]
		private static void Internal_SwitchIntoFastMemory([NotNull] CommandBuffer cmd, ref RenderTargetIdentifier rt, FastMemoryFlags fastMemoryFlags, float residency, bool copyContents)
		{
			if (cmd == null)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(cmd);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			CommandBufferExtensions.Internal_SwitchIntoFastMemory_Injected(intPtr, ref rt, fastMemoryFlags, residency, copyContents);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000345A0 File Offset: 0x000327A0
		[FreeFunction("RenderingCommandBufferExtensions_Bindings::Internal_SwitchOutOfFastMemory")]
		private static void Internal_SwitchOutOfFastMemory([NotNull] CommandBuffer cmd, ref RenderTargetIdentifier rt, bool copyContents)
		{
			if (cmd == null)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(cmd);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			CommandBufferExtensions.Internal_SwitchOutOfFastMemory_Injected(intPtr, ref rt, copyContents);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000345D8 File Offset: 0x000327D8
		[NativeConditional("UNITY_XBOXONE || UNITY_GAMECORE_XBOXONE")]
		public static void SwitchIntoFastMemory(this CommandBuffer cmd, RenderTargetIdentifier rid, FastMemoryFlags fastMemoryFlags, float residency, bool copyContents)
		{
			CommandBufferExtensions.Internal_SwitchIntoFastMemory(cmd, ref rid, fastMemoryFlags, residency, copyContents);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000345E8 File Offset: 0x000327E8
		[NativeConditional("UNITY_XBOXONE || UNITY_GAMECORE_XBOXONE")]
		public static void SwitchOutOfFastMemory(this CommandBuffer cmd, RenderTargetIdentifier rid, bool copyContents)
		{
			CommandBufferExtensions.Internal_SwitchOutOfFastMemory(cmd, ref rid, copyContents);
		}

		// Token: 0x060018A4 RID: 6308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SwitchIntoFastMemory_Injected(IntPtr cmd, ref RenderTargetIdentifier rt, FastMemoryFlags fastMemoryFlags, float residency, bool copyContents);

		// Token: 0x060018A5 RID: 6309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SwitchOutOfFastMemory_Injected(IntPtr cmd, ref RenderTargetIdentifier rt, bool copyContents);
	}
}
