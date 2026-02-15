using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000155 RID: 341
	internal class TempAssemblyCacheKey
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x00052717 File Offset: 0x00050917
		internal TempAssemblyCacheKey(string ns, object type)
		{
			this.type = type;
			this.ns = ns;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00052730 File Offset: 0x00050930
		public override bool Equals(object o)
		{
			TempAssemblyCacheKey tempAssemblyCacheKey = o as TempAssemblyCacheKey;
			return tempAssemblyCacheKey != null && tempAssemblyCacheKey.type == this.type && tempAssemblyCacheKey.ns == this.ns;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0005276A File Offset: 0x0005096A
		public override int GetHashCode()
		{
			return ((this.ns != null) ? this.ns.GetHashCode() : 0) ^ ((this.type != null) ? this.type.GetHashCode() : 0);
		}

		// Token: 0x04000819 RID: 2073
		private string ns;

		// Token: 0x0400081A RID: 2074
		private object type;
	}
}
