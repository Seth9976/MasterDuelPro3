using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x02000065 RID: 101
	[GenerateTestsForBurstCompatibility]
	[Serializable]
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal struct FixedBytes32Align8
	{
		// Token: 0x040000E5 RID: 229
		[SerializeField]
		[FieldOffset(0)]
		internal FixedBytes16Align8 offset0000;

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		[FieldOffset(16)]
		internal FixedBytes16Align8 offset0016;
	}
}
