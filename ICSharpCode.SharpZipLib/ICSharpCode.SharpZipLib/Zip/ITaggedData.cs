using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200001C RID: 28
	public interface ITaggedData
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C9 RID: 201
		ushort TagID { get; }

		// Token: 0x060000CA RID: 202
		void SetData(byte[] data, int offset, int count);

		// Token: 0x060000CB RID: 203
		byte[] GetData();
	}
}
