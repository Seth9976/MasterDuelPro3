using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200017C RID: 380
	internal class NameKey
	{
		// Token: 0x060011F8 RID: 4600 RVA: 0x00055E98 File Offset: 0x00054098
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00055EB0 File Offset: 0x000540B0
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00055EF4 File Offset: 0x000540F4
		public override int GetHashCode()
		{
			return ((this.ns == null) ? "<null>".GetHashCode() : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x04000898 RID: 2200
		private string ns;

		// Token: 0x04000899 RID: 2201
		private string name;
	}
}
