using System;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates the information needed when creating a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005C RID: 92
	public class CreateParams
	{
		/// <summary>Gets or sets the control's initial text.</summary>
		/// <returns>The control's initial text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		/// <summary>Gets or sets the name of the Windows class to derive the control from.</summary>
		/// <returns>The name of the Windows class to derive the control from.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00011005 File Offset: 0x0000F205
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001100D File Offset: 0x0000F20D
		public string ClassName
		{
			get
			{
				return this.class_name;
			}
			set
			{
				this.class_name = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of class style values.</summary>
		/// <returns>A bitwise combination of the class style values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00011016 File Offset: 0x0000F216
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001101E File Offset: 0x0000F21E
		public int ClassStyle
		{
			get
			{
				return this.class_style;
			}
			set
			{
				this.class_style = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of extended window style values.</summary>
		/// <returns>A bitwise combination of the extended window style values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00011027 File Offset: 0x0000F227
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001102F File Offset: 0x0000F22F
		public int ExStyle
		{
			get
			{
				return this.ex_style;
			}
			set
			{
				this.ex_style = value;
			}
		}

		/// <summary>Gets or sets the initial left position of the control.</summary>
		/// <returns>The numeric value that represents the initial left position of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00011038 File Offset: 0x0000F238
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00011040 File Offset: 0x0000F240
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets or sets the top position of the initial location of the control.</summary>
		/// <returns>The numeric value that represents the top position of the initial location of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00011049 File Offset: 0x0000F249
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00011051 File Offset: 0x0000F251
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>Gets or sets the initial width of the control.</summary>
		/// <returns>The numeric value that represents the initial width of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001105A File Offset: 0x0000F25A
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00011062 File Offset: 0x0000F262
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the initial height of the control.</summary>
		/// <returns>The numeric value that represents the initial height of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001106B File Offset: 0x0000F26B
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00011073 File Offset: 0x0000F273
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of window style values.</summary>
		/// <returns>A bitwise combination of the window style values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001107C File Offset: 0x0000F27C
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00011084 File Offset: 0x0000F284
		public int Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		/// <summary>Gets or sets additional parameter information needed to create the control.</summary>
		/// <returns>The <see cref="T:System.Object" /> that holds additional parameter information needed to create the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000102 RID: 258
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0001108D File Offset: 0x0000F28D
		public object Param
		{
			set
			{
				this.param = value;
			}
		}

		/// <summary>Gets or sets the control's parent.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle of the control's parent.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00011096 File Offset: 0x0000F296
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0001109E File Offset: 0x0000F29E
		public IntPtr Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000110A7 File Offset: 0x0000F2A7
		internal bool IsSet(WindowStyles Style)
		{
			return (this.style & (int)Style) == (int)Style;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000110B4 File Offset: 0x0000F2B4
		internal bool IsSet(WindowExStyles ExStyle)
		{
			return (this.ex_style & (int)ExStyle) == (int)ExStyle;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000110C1 File Offset: 0x0000F2C1
		internal static bool IsSet(WindowStyles Style, WindowStyles Option)
		{
			return (Option & Style) == Option;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000110CC File Offset: 0x0000F2CC
		internal bool HasWindowManager
		{
			get
			{
				if (this.control == null)
				{
					return false;
				}
				Form form = this.control as Form;
				return form != null && form.window_manager != null;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00011027 File Offset: 0x0000F227
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0001102F File Offset: 0x0000F22F
		internal WindowExStyles WindowExStyle
		{
			get
			{
				return (WindowExStyles)this.ex_style;
			}
			set
			{
				this.ex_style = (int)value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001107C File Offset: 0x0000F27C
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00011084 File Offset: 0x0000F284
		internal WindowStyles WindowStyle
		{
			get
			{
				return (WindowStyles)this.style;
			}
			set
			{
				this.style = (int)value;
			}
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000475 RID: 1141 RVA: 0x00011100 File Offset: 0x0000F300
		public override string ToString()
		{
			return string.Format("CreateParams {{'{0}', '{1}', 0x{2:X}, 0x{3:X}, {{{4}, {5}, {6}, {7}}}}}", new object[] { this.class_name, this.caption, this.class_style, this.ex_style, this.x, this.y, this.width, this.height });
		}

		// Token: 0x04000246 RID: 582
		private string caption;

		// Token: 0x04000247 RID: 583
		private string class_name;

		// Token: 0x04000248 RID: 584
		private int class_style;

		// Token: 0x04000249 RID: 585
		private int ex_style;

		// Token: 0x0400024A RID: 586
		private int x;

		// Token: 0x0400024B RID: 587
		private int y;

		// Token: 0x0400024C RID: 588
		private int height;

		// Token: 0x0400024D RID: 589
		private int width;

		// Token: 0x0400024E RID: 590
		private int style;

		// Token: 0x0400024F RID: 591
		private object param;

		// Token: 0x04000250 RID: 592
		private IntPtr parent;

		// Token: 0x04000251 RID: 593
		internal Menu menu;

		// Token: 0x04000252 RID: 594
		internal Control control;
	}
}
