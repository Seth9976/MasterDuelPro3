using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace System.Drawing
{
	/// <summary>Defines an object used to draw lines and curves. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Drawing.Pens" />
	// Token: 0x02000056 RID: 86
	public sealed class Pen : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000BBD2 File Offset: 0x00009DD2
		internal Pen(IntPtr p)
		{
			this.nativeObject = p;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" />.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the fill properties of this <see cref="T:System.Drawing.Pen" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		// Token: 0x06000318 RID: 792 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		public Pen(Brush brush)
			: this(brush, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified color.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />. </param>
		// Token: 0x06000319 RID: 793 RVA: 0x0000BBF6 File Offset: 0x00009DF6
		public Pen(Color color)
			: this(color, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="P:System.Drawing.Pen.Width" />.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the characteristics of this <see cref="T:System.Drawing.Pen" />. </param>
		/// <param name="width">The width of the new <see cref="T:System.Drawing.Pen" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		// Token: 0x0600031A RID: 794 RVA: 0x0000BC04 File Offset: 0x00009E04
		public Pen(Brush brush, float width)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePen2(brush.NativeBrush, width, GraphicsUnit.World, out this.nativeObject));
			this.color = Color.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Color" /> and <see cref="P:System.Drawing.Pen.Width" /> properties.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />. </param>
		/// <param name="width">A value indicating the width of this <see cref="T:System.Drawing.Pen" />. </param>
		// Token: 0x0600031B RID: 795 RVA: 0x0000BC44 File Offset: 0x00009E44
		public Pen(Color color, float width)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePen1(color.ToArgb(), width, GraphicsUnit.World, out this.nativeObject));
			this.color = color;
		}

		/// <summary>Gets or sets the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.DashStyle" /> that represents the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashStyle" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700010E RID: 270
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000BC74 File Offset: 0x00009E74
		public DashStyle DashStyle
		{
			set
			{
				if (value < DashStyle.Solid || value > DashStyle.Custom)
				{
					throw new InvalidEnumArgumentException("DashStyle", (int)value, typeof(DashStyle));
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenDashStyle(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Pen" />, in units of the <see cref="T:System.Drawing.Graphics" /> object used for drawing.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Width" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		public float Width
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenWidth(this.nativeObject, out num));
				return num;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		internal IntPtr NativePen
		{
			get
			{
				return this.nativeObject;
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast to a <see cref="T:System.Drawing.Pen" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600031F RID: 799 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		public object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipClonePen(this.nativeObject, out intPtr));
			return new Pen(intPtr)
			{
				startCap = this.startCap,
				endCap = this.endCap
			};
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000320 RID: 800 RVA: 0x0000BD2D File Offset: 0x00009F2D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000BD3C File Offset: 0x00009F3C
		private void Dispose(bool disposing)
		{
			if (disposing && !this.isModifiable)
			{
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
			if (this.nativeObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeletePen(this.nativeObject);
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000BD94 File Offset: 0x00009F94
		~Pen()
		{
			this.Dispose(false);
		}

		// Token: 0x04000187 RID: 391
		internal IntPtr nativeObject;

		// Token: 0x04000188 RID: 392
		internal bool isModifiable = true;

		// Token: 0x04000189 RID: 393
		private Color color;

		// Token: 0x0400018A RID: 394
		private CustomLineCap startCap;

		// Token: 0x0400018B RID: 395
		private CustomLineCap endCap;
	}
}
