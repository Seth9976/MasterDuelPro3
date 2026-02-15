using System;

namespace Mono.Btls
{
	// Token: 0x0200009A RID: 154
	internal interface IMonoBtlsBioMono
	{
		// Token: 0x06000268 RID: 616
		int Read(byte[] buffer, int offset, int size, out bool wantMore);

		// Token: 0x06000269 RID: 617
		bool Write(byte[] buffer, int offset, int size);

		// Token: 0x0600026A RID: 618
		void Flush();

		// Token: 0x0600026B RID: 619
		void Close();
	}
}
