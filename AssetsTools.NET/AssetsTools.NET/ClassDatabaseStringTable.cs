using System;
using System.Collections.Generic;

namespace AssetsTools.NET
{
	// Token: 0x02000064 RID: 100
	public class ClassDatabaseStringTable
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00015464 File Offset: 0x00013664
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0001546C File Offset: 0x0001366C
		public List<string> Strings { get; set; }

		// Token: 0x06000379 RID: 889 RVA: 0x00015478 File Offset: 0x00013678
		public void Read(AssetsFileReader reader)
		{
			int num = reader.ReadInt32();
			this.Strings = new List<string>(num);
			for (int i = 0; i < num; i++)
			{
				this.Strings.Add(reader.ReadString());
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000154C0 File Offset: 0x000136C0
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.Strings.Count);
			for (int i = 0; i < this.Strings.Count; i++)
			{
				writer.Write(this.Strings[i]);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00015510 File Offset: 0x00013710
		public ushort AddString(string str)
		{
			int num = this.Strings.IndexOf(str);
			bool flag = num == -1;
			if (flag)
			{
				num = this.Strings.Count;
				this.Strings.Add(str);
			}
			return (ushort)num;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00015554 File Offset: 0x00013754
		public string GetString(ushort index)
		{
			return this.Strings[(int)index];
		}
	}
}
