using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000172 RID: 370
	internal class ModelScope
	{
		// Token: 0x060011D0 RID: 4560 RVA: 0x000556DD File Offset: 0x000538DD
		internal ModelScope(TypeScope typeScope)
		{
			this.typeScope = typeScope;
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00055702 File Offset: 0x00053902
		internal TypeScope TypeScope
		{
			get
			{
				return this.typeScope;
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0005570A File Offset: 0x0005390A
		internal TypeModel GetTypeModel(Type type)
		{
			return this.GetTypeModel(type, true);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00055714 File Offset: 0x00053914
		internal TypeModel GetTypeModel(Type type, bool directReference)
		{
			TypeModel typeModel = (TypeModel)this.models[type];
			if (typeModel != null)
			{
				return typeModel;
			}
			TypeDesc typeDesc = this.typeScope.GetTypeDesc(type, null, directReference);
			switch (typeDesc.Kind)
			{
			case TypeKind.Root:
			case TypeKind.Struct:
			case TypeKind.Class:
				typeModel = new StructModel(type, typeDesc, this);
				break;
			case TypeKind.Primitive:
				typeModel = new PrimitiveModel(type, typeDesc, this);
				break;
			case TypeKind.Enum:
				typeModel = new EnumModel(type, typeDesc, this);
				break;
			case TypeKind.Array:
			case TypeKind.Collection:
			case TypeKind.Enumerable:
				typeModel = new ArrayModel(type, typeDesc, this);
				break;
			default:
				if (!typeDesc.IsSpecial)
				{
					throw new NotSupportedException(Res.GetString("The type {0} may not be serialized.", new object[] { type.FullName }));
				}
				typeModel = new SpecialModel(type, typeDesc, this);
				break;
			}
			this.models.Add(type, typeModel);
			return typeModel;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000557E0 File Offset: 0x000539E0
		internal ArrayModel GetArrayModel(Type type)
		{
			TypeModel typeModel = (TypeModel)this.arrayModels[type];
			if (typeModel == null)
			{
				typeModel = this.GetTypeModel(type);
				if (!(typeModel is ArrayModel))
				{
					TypeDesc arrayTypeDesc = this.typeScope.GetArrayTypeDesc(type);
					typeModel = new ArrayModel(type, arrayTypeDesc, this);
				}
				this.arrayModels.Add(type, typeModel);
			}
			return (ArrayModel)typeModel;
		}

		// Token: 0x04000881 RID: 2177
		private TypeScope typeScope;

		// Token: 0x04000882 RID: 2178
		private Hashtable models = new Hashtable();

		// Token: 0x04000883 RID: 2179
		private Hashtable arrayModels = new Hashtable();
	}
}
