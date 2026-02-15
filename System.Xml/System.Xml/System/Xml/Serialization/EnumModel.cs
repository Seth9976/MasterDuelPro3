using System;
using System.Collections;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200017B RID: 379
	internal class EnumModel : TypeModel
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x00055870 File Offset: 0x00053A70
		internal EnumModel(Type type, TypeDesc typeDesc, ModelScope scope)
			: base(type, typeDesc, scope)
		{
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00055E04 File Offset: 0x00054004
		internal ConstantModel[] Constants
		{
			get
			{
				if (this.constants == null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (FieldInfo fieldInfo in base.Type.GetFields())
					{
						ConstantModel constantModel = this.GetConstantModel(fieldInfo);
						if (constantModel != null)
						{
							arrayList.Add(constantModel);
						}
					}
					this.constants = (ConstantModel[])arrayList.ToArray(typeof(ConstantModel));
				}
				return this.constants;
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00055E74 File Offset: 0x00054074
		private ConstantModel GetConstantModel(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsSpecialName)
			{
				return null;
			}
			return new ConstantModel(fieldInfo, ((IConvertible)fieldInfo.GetValue(null)).ToInt64(null));
		}

		// Token: 0x04000897 RID: 2199
		private ConstantModel[] constants;
	}
}
