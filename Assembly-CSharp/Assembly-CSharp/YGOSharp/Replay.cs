using System;
using System.IO;
using SevenZip.Compression.LZMA;

namespace YGOSharp
{
	// Token: 0x020001C0 RID: 448
	public class Replay
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x000251AC File Offset: 0x000233AC
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x000251B4 File Offset: 0x000233B4
		public bool Disabled { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x000251BD File Offset: 0x000233BD
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x000251C5 File Offset: 0x000233C5
		public BinaryWriter Writer { get; private set; }

		// Token: 0x060007B0 RID: 1968 RVA: 0x000251D0 File Offset: 0x000233D0
		public Replay(uint seed, bool tag)
		{
			this.Header.Id = 829452921U;
			this.Header.Version = Program.ClientVersion;
			this.Header.Flag = (tag ? 2U : 0U);
			this.Header.Seed = seed;
			this._stream = new MemoryStream();
			this.Writer = new BinaryWriter(this._stream);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002523D File Offset: 0x0002343D
		public void Check()
		{
			if (this._stream.Position >= 131072L)
			{
				this.Writer.Close();
				this._stream.Dispose();
				this.Disabled = true;
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00025270 File Offset: 0x00023470
		public void End()
		{
			if (this.Disabled)
			{
				return;
			}
			this.Disabled = true;
			byte[] raw = this._stream.ToArray();
			this.Header.DataSize = (uint)raw.Length;
			this.Header.Flag = this.Header.Flag | 1U;
			this.Header.Props = new byte[8];
			SevenZip.Compression.LZMA.Encoder lzma = new SevenZip.Compression.LZMA.Encoder();
			using (MemoryStream props = new MemoryStream(this.Header.Props))
			{
				lzma.WriteCoderProperties(props);
			}
			MemoryStream compressed = new MemoryStream();
			lzma.Code(new MemoryStream(raw), compressed, (long)raw.Length, -1L, null);
			raw = compressed.ToArray();
			MemoryStream ms = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(ms);
			binaryWriter.Write(this.Header.Id);
			binaryWriter.Write(this.Header.Version);
			binaryWriter.Write(this.Header.Flag);
			binaryWriter.Write(this.Header.Seed);
			binaryWriter.Write(this.Header.DataSize);
			binaryWriter.Write(this.Header.Hash);
			binaryWriter.Write(this.Header.Props);
			binaryWriter.Write(raw);
			this._data = ms.ToArray();
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000253BC File Offset: 0x000235BC
		public byte[] GetContent()
		{
			return this._data;
		}

		// Token: 0x04000B92 RID: 2962
		public const uint FlagCompressed = 1U;

		// Token: 0x04000B93 RID: 2963
		public const uint FlagTag = 2U;

		// Token: 0x04000B94 RID: 2964
		public const int MaxReplaySize = 131072;

		// Token: 0x04000B96 RID: 2966
		public Replay.ReplayHeader Header;

		// Token: 0x04000B98 RID: 2968
		private MemoryStream _stream;

		// Token: 0x04000B99 RID: 2969
		private byte[] _data;

		// Token: 0x020001C1 RID: 449
		public struct ReplayHeader
		{
			// Token: 0x04000B9A RID: 2970
			public uint Id;

			// Token: 0x04000B9B RID: 2971
			public uint Version;

			// Token: 0x04000B9C RID: 2972
			public uint Flag;

			// Token: 0x04000B9D RID: 2973
			public uint Seed;

			// Token: 0x04000B9E RID: 2974
			public uint DataSize;

			// Token: 0x04000B9F RID: 2975
			public uint Hash;

			// Token: 0x04000BA0 RID: 2976
			public byte[] Props;
		}
	}
}
