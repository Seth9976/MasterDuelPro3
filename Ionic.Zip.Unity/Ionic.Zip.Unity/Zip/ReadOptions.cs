using System;
using System.IO;
using System.Text;

namespace Ionic.Zip
{
	// Token: 0x02000039 RID: 57
	public class ReadOptions
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000F5B1 File Offset: 0x0000D7B1
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000F5B9 File Offset: 0x0000D7B9
		public EventHandler<ReadProgressEventArgs> ReadProgress { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000F5C2 File Offset: 0x0000D7C2
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000F5CA File Offset: 0x0000D7CA
		public TextWriter StatusMessageWriter { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000F5D3 File Offset: 0x0000D7D3
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000F5DB File Offset: 0x0000D7DB
		public Encoding Encoding { get; set; }
	}
}
