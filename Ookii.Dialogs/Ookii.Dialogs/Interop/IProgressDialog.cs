using System;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200005B RID: 91
	[Guid("EBBC7C04-315E-11d2-B62F-006097DF5BD4")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IProgressDialog
	{
		// Token: 0x06000254 RID: 596
		[PreserveSig]
		void StartProgressDialog(IntPtr hwndParent, [MarshalAs(25)] object punkEnableModless, ProgressDialogFlags dwFlags, IntPtr pvResevered);

		// Token: 0x06000255 RID: 597
		[PreserveSig]
		void StopProgressDialog();

		// Token: 0x06000256 RID: 598
		[PreserveSig]
		void SetTitle([MarshalAs(21)] string pwzTitle);

		// Token: 0x06000257 RID: 599
		[PreserveSig]
		void SetAnimation(SafeModuleHandle hInstAnimation, ushort idAnimation);

		// Token: 0x06000258 RID: 600
		[PreserveSig]
		[return: MarshalAs(2)]
		bool HasUserCancelled();

		// Token: 0x06000259 RID: 601
		[PreserveSig]
		void SetProgress(uint dwCompleted, uint dwTotal);

		// Token: 0x0600025A RID: 602
		[PreserveSig]
		void SetProgress64(ulong ullCompleted, ulong ullTotal);

		// Token: 0x0600025B RID: 603
		[PreserveSig]
		void SetLine(uint dwLineNum, [MarshalAs(21)] string pwzString, [MarshalAs(37)] bool fCompactPath, IntPtr pvResevered);

		// Token: 0x0600025C RID: 604
		[PreserveSig]
		void SetCancelMsg([MarshalAs(21)] string pwzCancelMsg, object pvResevered);

		// Token: 0x0600025D RID: 605
		[PreserveSig]
		void Timer(uint dwTimerAction, object pvResevered);
	}
}
