using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Ookii.Dialogs.Properties
{
	// Token: 0x02000051 RID: 81
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00009D78 File Offset: 0x00007F78
		internal Resources()
		{
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009D84 File Offset: 0x00007F84
		[EditorBrowsable(2)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Ookii.Dialogs.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00009DCC File Offset: 0x00007FCC
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00009DE3 File Offset: 0x00007FE3
		[EditorBrowsable(2)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00009DEC File Offset: 0x00007FEC
		internal static string AnimationLoadErrorFormat
		{
			get
			{
				return Resources.ResourceManager.GetString("AnimationLoadErrorFormat", Resources.resourceCulture);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00009E14 File Offset: 0x00008014
		internal static string CredentialEmptyTargetError
		{
			get
			{
				return Resources.ResourceManager.GetString("CredentialEmptyTargetError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009E3C File Offset: 0x0000803C
		internal static string CredentialError
		{
			get
			{
				return Resources.ResourceManager.GetString("CredentialError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009E64 File Offset: 0x00008064
		internal static string CredentialPromptNotCalled
		{
			get
			{
				return Resources.ResourceManager.GetString("CredentialPromptNotCalled", Resources.resourceCulture);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00009E8C File Offset: 0x0000808C
		internal static string DuplicateButtonTypeError
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateButtonTypeError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009EB4 File Offset: 0x000080B4
		internal static string DuplicateItemIdError
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateItemIdError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009EDC File Offset: 0x000080DC
		internal static string FileNotFoundFormat
		{
			get
			{
				return Resources.ResourceManager.GetString("FileNotFoundFormat", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009F04 File Offset: 0x00008104
		internal static string GlassNotSupportedError
		{
			get
			{
				return Resources.ResourceManager.GetString("GlassNotSupportedError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009F2C File Offset: 0x0000812C
		internal static string Help
		{
			get
			{
				return Resources.ResourceManager.GetString("Help", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009F54 File Offset: 0x00008154
		internal static string InvalidFilterString
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidFilterString", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009F7C File Offset: 0x0000817C
		internal static string InvalidTaskDialogItemIdError
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidTaskDialogItemIdError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00009FA4 File Offset: 0x000081A4
		internal static string NoAssociatedTaskDialogError
		{
			get
			{
				return Resources.ResourceManager.GetString("NoAssociatedTaskDialogError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00009FCC File Offset: 0x000081CC
		internal static string NonCustomTaskDialogButtonIdError
		{
			get
			{
				return Resources.ResourceManager.GetString("NonCustomTaskDialogButtonIdError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00009FF4 File Offset: 0x000081F4
		internal static string Preview
		{
			get
			{
				return Resources.ResourceManager.GetString("Preview", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A01C File Offset: 0x0000821C
		internal static string ProgressDialogNotRunningError
		{
			get
			{
				return Resources.ResourceManager.GetString("ProgressDialogNotRunningError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A044 File Offset: 0x00008244
		internal static string ProgressDialogRunning
		{
			get
			{
				return Resources.ResourceManager.GetString("ProgressDialogRunning", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A06C File Offset: 0x0000826C
		internal static string TaskDialogEmptyButtonLabelError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogEmptyButtonLabelError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A094 File Offset: 0x00008294
		internal static string TaskDialogIllegalCrossThreadCallError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogIllegalCrossThreadCallError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A0BC File Offset: 0x000082BC
		internal static string TaskDialogItemHasOwnerError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogItemHasOwnerError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A0E4 File Offset: 0x000082E4
		internal static string TaskDialogNoButtonsError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogNoButtonsError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A10C File Offset: 0x0000830C
		internal static string TaskDialogNotRunningError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogNotRunningError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A134 File Offset: 0x00008334
		internal static string TaskDialogRunningError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogRunningError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A15C File Offset: 0x0000835C
		internal static string TaskDialogsNotSupportedError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogsNotSupportedError", Resources.resourceCulture);
			}
		}

		// Token: 0x04000214 RID: 532
		private static ResourceManager resourceMan;

		// Token: 0x04000215 RID: 533
		private static CultureInfo resourceCulture;
	}
}
