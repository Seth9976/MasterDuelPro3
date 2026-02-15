using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000082 RID: 130
	public class TarOutputStream : Stream
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0001629E File Offset: 0x0001449E
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarOutputStream(Stream outputStream)
			: this(outputStream, 20)
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000162A9 File Offset: 0x000144A9
		public TarOutputStream(Stream outputStream, Encoding nameEncoding)
			: this(outputStream, 20, nameEncoding)
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000162B8 File Offset: 0x000144B8
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarOutputStream(Stream outputStream, int blockFactor)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			this.outputStream = outputStream;
			this.buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
			this.assemblyBuffer = ArrayPool<byte>.Shared.Rent(512);
			this.blockBuffer = ArrayPool<byte>.Shared.Rent(512);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016318 File Offset: 0x00014518
		public TarOutputStream(Stream outputStream, int blockFactor, Encoding nameEncoding)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			this.outputStream = outputStream;
			this.buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
			this.assemblyBuffer = ArrayPool<byte>.Shared.Rent(512);
			this.blockBuffer = ArrayPool<byte>.Shared.Rent(512);
			this.nameEncoding = nameEncoding;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001637E File Offset: 0x0001457E
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0001638B File Offset: 0x0001458B
		public bool IsStreamOwner
		{
			get
			{
				return this.buffer.IsStreamOwner;
			}
			set
			{
				this.buffer.IsStreamOwner = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00016399 File Offset: 0x00014599
		public override bool CanRead
		{
			get
			{
				return this.outputStream.CanRead;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000163A6 File Offset: 0x000145A6
		public override bool CanSeek
		{
			get
			{
				return this.outputStream.CanSeek;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000163B3 File Offset: 0x000145B3
		public override bool CanWrite
		{
			get
			{
				return this.outputStream.CanWrite;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000163C0 File Offset: 0x000145C0
		public override long Length
		{
			get
			{
				return this.outputStream.Length;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000163CD File Offset: 0x000145CD
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000163DA File Offset: 0x000145DA
		public override long Position
		{
			get
			{
				return this.outputStream.Position;
			}
			set
			{
				this.outputStream.Position = value;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000163E8 File Offset: 0x000145E8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.outputStream.Seek(offset, origin);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000163F7 File Offset: 0x000145F7
		public override void SetLength(long value)
		{
			this.outputStream.SetLength(value);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00016405 File Offset: 0x00014605
		public override int ReadByte()
		{
			return this.outputStream.ReadByte();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00016412 File Offset: 0x00014612
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.outputStream.Read(buffer, offset, count);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00016424 File Offset: 0x00014624
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return await this.outputStream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00016488 File Offset: 0x00014688
		public override void Flush()
		{
			this.outputStream.Flush();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00016498 File Offset: 0x00014698
		public override async Task FlushAsync(CancellationToken cancellationToken)
		{
			await this.outputStream.FlushAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000164E4 File Offset: 0x000146E4
		public void Finish()
		{
			this.FinishAsync(CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001650A File Offset: 0x0001470A
		public Task FinishAsync(CancellationToken cancellationToken)
		{
			return this.FinishAsync(cancellationToken, true);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00016514 File Offset: 0x00014714
		private async Task FinishAsync(CancellationToken cancellationToken, bool isAsync)
		{
			if (this.IsEntryOpen)
			{
				await this.CloseEntryAsync(cancellationToken, isAsync).ConfigureAwait(false);
			}
			await this.WriteEofBlockAsync(cancellationToken, isAsync).ConfigureAwait(false);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00016568 File Offset: 0x00014768
		protected override void Dispose(bool disposing)
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				this.Finish();
				this.buffer.Close();
				ArrayPool<byte>.Shared.Return(this.assemblyBuffer, false);
				ArrayPool<byte>.Shared.Return(this.blockBuffer, false);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000165B7 File Offset: 0x000147B7
		public int RecordSize
		{
			get
			{
				return this.buffer.RecordSize;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000165B7 File Offset: 0x000147B7
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.buffer.RecordSize;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000165C4 File Offset: 0x000147C4
		private bool IsEntryOpen
		{
			get
			{
				return this.currBytes < this.currSize;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000165D4 File Offset: 0x000147D4
		public Task PutNextEntryAsync(TarEntry entry, CancellationToken cancellationToken)
		{
			return this.PutNextEntryAsync(entry, cancellationToken, true);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000165E0 File Offset: 0x000147E0
		public void PutNextEntry(TarEntry entry)
		{
			this.PutNextEntryAsync(entry, CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016608 File Offset: 0x00014808
		private async Task PutNextEntryAsync(TarEntry entry, CancellationToken cancellationToken, bool isAsync)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			int namelen = ((this.nameEncoding != null) ? this.nameEncoding.GetByteCount(entry.TarHeader.Name) : entry.TarHeader.Name.Length);
			if (namelen > 100)
			{
				TarHeader tarHeader = new TarHeader();
				tarHeader.TypeFlag = 76;
				tarHeader.Name += "././@LongLink";
				tarHeader.Mode = 420;
				tarHeader.UserId = entry.UserId;
				tarHeader.GroupId = entry.GroupId;
				tarHeader.GroupName = entry.GroupName;
				tarHeader.UserName = entry.UserName;
				tarHeader.LinkName = "";
				tarHeader.Size = (long)(namelen + 1);
				tarHeader.WriteHeader(this.blockBuffer, this.nameEncoding);
				await this.buffer.WriteBlockAsync(this.blockBuffer, 0, cancellationToken, isAsync).ConfigureAwait(false);
				int nameCharIndex = 0;
				while (nameCharIndex < namelen + 1)
				{
					Array.Clear(this.blockBuffer, 0, this.blockBuffer.Length);
					TarHeader.GetAsciiBytes(entry.TarHeader.Name, nameCharIndex, this.blockBuffer, 0, 512, this.nameEncoding);
					nameCharIndex += 512;
					await this.buffer.WriteBlockAsync(this.blockBuffer, 0, cancellationToken, isAsync).ConfigureAwait(false);
				}
			}
			entry.WriteEntryHeader(this.blockBuffer, this.nameEncoding);
			await this.buffer.WriteBlockAsync(this.blockBuffer, 0, cancellationToken, isAsync).ConfigureAwait(false);
			this.currBytes = 0L;
			this.currSize = (entry.IsDirectory ? 0L : entry.Size);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00016663 File Offset: 0x00014863
		public Task CloseEntryAsync(CancellationToken cancellationToken)
		{
			return this.CloseEntryAsync(cancellationToken, true);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00016670 File Offset: 0x00014870
		public void CloseEntry()
		{
			this.CloseEntryAsync(CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016698 File Offset: 0x00014898
		private async Task CloseEntryAsync(CancellationToken cancellationToken, bool isAsync)
		{
			if (this.assemblyBufferLength > 0)
			{
				Array.Clear(this.assemblyBuffer, this.assemblyBufferLength, this.assemblyBuffer.Length - this.assemblyBufferLength);
				await this.buffer.WriteBlockAsync(this.assemblyBuffer, 0, cancellationToken, isAsync).ConfigureAwait(false);
				this.currBytes += (long)this.assemblyBufferLength;
				this.assemblyBufferLength = 0;
			}
			if (this.currBytes < this.currSize)
			{
				throw new TarException(string.Format("Entry closed at '{0}' before the '{1}' bytes specified in the header were written", this.currBytes, this.currSize));
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000166EC File Offset: 0x000148EC
		public override void WriteByte(byte value)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(1);
			array[0] = value;
			this.Write(array, 0, 1);
			ArrayPool<byte>.Shared.Return(array, false);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00016720 File Offset: 0x00014920
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.WriteAsync(buffer, offset, count, CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00016749 File Offset: 0x00014949
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.WriteAsync(buffer, offset, count, cancellationToken, true);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016758 File Offset: 0x00014958
		private async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool isAsync)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset and count combination is invalid");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			if (this.currBytes + (long)count > this.currSize)
			{
				string text = string.Format("request to write '{0}' bytes exceeds size in header of '{1}' bytes", count, this.currSize);
				throw new ArgumentOutOfRangeException("count", text);
			}
			if (this.assemblyBufferLength > 0)
			{
				if (this.assemblyBufferLength + count >= this.blockBuffer.Length)
				{
					int aLen = this.blockBuffer.Length - this.assemblyBufferLength;
					Array.Copy(this.assemblyBuffer, 0, this.blockBuffer, 0, this.assemblyBufferLength);
					Array.Copy(buffer, offset, this.blockBuffer, this.assemblyBufferLength, aLen);
					await this.buffer.WriteBlockAsync(this.blockBuffer, 0, cancellationToken, isAsync).ConfigureAwait(false);
					this.currBytes += (long)this.blockBuffer.Length;
					offset += aLen;
					count -= aLen;
					this.assemblyBufferLength = 0;
				}
				else
				{
					Array.Copy(buffer, offset, this.assemblyBuffer, this.assemblyBufferLength, count);
					offset += count;
					this.assemblyBufferLength += count;
					count -= count;
				}
			}
			while (count > 0)
			{
				if (count < this.blockBuffer.Length)
				{
					Array.Copy(buffer, offset, this.assemblyBuffer, this.assemblyBufferLength, count);
					this.assemblyBufferLength += count;
					break;
				}
				await this.buffer.WriteBlockAsync(buffer, offset, cancellationToken, isAsync).ConfigureAwait(false);
				int num = this.blockBuffer.Length;
				this.currBytes += (long)num;
				count -= num;
				offset += num;
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000167C8 File Offset: 0x000149C8
		private async Task WriteEofBlockAsync(CancellationToken cancellationToken, bool isAsync)
		{
			Array.Clear(this.blockBuffer, 0, this.blockBuffer.Length);
			await this.buffer.WriteBlockAsync(this.blockBuffer, 0, cancellationToken, isAsync).ConfigureAwait(false);
			await this.buffer.WriteBlockAsync(this.blockBuffer, 0, cancellationToken, isAsync).ConfigureAwait(false);
		}

		// Token: 0x0400035B RID: 859
		private long currBytes;

		// Token: 0x0400035C RID: 860
		private int assemblyBufferLength;

		// Token: 0x0400035D RID: 861
		private bool isClosed;

		// Token: 0x0400035E RID: 862
		protected long currSize;

		// Token: 0x0400035F RID: 863
		protected byte[] blockBuffer;

		// Token: 0x04000360 RID: 864
		protected byte[] assemblyBuffer;

		// Token: 0x04000361 RID: 865
		protected TarBuffer buffer;

		// Token: 0x04000362 RID: 866
		protected Stream outputStream;

		// Token: 0x04000363 RID: 867
		protected Encoding nameEncoding;
	}
}
