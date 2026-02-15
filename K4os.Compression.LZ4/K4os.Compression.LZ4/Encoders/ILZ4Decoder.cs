using System;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000020 RID: 32
	public interface ILZ4Decoder : IDisposable
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000086 RID: 134
		int BlockSize { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000087 RID: 135
		int BytesReady { get; }

		// Token: 0x06000088 RID: 136
		unsafe int Decode(byte* source, int length, int blockSize = 0);

		// Token: 0x06000089 RID: 137
		unsafe int Inject(byte* source, int length);

		// Token: 0x0600008A RID: 138
		unsafe void Drain(byte* target, int offset, int length);
	}
}
