using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200006F RID: 111
	internal class Win32Resources : IDisposable
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000A1D4 File Offset: 0x000083D4
		public Win32Resources(string module)
		{
			this._moduleHandle = NativeMethods.LoadLibraryEx(module, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			bool isInvalid = this._moduleHandle.IsInvalid;
			if (isInvalid)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000A218 File Offset: 0x00008418
		public string LoadString(uint id)
		{
			this.CheckDisposed();
			StringBuilder stringBuilder = new StringBuilder(500);
			bool flag = NativeMethods.LoadString(this._moduleHandle, id, stringBuilder, stringBuilder.Capacity + 1) == 0;
			if (flag)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A26C File Offset: 0x0000846C
		public string FormatString(uint id, params string[] args)
		{
			this.CheckDisposed();
			IntPtr zero = IntPtr.Zero;
			string text = this.LoadString(id);
			NativeMethods.FormatMessageFlags formatMessageFlags = NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_FROM_STRING | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ARGUMENT_ARRAY;
			IntPtr intPtr = Marshal.StringToHGlobalAuto(text);
			try
			{
				bool flag = NativeMethods.FormatMessage(formatMessageFlags, intPtr, id, 0U, ref zero, 0U, args) == 0U;
				if (flag)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			string text2 = Marshal.PtrToStringAuto(zero);
			Marshal.FreeHGlobal(zero);
			return text2;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A2F4 File Offset: 0x000084F4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._moduleHandle.Dispose();
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000A314 File Offset: 0x00008514
		private void CheckDisposed()
		{
			bool isClosed = this._moduleHandle.IsClosed;
			if (isClosed)
			{
				throw new ObjectDisposedException("Win32Resources");
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000A33D File Offset: 0x0000853D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000242 RID: 578
		private SafeModuleHandle _moduleHandle;

		// Token: 0x04000243 RID: 579
		private const int _bufferSize = 500;
	}
}
