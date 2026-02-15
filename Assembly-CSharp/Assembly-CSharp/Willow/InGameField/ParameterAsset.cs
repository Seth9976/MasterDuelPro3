using System;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x02001571 RID: 5489
	[Serializable]
	public class ParameterAsset : ScriptableObject
	{
		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06009F1B RID: 40731 RVA: 0x0019B6CE File Offset: 0x001998CE
		// (set) Token: 0x06009F1C RID: 40732 RVA: 0x0019B6D6 File Offset: 0x001998D6
		public string type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				if (this.m_type != value)
				{
					this.m_type = value;
				}
			}
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06009F1D RID: 40733 RVA: 0x0019B6ED File Offset: 0x001998ED
		// (set) Token: 0x06009F1E RID: 40734 RVA: 0x0000216D File Offset: 0x0000036D
		public ParameterContainer parent
		{
			get
			{
				return this.m_parentContainer;
			}
			set
			{
			}
		}

		// Token: 0x0400DE59 RID: 56921
		[SerializeField]
		protected ParameterContainer m_parentContainer;

		// Token: 0x0400DE5A RID: 56922
		[SerializeField]
		protected string m_type;
	}
}
