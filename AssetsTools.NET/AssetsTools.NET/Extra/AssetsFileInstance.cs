using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000078 RID: 120
	public class AssetsFileInstance
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00018144 File Offset: 0x00016344
		public Stream AssetsStream
		{
			get
			{
				return this.file.Reader.BaseStream;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00018158 File Offset: 0x00016358
		public AssetsFileInstance(Stream stream, string filePath)
		{
			this.path = Path.GetFullPath(filePath);
			this.name = Path.GetFileName(this.path);
			this.file = new AssetsFile();
			this.file.Read(new AssetsFileReader(stream));
			this.dependencyCache = new Dictionary<int, AssetsFileInstance>();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000181BC File Offset: 0x000163BC
		public AssetsFileInstance(FileStream stream)
		{
			this.path = stream.Name;
			this.name = Path.GetFileName(this.path);
			this.file = new AssetsFile();
			this.file.Read(new AssetsFileReader(stream));
			this.dependencyCache = new Dictionary<int, AssetsFileInstance>();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00018220 File Offset: 0x00016420
		public AssetsFileInstance GetDependency(AssetsManager am, int depIdx)
		{
			bool flag = !this.dependencyCache.ContainsKey(depIdx) || this.dependencyCache[depIdx] == null;
			if (flag)
			{
				string depPath = this.file.Metadata.Externals[depIdx].PathName;
				bool flag2 = depPath == string.Empty;
				if (flag2)
				{
					return null;
				}
				AssetsFileInstance assetsFileInstance;
				bool flag3 = !am.FileLookup.TryGetValue(am.GetFileLookupKey(depPath), out assetsFileInstance);
				if (flag3)
				{
					string directoryName = Path.GetDirectoryName(this.path);
					string text = Path.Combine(directoryName, depPath);
					string text2 = Path.Combine(directoryName, Path.GetFileName(depPath));
					bool flag4 = File.Exists(text);
					if (flag4)
					{
						this.dependencyCache[depIdx] = am.LoadAssetsFile(text, true);
					}
					else
					{
						bool flag5 = File.Exists(text2);
						if (flag5)
						{
							this.dependencyCache[depIdx] = am.LoadAssetsFile(text2, true);
						}
						else
						{
							bool flag6 = this.parentBundle != null;
							if (!flag6)
							{
								return null;
							}
							AssetBundleFile assetBundleFile = this.parentBundle.file;
							bool flag7 = assetBundleFile.BlockAndDirInfo.DirectoryInfos.Any((AssetBundleDirectoryInfo di) => di.Name == depPath);
							bool flag8 = flag7;
							if (flag8)
							{
								this.dependencyCache[depIdx] = am.LoadAssetsFileFromBundle(this.parentBundle, depPath, true);
							}
							else
							{
								string directoryName2 = Path.GetDirectoryName(directoryName);
								string text3 = Path.Combine(directoryName2, Path.GetFileName(depPath));
								bool flag9 = File.Exists(text3);
								if (!flag9)
								{
									return null;
								}
								this.dependencyCache[depIdx] = am.LoadAssetsFile(text3, true);
							}
						}
					}
				}
				else
				{
					this.dependencyCache[depIdx] = assetsFileInstance;
				}
			}
			return this.dependencyCache[depIdx];
		}

		// Token: 0x040003F7 RID: 1015
		public string path;

		// Token: 0x040003F8 RID: 1016
		public string name;

		// Token: 0x040003F9 RID: 1017
		public AssetsFile file;

		// Token: 0x040003FA RID: 1018
		public BundleFileInstance parentBundle = null;

		// Token: 0x040003FB RID: 1019
		internal Dictionary<int, AssetsFileInstance> dependencyCache;
	}
}
