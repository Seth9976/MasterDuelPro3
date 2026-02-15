using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000012 RID: 18
	internal class SafeNativeMethods
	{
		// Token: 0x02000013 RID: 19
		internal class Gdip : GDIPlus
		{
			// Token: 0x06000042 RID: 66 RVA: 0x00003F2C File Offset: 0x0000212C
			static Gdip()
			{
				AppDomain currentDomain = AppDomain.CurrentDomain;
				currentDomain.ProcessExit += SafeNativeMethods.Gdip.OnProcessExit;
				if (!currentDomain.IsDefaultAppDomain())
				{
					currentDomain.DomainUnload += SafeNativeMethods.Gdip.OnProcessExit;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000043 RID: 67 RVA: 0x00003F8A File Offset: 0x0000218A
			private static bool Initialized
			{
				get
				{
					return SafeNativeMethods.Gdip.s_initToken != IntPtr.Zero;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000044 RID: 68 RVA: 0x00003F9C File Offset: 0x0000219C
			internal static IDictionary ThreadData
			{
				get
				{
					LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("system.drawing.threaddata");
					IDictionary dictionary = (IDictionary)Thread.GetData(namedDataSlot);
					if (dictionary == null)
					{
						dictionary = new Hashtable();
						Thread.SetData(namedDataSlot, dictionary);
					}
					return dictionary;
				}
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00003FD1 File Offset: 0x000021D1
			[MethodImpl(MethodImplOptions.NoInlining)]
			private static void ClearThreadData()
			{
				Thread.SetData(Thread.GetNamedDataSlot("system.drawing.threaddata"), null);
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00003FE4 File Offset: 0x000021E4
			private static void Shutdown()
			{
				if (SafeNativeMethods.Gdip.Initialized)
				{
					SafeNativeMethods.Gdip.ClearThreadData();
					AppDomain currentDomain = AppDomain.CurrentDomain;
					currentDomain.ProcessExit -= SafeNativeMethods.Gdip.OnProcessExit;
					if (!currentDomain.IsDefaultAppDomain())
					{
						currentDomain.DomainUnload -= SafeNativeMethods.Gdip.OnProcessExit;
					}
				}
			}

			// Token: 0x06000047 RID: 71 RVA: 0x0000402F File Offset: 0x0000222F
			[PrePrepareMethod]
			private static void OnProcessExit(object sender, EventArgs e)
			{
				SafeNativeMethods.Gdip.Shutdown();
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00004036 File Offset: 0x00002236
			internal static void CheckStatus(int status)
			{
				if (status != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(status);
				}
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00004044 File Offset: 0x00002244
			internal static Exception StatusException(int status)
			{
				switch (status)
				{
				case 1:
					return new ExternalException(SR.Format("A generic error occurred in GDI+.", Array.Empty<object>()), -2147467259);
				case 2:
					return new ArgumentException(SR.Format("Parameter is not valid.", Array.Empty<object>()));
				case 3:
					return new OutOfMemoryException(SR.Format("Out of memory.", Array.Empty<object>()));
				case 4:
					return new InvalidOperationException(SR.Format("Object is currently in use elsewhere.", Array.Empty<object>()));
				case 5:
					return new OutOfMemoryException(SR.Format("Buffer is too small (internal GDI+ error).", Array.Empty<object>()));
				case 6:
					return new NotImplementedException(SR.Format("Not implemented.", Array.Empty<object>()));
				case 7:
					return new ExternalException(SR.Format("A generic error occurred in GDI+.", Array.Empty<object>()), -2147467259);
				case 8:
					return new InvalidOperationException(SR.Format("Bitmap region is already locked.", Array.Empty<object>()));
				case 9:
					return new ExternalException(SR.Format("Function was ended.", Array.Empty<object>()), -2147467260);
				case 10:
					return new FileNotFoundException(SR.Format("File not found.", Array.Empty<object>()));
				case 11:
					return new OverflowException(SR.Format("Overflow error.", Array.Empty<object>()));
				case 12:
					return new ExternalException(SR.Format("File access is denied.", Array.Empty<object>()), -2147024891);
				case 13:
					return new ArgumentException(SR.Format("Image format is unknown.", Array.Empty<object>()));
				case 14:
					return new ArgumentException(SR.Format("Font '{0}' cannot be found.", new object[] { "?" }));
				case 15:
					return new ArgumentException(SR.Format("Font '{0}' does not support style '{1}'.", new object[] { "?", "?" }));
				case 16:
					return new ArgumentException(SR.Format("Only TrueType fonts are supported. This is not a TrueType font.", Array.Empty<object>()));
				case 17:
					return new ExternalException(SR.Format("Current version of GDI+ does not support this feature.", Array.Empty<object>()), -2147467259);
				case 18:
					return new ExternalException(SR.Format("GDI+ is not properly initialized (internal GDI+ error).", Array.Empty<object>()), -2147467259);
				case 19:
					return new ArgumentException(SR.Format("Property cannot be found.", Array.Empty<object>()));
				case 20:
					return new ArgumentException(SR.Format("Property is not supported.", Array.Empty<object>()));
				default:
					return new ExternalException(SR.Format("Unknown GDI+ error occurred.", Array.Empty<object>()), -2147418113);
				}
			}

			// Token: 0x040000C4 RID: 196
			private static readonly TraceSwitch s_gdiPlusInitialization = new TraceSwitch("GdiPlusInitialization", "Tracks GDI+ initialization and teardown");

			// Token: 0x040000C5 RID: 197
			private static IntPtr s_initToken = (IntPtr)1;
		}
	}
}
