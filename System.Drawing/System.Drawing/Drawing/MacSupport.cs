using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200006F RID: 111
	internal static class MacSupport
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000D174 File Offset: 0x0000B374
		static MacSupport()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (string.Equals(assembly.GetName().Name, "System.Windows.Forms"))
				{
					Type type = assembly.GetType("System.Windows.Forms.XplatUICarbon");
					if (type != null)
					{
						MacSupport.hwnd_delegate = (Delegate)type.GetTypeInfo().GetField("HwndDelegate", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					}
				}
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000D200 File Offset: 0x0000B400
		internal static CocoaContext GetCGContextForNSView(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr = MacSupport.objc_msgSend(MacSupport.objc_getClass("NSView"), MacSupport.sel_registerName("focusView"));
			IntPtr intPtr2 = IntPtr.Zero;
			if (intPtr != handle)
			{
				if (!MacSupport.bool_objc_msgSend(handle, MacSupport.sel_registerName("lockFocusIfCanDraw")))
				{
					return null;
				}
				intPtr2 = handle;
			}
			IntPtr intPtr3 = MacSupport.objc_msgSend(MacSupport.objc_msgSend(MacSupport.objc_msgSend(handle, MacSupport.sel_registerName("window")), MacSupport.sel_registerName("graphicsContext")), MacSupport.sel_registerName("graphicsPort"));
			bool flag = MacSupport.bool_objc_msgSend(handle, MacSupport.sel_registerName("isFlipped"));
			MacSupport.CGContextSaveGState(intPtr3);
			Size size;
			if (IntPtr.Size == 4)
			{
				CGRect32 cgrect = default(CGRect32);
				MacSupport.objc_msgSend_stret(ref cgrect, handle, MacSupport.sel_registerName("bounds"));
				if (flag)
				{
					MacSupport.CGContextTranslateCTM32(intPtr3, cgrect.origin.x, cgrect.size.height);
					MacSupport.CGContextScaleCTM32(intPtr3, 1f, -1f);
				}
				size = new Size((int)cgrect.size.width, (int)cgrect.size.height);
			}
			else
			{
				CGRect64 cgrect2 = default(CGRect64);
				MacSupport.objc_msgSend_stret(ref cgrect2, handle, MacSupport.sel_registerName("bounds"));
				if (flag)
				{
					MacSupport.CGContextTranslateCTM64(intPtr3, cgrect2.origin.x, cgrect2.size.height);
					MacSupport.CGContextScaleCTM64(intPtr3, 1.0, -1.0);
				}
				size = new Size((int)cgrect2.size.width, (int)cgrect2.size.height);
			}
			return new CocoaContext(intPtr2, intPtr3, size.Width, size.Height);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		internal static CarbonContext GetCGContextForView(IntPtr handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			if (IntPtr.Size == 8)
			{
				throw new NotSupportedException();
			}
			intPtr3 = MacSupport.GetControlOwner(handle);
			if (handle == IntPtr.Zero || intPtr3 == IntPtr.Zero)
			{
				intPtr2 = MacSupport.GetQDGlobalsThePort();
				MacSupport.CreateCGContextForPort(intPtr2, ref intPtr);
				CGRect32 cgrect = MacSupport.CGDisplayBounds32(MacSupport.CGMainDisplayID());
				return new CarbonContext(intPtr2, intPtr, (int)cgrect.size.width, (int)cgrect.size.height);
			}
			QDRect qdrect = default(QDRect);
			CGRect32 cgrect2 = default(CGRect32);
			intPtr2 = MacSupport.GetWindowPort(intPtr3);
			intPtr = MacSupport.GetContext(intPtr2);
			MacSupport.GetWindowBounds(intPtr3, 32U, ref qdrect);
			MacSupport.HIViewGetBounds(handle, ref cgrect2);
			MacSupport.HIViewConvertRect(ref cgrect2, handle, IntPtr.Zero);
			if (cgrect2.size.height < 0f)
			{
				cgrect2.size.height = 0f;
			}
			if (cgrect2.size.width < 0f)
			{
				cgrect2.size.width = 0f;
			}
			MacSupport.CGContextTranslateCTM32(intPtr, cgrect2.origin.x, (float)(qdrect.bottom - qdrect.top) - (cgrect2.origin.y + cgrect2.size.height));
			CGRect32 cgrect3 = new CGRect32(0f, 0f, cgrect2.size.width, cgrect2.size.height);
			MacSupport.CGContextSaveGState(intPtr);
			Rectangle[] array = (Rectangle[])MacSupport.hwnd_delegate.DynamicInvoke(new object[] { handle });
			if (array != null && array.Length != 0)
			{
				int num = array.Length;
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect32(intPtr, cgrect3);
				for (int i = 0; i < num; i++)
				{
					MacSupport.CGContextAddRect32(intPtr, new CGRect32((float)array[i].X, cgrect2.size.height - (float)array[i].Y - (float)array[i].Height, (float)array[i].Width, (float)array[i].Height));
				}
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextEOClip(intPtr);
			}
			else
			{
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect32(intPtr, cgrect3);
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextClip(intPtr);
			}
			return new CarbonContext(intPtr2, intPtr, (int)cgrect2.size.width, (int)cgrect2.size.height);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000D61C File Offset: 0x0000B81C
		internal static IntPtr GetContext(IntPtr port)
		{
			IntPtr zero = IntPtr.Zero;
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CreateCGContextForPort(port, ref zero);
			}
			return zero;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000D664 File Offset: 0x0000B864
		internal static void ReleaseContext(IntPtr port, IntPtr context)
		{
			MacSupport.CGContextRestoreGState(context);
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CFRelease(context);
			}
		}

		// Token: 0x06000422 RID: 1058
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_getClass(string className);

		// Token: 0x06000423 RID: 1059
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr basePtr, IntPtr selector);

		// Token: 0x06000424 RID: 1060
		[DllImport("libobjc.dylib")]
		public static extern void objc_msgSend_stret(ref CGRect32 arect, IntPtr basePtr, IntPtr selector);

		// Token: 0x06000425 RID: 1061
		[DllImport("libobjc.dylib")]
		public static extern void objc_msgSend_stret(ref CGRect64 arect, IntPtr basePtr, IntPtr selector);

		// Token: 0x06000426 RID: 1062
		[DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern bool bool_objc_msgSend(IntPtr handle, IntPtr selector);

		// Token: 0x06000427 RID: 1063
		[DllImport("libobjc.dylib")]
		public static extern IntPtr sel_registerName(string selectorName);

		// Token: 0x06000428 RID: 1064
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr CGMainDisplayID();

		// Token: 0x06000429 RID: 1065
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGDisplayBounds")]
		internal static extern CGRect32 CGDisplayBounds32(IntPtr display);

		// Token: 0x0600042A RID: 1066
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewGetBounds(IntPtr vHnd, ref CGRect32 r);

		// Token: 0x0600042B RID: 1067
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewConvertRect(ref CGRect32 r, IntPtr a, IntPtr b);

		// Token: 0x0600042C RID: 1068
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetControlOwner(IntPtr aView);

		// Token: 0x0600042D RID: 1069
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetWindowBounds(IntPtr wHnd, uint reg, ref QDRect rect);

		// Token: 0x0600042E RID: 1070
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetWindowPort(IntPtr hWnd);

		// Token: 0x0600042F RID: 1071
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetQDGlobalsThePort();

		// Token: 0x06000430 RID: 1072
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CreateCGContextForPort(IntPtr port, ref IntPtr context);

		// Token: 0x06000431 RID: 1073
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CFRelease(IntPtr context);

		// Token: 0x06000432 RID: 1074
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextTranslateCTM")]
		internal static extern void CGContextTranslateCTM32(IntPtr context, float tx, float ty);

		// Token: 0x06000433 RID: 1075
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextScaleCTM")]
		internal static extern void CGContextScaleCTM32(IntPtr context, float x, float y);

		// Token: 0x06000434 RID: 1076
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextTranslateCTM")]
		internal static extern void CGContextTranslateCTM64(IntPtr context, double tx, double ty);

		// Token: 0x06000435 RID: 1077
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextScaleCTM")]
		internal static extern void CGContextScaleCTM64(IntPtr context, double x, double y);

		// Token: 0x06000436 RID: 1078
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextFlush(IntPtr context);

		// Token: 0x06000437 RID: 1079
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSynchronize(IntPtr context);

		// Token: 0x06000438 RID: 1080
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextAddRect")]
		internal static extern void CGContextAddRect32(IntPtr context, CGRect32 rect);

		// Token: 0x06000439 RID: 1081
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextBeginPath(IntPtr context);

		// Token: 0x0600043A RID: 1082
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClosePath(IntPtr context);

		// Token: 0x0600043B RID: 1083
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClip(IntPtr context);

		// Token: 0x0600043C RID: 1084
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextEOClip(IntPtr context);

		// Token: 0x0600043D RID: 1085
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSaveGState(IntPtr context);

		// Token: 0x0600043E RID: 1086
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextRestoreGState(IntPtr context);

		// Token: 0x0400020A RID: 522
		internal static Hashtable contextReference = new Hashtable();

		// Token: 0x0400020B RID: 523
		internal static object lockobj = new object();

		// Token: 0x0400020C RID: 524
		internal static Delegate hwnd_delegate;
	}
}
