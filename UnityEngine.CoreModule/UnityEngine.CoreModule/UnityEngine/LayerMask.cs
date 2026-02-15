using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A8 RID: 424
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	[NativeHeader("Runtime/BaseClasses/BitField.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("BitField", "struct BitField;")]
	public struct LayerMask
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x000238EC File Offset: 0x00021AEC
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00023904 File Offset: 0x00021B04
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask mask;
			mask.m_Mask = intVal;
			return mask;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00023920 File Offset: 0x00021B20
		public int value
		{
			get
			{
				return this.m_Mask;
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00023938 File Offset: 0x00021B38
		[NativeMethod("StringToLayer")]
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		public unsafe static int NameToLayer(string layerName)
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
				num = LayerMask.NameToLayer_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x060010C2 RID: 4290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int NameToLayer_Injected(ref ManagedSpanWrapper layerName);

		// Token: 0x04000672 RID: 1650
		[NativeName("m_Bits")]
		private int m_Mask;
	}
}
