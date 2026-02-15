using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the permissions related to user interfaces and the Clipboard. This class cannot be inherited.</summary>
	// Token: 0x02000370 RID: 880
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with either fully restricted or unrestricted access, as specified.</summary>
		/// <param name="state">One of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001EAE RID: 7854 RVA: 0x000796A9 File Offset: 0x000778A9
		public UIPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._clipboard = UIPermissionClipboard.AllClipboard;
				this._window = UIPermissionWindow.AllWindows;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the specified permissions for windows and the Clipboard.</summary>
		/// <param name="windowFlag">One of the enumeration values. </param>
		/// <param name="clipboardFlag">One of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="windowFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionWindow" /> value.-or- The <paramref name="clipboardFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> value. </exception>
		// Token: 0x06001EAF RID: 7855 RVA: 0x000796C9 File Offset: 0x000778C9
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
			this.Window = windowFlag;
		}

		/// <summary>Gets or sets the Clipboard access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values.</returns>
		// Token: 0x17000357 RID: 855
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x000796DF File Offset: 0x000778DF
		public UIPermissionClipboard Clipboard
		{
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionClipboard), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionClipboard");
				}
				this._clipboard = value;
			}
		}

		/// <summary>Gets or sets the window access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values.</returns>
		// Token: 0x17000358 RID: 856
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x0007971F File Offset: 0x0007791F
		public UIPermissionWindow Window
		{
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionWindow), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionWindow");
				}
				this._window = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001EB2 RID: 7858 RVA: 0x0007975F File Offset: 0x0007795F
		public override IPermission Copy()
		{
			return new UIPermission(this._window, this._clipboard);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06001EB3 RID: 7859 RVA: 0x00079774 File Offset: 0x00077974
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._window = UIPermissionWindow.AllWindows;
				this._clipboard = UIPermissionClipboard.AllClipboard;
				return;
			}
			string text = esd.Attribute("Window");
			if (text == null)
			{
				this._window = UIPermissionWindow.NoWindows;
			}
			else
			{
				this._window = (UIPermissionWindow)Enum.Parse(typeof(UIPermissionWindow), text);
			}
			string text2 = esd.Attribute("Clipboard");
			if (text2 == null)
			{
				this._clipboard = UIPermissionClipboard.NoClipboard;
				return;
			}
			this._clipboard = (UIPermissionClipboard)Enum.Parse(typeof(UIPermissionClipboard), text2);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001EB4 RID: 7860 RVA: 0x0007980C File Offset: 0x00077A0C
		public override IPermission Intersect(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return null;
			}
			UIPermissionWindow uipermissionWindow = ((this._window < uipermission._window) ? this._window : uipermission._window);
			UIPermissionClipboard uipermissionClipboard = ((this._clipboard < uipermission._clipboard) ? this._clipboard : uipermission._clipboard);
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001EB5 RID: 7861 RVA: 0x00079874 File Offset: 0x00077A74
		public override bool IsSubsetOf(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.IsEmpty(this._window, this._clipboard);
			}
			return uipermission.IsUnrestricted() || (this._window <= uipermission._window && this._clipboard <= uipermission._clipboard);
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06001EB6 RID: 7862 RVA: 0x000798CA File Offset: 0x00077ACA
		public bool IsUnrestricted()
		{
			return this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06001EB7 RID: 7863 RVA: 0x000798E0 File Offset: 0x00077AE0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				if (this._window != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", this._window.ToString());
				}
				if (this._clipboard != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", this._clipboard.ToString());
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001EB8 RID: 7864 RVA: 0x00079964 File Offset: 0x00077B64
		public override IPermission Union(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.Copy();
			}
			UIPermissionWindow uipermissionWindow = ((this._window > uipermission._window) ? this._window : uipermission._window);
			UIPermissionClipboard uipermissionClipboard = ((this._clipboard > uipermission._clipboard) ? this._clipboard : uipermission._clipboard);
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000799D0 File Offset: 0x00077BD0
		private bool IsEmpty(UIPermissionWindow w, UIPermissionClipboard c)
		{
			return w == UIPermissionWindow.NoWindows && c == UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000799DB File Offset: 0x00077BDB
		private UIPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UIPermission uipermission = target as UIPermission;
			if (uipermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UIPermission));
			}
			return uipermission;
		}

		// Token: 0x04000E42 RID: 3650
		private UIPermissionWindow _window;

		// Token: 0x04000E43 RID: 3651
		private UIPermissionClipboard _clipboard;
	}
}
