using System;

namespace System.IO.Compression
{
	// Token: 0x02000005 RID: 5
	internal sealed class DeflateInput
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020FF File Offset: 0x000002FF
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002107 File Offset: 0x00000307
		internal byte[] Buffer { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002110 File Offset: 0x00000310
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		internal int Count { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002121 File Offset: 0x00000321
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002129 File Offset: 0x00000329
		internal int StartIndex { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002132 File Offset: 0x00000332
		internal void ConsumeBytes(int n)
		{
			this.StartIndex += n;
			this.Count -= n;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002150 File Offset: 0x00000350
		internal DeflateInput.InputState DumpState()
		{
			return new DeflateInput.InputState(this.Count, this.StartIndex);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002163 File Offset: 0x00000363
		internal void RestoreState(DeflateInput.InputState state)
		{
			this.Count = state._count;
			this.StartIndex = state._startIndex;
		}

		// Token: 0x02000006 RID: 6
		internal readonly struct InputState
		{
			// Token: 0x0600000E RID: 14 RVA: 0x00002185 File Offset: 0x00000385
			internal InputState(int count, int startIndex)
			{
				this._count = count;
				this._startIndex = startIndex;
			}

			// Token: 0x04000008 RID: 8
			internal readonly int _count;

			// Token: 0x04000009 RID: 9
			internal readonly int _startIndex;
		}
	}
}
