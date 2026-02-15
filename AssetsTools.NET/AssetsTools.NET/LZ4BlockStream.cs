using System;
using System.Collections.Generic;
using System.IO;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Extra.Decompressors.LZ4;

namespace AssetsTools.NET
{
	// Token: 0x02000072 RID: 114
	public class LZ4BlockStream : Stream
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x00017494 File Offset: 0x00015694
		public LZ4BlockStream(Stream baseStream, long baseOffset, AssetBundleBlockInfo[] blockInfos, int maxBlockMapSize = 382)
		{
			bool flag = baseOffset < 0L || baseOffset > baseStream.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("baseOffset");
			}
			bool flag2 = this.length >= 0L && baseOffset + this.length > baseStream.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.decompressedBlockMap = new Dictionary<int, MemoryStream>();
			this.decompressedBlockQueue = new Queue<int>();
			bool flag3 = blockInfos.Length == 0;
			if (flag3)
			{
				this.length = 0L;
				this.blockSize = 131072L;
				this.BaseStream = new MemoryStream();
				this.BaseOffset = baseOffset;
				this.blockInfos = new AssetBundleBlockInfo[0];
				this.blockPoses = new long[0];
			}
			else
			{
				long num = (long)((ulong)blockInfos[0].CompressedSize);
				this.blockPoses = new long[blockInfos.Length];
				this.blockPoses[0] = 0L;
				this.blockSize = this.GetLz4BlockSize(blockInfos);
				this.length = (long)((ulong)blockInfos[0].DecompressedSize);
				for (int i = 1; i < blockInfos.Length; i++)
				{
					bool flag4 = (ulong)blockInfos[i].DecompressedSize != (ulong)this.blockSize && i != blockInfos.Length - 1;
					if (flag4)
					{
						throw new NotImplementedException("Cannot handle bundles with multiple block sizes yet.");
					}
					this.length += (long)((ulong)blockInfos[i].DecompressedSize);
					this.blockPoses[i] = num;
					num += (long)((ulong)blockInfos[i].CompressedSize);
				}
				bool flag5 = this.blockSize > 2147483647L;
				if (flag5)
				{
					throw new NotImplementedException("Block size too large!");
				}
				this.blockInfos = blockInfos;
				this.maxBlockMapSize = maxBlockMapSize;
				this.BaseStream = baseStream;
				this.BaseOffset = baseOffset;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001764E File Offset: 0x0001584E
		public Stream BaseStream { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00017656 File Offset: 0x00015856
		public long BaseOffset { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001765E File Offset: 0x0001585E
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00017666 File Offset: 0x00015866
		public override long Position { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00017670 File Offset: 0x00015870
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00017688 File Offset: 0x00015888
		public override void Flush()
		{
			this.BaseStream.Flush();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00017698 File Offset: 0x00015898
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = 0;
			while (i < count)
			{
				int num = (int)(this.Position / this.blockSize);
				bool flag = !this.decompressedBlockMap.ContainsKey(num);
				MemoryStream memoryStream2;
				if (flag)
				{
					bool flag2 = this.decompressedBlockMap.Count >= this.maxBlockMapSize;
					if (flag2)
					{
						int num2 = this.decompressedBlockQueue.Dequeue();
						this.decompressedBlockMap[num2].Close();
						this.decompressedBlockMap.Remove(num2);
					}
					this.BaseStream.Position = this.BaseOffset + this.blockPoses[num];
					MemoryStream memoryStream = new MemoryStream();
					this.BaseStream.CopyToCompat(memoryStream, (long)((ulong)this.blockInfos[num].CompressedSize), 81920);
					memoryStream.Position = 0L;
					byte compressionType = this.blockInfos[num].GetCompressionType();
					bool flag3 = compressionType == 0;
					if (flag3)
					{
						memoryStream2 = memoryStream;
					}
					else
					{
						bool flag4 = compressionType == 2 || compressionType == 3;
						if (!flag4)
						{
							throw new Exception("Invalid block compression type in supposed LZ4 only stream!");
						}
						byte[] array = new byte[this.blockInfos[num].DecompressedSize];
						using (Lz4DecoderStream lz4DecoderStream = new Lz4DecoderStream(memoryStream, long.MaxValue))
						{
							lz4DecoderStream.Read(array, 0, array.Length);
						}
						memoryStream2 = new MemoryStream(array);
					}
					this.decompressedBlockMap[num] = memoryStream2;
					this.decompressedBlockQueue.Enqueue(num);
				}
				else
				{
					memoryStream2 = this.decompressedBlockMap[num];
				}
				memoryStream2.Position = this.Position % this.blockSize;
				int num3 = memoryStream2.Read(buffer, offset + i, (int)Math.Min(memoryStream2.Length, (long)(count - i)));
				bool flag5 = num3 == 0;
				if (flag5)
				{
					break;
				}
				i += num3;
				this.Position += (long)num3;
			}
			return i;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000178A4 File Offset: 0x00015AA4
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Position + this.Length + offset;
				break;
			default:
				throw new ArgumentException();
			}
			bool flag = num < 0L || num > this.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this.Position = num;
			return this.Position;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00017925 File Offset: 0x00015B25
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001792D File Offset: 0x00015B2D
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("LZ4BlockStream cannot be written to, only read from.");
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001793C File Offset: 0x00015B3C
		private long GetLz4BlockSize(AssetBundleBlockInfo[] blockInfos)
		{
			for (int i = 0; i < blockInfos.Length; i++)
			{
				bool flag = blockInfos[i].GetCompressionType() == 2 || blockInfos[i].GetCompressionType() == 3;
				if (flag)
				{
					return (long)((ulong)blockInfos[i].DecompressedSize);
				}
			}
			bool flag2 = blockInfos[0].GetCompressionType() == 0;
			if (flag2)
			{
				return (long)((ulong)blockInfos[0].DecompressedSize);
			}
			throw new Exception("No LZ4 blocks were found in block infos. Can't find block size.");
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000179B2 File Offset: 0x00015BB2
		public override bool CanRead
		{
			get
			{
				return this.BaseStream.CanRead;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000179BF File Offset: 0x00015BBF
		public override bool CanSeek
		{
			get
			{
				return this.BaseStream.CanSeek;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000179CC File Offset: 0x00015BCC
		public override bool CanWrite
		{
			get
			{
				return this.BaseStream.CanWrite;
			}
		}

		// Token: 0x04000268 RID: 616
		private readonly long length;

		// Token: 0x04000269 RID: 617
		private readonly long blockSize;

		// Token: 0x0400026A RID: 618
		private readonly AssetBundleBlockInfo[] blockInfos;

		// Token: 0x0400026B RID: 619
		private readonly long[] blockPoses;

		// Token: 0x0400026C RID: 620
		private readonly Dictionary<int, MemoryStream> decompressedBlockMap;

		// Token: 0x0400026D RID: 621
		private readonly Queue<int> decompressedBlockQueue;

		// Token: 0x0400026E RID: 622
		public int maxBlockMapSize;

		// Token: 0x0400026F RID: 623
		public const int DEFAULT_MAX_BLOCK_MAP_SIZE = 382;
	}
}
