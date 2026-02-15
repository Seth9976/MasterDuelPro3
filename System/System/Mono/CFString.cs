using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200000D RID: 13
	internal class CFString : CFObject
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000022B4 File Offset: 0x000004B4
		public CFString(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x06000024 RID: 36
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringCreateWithCharacters(IntPtr alloc, IntPtr chars, IntPtr length);

		// Token: 0x06000025 RID: 37 RVA: 0x00002384 File Offset: 0x00000584
		public unsafe static CFString Create(string value)
		{
			IntPtr intPtr;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				intPtr = CFString.CFStringCreateWithCharacters(IntPtr.Zero, (IntPtr)((void*)ptr), (IntPtr)value.Length);
			}
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFString(intPtr, true);
		}

		// Token: 0x06000026 RID: 38
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetLength(IntPtr handle);

		// Token: 0x06000027 RID: 39
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern int CFStringCompare(IntPtr theString1, IntPtr theString2, int compareOptions);

		// Token: 0x06000028 RID: 40 RVA: 0x000023D5 File Offset: 0x000005D5
		public static int Compare(IntPtr string1, IntPtr string2, int compareOptions = 0)
		{
			return CFString.CFStringCompare(string1, string2, compareOptions);
		}

		// Token: 0x06000029 RID: 41
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetCharactersPtr(IntPtr handle);

		// Token: 0x0600002A RID: 42
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetCharacters(IntPtr handle, CFRange range, IntPtr buffer);

		// Token: 0x0600002B RID: 43 RVA: 0x000023E0 File Offset: 0x000005E0
		public unsafe static string AsString(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			int num = (int)CFString.CFStringGetLength(handle);
			if (num == 0)
			{
				return string.Empty;
			}
			IntPtr intPtr = CFString.CFStringGetCharactersPtr(handle);
			IntPtr intPtr2 = IntPtr.Zero;
			if (intPtr == IntPtr.Zero)
			{
				CFRange cfrange = new CFRange(0, num);
				intPtr2 = Marshal.AllocHGlobal(num * 2);
				CFString.CFStringGetCharacters(handle, cfrange, intPtr2);
				intPtr = intPtr2;
			}
			string text = new string((char*)(void*)intPtr, 0, num);
			if (intPtr2 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr2);
			}
			return text;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002468 File Offset: 0x00000668
		public override string ToString()
		{
			if (this.str == null)
			{
				this.str = CFString.AsString(base.Handle);
			}
			return this.str;
		}

		// Token: 0x04000019 RID: 25
		private string str;
	}
}
