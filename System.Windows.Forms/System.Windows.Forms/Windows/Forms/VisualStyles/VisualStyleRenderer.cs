using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Provides methods for drawing and getting information about a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />. This class cannot be inherited.</summary>
	// Token: 0x0200036A RID: 874
	public sealed class VisualStyleRenderer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class using the given class, part, and state values.</summary>
		/// <param name="className">The class name of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <param name="part">The part of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <param name="state">The state of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <exception cref="T:System.ArgumentException">The combination of <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> is not defined by the current visual style.</exception>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x00086EC2 File Offset: 0x000850C2
		public VisualStyleRenderer(string className, int part, int state)
		{
			this.theme_handle_manager.VisualStyleRenderer = this;
			this.SetParameters(className, part, state);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class using the given <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />.</summary>
		/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="element" /> is not defined by the current visual style.</exception>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x00086EEA File Offset: 0x000850EA
		public VisualStyleRenderer(VisualStyleElement element)
			: this(element.ClassName, element.Part, element.State)
		{
		}

		/// <summary>Gets a unique identifier for the current class of visual style elements.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that identifies a set of data that defines the class of elements specified by <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00086F04 File Offset: 0x00085104
		public IntPtr Handle
		{
			get
			{
				return this.theme;
			}
		}

		/// <summary>Gets a value specifying whether the operating system is using visual styles to draw controls.</summary>
		/// <returns>true if the operating system supports visual styles, the user has enabled visual styles in the operating system, and visual styles are applied to the client area of application windows; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00086F0C File Offset: 0x0008510C
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}

		/// <summary>Determines whether the specified visual style element is defined by the current visual style.</summary>
		/// <returns>true if the combination of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" /> and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.Part" /> properties of <paramref name="element" /> are defined; otherwise, false. </returns>
		/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> whose class and part combination will be verified.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x00086F2C File Offset: 0x0008512C
		public static bool IsElementDefined(VisualStyleElement element)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				throw new InvalidOperationException("Visual Styles are not enabled.");
			}
			if (VisualStyleRenderer.IsElementKnownToBeSupported(element.ClassName, element.Part, element.State))
			{
				return true;
			}
			IntPtr intPtr = VisualStyleRenderer.VisualStyles.UxThemeOpenThemeData(IntPtr.Zero, element.ClassName);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			bool flag = VisualStyleRenderer.VisualStyles.UxThemeIsThemePartDefined(intPtr, element.Part);
			VisualStyleRenderer.VisualStyles.UxThemeCloseThemeData(intPtr);
			return flag;
		}

		/// <summary>Draws the background image of the current visual style element within the specified bounding rectangle.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background image.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the background image is drawn.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06001CA6 RID: 7334 RVA: 0x00086FA8 File Offset: 0x000851A8
		public void DrawBackground(IDeviceContext dc, Rectangle bounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeBackground(this.theme, dc, this.part, this.state, bounds);
		}

		/// <summary>Draws the background image of the current visual style element within the specified bounding rectangle and clipped to the specified clipping rectangle.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background image.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the background image is drawn.</param>
		/// <param name="clipRectangle">A <see cref="T:System.Drawing.Rectangle" /> that defines a clipping rectangle for the drawing operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06001CA7 RID: 7335 RVA: 0x00086FDC File Offset: 0x000851DC
		public void DrawBackground(IDeviceContext dc, Rectangle bounds, Rectangle clipRectangle)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeBackground(this.theme, dc, this.part, this.state, bounds, clipRectangle);
		}

		/// <summary>Draws the specified image within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the image.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the image is drawn.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> or <paramref name="image" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CA8 RID: 7336 RVA: 0x00087011 File Offset: 0x00085211
		public void DrawImage(Graphics g, Rectangle bounds, Image image)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			g.DrawImage(image, bounds);
		}

		/// <summary>Draws text in the specified bounding rectangle with the option of displaying disabled text and applying other text formatting.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the text.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the text.</param>
		/// <param name="textToDraw">The text to draw.</param>
		/// <param name="drawDisabled">true to draw grayed-out text; otherwise, false.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06001CA9 RID: 7337 RVA: 0x00087038 File Offset: 0x00085238
		public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw, bool drawDisabled, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeText(this.theme, dc, this.part, this.state, textToDraw, flags, bounds);
		}

		/// <summary>Returns the region for the background of the current visual style element.</summary>
		/// <returns>The <see cref="T:System.Drawing.Region" /> that contains the background of the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06001CAA RID: 7338 RVA: 0x0008707C File Offset: 0x0008527C
		public Region GetBackgroundRegion(IDeviceContext dc, Rectangle bounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Region region;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeBackgroundRegion(this.theme, dc, this.part, this.state, bounds, out region);
			return region;
		}

		/// <summary>Returns the value of the specified color property for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.ColorProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ColorProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CAB RID: 7339 RVA: 0x000870C0 File Offset: 0x000852C0
		public Color GetColor(ColorProperty prop)
		{
			if (!Enum.IsDefined(typeof(ColorProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(ColorProperty));
			}
			Color color;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeColor(this.theme, this.part, this.state, prop, out color);
			return color;
		}

		/// <summary>Returns the value of the specified size property of the current visual style part.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the size specified by the <paramref name="type" /> parameter for the current visual style part. </returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
		// Token: 0x06001CAC RID: 7340 RVA: 0x00087120 File Offset: 0x00085320
		public Size GetPartSize(IDeviceContext dc, ThemeSizeType type)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!Enum.IsDefined(typeof(ThemeSizeType), type))
			{
				throw new InvalidEnumArgumentException("prop", (int)type, typeof(ThemeSizeType));
			}
			Size size;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemePartSize(this.theme, dc, this.part, this.state, type, out size);
			return size;
		}

		/// <summary>Returns the size and location of the specified string when drawn with the font of the current visual style element within the specified initial bounding rectangle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the area required to fit the rendered text. </returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> used to control the flow and wrapping of the text.</param>
		/// <param name="textToDraw">The string to measure.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06001CAD RID: 7341 RVA: 0x00087190 File Offset: 0x00085390
		public Rectangle GetTextExtent(IDeviceContext dc, Rectangle bounds, string textToDraw, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Rectangle rectangle;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeTextExtent(this.theme, dc, this.part, this.state, textToDraw, flags, bounds, out rectangle);
			return rectangle;
		}

		/// <summary>Indicates whether the background of the current visual style element has any semitransparent or alpha-blended pieces.</summary>
		/// <returns>true if the background of the current visual style element has any semitransparent or alpha-blended pieces; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CAE RID: 7342 RVA: 0x000871D5 File Offset: 0x000853D5
		public bool IsBackgroundPartiallyTransparent()
		{
			return VisualStyleRenderer.VisualStyles.UxThemeIsThemeBackgroundPartiallyTransparent(this.theme, this.part, this.state);
		}

		/// <summary>Sets this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to the visual style element represented by the specified class, part, and state values.</summary>
		/// <param name="className">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" /> property.</param>
		/// <param name="part">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Part" /> property.</param>
		/// <param name="state">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.State" /> property.</param>
		/// <exception cref="T:System.ArgumentException">The combination of <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> is not defined by the current visual style.</exception>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CAF RID: 7343 RVA: 0x000871F4 File Offset: 0x000853F4
		public void SetParameters(string className, int part, int state)
		{
			if (this.theme != IntPtr.Zero)
			{
				this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeCloseThemeData(this.theme);
			}
			if (!VisualStyleRenderer.IsSupported)
			{
				throw new InvalidOperationException("Visual Styles are not enabled.");
			}
			this.class_name = className;
			this.part = part;
			this.state = state;
			this.theme = VisualStyleRenderer.VisualStyles.UxThemeOpenThemeData(IntPtr.Zero, this.class_name);
			if (VisualStyleRenderer.IsElementKnownToBeSupported(className, part, state))
			{
				return;
			}
			if (this.theme == IntPtr.Zero || !VisualStyleRenderer.VisualStyles.UxThemeIsThemePartDefined(this.theme, this.part))
			{
				throw new ArgumentException("This element is not supported by the current visual style.");
			}
		}

		/// <summary>Sets this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to the visual style element represented by the specified <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />.</summary>
		/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that specifies the new values of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" />, <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Part" />, and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.State" /> properties.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="element" /> is not defined by the current visual style.</exception>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CB0 RID: 7344 RVA: 0x000872AB File Offset: 0x000854AB
		public void SetParameters(VisualStyleElement element)
		{
			this.SetParameters(element.ClassName, element.Part, element.State);
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x00086EBB File Offset: 0x000850BB
		internal static IVisualStyles VisualStyles
		{
			get
			{
				return VisualStylesEngine.Instance;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000872C5 File Offset: 0x000854C5
		internal void DrawBackgroundExcludingArea(IDeviceContext dc, Rectangle bounds, Rectangle excludedArea)
		{
			VisualStyleRenderer.VisualStyles.VisualStyleRendererDrawBackgroundExcludingArea(this.theme, dc, this.part, this.state, bounds, excludedArea);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000872E6 File Offset: 0x000854E6
		private static bool IsElementKnownToBeSupported(string className, int part, int state)
		{
			return className == "STATUS" && part == 0 && state == 0;
		}

		// Token: 0x0400181B RID: 6171
		private string class_name;

		// Token: 0x0400181C RID: 6172
		private int part;

		// Token: 0x0400181D RID: 6173
		private int state;

		// Token: 0x0400181E RID: 6174
		private IntPtr theme;

		// Token: 0x0400181F RID: 6175
		private int last_hresult;

		// Token: 0x04001820 RID: 6176
		private VisualStyleRenderer.ThemeHandleManager theme_handle_manager = new VisualStyleRenderer.ThemeHandleManager();

		// Token: 0x0200036B RID: 875
		private class ThemeHandleManager
		{
			// Token: 0x06001CB4 RID: 7348 RVA: 0x00087300 File Offset: 0x00085500
			~ThemeHandleManager()
			{
				if (!(this.VisualStyleRenderer.theme == IntPtr.Zero))
				{
					VisualStyleRenderer.VisualStyles.UxThemeCloseThemeData(this.VisualStyleRenderer.theme);
				}
			}

			// Token: 0x04001821 RID: 6177
			public VisualStyleRenderer VisualStyleRenderer;
		}
	}
}
