using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000019 RID: 25
	public class ZipEntryFactory : IEntryFactory
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003E73 File Offset: 0x00002073
		public ZipEntryFactory()
		{
			this.nameTransform_ = new ZipNameTransform();
			this.isUnicodeText_ = true;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E9F File Offset: 0x0000209F
		public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting)
			: this()
		{
			this.timeSetting_ = timeSetting;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003EAE File Offset: 0x000020AE
		public ZipEntryFactory(DateTime time)
			: this()
		{
			this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
			this.FixedDateTime = time;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003EC4 File Offset: 0x000020C4
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003ECC File Offset: 0x000020CC
		public INameTransform NameTransform
		{
			get
			{
				return this.nameTransform_;
			}
			set
			{
				if (value == null)
				{
					this.nameTransform_ = new ZipNameTransform();
					return;
				}
				this.nameTransform_ = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003EE4 File Offset: 0x000020E4
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003EEC File Offset: 0x000020EC
		public ZipEntryFactory.TimeSetting Setting
		{
			get
			{
				return this.timeSetting_;
			}
			set
			{
				this.timeSetting_ = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003EF5 File Offset: 0x000020F5
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003EFD File Offset: 0x000020FD
		public DateTime FixedDateTime
		{
			get
			{
				return this.fixedDateTime_;
			}
			set
			{
				if (value.Year < 1970)
				{
					throw new ArgumentException("Value is too old to be valid", "value");
				}
				this.fixedDateTime_ = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003F24 File Offset: 0x00002124
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003F2C File Offset: 0x0000212C
		public int GetAttributes
		{
			get
			{
				return this.getAttributes_;
			}
			set
			{
				this.getAttributes_ = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003F35 File Offset: 0x00002135
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003F3D File Offset: 0x0000213D
		public int SetAttributes
		{
			get
			{
				return this.setAttributes_;
			}
			set
			{
				this.setAttributes_ = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003F46 File Offset: 0x00002146
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003F4E File Offset: 0x0000214E
		public bool IsUnicodeText
		{
			get
			{
				return this.isUnicodeText_;
			}
			set
			{
				this.isUnicodeText_ = value;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003F57 File Offset: 0x00002157
		public ZipEntry MakeFileEntry(string fileName)
		{
			return this.MakeFileEntry(fileName, null, true);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003F62 File Offset: 0x00002162
		public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
		{
			return this.MakeFileEntry(fileName, null, useFileSystem);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003F70 File Offset: 0x00002170
		public ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformFile((!string.IsNullOrEmpty(entryName)) ? entryName : fileName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			int num = 0;
			bool flag = this.setAttributes_ != 0;
			FileInfo fileInfo = null;
			if (useFileSystem)
			{
				fileInfo = new FileInfo(fileName);
			}
			if (fileInfo != null && fileInfo.Exists)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = fileInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = fileInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = fileInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = fileInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = fileInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = fileInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeFileEntry");
				}
				zipEntry.Size = fileInfo.Length;
				flag = true;
				num = (int)(fileInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
			{
				zipEntry.DateTime = this.fixedDateTime_;
			}
			if (flag)
			{
				num |= this.setAttributes_;
				zipEntry.ExternalFileAttributes = num;
			}
			return zipEntry;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000040B0 File Offset: 0x000022B0
		public ZipEntry MakeDirectoryEntry(string directoryName)
		{
			return this.MakeDirectoryEntry(directoryName, true);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000040BC File Offset: 0x000022BC
		public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformDirectory(directoryName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			zipEntry.Size = 0L;
			int num = 0;
			DirectoryInfo directoryInfo = null;
			if (useFileSystem)
			{
				directoryInfo = new DirectoryInfo(directoryName);
			}
			if (directoryInfo != null && directoryInfo.Exists)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = directoryInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = directoryInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = directoryInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = directoryInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = directoryInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = directoryInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
				}
				num = (int)(directoryInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
			{
				zipEntry.DateTime = this.fixedDateTime_;
			}
			num |= this.setAttributes_ | 16;
			zipEntry.ExternalFileAttributes = num;
			return zipEntry;
		}

		// Token: 0x040000AE RID: 174
		private INameTransform nameTransform_;

		// Token: 0x040000AF RID: 175
		private DateTime fixedDateTime_ = DateTime.Now;

		// Token: 0x040000B0 RID: 176
		private ZipEntryFactory.TimeSetting timeSetting_;

		// Token: 0x040000B1 RID: 177
		private bool isUnicodeText_;

		// Token: 0x040000B2 RID: 178
		private int getAttributes_ = -1;

		// Token: 0x040000B3 RID: 179
		private int setAttributes_;

		// Token: 0x0200001A RID: 26
		public enum TimeSetting
		{
			// Token: 0x040000B5 RID: 181
			LastWriteTime,
			// Token: 0x040000B6 RID: 182
			LastWriteTimeUtc,
			// Token: 0x040000B7 RID: 183
			CreateTime,
			// Token: 0x040000B8 RID: 184
			CreateTimeUtc,
			// Token: 0x040000B9 RID: 185
			LastAccessTime,
			// Token: 0x040000BA RID: 186
			LastAccessTimeUtc,
			// Token: 0x040000BB RID: 187
			Fixed
		}
	}
}
