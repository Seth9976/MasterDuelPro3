using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/Terrain/Public/SpeedTreeWind.h")]
	[UsedByNativeCode]
	internal struct SpeedTreeWindParamsBufferIterator
	{
		// Token: 0x04000023 RID: 35
		public IntPtr bufferPtr;

		// Token: 0x04000024 RID: 36
		[FixedBuffer(typeof(int), 16)]
		public SpeedTreeWindParamsBufferIterator.<uintParamOffsets>e__FixedBuffer uintParamOffsets;

		// Token: 0x04000025 RID: 37
		public int uintStride;

		// Token: 0x04000026 RID: 38
		public int elementOffset;

		// Token: 0x04000027 RID: 39
		public int elementsCount;

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <uintParamOffsets>e__FixedBuffer
		{
			// Token: 0x04000028 RID: 40
			public int FixedElementField;
		}
	}
}
