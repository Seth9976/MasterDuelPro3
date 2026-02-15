using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200001D RID: 29
	internal interface IMemberInfo
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006A RID: 106
		string Name { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107
		bool IsReadOnly { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108
		Type ValueType { get; }

		// Token: 0x0600006D RID: 109
		object GetValue(object obj);

		// Token: 0x0600006E RID: 110
		void SetValue(object obj, object value);

		// Token: 0x0600006F RID: 111
		IEnumerable<Attribute> GetCustomAttributes();
	}
}
