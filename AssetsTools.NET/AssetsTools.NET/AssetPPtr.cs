using System;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x0200003B RID: 59
	public class AssetPPtr
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000EE32 File Offset: 0x0000D032
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000EE3A File Offset: 0x0000D03A
		public string FilePath { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000EE43 File Offset: 0x0000D043
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000EE4B File Offset: 0x0000D04B
		public int FileId { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000EE54 File Offset: 0x0000D054
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public long PathId { get; set; }

		// Token: 0x06000184 RID: 388 RVA: 0x0000EE65 File Offset: 0x0000D065
		public AssetPPtr()
		{
			this.FilePath = string.Empty;
			this.FileId = 0;
			this.PathId = 0L;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000EE8C File Offset: 0x0000D08C
		public AssetPPtr(int fileId, long pathId)
		{
			this.FilePath = string.Empty;
			this.FileId = fileId;
			this.PathId = pathId;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000EEB2 File Offset: 0x0000D0B2
		public AssetPPtr(string fileName, long pathId)
		{
			this.FilePath = fileName;
			this.FileId = 0;
			this.PathId = pathId;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public AssetPPtr(string fileName, int fileId, long pathId)
		{
			this.FilePath = fileName;
			this.FileId = fileId;
			this.PathId = pathId;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public bool HasFilePath()
		{
			return this.FilePath != string.Empty && this.FilePath != null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000EF28 File Offset: 0x0000D128
		public bool IsNull()
		{
			bool flag = this.HasFilePath();
			bool flag2;
			if (flag)
			{
				flag2 = this.PathId == 0L;
			}
			else
			{
				flag2 = this.FileId == 0 && this.PathId == 0L;
			}
			return flag2;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000EF68 File Offset: 0x0000D168
		public void SetFilePathFromFile(AssetsFile file)
		{
			int num = this.FileId - 1;
			bool flag = this.FileId > 0 && file.Metadata.Externals.Count < num;
			if (flag)
			{
				this.FilePath = file.Metadata.Externals[num].PathName;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public void SetFilePathFromFile(AssetsManager am, AssetsFileInstance fileInst)
		{
			bool flag = this.FileId == 0;
			if (flag)
			{
				this.FilePath = fileInst.path;
			}
			else
			{
				int num = this.FileId - 1;
				AssetsFileInstance dependency = fileInst.GetDependency(am, num);
				bool flag2 = dependency != null;
				if (flag2)
				{
					this.FilePath = dependency.path;
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000F014 File Offset: 0x0000D214
		public static AssetPPtr FromField(AssetTypeValueField field)
		{
			return new AssetPPtr(field["m_FileID"].AsInt, field["m_PathID"].AsLong);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000F04C File Offset: 0x0000D24C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is AssetPPtr);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				AssetPPtr assetPPtr = (AssetPPtr)obj;
				bool flag3 = assetPPtr.HasFilePath() && this.HasFilePath();
				if (flag3)
				{
					flag2 = assetPPtr.PathId == this.PathId && assetPPtr.FilePath == this.FilePath;
				}
				else
				{
					bool flag4 = !assetPPtr.HasFilePath() && !this.HasFilePath();
					flag2 = flag4 && assetPPtr.PathId == this.PathId && assetPPtr.FileId == this.FileId;
				}
			}
			return flag2;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + (this.HasFilePath() ? this.FilePath.GetHashCode() : this.FileId.GetHashCode());
			return num * 23 + this.PathId.GetHashCode();
		}
	}
}
