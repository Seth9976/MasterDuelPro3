using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200016E RID: 366
	[DefaultMember("Item")]
	internal class RelatedCurrencyManager : CurrencyManager
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x0003F21A File Offset: 0x0003D41A
		public RelatedCurrencyManager(BindingManagerBase parent, PropertyDescriptor prop_desc)
			: base(prop_desc.GetValue(parent.Current))
		{
			this.parent = parent;
			this.prop_desc = prop_desc;
			parent.PositionChanged += this.parent_PositionChanged;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003F24E File Offset: 0x0003D44E
		private void parent_PositionChanged(object sender, EventArgs args)
		{
			base.SetDataSource(this.prop_desc.GetValue(this.parent.Current));
		}

		// Token: 0x040008E9 RID: 2281
		private BindingManagerBase parent;

		// Token: 0x040008EA RID: 2282
		private PropertyDescriptor prop_desc;
	}
}
