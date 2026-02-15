using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000173 RID: 371
	internal abstract class TypeModel
	{
		// Token: 0x060011D5 RID: 4565 RVA: 0x0005583B File Offset: 0x00053A3B
		protected TypeModel(Type type, TypeDesc typeDesc, ModelScope scope)
		{
			this.scope = scope;
			this.type = type;
			this.typeDesc = typeDesc;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00055858 File Offset: 0x00053A58
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00055860 File Offset: 0x00053A60
		internal ModelScope ModelScope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00055868 File Offset: 0x00053A68
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
		}

		// Token: 0x04000884 RID: 2180
		private TypeDesc typeDesc;

		// Token: 0x04000885 RID: 2181
		private Type type;

		// Token: 0x04000886 RID: 2182
		private ModelScope scope;
	}
}
