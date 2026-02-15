using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x02000399 RID: 921
	internal class Cursor
	{
		// Token: 0x06001DC3 RID: 7619 RVA: 0x00093F6C File Offset: 0x0009216C
		internal static IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			return (IntPtr)GCHandle.Alloc(new CarbonCursor(bitmap, mask, cursor_pixel, mask_pixel, xHotSpot, yHotSpot));
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00093F8A File Offset: 0x0009218A
		internal static IntPtr DefineStdCursor(StdCursor id)
		{
			return (IntPtr)GCHandle.Alloc(new CarbonCursor(id));
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00093FA4 File Offset: 0x000921A4
		internal static void SetCursor(IntPtr cursor)
		{
			if (cursor == IntPtr.Zero)
			{
				Cursor.defcur.SetCursor();
				return;
			}
			((CarbonCursor)((GCHandle)cursor).Target).SetCursor();
		}

		// Token: 0x04001CE2 RID: 7394
		internal static CarbonCursor defcur = new CarbonCursor(StdCursor.Default);
	}
}
