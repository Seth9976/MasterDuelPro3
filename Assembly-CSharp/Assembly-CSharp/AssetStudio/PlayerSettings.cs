using System;

namespace AssetStudio
{
	// Token: 0x02000108 RID: 264
	public sealed class PlayerSettings : Object
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00011E0C File Offset: 0x0001000C
		public PlayerSettings(ObjectReader reader)
			: base(reader)
		{
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 4))
			{
				reader.ReadBytes(16);
			}
			reader.ReadBoolean();
			reader.AlignStream();
			reader.ReadInt32();
			reader.ReadInt32();
			if (this.version[0] < 5 || (this.version[0] == 5 && this.version[1] < 3))
			{
				if (this.version[0] < 5)
				{
					reader.ReadInt32();
					if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 6))
					{
						reader.ReadInt32();
					}
				}
				reader.ReadInt32();
			}
			else
			{
				reader.ReadBoolean();
				reader.AlignStream();
			}
			if (this.version[0] > 3 || (this.version[0] == 3 && this.version[1] >= 5))
			{
				reader.ReadInt32();
			}
			this.companyName = reader.ReadAlignedString();
			this.productName = reader.ReadAlignedString();
		}

		// Token: 0x04000770 RID: 1904
		public string companyName;

		// Token: 0x04000771 RID: 1905
		public string productName;
	}
}
