using System;
using System.ComponentModel.Design;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000045 RID: 69
	internal class TaskDialogDesigner : ComponentDesigner
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007D4C File Offset: 0x00005F4C
		public override DesignerVerbCollection Verbs
		{
			get
			{
				DesignerVerbCollection designerVerbCollection = new DesignerVerbCollection();
				designerVerbCollection.Add(new DesignerVerb(Resources.Preview, new EventHandler(this.Preview)));
				return designerVerbCollection;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007D82 File Offset: 0x00005F82
		private void Preview(object sender, EventArgs e)
		{
			((TaskDialog)base.Component).ShowDialog();
		}
	}
}
