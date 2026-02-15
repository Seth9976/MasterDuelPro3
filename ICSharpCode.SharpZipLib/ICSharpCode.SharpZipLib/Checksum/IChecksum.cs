using System;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x020000C6 RID: 198
	public interface IChecksum
	{
		// Token: 0x060005F1 RID: 1521
		void Reset();

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005F2 RID: 1522
		long Value { get; }

		// Token: 0x060005F3 RID: 1523
		void Update(int bval);

		// Token: 0x060005F4 RID: 1524
		void Update(byte[] buffer);

		// Token: 0x060005F5 RID: 1525
		void Update(ArraySegment<byte> segment);
	}
}
