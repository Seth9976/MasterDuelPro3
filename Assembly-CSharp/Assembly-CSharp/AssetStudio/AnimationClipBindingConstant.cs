using System;

namespace AssetStudio
{
	// Token: 0x020000B6 RID: 182
	public class AnimationClipBindingConstant
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00002739 File Offset: 0x00000939
		public AnimationClipBindingConstant()
		{
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000D714 File Offset: 0x0000B914
		public AnimationClipBindingConstant(ObjectReader reader)
		{
			int numBindings = reader.ReadInt32();
			this.genericBindings = new GenericBinding[numBindings];
			for (int i = 0; i < numBindings; i++)
			{
				this.genericBindings[i] = new GenericBinding(reader);
			}
			int numMappings = reader.ReadInt32();
			this.pptrCurveMapping = new PPtr<Object>[numMappings];
			for (int j = 0; j < numMappings; j++)
			{
				this.pptrCurveMapping[j] = new PPtr<Object>(reader);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000D784 File Offset: 0x0000B984
		public GenericBinding FindBinding(int index)
		{
			int curves = 0;
			foreach (GenericBinding b in this.genericBindings)
			{
				if (b.typeID == ClassIDType.Transform)
				{
					switch (b.attribute)
					{
					case 1U:
					case 3U:
					case 4U:
						curves += 3;
						break;
					case 2U:
						curves += 4;
						break;
					default:
						curves++;
						break;
					}
				}
				else
				{
					curves++;
				}
				if (curves > index)
				{
					return b;
				}
			}
			return null;
		}

		// Token: 0x040005BD RID: 1469
		public GenericBinding[] genericBindings;

		// Token: 0x040005BE RID: 1470
		public PPtr<Object>[] pptrCurveMapping;
	}
}
