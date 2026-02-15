using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x020000B2 RID: 178
	public class Clip
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		public Clip(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_StreamedClip = new StreamedClip(reader);
			this.m_DenseClip = new DenseClip(reader);
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				this.m_ConstantClip = new ConstantClip(reader);
			}
			if (version[0] < 2018 || (version[0] == 2018 && version[1] < 3))
			{
				this.m_Binding = new ValueArrayConstant(reader);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000D318 File Offset: 0x0000B518
		public AnimationClipBindingConstant ConvertValueArrayToGenericBinding()
		{
			AnimationClipBindingConstant bindings = new AnimationClipBindingConstant();
			List<GenericBinding> genericBindings = new List<GenericBinding>();
			ValueArrayConstant values = this.m_Binding;
			int i = 0;
			while (i < values.m_ValueArray.Length)
			{
				uint curveID = values.m_ValueArray[i].m_ID;
				uint curveTypeID = values.m_ValueArray[i].m_TypeID;
				GenericBinding binding = new GenericBinding();
				genericBindings.Add(binding);
				if (curveTypeID == 4174552735U)
				{
					binding.path = curveID;
					binding.attribute = 1U;
					binding.typeID = ClassIDType.Transform;
					i += 3;
				}
				else if (curveTypeID == 2211994246U)
				{
					binding.path = curveID;
					binding.attribute = 2U;
					binding.typeID = ClassIDType.Transform;
					i += 4;
				}
				else if (curveTypeID == 1512518241U)
				{
					binding.path = curveID;
					binding.attribute = 3U;
					binding.typeID = ClassIDType.Transform;
					i += 3;
				}
				else
				{
					binding.typeID = ClassIDType.Animator;
					binding.path = 0U;
					binding.attribute = curveID;
					i++;
				}
			}
			bindings.genericBindings = genericBindings.ToArray();
			return bindings;
		}

		// Token: 0x04000593 RID: 1427
		public StreamedClip m_StreamedClip;

		// Token: 0x04000594 RID: 1428
		public DenseClip m_DenseClip;

		// Token: 0x04000595 RID: 1429
		public ConstantClip m_ConstantClip;

		// Token: 0x04000596 RID: 1430
		public ValueArrayConstant m_Binding;
	}
}
