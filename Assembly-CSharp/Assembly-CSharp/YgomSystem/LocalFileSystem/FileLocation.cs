using System;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x02000741 RID: 1857
	public struct FileLocation
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isNull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLocationString(Storage storage, string name, FileNameType nameType)
		{
			return null;
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000F3724 File Offset: 0x000F1924
		public static FileLocation ParseLocationString(string str)
		{
			return default(FileLocation);
		}

		// Token: 0x04003416 RID: 13334
		public Storage storage;

		// Token: 0x04003417 RID: 13335
		public string name;

		// Token: 0x04003418 RID: 13336
		public FileNameType nameType;

		// Token: 0x04003419 RID: 13337
		private static readonly string s_locationDelimiter;

		// Token: 0x0400341A RID: 13338
		private static string[] s_locationSeparators;

		// Token: 0x0400341B RID: 13339
		public static FileLocation nullLocation;
	}
}
