using System;

namespace AssetStudio
{
	// Token: 0x0200013F RID: 319
	public abstract class Texture : NamedObject
	{
		// Token: 0x06000396 RID: 918 RVA: 0x00013D10 File Offset: 0x00011F10
		protected Texture(ObjectReader reader)
			: base(reader)
		{
			if (this.version[0] > 2017 || (this.version[0] == 2017 && this.version[1] >= 3))
			{
				reader.ReadInt32();
				reader.ReadBoolean();
				if (this.version[0] > 2020 || (this.version[0] == 2020 && this.version[1] >= 2))
				{
					reader.ReadBoolean();
				}
				reader.AlignStream();
			}
		}
	}
}
