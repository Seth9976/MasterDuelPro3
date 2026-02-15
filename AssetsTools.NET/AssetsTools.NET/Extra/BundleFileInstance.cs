using System;
using System.Collections.Generic;
using System.IO;

namespace AssetsTools.NET.Extra
{
	// Token: 0x0200007F RID: 127
	public class BundleFileInstance
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00019A30 File Offset: 0x00017C30
		public Stream BundleStream
		{
			get
			{
				return this.file.Reader.BaseStream;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00019A42 File Offset: 0x00017C42
		public Stream DataStream
		{
			get
			{
				return this.file.DataReader.BaseStream;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00019A54 File Offset: 0x00017C54
		public BundleFileInstance(Stream stream, string filePath, bool unpackIfPacked = true)
		{
			this.path = Path.GetFullPath(filePath);
			this.name = Path.GetFileName(this.path);
			this.file = new AssetBundleFile();
			this.file.Read(new AssetsFileReader(stream));
			bool flag = this.file.Header != null && this.file.DataIsCompressed && unpackIfPacked;
			if (flag)
			{
				this.file = BundleHelper.UnpackBundle(this.file, true);
			}
			this.loadedAssetsFiles = new List<AssetsFileInstance>();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00019AE3 File Offset: 0x00017CE3
		public BundleFileInstance(FileStream stream, bool unpackIfPacked = true)
			: this(stream, stream.Name, unpackIfPacked)
		{
		}

		// Token: 0x04000413 RID: 1043
		public string path;

		// Token: 0x04000414 RID: 1044
		public string name;

		// Token: 0x04000415 RID: 1045
		public AssetBundleFile file;

		// Token: 0x04000416 RID: 1046
		public List<AssetsFileInstance> loadedAssetsFiles;
	}
}
