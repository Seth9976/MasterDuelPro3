using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents a generalized Clipboard format. </summary>
	// Token: 0x02000115 RID: 277
	public struct FORMATETC
	{
		/// <summary>Specifies the particular clipboard format of interest.</summary>
		// Token: 0x040004A2 RID: 1186
		[MarshalAs(UnmanagedType.U2)]
		public short cfFormat;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.DVASPECT" /> enumeration constants that indicates how much detail should be contained in the rendering.</summary>
		// Token: 0x040004A3 RID: 1187
		[MarshalAs(UnmanagedType.U4)]
		public DVASPECT dwAspect;

		/// <summary>Specifies part of the aspect when the data must be split across page boundaries. </summary>
		// Token: 0x040004A4 RID: 1188
		public int lindex;

		/// <summary>Specifies a pointer to a DVTARGETDEVICE structure containing information about the target device that the data is being composed for. </summary>
		// Token: 0x040004A5 RID: 1189
		public IntPtr ptd;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> enumeration constants, which indicates the type of storage medium used to transfer the object's data. </summary>
		// Token: 0x040004A6 RID: 1190
		[MarshalAs(UnmanagedType.U4)]
		public TYMED tymed;
	}
}
