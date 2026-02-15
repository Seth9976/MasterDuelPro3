using System;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class AssetReferenceUILabelRestriction : AssetReferenceUIRestriction
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020FF File Offset: 0x000002FF
		public AssetReferenceUILabelRestriction(params string[] allowedLabels)
		{
			this.m_AllowedLabels = allowedLabels;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020F4 File Offset: 0x000002F4
		public override bool ValidateAsset(Object obj)
		{
			return true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020F4 File Offset: 0x000002F4
		public override bool ValidateAsset(string path)
		{
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002110 File Offset: 0x00000310
		public override string ToString()
		{
			if (this.m_CachedToString == null)
			{
				StringBuilder sb = new StringBuilder();
				bool first = true;
				foreach (string t in this.m_AllowedLabels)
				{
					if (!first)
					{
						sb.Append(',');
					}
					first = false;
					sb.Append(t);
				}
				this.m_CachedToString = sb.ToString();
			}
			return this.m_CachedToString;
		}

		// Token: 0x04000009 RID: 9
		public string[] m_AllowedLabels;

		// Token: 0x0400000A RID: 10
		public string m_CachedToString;
	}
}
