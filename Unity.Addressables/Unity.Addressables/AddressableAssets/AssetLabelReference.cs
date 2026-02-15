using System;
using UnityEngine.Serialization;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class AssetLabelReference : IKeyEvaluator
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006046 File Offset: 0x00004246
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000604E File Offset: 0x0000424E
		public string labelString
		{
			get
			{
				return this.m_LabelString;
			}
			set
			{
				this.m_LabelString = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006057 File Offset: 0x00004257
		public object RuntimeKey
		{
			get
			{
				if (this.labelString == null)
				{
					this.labelString = string.Empty;
				}
				return this.labelString;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006072 File Offset: 0x00004272
		public bool RuntimeKeyIsValid()
		{
			return !string.IsNullOrEmpty(this.RuntimeKey.ToString());
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006087 File Offset: 0x00004287
		public override int GetHashCode()
		{
			return this.labelString.GetHashCode();
		}

		// Token: 0x040000B0 RID: 176
		[FormerlySerializedAs("m_labelString")]
		[SerializeField]
		private string m_LabelString;
	}
}
