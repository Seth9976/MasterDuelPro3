using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200003F RID: 63
	internal struct MonoAssemblyName
	{
		// Token: 0x0400011E RID: 286
		internal IntPtr name;

		// Token: 0x0400011F RID: 287
		internal IntPtr culture;

		// Token: 0x04000120 RID: 288
		internal IntPtr hash_value;

		// Token: 0x04000121 RID: 289
		internal IntPtr public_key;

		// Token: 0x04000122 RID: 290
		[FixedBuffer(typeof(byte), 17)]
		internal MonoAssemblyName.<public_key_token>e__FixedBuffer public_key_token;

		// Token: 0x04000123 RID: 291
		internal uint hash_alg;

		// Token: 0x04000124 RID: 292
		internal uint hash_len;

		// Token: 0x04000125 RID: 293
		internal uint flags;

		// Token: 0x04000126 RID: 294
		internal ushort major;

		// Token: 0x04000127 RID: 295
		internal ushort minor;

		// Token: 0x04000128 RID: 296
		internal ushort build;

		// Token: 0x04000129 RID: 297
		internal ushort revision;

		// Token: 0x0400012A RID: 298
		internal ushort arch;

		// Token: 0x02000040 RID: 64
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 17)]
		public struct <public_key_token>e__FixedBuffer
		{
			// Token: 0x0400012B RID: 299
			public byte FixedElementField;
		}
	}
}
