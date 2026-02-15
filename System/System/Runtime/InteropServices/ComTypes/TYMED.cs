using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the TYMED structure.</summary>
	// Token: 0x0200011B RID: 283
	[Flags]
	public enum TYMED
	{
		/// <summary>The storage medium is a global memory handle (HGLOBAL). Allocate the global handle with the GMEM_SHARE flag. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use GlobalFree to release the memory.</summary>
		// Token: 0x040004AB RID: 1195
		TYMED_HGLOBAL = 1,
		/// <summary>The storage medium is a disk file identified by a path. If the STGMEDIUM<see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use OpenFile to delete the file.</summary>
		// Token: 0x040004AC RID: 1196
		TYMED_FILE = 2,
		/// <summary>The storage medium is a stream object identified by an IStream pointer. Use ISequentialStream::Read to read the data. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is not null, the destination process should use IStream::Release to release the stream component.</summary>
		// Token: 0x040004AD RID: 1197
		TYMED_ISTREAM = 4,
		/// <summary>The storage medium is a storage component identified by an IStorage pointer. The data is in the streams and storages contained by this IStorage instance. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is not null, the destination process should use IStorage::Release to release the storage component.</summary>
		// Token: 0x040004AE RID: 1198
		TYMED_ISTORAGE = 8,
		/// <summary>The storage medium is a Graphics Device Interface (GDI) component (HBITMAP). If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use DeleteObject to delete the bitmap.</summary>
		// Token: 0x040004AF RID: 1199
		TYMED_GDI = 16,
		/// <summary>The storage medium is a metafile (HMETAFILE). Use the Windows or WIN32 functions to access the metafile's data. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use DeleteMetaFile to delete the bitmap.</summary>
		// Token: 0x040004B0 RID: 1200
		TYMED_MFPICT = 32,
		/// <summary>The storage medium is an enhanced metafile. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use DeleteEnhMetaFile to delete the bitmap.</summary>
		// Token: 0x040004B1 RID: 1201
		TYMED_ENHMF = 64,
		/// <summary>No data is being passed.</summary>
		// Token: 0x040004B2 RID: 1202
		TYMED_NULL = 0
	}
}
