using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000093 RID: 147
	public class GZipOutputStream : DeflaterOutputStream
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0001833C File Offset: 0x0001653C
		public GZipOutputStream(Stream baseOutputStream)
			: this(baseOutputStream, 4096)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001834A File Offset: 0x0001654A
		public GZipOutputStream(Stream baseOutputStream, int size)
			: base(baseOutputStream, new Deflater(-1, true), size)
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00018366 File Offset: 0x00016566
		public void SetLevel(int level)
		{
			if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level", "Compression level must be 0-9");
			}
			this.deflater_.SetLevel(level);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000A938 File Offset: 0x00008B38
		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001838D File Offset: 0x0001658D
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00018395 File Offset: 0x00016595
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = GZipOutputStream.CleanFilename(value);
				if (string.IsNullOrEmpty(this.fileName))
				{
					this.flags &= ~GZipFlags.FNAME;
					return;
				}
				this.flags |= GZipFlags.FNAME;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000183D1 File Offset: 0x000165D1
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000183D9 File Offset: 0x000165D9
		public DateTime? ModifiedTime { get; set; }

		// Token: 0x060004E7 RID: 1255 RVA: 0x000183E4 File Offset: 0x000165E4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.WriteSyncOrAsync(buffer, offset, count, null).GetAwaiter().GetResult();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00018410 File Offset: 0x00016610
		private async Task WriteSyncOrAsync(byte[] buffer, int offset, int count, CancellationToken? ct)
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				if (ct != null)
				{
					await this.WriteHeaderAsync(ct.Value).ConfigureAwait(false);
				}
				else
				{
					this.WriteHeader();
				}
			}
			if (this.state_ != GZipOutputStream.OutputState.Footer)
			{
				throw new InvalidOperationException("Write not permitted in current state");
			}
			this.crc.Update(new ArraySegment<byte>(buffer, offset, count));
			if (ct != null)
			{
				await base.WriteAsync(buffer, offset, count, ct.Value).ConfigureAwait(false);
			}
			else
			{
				base.Write(buffer, offset, count);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00018474 File Offset: 0x00016674
		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken ct)
		{
			await this.WriteSyncOrAsync(buffer, offset, count, new CancellationToken?(ct)).ConfigureAwait(false);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000184D8 File Offset: 0x000166D8
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.Finish();
			}
			finally
			{
				if (this.state_ != GZipOutputStream.OutputState.Closed)
				{
					this.state_ = GZipOutputStream.OutputState.Closed;
					if (base.IsStreamOwner)
					{
						this.baseOutputStream_.Dispose();
					}
				}
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00018524 File Offset: 0x00016724
		public override void Flush()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			base.Flush();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001853C File Offset: 0x0001673C
		public override async Task FlushAsync(CancellationToken ct)
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				await this.WriteHeaderAsync(ct).ConfigureAwait(false);
			}
			await base.FlushAsync(ct).ConfigureAwait(false);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00018588 File Offset: 0x00016788
		public override void Finish()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			if (this.state_ == GZipOutputStream.OutputState.Footer)
			{
				this.state_ = GZipOutputStream.OutputState.Finished;
				base.Finish();
				byte[] footer = this.GetFooter();
				this.baseOutputStream_.Write(footer, 0, footer.Length);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000185D0 File Offset: 0x000167D0
		public override async Task FinishAsync(CancellationToken ct)
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				await this.WriteHeaderAsync(ct).ConfigureAwait(false);
			}
			if (this.state_ == GZipOutputStream.OutputState.Footer)
			{
				this.state_ = GZipOutputStream.OutputState.Finished;
				await base.FinishAsync(ct).ConfigureAwait(false);
				byte[] footer = this.GetFooter();
				await this.baseOutputStream_.WriteAsync(footer, 0, footer.Length, ct).ConfigureAwait(false);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001861C File Offset: 0x0001681C
		private byte[] GetFooter()
		{
			uint num = (uint)(this.deflater_.TotalIn & (long)((ulong)(-1)));
			uint num2 = (uint)(this.crc.Value & (long)((ulong)(-1)));
			return new byte[]
			{
				(byte)num2,
				(byte)(num2 >> 8),
				(byte)(num2 >> 16),
				(byte)(num2 >> 24),
				(byte)num,
				(byte)(num >> 8),
				(byte)(num >> 16),
				(byte)(num >> 24)
			};
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00018688 File Offset: 0x00016888
		private byte[] GetHeader()
		{
			DateTime? dateTime;
			int num = (int)((((this.ModifiedTime != null) ? dateTime.GetValueOrDefault().ToUniversalTime() : DateTime.UtcNow) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks / 10000000L);
			byte[] array = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 0, byte.MaxValue };
			array[3] = this.flags;
			array[4] = (byte)num;
			array[5] = (byte)(num >> 8);
			array[6] = (byte)(num >> 16);
			array[7] = (byte)(num >> 24);
			byte[] array2 = array;
			if (!this.flags.HasFlag(GZipFlags.FNAME))
			{
				return array2;
			}
			return array2.Concat(GZipConstants.Encoding.GetBytes(this.fileName)).Concat(new byte[1]).ToArray<byte>();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001875A File Offset: 0x0001695A
		private static string CleanFilename(string path)
		{
			return path.Substring(path.LastIndexOf('/') + 1);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001876C File Offset: 0x0001696C
		private void WriteHeader()
		{
			if (this.state_ != GZipOutputStream.OutputState.Header)
			{
				return;
			}
			this.state_ = GZipOutputStream.OutputState.Footer;
			byte[] header = this.GetHeader();
			this.baseOutputStream_.Write(header, 0, header.Length);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000187A0 File Offset: 0x000169A0
		private async Task WriteHeaderAsync(CancellationToken ct)
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.state_ = GZipOutputStream.OutputState.Footer;
				byte[] header = this.GetHeader();
				await this.baseOutputStream_.WriteAsync(header, 0, header.Length, ct).ConfigureAwait(false);
			}
		}

		// Token: 0x040003C6 RID: 966
		protected Crc32 crc = new Crc32();

		// Token: 0x040003C7 RID: 967
		private GZipOutputStream.OutputState state_;

		// Token: 0x040003C8 RID: 968
		private string fileName;

		// Token: 0x040003C9 RID: 969
		private GZipFlags flags;

		// Token: 0x02000094 RID: 148
		private enum OutputState
		{
			// Token: 0x040003CC RID: 972
			Header,
			// Token: 0x040003CD RID: 973
			Footer,
			// Token: 0x040003CE RID: 974
			Finished,
			// Token: 0x040003CF RID: 975
			Closed
		}
	}
}
