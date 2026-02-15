using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200016B RID: 363
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	[NativeClass("RenderingLayerMask", "struct RenderingLayerMask;")]
	[NativeHeader("Runtime/Graphics/RenderingLayerMask.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct RenderingLayerMask
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x00020428 File Offset: 0x0001E628
		public static implicit operator uint(RenderingLayerMask mask)
		{
			return mask.m_Bits;
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00020440 File Offset: 0x0001E640
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[NativeMethod("StringToRenderingLayer")]
		public unsafe static int NameToRenderingLayer(string layerName)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(layerName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = layerName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = RenderingLayerMask.NameToRenderingLayer_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06000F4D RID: 3917
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetDefinedRenderingLayersCombinedMaskValue();

		// Token: 0x06000F4E RID: 3918
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetDefinedRenderingLayerNames();

		// Token: 0x06000F4F RID: 3919
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRenderingLayerCount();

		// Token: 0x06000F50 RID: 3920 RVA: 0x00020498 File Offset: 0x0001E698
		// Note: this type is marked as 'beforefieldinit'.
		static RenderingLayerMask()
		{
			RenderingLayerMask.<defaultRenderingLayerMask>k__BackingField = new RenderingLayerMask
			{
				m_Bits = 1U
			};
		}

		// Token: 0x06000F51 RID: 3921
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int NameToRenderingLayer_Injected(ref ManagedSpanWrapper layerName);

		// Token: 0x0400060B RID: 1547
		[NativeName("m_Bits")]
		private uint m_Bits;

		// Token: 0x0400060D RID: 1549
		internal const int maxRenderingLayerSize = 32;
	}
}
