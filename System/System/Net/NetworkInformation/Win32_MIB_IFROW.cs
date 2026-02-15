using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000478 RID: 1144
	internal struct Win32_MIB_IFROW
	{
		// Token: 0x0400135C RID: 4956
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public char[] Name;

		// Token: 0x0400135D RID: 4957
		public int Index;

		// Token: 0x0400135E RID: 4958
		public NetworkInterfaceType Type;

		// Token: 0x0400135F RID: 4959
		public int Mtu;

		// Token: 0x04001360 RID: 4960
		public uint Speed;

		// Token: 0x04001361 RID: 4961
		public int PhysAddrLen;

		// Token: 0x04001362 RID: 4962
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysAddr;

		// Token: 0x04001363 RID: 4963
		public uint AdminStatus;

		// Token: 0x04001364 RID: 4964
		public uint OperStatus;

		// Token: 0x04001365 RID: 4965
		public uint LastChange;

		// Token: 0x04001366 RID: 4966
		public int InOctets;

		// Token: 0x04001367 RID: 4967
		public int InUcastPkts;

		// Token: 0x04001368 RID: 4968
		public int InNUcastPkts;

		// Token: 0x04001369 RID: 4969
		public int InDiscards;

		// Token: 0x0400136A RID: 4970
		public int InErrors;

		// Token: 0x0400136B RID: 4971
		public int InUnknownProtos;

		// Token: 0x0400136C RID: 4972
		public int OutOctets;

		// Token: 0x0400136D RID: 4973
		public int OutUcastPkts;

		// Token: 0x0400136E RID: 4974
		public int OutNUcastPkts;

		// Token: 0x0400136F RID: 4975
		public int OutDiscards;

		// Token: 0x04001370 RID: 4976
		public int OutErrors;

		// Token: 0x04001371 RID: 4977
		public int OutQLen;

		// Token: 0x04001372 RID: 4978
		public int DescrLen;

		// Token: 0x04001373 RID: 4979
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] Descr;
	}
}
