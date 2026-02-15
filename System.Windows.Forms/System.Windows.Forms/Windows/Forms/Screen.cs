using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a display device or multiple display devices on a single system.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000178 RID: 376
	public class Screen
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x00040A14 File Offset: 0x0003EC14
		static Screen()
		{
			try
			{
				Screen.all_screens = XplatUI.AllScreens;
			}
			catch (Exception ex)
			{
				Console.WriteLine("{0} trying to get all screens: {1}", ex.GetType(), ex.Message);
			}
			if (Screen.all_screens == null || Screen.all_screens.Length == 0)
			{
				Screen.all_screens = new Screen[]
				{
					new Screen(true, "Mono MWF Primary Display", XplatUI.VirtualScreen, XplatUI.WorkingArea)
				};
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00040A88 File Offset: 0x0003EC88
		internal Screen(bool primary, string name, Rectangle bounds, Rectangle workarea)
		{
			this.primary = primary;
			this.name = name;
			this.bounds = bounds;
			this.workarea = workarea;
			this.bits_per_pixel = 32;
		}

		/// <summary>Gets an array of all displays on the system.</summary>
		/// <returns>An array of type <see cref="T:System.Windows.Forms.Screen" />, containing all displays on the system.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00040AB5 File Offset: 0x0003ECB5
		public static Screen[] AllScreens
		{
			get
			{
				return Screen.all_screens;
			}
		}

		/// <summary>Gets the primary display.</summary>
		/// <returns>The primary display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00040ABC File Offset: 0x0003ECBC
		public static Screen PrimaryScreen
		{
			get
			{
				return Screen.all_screens[0];
			}
		}

		/// <summary>Gets the bounds of the display.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the bounds of the display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00040AC5 File Offset: 0x0003ECC5
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the device name associated with a display.</summary>
		/// <returns>The device name associated with a display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00040ACD File Offset: 0x0003ECCD
		public string DeviceName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a value indicating whether a particular display is the primary device.</summary>
		/// <returns>true if this display is primary; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00040AD5 File Offset: 0x0003ECD5
		public bool Primary
		{
			get
			{
				return this.primary;
			}
		}

		/// <summary>Gets the working area of the display. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the working area of the display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00040ADD File Offset: 0x0003ECDD
		public Rectangle WorkingArea
		{
			get
			{
				return this.workarea;
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the specified control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the specified control. In multiple display environments where no display contains the control, the display closest to the specified control is returned.</returns>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000E2E RID: 3630 RVA: 0x00040AE5 File Offset: 0x0003ECE5
		public static Screen FromControl(Control control)
		{
			return Screen.FromPoint((control.Parent != null) ? control.Parent.PointToScreen(control.Location) : control.Location);
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the specified point.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the point. In multiple display environments where no display contains the point, the display closest to the specified point is returned.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E2F RID: 3631 RVA: 0x00040B10 File Offset: 0x0003ED10
		public static Screen FromPoint(Point point)
		{
			for (int i = 0; i < Screen.all_screens.Length; i++)
			{
				if (Screen.all_screens[i].Bounds.Contains(point))
				{
					return Screen.all_screens[i];
				}
			}
			return Screen.PrimaryScreen;
		}

		/// <summary>Retrieves the working area for the display that contains the largest region of the specified control. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified control, the display closest to the control is returned.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the working area. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000E30 RID: 3632 RVA: 0x00040B53 File Offset: 0x0003ED53
		public static Rectangle GetWorkingArea(Control ctl)
		{
			return Screen.FromControl(ctl).WorkingArea;
		}

		/// <summary>Gets or sets a value indicating whether the specified object is equal to this Screen.</summary>
		/// <returns>true if the specified object is equal to this <see cref="T:System.Windows.Forms.Screen" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this <see cref="T:System.Windows.Forms.Screen" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E31 RID: 3633 RVA: 0x00040B60 File Offset: 0x0003ED60
		public override bool Equals(object obj)
		{
			if (obj is Screen)
			{
				Screen screen = (Screen)obj;
				if (this.name.Equals(screen.name) && this.primary == screen.primary && this.bounds.Equals(screen.Bounds) && this.workarea.Equals(screen.workarea))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Computes and retrieves a hash code for an object.</summary>
		/// <returns>A hash code for an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E32 RID: 3634 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Retrieves a string representing this object.</summary>
		/// <returns>A string representation of the object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E33 RID: 3635 RVA: 0x00040BDC File Offset: 0x0003EDDC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Screen[Bounds={",
				this.Bounds,
				"} WorkingArea={",
				this.WorkingArea,
				"} Primary={",
				this.Primary.ToString(),
				"} DeviceName=",
				this.DeviceName
			});
		}

		// Token: 0x04000914 RID: 2324
		private static Screen[] all_screens;

		// Token: 0x04000915 RID: 2325
		private bool primary;

		// Token: 0x04000916 RID: 2326
		private Rectangle bounds;

		// Token: 0x04000917 RID: 2327
		private Rectangle workarea;

		// Token: 0x04000918 RID: 2328
		private string name;

		// Token: 0x04000919 RID: 2329
		private int bits_per_pixel;
	}
}
