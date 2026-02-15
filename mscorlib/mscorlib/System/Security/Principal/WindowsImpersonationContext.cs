using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents the Windows user prior to an impersonation operation. </summary>
	// Token: 0x020003DD RID: 989
	[ComVisible(true)]
	public class WindowsImpersonationContext : IDisposable
	{
		// Token: 0x0600219B RID: 8603 RVA: 0x0008BE2B File Offset: 0x0008A02B
		internal WindowsImpersonationContext(IntPtr token)
		{
			this._token = WindowsImpersonationContext.DuplicateToken(token);
			if (!WindowsImpersonationContext.SetCurrentToken(token))
			{
				throw new SecurityException("Couldn't impersonate token.");
			}
			this.undo = false;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsImpersonationContext" />.</summary>
		// Token: 0x0600219C RID: 8604 RVA: 0x0008BE59 File Offset: 0x0008A059
		[ComVisible(false)]
		public void Dispose()
		{
			if (!this.undo)
			{
				this.Undo();
			}
		}

		/// <summary>Reverts the user context to the Windows user represented by this object.</summary>
		/// <exception cref="T:System.Security.SecurityException">An attempt is made to use this method for any purpose other than to revert identity to self. </exception>
		// Token: 0x0600219D RID: 8605 RVA: 0x0008BE69 File Offset: 0x0008A069
		public void Undo()
		{
			if (!WindowsImpersonationContext.RevertToSelf())
			{
				WindowsImpersonationContext.CloseToken(this._token);
				throw new SecurityException("Couldn't switch back to original token.");
			}
			WindowsImpersonationContext.CloseToken(this._token);
			this.undo = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600219E RID: 8606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CloseToken(IntPtr token);

		// Token: 0x0600219F RID: 8607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr DuplicateToken(IntPtr token);

		// Token: 0x060021A0 RID: 8608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetCurrentToken(IntPtr token);

		// Token: 0x060021A1 RID: 8609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RevertToSelf();

		// Token: 0x04001019 RID: 4121
		private IntPtr _token;

		// Token: 0x0400101A RID: 4122
		private bool undo;
	}
}
