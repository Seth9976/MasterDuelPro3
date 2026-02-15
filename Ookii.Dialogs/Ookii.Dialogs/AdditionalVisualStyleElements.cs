using System;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	// Token: 0x02000002 RID: 2
	public static class AdditionalVisualStyleElements
	{
		// Token: 0x02000003 RID: 3
		public static class TextStyle
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
			public static VisualStyleElement MainInstruction
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TextStyle._mainInstruction) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TextStyle._mainInstruction = VisualStyleElement.CreateElement("TEXTSTYLE", 1, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000002 RID: 2 RVA: 0x00002080 File Offset: 0x00000280
			public static VisualStyleElement BodyText
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TextStyle._bodyText) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TextStyle._bodyText = VisualStyleElement.CreateElement("TEXTSTYLE", 4, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x04000001 RID: 1
			private const string _className = "TEXTSTYLE";

			// Token: 0x04000002 RID: 2
			private static VisualStyleElement _mainInstruction;

			// Token: 0x04000003 RID: 3
			private static VisualStyleElement _bodyText;
		}

		// Token: 0x02000004 RID: 4
		public static class TaskDialog
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000003 RID: 3 RVA: 0x000020B0 File Offset: 0x000002B0
			public static VisualStyleElement PrimaryPanel
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TaskDialog._primaryPanel) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TaskDialog._primaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 1, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E0 File Offset: 0x000002E0
			public static VisualStyleElement SecondaryPanel
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TaskDialog._secondaryPanel) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TaskDialog._secondaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 8, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x04000004 RID: 4
			private const string _className = "TASKDIALOG";

			// Token: 0x04000005 RID: 5
			private static VisualStyleElement _primaryPanel;

			// Token: 0x04000006 RID: 6
			private static VisualStyleElement _secondaryPanel;
		}
	}
}
