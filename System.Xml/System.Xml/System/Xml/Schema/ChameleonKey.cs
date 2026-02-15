using System;

namespace System.Xml.Schema
{
	// Token: 0x02000210 RID: 528
	internal class ChameleonKey
	{
		// Token: 0x06001A5B RID: 6747 RVA: 0x000998DB File Offset: 0x00097ADB
		public ChameleonKey(string ns, XmlSchema originalSchema)
		{
			this.targetNS = ns;
			this.chameleonLocation = originalSchema.BaseUri;
			if (this.chameleonLocation.OriginalString.Length == 0)
			{
				this.originalSchema = originalSchema;
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00099910 File Offset: 0x00097B10
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = this.targetNS.GetHashCode() + this.chameleonLocation.GetHashCode() + ((this.originalSchema == null) ? 0 : this.originalSchema.GetHashCode());
			}
			return this.hashCode;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00099960 File Offset: 0x00097B60
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			ChameleonKey chameleonKey = obj as ChameleonKey;
			return chameleonKey != null && (this.targetNS.Equals(chameleonKey.targetNS) && this.chameleonLocation.Equals(chameleonKey.chameleonLocation)) && this.originalSchema == chameleonKey.originalSchema;
		}

		// Token: 0x04000B32 RID: 2866
		internal string targetNS;

		// Token: 0x04000B33 RID: 2867
		internal Uri chameleonLocation;

		// Token: 0x04000B34 RID: 2868
		internal XmlSchema originalSchema;

		// Token: 0x04000B35 RID: 2869
		private int hashCode;
	}
}
