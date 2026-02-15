using System;
using System.Reflection;
using System.Resources;

namespace System.Windows.Forms
{
	// Token: 0x020000F3 RID: 243
	internal class KeyboardLayouts
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x00024B50 File Offset: 0x00022D50
		public void LoadLayouts()
		{
			ResourceManager resourceManager = new ResourceManager("keyboards", Assembly.GetExecutingAssembly());
			this.keyboard_layouts = (KeyboardLayout[])resourceManager.GetObject("keyboard_table");
			this.vkey_table = (int[][])resourceManager.GetObject("vkey_table");
			this.scan_table = (short[][])resourceManager.GetObject("scan_table");
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00024BAF File Offset: 0x00022DAF
		public KeyboardLayout[] Layouts
		{
			get
			{
				if (this.keyboard_layouts == null)
				{
					this.LoadLayouts();
				}
				return this.keyboard_layouts;
			}
		}

		// Token: 0x04000571 RID: 1393
		private KeyboardLayout[] keyboard_layouts;

		// Token: 0x04000572 RID: 1394
		public int[][] vkey_table;

		// Token: 0x04000573 RID: 1395
		public short[][] scan_table;
	}
}
