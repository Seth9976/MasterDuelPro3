using System;

namespace AssetStudio
{
	// Token: 0x020000B5 RID: 181
	public class GenericBinding
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00002739 File Offset: 0x00000939
		public GenericBinding()
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000D664 File Offset: 0x0000B864
		public GenericBinding(ObjectReader reader)
		{
			int[] version = reader.version;
			this.path = reader.ReadUInt32();
			this.attribute = reader.ReadUInt32();
			this.script = new PPtr<Object>(reader);
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 6))
			{
				this.typeID = (ClassIDType)reader.ReadInt32();
			}
			else
			{
				this.typeID = (ClassIDType)reader.ReadUInt16();
			}
			this.customType = reader.ReadByte();
			this.isPPtrCurve = reader.ReadByte();
			if (version[0] > 2022 || (version[0] == 2022 && version[1] >= 1))
			{
				this.isIntCurve = reader.ReadByte();
			}
			reader.AlignStream();
		}

		// Token: 0x040005B6 RID: 1462
		public uint path;

		// Token: 0x040005B7 RID: 1463
		public uint attribute;

		// Token: 0x040005B8 RID: 1464
		public PPtr<Object> script;

		// Token: 0x040005B9 RID: 1465
		public ClassIDType typeID;

		// Token: 0x040005BA RID: 1466
		public byte customType;

		// Token: 0x040005BB RID: 1467
		public byte isPPtrCurve;

		// Token: 0x040005BC RID: 1468
		public byte isIntCurve;
	}
}
