using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000006 RID: 6
	internal class ConfigNameValueCollection : NameValueCollection
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002131 File Offset: 0x00000331
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002139 File Offset: 0x00000339
		public ConfigNameValueCollection(ConfigNameValueCollection col)
			: base(col.Count, col)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002151 File Offset: 0x00000351
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002159 File Offset: 0x00000359
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x04000004 RID: 4
		private bool modified;
	}
}
