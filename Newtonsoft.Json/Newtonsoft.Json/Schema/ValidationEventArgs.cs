using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000153 RID: 339
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00032A91 File Offset: 0x00030C91
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00032AAB File Offset: 0x00030CAB
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00032AB3 File Offset: 0x00030CB3
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00032AC0 File Offset: 0x00030CC0
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x04000656 RID: 1622
		private readonly JsonSchemaException _ex;
	}
}
