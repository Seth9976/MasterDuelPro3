using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines an array of colors that make up a color palette. The colors are 32-bit ARGB colors. Not inheritable.</summary>
	// Token: 0x02000082 RID: 130
	public sealed class ColorPalette
	{
		/// <summary>Gets an array of <see cref="T:System.Drawing.Color" /> structures.</summary>
		/// <returns>The array of <see cref="T:System.Drawing.Color" /> structure that make up this <see cref="T:System.Drawing.Imaging.ColorPalette" />.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000DA14 File Offset: 0x0000BC14
		public Color[] Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000DA1C File Offset: 0x0000BC1C
		internal ColorPalette()
		{
			this._entries = new Color[1];
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000DA30 File Offset: 0x0000BC30
		internal void ConvertFromMemory(IntPtr memory)
		{
			this._flags = Marshal.ReadInt32(memory);
			int num = Marshal.ReadInt32((IntPtr)((long)memory + 4L));
			this._entries = new Color[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = Marshal.ReadInt32((IntPtr)((long)memory + 8L + (long)(i * 4)));
				this._entries[i] = Color.FromArgb(num2);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		internal IntPtr ConvertToMemory()
		{
			int num = this._entries.Length;
			IntPtr intPtr;
			checked
			{
				intPtr = Marshal.AllocHGlobal(4 * (2 + num));
				Marshal.WriteInt32(intPtr, 0, this._flags);
				Marshal.WriteInt32((IntPtr)((long)intPtr + 4L), 0, num);
			}
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteInt32((IntPtr)((long)intPtr + (long)(4 * (i + 2))), 0, this._entries[i].ToArgb());
			}
			return intPtr;
		}

		// Token: 0x04000253 RID: 595
		private int _flags;

		// Token: 0x04000254 RID: 596
		private Color[] _entries;
	}
}
