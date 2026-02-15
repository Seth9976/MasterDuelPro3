using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Extra.Decompressors.LZ4;
using LZ4ps;
using SevenZip.Compression.LZMA;

namespace AssetsTools.NET
{
	// Token: 0x02000031 RID: 49
	public class AssetBundleFile
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public AssetBundleHeader Header { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000CFB9 File Offset: 0x0000B1B9
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000CFC1 File Offset: 0x0000B1C1
		public AssetBundleBlockAndDirInfo BlockAndDirInfo { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000CFCA File Offset: 0x0000B1CA
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000CFD2 File Offset: 0x0000B1D2
		public AssetsFileReader DataReader { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000CFDB File Offset: 0x0000B1DB
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		public bool DataIsCompressed { get; set; }

		// Token: 0x06000132 RID: 306 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		public void Close()
		{
			this.Reader.Close();
			this.DataReader.Close();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000D008 File Offset: 0x0000B208
		public void Read(AssetsFileReader reader)
		{
			this.Reader = reader;
			this.Reader.Position = 0L;
			this.Reader.BigEndian = true;
			string text = reader.ReadNullTerminated();
			uint num = reader.ReadUInt32();
			bool flag = num >= 6U || num <= 8U;
			if (flag)
			{
				this.Reader.Position = 0L;
				this.Header = new AssetBundleHeader();
				this.Header.Read(reader);
				bool flag2 = this.Header.Version >= 7U;
				if (flag2)
				{
					reader.Align16();
				}
				bool flag3 = this.Header.Signature == "UnityFS";
				if (flag3)
				{
					this.UnpackInfoOnly();
				}
				else
				{
					new NotImplementedException("Non UnityFS bundles are not supported yet.");
				}
			}
			else
			{
				new NotImplementedException(string.Format("Version {0} bundles are not supported yet.", num));
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		public void Write(AssetsFileWriter writer, List<BundleReplacer> replacers, ClassDatabaseFile typeMeta = null)
		{
			bool flag = this.Header == null;
			if (flag)
			{
				throw new Exception("Header must be loaded! (Did you forget to call bundle.Read?)");
			}
			bool flag2 = this.Header.Signature != "UnityFS";
			if (flag2)
			{
				throw new NotImplementedException("Non UnityFS bundles are not supported yet.");
			}
			bool dataIsCompressed = this.DataIsCompressed;
			if (dataIsCompressed)
			{
				throw new Exception("Bundles must be decompressed before writing.");
			}
			writer.Position = 0L;
			AssetBundleDirectoryInfo[] directoryInfos = this.BlockAndDirInfo.DirectoryInfos;
			this.Header.Write(writer);
			bool flag3 = this.Header.Version >= 7U;
			if (flag3)
			{
				writer.Align16();
			}
			AssetBundleBlockInfo assetBundleBlockInfo = new AssetBundleBlockInfo
			{
				CompressedSize = 0U,
				DecompressedSize = 0U,
				Flags = 64
			};
			AssetBundleBlockAndDirInfo assetBundleBlockAndDirInfo = new AssetBundleBlockAndDirInfo
			{
				Hash = default(Hash128),
				BlockInfos = new AssetBundleBlockInfo[] { assetBundleBlockInfo }
			};
			Dictionary<AssetBundleDirectoryInfo, AssetBundleDirectoryInfo> dictionary = new Dictionary<AssetBundleDirectoryInfo, AssetBundleDirectoryInfo>();
			List<AssetBundleDirectoryInfo> list = new List<AssetBundleDirectoryInfo>();
			List<AssetBundleDirectoryInfo> list2 = new List<AssetBundleDirectoryInfo>();
			List<BundleReplacer> list3 = replacers.ToList<BundleReplacer>();
			int i = 0;
			while (i < directoryInfos.Length)
			{
				AssetBundleDirectoryInfo assetBundleDirectoryInfo = directoryInfos[i];
				list.Add(assetBundleDirectoryInfo);
				AssetBundleDirectoryInfo newInfo = new AssetBundleDirectoryInfo
				{
					Offset = 0L,
					DecompressedSize = 0L,
					Flags = assetBundleDirectoryInfo.Flags,
					Name = assetBundleDirectoryInfo.Name
				};
				BundleReplacer bundleReplacer = list3.FirstOrDefault((BundleReplacer rep) => rep.GetOriginalEntryName() == newInfo.Name);
				bool flag4 = bundleReplacer != null;
				if (!flag4)
				{
					dictionary[newInfo] = assetBundleDirectoryInfo;
					goto IL_0285;
				}
				bool flag5 = !bundleReplacer.Init(this.DataReader, assetBundleDirectoryInfo.Offset, assetBundleDirectoryInfo.DecompressedSize, typeMeta);
				if (flag5)
				{
					throw new Exception("Something went wrong initializing a replacer!");
				}
				list3.Remove(bundleReplacer);
				bool flag6 = bundleReplacer.GetReplacementType() == BundleReplacementType.AddOrModify;
				if (flag6)
				{
					newInfo = new AssetBundleDirectoryInfo
					{
						Offset = 0L,
						DecompressedSize = 0L,
						Flags = assetBundleDirectoryInfo.Flags,
						Name = bundleReplacer.GetEntryName()
					};
				}
				else
				{
					bool flag7 = bundleReplacer.GetReplacementType() == BundleReplacementType.Rename;
					if (flag7)
					{
						newInfo = new AssetBundleDirectoryInfo
						{
							Offset = 0L,
							DecompressedSize = 0L,
							Flags = assetBundleDirectoryInfo.Flags,
							Name = bundleReplacer.GetEntryName()
						};
						dictionary[newInfo] = assetBundleDirectoryInfo;
					}
					else
					{
						bool flag8 = bundleReplacer.GetReplacementType() == BundleReplacementType.Remove;
						if (flag8)
						{
							goto IL_0295;
						}
					}
				}
				goto IL_0285;
				IL_0295:
				i++;
				continue;
				IL_0285:
				list2.Add(newInfo);
				goto IL_0295;
			}
			while (list3.Count > 0)
			{
				BundleReplacer bundleReplacer2 = list3[0];
				bool flag9 = bundleReplacer2.GetReplacementType() == BundleReplacementType.AddOrModify;
				if (flag9)
				{
					AssetBundleDirectoryInfo assetBundleDirectoryInfo2 = new AssetBundleDirectoryInfo
					{
						Offset = 0L,
						DecompressedSize = 0L,
						Flags = (bundleReplacer2.HasSerializedData() ? 4U : 0U),
						Name = bundleReplacer2.GetEntryName()
					};
					list2.Add(assetBundleDirectoryInfo2);
				}
				list3.Remove(bundleReplacer2);
			}
			long position = writer.Position;
			assetBundleBlockAndDirInfo.DirectoryInfos = list2.ToArray();
			assetBundleBlockAndDirInfo.Write(writer);
			bool flag10 = (this.Header.FileStreamHeader.Flags & AssetBundleFSHeaderFlags.BlockInfoNeedPaddingAtStart) > AssetBundleFSHeaderFlags.None;
			if (flag10)
			{
				writer.Align16();
			}
			long position2 = writer.Position;
			int j = 0;
			while (j < list2.Count)
			{
				AssetBundleDirectoryInfo info = list2[j];
				BundleReplacer bundleReplacer3 = replacers.FirstOrDefault((BundleReplacer n) => n.GetEntryName() == info.Name);
				bool flag11 = bundleReplacer3 != null && bundleReplacer3.GetReplacementType() != BundleReplacementType.Rename;
				if (flag11)
				{
					bool flag12 = bundleReplacer3.GetReplacementType() == BundleReplacementType.AddOrModify;
					if (flag12)
					{
						long position3 = writer.Position;
						long num = bundleReplacer3.Write(writer);
						long num2 = num - position3;
						list2[j].DecompressedSize = num2;
						list2[j].Offset = position3 - position2;
					}
					else
					{
						bool flag13 = bundleReplacer3.GetReplacementType() == BundleReplacementType.Remove;
						if (flag13)
						{
						}
					}
				}
				else
				{
					AssetBundleDirectoryInfo assetBundleDirectoryInfo3;
					bool flag14 = dictionary.TryGetValue(info, out assetBundleDirectoryInfo3);
					if (flag14)
					{
						long position4 = writer.Position;
						this.DataReader.Position = assetBundleDirectoryInfo3.Offset;
						this.DataReader.BaseStream.CopyToCompat(writer.BaseStream, assetBundleDirectoryInfo3.DecompressedSize, 81920);
						list2[j].DecompressedSize = assetBundleDirectoryInfo3.DecompressedSize;
						list2[j].Offset = position4 - position2;
					}
				}
				IL_04AD:
				j++;
				continue;
				goto IL_04AD;
			}
			long position5 = writer.Position;
			uint num3 = (uint)(position5 - position2);
			writer.Position = position;
			assetBundleBlockInfo.DecompressedSize = num3;
			assetBundleBlockInfo.CompressedSize = num3;
			assetBundleBlockAndDirInfo.DirectoryInfos = list2.ToArray();
			assetBundleBlockAndDirInfo.Write(writer);
			uint num4 = (uint)(position2 - position);
			writer.Position = 0L;
			AssetBundleHeader assetBundleHeader = new AssetBundleHeader
			{
				Signature = this.Header.Signature,
				Version = this.Header.Version,
				GenerationVersion = this.Header.GenerationVersion,
				EngineVersion = this.Header.EngineVersion,
				FileStreamHeader = new AssetBundleFSHeader
				{
					TotalFileSize = position5,
					CompressedSize = num4,
					DecompressedSize = num4,
					Flags = (this.Header.FileStreamHeader.Flags & (AssetBundleFSHeaderFlags)(-129) & (AssetBundleFSHeaderFlags)(-64))
				}
			};
			assetBundleHeader.Write(writer);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		public void Unpack(AssetsFileWriter writer)
		{
			bool flag = this.Header == null;
			if (flag)
			{
				new Exception("Header must be loaded! (Did you forget to call bundle.Read?)");
			}
			bool flag2 = this.Header.Signature != "UnityFS";
			if (flag2)
			{
				new NotImplementedException("Non UnityFS bundles are not supported yet.");
			}
			AssetBundleFSHeader fileStreamHeader = this.Header.FileStreamHeader;
			AssetsFileReader dataReader = this.DataReader;
			AssetBundleBlockInfo[] blockInfos = this.BlockAndDirInfo.BlockInfos;
			AssetBundleDirectoryInfo[] directoryInfos = this.BlockAndDirInfo.DirectoryInfos;
			AssetBundleHeader assetBundleHeader = new AssetBundleHeader
			{
				Signature = this.Header.Signature,
				Version = this.Header.Version,
				GenerationVersion = this.Header.GenerationVersion,
				EngineVersion = this.Header.EngineVersion,
				FileStreamHeader = new AssetBundleFSHeader
				{
					TotalFileSize = 0L,
					CompressedSize = fileStreamHeader.DecompressedSize,
					DecompressedSize = fileStreamHeader.DecompressedSize,
					Flags = (AssetBundleFSHeaderFlags.HasDirectoryInfo | (((fileStreamHeader.Flags & AssetBundleFSHeaderFlags.BlockInfoNeedPaddingAtStart) != AssetBundleFSHeaderFlags.None) ? AssetBundleFSHeaderFlags.BlockInfoNeedPaddingAtStart : AssetBundleFSHeaderFlags.None))
				}
			};
			long num = assetBundleHeader.GetFileDataOffset();
			for (int i = 0; i < blockInfos.Length; i++)
			{
				num += (long)((ulong)blockInfos[i].DecompressedSize);
			}
			assetBundleHeader.FileStreamHeader.TotalFileSize = num;
			AssetBundleBlockAndDirInfo assetBundleBlockAndDirInfo = new AssetBundleBlockAndDirInfo
			{
				Hash = default(Hash128),
				BlockInfos = new AssetBundleBlockInfo[blockInfos.Length],
				DirectoryInfos = new AssetBundleDirectoryInfo[directoryInfos.Length]
			};
			for (int j = 0; j < assetBundleBlockAndDirInfo.BlockInfos.Length; j++)
			{
				assetBundleBlockAndDirInfo.BlockInfos[j] = new AssetBundleBlockInfo
				{
					CompressedSize = blockInfos[j].DecompressedSize,
					DecompressedSize = blockInfos[j].DecompressedSize,
					Flags = (ushort)((int)blockInfos[j].Flags & -64)
				};
			}
			for (int k = 0; k < assetBundleBlockAndDirInfo.DirectoryInfos.Length; k++)
			{
				assetBundleBlockAndDirInfo.DirectoryInfos[k] = new AssetBundleDirectoryInfo
				{
					Offset = directoryInfos[k].Offset,
					DecompressedSize = directoryInfos[k].DecompressedSize,
					Flags = directoryInfos[k].Flags,
					Name = directoryInfos[k].Name
				};
			}
			assetBundleHeader.Write(writer);
			bool flag3 = assetBundleHeader.Version >= 7U;
			if (flag3)
			{
				writer.Align16();
			}
			assetBundleBlockAndDirInfo.Write(writer);
			bool flag4 = (assetBundleHeader.FileStreamHeader.Flags & AssetBundleFSHeaderFlags.BlockInfoNeedPaddingAtStart) > AssetBundleFSHeaderFlags.None;
			if (flag4)
			{
				writer.Align16();
			}
			dataReader.Position = 0L;
			bool dataIsCompressed = this.DataIsCompressed;
			if (dataIsCompressed)
			{
				for (int l = 0; l < assetBundleBlockAndDirInfo.BlockInfos.Length; l++)
				{
					AssetBundleBlockInfo assetBundleBlockInfo = blockInfos[l];
					switch (assetBundleBlockInfo.GetCompressionType())
					{
					case 0:
						dataReader.BaseStream.CopyToCompat(writer.BaseStream, (long)((ulong)assetBundleBlockInfo.CompressedSize), 81920);
						break;
					case 1:
						SevenZipHelper.StreamDecompress(dataReader.BaseStream, writer.BaseStream, (long)((ulong)assetBundleBlockInfo.CompressedSize), (long)((ulong)assetBundleBlockInfo.DecompressedSize));
						break;
					case 2:
					case 3:
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							dataReader.BaseStream.CopyToCompat(memoryStream, (long)((ulong)assetBundleBlockInfo.CompressedSize), 81920);
							memoryStream.Position = 0L;
							using (Lz4DecoderStream lz4DecoderStream = new Lz4DecoderStream(memoryStream, long.MaxValue))
							{
								lz4DecoderStream.CopyToCompat(writer.BaseStream, (long)((ulong)assetBundleBlockInfo.DecompressedSize), 81920);
							}
						}
						break;
					}
					}
				}
			}
			else
			{
				for (int m = 0; m < assetBundleBlockAndDirInfo.BlockInfos.Length; m++)
				{
					AssetBundleBlockInfo assetBundleBlockInfo2 = blockInfos[m];
					dataReader.BaseStream.CopyToCompat(writer.BaseStream, (long)((ulong)assetBundleBlockInfo2.DecompressedSize), 81920);
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public void Pack(AssetsFileReader reader, AssetsFileWriter writer, AssetBundleCompressionType compType, bool blockDirAtEnd = true, IAssetBundleCompressProgress progress = null)
		{
			bool flag = this.Header == null;
			if (flag)
			{
				throw new Exception("Header must be loaded! (Did you forget to call bundle.Read?)");
			}
			bool flag2 = this.Header.Signature != "UnityFS";
			if (flag2)
			{
				throw new NotImplementedException("Non UnityFS bundles are not supported yet.");
			}
			bool dataIsCompressed = this.DataIsCompressed;
			if (dataIsCompressed)
			{
				throw new Exception("Bundles must be decompressed before writing.");
			}
			reader.Position = 0L;
			writer.Position = 0L;
			AssetBundleFSHeader assetBundleFSHeader = new AssetBundleFSHeader
			{
				TotalFileSize = 0L,
				CompressedSize = 0U,
				DecompressedSize = 0U,
				Flags = ((AssetBundleFSHeaderFlags)67 | (blockDirAtEnd ? AssetBundleFSHeaderFlags.BlockAndDirAtEnd : AssetBundleFSHeaderFlags.None))
			};
			AssetBundleHeader assetBundleHeader = new AssetBundleHeader
			{
				Signature = this.Header.Signature,
				Version = this.Header.Version,
				GenerationVersion = this.Header.GenerationVersion,
				EngineVersion = this.Header.EngineVersion,
				FileStreamHeader = assetBundleFSHeader
			};
			AssetBundleBlockAndDirInfo assetBundleBlockAndDirInfo = new AssetBundleBlockAndDirInfo
			{
				Hash = default(Hash128),
				BlockInfos = null,
				DirectoryInfos = this.BlockAndDirInfo.DirectoryInfos
			};
			long position = writer.Position;
			assetBundleHeader.Write(writer);
			bool flag3 = assetBundleHeader.Version >= 7U;
			if (flag3)
			{
				writer.Align16();
			}
			int num = (int)(writer.Position - position);
			long num2 = 0L;
			List<AssetBundleBlockInfo> list = new List<AssetBundleBlockInfo>();
			List<Stream> list2 = new List<Stream>();
			Stream baseStream = this.DataReader.BaseStream;
			baseStream.Position = 0L;
			int num3 = (int)baseStream.Length;
			switch (compType)
			{
			case AssetBundleCompressionType.None:
			{
				AssetBundleBlockInfo assetBundleBlockInfo = new AssetBundleBlockInfo
				{
					CompressedSize = (uint)num3,
					DecompressedSize = (uint)num3,
					Flags = 0
				};
				num2 += (long)((ulong)assetBundleBlockInfo.CompressedSize);
				list.Add(assetBundleBlockInfo);
				if (blockDirAtEnd)
				{
					baseStream.CopyToCompat(writer.BaseStream, -1L, 81920);
				}
				else
				{
					list2.Add(baseStream);
				}
				break;
			}
			case AssetBundleCompressionType.LZMA:
			{
				Stream stream;
				if (blockDirAtEnd)
				{
					stream = writer.BaseStream;
				}
				else
				{
					stream = this.GetTempFileStream();
				}
				AssetBundleLZMAProgress assetBundleLZMAProgress = new AssetBundleLZMAProgress(progress, baseStream.Length);
				long position2 = stream.Position;
				SevenZipHelper.Compress(baseStream, stream, assetBundleLZMAProgress);
				uint num4 = (uint)(stream.Position - position2);
				AssetBundleBlockInfo assetBundleBlockInfo2 = new AssetBundleBlockInfo
				{
					CompressedSize = num4,
					DecompressedSize = (uint)num3,
					Flags = 65
				};
				num2 += (long)((ulong)assetBundleBlockInfo2.CompressedSize);
				list.Add(assetBundleBlockInfo2);
				bool flag4 = !blockDirAtEnd;
				if (flag4)
				{
					list2.Add(stream);
				}
				bool flag5 = progress != null;
				if (flag5)
				{
					progress.SetProgress(1f);
				}
				break;
			}
			case AssetBundleCompressionType.LZ4:
			{
				BinaryReader binaryReader = new BinaryReader(baseStream);
				Stream stream2;
				if (blockDirAtEnd)
				{
					stream2 = writer.BaseStream;
				}
				else
				{
					stream2 = this.GetTempFileStream();
				}
				byte[] array = binaryReader.ReadBytes(131072);
				while (array.Length != 0)
				{
					byte[] array2 = LZ4Codec.Encode32HC(array, 0, array.Length);
					bool flag6 = progress != null;
					if (flag6)
					{
						progress.SetProgress((float)binaryReader.BaseStream.Position / (float)binaryReader.BaseStream.Length);
					}
					bool flag7 = array2.Length > array.Length;
					if (flag7)
					{
						stream2.Write(array, 0, array.Length);
						AssetBundleBlockInfo assetBundleBlockInfo3 = new AssetBundleBlockInfo
						{
							CompressedSize = (uint)array.Length,
							DecompressedSize = (uint)array.Length,
							Flags = 0
						};
						num2 += (long)((ulong)assetBundleBlockInfo3.CompressedSize);
						list.Add(assetBundleBlockInfo3);
					}
					else
					{
						stream2.Write(array2, 0, array2.Length);
						AssetBundleBlockInfo assetBundleBlockInfo4 = new AssetBundleBlockInfo
						{
							CompressedSize = (uint)array2.Length,
							DecompressedSize = (uint)array.Length,
							Flags = 3
						};
						num2 += (long)((ulong)assetBundleBlockInfo4.CompressedSize);
						list.Add(assetBundleBlockInfo4);
					}
					array = binaryReader.ReadBytes(131072);
				}
				bool flag8 = !blockDirAtEnd;
				if (flag8)
				{
					list2.Add(stream2);
				}
				bool flag9 = progress != null;
				if (flag9)
				{
					progress.SetProgress(1f);
				}
				break;
			}
			}
			assetBundleBlockAndDirInfo.BlockInfos = list.ToArray();
			byte[] array3;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				assetBundleBlockAndDirInfo.Write(new AssetsFileWriter(memoryStream)
				{
					BigEndian = writer.BigEndian
				});
				array3 = memoryStream.ToArray();
			}
			byte[] array4 = LZ4Codec.Encode32HC(array3, 0, array3.Length);
			long num5 = (long)(num + array4.Length) + num2;
			assetBundleFSHeader.TotalFileSize = num5;
			assetBundleFSHeader.DecompressedSize = (uint)array3.Length;
			assetBundleFSHeader.CompressedSize = (uint)array4.Length;
			bool flag10 = !blockDirAtEnd;
			if (flag10)
			{
				writer.Write(array4);
				foreach (Stream stream3 in list2)
				{
					stream3.Position = 0L;
					stream3.CopyToCompat(writer.BaseStream, -1L, 81920);
					stream3.Close();
				}
			}
			else
			{
				writer.Write(array4);
			}
			writer.Position = 0L;
			assetBundleHeader.Write(writer);
			bool flag11 = assetBundleHeader.Version >= 7U;
			if (flag11)
			{
				writer.Align16();
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000E09C File Offset: 0x0000C29C
		public void UnpackInfoOnly()
		{
			this.Reader.Position = this.Header.GetBundleInfoOffset();
			bool flag = this.Header.GetCompressionType() == 0;
			if (flag)
			{
				this.BlockAndDirInfo = new AssetBundleBlockAndDirInfo();
				this.BlockAndDirInfo.Read(this.Reader);
			}
			else
			{
				int compressedSize = (int)this.Header.FileStreamHeader.CompressedSize;
				int decompressedSize = (int)this.Header.FileStreamHeader.DecompressedSize;
				byte compressionType = this.Header.GetCompressionType();
				byte b = compressionType;
				MemoryStream memoryStream;
				if (b != 1)
				{
					if (b - 2 > 1)
					{
						memoryStream = null;
					}
					else
					{
						byte[] array = new byte[this.Header.FileStreamHeader.DecompressedSize];
						using (MemoryStream memoryStream2 = new MemoryStream(this.Reader.ReadBytes(compressedSize)))
						{
							Lz4DecoderStream lz4DecoderStream = new Lz4DecoderStream(memoryStream2, long.MaxValue);
							lz4DecoderStream.Read(array, 0, (int)this.Header.FileStreamHeader.DecompressedSize);
							lz4DecoderStream.Dispose();
						}
						memoryStream = new MemoryStream(array);
					}
				}
				else
				{
					using (MemoryStream memoryStream3 = new MemoryStream(this.Reader.ReadBytes(compressedSize)))
					{
						memoryStream = new MemoryStream();
						SevenZipHelper.StreamDecompress(memoryStream3, memoryStream, (long)compressedSize, (long)decompressedSize);
					}
				}
				AssetsFileReader assetsFileReader2;
				AssetsFileReader assetsFileReader = (assetsFileReader2 = new AssetsFileReader(memoryStream));
				try
				{
					assetsFileReader.Position = 0L;
					assetsFileReader.BigEndian = this.Reader.BigEndian;
					this.BlockAndDirInfo = new AssetBundleBlockAndDirInfo();
					this.BlockAndDirInfo.Read(assetsFileReader);
				}
				finally
				{
					if (assetsFileReader2 != null)
					{
						((IDisposable)assetsFileReader2).Dispose();
					}
				}
			}
			switch (this.GetCompressionType(this.BlockAndDirInfo.BlockInfos))
			{
			case AssetBundleCompressionType.None:
			{
				SegmentStream segmentStream = new SegmentStream(this.Reader.BaseStream, this.Header.GetFileDataOffset());
				this.DataReader = new AssetsFileReader(segmentStream);
				this.DataIsCompressed = false;
				break;
			}
			case AssetBundleCompressionType.LZMA:
			{
				SegmentStream segmentStream2 = new SegmentStream(this.Reader.BaseStream, this.Header.GetFileDataOffset());
				this.DataReader = new AssetsFileReader(segmentStream2);
				this.DataIsCompressed = true;
				break;
			}
			case AssetBundleCompressionType.LZ4:
			{
				LZ4BlockStream lz4BlockStream = new LZ4BlockStream(this.Reader.BaseStream, this.Header.GetFileDataOffset(), this.BlockAndDirInfo.BlockInfos, 382);
				this.DataReader = new AssetsFileReader(lz4BlockStream);
				this.DataIsCompressed = false;
				break;
			}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000E358 File Offset: 0x0000C558
		public AssetBundleCompressionType GetCompressionType(AssetBundleBlockInfo[] blockInfos)
		{
			int i = 0;
			while (i < blockInfos.Length)
			{
				byte compressionType = blockInfos[i].GetCompressionType();
				bool flag = compressionType == 2 || compressionType == 3;
				AssetBundleCompressionType assetBundleCompressionType;
				if (flag)
				{
					assetBundleCompressionType = AssetBundleCompressionType.LZ4;
				}
				else
				{
					bool flag2 = compressionType == 1;
					if (!flag2)
					{
						i++;
						continue;
					}
					assetBundleCompressionType = AssetBundleCompressionType.LZMA;
				}
				return assetBundleCompressionType;
			}
			return AssetBundleCompressionType.None;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		public bool IsAssetsFile(int index)
		{
			long num;
			long num2;
			this.GetFileRange(index, out num, out num2);
			return AssetsFile.IsAssetsFile(this.DataReader, num, num2);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		public int GetFileIndex(string name)
		{
			bool flag = this.Header == null;
			if (flag)
			{
				throw new Exception("Header must be loaded! (Did you forget to call bundle.Read?)");
			}
			for (int i = 0; i < this.BlockAndDirInfo.DirectoryInfos.Length; i++)
			{
				bool flag2 = this.BlockAndDirInfo.DirectoryInfos[i].Name == name;
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000E448 File Offset: 0x0000C648
		public string GetFileName(int index)
		{
			bool flag = this.Header == null;
			if (flag)
			{
				throw new Exception("Header must be loaded! (Did you forget to call bundle.Read?)");
			}
			return this.BlockAndDirInfo.DirectoryInfos[index].Name;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000E484 File Offset: 0x0000C684
		public void GetFileRange(int index, out long offset, out long length)
		{
			bool flag = this.Header == null;
			if (flag)
			{
				throw new Exception("Header must be loaded! (Did you forget to call bundle.Read?)");
			}
			AssetBundleDirectoryInfo assetBundleDirectoryInfo = this.BlockAndDirInfo.DirectoryInfos[index];
			offset = assetBundleDirectoryInfo.Offset;
			length = assetBundleDirectoryInfo.DecompressedSize;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		public List<string> GetAllFileNames()
		{
			List<string> list = new List<string>();
			AssetBundleDirectoryInfo[] directoryInfos = this.BlockAndDirInfo.DirectoryInfos;
			foreach (AssetBundleDirectoryInfo assetBundleDirectoryInfo in directoryInfos)
			{
				list.Add(assetBundleDirectoryInfo.Name);
			}
			return list;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000E518 File Offset: 0x0000C718
		private FileStream GetTempFileStream()
		{
			string tempFileName = Path.GetTempFileName();
			return new FileStream(tempFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose);
		}

		// Token: 0x04000144 RID: 324
		public AssetsFileReader Reader;
	}
}
