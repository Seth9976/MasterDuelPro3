using System;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000069 RID: 105
	[Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
	[CoClass(typeof(FileOpenDialogRCW))]
	[ComImport]
	internal interface NativeFileOpenDialog : IFileOpenDialog, IFileDialog, IModalWindow
	{
	}
}
