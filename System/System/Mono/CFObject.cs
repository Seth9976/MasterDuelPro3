using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x02000009 RID: 9
	internal class CFObject : IDisposable
	{
		// Token: 0x0600000B RID: 11
		[DllImport("/usr/lib/libSystem.dylib")]
		public static extern IntPtr dlopen(string path, int mode);

		// Token: 0x0600000C RID: 12
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x0600000D RID: 13
		[DllImport("/usr/lib/libSystem.dylib")]
		public static extern void dlclose(IntPtr handle);

		// Token: 0x0600000E RID: 14 RVA: 0x000021D0 File Offset: 0x000003D0
		public static IntPtr GetIndirect(IntPtr handle, string symbol)
		{
			return CFObject.dlsym(handle, symbol);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021DC File Offset: 0x000003DC
		public static IntPtr GetCFObjectHandle(IntPtr handle, string symbol)
		{
			IntPtr intPtr = CFObject.dlsym(handle, symbol);
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			return Marshal.ReadIntPtr(intPtr);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000220A File Offset: 0x0000040A
		public CFObject(IntPtr handle, bool own)
		{
			this.Handle = handle;
			if (!own)
			{
				this.Retain();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002224 File Offset: 0x00000424
		~CFObject()
		{
			this.Dispose(false);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002254 File Offset: 0x00000454
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000225C File Offset: 0x0000045C
		public IntPtr Handle { get; private set; }

		// Token: 0x06000014 RID: 20
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern IntPtr CFRetain(IntPtr handle);

		// Token: 0x06000015 RID: 21 RVA: 0x00002265 File Offset: 0x00000465
		private void Retain()
		{
			CFObject.CFRetain(this.Handle);
		}

		// Token: 0x06000016 RID: 22
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern void CFRelease(IntPtr handle);

		// Token: 0x06000017 RID: 23 RVA: 0x00002273 File Offset: 0x00000473
		private void Release()
		{
			CFObject.CFRelease(this.Handle);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002280 File Offset: 0x00000480
		protected virtual void Dispose(bool disposing)
		{
			if (this.Handle != IntPtr.Zero)
			{
				this.Release();
				this.Handle = IntPtr.Zero;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022A5 File Offset: 0x000004A5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
