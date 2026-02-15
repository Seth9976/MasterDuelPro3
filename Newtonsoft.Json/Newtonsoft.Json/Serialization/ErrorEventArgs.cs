using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200010B RID: 267
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00026ABB File Offset: 0x00024CBB
		[Nullable(2)]
		public object CurrentObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00026AC3 File Offset: 0x00024CC3
		public ErrorContext ErrorContext { get; }

		// Token: 0x060007BE RID: 1982 RVA: 0x00026ACB File Offset: 0x00024CCB
		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
