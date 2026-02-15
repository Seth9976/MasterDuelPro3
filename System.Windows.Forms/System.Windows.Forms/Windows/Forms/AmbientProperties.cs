using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides ambient property values to top-level controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000009 RID: 9
	public sealed class AmbientProperties
	{
		/// <summary>Gets or sets the ambient background color of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the background color of an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020E1 File Offset: 0x000002E1
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		/// <summary>Gets or sets the ambient cursor of an object.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor of an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020E9 File Offset: 0x000002E9
		public Cursor Cursor
		{
			get
			{
				return this.cursor;
			}
		}

		/// <summary>Gets or sets the ambient font of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font used when displaying text within an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020F1 File Offset: 0x000002F1
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Gets or sets the ambient foreground color of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the foreground color of an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020F9 File Offset: 0x000002F9
		public Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
		}

		// Token: 0x04000046 RID: 70
		private Color fore_color;

		// Token: 0x04000047 RID: 71
		private Color back_color;

		// Token: 0x04000048 RID: 72
		private Font font;

		// Token: 0x04000049 RID: 73
		private Cursor cursor;
	}
}
