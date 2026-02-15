using System;
using System.Collections.Generic;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x0200006A RID: 106
	public class ClassPackageClassInfo
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00015F4B File Offset: 0x0001414B
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00015F53 File Offset: 0x00014153
		public int ClassId { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00015F5C File Offset: 0x0001415C
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00015F64 File Offset: 0x00014164
		public List<KeyValuePair<UnityVersion, ClassPackageType>> Classes { get; set; }

		// Token: 0x060003AF RID: 943 RVA: 0x00015F70 File Offset: 0x00014170
		public void Read(AssetsFileReader reader)
		{
			this.ClassId = reader.ReadInt32();
			int num = reader.ReadInt32();
			this.Classes = new List<KeyValuePair<UnityVersion, ClassPackageType>>(num);
			for (int i = 0; i < num; i++)
			{
				UnityVersion unityVersion = UnityVersion.FromUInt64(reader.ReadUInt64());
				bool flag = reader.ReadBoolean();
				ClassPackageType classPackageType = null;
				bool flag2 = flag;
				if (flag2)
				{
					classPackageType = new ClassPackageType();
					classPackageType.Read(reader, this.ClassId);
				}
				this.Classes.Add(new KeyValuePair<UnityVersion, ClassPackageType>(unityVersion, classPackageType));
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00015FFC File Offset: 0x000141FC
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.ClassId);
			writer.Write(this.Classes.Count);
			for (int i = 0; i < this.Classes.Count; i++)
			{
				writer.Write(this.Classes[i].Key.ToUInt64());
				bool flag = this.Classes[i].Value != null;
				if (flag)
				{
					writer.Write(1);
					this.Classes[i].Value.Write(writer);
				}
				else
				{
					writer.Write(0);
				}
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000160B4 File Offset: 0x000142B4
		public ClassPackageType GetTypeForVersion(UnityVersion version)
		{
			bool flag = this.Classes.Count == 0;
			ClassPackageType classPackageType;
			if (flag)
			{
				classPackageType = null;
			}
			else
			{
				bool flag2 = this.Classes[0].Key.ToUInt64() > version.ToUInt64();
				if (flag2)
				{
					classPackageType = null;
				}
				else
				{
					ClassPackageType classPackageType2 = this.Classes[0].Value;
					for (int i = 0; i < this.Classes.Count; i++)
					{
						bool flag3 = this.Classes[i].Key.ToUInt64() == version.ToUInt64();
						if (flag3)
						{
							return this.Classes[i].Value;
						}
						bool flag4 = this.Classes[i].Key.ToUInt64() > version.ToUInt64();
						if (flag4)
						{
							return classPackageType2;
						}
						classPackageType2 = this.Classes[i].Value;
					}
					classPackageType = classPackageType2;
				}
			}
			return classPackageType;
		}
	}
}
