using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200015E RID: 350
	internal class ElementAccessor : Accessor
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00053E32 File Offset: 0x00052032
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00053E3A File Offset: 0x0005203A
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00053E43 File Offset: 0x00052043
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00053E4B File Offset: 0x0005204B
		internal bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00053E54 File Offset: 0x00052054
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00053E5C File Offset: 0x0005205C
		internal bool IsUnbounded
		{
			get
			{
				return this.unbounded;
			}
			set
			{
				this.unbounded = value;
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00053E68 File Offset: 0x00052068
		internal ElementAccessor Clone()
		{
			return new ElementAccessor
			{
				nullable = this.nullable,
				IsTopLevelInSchema = base.IsTopLevelInSchema,
				Form = base.Form,
				isSoap = this.isSoap,
				Name = this.Name,
				Default = base.Default,
				Namespace = base.Namespace,
				Mapping = base.Mapping,
				Any = base.Any
			};
		}

		// Token: 0x04000837 RID: 2103
		private bool nullable;

		// Token: 0x04000838 RID: 2104
		private bool isSoap;

		// Token: 0x04000839 RID: 2105
		private bool unbounded;
	}
}
