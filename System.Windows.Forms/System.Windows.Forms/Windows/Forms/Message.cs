using System;

namespace System.Windows.Forms
{
	/// <summary>Implements a Windows message.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013B RID: 315
	public struct Message
	{
		/// <summary>Gets or sets the window handle of the message.</summary>
		/// <returns>The window handle of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0003778E File Offset: 0x0003598E
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00037796 File Offset: 0x00035996
		public IntPtr HWnd
		{
			get
			{
				return this.hwnd;
			}
			set
			{
				this.hwnd = value;
			}
		}

		/// <summary>Specifies the <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0003779F File Offset: 0x0003599F
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x000377A7 File Offset: 0x000359A7
		public IntPtr LParam
		{
			get
			{
				return this.lParam;
			}
			set
			{
				this.lParam = value;
			}
		}

		/// <summary>Gets or sets the ID number for the message.</summary>
		/// <returns>The ID number for the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x000377B0 File Offset: 0x000359B0
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x000377B8 File Offset: 0x000359B8
		public int Msg
		{
			get
			{
				return this.msg;
			}
			set
			{
				this.msg = value;
			}
		}

		/// <summary>Specifies the value that is returned to Windows in response to handling the message.</summary>
		/// <returns>The return value of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x000377C1 File Offset: 0x000359C1
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x000377C9 File Offset: 0x000359C9
		public IntPtr Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x000377D2 File Offset: 0x000359D2
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x000377DA File Offset: 0x000359DA
		public IntPtr WParam
		{
			get
			{
				return this.wParam;
			}
			set
			{
				this.wParam = value;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.Message" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Message" /> that represents the message that was created.</returns>
		/// <param name="hWnd">The window handle that the message is for. </param>
		/// <param name="msg">The message ID. </param>
		/// <param name="wparam">The message <paramref name="wparam" /> field. </param>
		/// <param name="lparam">The message <paramref name="lparam" /> field. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000CBD RID: 3261 RVA: 0x000377E4 File Offset: 0x000359E4
		public static Message Create(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			return new Message
			{
				msg = msg,
				hwnd = hWnd,
				wParam = wparam,
				lParam = lparam
			};
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current object.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000CBE RID: 3262 RVA: 0x0003781C File Offset: 0x00035A1C
		public override bool Equals(object o)
		{
			return o is Message && (this.msg == ((Message)o).msg && this.hwnd == ((Message)o).hwnd && this.lParam == ((Message)o).lParam && this.wParam == ((Message)o).wParam) && this.result == ((Message)o).result;
		}

		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000CBF RID: 3263 RVA: 0x000378A6 File Offset: 0x00035AA6
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000CC0 RID: 3264 RVA: 0x000378B8 File Offset: 0x00035AB8
		public override string ToString()
		{
			string text = "msg=0x{0:x} ({1}) hwnd=0x{2:x} wparam=0x{3:x} lparam=0x{4:x} result=0x{5:x}";
			object[] array = new object[6];
			array[0] = this.msg;
			int num = 1;
			Msg msg = (Msg)this.msg;
			array[num] = msg.ToString();
			array[2] = this.hwnd.ToInt32();
			array[3] = this.wParam.ToInt32();
			array[4] = this.lParam.ToInt32();
			array[5] = this.result.ToInt32();
			return string.Format(text, array);
		}

		// Token: 0x040007FD RID: 2045
		private int msg;

		// Token: 0x040007FE RID: 2046
		private IntPtr hwnd;

		// Token: 0x040007FF RID: 2047
		private IntPtr lParam;

		// Token: 0x04000800 RID: 2048
		private IntPtr wParam;

		// Token: 0x04000801 RID: 2049
		private IntPtr result;
	}
}
