using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003A8 RID: 936
	internal class Pasteboard
	{
		// Token: 0x06001E1B RID: 7707 RVA: 0x00095994 File Offset: 0x00093B94
		static Pasteboard()
		{
			Pasteboard.PasteboardCreate(XplatUICarbon.__CFStringMakeConstantString("com.apple.pasteboard.clipboard"), ref Pasteboard.primary_pbref);
			Pasteboard.PasteboardCreate(IntPtr.Zero, ref Pasteboard.app_pbref);
			Pasteboard.internal_format = XplatUICarbon.__CFStringMakeConstantString("com.novell.mono.mwf.pasteboard");
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000959CC File Offset: 0x00093BCC
		internal static object Retrieve(IntPtr pbref, int key)
		{
			uint num = 0U;
			key = (int)Pasteboard.internal_format;
			Pasteboard.PasteboardGetItemCount(pbref, ref num);
			int num2 = 1;
			while ((long)num2 <= (long)((ulong)num))
			{
				uint num3 = 0U;
				Pasteboard.PasteboardGetItemIdentifier(pbref, (uint)num2, ref num3);
				if (num3 == 64206U)
				{
					IntPtr zero = IntPtr.Zero;
					Pasteboard.PasteboardCopyItemFlavorData(pbref, 64206U, (uint)key, ref zero);
					if (zero != IntPtr.Zero)
					{
						return ((GCHandle)Marshal.ReadIntPtr(Pasteboard.CFDataGetBytePtr(zero))).Target;
					}
				}
				num2++;
			}
			return null;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00095A50 File Offset: 0x00093C50
		internal static void Store(IntPtr pbref, object data, int key)
		{
			IntPtr intPtr = (IntPtr)GCHandle.Alloc(data);
			IntPtr intPtr2 = Pasteboard.CFDataCreate(IntPtr.Zero, ref intPtr, Marshal.SizeOf(typeof(IntPtr)));
			key = (int)Pasteboard.internal_format;
			Pasteboard.PasteboardClear(pbref);
			Pasteboard.PasteboardPutItemFlavor(pbref, 64206U, (uint)key, intPtr2, 0U);
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00095AA7 File Offset: 0x00093CA7
		internal static IntPtr Primary
		{
			get
			{
				return Pasteboard.primary_pbref;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00095AAE File Offset: 0x00093CAE
		internal static IntPtr Application
		{
			get
			{
				return Pasteboard.app_pbref;
			}
		}

		// Token: 0x06001E20 RID: 7712
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CFDataCreate(IntPtr allocator, ref IntPtr buf, int length);

		// Token: 0x06001E21 RID: 7713
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CFDataGetBytePtr(IntPtr data);

		// Token: 0x06001E22 RID: 7714
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardClear(IntPtr pbref);

		// Token: 0x06001E23 RID: 7715
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardCreate(IntPtr str, ref IntPtr pbref);

		// Token: 0x06001E24 RID: 7716
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardCopyItemFlavorData(IntPtr pbref, uint itemid, uint key, ref IntPtr data);

		// Token: 0x06001E25 RID: 7717
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardGetItemCount(IntPtr pbref, ref uint count);

		// Token: 0x06001E26 RID: 7718
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardGetItemIdentifier(IntPtr pbref, uint itemindex, ref uint itemid);

		// Token: 0x06001E27 RID: 7719
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardPutItemFlavor(IntPtr pbref, uint itemid, uint key, IntPtr data, uint flags);

		// Token: 0x04001D40 RID: 7488
		private static IntPtr primary_pbref;

		// Token: 0x04001D41 RID: 7489
		private static IntPtr app_pbref;

		// Token: 0x04001D42 RID: 7490
		private static IntPtr internal_format;
	}
}
