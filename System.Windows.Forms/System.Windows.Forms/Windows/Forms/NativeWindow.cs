using System;
using System.Collections;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides a low-level encapsulation of a window handle and a window procedure.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000154 RID: 340
	public class NativeWindow : MarshalByRefObject, IWin32Window
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.NativeWindow" /> class.</summary>
		// Token: 0x06000D38 RID: 3384 RVA: 0x0003A55D File Offset: 0x0003875D
		public NativeWindow()
		{
			this.window_handle = IntPtr.Zero;
		}

		/// <summary>Gets the handle for this window. </summary>
		/// <returns>If successful, an <see cref="T:System.IntPtr" /> representing the handle to the associated native Win32 window; otherwise, 0 if no handle is associated with the window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0003A57B File Offset: 0x0003877B
		public IntPtr Handle
		{
			get
			{
				return this.window_handle;
			}
		}

		/// <summary>Retrieves the window associated with the specified handle. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.NativeWindow" /> associated with the specified handle. This method returns null when the handle does not have an associated window.</returns>
		/// <param name="handle">A handle to a window. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000D3A RID: 3386 RVA: 0x0003A583 File Offset: 0x00038783
		public static NativeWindow FromHandle(IntPtr handle)
		{
			return NativeWindow.FindFirstInTable(handle);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003A58B File Offset: 0x0003878B
		internal void InvalidateHandle()
		{
			NativeWindow.RemoveFromTable(this);
			this.window_handle = IntPtr.Zero;
		}

		/// <summary>Assigns a handle to this window. </summary>
		/// <param name="handle">The handle to assign to this window. </param>
		/// <exception cref="T:System.Exception">This window already has a handle. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The windows procedure for the associated native window could not be retrieved.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D3C RID: 3388 RVA: 0x0003A59E File Offset: 0x0003879E
		public void AssignHandle(IntPtr handle)
		{
			NativeWindow.RemoveFromTable(this);
			this.window_handle = handle;
			NativeWindow.AddToTable(this);
			this.OnHandleChange();
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003A5BC File Offset: 0x000387BC
		private static void AddToTable(NativeWindow window)
		{
			IntPtr handle = window.Handle;
			if (handle == IntPtr.Zero)
			{
				return;
			}
			Hashtable hashtable = NativeWindow.window_collection;
			lock (hashtable)
			{
				object obj = NativeWindow.window_collection[handle];
				if (obj == null)
				{
					NativeWindow.window_collection.Add(handle, window);
				}
				else
				{
					NativeWindow nativeWindow = obj as NativeWindow;
					if (nativeWindow != null)
					{
						if (nativeWindow != window)
						{
							ArrayList arrayList = new ArrayList();
							arrayList.Add(nativeWindow);
							arrayList.Add(window);
							NativeWindow.window_collection[handle] = arrayList;
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)NativeWindow.window_collection[handle];
						if (!arrayList2.Contains(window))
						{
							arrayList2.Add(window);
						}
					}
				}
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003A69C File Offset: 0x0003889C
		private static void RemoveFromTable(NativeWindow window)
		{
			IntPtr handle = window.Handle;
			if (handle == IntPtr.Zero)
			{
				return;
			}
			Hashtable hashtable = NativeWindow.window_collection;
			lock (hashtable)
			{
				object obj = NativeWindow.window_collection[handle];
				if (obj != null)
				{
					if (obj is NativeWindow)
					{
						NativeWindow.window_collection.Remove(handle);
					}
					else
					{
						ArrayList arrayList = (ArrayList)NativeWindow.window_collection[handle];
						arrayList.Remove(window);
						if (arrayList.Count == 0)
						{
							NativeWindow.window_collection.Remove(handle);
						}
						else if (arrayList.Count == 1)
						{
							NativeWindow.window_collection[handle] = arrayList[0];
						}
					}
				}
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003A774 File Offset: 0x00038974
		private static NativeWindow FindFirstInTable(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			NativeWindow nativeWindow = null;
			Hashtable hashtable = NativeWindow.window_collection;
			lock (hashtable)
			{
				object obj = NativeWindow.window_collection[handle];
				if (obj != null)
				{
					nativeWindow = obj as NativeWindow;
					if (nativeWindow == null)
					{
						ArrayList arrayList = (ArrayList)obj;
						if (arrayList.Count > 0)
						{
							nativeWindow = (NativeWindow)arrayList[0];
						}
					}
				}
			}
			return nativeWindow;
		}

		/// <summary>Creates a window and its handle with the specified creation parameters. </summary>
		/// <param name="cp">A <see cref="T:System.Windows.Forms.CreateParams" /> that specifies the creation parameters for this window. </param>
		/// <exception cref="T:System.OutOfMemoryException">The operating system ran out of resources when trying to create the native window.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 API could not create the specified window. </exception>
		/// <exception cref="T:System.InvalidOperationException">The handle of the current native window is already assigned; in explanation, the <see cref="P:System.Windows.Forms.NativeWindow.Handle" /> property is not equal to <see cref="F:System.IntPtr.Zero" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D40 RID: 3392 RVA: 0x0003A800 File Offset: 0x00038A00
		public virtual void CreateHandle(CreateParams cp)
		{
			if (cp != null)
			{
				NativeWindow.WindowCreating = this;
				this.window_handle = XplatUI.CreateWindow(cp);
				NativeWindow.WindowCreating = null;
				if (this.window_handle != IntPtr.Zero)
				{
					NativeWindow.AddToTable(this);
				}
			}
		}

		/// <summary>Invokes the default window procedure associated with this window. </summary>
		/// <param name="m">The message that is currently being processed. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000D41 RID: 3393 RVA: 0x0003A835 File Offset: 0x00038A35
		public void DefWndProc(ref Message m)
		{
			m.Result = XplatUI.DefWndProc(ref m);
		}

		/// <summary>Destroys the window and its handle. </summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000D42 RID: 3394 RVA: 0x0003A843 File Offset: 0x00038A43
		public virtual void DestroyHandle()
		{
			if (this.window_handle != IntPtr.Zero)
			{
				XplatUI.DestroyWindow(this.window_handle);
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003A864 File Offset: 0x00038A64
		~NativeWindow()
		{
		}

		/// <summary>Specifies a notification method that is called when the handle for a window is changed. </summary>
		// Token: 0x06000D44 RID: 3396 RVA: 0x0000493C File Offset: 0x00002B3C
		protected virtual void OnHandleChange()
		{
		}

		/// <summary>When overridden in a derived class, manages an unhandled thread exception. </summary>
		/// <param name="e">An <see cref="T:System.Exception" /> that specifies the unhandled thread exception. </param>
		// Token: 0x06000D45 RID: 3397 RVA: 0x0003A88C File Offset: 0x00038A8C
		protected virtual void OnThreadException(Exception e)
		{
			Application.OnThreadException(e);
		}

		/// <summary>Invokes the default window procedure associated with this window. </summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that is associated with the current Windows message. </param>
		// Token: 0x06000D46 RID: 3398 RVA: 0x0003A894 File Offset: 0x00038A94
		protected virtual void WndProc(ref Message m)
		{
			this.DefWndProc(ref m);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003A8A0 File Offset: 0x00038AA0
		internal static IntPtr WndProc(IntPtr hWnd, Msg msg, IntPtr wParam, IntPtr lParam)
		{
			IntPtr intPtr = IntPtr.Zero;
			Message message = default(Message);
			message.HWnd = hWnd;
			message.Msg = (int)msg;
			message.WParam = wParam;
			message.LParam = lParam;
			message.Result = IntPtr.Zero;
			NativeWindow nativeWindow = null;
			try
			{
				object obj = null;
				Hashtable hashtable = NativeWindow.window_collection;
				lock (hashtable)
				{
					obj = NativeWindow.window_collection[hWnd];
				}
				nativeWindow = obj as NativeWindow;
				if (obj == null)
				{
					nativeWindow = NativeWindow.EnsureCreated(nativeWindow, hWnd);
				}
				if (nativeWindow != null)
				{
					nativeWindow.WndProc(ref message);
					intPtr = message.Result;
				}
				else
				{
					if (obj is ArrayList)
					{
						ArrayList arrayList = (ArrayList)obj;
						ArrayList arrayList2 = arrayList;
						lock (arrayList2)
						{
							if (arrayList.Count > 0)
							{
								nativeWindow = NativeWindow.EnsureCreated((NativeWindow)arrayList[0], hWnd);
								nativeWindow.WndProc(ref message);
								intPtr = message.Result;
								for (int i = 1; i < arrayList.Count; i++)
								{
									((NativeWindow)arrayList[i]).WndProc(ref message);
								}
							}
							goto IL_0129;
						}
					}
					intPtr = XplatUI.DefWndProc(ref message);
				}
				IL_0129:;
			}
			catch (Exception ex)
			{
				if (nativeWindow != null)
				{
					if (msg == Msg.WM_PAINT && nativeWindow is Control.ControlNativeWindow)
					{
						Control owner = ((Control.ControlNativeWindow)nativeWindow).Owner;
						owner.Hide();
						new Control(owner.Parent, string.Empty)
						{
							BackColor = Color.White,
							ForeColor = Color.Red,
							Bounds = owner.Bounds
						}.Paint += NativeWindow.HandleRedCrossPaint;
					}
					nativeWindow.OnThreadException(ex);
				}
			}
			return intPtr;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003AA78 File Offset: 0x00038C78
		private static void HandleRedCrossPaint(object sender, PaintEventArgs e)
		{
			Control control = sender as Control;
			using (Pen pen = new Pen(control.ForeColor, 2f))
			{
				Rectangle displayRectangle = control.DisplayRectangle;
				e.Graphics.DrawRectangle(pen, displayRectangle.Left + 1, displayRectangle.Top + 1, displayRectangle.Width - 1, displayRectangle.Height - 1);
				e.Graphics.DrawLine(pen, displayRectangle.Location, displayRectangle.Location + displayRectangle.Size);
				e.Graphics.DrawLine(pen, new Point(displayRectangle.Left, displayRectangle.Bottom), new Point(displayRectangle.Right, displayRectangle.Top));
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003AB48 File Offset: 0x00038D48
		private static NativeWindow EnsureCreated(NativeWindow window, IntPtr hWnd)
		{
			if (window == null && NativeWindow.WindowCreating != null)
			{
				window = NativeWindow.WindowCreating;
				NativeWindow.WindowCreating = null;
				if (window.Handle == IntPtr.Zero)
				{
					window.AssignHandle(hWnd);
				}
			}
			return window;
		}

		// Token: 0x0400086A RID: 2154
		private IntPtr window_handle = IntPtr.Zero;

		// Token: 0x0400086B RID: 2155
		private static Hashtable window_collection = new Hashtable();

		// Token: 0x0400086C RID: 2156
		[ThreadStatic]
		private static NativeWindow WindowCreating;
	}
}
