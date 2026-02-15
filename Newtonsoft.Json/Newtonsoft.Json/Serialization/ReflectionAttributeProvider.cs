using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000137 RID: 311
	[NullableContext(1)]
	[Nullable(0)]
	public class ReflectionAttributeProvider : IAttributeProvider
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x0002EB41 File Offset: 0x0002CD41
		public ReflectionAttributeProvider(object attributeProvider)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002EB5B File Offset: 0x0002CD5B
		public IList<Attribute> GetAttributes(bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, null, inherit);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002EB6A File Offset: 0x0002CD6A
		public IList<Attribute> GetAttributes(Type attributeType, bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, attributeType, inherit);
		}

		// Token: 0x040005BC RID: 1468
		private readonly object _attributeProvider;
	}
}
