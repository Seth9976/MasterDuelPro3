using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200006D RID: 109
	internal struct GdipPropertyItem
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0000D100 File Offset: 0x0000B300
		internal static void MarshalTo(GdipPropertyItem gdipProp, PropertyItem prop)
		{
			prop.Id = gdipProp.id;
			prop.Len = gdipProp.len;
			prop.Type = gdipProp.type;
			prop.Value = new byte[gdipProp.len];
			Marshal.Copy(gdipProp.value, prop.Value, 0, gdipProp.len);
		}

		// Token: 0x04000201 RID: 513
		internal int id;

		// Token: 0x04000202 RID: 514
		internal int len;

		// Token: 0x04000203 RID: 515
		internal short type;

		// Token: 0x04000204 RID: 516
		internal IntPtr value;
	}
}
