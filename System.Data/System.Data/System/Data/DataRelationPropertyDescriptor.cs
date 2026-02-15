using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200003F RID: 63
	internal sealed class DataRelationPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x00016725 File Offset: 0x00014925
		internal DataRelationPropertyDescriptor(DataRelation dataRelation)
			: base(dataRelation.RelationName, null)
		{
			this.Relation = dataRelation;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001673B File Offset: 0x0001493B
		internal DataRelation Relation { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001444A File Offset: 0x0001264A
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00016743 File Offset: 0x00014943
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001674F File Offset: 0x0001494F
		public override bool Equals(object other)
		{
			return other is DataRelationPropertyDescriptor && ((DataRelationPropertyDescriptor)other).Relation == this.Relation;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001676E File Offset: 0x0001496E
		public override int GetHashCode()
		{
			return this.Relation.GetHashCode();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001677B File Offset: 0x0001497B
		public override object GetValue(object component)
		{
			return ((DataRowView)component).CreateChildView(this.Relation);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00003FD2 File Offset: 0x000021D2
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00003FD2 File Offset: 0x000021D2
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
