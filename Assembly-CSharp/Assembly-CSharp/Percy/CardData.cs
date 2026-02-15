using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Percy
{
	// Token: 0x020011DF RID: 4575
	public struct CardData
	{
		// Token: 0x060087D8 RID: 34776 RVA: 0x000F77E8 File Offset: 0x000F59E8
		public unsafe void ConvertLongToSetCode(long value)
		{
			this.Setcode.FixedElementField = (short)(value & 65535L);
			*((ref this.Setcode.FixedElementField) + 2) = (short)((value >> 16) & 65535L);
			*((ref this.Setcode.FixedElementField) + (IntPtr)2 * 2) = (short)((value >> 32) & 65535L);
			*((ref this.Setcode.FixedElementField) + (IntPtr)3 * 2) = (short)((value >> 48) & 65535L);
		}

		// Token: 0x0400C2DF RID: 49887
		public int Code;

		// Token: 0x0400C2E0 RID: 49888
		public int Alias;

		// Token: 0x0400C2E1 RID: 49889
		[FixedBuffer(typeof(short), 16)]
		public CardData.<Setcode>e__FixedBuffer Setcode;

		// Token: 0x0400C2E2 RID: 49890
		public int Type;

		// Token: 0x0400C2E3 RID: 49891
		public int Level;

		// Token: 0x0400C2E4 RID: 49892
		public int Attribute;

		// Token: 0x0400C2E5 RID: 49893
		public int Race;

		// Token: 0x0400C2E6 RID: 49894
		public int Attack;

		// Token: 0x0400C2E7 RID: 49895
		public int Defense;

		// Token: 0x0400C2E8 RID: 49896
		public int LScale;

		// Token: 0x0400C2E9 RID: 49897
		public int RScale;

		// Token: 0x0400C2EA RID: 49898
		public int LinkMarker;

		// Token: 0x020011E0 RID: 4576
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <Setcode>e__FixedBuffer
		{
			// Token: 0x0400C2EB RID: 49899
			public short FixedElementField;
		}
	}
}
