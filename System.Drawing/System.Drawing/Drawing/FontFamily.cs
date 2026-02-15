using System;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines a group of type faces having a similar basic design and certain variations in styles. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000042 RID: 66
	public sealed class FontFamily : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000826E File Offset: 0x0000646E
		internal FontFamily(IntPtr fntfamily)
		{
			this.nativeFontFamily = fntfamily;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008288 File Offset: 0x00006488
		internal unsafe void refreshName()
		{
			if (this.nativeFontFamily == IntPtr.Zero)
			{
				return;
			}
			char* ptr = stackalloc char[(UIntPtr)64];
			GDIPlus.CheckStatus(GDIPlus.GdipGetFamilyName(this.nativeFontFamily, (IntPtr)((void*)ptr), 0));
			this.name = Marshal.PtrToStringUni((IntPtr)((void*)ptr));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000082D8 File Offset: 0x000064D8
		~FontFamily()
		{
			this.Dispose();
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00008304 File Offset: 0x00006504
		internal IntPtr NativeFamily
		{
			get
			{
				return this.nativeFontFamily;
			}
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> from the specified generic font family.</summary>
		/// <param name="genericFamily">The <see cref="T:System.Drawing.Text.GenericFontFamilies" /> from which to create the new <see cref="T:System.Drawing.FontFamily" />. </param>
		// Token: 0x0600025D RID: 605 RVA: 0x0000830C File Offset: 0x0000650C
		public FontFamily(GenericFontFamilies genericFamily)
		{
			Status status;
			switch (genericFamily)
			{
			case GenericFontFamilies.Serif:
				status = GDIPlus.GdipGetGenericFontFamilySerif(out this.nativeFontFamily);
				goto IL_004D;
			case GenericFontFamilies.SansSerif:
				status = GDIPlus.GdipGetGenericFontFamilySansSerif(out this.nativeFontFamily);
				goto IL_004D;
			}
			status = GDIPlus.GdipGetGenericFontFamilyMonospace(out this.nativeFontFamily);
			IL_004D:
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> with the specified name.</summary>
		/// <param name="name">The name of the new <see cref="T:System.Drawing.FontFamily" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").-or-<paramref name="name" /> specifies a font that is not installed on the computer running the application.-or-<paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x0600025E RID: 606 RVA: 0x0000836C File Offset: 0x0000656C
		public FontFamily(string name)
			: this(name, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> in the specified <see cref="T:System.Drawing.Text.FontCollection" /> with the specified name.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the name of the new <see cref="T:System.Drawing.FontFamily" />. </param>
		/// <param name="fontCollection">The <see cref="T:System.Drawing.Text.FontCollection" /> that contains this <see cref="T:System.Drawing.FontFamily" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").-or-<paramref name="name" /> specifies a font that is not installed on the computer running the application.-or-<paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x0600025F RID: 607 RVA: 0x00008378 File Offset: 0x00006578
		public FontFamily(string name, FontCollection fontCollection)
		{
			IntPtr intPtr = ((fontCollection == null) ? IntPtr.Zero : fontCollection._nativeFontCollection);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFamilyFromName(name, intPtr, out this.nativeFontFamily));
		}

		/// <summary>Gets the name of this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of this <see cref="T:System.Drawing.FontFamily" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000083B9 File Offset: 0x000065B9
		public string Name
		{
			get
			{
				if (this.nativeFontFamily == IntPtr.Zero)
				{
					throw new ArgumentException("Name", Locale.GetText("Object was disposed."));
				}
				if (this.name == null)
				{
					this.refreshName();
				}
				return this.name;
			}
		}

		/// <summary>Gets a generic sans serif <see cref="T:System.Drawing.FontFamily" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> object that represents a generic sans serif font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000083F6 File Offset: 0x000065F6
		public static FontFamily GenericSansSerif
		{
			get
			{
				return new FontFamily(GenericFontFamilies.SansSerif);
			}
		}

		/// <summary>Returns the cell ascent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
		/// <returns>The cell ascent for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000262 RID: 610 RVA: 0x00008400 File Offset: 0x00006600
		public int GetCellAscent(FontStyle style)
		{
			short num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellAscent(this.nativeFontFamily, (int)style, out num));
			return (int)num;
		}

		/// <summary>Returns the cell descent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style. </summary>
		/// <returns>The cell descent metric for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000263 RID: 611 RVA: 0x00008424 File Offset: 0x00006624
		public int GetCellDescent(FontStyle style)
		{
			short num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellDescent(this.nativeFontFamily, (int)style, out num));
			return (int)num;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000264 RID: 612 RVA: 0x00008445 File Offset: 0x00006645
		public void Dispose()
		{
			if (this.nativeFontFamily != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteFontFamily(this.nativeFontFamily);
				this.nativeFontFamily = IntPtr.Zero;
				GC.SuppressFinalize(this);
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000265 RID: 613 RVA: 0x0000847C File Offset: 0x0000667C
		public override bool Equals(object obj)
		{
			FontFamily fontFamily = obj as FontFamily;
			return fontFamily != null && this.Name == fontFamily.Name;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.FontFamily" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000266 RID: 614 RVA: 0x000084A6 File Offset: 0x000066A6
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000084B3 File Offset: 0x000066B3
		public static FontFamily[] Families
		{
			get
			{
				return new InstalledFontCollection().Families;
			}
		}

		/// <summary>Converts this <see cref="T:System.Drawing.FontFamily" /> to a human-readable string representation.</summary>
		/// <returns>The string that represents this <see cref="T:System.Drawing.FontFamily" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000268 RID: 616 RVA: 0x000084BF File Offset: 0x000066BF
		public override string ToString()
		{
			return "[FontFamily: Name=" + this.Name + "]";
		}

		// Token: 0x04000147 RID: 327
		private string name;

		// Token: 0x04000148 RID: 328
		private IntPtr nativeFontFamily = IntPtr.Zero;
	}
}
