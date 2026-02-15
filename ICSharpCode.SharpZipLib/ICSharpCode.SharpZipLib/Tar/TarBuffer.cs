using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200006D RID: 109
	public class TarBuffer
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000122A8 File Offset: 0x000104A8
		public int RecordSize
		{
			get
			{
				return this.recordSize;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000122A8 File Offset: 0x000104A8
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.recordSize;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000122B0 File Offset: 0x000104B0
		public int BlockFactor
		{
			get
			{
				return this.blockFactor;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000122B0 File Offset: 0x000104B0
		[Obsolete("Use BlockFactor property instead")]
		public int GetBlockFactor()
		{
			return this.blockFactor;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000122B8 File Offset: 0x000104B8
		protected TarBuffer()
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000122DA File Offset: 0x000104DA
		public static TarBuffer CreateInputTarBuffer(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			return TarBuffer.CreateInputTarBuffer(inputStream, 20);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000122F2 File Offset: 0x000104F2
		public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (blockFactor <= 0)
			{
				throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
			}
			TarBuffer tarBuffer = new TarBuffer();
			tarBuffer.inputStream = inputStream;
			tarBuffer.outputStream = null;
			tarBuffer.Initialize(blockFactor);
			return tarBuffer;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012330 File Offset: 0x00010530
		public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			return TarBuffer.CreateOutputTarBuffer(outputStream, 20);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012348 File Offset: 0x00010548
		public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (blockFactor <= 0)
			{
				throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
			}
			TarBuffer tarBuffer = new TarBuffer();
			tarBuffer.inputStream = null;
			tarBuffer.outputStream = outputStream;
			tarBuffer.Initialize(blockFactor);
			return tarBuffer;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00012388 File Offset: 0x00010588
		private void Initialize(int archiveBlockFactor)
		{
			this.blockFactor = archiveBlockFactor;
			this.recordSize = archiveBlockFactor * 512;
			this.recordBuffer = ArrayPool<byte>.Shared.Rent(this.RecordSize);
			if (this.inputStream != null)
			{
				this.currentRecordIndex = -1;
				this.currentBlockIndex = this.BlockFactor;
				return;
			}
			this.currentRecordIndex = 0;
			this.currentBlockIndex = 0;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000123EC File Offset: 0x000105EC
		[Obsolete("Use IsEndOfArchiveBlock instead")]
		public bool IsEOFBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (block.Length != 512)
			{
				throw new ArgumentException("block length is invalid");
			}
			for (int i = 0; i < 512; i++)
			{
				if (block[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012434 File Offset: 0x00010634
		public static bool IsEndOfArchiveBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (block.Length != 512)
			{
				throw new ArgumentException("block length is invalid");
			}
			for (int i = 0; i < 512; i++)
			{
				if (block[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001247C File Offset: 0x0001067C
		public void SkipBlock()
		{
			this.SkipBlockAsync(CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000124A8 File Offset: 0x000106A8
		public Task SkipBlockAsync(CancellationToken ct)
		{
			return this.SkipBlockAsync(ct, true).AsTask();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000124C8 File Offset: 0x000106C8
		private async ValueTask SkipBlockAsync(CancellationToken ct, bool isAsync)
		{
			if (this.inputStream == null)
			{
				throw new TarException("no input stream defined");
			}
			if (this.currentBlockIndex >= this.BlockFactor)
			{
				ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter = this.ReadRecordAsync(ct, isAsync).ConfigureAwait(false).GetAwaiter();
				if (!configuredValueTaskAwaiter.IsCompleted)
				{
					await configuredValueTaskAwaiter;
					ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
					configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
					configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter);
				}
				if (!configuredValueTaskAwaiter.GetResult())
				{
					throw new TarException("Failed to read a record");
				}
			}
			this.currentBlockIndex++;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001251C File Offset: 0x0001071C
		public byte[] ReadBlock()
		{
			if (this.inputStream == null)
			{
				throw new TarException("TarBuffer.ReadBlock - no input stream defined");
			}
			if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecordAsync(CancellationToken.None, false).GetAwaiter().GetResult())
			{
				throw new TarException("Failed to read a record");
			}
			byte[] array = new byte[512];
			Array.Copy(this.recordBuffer, this.currentBlockIndex * 512, array, 0, 512);
			this.currentBlockIndex++;
			return array;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000125AC File Offset: 0x000107AC
		internal async ValueTask ReadBlockIntAsync(byte[] buffer, CancellationToken ct, bool isAsync)
		{
			if (buffer.Length != 512)
			{
				throw new ArgumentException("BUG: buffer must have length BlockSize");
			}
			if (this.inputStream == null)
			{
				throw new TarException("TarBuffer.ReadBlock - no input stream defined");
			}
			if (this.currentBlockIndex >= this.BlockFactor)
			{
				ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter = this.ReadRecordAsync(ct, isAsync).ConfigureAwait(false).GetAwaiter();
				if (!configuredValueTaskAwaiter.IsCompleted)
				{
					await configuredValueTaskAwaiter;
					ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
					configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
					configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter);
				}
				if (!configuredValueTaskAwaiter.GetResult())
				{
					throw new TarException("Failed to read a record");
				}
			}
			this.recordBuffer.AsSpan<byte>().Slice(this.currentBlockIndex * 512, 512).CopyTo(buffer);
			this.currentBlockIndex++;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00012608 File Offset: 0x00010808
		private async ValueTask<bool> ReadRecordAsync(CancellationToken ct, bool isAsync)
		{
			if (this.inputStream == null)
			{
				throw new TarException("no input stream defined");
			}
			this.currentBlockIndex = 0;
			int offset = 0;
			long num2;
			for (int bytesNeeded = this.RecordSize; bytesNeeded > 0; bytesNeeded -= (int)num2)
			{
				int num;
				if (isAsync)
				{
					num = await this.inputStream.ReadAsync(this.recordBuffer, offset, bytesNeeded, ct).ConfigureAwait(false);
				}
				else
				{
					num = this.inputStream.Read(this.recordBuffer, offset, bytesNeeded);
				}
				num2 = (long)num;
				if (num2 <= 0L)
				{
					while (offset < this.RecordSize)
					{
						this.recordBuffer[offset] = 0;
						num = offset;
						offset = num + 1;
					}
					break;
				}
				offset += (int)num2;
			}
			this.currentRecordIndex++;
			return 1;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001265B File Offset: 0x0001085B
		public int CurrentBlock
		{
			get
			{
				return this.currentBlockIndex;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00012663 File Offset: 0x00010863
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0001266B File Offset: 0x0001086B
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x060003C1 RID: 961 RVA: 0x0001265B File Offset: 0x0001085B
		[Obsolete("Use CurrentBlock property instead")]
		public int GetCurrentBlockNum()
		{
			return this.currentBlockIndex;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00012674 File Offset: 0x00010874
		public int CurrentRecord
		{
			get
			{
				return this.currentRecordIndex;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00012674 File Offset: 0x00010874
		[Obsolete("Use CurrentRecord property instead")]
		public int GetCurrentRecordNum()
		{
			return this.currentRecordIndex;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001267C File Offset: 0x0001087C
		public ValueTask WriteBlockAsync(byte[] block, CancellationToken ct)
		{
			return this.WriteBlockAsync(block, 0, ct);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012687 File Offset: 0x00010887
		public void WriteBlock(byte[] block)
		{
			this.WriteBlock(block, 0);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012691 File Offset: 0x00010891
		public ValueTask WriteBlockAsync(byte[] buffer, int offset, CancellationToken ct)
		{
			return this.WriteBlockAsync(buffer, offset, ct, true);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000126A0 File Offset: 0x000108A0
		public void WriteBlock(byte[] buffer, int offset)
		{
			this.WriteBlockAsync(buffer, offset, CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000126CC File Offset: 0x000108CC
		internal async ValueTask WriteBlockAsync(byte[] buffer, int offset, CancellationToken ct, bool isAsync)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteBlock - no output stream defined");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + 512 > buffer.Length)
			{
				throw new TarException(string.Format("TarBuffer.WriteBlock - record has length '{0}' with offset '{1}' which is less than the record size of '{2}'", buffer.Length, offset, this.recordSize));
			}
			if (this.currentBlockIndex >= this.BlockFactor)
			{
				await this.WriteRecordAsync(CancellationToken.None, isAsync).ConfigureAwait(false);
			}
			Array.Copy(buffer, offset, this.recordBuffer, this.currentBlockIndex * 512, 512);
			this.currentBlockIndex++;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00012728 File Offset: 0x00010928
		private async ValueTask WriteRecordAsync(CancellationToken ct, bool isAsync)
		{
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteRecord no output stream defined");
			}
			if (isAsync)
			{
				await this.outputStream.WriteAsync(this.recordBuffer, 0, this.RecordSize, ct).ConfigureAwait(false);
				await this.outputStream.FlushAsync(ct).ConfigureAwait(false);
			}
			else
			{
				this.outputStream.Write(this.recordBuffer, 0, this.RecordSize);
				this.outputStream.Flush();
			}
			this.currentBlockIndex = 0;
			this.currentRecordIndex++;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001277C File Offset: 0x0001097C
		private async ValueTask WriteFinalRecordAsync(CancellationToken ct, bool isAsync)
		{
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteFinalRecord no output stream defined");
			}
			if (this.currentBlockIndex > 0)
			{
				int num = this.currentBlockIndex * 512;
				Array.Clear(this.recordBuffer, num, this.RecordSize - num);
				await this.WriteRecordAsync(ct, isAsync).ConfigureAwait(false);
			}
			if (isAsync)
			{
				await this.outputStream.FlushAsync(ct).ConfigureAwait(false);
			}
			else
			{
				this.outputStream.Flush();
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000127D0 File Offset: 0x000109D0
		public void Close()
		{
			this.CloseAsync(CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000127FC File Offset: 0x000109FC
		public Task CloseAsync(CancellationToken ct)
		{
			return this.CloseAsync(ct, true).AsTask();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001281C File Offset: 0x00010A1C
		private async ValueTask CloseAsync(CancellationToken ct, bool isAsync)
		{
			if (this.outputStream != null)
			{
				await this.WriteFinalRecordAsync(ct, isAsync).ConfigureAwait(false);
				if (this.IsStreamOwner)
				{
					if (isAsync)
					{
						this.outputStream.Dispose();
					}
					else
					{
						this.outputStream.Dispose();
					}
				}
				this.outputStream = null;
			}
			else if (this.inputStream != null)
			{
				if (this.IsStreamOwner)
				{
					if (isAsync)
					{
						this.inputStream.Dispose();
					}
					else
					{
						this.inputStream.Dispose();
					}
				}
				this.inputStream = null;
			}
			ArrayPool<byte>.Shared.Return(this.recordBuffer, false);
		}

		// Token: 0x0400028F RID: 655
		public const int BlockSize = 512;

		// Token: 0x04000290 RID: 656
		public const int DefaultBlockFactor = 20;

		// Token: 0x04000291 RID: 657
		public const int DefaultRecordSize = 10240;

		// Token: 0x04000293 RID: 659
		private Stream inputStream;

		// Token: 0x04000294 RID: 660
		private Stream outputStream;

		// Token: 0x04000295 RID: 661
		private byte[] recordBuffer;

		// Token: 0x04000296 RID: 662
		private int currentBlockIndex;

		// Token: 0x04000297 RID: 663
		private int currentRecordIndex;

		// Token: 0x04000298 RID: 664
		private int recordSize = 10240;

		// Token: 0x04000299 RID: 665
		private int blockFactor = 20;
	}
}
