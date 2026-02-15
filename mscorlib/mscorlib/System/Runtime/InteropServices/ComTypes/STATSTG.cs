using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains statistical information about an open storage, stream, or byte-array object.</summary>
	// Token: 0x0200056B RID: 1387
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Represents a pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x040015A9 RID: 5545
		public string pwcsName;

		/// <summary>Indicates the type of storage object, which is one of the values from the STGTY enumeration.</summary>
		// Token: 0x040015AA RID: 5546
		public int type;

		/// <summary>Specifies the size, in bytes, of the stream or byte array.</summary>
		// Token: 0x040015AB RID: 5547
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x040015AC RID: 5548
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x040015AD RID: 5549
		public FILETIME ctime;

		/// <summary>Specifies the last access time for this storage, stream, or byte array. </summary>
		// Token: 0x040015AE RID: 5550
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x040015AF RID: 5551
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x040015B0 RID: 5552
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x040015B1 RID: 5553
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the IStorage::SetStateBits method).</summary>
		// Token: 0x040015B2 RID: 5554
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x040015B3 RID: 5555
		public int reserved;
	}
}
