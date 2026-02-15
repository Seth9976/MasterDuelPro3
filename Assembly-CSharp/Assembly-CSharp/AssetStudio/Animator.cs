using System;

namespace AssetStudio
{
	// Token: 0x020000BA RID: 186
	public sealed class Animator : Behaviour
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		public Animator(ObjectReader reader)
			: base(reader)
		{
			this.m_Avatar = new PPtr<Avatar>(reader);
			this.m_Controller = new PPtr<RuntimeAnimatorController>(reader);
			reader.ReadInt32();
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 5))
			{
				reader.ReadInt32();
			}
			reader.ReadBoolean();
			if (this.version[0] == 4 && this.version[1] >= 5)
			{
				reader.AlignStream();
			}
			if (this.version[0] >= 5)
			{
				reader.ReadBoolean();
				if (this.version[0] > 2021 || (this.version[0] == 2021 && this.version[1] >= 2))
				{
					reader.ReadBoolean();
				}
				reader.AlignStream();
			}
			if (this.version[0] < 4 || (this.version[0] == 4 && this.version[1] < 5))
			{
				reader.ReadBoolean();
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				this.m_HasTransformHierarchy = reader.ReadBoolean();
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 5))
			{
				reader.ReadBoolean();
			}
			if (this.version[0] >= 5 && this.version[0] < 2018)
			{
				reader.AlignStream();
			}
			if (this.version[0] >= 2018)
			{
				reader.ReadBoolean();
				reader.AlignStream();
			}
		}

		// Token: 0x040005DC RID: 1500
		public PPtr<Avatar> m_Avatar;

		// Token: 0x040005DD RID: 1501
		public PPtr<RuntimeAnimatorController> m_Controller;

		// Token: 0x040005DE RID: 1502
		public bool m_HasTransformHierarchy = true;
	}
}
