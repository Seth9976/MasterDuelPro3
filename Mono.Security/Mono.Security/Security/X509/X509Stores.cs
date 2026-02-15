using System;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x02000022 RID: 34
	public class X509Stores
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00008C45 File Offset: 0x00006E45
		internal X509Stores(string path, bool newFormat)
		{
			this._storePath = path;
			this._newFormat = newFormat;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00008C5C File Offset: 0x00006E5C
		public X509Store TrustedRoot
		{
			get
			{
				if (this._trusted == null)
				{
					string text = Path.Combine(this._storePath, "Trust");
					this._trusted = new X509Store(text, true, this._newFormat);
				}
				return this._trusted;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00008C9C File Offset: 0x00006E9C
		public X509Store Open(string storeName, bool create)
		{
			if (storeName == null)
			{
				throw new ArgumentNullException("storeName");
			}
			string text = Path.Combine(this._storePath, storeName);
			if (!create && !Directory.Exists(text))
			{
				return null;
			}
			return new X509Store(text, true, false);
		}

		// Token: 0x0400008F RID: 143
		private string _storePath;

		// Token: 0x04000090 RID: 144
		private bool _newFormat;

		// Token: 0x04000091 RID: 145
		private X509Store _trusted;
	}
}
