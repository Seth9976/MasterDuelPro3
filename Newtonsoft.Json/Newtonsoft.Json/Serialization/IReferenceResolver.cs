using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200010E RID: 270
	[NullableContext(1)]
	public interface IReferenceResolver
	{
		// Token: 0x060007C2 RID: 1986
		object ResolveReference(object context, string reference);

		// Token: 0x060007C3 RID: 1987
		string GetReference(object context, object value);

		// Token: 0x060007C4 RID: 1988
		bool IsReferenced(object context, object value);

		// Token: 0x060007C5 RID: 1989
		void AddReference(object context, string reference, object value);
	}
}
