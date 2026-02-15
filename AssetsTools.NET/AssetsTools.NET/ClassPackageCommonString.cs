using System;
using System.Collections.Generic;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x0200006B RID: 107
	public class ClassPackageCommonString
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000161D5 File Offset: 0x000143D5
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000161DD File Offset: 0x000143DD
		public List<KeyValuePair<UnityVersion, byte>> VersionInformation { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000161E6 File Offset: 0x000143E6
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x000161EE File Offset: 0x000143EE
		public List<ushort> StringBufferIndices { get; set; }

		// Token: 0x060003B7 RID: 951 RVA: 0x000161F8 File Offset: 0x000143F8
		public void Read(AssetsFileReader reader)
		{
			int num = reader.ReadInt32();
			this.VersionInformation = new List<KeyValuePair<UnityVersion, byte>>(num);
			for (int i = 0; i < num; i++)
			{
				UnityVersion unityVersion = UnityVersion.FromUInt64(reader.ReadUInt64());
				byte b = reader.ReadByte();
				this.VersionInformation.Add(new KeyValuePair<UnityVersion, byte>(unityVersion, b));
			}
			int num2 = reader.ReadInt32();
			this.StringBufferIndices = new List<ushort>(num2);
			for (int j = 0; j < num2; j++)
			{
				this.StringBufferIndices.Add(reader.ReadUInt16());
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00016294 File Offset: 0x00014494
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.VersionInformation.Count);
			foreach (KeyValuePair<UnityVersion, byte> keyValuePair in this.VersionInformation)
			{
				writer.Write(keyValuePair.Key.ToUInt64());
				writer.Write(keyValuePair.Value);
			}
			writer.Write(this.StringBufferIndices.Count);
			for (int i = 0; i < this.StringBufferIndices.Count; i++)
			{
				writer.Write(this.StringBufferIndices[i]);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001635C File Offset: 0x0001455C
		public byte GetCommonStringLengthForVersion(UnityVersion version)
		{
			bool flag = this.VersionInformation.Count == 0;
			byte b;
			if (flag)
			{
				b = 0;
			}
			else
			{
				byte b2 = this.VersionInformation[0].Value;
				for (int i = 0; i < this.VersionInformation.Count; i++)
				{
					bool flag2 = this.VersionInformation[i].Key.ToUInt64() >= version.ToUInt64();
					if (flag2)
					{
						return b2;
					}
					b2 = this.VersionInformation[i].Value;
				}
				b = b2;
			}
			return b;
		}
	}
}
