using System;

namespace System.IO.Compression
{
	// Token: 0x02000015 RID: 21
	internal sealed class Match
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004A1E File Offset: 0x00002C1E
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004A26 File Offset: 0x00002C26
		internal MatchState State { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004A2F File Offset: 0x00002C2F
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004A37 File Offset: 0x00002C37
		internal int Position { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004A40 File Offset: 0x00002C40
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004A48 File Offset: 0x00002C48
		internal int Length { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004A51 File Offset: 0x00002C51
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004A59 File Offset: 0x00002C59
		internal byte Symbol { get; set; }
	}
}
