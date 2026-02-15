using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000170 RID: 368
	internal class RelatedPropertyManager : PropertyManager
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x0003F27B File Offset: 0x0003D47B
		public RelatedPropertyManager(BindingManagerBase parent, string property_name)
		{
			this.parent = parent;
			this.property_name = property_name;
			if (parent.Position != -1)
			{
				base.SetDataSource(parent.Current);
			}
			parent.PositionChanged += this.parent_PositionChanged;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003F2B8 File Offset: 0x0003D4B8
		private void parent_PositionChanged(object sender, EventArgs args)
		{
			if (this.parent.Position == -1)
			{
				base.SetDataSource(null);
			}
			else
			{
				base.SetDataSource(this.parent.Current);
			}
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003F2ED File Offset: 0x0003D4ED
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return TypeDescriptor.GetProperties(this.parent.GetItemProperties().Find(this.property_name, true).GetValue(this.parent.Current));
		}

		// Token: 0x040008EC RID: 2284
		private BindingManagerBase parent;
	}
}
