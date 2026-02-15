using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200010A RID: 266
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorContext
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x00026A54 File Offset: 0x00024C54
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00026A79 File Offset: 0x00024C79
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00026A81 File Offset: 0x00024C81
		internal bool Traced { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00026A8A File Offset: 0x00024C8A
		public Exception Error { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00026A92 File Offset: 0x00024C92
		[Nullable(2)]
		public object OriginalObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00026A9A File Offset: 0x00024C9A
		[Nullable(2)]
		public object Member
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00026AA2 File Offset: 0x00024CA2
		public string Path { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00026AAA File Offset: 0x00024CAA
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00026AB2 File Offset: 0x00024CB2
		public bool Handled { get; set; }
	}
}
