using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x02000012 RID: 18
	internal static class NativeMethods
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public static bool IsWindowsVistaOrLater
		{
			get
			{
				return Environment.OSVersion.Platform == 2 && Environment.OSVersion.Version >= new Version(6, 0, 6000);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004C30 File Offset: 0x00002E30
		public static bool IsWindowsXPOrLater
		{
			get
			{
				return Environment.OSVersion.Platform == 2 && Environment.OSVersion.Version >= new Version(5, 1, 2600);
			}
		}

		// Token: 0x060000A2 RID: 162
		[DllImport("kernel32", CharSet = 3, SetLastError = true)]
		public static extern SafeModuleHandle LoadLibraryEx(string lpFileName, IntPtr hFile, NativeMethods.LoadLibraryExFlags dwFlags);

		// Token: 0x060000A3 RID: 163
		[ReliabilityContract(3, 2)]
		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060000A4 RID: 164
		[DllImport("user32.dll", CharSet = 4, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		// Token: 0x060000A5 RID: 165
		[DllImport("user32.dll", CharSet = 4, ExactSpelling = true)]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		// Token: 0x060000A6 RID: 166
		[DllImport("kernel32.dll", CharSet = 4, ExactSpelling = true)]
		public static extern int GetCurrentThreadId();

		// Token: 0x060000A7 RID: 167
		[DllImport("comctl32.dll", PreserveSig = false)]
		public static extern void TaskDialogIndirect([In] ref NativeMethods.TASKDIALOGCONFIG pTaskConfig, out int pnButton, out int pnRadioButton, [MarshalAs(2)] out bool pfVerificationFlagChecked);

		// Token: 0x060000A8 RID: 168
		[DllImport("user32.dll", CharSet = 4)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060000A9 RID: 169
		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern ActivationContextSafeHandle CreateActCtx(ref NativeMethods.ACTCTX actctx);

		// Token: 0x060000AA RID: 170
		[ReliabilityContract(3, 1)]
		[DllImport("kernel32.dll")]
		public static extern void ReleaseActCtx(IntPtr hActCtx);

		// Token: 0x060000AB RID: 171
		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool ActivateActCtx(ActivationContextSafeHandle hActCtx, out IntPtr lpCookie);

		// Token: 0x060000AC RID: 172
		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool DeactivateActCtx(uint dwFlags, IntPtr lpCookie);

		// Token: 0x060000AD RID: 173
		[DllImport("shell32.dll", CharSet = 3)]
		public static extern int SHCreateItemFromParsingName([MarshalAs(21)] string pszPath, IntPtr pbc, ref Guid riid, [MarshalAs(28)] out object ppv);

		// Token: 0x060000AE RID: 174 RVA: 0x00004C70 File Offset: 0x00002E70
		public static IShellItem CreateItemFromParsingName(string path)
		{
			Guid guid;
			guid..ctor("43826d1e-e718-42ee-bc55-a1e261c37bfe");
			object obj;
			int num = NativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero, ref guid, out obj);
			bool flag = num != 0;
			if (flag)
			{
				throw new Win32Exception(num);
			}
			return (IShellItem)obj;
		}

		// Token: 0x060000AF RID: 175
		[DllImport("user32.dll", BestFitMapping = false, CharSet = 4, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern int LoadString(SafeModuleHandle hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

		// Token: 0x060000B0 RID: 176
		[DllImport("Kernel32.dll", CharSet = 4, SetLastError = true)]
		public static extern uint FormatMessage([MarshalAs(8)] NativeMethods.FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, string[] Arguments);

		// Token: 0x060000B1 RID: 177
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, [In] ref NativeMethods.MARGINS pMarInset);

		// Token: 0x060000B2 RID: 178
		[DllImport("dwmapi.dll", PreserveSig = false)]
		[return: MarshalAs(2)]
		public static extern bool DwmIsCompositionEnabled();

		// Token: 0x060000B3 RID: 179
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern SafeDeviceHandle CreateCompatibleDC(IntPtr hDC);

		// Token: 0x060000B4 RID: 180
		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr SelectObject(SafeDeviceHandle hDC, SafeGDIHandle hObject);

		// Token: 0x060000B5 RID: 181
		[ReliabilityContract(3, 2)]
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool DeleteObject(IntPtr hObject);

		// Token: 0x060000B6 RID: 182
		[ReliabilityContract(3, 2)]
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x060000B7 RID: 183
		[DllImport("gdi32.dll")]
		[return: MarshalAs(2)]
		public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, SafeDeviceHandle hdcSrc, int nXSrc, int nYSrc, uint dwRop);

		// Token: 0x060000B8 RID: 184
		[DllImport("UxTheme.dll", CharSet = 3, PreserveSig = false)]
		public static extern void DrawThemeTextEx(IntPtr hTheme, SafeDeviceHandle hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref NativeMethods.RECT pRect, ref NativeMethods.DTTOPTS pOptions);

		// Token: 0x060000B9 RID: 185
		[DllImport("gdi32.dll")]
		public static extern SafeGDIHandle CreateDIBSection(IntPtr hdc, NativeMethods.BITMAPINFO pbmi, uint iUsage, IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x060000BA RID: 186
		[DllImport("UxTheme.dll", CharSet = 3, PreserveSig = false)]
		public static extern void GetThemeTextExtent(IntPtr hTheme, SafeDeviceHandle hdc, int iPartId, int iStateId, string text, int iCharCount, int dwTextFlags, [In] ref NativeMethods.RECT bounds, out NativeMethods.RECT rect);

		// Token: 0x060000BB RID: 187 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public static SafeGDIHandle CreateDib(Rectangle bounds, IntPtr primaryHdc, SafeDeviceHandle memoryHdc)
		{
			NativeMethods.BITMAPINFO bitmapinfo = new NativeMethods.BITMAPINFO();
			bitmapinfo.biSize = Marshal.SizeOf(bitmapinfo);
			bitmapinfo.biWidth = bounds.Width;
			bitmapinfo.biHeight = -bounds.Height;
			bitmapinfo.biPlanes = 1;
			bitmapinfo.biBitCount = 32;
			bitmapinfo.biCompression = 0;
			SafeGDIHandle safeGDIHandle = NativeMethods.CreateDIBSection(primaryHdc, bitmapinfo, 0U, IntPtr.Zero, IntPtr.Zero, 0U);
			NativeMethods.SelectObject(memoryHdc, safeGDIHandle);
			return safeGDIHandle;
		}

		// Token: 0x060000BC RID: 188
		[DllImport("credui.dll", CharSet = 3)]
		internal static extern NativeMethods.CredUIReturnCodes CredUIPromptForCredentials(ref NativeMethods.CREDUI_INFO pUiInfo, string targetName, IntPtr Reserved, int dwAuthError, StringBuilder pszUserName, uint ulUserNameMaxChars, StringBuilder pszPassword, uint ulPaswordMaxChars, [MarshalAs(2)] [In] [Out] ref bool pfSave, NativeMethods.CREDUI_FLAGS dwFlags);

		// Token: 0x060000BD RID: 189
		[DllImport("credui.dll", CharSet = 3)]
		public static extern NativeMethods.CredUIReturnCodes CredUIPromptForWindowsCredentials(ref NativeMethods.CREDUI_INFO pUiInfo, uint dwAuthError, ref uint pulAuthPackage, IntPtr pvInAuthBuffer, uint ulInAuthBufferSize, out IntPtr ppvOutAuthBuffer, out uint pulOutAuthBufferSize, [MarshalAs(2)] ref bool pfSave, NativeMethods.CredUIWinFlags dwFlags);

		// Token: 0x060000BE RID: 190
		[DllImport("advapi32.dll", CharSet = 3, EntryPoint = "CredReadW", SetLastError = true)]
		[return: MarshalAs(2)]
		internal static extern bool CredRead(string TargetName, NativeMethods.CredTypes Type, int Flags, out IntPtr Credential);

		// Token: 0x060000BF RID: 191
		[ReliabilityContract(3, 2)]
		[DllImport("advapi32.dll")]
		internal static extern void CredFree(IntPtr Buffer);

		// Token: 0x060000C0 RID: 192
		[DllImport("advapi32.dll", CharSet = 3, EntryPoint = "CredDeleteW", SetLastError = true)]
		[return: MarshalAs(2)]
		internal static extern bool CredDelete(string TargetName, NativeMethods.CredTypes Type, int Flags);

		// Token: 0x060000C1 RID: 193
		[DllImport("advapi32.dll", CharSet = 3, EntryPoint = "CredWriteW", SetLastError = true)]
		[return: MarshalAs(2)]
		internal static extern bool CredWrite(ref NativeMethods.CREDENTIAL Credential, int Flags);

		// Token: 0x060000C2 RID: 194
		[DllImport("credui.dll", CharSet = 3, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool CredPackAuthenticationBuffer(uint dwFlags, string pszUserName, string pszPassword, IntPtr pPackedCredentials, ref uint pcbPackedCredentials);

		// Token: 0x060000C3 RID: 195
		[DllImport("credui.dll", CharSet = 3, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool CredUnPackAuthenticationBuffer(uint dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, StringBuilder pszUserName, ref uint pcchMaxUserName, StringBuilder pszDomainName, ref uint pcchMaxDomainName, StringBuilder pszPassword, ref uint pcchMaxPassword);

		// Token: 0x04000045 RID: 69
		public const int ErrorFileNotFound = 2;

		// Token: 0x04000046 RID: 70
		public const int WM_USER = 1024;

		// Token: 0x04000047 RID: 71
		public const int WM_GETICON = 127;

		// Token: 0x04000048 RID: 72
		public const int WM_SETICON = 128;

		// Token: 0x04000049 RID: 73
		public const int ICON_SMALL = 0;

		// Token: 0x0400004A RID: 74
		public const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4;

		// Token: 0x0400004B RID: 75
		public const int WM_NCHITTEST = 132;

		// Token: 0x0400004C RID: 76
		public const int WM_DWMCOMPOSITIONCHANGED = 798;

		// Token: 0x0400004D RID: 77
		internal const int CREDUI_MAX_USERNAME_LENGTH = 513;

		// Token: 0x0400004E RID: 78
		internal const int CREDUI_MAX_PASSWORD_LENGTH = 256;

		// Token: 0x02000013 RID: 19
		[Flags]
		public enum LoadLibraryExFlags : uint
		{
			// Token: 0x04000050 RID: 80
			DontResolveDllReferences = 1U,
			// Token: 0x04000051 RID: 81
			LoadLibraryAsDatafile = 2U,
			// Token: 0x04000052 RID: 82
			LoadWithAlteredSearchPath = 8U,
			// Token: 0x04000053 RID: 83
			LoadIgnoreCodeAuthzLevel = 16U
		}

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x060000C5 RID: 197
		public delegate uint TaskDialogCallback(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData);

		// Token: 0x02000015 RID: 21
		public enum TaskDialogNotifications
		{
			// Token: 0x04000055 RID: 85
			Created,
			// Token: 0x04000056 RID: 86
			Navigated,
			// Token: 0x04000057 RID: 87
			ButtonClicked,
			// Token: 0x04000058 RID: 88
			HyperlinkClicked,
			// Token: 0x04000059 RID: 89
			Timer,
			// Token: 0x0400005A RID: 90
			Destroyed,
			// Token: 0x0400005B RID: 91
			RadioButtonClicked,
			// Token: 0x0400005C RID: 92
			DialogConstructed,
			// Token: 0x0400005D RID: 93
			VerificationClicked,
			// Token: 0x0400005E RID: 94
			Help,
			// Token: 0x0400005F RID: 95
			ExpandoButtonClicked
		}

		// Token: 0x02000016 RID: 22
		[Flags]
		public enum TaskDialogCommonButtonFlags
		{
			// Token: 0x04000061 RID: 97
			OkButton = 1,
			// Token: 0x04000062 RID: 98
			YesButton = 2,
			// Token: 0x04000063 RID: 99
			NoButton = 4,
			// Token: 0x04000064 RID: 100
			CancelButton = 8,
			// Token: 0x04000065 RID: 101
			RetryButton = 16,
			// Token: 0x04000066 RID: 102
			CloseButton = 32
		}

		// Token: 0x02000017 RID: 23
		[Flags]
		public enum TaskDialogFlags
		{
			// Token: 0x04000068 RID: 104
			EnableHyperLinks = 1,
			// Token: 0x04000069 RID: 105
			UseHIconMain = 2,
			// Token: 0x0400006A RID: 106
			UseHIconFooter = 4,
			// Token: 0x0400006B RID: 107
			AllowDialogCancellation = 8,
			// Token: 0x0400006C RID: 108
			UseCommandLinks = 16,
			// Token: 0x0400006D RID: 109
			UseCommandLinksNoIcon = 32,
			// Token: 0x0400006E RID: 110
			ExpandFooterArea = 64,
			// Token: 0x0400006F RID: 111
			ExpandedByDefault = 128,
			// Token: 0x04000070 RID: 112
			VerificationFlagChecked = 256,
			// Token: 0x04000071 RID: 113
			ShowProgressBar = 512,
			// Token: 0x04000072 RID: 114
			ShowMarqueeProgressBar = 1024,
			// Token: 0x04000073 RID: 115
			CallbackTimer = 2048,
			// Token: 0x04000074 RID: 116
			PositionRelativeToWindow = 4096,
			// Token: 0x04000075 RID: 117
			RtlLayout = 8192,
			// Token: 0x04000076 RID: 118
			NoDefaultRadioButton = 16384,
			// Token: 0x04000077 RID: 119
			CanBeMinimized = 32768
		}

		// Token: 0x02000018 RID: 24
		public enum TaskDialogMessages
		{
			// Token: 0x04000079 RID: 121
			NavigatePage = 1125,
			// Token: 0x0400007A RID: 122
			ClickButton,
			// Token: 0x0400007B RID: 123
			SetMarqueeProgressBar,
			// Token: 0x0400007C RID: 124
			SetProgressBarState,
			// Token: 0x0400007D RID: 125
			SetProgressBarRange,
			// Token: 0x0400007E RID: 126
			SetProgressBarPos,
			// Token: 0x0400007F RID: 127
			SetProgressBarMarquee,
			// Token: 0x04000080 RID: 128
			SetElementText,
			// Token: 0x04000081 RID: 129
			ClickRadioButton = 1134,
			// Token: 0x04000082 RID: 130
			EnableButton,
			// Token: 0x04000083 RID: 131
			EnableRadioButton,
			// Token: 0x04000084 RID: 132
			ClickVerification,
			// Token: 0x04000085 RID: 133
			UpdateElementText,
			// Token: 0x04000086 RID: 134
			SetButtonElevationRequiredState,
			// Token: 0x04000087 RID: 135
			UpdateIcon
		}

		// Token: 0x02000019 RID: 25
		public enum TaskDialogElements
		{
			// Token: 0x04000089 RID: 137
			Content,
			// Token: 0x0400008A RID: 138
			ExpandedInformation,
			// Token: 0x0400008B RID: 139
			Footer,
			// Token: 0x0400008C RID: 140
			MainInstruction
		}

		// Token: 0x0200001A RID: 26
		[StructLayout(0, Pack = 4)]
		public struct TASKDIALOG_BUTTON
		{
			// Token: 0x0400008D RID: 141
			public int nButtonID;

			// Token: 0x0400008E RID: 142
			[MarshalAs(21)]
			public string pszButtonText;
		}

		// Token: 0x0200001B RID: 27
		[StructLayout(0, Pack = 4)]
		public struct TASKDIALOGCONFIG
		{
			// Token: 0x0400008F RID: 143
			public uint cbSize;

			// Token: 0x04000090 RID: 144
			public IntPtr hwndParent;

			// Token: 0x04000091 RID: 145
			public IntPtr hInstance;

			// Token: 0x04000092 RID: 146
			public NativeMethods.TaskDialogFlags dwFlags;

			// Token: 0x04000093 RID: 147
			public NativeMethods.TaskDialogCommonButtonFlags dwCommonButtons;

			// Token: 0x04000094 RID: 148
			[MarshalAs(21)]
			public string pszWindowTitle;

			// Token: 0x04000095 RID: 149
			public IntPtr hMainIcon;

			// Token: 0x04000096 RID: 150
			[MarshalAs(21)]
			public string pszMainInstruction;

			// Token: 0x04000097 RID: 151
			[MarshalAs(21)]
			public string pszContent;

			// Token: 0x04000098 RID: 152
			public uint cButtons;

			// Token: 0x04000099 RID: 153
			public IntPtr pButtons;

			// Token: 0x0400009A RID: 154
			public int nDefaultButton;

			// Token: 0x0400009B RID: 155
			public uint cRadioButtons;

			// Token: 0x0400009C RID: 156
			public IntPtr pRadioButtons;

			// Token: 0x0400009D RID: 157
			public int nDefaultRadioButton;

			// Token: 0x0400009E RID: 158
			[MarshalAs(21)]
			public string pszVerificationText;

			// Token: 0x0400009F RID: 159
			[MarshalAs(21)]
			public string pszExpandedInformation;

			// Token: 0x040000A0 RID: 160
			[MarshalAs(21)]
			public string pszExpandedControlText;

			// Token: 0x040000A1 RID: 161
			[MarshalAs(21)]
			public string pszCollapsedControlText;

			// Token: 0x040000A2 RID: 162
			public IntPtr hFooterIcon;

			// Token: 0x040000A3 RID: 163
			[MarshalAs(21)]
			public string pszFooterText;

			// Token: 0x040000A4 RID: 164
			[MarshalAs(38)]
			public NativeMethods.TaskDialogCallback pfCallback;

			// Token: 0x040000A5 RID: 165
			public IntPtr lpCallbackData;

			// Token: 0x040000A6 RID: 166
			public uint cxWidth;
		}

		// Token: 0x0200001C RID: 28
		public struct ACTCTX
		{
			// Token: 0x040000A7 RID: 167
			public int cbSize;

			// Token: 0x040000A8 RID: 168
			public uint dwFlags;

			// Token: 0x040000A9 RID: 169
			public string lpSource;

			// Token: 0x040000AA RID: 170
			public ushort wProcessorArchitecture;

			// Token: 0x040000AB RID: 171
			public ushort wLangId;

			// Token: 0x040000AC RID: 172
			public string lpAssemblyDirectory;

			// Token: 0x040000AD RID: 173
			public string lpResourceName;

			// Token: 0x040000AE RID: 174
			public string lpApplicationName;
		}

		// Token: 0x0200001D RID: 29
		[StructLayout(0, CharSet = 4, Pack = 4)]
		internal struct COMDLG_FILTERSPEC
		{
			// Token: 0x040000AF RID: 175
			[MarshalAs(21)]
			internal string pszName;

			// Token: 0x040000B0 RID: 176
			[MarshalAs(21)]
			internal string pszSpec;
		}

		// Token: 0x0200001E RID: 30
		internal enum FDAP
		{
			// Token: 0x040000B2 RID: 178
			FDAP_BOTTOM,
			// Token: 0x040000B3 RID: 179
			FDAP_TOP
		}

		// Token: 0x0200001F RID: 31
		internal enum FDE_SHAREVIOLATION_RESPONSE
		{
			// Token: 0x040000B5 RID: 181
			FDESVR_DEFAULT,
			// Token: 0x040000B6 RID: 182
			FDESVR_ACCEPT,
			// Token: 0x040000B7 RID: 183
			FDESVR_REFUSE
		}

		// Token: 0x02000020 RID: 32
		internal enum FDE_OVERWRITE_RESPONSE
		{
			// Token: 0x040000B9 RID: 185
			FDEOR_DEFAULT,
			// Token: 0x040000BA RID: 186
			FDEOR_ACCEPT,
			// Token: 0x040000BB RID: 187
			FDEOR_REFUSE
		}

		// Token: 0x02000021 RID: 33
		internal enum SIATTRIBFLAGS
		{
			// Token: 0x040000BD RID: 189
			SIATTRIBFLAGS_AND = 1,
			// Token: 0x040000BE RID: 190
			SIATTRIBFLAGS_OR,
			// Token: 0x040000BF RID: 191
			SIATTRIBFLAGS_APPCOMPAT
		}

		// Token: 0x02000022 RID: 34
		internal enum SIGDN : uint
		{
			// Token: 0x040000C1 RID: 193
			SIGDN_NORMALDISPLAY,
			// Token: 0x040000C2 RID: 194
			SIGDN_PARENTRELATIVEPARSING = 2147581953U,
			// Token: 0x040000C3 RID: 195
			SIGDN_DESKTOPABSOLUTEPARSING = 2147647488U,
			// Token: 0x040000C4 RID: 196
			SIGDN_PARENTRELATIVEEDITING = 2147684353U,
			// Token: 0x040000C5 RID: 197
			SIGDN_DESKTOPABSOLUTEEDITING = 2147794944U,
			// Token: 0x040000C6 RID: 198
			SIGDN_FILESYSPATH = 2147844096U,
			// Token: 0x040000C7 RID: 199
			SIGDN_URL = 2147909632U,
			// Token: 0x040000C8 RID: 200
			SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553U,
			// Token: 0x040000C9 RID: 201
			SIGDN_PARENTRELATIVE = 2148007937U
		}

		// Token: 0x02000023 RID: 35
		[Flags]
		internal enum FOS : uint
		{
			// Token: 0x040000CB RID: 203
			FOS_OVERWRITEPROMPT = 2U,
			// Token: 0x040000CC RID: 204
			FOS_STRICTFILETYPES = 4U,
			// Token: 0x040000CD RID: 205
			FOS_NOCHANGEDIR = 8U,
			// Token: 0x040000CE RID: 206
			FOS_PICKFOLDERS = 32U,
			// Token: 0x040000CF RID: 207
			FOS_FORCEFILESYSTEM = 64U,
			// Token: 0x040000D0 RID: 208
			FOS_ALLNONSTORAGEITEMS = 128U,
			// Token: 0x040000D1 RID: 209
			FOS_NOVALIDATE = 256U,
			// Token: 0x040000D2 RID: 210
			FOS_ALLOWMULTISELECT = 512U,
			// Token: 0x040000D3 RID: 211
			FOS_PATHMUSTEXIST = 2048U,
			// Token: 0x040000D4 RID: 212
			FOS_FILEMUSTEXIST = 4096U,
			// Token: 0x040000D5 RID: 213
			FOS_CREATEPROMPT = 8192U,
			// Token: 0x040000D6 RID: 214
			FOS_SHAREAWARE = 16384U,
			// Token: 0x040000D7 RID: 215
			FOS_NOREADONLYRETURN = 32768U,
			// Token: 0x040000D8 RID: 216
			FOS_NOTESTFILECREATE = 65536U,
			// Token: 0x040000D9 RID: 217
			FOS_HIDEMRUPLACES = 131072U,
			// Token: 0x040000DA RID: 218
			FOS_HIDEPINNEDPLACES = 262144U,
			// Token: 0x040000DB RID: 219
			FOS_NODEREFERENCELINKS = 1048576U,
			// Token: 0x040000DC RID: 220
			FOS_DONTADDTORECENT = 33554432U,
			// Token: 0x040000DD RID: 221
			FOS_FORCESHOWHIDDEN = 268435456U,
			// Token: 0x040000DE RID: 222
			FOS_DEFAULTNOMINIMODE = 536870912U
		}

		// Token: 0x02000024 RID: 36
		internal enum CDCONTROLSTATE
		{
			// Token: 0x040000E0 RID: 224
			CDCS_INACTIVE,
			// Token: 0x040000E1 RID: 225
			CDCS_ENABLED,
			// Token: 0x040000E2 RID: 226
			CDCS_VISIBLE
		}

		// Token: 0x02000025 RID: 37
		internal enum FFFP_MODE
		{
			// Token: 0x040000E4 RID: 228
			FFFP_EXACTMATCH,
			// Token: 0x040000E5 RID: 229
			FFFP_NEARESTPARENTMATCH
		}

		// Token: 0x02000026 RID: 38
		[StructLayout(0, CharSet = 4, Pack = 4)]
		internal struct KNOWNFOLDER_DEFINITION
		{
			// Token: 0x040000E6 RID: 230
			internal NativeMethods.KF_CATEGORY category;

			// Token: 0x040000E7 RID: 231
			[MarshalAs(21)]
			internal string pszName;

			// Token: 0x040000E8 RID: 232
			[MarshalAs(21)]
			internal string pszCreator;

			// Token: 0x040000E9 RID: 233
			[MarshalAs(21)]
			internal string pszDescription;

			// Token: 0x040000EA RID: 234
			internal Guid fidParent;

			// Token: 0x040000EB RID: 235
			[MarshalAs(21)]
			internal string pszRelativePath;

			// Token: 0x040000EC RID: 236
			[MarshalAs(21)]
			internal string pszParsingName;

			// Token: 0x040000ED RID: 237
			[MarshalAs(21)]
			internal string pszToolTip;

			// Token: 0x040000EE RID: 238
			[MarshalAs(21)]
			internal string pszLocalizedName;

			// Token: 0x040000EF RID: 239
			[MarshalAs(21)]
			internal string pszIcon;

			// Token: 0x040000F0 RID: 240
			[MarshalAs(21)]
			internal string pszSecurity;

			// Token: 0x040000F1 RID: 241
			internal uint dwAttributes;

			// Token: 0x040000F2 RID: 242
			internal NativeMethods.KF_DEFINITION_FLAGS kfdFlags;

			// Token: 0x040000F3 RID: 243
			internal Guid ftidType;
		}

		// Token: 0x02000027 RID: 39
		internal enum KF_CATEGORY
		{
			// Token: 0x040000F5 RID: 245
			KF_CATEGORY_VIRTUAL = 1,
			// Token: 0x040000F6 RID: 246
			KF_CATEGORY_FIXED,
			// Token: 0x040000F7 RID: 247
			KF_CATEGORY_COMMON,
			// Token: 0x040000F8 RID: 248
			KF_CATEGORY_PERUSER
		}

		// Token: 0x02000028 RID: 40
		[Flags]
		internal enum KF_DEFINITION_FLAGS
		{
			// Token: 0x040000FA RID: 250
			KFDF_PERSONALIZE = 1,
			// Token: 0x040000FB RID: 251
			KFDF_LOCAL_REDIRECT_ONLY = 2,
			// Token: 0x040000FC RID: 252
			KFDF_ROAMABLE = 4
		}

		// Token: 0x02000029 RID: 41
		[StructLayout(0, Pack = 4)]
		internal struct PROPERTYKEY
		{
			// Token: 0x040000FD RID: 253
			internal Guid fmtid;

			// Token: 0x040000FE RID: 254
			internal uint pid;
		}

		// Token: 0x0200002A RID: 42
		[Flags]
		public enum FormatMessageFlags
		{
			// Token: 0x04000100 RID: 256
			FORMAT_MESSAGE_ALLOCATE_BUFFER = 256,
			// Token: 0x04000101 RID: 257
			FORMAT_MESSAGE_IGNORE_INSERTS = 512,
			// Token: 0x04000102 RID: 258
			FORMAT_MESSAGE_FROM_STRING = 1024,
			// Token: 0x04000103 RID: 259
			FORMAT_MESSAGE_FROM_HMODULE = 2048,
			// Token: 0x04000104 RID: 260
			FORMAT_MESSAGE_FROM_SYSTEM = 4096,
			// Token: 0x04000105 RID: 261
			FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192
		}

		// Token: 0x0200002B RID: 43
		public enum HitTestResult
		{
			// Token: 0x04000107 RID: 263
			Error = -2,
			// Token: 0x04000108 RID: 264
			Transparent,
			// Token: 0x04000109 RID: 265
			Nowhere,
			// Token: 0x0400010A RID: 266
			Client,
			// Token: 0x0400010B RID: 267
			Caption,
			// Token: 0x0400010C RID: 268
			SysMenu,
			// Token: 0x0400010D RID: 269
			GrowBox,
			// Token: 0x0400010E RID: 270
			Size = 4,
			// Token: 0x0400010F RID: 271
			Menu,
			// Token: 0x04000110 RID: 272
			HScroll,
			// Token: 0x04000111 RID: 273
			VScroll,
			// Token: 0x04000112 RID: 274
			MinButton,
			// Token: 0x04000113 RID: 275
			MaxButton,
			// Token: 0x04000114 RID: 276
			Left,
			// Token: 0x04000115 RID: 277
			Right,
			// Token: 0x04000116 RID: 278
			Top,
			// Token: 0x04000117 RID: 279
			TopLeft,
			// Token: 0x04000118 RID: 280
			TopRight,
			// Token: 0x04000119 RID: 281
			Bottom,
			// Token: 0x0400011A RID: 282
			BottomLeft,
			// Token: 0x0400011B RID: 283
			BottomRight,
			// Token: 0x0400011C RID: 284
			Border,
			// Token: 0x0400011D RID: 285
			Reduce = 8,
			// Token: 0x0400011E RID: 286
			Zoom,
			// Token: 0x0400011F RID: 287
			SizeFirst,
			// Token: 0x04000120 RID: 288
			SizeLast = 17,
			// Token: 0x04000121 RID: 289
			Object = 19,
			// Token: 0x04000122 RID: 290
			Close,
			// Token: 0x04000123 RID: 291
			Help
		}

		// Token: 0x0200002C RID: 44
		public struct MARGINS
		{
			// Token: 0x060000C8 RID: 200 RVA: 0x00004D2A File Offset: 0x00002F2A
			public MARGINS(Padding value)
			{
				this.Left = value.Left;
				this.Right = value.Right;
				this.Top = value.Top;
				this.Bottom = value.Bottom;
			}

			// Token: 0x04000124 RID: 292
			public int Left;

			// Token: 0x04000125 RID: 293
			public int Right;

			// Token: 0x04000126 RID: 294
			public int Top;

			// Token: 0x04000127 RID: 295
			public int Bottom;
		}

		// Token: 0x0200002D RID: 45
		public struct DTTOPTS
		{
			// Token: 0x04000128 RID: 296
			public int dwSize;

			// Token: 0x04000129 RID: 297
			[MarshalAs(8)]
			public NativeMethods.DrawThemeTextFlags dwFlags;

			// Token: 0x0400012A RID: 298
			public int crText;

			// Token: 0x0400012B RID: 299
			public int crBorder;

			// Token: 0x0400012C RID: 300
			public int crShadow;

			// Token: 0x0400012D RID: 301
			public int iTextShadowType;

			// Token: 0x0400012E RID: 302
			public Point ptShadowOffset;

			// Token: 0x0400012F RID: 303
			public int iBorderSize;

			// Token: 0x04000130 RID: 304
			public int iFontPropId;

			// Token: 0x04000131 RID: 305
			public int iColorPropId;

			// Token: 0x04000132 RID: 306
			public int iStateId;

			// Token: 0x04000133 RID: 307
			public bool fApplyOverlay;

			// Token: 0x04000134 RID: 308
			public int iGlowSize;

			// Token: 0x04000135 RID: 309
			public int pfnDrawTextCallback;

			// Token: 0x04000136 RID: 310
			public IntPtr lParam;
		}

		// Token: 0x0200002E RID: 46
		[Flags]
		public enum DrawThemeTextFlags
		{
			// Token: 0x04000138 RID: 312
			TextColor = 1,
			// Token: 0x04000139 RID: 313
			BorderColor = 2,
			// Token: 0x0400013A RID: 314
			ShadowColor = 4,
			// Token: 0x0400013B RID: 315
			ShadowType = 8,
			// Token: 0x0400013C RID: 316
			ShadowOffset = 16,
			// Token: 0x0400013D RID: 317
			BorderSize = 32,
			// Token: 0x0400013E RID: 318
			FontProp = 64,
			// Token: 0x0400013F RID: 319
			ColorProp = 128,
			// Token: 0x04000140 RID: 320
			StateId = 256,
			// Token: 0x04000141 RID: 321
			CalcRect = 512,
			// Token: 0x04000142 RID: 322
			ApplyOverlay = 1024,
			// Token: 0x04000143 RID: 323
			GlowSize = 2048,
			// Token: 0x04000144 RID: 324
			Callback = 4096,
			// Token: 0x04000145 RID: 325
			Composited = 8192
		}

		// Token: 0x0200002F RID: 47
		[StructLayout(0)]
		public class BITMAPINFO
		{
			// Token: 0x04000146 RID: 326
			public int biSize;

			// Token: 0x04000147 RID: 327
			public int biWidth;

			// Token: 0x04000148 RID: 328
			public int biHeight;

			// Token: 0x04000149 RID: 329
			public short biPlanes;

			// Token: 0x0400014A RID: 330
			public short biBitCount;

			// Token: 0x0400014B RID: 331
			public int biCompression;

			// Token: 0x0400014C RID: 332
			public int biSizeImage;

			// Token: 0x0400014D RID: 333
			public int biXPelsPerMeter;

			// Token: 0x0400014E RID: 334
			public int biYPelsPerMeter;

			// Token: 0x0400014F RID: 335
			public int biClrUsed;

			// Token: 0x04000150 RID: 336
			public int biClrImportant;

			// Token: 0x04000151 RID: 337
			public byte bmiColors_rgbBlue;

			// Token: 0x04000152 RID: 338
			public byte bmiColors_rgbGreen;

			// Token: 0x04000153 RID: 339
			public byte bmiColors_rgbRed;

			// Token: 0x04000154 RID: 340
			public byte bmiColors_rgbReserved;
		}

		// Token: 0x02000030 RID: 48
		public struct RECT
		{
			// Token: 0x060000CA RID: 202 RVA: 0x00004D6A File Offset: 0x00002F6A
			public RECT(int left, int top, int right, int bottom)
			{
				this.Left = left;
				this.Top = top;
				this.Right = right;
				this.Bottom = bottom;
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00004D8A File Offset: 0x00002F8A
			public RECT(Rectangle rectangle)
			{
				this.Left = rectangle.X;
				this.Top = rectangle.Y;
				this.Right = rectangle.Right;
				this.Bottom = rectangle.Bottom;
			}

			// Token: 0x060000CC RID: 204 RVA: 0x00004DC4 File Offset: 0x00002FC4
			public override string ToString()
			{
				return string.Concat(new object[] { "Left: ", this.Left, ", Top: ", this.Top, ", Right: ", this.Right, ", Bottom: ", this.Bottom });
			}

			// Token: 0x04000155 RID: 341
			public int Left;

			// Token: 0x04000156 RID: 342
			public int Top;

			// Token: 0x04000157 RID: 343
			public int Right;

			// Token: 0x04000158 RID: 344
			public int Bottom;
		}

		// Token: 0x02000031 RID: 49
		[Flags]
		public enum CREDUI_FLAGS
		{
			// Token: 0x0400015A RID: 346
			INCORRECT_PASSWORD = 1,
			// Token: 0x0400015B RID: 347
			DO_NOT_PERSIST = 2,
			// Token: 0x0400015C RID: 348
			REQUEST_ADMINISTRATOR = 4,
			// Token: 0x0400015D RID: 349
			EXCLUDE_CERTIFICATES = 8,
			// Token: 0x0400015E RID: 350
			REQUIRE_CERTIFICATE = 16,
			// Token: 0x0400015F RID: 351
			SHOW_SAVE_CHECK_BOX = 64,
			// Token: 0x04000160 RID: 352
			ALWAYS_SHOW_UI = 128,
			// Token: 0x04000161 RID: 353
			REQUIRE_SMARTCARD = 256,
			// Token: 0x04000162 RID: 354
			PASSWORD_ONLY_OK = 512,
			// Token: 0x04000163 RID: 355
			VALIDATE_USERNAME = 1024,
			// Token: 0x04000164 RID: 356
			COMPLETE_USERNAME = 2048,
			// Token: 0x04000165 RID: 357
			PERSIST = 4096,
			// Token: 0x04000166 RID: 358
			SERVER_CREDENTIAL = 16384,
			// Token: 0x04000167 RID: 359
			EXPECT_CONFIRMATION = 131072,
			// Token: 0x04000168 RID: 360
			GENERIC_CREDENTIALS = 262144,
			// Token: 0x04000169 RID: 361
			USERNAME_TARGET_CREDENTIALS = 524288,
			// Token: 0x0400016A RID: 362
			KEEP_USERNAME = 1048576
		}

		// Token: 0x02000032 RID: 50
		[Flags]
		public enum CredUIWinFlags
		{
			// Token: 0x0400016C RID: 364
			Generic = 1,
			// Token: 0x0400016D RID: 365
			Checkbox = 2,
			// Token: 0x0400016E RID: 366
			AutoPackageOnly = 16,
			// Token: 0x0400016F RID: 367
			InCredOnly = 32,
			// Token: 0x04000170 RID: 368
			EnumerateAdmins = 256,
			// Token: 0x04000171 RID: 369
			EnumerateCurrentUser = 512,
			// Token: 0x04000172 RID: 370
			SecurePrompt = 4096,
			// Token: 0x04000173 RID: 371
			Pack32Wow = 268435456
		}

		// Token: 0x02000033 RID: 51
		internal enum CredUIReturnCodes
		{
			// Token: 0x04000175 RID: 373
			NO_ERROR,
			// Token: 0x04000176 RID: 374
			ERROR_CANCELLED = 1223,
			// Token: 0x04000177 RID: 375
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			// Token: 0x04000178 RID: 376
			ERROR_NOT_FOUND = 1168,
			// Token: 0x04000179 RID: 377
			ERROR_INVALID_ACCOUNT_NAME = 1315,
			// Token: 0x0400017A RID: 378
			ERROR_INSUFFICIENT_BUFFER = 122,
			// Token: 0x0400017B RID: 379
			ERROR_INVALID_PARAMETER = 87,
			// Token: 0x0400017C RID: 380
			ERROR_INVALID_FLAGS = 1004
		}

		// Token: 0x02000034 RID: 52
		internal enum CredTypes
		{
			// Token: 0x0400017E RID: 382
			CRED_TYPE_GENERIC = 1,
			// Token: 0x0400017F RID: 383
			CRED_TYPE_DOMAIN_PASSWORD,
			// Token: 0x04000180 RID: 384
			CRED_TYPE_DOMAIN_CERTIFICATE,
			// Token: 0x04000181 RID: 385
			CRED_TYPE_DOMAIN_VISIBLE_PASSWORD
		}

		// Token: 0x02000035 RID: 53
		internal enum CredPersist
		{
			// Token: 0x04000183 RID: 387
			Session = 1,
			// Token: 0x04000184 RID: 388
			LocalMachine,
			// Token: 0x04000185 RID: 389
			Enterprise
		}

		// Token: 0x02000036 RID: 54
		internal struct CREDUI_INFO
		{
			// Token: 0x04000186 RID: 390
			public int cbSize;

			// Token: 0x04000187 RID: 391
			public IntPtr hwndParent;

			// Token: 0x04000188 RID: 392
			[MarshalAs(21)]
			public string pszMessageText;

			// Token: 0x04000189 RID: 393
			[MarshalAs(21)]
			public string pszCaptionText;

			// Token: 0x0400018A RID: 394
			public IntPtr hbmBanner;
		}

		// Token: 0x02000037 RID: 55
		public struct CREDENTIAL
		{
			// Token: 0x0400018B RID: 395
			public int Flags;

			// Token: 0x0400018C RID: 396
			public NativeMethods.CredTypes Type;

			// Token: 0x0400018D RID: 397
			[MarshalAs(21)]
			public string TargetName;

			// Token: 0x0400018E RID: 398
			[MarshalAs(21)]
			public string Comment;

			// Token: 0x0400018F RID: 399
			public long LastWritten;

			// Token: 0x04000190 RID: 400
			public uint CredentialBlobSize;

			// Token: 0x04000191 RID: 401
			public IntPtr CredentialBlob;

			// Token: 0x04000192 RID: 402
			[MarshalAs(8)]
			public NativeMethods.CredPersist Persist;

			// Token: 0x04000193 RID: 403
			public int AttributeCount;

			// Token: 0x04000194 RID: 404
			public IntPtr Attributes;

			// Token: 0x04000195 RID: 405
			[MarshalAs(21)]
			public string TargetAlias;

			// Token: 0x04000196 RID: 406
			[MarshalAs(21)]
			public string UserName;
		}
	}
}
