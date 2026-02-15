using System;
using System.Collections.Generic;
using System.IO;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000083 RID: 131
	public class BundleCreator
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x00019BFC File Offset: 0x00017DFC
		public static void CreateBlankAssets(MemoryStream ms, string engineVersion, uint formatVersion, uint typeTreeVersion, bool hasTypeTree = false)
		{
			AssetsFileWriter assetsFileWriter = new AssetsFileWriter(ms);
			AssetsFileHeader assetsFileHeader = new AssetsFileHeader
			{
				MetadataSize = 0L,
				FileSize = -1L,
				Version = formatVersion,
				DataOffset = -1L,
				Endianness = false
			};
			AssetsFileMetadata assetsFileMetadata = new AssetsFileMetadata
			{
				UnityVersion = engineVersion,
				TargetPlatform = typeTreeVersion,
				TypeTreeEnabled = hasTypeTree,
				TypeTreeTypes = new List<TypeTreeType>(),
				AssetInfos = new List<AssetFileInfo>(),
				ScriptTypes = new List<AssetPPtr>(),
				Externals = new List<AssetsFileExternal>(),
				RefTypes = new List<TypeTreeType>()
			};
			assetsFileHeader.Write(assetsFileWriter);
			assetsFileMetadata.Write(assetsFileWriter, formatVersion);
			assetsFileWriter.Write(0U);
			assetsFileWriter.Align();
			assetsFileWriter.Write(0U);
			assetsFileWriter.Write(0U);
			bool flag = assetsFileHeader.Version >= 20U;
			if (flag)
			{
				assetsFileWriter.Write(0);
			}
			uint num = (uint)(assetsFileWriter.Position - 19L);
			bool flag2 = assetsFileHeader.Version >= 22U;
			if (flag2)
			{
				num -= 28U;
			}
			bool flag3 = assetsFileWriter.Position < 4096L;
			if (flag3)
			{
				while (assetsFileWriter.Position < 4096L)
				{
					assetsFileWriter.Write(0);
				}
			}
			else
			{
				bool flag4 = assetsFileWriter.Position % 16L == 0L;
				if (flag4)
				{
					assetsFileWriter.Position += 16L;
				}
				else
				{
					assetsFileWriter.Align16();
				}
			}
			long position = assetsFileWriter.Position;
			assetsFileHeader.FileSize = position;
			assetsFileHeader.DataOffset = position;
			assetsFileHeader.MetadataSize = (long)((ulong)num);
			assetsFileWriter.Position = 0L;
			assetsFileHeader.Write(assetsFileWriter);
			assetsFileWriter.Position = position;
		}
	}
}
