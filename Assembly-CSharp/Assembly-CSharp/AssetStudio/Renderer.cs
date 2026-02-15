using System;

namespace AssetStudio
{
	// Token: 0x0200010B RID: 267
	public abstract class Renderer : Component
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00011F40 File Offset: 0x00010140
		protected Renderer(ObjectReader reader)
			: base(reader)
		{
			if (this.version[0] < 5)
			{
				reader.ReadBoolean();
				reader.ReadBoolean();
				reader.ReadBoolean();
				reader.ReadByte();
			}
			else
			{
				if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 4))
				{
					reader.ReadBoolean();
					reader.ReadByte();
					reader.ReadByte();
					if (this.version[0] > 2017 || (this.version[0] == 2017 && this.version[1] >= 2))
					{
						reader.ReadByte();
					}
					if (this.version[0] >= 2021)
					{
						reader.ReadByte();
					}
					reader.ReadByte();
					reader.ReadByte();
					reader.ReadByte();
					if (this.version[0] > 2019 || (this.version[0] == 2019 && this.version[1] >= 3))
					{
						reader.ReadByte();
					}
					if (this.version[0] >= 2020)
					{
						reader.ReadByte();
					}
					reader.AlignStream();
				}
				else
				{
					reader.ReadBoolean();
					reader.AlignStream();
					reader.ReadByte();
					reader.ReadBoolean();
					reader.AlignStream();
				}
				if (this.version[0] >= 2018)
				{
					reader.ReadUInt32();
				}
				if (this.version[0] > 2018 || (this.version[0] == 2018 && this.version[1] >= 3))
				{
					reader.ReadInt32();
				}
				reader.ReadUInt16();
				reader.ReadUInt16();
			}
			if (this.version[0] >= 3)
			{
				reader.ReadVector4();
			}
			if (this.version[0] >= 5)
			{
				reader.ReadVector4();
			}
			int m_MaterialsSize = reader.ReadInt32();
			this.m_Materials = new PPtr<Material>[m_MaterialsSize];
			for (int i = 0; i < m_MaterialsSize; i++)
			{
				this.m_Materials[i] = new PPtr<Material>(reader);
			}
			if (this.version[0] < 3)
			{
				reader.ReadVector4();
			}
			else
			{
				if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 5))
				{
					this.m_StaticBatchInfo = new StaticBatchInfo(reader);
				}
				else
				{
					this.m_SubsetIndices = reader.ReadUInt32Array();
				}
				new PPtr<Transform>(reader);
			}
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 4))
			{
				new PPtr<Transform>(reader);
				new PPtr<GameObject>(reader);
			}
			else if (this.version[0] > 3 || (this.version[0] == 3 && this.version[1] >= 5))
			{
				reader.ReadBoolean();
				reader.AlignStream();
				if (this.version[0] >= 5)
				{
					reader.ReadInt32();
				}
				new PPtr<Transform>(reader);
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				if (this.version[0] == 4 && this.version[1] == 3)
				{
					reader.ReadInt16();
				}
				else
				{
					reader.ReadUInt32();
				}
				reader.ReadInt16();
				reader.AlignStream();
			}
		}

		// Token: 0x04000774 RID: 1908
		public PPtr<Material>[] m_Materials;

		// Token: 0x04000775 RID: 1909
		public StaticBatchInfo m_StaticBatchInfo;

		// Token: 0x04000776 RID: 1910
		public uint[] m_SubsetIndices;
	}
}
