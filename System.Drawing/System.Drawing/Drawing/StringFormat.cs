using System;
using System.ComponentModel;
using System.Drawing.Text;

namespace System.Drawing
{
	/// <summary>Encapsulates text layout information (such as alignment, orientation and tab stops) display manipulations (such as ellipsis insertion and national digit substitution) and OpenType features. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005A RID: 90
	public sealed class StringFormat : MarshalByRefObject, IDisposable, ICloneable
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		// Token: 0x0600033E RID: 830 RVA: 0x0000C334 File Offset: 0x0000A534
		public StringFormat()
			: this((StringFormatFlags)0, 0)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object with the specified <see cref="T:System.Drawing.StringFormatFlags" /> enumeration and language.</summary>
		/// <param name="options">The <see cref="T:System.Drawing.StringFormatFlags" /> enumeration for the new <see cref="T:System.Drawing.StringFormat" /> object. </param>
		/// <param name="language">A value that indicates the language of the text. </param>
		// Token: 0x0600033F RID: 831 RVA: 0x0000C33E File Offset: 0x0000A53E
		public StringFormat(StringFormatFlags options, int language)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			GDIPlus.CheckStatus(GDIPlus.GdipCreateStringFormat(options, language, out this.nativeStrFmt));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C363 File Offset: 0x0000A563
		internal StringFormat(IntPtr native)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			this.nativeStrFmt = native;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C380 File Offset: 0x0000A580
		~StringFormat()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000342 RID: 834 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C3BF File Offset: 0x0000A5BF
		private void Dispose(bool disposing)
		{
			if (this.nativeStrFmt != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteStringFormat(this.nativeStrFmt);
				this.nativeStrFmt = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object from the specified existing <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> object from which to initialize the new <see cref="T:System.Drawing.StringFormat" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null.</exception>
		// Token: 0x06000344 RID: 836 RVA: 0x0000C3EE File Offset: 0x0000A5EE
		public StringFormat(StringFormat format)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(format.NativeObject, out this.nativeStrFmt));
		}

		/// <summary>Gets or sets horizontal alignment of the string..</summary>
		/// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that specifies the horizontal  alignment of the string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000C428 File Offset: 0x0000A628
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000C448 File Offset: 0x0000A648
		public StringAlignment Alignment
		{
			get
			{
				StringAlignment stringAlignment;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatAlign(this.nativeStrFmt, out stringAlignment));
				return stringAlignment;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatAlign(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the vertical alignment of the string.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that represents the vertical line alignment.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000116 RID: 278
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000C46E File Offset: 0x0000A66E
		public StringAlignment LineAlignment
		{
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatLineAlign(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000C494 File Offset: 0x0000A694
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		public StringFormatFlags FormatFlags
		{
			get
			{
				StringFormatFlags stringFormatFlags;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatFlags(this.nativeStrFmt, out stringFormatFlags));
				return stringFormatFlags;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatFlags(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object, the default is <see cref="F:System.Drawing.Text.HotkeyPrefix.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000118 RID: 280
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000C4C7 File Offset: 0x0000A6C7
		public HotkeyPrefix HotkeyPrefix
		{
			set
			{
				if (value < HotkeyPrefix.None || value > HotkeyPrefix.Hide)
				{
					throw new InvalidEnumArgumentException("HotkeyPrefix");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatHotkeyPrefix(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.StringTrimming" /> enumeration for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringTrimming" /> enumeration that indicates how text drawn with this <see cref="T:System.Drawing.StringFormat" /> object is trimmed when it exceeds the edges of the layout rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000119 RID: 281
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000C4ED File Offset: 0x0000A6ED
		public StringTrimming Trimming
		{
			set
			{
				if (value < StringTrimming.None || value > StringTrimming.EllipsisPath)
				{
					throw new InvalidEnumArgumentException("Trimming");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTrimming(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets a generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>A generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000C514 File Offset: 0x0000A714
		public static StringFormat GenericTypographic
		{
			get
			{
				IntPtr intPtr;
				GDIPlus.CheckStatus(GDIPlus.GdipStringFormatGetGenericTypographic(out intPtr));
				return new StringFormat(intPtr);
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.StringFormat" /> object this method creates.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600034D RID: 845 RVA: 0x0000C534 File Offset: 0x0000A734
		public object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(this.nativeStrFmt, out intPtr));
			return new StringFormat(intPtr);
		}

		/// <summary>Converts this <see cref="T:System.Drawing.StringFormat" /> object to a human-readable string.</summary>
		/// <returns>A string representation of this <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600034E RID: 846 RVA: 0x0000C55C File Offset: 0x0000A75C
		public override string ToString()
		{
			return "[StringFormat, FormatFlags=" + this.FormatFlags.ToString() + "]";
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000C58C File Offset: 0x0000A78C
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeStrFmt;
			}
		}

		/// <summary>Sets tab stops for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <param name="firstTabOffset">The number of spaces between the beginning of a line of text and the first tab stop. </param>
		/// <param name="tabStops">An array of distances between tab stops in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000350 RID: 848 RVA: 0x0000C594 File Offset: 0x0000A794
		public void SetTabStops(float firstTabOffset, float[] tabStops)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTabStops(this.nativeStrFmt, firstTabOffset, tabStops.Length, tabStops));
		}

		// Token: 0x04000190 RID: 400
		private IntPtr nativeStrFmt;
	}
}
