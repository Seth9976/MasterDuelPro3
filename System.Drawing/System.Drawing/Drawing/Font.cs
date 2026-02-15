using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>Defines a particular format for text, including font face, size, and style attributes. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003E RID: 62
	[TypeConverter(typeof(FontConverter))]
	[Editor("System.Drawing.Design.FontEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	[Serializable]
	public sealed class Font : MarshalByRefObject, ISerializable, ICloneable, IDisposable
	{
		// Token: 0x0600021A RID: 538 RVA: 0x000071C4 File Offset: 0x000053C4
		private void CreateFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte charSet, bool isVertical)
		{
			this.originalFontName = familyName;
			FontFamily fontFamily;
			try
			{
				fontFamily = new FontFamily(familyName);
			}
			catch (Exception)
			{
				fontFamily = FontFamily.GenericSansSerif;
			}
			this.setProperties(fontFamily, emSize, style, unit, charSet, isVertical);
			Status status = GDIPlus.GdipCreateFont(fontFamily.NativeFamily, emSize, style, unit, out this.fontObject);
			if (status == Status.FontStyleNotFound)
			{
				throw new ArgumentException(Locale.GetText("Style {0} isn't supported by font {1}.", new object[]
				{
					style.ToString(),
					familyName
				}));
			}
			GDIPlus.CheckStatus(status);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007254 File Offset: 0x00005454
		private Font(SerializationInfo info, StreamingContext context)
		{
			string text = (string)info.GetValue("Name", typeof(string));
			float num = (float)info.GetValue("Size", typeof(float));
			FontStyle fontStyle = (FontStyle)info.GetValue("Style", typeof(FontStyle));
			GraphicsUnit graphicsUnit = (GraphicsUnit)info.GetValue("Unit", typeof(GraphicsUnit));
			this.CreateFont(text, num, fontStyle, graphicsUnit, 1, false);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x0600021C RID: 540 RVA: 0x000072EC File Offset: 0x000054EC
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Name", this.Name);
			si.AddValue("Size", this.Size);
			si.AddValue("Style", this.Style);
			si.AddValue("Unit", this.Unit);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007348 File Offset: 0x00005548
		~Font()
		{
			this.Dispose();
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Font" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600021E RID: 542 RVA: 0x00007374 File Offset: 0x00005574
		public void Dispose()
		{
			if (this.fontObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteFont(this.fontObject);
				this.fontObject = IntPtr.Zero;
				GC.SuppressFinalize(this);
				GDIPlus.CheckStatus(status);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000073AC File Offset: 0x000055AC
		internal void unitConversion(GraphicsUnit fromUnit, GraphicsUnit toUnit, float nSrc, out float nTrg)
		{
			nTrg = 0f;
			float num;
			switch (fromUnit)
			{
			case GraphicsUnit.World:
			case GraphicsUnit.Pixel:
				num = nSrc / Graphics.systemDpiX;
				break;
			case GraphicsUnit.Display:
				num = nSrc / 75f;
				break;
			case GraphicsUnit.Point:
				num = nSrc / 72f;
				break;
			case GraphicsUnit.Inch:
				num = nSrc;
				break;
			case GraphicsUnit.Document:
				num = nSrc / 300f;
				break;
			case GraphicsUnit.Millimeter:
				num = nSrc / 25.4f;
				break;
			default:
				throw new ArgumentException("Invalid GraphicsUnit");
			}
			switch (toUnit)
			{
			case GraphicsUnit.World:
			case GraphicsUnit.Pixel:
				nTrg = num * Graphics.systemDpiX;
				return;
			case GraphicsUnit.Display:
				nTrg = num * 75f;
				return;
			case GraphicsUnit.Point:
				nTrg = num * 72f;
				return;
			case GraphicsUnit.Inch:
				nTrg = num;
				return;
			case GraphicsUnit.Document:
				nTrg = num * 300f;
				return;
			case GraphicsUnit.Millimeter:
				nTrg = num * 25.4f;
				return;
			default:
				throw new ArgumentException("Invalid GraphicsUnit");
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007498 File Offset: 0x00005698
		private void setProperties(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte charSet, bool isVertical)
		{
			this._name = family.Name;
			this._fontFamily = family;
			this._size = emSize;
			this._unit = unit;
			this._style = style;
			this._gdiCharSet = charSet;
			this._gdiVerticalFont = isVertical;
			this.unitConversion(unit, GraphicsUnit.Point, emSize, out this._sizeInPoints);
			this._bold = (this._italic = (this._strikeout = (this._underline = false)));
			if ((style & FontStyle.Bold) == FontStyle.Bold)
			{
				this._bold = true;
			}
			if ((style & FontStyle.Italic) == FontStyle.Italic)
			{
				this._italic = true;
			}
			if ((style & FontStyle.Strikeout) == FontStyle.Strikeout)
			{
				this._strikeout = true;
			}
			if ((style & FontStyle.Underline) == FontStyle.Underline)
			{
				this._underline = true;
			}
		}

		/// <summary>Returns a handle to this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A Windows handle to this <see cref="T:System.Drawing.Font" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation was unsuccessful.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000221 RID: 545 RVA: 0x00007544 File Offset: 0x00005744
		public IntPtr ToHfont()
		{
			if (this.fontObject == IntPtr.Zero)
			{
				throw new ArgumentException(Locale.GetText("Object has been disposed."));
			}
			if (GDIPlus.RunningOnUnix())
			{
				return this.fontObject;
			}
			if (this.olf == null)
			{
				this.olf = default(LOGFONT);
				this.ToLogFont(this.olf);
			}
			LOGFONT logfont = (LOGFONT)this.olf;
			return GDIPlus.CreateFontIndirect(ref logfont);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> that uses the specified existing <see cref="T:System.Drawing.Font" /> and <see cref="T:System.Drawing.FontStyle" /> enumeration.</summary>
		/// <param name="prototype">The existing <see cref="T:System.Drawing.Font" /> from which to create the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="newStyle">The <see cref="T:System.Drawing.FontStyle" /> to apply to the new <see cref="T:System.Drawing.Font" />. Multiple values of the <see cref="T:System.Drawing.FontStyle" /> enumeration can be combined with the OR operator. </param>
		// Token: 0x06000222 RID: 546 RVA: 0x000075BC File Offset: 0x000057BC
		public Font(Font prototype, FontStyle newStyle)
		{
			this.setProperties(prototype.FontFamily, prototype.Size, newStyle, prototype.Unit, prototype.GdiCharSet, prototype.GdiVerticalFont);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFont(this._fontFamily.NativeFamily, this.Size, this.Style, this.Unit, out this.fontObject));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. Sets the style to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x06000223 RID: 547 RVA: 0x0000762C File Offset: 0x0000582C
		public Font(FontFamily family, float emSize, GraphicsUnit unit)
			: this(family, emSize, FontStyle.Regular, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. The style is set to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x06000224 RID: 548 RVA: 0x0000763A File Offset: 0x0000583A
		public Font(string familyName, float emSize, GraphicsUnit unit)
			: this(new FontFamily(familyName), emSize, FontStyle.Regular, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size. </summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x06000225 RID: 549 RVA: 0x0000764D File Offset: 0x0000584D
		public Font(FontFamily family, float emSize)
			: this(family, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style. </summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		// Token: 0x06000226 RID: 550 RVA: 0x0000765B File Offset: 0x0000585B
		public Font(FontFamily family, float emSize, FontStyle style)
			: this(family, emSize, style, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		// Token: 0x06000227 RID: 551 RVA: 0x00007669 File Offset: 0x00005869
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
			: this(family, emSize, style, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		// Token: 0x06000228 RID: 552 RVA: 0x00007678 File Offset: 0x00005878
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
			: this(family, emSize, style, unit, gdiCharSet, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
		/// <param name="gdiVerticalFont">A Boolean value indicating whether the new font is derived from a GDI vertical font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null </exception>
		// Token: 0x06000229 RID: 553 RVA: 0x00007688 File Offset: 0x00005888
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			if (family == null)
			{
				throw new ArgumentNullException("family");
			}
			this.setProperties(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFont(family.NativeFamily, emSize, style, unit, out this.fontObject));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size. </summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number. </exception>
		// Token: 0x0600022A RID: 554 RVA: 0x000076DD File Offset: 0x000058DD
		public Font(string familyName, float emSize)
			: this(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style. </summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x0600022B RID: 555 RVA: 0x000076EB File Offset: 0x000058EB
		public Font(string familyName, float emSize, FontStyle style)
			: this(familyName, emSize, style, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number. </exception>
		// Token: 0x0600022C RID: 556 RVA: 0x000076F9 File Offset: 0x000058F9
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
			: this(familyName, emSize, style, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x0600022D RID: 557 RVA: 0x00007708 File Offset: 0x00005908
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
			: this(familyName, emSize, style, unit, gdiCharSet, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using the specified size, style, unit, and character set.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
		/// <param name="gdiVerticalFont">A Boolean value indicating whether the new <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x0600022E RID: 558 RVA: 0x00007718 File Offset: 0x00005918
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			this.CreateFont(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000773A File Offset: 0x0000593A
		internal Font(string familyName, float emSize, string systemName)
			: this(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
			this.systemFontName = systemName;
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> this method creates, cast as an <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000230 RID: 560 RVA: 0x0000774F File Offset: 0x0000594F
		public object Clone()
		{
			return new Font(this, this.Style);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000775D File Offset: 0x0000595D
		internal IntPtr NativeObject
		{
			get
			{
				return this.fontObject;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is bold.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> is bold; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00007765 File Offset: 0x00005965
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Bold
		{
			get
			{
				return this._bold;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000776D File Offset: 0x0000596D
		[Browsable(false)]
		public FontFamily FontFamily
		{
			get
			{
				return this._fontFamily;
			}
		}

		/// <summary>Gets a byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses.</summary>
		/// <returns>A byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses. The default is 1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00007775 File Offset: 0x00005975
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte GdiCharSet
		{
			get
			{
				return this._gdiCharSet;
			}
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000777D File Offset: 0x0000597D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool GdiVerticalFont
		{
			get
			{
				return this._gdiVerticalFont;
			}
		}

		/// <summary>Gets the line spacing of this font.</summary>
		/// <returns>The line spacing, in pixels, of this font. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00007785 File Offset: 0x00005985
		[Browsable(false)]
		public int Height
		{
			get
			{
				return (int)Math.Ceiling((double)this.GetHeight());
			}
		}

		/// <summary>Gets a value that indicates whether this font has the italic style applied.</summary>
		/// <returns>true to indicate this font has the italic style applied; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00007794 File Offset: 0x00005994
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Italic
		{
			get
			{
				return this._italic;
			}
		}

		/// <summary>Gets the face name of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A string representation of the face name of this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000779C File Offset: 0x0000599C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the em-size of this <see cref="T:System.Drawing.Font" /> measured in the units specified by the <see cref="P:System.Drawing.Font.Unit" /> property.</summary>
		/// <returns>The em-size of this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000077A4 File Offset: 0x000059A4
		public float Size
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> specifies a horizontal line through the font.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> has a horizontal line through it; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000077AC File Offset: 0x000059AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Strikeout
		{
			get
			{
				return this._strikeout;
			}
		}

		/// <summary>Gets style information for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontStyle" /> enumeration that contains style information for this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000077B4 File Offset: 0x000059B4
		[Browsable(false)]
		public FontStyle Style
		{
			get
			{
				return this._style;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is underlined.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> is underlined; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000077BC File Offset: 0x000059BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Underline
		{
			get
			{
				return this._underline;
			}
		}

		/// <summary>Gets the unit of measure for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.GraphicsUnit" /> that represents the unit of measure for this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000077C4 File Offset: 0x000059C4
		[TypeConverter(typeof(FontConverter.FontUnitConverter))]
		public GraphicsUnit Unit
		{
			get
			{
				return this._unit;
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600023E RID: 574 RVA: 0x000077CC File Offset: 0x000059CC
		public override bool Equals(object obj)
		{
			Font font = obj as Font;
			return font != null && (font.FontFamily.Equals(this.FontFamily) && font.Size == this.Size && font.Style == this.Style && font.Unit == this.Unit && font.GdiCharSet == this.GdiCharSet && font.GdiVerticalFont == this.GdiVerticalFont);
		}

		/// <summary>Gets the hash code for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600023F RID: 575 RVA: 0x00007844 File Offset: 0x00005A44
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				this._hashCode = 17;
				this._hashCode = this._hashCode * 23 + this._name.GetHashCode();
				this._hashCode = this._hashCode * 23 + this.FontFamily.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._size.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._unit.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._style.GetHashCode();
				this._hashCode = this._hashCode * 23 + (int)this._gdiCharSet;
				this._hashCode = this._hashCode * 23 + this._gdiVerticalFont.GetHashCode();
			}
			return this._hashCode;
		}

		/// <summary>Returns the line spacing, in pixels, of this font. </summary>
		/// <returns>The line spacing, in pixels, of this font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000240 RID: 576 RVA: 0x0000792E File Offset: 0x00005B2E
		public float GetHeight()
		{
			return this.GetHeight(Graphics.systemDpiY);
		}

		/// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="logFont">An <see cref="T:System.Object" /> that represents the LOGFONT structure that this method creates. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000241 RID: 577 RVA: 0x0000793C File Offset: 0x00005B3C
		public void ToLogFont(object logFont)
		{
			if (GDIPlus.RunningOnUnix())
			{
				using (Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						this.ToLogFont(logFont, graphics);
						return;
					}
				}
			}
			IntPtr dc = GDIPlus.GetDC(IntPtr.Zero);
			try
			{
				using (Graphics graphics2 = Graphics.FromHdc(dc))
				{
					this.ToLogFont(logFont, graphics2);
				}
			}
			finally
			{
				GDIPlus.ReleaseDC(IntPtr.Zero, dc);
			}
		}

		/// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="logFont">An <see cref="T:System.Object" /> that represents the LOGFONT structure that this method creates. </param>
		/// <param name="graphics">A <see cref="T:System.Drawing.Graphics" /> that provides additional information for the LOGFONT structure. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000242 RID: 578 RVA: 0x000079EC File Offset: 0x00005BEC
		public void ToLogFont(object logFont, Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (logFont == null)
			{
				throw new AccessViolationException("logFont");
			}
			if (!logFont.GetType().GetTypeInfo().IsLayoutSequential)
			{
				throw new ArgumentException("logFont", Locale.GetText("Layout must be sequential."));
			}
			Type typeFromHandle = typeof(LOGFONT);
			int num = Marshal.SizeOf(logFont);
			if (num >= Marshal.SizeOf(typeFromHandle))
			{
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				Status status;
				try
				{
					Marshal.StructureToPtr(logFont, intPtr, false);
					status = GDIPlus.GdipGetLogFont(this.NativeObject, graphics.NativeObject, logFont);
					if (status != Status.Ok)
					{
						Marshal.PtrToStructure(intPtr, logFont);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (Font.CharSetOffset == -1)
				{
					Font.CharSetOffset = (int)Marshal.OffsetOf(typeFromHandle, "lfCharSet");
				}
				GCHandle gchandle = GCHandle.Alloc(logFont, GCHandleType.Pinned);
				try
				{
					IntPtr intPtr2 = gchandle.AddrOfPinnedObject();
					if (Marshal.ReadByte(intPtr2, Font.CharSetOffset) == 0)
					{
						Marshal.WriteByte(intPtr2, Font.CharSetOffset, 1);
					}
				}
				finally
				{
					gchandle.Free();
				}
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Returns the height, in pixels, of this <see cref="T:System.Drawing.Font" /> when drawn to a device with the specified vertical resolution.</summary>
		/// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Font" />.</returns>
		/// <param name="dpi">The vertical resolution, in dots per inch, used to calculate the height of the font. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000243 RID: 579 RVA: 0x00007B08 File Offset: 0x00005D08
		public float GetHeight(float dpi)
		{
			float num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetFontHeightGivenDPI(this.fontObject, dpi, out num));
			return num;
		}

		/// <summary>Returns a human-readable string representation of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000244 RID: 580 RVA: 0x00007B2C File Offset: 0x00005D2C
		public override string ToString()
		{
			return string.Format("[Font: Name={0}, Size={1}, Units={2}, GdiCharSet={3}, GdiVerticalFont={4}]", new object[]
			{
				this._name,
				this.Size,
				(int)this._unit,
				this._gdiCharSet,
				this._gdiVerticalFont
			});
		}

		// Token: 0x04000134 RID: 308
		private IntPtr fontObject = IntPtr.Zero;

		// Token: 0x04000135 RID: 309
		private string systemFontName;

		// Token: 0x04000136 RID: 310
		private string originalFontName;

		// Token: 0x04000137 RID: 311
		private float _size;

		// Token: 0x04000138 RID: 312
		private object olf;

		// Token: 0x04000139 RID: 313
		private static int CharSetOffset = -1;

		// Token: 0x0400013A RID: 314
		private bool _bold;

		// Token: 0x0400013B RID: 315
		private FontFamily _fontFamily;

		// Token: 0x0400013C RID: 316
		private byte _gdiCharSet;

		// Token: 0x0400013D RID: 317
		private bool _gdiVerticalFont;

		// Token: 0x0400013E RID: 318
		private bool _italic;

		// Token: 0x0400013F RID: 319
		private string _name;

		// Token: 0x04000140 RID: 320
		private float _sizeInPoints;

		// Token: 0x04000141 RID: 321
		private bool _strikeout;

		// Token: 0x04000142 RID: 322
		private FontStyle _style;

		// Token: 0x04000143 RID: 323
		private bool _underline;

		// Token: 0x04000144 RID: 324
		private GraphicsUnit _unit;

		// Token: 0x04000145 RID: 325
		private int _hashCode;
	}
}
