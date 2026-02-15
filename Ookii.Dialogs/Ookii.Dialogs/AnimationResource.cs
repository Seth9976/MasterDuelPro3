using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000005 RID: 5
	public sealed class AnimationResource
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		public AnimationResource(string resourceFile, int resourceId)
		{
			bool flag = resourceFile == null;
			if (flag)
			{
				throw new ArgumentNullException("resourceFile");
			}
			this.ResourceFile = resourceFile;
			this.ResourceId = resourceId;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		public string ResourceFile { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002159 File Offset: 0x00000359
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002161 File Offset: 0x00000361
		public int ResourceId { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public static AnimationResource GetShellAnimation(ShellAnimation animation)
		{
			bool flag = !Enum.IsDefined(typeof(ShellAnimation), animation);
			if (flag)
			{
				throw new ArgumentOutOfRangeException("animation");
			}
			return new AnimationResource("shell32.dll", (int)animation);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B0 File Offset: 0x000003B0
		internal SafeModuleHandle LoadLibrary()
		{
			SafeModuleHandle safeModuleHandle = NativeMethods.LoadLibraryEx(this.ResourceFile, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			bool isInvalid = safeModuleHandle.IsInvalid;
			if (!isInvalid)
			{
				return safeModuleHandle;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			bool flag = lastWin32Error == 2;
			if (flag)
			{
				throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, Resources.FileNotFoundFormat, new object[] { this.ResourceFile }));
			}
			throw new Win32Exception(lastWin32Error);
		}
	}
}
