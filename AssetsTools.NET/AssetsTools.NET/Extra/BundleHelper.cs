using System;
using System.Collections.Generic;
using System.IO;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000084 RID: 132
	public static class BundleHelper
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00019DB8 File Offset: 0x00017FB8
		public static byte[] LoadAssetDataFromBundle(AssetBundleFile bundle, int index)
		{
			long num;
			long num2;
			bundle.GetFileRange(index, out num, out num2);
			AssetsFileReader dataReader = bundle.DataReader;
			dataReader.Position = num;
			return dataReader.ReadBytes((int)num2);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00019DF0 File Offset: 0x00017FF0
		public static byte[] LoadAssetDataFromBundle(AssetBundleFile bundle, string name)
		{
			int fileIndex = bundle.GetFileIndex(name);
			bool flag = fileIndex < 0;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				array = BundleHelper.LoadAssetDataFromBundle(bundle, fileIndex);
			}
			return array;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00019E20 File Offset: 0x00018020
		public static AssetsFile LoadAssetFromBundle(AssetBundleFile bundle, int index)
		{
			long num;
			long num2;
			bundle.GetFileRange(index, out num, out num2);
			Stream stream = new SegmentStream(bundle.DataReader.BaseStream, num, num2);
			AssetsFileReader assetsFileReader = new AssetsFileReader(stream);
			AssetsFile assetsFile = new AssetsFile();
			assetsFile.Read(assetsFileReader);
			return assetsFile;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00019E6C File Offset: 0x0001806C
		public static AssetsFile LoadAssetFromBundle(AssetBundleFile bundle, string name)
		{
			int fileIndex = bundle.GetFileIndex(name);
			bool flag = fileIndex < 0;
			AssetsFile assetsFile;
			if (flag)
			{
				assetsFile = null;
			}
			else
			{
				assetsFile = BundleHelper.LoadAssetFromBundle(bundle, fileIndex);
			}
			return assetsFile;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00019E9C File Offset: 0x0001809C
		public static List<byte[]> LoadAllAssetsDataFromBundle(AssetBundleFile bundle)
		{
			List<byte[]> list = new List<byte[]>();
			int num = bundle.BlockAndDirInfo.DirectoryInfos.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = bundle.IsAssetsFile(i);
				if (flag)
				{
					list.Add(BundleHelper.LoadAssetDataFromBundle(bundle, i));
				}
			}
			return list;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00019EF4 File Offset: 0x000180F4
		public static List<AssetsFile> LoadAllAssetsFromBundle(AssetBundleFile bundle)
		{
			List<AssetsFile> list = new List<AssetsFile>();
			int num = bundle.BlockAndDirInfo.DirectoryInfos.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = bundle.IsAssetsFile(i);
				if (flag)
				{
					list.Add(BundleHelper.LoadAssetFromBundle(bundle, i));
				}
			}
			return list;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00019F4C File Offset: 0x0001814C
		public static AssetBundleFile UnpackBundle(AssetBundleFile file, bool freeOriginalStream = true)
		{
			MemoryStream memoryStream = new MemoryStream();
			file.Unpack(new AssetsFileWriter(memoryStream));
			memoryStream.Position = 0L;
			AssetBundleFile assetBundleFile = new AssetBundleFile();
			assetBundleFile.Read(new AssetsFileReader(memoryStream));
			if (freeOriginalStream)
			{
				file.Reader.Close();
				file.DataReader.Close();
			}
			return assetBundleFile;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00019FB0 File Offset: 0x000181B0
		public static AssetBundleFile UnpackBundleToStream(AssetBundleFile file, Stream stream, bool freeOriginalStream = true)
		{
			file.Unpack(new AssetsFileWriter(stream));
			stream.Position = 0L;
			AssetBundleFile assetBundleFile = new AssetBundleFile();
			assetBundleFile.Read(new AssetsFileReader(stream));
			if (freeOriginalStream)
			{
				file.Reader.Close();
				file.DataReader.Close();
			}
			return assetBundleFile;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001A00C File Offset: 0x0001820C
		public static AssetBundleDirectoryInfo GetDirInfo(AssetBundleFile bundle, int index)
		{
			AssetBundleDirectoryInfo[] directoryInfos = bundle.BlockAndDirInfo.DirectoryInfos;
			return directoryInfos[index];
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001A030 File Offset: 0x00018230
		public static AssetBundleDirectoryInfo GetDirInfo(AssetBundleFile bundle, string name)
		{
			foreach (AssetBundleDirectoryInfo assetBundleDirectoryInfo in bundle.BlockAndDirInfo.DirectoryInfos)
			{
				bool flag = assetBundleDirectoryInfo.Name == name;
				if (flag)
				{
					return assetBundleDirectoryInfo;
				}
			}
			return null;
		}
	}
}
