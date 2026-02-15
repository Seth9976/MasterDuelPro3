using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200017C RID: 380
	public class JsonCloneSettings
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00037FA6 File Offset: 0x000361A6
		public JsonCloneSettings()
		{
			this.CopyAnnotations = true;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00037FB5 File Offset: 0x000361B5
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x00037FBD File Offset: 0x000361BD
		public bool CopyAnnotations { get; set; }

		// Token: 0x040006FA RID: 1786
		[Nullable(1)]
		internal static readonly JsonCloneSettings SkipCopyAnnotations = new JsonCloneSettings
		{
			CopyAnnotations = false
		};
	}
}
