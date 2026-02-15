using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Used to pass a value, or an array of values, to an image encoder. </summary>
	// Token: 0x02000083 RID: 131
	[StructLayout(LayoutKind.Sequential)]
	public sealed class EncoderParameter : IDisposable
	{
		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		// Token: 0x06000456 RID: 1110 RVA: 0x0000DB19 File Offset: 0x0000BD19
		public void Dispose()
		{
			this.Dispose(true);
			GC.KeepAlive(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000DB2E File Offset: 0x0000BD2E
		private void Dispose(bool disposing)
		{
			if (this._parameterValue != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this._parameterValue);
			}
			this._parameterValue = IntPtr.Zero;
		}

		// Token: 0x04000255 RID: 597
		[MarshalAs(UnmanagedType.Struct)]
		private Guid _parameterGuid;

		// Token: 0x04000256 RID: 598
		private int _numberOfValues;

		// Token: 0x04000257 RID: 599
		private EncoderParameterValueType _parameterValueType;

		// Token: 0x04000258 RID: 600
		private IntPtr _parameterValue;
	}
}
