using System;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000021 RID: 33
	public interface ILZ4Encoder : IDisposable
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600008B RID: 139
		int BlockSize { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008C RID: 140
		int BytesReady { get; }

		// Token: 0x0600008D RID: 141
		unsafe int Topup(byte* source, int length);

		// Token: 0x0600008E RID: 142
		unsafe int Encode(byte* target, int length, bool allowCopy);
	}
}
