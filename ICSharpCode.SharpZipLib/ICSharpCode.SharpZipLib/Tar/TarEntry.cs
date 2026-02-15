using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000075 RID: 117
	public class TarEntry
	{
		// Token: 0x060003DC RID: 988 RVA: 0x00013392 File Offset: 0x00011592
		private TarEntry()
		{
			this.header = new TarHeader();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000133A5 File Offset: 0x000115A5
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarEntry(byte[] headerBuffer)
			: this(headerBuffer, null)
		{
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000133AF File Offset: 0x000115AF
		public TarEntry(byte[] headerBuffer, Encoding nameEncoding)
		{
			this.header = new TarHeader();
			this.header.ParseBuffer(headerBuffer, nameEncoding);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000133CF File Offset: 0x000115CF
		public TarEntry(TarHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			this.header = (TarHeader)header.Clone();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000133F6 File Offset: 0x000115F6
		public object Clone()
		{
			return new TarEntry
			{
				file = this.file,
				header = (TarHeader)this.header.Clone(),
				Name = this.Name
			};
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001342B File Offset: 0x0001162B
		public static TarEntry CreateTarEntry(string name)
		{
			TarEntry tarEntry = new TarEntry();
			tarEntry.NameTarHeader(name);
			return tarEntry;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013439 File Offset: 0x00011639
		public static TarEntry CreateEntryFromFile(string fileName)
		{
			TarEntry tarEntry = new TarEntry();
			tarEntry.GetFileTarHeader(tarEntry.header, fileName);
			return tarEntry;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013450 File Offset: 0x00011650
		public override bool Equals(object obj)
		{
			TarEntry tarEntry = obj as TarEntry;
			return tarEntry != null && this.Name.Equals(tarEntry.Name);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001347A File Offset: 0x0001167A
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013487 File Offset: 0x00011687
		public bool IsDescendent(TarEntry toTest)
		{
			if (toTest == null)
			{
				throw new ArgumentNullException("toTest");
			}
			return toTest.Name.StartsWith(this.Name, StringComparison.Ordinal);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000134A9 File Offset: 0x000116A9
		public TarHeader TarHeader
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000134B1 File Offset: 0x000116B1
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000134BE File Offset: 0x000116BE
		public string Name
		{
			get
			{
				return this.header.Name;
			}
			set
			{
				this.header.Name = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000134CC File Offset: 0x000116CC
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000134D9 File Offset: 0x000116D9
		public int UserId
		{
			get
			{
				return this.header.UserId;
			}
			set
			{
				this.header.UserId = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000134E7 File Offset: 0x000116E7
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x000134F4 File Offset: 0x000116F4
		public int GroupId
		{
			get
			{
				return this.header.GroupId;
			}
			set
			{
				this.header.GroupId = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00013502 File Offset: 0x00011702
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0001350F File Offset: 0x0001170F
		public string UserName
		{
			get
			{
				return this.header.UserName;
			}
			set
			{
				this.header.UserName = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001351D File Offset: 0x0001171D
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0001352A File Offset: 0x0001172A
		public string GroupName
		{
			get
			{
				return this.header.GroupName;
			}
			set
			{
				this.header.GroupName = value;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013538 File Offset: 0x00011738
		public void SetIds(int userId, int groupId)
		{
			this.UserId = userId;
			this.GroupId = groupId;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013548 File Offset: 0x00011748
		public void SetNames(string userName, string groupName)
		{
			this.UserName = userName;
			this.GroupName = groupName;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00013558 File Offset: 0x00011758
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00013565 File Offset: 0x00011765
		public DateTime ModTime
		{
			get
			{
				return this.header.ModTime;
			}
			set
			{
				this.header.ModTime = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00013573 File Offset: 0x00011773
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0001357B File Offset: 0x0001177B
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00013588 File Offset: 0x00011788
		public long Size
		{
			get
			{
				return this.header.Size;
			}
			set
			{
				this.header.Size = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013598 File Offset: 0x00011798
		public bool IsDirectory
		{
			get
			{
				if (this.file != null)
				{
					return Directory.Exists(this.file);
				}
				return this.header != null && (this.header.TypeFlag == 53 || this.Name.EndsWith("/", StringComparison.Ordinal));
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000135E8 File Offset: 0x000117E8
		public void GetFileTarHeader(TarHeader header, string file)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			this.file = file;
			string text = file;
			if (text.IndexOf(Directory.GetCurrentDirectory(), StringComparison.Ordinal) == 0)
			{
				text = text.Substring(Directory.GetCurrentDirectory().Length);
			}
			text = text.ToTarArchivePath();
			header.LinkName = string.Empty;
			header.Name = text;
			if (Directory.Exists(file))
			{
				header.Mode = 1003;
				header.TypeFlag = 53;
				if (header.Name.Length == 0 || header.Name[header.Name.Length - 1] != '/')
				{
					header.Name += "/";
				}
				header.Size = 0L;
			}
			else
			{
				header.Mode = 33216;
				header.TypeFlag = 48;
				header.Size = new FileInfo(file.Replace('/', Path.DirectorySeparatorChar)).Length;
			}
			header.ModTime = global::System.IO.File.GetLastWriteTime(file.Replace('/', Path.DirectorySeparatorChar)).ToUniversalTime();
			header.DevMajor = 0;
			header.DevMinor = 0;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00013714 File Offset: 0x00011914
		public TarEntry[] GetDirectoryEntries()
		{
			if (this.file == null || !Directory.Exists(this.file))
			{
				return Empty.Array<TarEntry>();
			}
			string[] fileSystemEntries = Directory.GetFileSystemEntries(this.file);
			TarEntry[] array = new TarEntry[fileSystemEntries.Length];
			for (int i = 0; i < fileSystemEntries.Length; i++)
			{
				array[i] = TarEntry.CreateEntryFromFile(fileSystemEntries[i]);
			}
			return array;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001376B File Offset: 0x0001196B
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void WriteEntryHeader(byte[] outBuffer)
		{
			this.WriteEntryHeader(outBuffer, null);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013775 File Offset: 0x00011975
		public void WriteEntryHeader(byte[] outBuffer, Encoding nameEncoding)
		{
			this.header.WriteHeader(outBuffer, nameEncoding);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00013784 File Offset: 0x00011984
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static void AdjustEntryName(byte[] buffer, string newName)
		{
			TarEntry.AdjustEntryName(buffer, newName, null);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001378E File Offset: 0x0001198E
		public static void AdjustEntryName(byte[] buffer, string newName, Encoding nameEncoding)
		{
			TarHeader.GetNameBytes(newName, buffer, 0, 100, nameEncoding);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001379C File Offset: 0x0001199C
		public void NameTarHeader(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool flag = name.EndsWith("/", StringComparison.Ordinal);
			this.header.Name = name;
			this.header.Mode = (flag ? 1003 : 33216);
			this.header.UserId = 0;
			this.header.GroupId = 0;
			this.header.Size = 0L;
			this.header.ModTime = DateTime.UtcNow;
			this.header.TypeFlag = (flag ? 53 : 48);
			this.header.LinkName = string.Empty;
			this.header.UserName = string.Empty;
			this.header.GroupName = string.Empty;
			this.header.DevMajor = 0;
			this.header.DevMinor = 0;
		}

		// Token: 0x040002C9 RID: 713
		private string file;

		// Token: 0x040002CA RID: 714
		private TarHeader header;
	}
}
