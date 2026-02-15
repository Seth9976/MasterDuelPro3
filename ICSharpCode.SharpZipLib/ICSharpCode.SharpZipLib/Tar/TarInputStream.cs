using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000079 RID: 121
	public class TarInputStream : Stream
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0001474B File Offset: 0x0001294B
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarInputStream(Stream inputStream)
			: this(inputStream, 20, null)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00014757 File Offset: 0x00012957
		public TarInputStream(Stream inputStream, Encoding nameEncoding)
			: this(inputStream, 20, nameEncoding)
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014763 File Offset: 0x00012963
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarInputStream(Stream inputStream, int blockFactor)
		{
			this.inputStream = inputStream;
			this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
			this.encoding = null;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00014786 File Offset: 0x00012986
		public TarInputStream(Stream inputStream, int blockFactor, Encoding nameEncoding)
		{
			this.inputStream = inputStream;
			this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
			this.encoding = nameEncoding;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000147A9 File Offset: 0x000129A9
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x000147B6 File Offset: 0x000129B6
		public bool IsStreamOwner
		{
			get
			{
				return this.tarBuffer.IsStreamOwner;
			}
			set
			{
				this.tarBuffer.IsStreamOwner = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000147C4 File Offset: 0x000129C4
		public override bool CanRead
		{
			get
			{
				return this.inputStream.CanRead;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000147D1 File Offset: 0x000129D1
		public override long Length
		{
			get
			{
				return this.inputStream.Length;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000147DE File Offset: 0x000129DE
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000147EB File Offset: 0x000129EB
		public override long Position
		{
			get
			{
				return this.inputStream.Position;
			}
			set
			{
				throw new NotSupportedException("TarInputStream Seek not supported");
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000147F7 File Offset: 0x000129F7
		public override void Flush()
		{
			this.inputStream.Flush();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00014804 File Offset: 0x00012A04
		public override async Task FlushAsync(CancellationToken cancellationToken)
		{
			await this.inputStream.FlushAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000147EB File Offset: 0x000129EB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("TarInputStream Seek not supported");
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001484F File Offset: 0x00012A4F
		public override void SetLength(long value)
		{
			throw new NotSupportedException("TarInputStream SetLength not supported");
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001485B File Offset: 0x00012A5B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("TarInputStream Write not supported");
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00014867 File Offset: 0x00012A67
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("TarInputStream WriteByte not supported");
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00014874 File Offset: 0x00012A74
		public override int ReadByte()
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(1);
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			int num = (int)array[0];
			ArrayPool<byte>.Shared.Return(array, false);
			return num;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000148AC File Offset: 0x00012AAC
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.ReadAsync(buffer.AsMemory<byte>().Slice(offset, count), cancellationToken, true).AsTask();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000148DC File Offset: 0x00012ADC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.ReadAsync(buffer.AsMemory<byte>().Slice(offset, count), CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014924 File Offset: 0x00012B24
		private async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken ct, bool isAsync)
		{
			int offset = 0;
			int totalRead = 0;
			int num;
			if (this.entryOffset >= this.entrySize)
			{
				num = 0;
			}
			else
			{
				long numToRead = (long)buffer.Length;
				if (numToRead + this.entryOffset > this.entrySize)
				{
					numToRead = this.entrySize - this.entryOffset;
				}
				if (this.readBuffer != null)
				{
					int num2 = ((numToRead > (long)this.readBuffer.Memory.Length) ? this.readBuffer.Memory.Length : ((int)numToRead));
					this.readBuffer.Memory.Slice(0, num2).CopyTo(buffer.Slice(offset, num2));
					if (num2 >= this.readBuffer.Memory.Length)
					{
						this.readBuffer.Dispose();
						this.readBuffer = null;
					}
					else
					{
						int num3 = this.readBuffer.Memory.Length - num2;
						IMemoryOwner<byte> memoryOwner = ExactMemoryPool<byte>.Shared.Rent(num3);
						this.readBuffer.Memory.Slice(num2, num3).CopyTo(memoryOwner.Memory);
						this.readBuffer.Dispose();
						this.readBuffer = memoryOwner;
					}
					totalRead += num2;
					numToRead -= (long)num2;
					offset += num2;
				}
				int recLen = 512;
				byte[] recBuf = ArrayPool<byte>.Shared.Rent(recLen);
				while (numToRead > 0L)
				{
					await this.tarBuffer.ReadBlockIntAsync(recBuf, ct, isAsync).ConfigureAwait(false);
					int num4 = (int)numToRead;
					if (recLen > num4)
					{
						recBuf.AsSpan<byte>().Slice(0, num4).CopyTo(buffer.Slice(offset, num4).Span);
						IMemoryOwner<byte> memoryOwner2 = this.readBuffer;
						if (memoryOwner2 != null)
						{
							memoryOwner2.Dispose();
						}
						this.readBuffer = ExactMemoryPool<byte>.Shared.Rent(recLen - num4);
						recBuf.AsSpan<byte>().Slice(num4, recLen - num4).CopyTo(this.readBuffer.Memory.Span);
					}
					else
					{
						num4 = recLen;
						recBuf.AsSpan<byte>().CopyTo(buffer.Slice(offset, recLen).Span);
					}
					totalRead += num4;
					numToRead -= (long)num4;
					offset += num4;
				}
				ArrayPool<byte>.Shared.Return(recBuf, false);
				this.entryOffset += (long)totalRead;
				num = totalRead;
			}
			return num;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001497F File Offset: 0x00012B7F
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.tarBuffer.Close();
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001498F File Offset: 0x00012B8F
		public void SetEntryFactory(TarInputStream.IEntryFactory factory)
		{
			this.entryFactory = factory;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00014998 File Offset: 0x00012B98
		public int RecordSize
		{
			get
			{
				return this.tarBuffer.RecordSize;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00014998 File Offset: 0x00012B98
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.tarBuffer.RecordSize;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000149A5 File Offset: 0x00012BA5
		public long Available
		{
			get
			{
				return this.entrySize - this.entryOffset;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000149B4 File Offset: 0x00012BB4
		private Task SkipAsync(long skipCount, CancellationToken ct)
		{
			return this.SkipAsync(skipCount, ct, true).AsTask();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000149D4 File Offset: 0x00012BD4
		private void Skip(long skipCount)
		{
			this.SkipAsync(skipCount, CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00014A00 File Offset: 0x00012C00
		private async ValueTask SkipAsync(long skipCount, CancellationToken ct, bool isAsync)
		{
			int length = 8192;
			using (IMemoryOwner<byte> skipBuf = ExactMemoryPool<byte>.Shared.Rent(length))
			{
				int num3;
				for (long num = skipCount; num > 0L; num -= (long)num3)
				{
					int num2 = ((num > (long)length) ? length : ((int)num));
					num3 = await this.ReadAsync(skipBuf.Memory.Slice(0, num2), ct, isAsync).ConfigureAwait(false);
					if (num3 == -1)
					{
						break;
					}
				}
			}
			IMemoryOwner<byte> skipBuf = null;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000840F File Offset: 0x0000660F
		public bool IsMarkSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00008444 File Offset: 0x00006644
		public void Mark(int markLimit)
		{
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00008444 File Offset: 0x00006644
		public void Reset()
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014A5C File Offset: 0x00012C5C
		public Task<TarEntry> GetNextEntryAsync(CancellationToken ct)
		{
			return this.GetNextEntryAsync(ct, true).AsTask();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014A7C File Offset: 0x00012C7C
		public TarEntry GetNextEntry()
		{
			return this.GetNextEntryAsync(CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00014AA8 File Offset: 0x00012CA8
		private async ValueTask<TarEntry> GetNextEntryAsync(CancellationToken ct, bool isAsync)
		{
			TarEntry tarEntry;
			if (this.hasHitEOF)
			{
				tarEntry = null;
			}
			else
			{
				if (this.currentEntry != null)
				{
					await this.SkipToNextEntryAsync(ct, isAsync).ConfigureAwait(false);
				}
				byte[] headerBuf = ArrayPool<byte>.Shared.Rent(512);
				await this.tarBuffer.ReadBlockIntAsync(headerBuf, ct, isAsync).ConfigureAwait(false);
				if (TarBuffer.IsEndOfArchiveBlock(headerBuf))
				{
					this.hasHitEOF = true;
					await this.tarBuffer.ReadBlockIntAsync(headerBuf, ct, isAsync).ConfigureAwait(false);
				}
				else
				{
					this.hasHitEOF = false;
				}
				if (this.hasHitEOF)
				{
					this.currentEntry = null;
					IMemoryOwner<byte> memoryOwner = this.readBuffer;
					if (memoryOwner != null)
					{
						memoryOwner.Dispose();
					}
				}
				else
				{
					try
					{
						TarHeader tarHeader = new TarHeader();
						tarHeader.ParseBuffer(headerBuf, this.encoding);
						if (!tarHeader.IsChecksumValid)
						{
							throw new TarException("Header checksum is invalid");
						}
						this.entryOffset = 0L;
						this.entrySize = tarHeader.Size;
						string longName = null;
						if (tarHeader.TypeFlag == 76)
						{
							using (IMemoryOwner<byte> nameBuffer = ExactMemoryPool<byte>.Shared.Rent(512))
							{
								long numToRead = this.entrySize;
								StringBuilder longNameBuilder = StringBuilderPool.Instance.Rent();
								while (numToRead > 0L)
								{
									int num = ((numToRead > 512L) ? 512 : ((int)numToRead));
									int num2 = await this.ReadAsync(nameBuffer.Memory.Slice(0, num), ct, isAsync).ConfigureAwait(false);
									if (num2 == -1)
									{
										throw new InvalidHeaderException("Failed to read long name entry");
									}
									longNameBuilder.Append(TarHeader.ParseName(nameBuffer.Memory.Slice(0, num2).Span, this.encoding));
									numToRead -= (long)num2;
								}
								longName = longNameBuilder.ToString();
								StringBuilderPool.Instance.Return(longNameBuilder);
								await this.SkipToNextEntryAsync(ct, isAsync).ConfigureAwait(false);
								await this.tarBuffer.ReadBlockIntAsync(headerBuf, ct, isAsync).ConfigureAwait(false);
								longNameBuilder = null;
							}
							IMemoryOwner<byte> nameBuffer = null;
						}
						else if (tarHeader.TypeFlag == 103)
						{
							await this.SkipToNextEntryAsync(ct, isAsync).ConfigureAwait(false);
							await this.tarBuffer.ReadBlockIntAsync(headerBuf, ct, isAsync).ConfigureAwait(false);
						}
						else if (tarHeader.TypeFlag == 120)
						{
							byte[] nameBuffer2 = ArrayPool<byte>.Shared.Rent(512);
							long numToRead = this.entrySize;
							TarExtendedHeaderReader xhr = new TarExtendedHeaderReader();
							while (numToRead > 0L)
							{
								int num3 = ((numToRead > (long)nameBuffer2.Length) ? nameBuffer2.Length : ((int)numToRead));
								int num4 = await this.ReadAsync(nameBuffer2.AsMemory<byte>().Slice(0, num3), ct, isAsync).ConfigureAwait(false);
								if (num4 == -1)
								{
									throw new InvalidHeaderException("Failed to read long name entry");
								}
								xhr.Read(nameBuffer2, num4);
								numToRead -= (long)num4;
							}
							ArrayPool<byte>.Shared.Return(nameBuffer2, false);
							string text;
							if (xhr.Headers.TryGetValue("path", out text))
							{
								longName = text;
							}
							await this.SkipToNextEntryAsync(ct, isAsync).ConfigureAwait(false);
							await this.tarBuffer.ReadBlockIntAsync(headerBuf, ct, isAsync).ConfigureAwait(false);
							nameBuffer2 = null;
							xhr = null;
						}
						else if (tarHeader.TypeFlag == 86)
						{
							await this.SkipToNextEntryAsync(ct, isAsync).ConfigureAwait(false);
							await this.tarBuffer.ReadBlockIntAsync(headerBuf, ct, isAsync).ConfigureAwait(false);
						}
						else if (tarHeader.TypeFlag != 48 && tarHeader.TypeFlag != 0 && tarHeader.TypeFlag != 49 && tarHeader.TypeFlag != 50 && tarHeader.TypeFlag != 53)
						{
							await this.SkipToNextEntryAsync(ct, isAsync).ConfigureAwait(false);
							await this.tarBuffer.ReadBlockIntAsync(headerBuf, ct, isAsync).ConfigureAwait(false);
						}
						if (this.entryFactory == null)
						{
							this.currentEntry = new TarEntry(headerBuf, this.encoding);
							IMemoryOwner<byte> memoryOwner2 = this.readBuffer;
							if (memoryOwner2 != null)
							{
								memoryOwner2.Dispose();
							}
							if (longName != null)
							{
								this.currentEntry.Name = longName;
							}
						}
						else
						{
							this.currentEntry = this.entryFactory.CreateEntry(headerBuf);
							IMemoryOwner<byte> memoryOwner3 = this.readBuffer;
							if (memoryOwner3 != null)
							{
								memoryOwner3.Dispose();
							}
						}
						this.entryOffset = 0L;
						this.entrySize = this.currentEntry.Size;
						longName = null;
					}
					catch (InvalidHeaderException ex)
					{
						this.entrySize = 0L;
						this.entryOffset = 0L;
						this.currentEntry = null;
						IMemoryOwner<byte> memoryOwner4 = this.readBuffer;
						if (memoryOwner4 != null)
						{
							memoryOwner4.Dispose();
						}
						throw new InvalidHeaderException(string.Format("Bad header in record {0} block {1} {2}", this.tarBuffer.CurrentRecord, this.tarBuffer.CurrentBlock, ex.Message));
					}
				}
				ArrayPool<byte>.Shared.Return(headerBuf, false);
				tarEntry = this.currentEntry;
			}
			return tarEntry;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014AFC File Offset: 0x00012CFC
		public Task CopyEntryContentsAsync(Stream outputStream, CancellationToken ct)
		{
			return this.CopyEntryContentsAsync(outputStream, ct, true).AsTask();
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014B1C File Offset: 0x00012D1C
		public void CopyEntryContents(Stream outputStream)
		{
			this.CopyEntryContentsAsync(outputStream, CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00014B48 File Offset: 0x00012D48
		private async ValueTask CopyEntryContentsAsync(Stream outputStream, CancellationToken ct, bool isAsync)
		{
			byte[] tempBuffer = ArrayPool<byte>.Shared.Rent(32768);
			for (;;)
			{
				int num = await this.ReadAsync(tempBuffer, ct, isAsync).ConfigureAwait(false);
				if (num <= 0)
				{
					break;
				}
				if (isAsync)
				{
					await outputStream.WriteAsync(tempBuffer, 0, num, ct).ConfigureAwait(false);
				}
				else
				{
					outputStream.Write(tempBuffer, 0, num);
				}
			}
			ArrayPool<byte>.Shared.Return(tempBuffer, false);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00014BA4 File Offset: 0x00012DA4
		private async ValueTask SkipToNextEntryAsync(CancellationToken ct, bool isAsync)
		{
			long num = this.entrySize - this.entryOffset;
			if (num > 0L)
			{
				await this.SkipAsync(num, ct, isAsync).ConfigureAwait(false);
			}
			IMemoryOwner<byte> memoryOwner = this.readBuffer;
			if (memoryOwner != null)
			{
				memoryOwner.Dispose();
			}
			this.readBuffer = null;
		}

		// Token: 0x04000319 RID: 793
		protected bool hasHitEOF;

		// Token: 0x0400031A RID: 794
		protected long entrySize;

		// Token: 0x0400031B RID: 795
		protected long entryOffset;

		// Token: 0x0400031C RID: 796
		protected IMemoryOwner<byte> readBuffer;

		// Token: 0x0400031D RID: 797
		protected TarBuffer tarBuffer;

		// Token: 0x0400031E RID: 798
		private TarEntry currentEntry;

		// Token: 0x0400031F RID: 799
		protected TarInputStream.IEntryFactory entryFactory;

		// Token: 0x04000320 RID: 800
		private readonly Stream inputStream;

		// Token: 0x04000321 RID: 801
		private readonly Encoding encoding;

		// Token: 0x0200007A RID: 122
		public interface IEntryFactory
		{
			// Token: 0x06000470 RID: 1136
			TarEntry CreateEntry(string name);

			// Token: 0x06000471 RID: 1137
			TarEntry CreateEntryFromFile(string fileName);

			// Token: 0x06000472 RID: 1138
			TarEntry CreateEntry(byte[] headerBuffer);
		}

		// Token: 0x0200007B RID: 123
		public class EntryFactoryAdapter : TarInputStream.IEntryFactory
		{
			// Token: 0x06000473 RID: 1139 RVA: 0x000080C2 File Offset: 0x000062C2
			[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
			public EntryFactoryAdapter()
			{
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x00014BF7 File Offset: 0x00012DF7
			public EntryFactoryAdapter(Encoding nameEncoding)
			{
				this.nameEncoding = nameEncoding;
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x00014C06 File Offset: 0x00012E06
			public TarEntry CreateEntry(string name)
			{
				return TarEntry.CreateTarEntry(name);
			}

			// Token: 0x06000476 RID: 1142 RVA: 0x00014C0E File Offset: 0x00012E0E
			public TarEntry CreateEntryFromFile(string fileName)
			{
				return TarEntry.CreateEntryFromFile(fileName);
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x00014C16 File Offset: 0x00012E16
			public TarEntry CreateEntry(byte[] headerBuffer)
			{
				return new TarEntry(headerBuffer, this.nameEncoding);
			}

			// Token: 0x04000322 RID: 802
			private Encoding nameEncoding;
		}
	}
}
