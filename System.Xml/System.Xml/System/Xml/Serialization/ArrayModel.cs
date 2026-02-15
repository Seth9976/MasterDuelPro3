using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000174 RID: 372
	internal class ArrayModel : TypeModel
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00055870 File Offset: 0x00053A70
		internal ArrayModel(Type type, TypeDesc typeDesc, ModelScope scope)
			: base(type, typeDesc, scope)
		{
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0005587B File Offset: 0x00053A7B
		internal TypeModel Element
		{
			get
			{
				return base.ModelScope.GetTypeModel(TypeScope.GetArrayElementType(base.Type, null));
			}
		}
	}
}
