using System;

namespace System.Drawing
{
	/// <summary>Defines methods for obtaining and releasing an existing handle to a Windows device context.</summary>
	// Token: 0x02000014 RID: 20
	public interface IDeviceContext : IDisposable
	{
		/// <summary>Returns the handle to a Windows device context.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> representing the handle of a device context.</returns>
		// Token: 0x0600004A RID: 74
		IntPtr GetHdc();

		/// <summary>Releases the handle of a Windows device context.</summary>
		// Token: 0x0600004B RID: 75
		void ReleaseHdc();
	}
}
