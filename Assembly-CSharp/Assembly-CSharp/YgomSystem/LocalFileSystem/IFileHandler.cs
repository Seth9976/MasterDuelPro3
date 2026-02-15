using System;
using System.IO;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000743 RID: 1859
	public interface IFileHandler
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060039A2 RID: 14754
		bool isValid { get; }

		// Token: 0x060039A3 RID: 14755
		bool Open(string nativePath, StreamOpenMode openMode);

		// Token: 0x060039A4 RID: 14756
		void Close();

		// Token: 0x060039A5 RID: 14757
		int Write(byte[] data, int offset, int count);

		// Token: 0x060039A6 RID: 14758
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x060039A7 RID: 14759
		long Seek(long offset, SeekOrigin origin);

		// Token: 0x060039A8 RID: 14760
		long GetSeek();

		// Token: 0x060039A9 RID: 14761
		long GetSize();

		// Token: 0x060039AA RID: 14762
		long SetSize(long size);

		// Token: 0x060039AB RID: 14763
		bool Flush();
	}
}
