using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000210 RID: 528
	internal class Win32DnD
	{
		// Token: 0x06001652 RID: 5714 RVA: 0x0006FBAC File Offset: 0x0006DDAC
		static Win32DnD()
		{
			Win32DnD.Win32OleInitialize(IntPtr.Zero);
			Win32DnD.DragDropEventArgs = new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[0]), 0, 0, 0, DragDropEffects.None, DragDropEffects.None);
			Win32DnD.DragFeedbackEventArgs = new GiveFeedbackEventArgs(DragDropEffects.None, true);
			Win32DnD.DragContinueEventArgs = new QueryContinueDragEventArgs(0, false, DragAction.Continue);
			Win32DnD.DragFormats = new ArrayList();
			Win32DnD.DragFormatArray = new Win32DnD.FORMATETC[0];
			Win32DnD.DragMediums = new ArrayList();
			Win32DnD.DOQueryInterface = new Win32DnD.QueryInterfaceDelegate(Win32DnD.ComIDataObject.QueryInterface);
			Win32DnD.DOAddRef = new Win32DnD.AddRefDelegate(Win32DnD.ComIDataObject.AddRef);
			Win32DnD.DORelease = new Win32DnD.ReleaseDelegate(Win32DnD.ComIDataObject.Release);
			Win32DnD.GetData = new Win32DnD.GetDataDelegate(Win32DnD.ComIDataObject.GetData);
			Win32DnD.GetDataHere = new Win32DnD.GetDataHereDelegate(Win32DnD.ComIDataObject.GetDataHere);
			Win32DnD.QueryGetData = new Win32DnD.QueryGetDataDelegate(Win32DnD.ComIDataObject.QueryGetData);
			Win32DnD.GetCanonicalFormatEtc = new Win32DnD.GetCanonicalFormatEtcDelegate(Win32DnD.ComIDataObject.GetCanonicalFormatEtc);
			Win32DnD.SetData = new Win32DnD.SetDataDelegate(Win32DnD.ComIDataObject.SetData);
			Win32DnD.EnumFormatEtc = new Win32DnD.EnumFormatEtcDelegate(Win32DnD.ComIDataObject.EnumFormatEtc);
			Win32DnD.DAdvise = new Win32DnD.DAdviseDelegate(Win32DnD.ComIDataObject.DAdvise);
			Win32DnD.DUnadvise = new Win32DnD.DUnadviseDelegate(Win32DnD.ComIDataObject.DUnadvise);
			Win32DnD.EnumDAdvise = new Win32DnD.EnumDAdviseDelegate(Win32DnD.ComIDataObject.EnumDAdvise);
			Win32DnD.DSQueryInterface = new Win32DnD.QueryInterfaceDelegate(Win32DnD.ComIDropSource.QueryInterface);
			Win32DnD.DSAddRef = new Win32DnD.AddRefDelegate(Win32DnD.ComIDropSource.AddRef);
			Win32DnD.DSRelease = new Win32DnD.ReleaseDelegate(Win32DnD.ComIDropSource.Release);
			Win32DnD.QueryContinueDrag = new Win32DnD.QueryContinueDragDelegate(Win32DnD.ComIDropSource.QueryContinueDrag);
			Win32DnD.GiveFeedback = new Win32DnD.GiveFeedbackDelegate(Win32DnD.ComIDropSource.GiveFeedback);
			Win32DnD.DTQueryInterface = new Win32DnD.QueryInterfaceDelegate(Win32DnD.ComIDropTarget.QueryInterface);
			Win32DnD.DTAddRef = new Win32DnD.AddRefDelegate(Win32DnD.ComIDropTarget.AddRef);
			Win32DnD.DTRelease = new Win32DnD.ReleaseDelegate(Win32DnD.ComIDropTarget.Release);
			Win32DnD.DragEnter = new Win32DnD.DragEnterDelegate(Win32DnD.ComIDropTarget.DragEnter);
			Win32DnD.DragOver = new Win32DnD.DragOverDelegate(Win32DnD.ComIDropTarget.DragOver);
			Win32DnD.DragLeave = new Win32DnD.DragLeaveDelegate(Win32DnD.ComIDropTarget.DragLeave);
			Win32DnD.Drop = new Win32DnD.DropDelegate(Win32DnD.ComIDropTarget.Drop);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0006FDF0 File Offset: 0x0006DFF0
		internal static bool HandleWMDropFiles(ref MSG msg)
		{
			IntPtr wParam = msg.wParam;
			int num = Win32DnD.Win32DragQueryFile(wParam, -1, IntPtr.Zero, 0);
			string[] array = new string[num];
			StringBuilder stringBuilder = new StringBuilder(256);
			for (int i = 0; i < num; i++)
			{
				Win32DnD.Win32DragQueryFile(wParam, i, stringBuilder, stringBuilder.Capacity);
				array[i] = stringBuilder.ToString();
			}
			Win32DnD.DragDropEventArgs.Data.SetData(DataFormats.FileDrop, array);
			Control.FromHandle(msg.hwnd).DndDrop(Win32DnD.DragDropEventArgs);
			return true;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0006FE7C File Offset: 0x0006E07C
		private static int FindFormat(Win32DnD.FORMATETC pformatetc)
		{
			for (int i = 0; i < Win32DnD.DragFormats.Count; i++)
			{
				if (((Win32DnD.FORMATETC)Win32DnD.DragFormats[i]).cfFormat == pformatetc.cfFormat && ((Win32DnD.FORMATETC)Win32DnD.DragFormats[i]).dwAspect == pformatetc.dwAspect && (((Win32DnD.FORMATETC)Win32DnD.DragFormats[i]).tymed & pformatetc.tymed) != Win32DnD.TYMED.TYMED_NULL)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0006FEF9 File Offset: 0x0006E0F9
		internal static bool UnregisterDropTarget(IntPtr Window)
		{
			Win32DnD.Win32RevokeDragDrop(Window);
			return true;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0006FF04 File Offset: 0x0006E104
		internal static bool RegisterDropTarget(IntPtr Window)
		{
			Hwnd hwnd = Hwnd.ObjectFromWindow(Window);
			if (hwnd == null)
			{
				return false;
			}
			IntPtr unmanaged = Win32DnD.ComIDropTarget.GetUnmanaged(Window);
			hwnd.marshal_free_list.Add(unmanaged);
			return Win32DnD.Win32RegisterDragDrop(Window, unmanaged) == 0U;
		}

		// Token: 0x06001657 RID: 5719
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "RegisterDragDrop")]
		private static extern uint Win32RegisterDragDrop(IntPtr Window, IntPtr pDropTarget);

		// Token: 0x06001658 RID: 5720
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "RevokeDragDrop")]
		private static extern int Win32RevokeDragDrop(IntPtr Window);

		// Token: 0x06001659 RID: 5721
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "OleInitialize")]
		private static extern int Win32OleInitialize(IntPtr pvReserved);

		// Token: 0x0600165A RID: 5722
		[DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DragQueryFileW")]
		private static extern int Win32DragQueryFile(IntPtr hDrop, int iFile, IntPtr lpszFile, int cch);

		// Token: 0x0600165B RID: 5723
		[DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DragQueryFileW")]
		private static extern int Win32DragQueryFile(IntPtr hDrop, int iFile, StringBuilder lpszFile, int cch);

		// Token: 0x0600165C RID: 5724
		[DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SHCreateStdEnumFmtEtc")]
		private static extern uint Win32SHCreateStdEnumFmtEtc(uint cfmt, Win32DnD.FORMATETC[] afmt, ref IntPtr ppenumFormatEtc);

		// Token: 0x04000D74 RID: 3444
		private static Win32DnD.QueryInterfaceDelegate DOQueryInterface;

		// Token: 0x04000D75 RID: 3445
		private static Win32DnD.AddRefDelegate DOAddRef;

		// Token: 0x04000D76 RID: 3446
		private static Win32DnD.ReleaseDelegate DORelease;

		// Token: 0x04000D77 RID: 3447
		private static Win32DnD.GetDataDelegate GetData;

		// Token: 0x04000D78 RID: 3448
		private static Win32DnD.GetDataHereDelegate GetDataHere;

		// Token: 0x04000D79 RID: 3449
		private static Win32DnD.QueryGetDataDelegate QueryGetData;

		// Token: 0x04000D7A RID: 3450
		private static Win32DnD.GetCanonicalFormatEtcDelegate GetCanonicalFormatEtc;

		// Token: 0x04000D7B RID: 3451
		private static Win32DnD.SetDataDelegate SetData;

		// Token: 0x04000D7C RID: 3452
		private static Win32DnD.EnumFormatEtcDelegate EnumFormatEtc;

		// Token: 0x04000D7D RID: 3453
		private static Win32DnD.DAdviseDelegate DAdvise;

		// Token: 0x04000D7E RID: 3454
		private static Win32DnD.DUnadviseDelegate DUnadvise;

		// Token: 0x04000D7F RID: 3455
		private static Win32DnD.EnumDAdviseDelegate EnumDAdvise;

		// Token: 0x04000D80 RID: 3456
		private static Win32DnD.QueryInterfaceDelegate DSQueryInterface;

		// Token: 0x04000D81 RID: 3457
		private static Win32DnD.AddRefDelegate DSAddRef;

		// Token: 0x04000D82 RID: 3458
		private static Win32DnD.ReleaseDelegate DSRelease;

		// Token: 0x04000D83 RID: 3459
		private static Win32DnD.QueryContinueDragDelegate QueryContinueDrag;

		// Token: 0x04000D84 RID: 3460
		private static Win32DnD.GiveFeedbackDelegate GiveFeedback;

		// Token: 0x04000D85 RID: 3461
		private static Win32DnD.QueryInterfaceDelegate DTQueryInterface;

		// Token: 0x04000D86 RID: 3462
		private static Win32DnD.AddRefDelegate DTAddRef;

		// Token: 0x04000D87 RID: 3463
		private static Win32DnD.ReleaseDelegate DTRelease;

		// Token: 0x04000D88 RID: 3464
		private static Win32DnD.DragEnterDelegate DragEnter;

		// Token: 0x04000D89 RID: 3465
		private static Win32DnD.DragOverDelegate DragOver;

		// Token: 0x04000D8A RID: 3466
		private static Win32DnD.DragLeaveDelegate DragLeave;

		// Token: 0x04000D8B RID: 3467
		private static Win32DnD.DropDelegate Drop;

		// Token: 0x04000D8C RID: 3468
		private static DragEventArgs DragDropEventArgs;

		// Token: 0x04000D8D RID: 3469
		private static GiveFeedbackEventArgs DragFeedbackEventArgs;

		// Token: 0x04000D8E RID: 3470
		private static QueryContinueDragEventArgs DragContinueEventArgs;

		// Token: 0x04000D8F RID: 3471
		private static ArrayList DragFormats;

		// Token: 0x04000D90 RID: 3472
		private static Win32DnD.FORMATETC[] DragFormatArray;

		// Token: 0x04000D91 RID: 3473
		private static ArrayList DragMediums;

		// Token: 0x04000D92 RID: 3474
		private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

		// Token: 0x04000D93 RID: 3475
		private static readonly Guid IID_IDataObject = new Guid("0000010e-0000-0000-C000-000000000046");

		// Token: 0x04000D94 RID: 3476
		private static readonly Guid IID_IDropSource = new Guid("00000121-0000-0000-C000-000000000046");

		// Token: 0x04000D95 RID: 3477
		private static readonly Guid IID_IDropTarget = new Guid("00000122-0000-0000-C000-000000000046");

		// Token: 0x02000211 RID: 529
		// (Invoke) Token: 0x0600165E RID: 5726
		internal delegate uint QueryInterfaceDelegate(IntPtr @this, ref Guid riid, IntPtr ppvObject);

		// Token: 0x02000212 RID: 530
		// (Invoke) Token: 0x06001660 RID: 5728
		internal delegate uint AddRefDelegate(IntPtr @this);

		// Token: 0x02000213 RID: 531
		// (Invoke) Token: 0x06001662 RID: 5730
		internal delegate uint ReleaseDelegate(IntPtr @this);

		// Token: 0x02000214 RID: 532
		// (Invoke) Token: 0x06001664 RID: 5732
		internal delegate uint GetDataDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pmedium);

		// Token: 0x02000215 RID: 533
		// (Invoke) Token: 0x06001666 RID: 5734
		internal delegate uint GetDataHereDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium);

		// Token: 0x02000216 RID: 534
		// (Invoke) Token: 0x06001668 RID: 5736
		internal delegate uint QueryGetDataDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc);

		// Token: 0x02000217 RID: 535
		// (Invoke) Token: 0x0600166A RID: 5738
		internal delegate uint GetCanonicalFormatEtcDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pformatetcOut);

		// Token: 0x02000218 RID: 536
		// (Invoke) Token: 0x0600166C RID: 5740
		internal delegate uint SetDataDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium, bool release);

		// Token: 0x02000219 RID: 537
		// (Invoke) Token: 0x0600166E RID: 5742
		internal delegate uint EnumFormatEtcDelegate(IntPtr @this, uint direction, IntPtr ppenumFormatEtc);

		// Token: 0x0200021A RID: 538
		// (Invoke) Token: 0x06001670 RID: 5744
		internal delegate uint DAdviseDelegate(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, uint advf, IntPtr pAdvSink, ref uint pdwConnection);

		// Token: 0x0200021B RID: 539
		// (Invoke) Token: 0x06001672 RID: 5746
		internal delegate uint DUnadviseDelegate(IntPtr @this, uint pdwConnection);

		// Token: 0x0200021C RID: 540
		// (Invoke) Token: 0x06001674 RID: 5748
		internal delegate uint EnumDAdviseDelegate(IntPtr @this, IntPtr ppenumAdvise);

		// Token: 0x0200021D RID: 541
		// (Invoke) Token: 0x06001676 RID: 5750
		internal delegate uint QueryContinueDragDelegate(IntPtr @this, bool fEscapePressed, uint grfkeyState);

		// Token: 0x0200021E RID: 542
		// (Invoke) Token: 0x06001678 RID: 5752
		internal delegate uint GiveFeedbackDelegate(IntPtr @this, uint pdwEffect);

		// Token: 0x0200021F RID: 543
		// (Invoke) Token: 0x0600167A RID: 5754
		internal delegate uint DragEnterDelegate(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect);

		// Token: 0x02000220 RID: 544
		// (Invoke) Token: 0x0600167C RID: 5756
		internal delegate uint DragOverDelegate(IntPtr @this, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect);

		// Token: 0x02000221 RID: 545
		// (Invoke) Token: 0x0600167E RID: 5758
		internal delegate uint DragLeaveDelegate(IntPtr @this);

		// Token: 0x02000222 RID: 546
		// (Invoke) Token: 0x06001680 RID: 5760
		internal delegate uint DropDelegate(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect);

		// Token: 0x02000223 RID: 547
		internal struct FORMATETC
		{
			// Token: 0x04000D96 RID: 3478
			[MarshalAs(UnmanagedType.U2)]
			internal ClipboardFormats cfFormat;

			// Token: 0x04000D97 RID: 3479
			internal IntPtr ptd;

			// Token: 0x04000D98 RID: 3480
			internal Win32DnD.DVASPECT dwAspect;

			// Token: 0x04000D99 RID: 3481
			internal int lindex;

			// Token: 0x04000D9A RID: 3482
			internal Win32DnD.TYMED tymed;
		}

		// Token: 0x02000224 RID: 548
		internal struct STGMEDIUM
		{
			// Token: 0x04000D9B RID: 3483
			internal Win32DnD.TYMED tymed;

			// Token: 0x04000D9C RID: 3484
			internal IntPtr hHandle;

			// Token: 0x04000D9D RID: 3485
			internal IntPtr pUnkForRelease;
		}

		// Token: 0x02000225 RID: 549
		internal enum DVASPECT
		{
			// Token: 0x04000D9F RID: 3487
			DVASPECT_CONTENT = 1,
			// Token: 0x04000DA0 RID: 3488
			DVASPECT_THUMBNAIL,
			// Token: 0x04000DA1 RID: 3489
			DVASPECT_ICON = 4,
			// Token: 0x04000DA2 RID: 3490
			DVASPECT_DOCPRINT = 8
		}

		// Token: 0x02000226 RID: 550
		internal enum TYMED
		{
			// Token: 0x04000DA4 RID: 3492
			TYMED_HGLOBAL = 1,
			// Token: 0x04000DA5 RID: 3493
			TYMED_FILE,
			// Token: 0x04000DA6 RID: 3494
			TYMED_ISTREAM = 4,
			// Token: 0x04000DA7 RID: 3495
			TYMED_ISTORAGE = 8,
			// Token: 0x04000DA8 RID: 3496
			TYMED_GDI = 16,
			// Token: 0x04000DA9 RID: 3497
			TYMED_MFPICT = 32,
			// Token: 0x04000DAA RID: 3498
			TYMED_ENHMF = 64,
			// Token: 0x04000DAB RID: 3499
			TYMED_NULL = 0
		}

		// Token: 0x02000227 RID: 551
		internal class ComIDataObject
		{
			// Token: 0x06001681 RID: 5761 RVA: 0x0006FF44 File Offset: 0x0006E144
			internal static uint QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				try
				{
					if (Win32DnD.IID_IUnknown.Equals(riid) || Win32DnD.IID_IDataObject.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						return 0U;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Got exception {0}", ex.Message);
				}
				Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
				return 2147500034U;
			}

			// Token: 0x06001682 RID: 5762 RVA: 0x00006F54 File Offset: 0x00005154
			internal static uint AddRef(IntPtr @this)
			{
				return 1U;
			}

			// Token: 0x06001683 RID: 5763 RVA: 0x00002D70 File Offset: 0x00000F70
			internal static uint Release(IntPtr @this)
			{
				return 0U;
			}

			// Token: 0x06001684 RID: 5764 RVA: 0x0006FFC0 File Offset: 0x0006E1C0
			internal static uint GetData(IntPtr this_, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pmedium)
			{
				int num = Win32DnD.FindFormat(pformatetcIn);
				if (num != -1)
				{
					Win32DnD.ComIDataObject.medium.tymed = Win32DnD.TYMED.TYMED_HGLOBAL;
					Win32DnD.ComIDataObject.medium.hHandle = XplatUIWin32.DupGlobalMem(((Win32DnD.STGMEDIUM)Win32DnD.DragMediums[num]).hHandle);
					Win32DnD.ComIDataObject.medium.pUnkForRelease = IntPtr.Zero;
					try
					{
						Marshal.StructureToPtr<Win32DnD.STGMEDIUM>(Win32DnD.ComIDataObject.medium, pmedium, false);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: {0}", ex.Message);
					}
					return 0U;
				}
				return 2147745892U;
			}

			// Token: 0x06001685 RID: 5765 RVA: 0x00070054 File Offset: 0x0006E254
			internal static uint GetDataHere(IntPtr @this, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium)
			{
				return 2147745892U;
			}

			// Token: 0x06001686 RID: 5766 RVA: 0x0007005B File Offset: 0x0006E25B
			internal static uint QueryGetData(IntPtr @this, ref Win32DnD.FORMATETC pformatetc)
			{
				if (Win32DnD.FindFormat(pformatetc) != -1)
				{
					return 0U;
				}
				return 2147745892U;
			}

			// Token: 0x06001687 RID: 5767 RVA: 0x00070072 File Offset: 0x0006E272
			internal static uint GetCanonicalFormatEtc(IntPtr @this, ref Win32DnD.FORMATETC pformatetcIn, IntPtr pformatetcOut)
			{
				Marshal.WriteIntPtr(pformatetcOut, Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero);
				return 2147500033U;
			}

			// Token: 0x06001688 RID: 5768 RVA: 0x00070093 File Offset: 0x0006E293
			internal static uint SetData(IntPtr this_, ref Win32DnD.FORMATETC pformatetc, ref Win32DnD.STGMEDIUM pmedium, bool release)
			{
				return 2147500033U;
			}

			// Token: 0x06001689 RID: 5769 RVA: 0x0007009C File Offset: 0x0006E29C
			internal static uint EnumFormatEtc(IntPtr this_, uint direction, IntPtr ppenumFormatEtc)
			{
				if (direction == 1U)
				{
					IntPtr zero = IntPtr.Zero;
					Win32DnD.DragFormatArray = new Win32DnD.FORMATETC[Win32DnD.DragFormats.Count];
					for (int i = 0; i < Win32DnD.DragFormats.Count; i++)
					{
						Win32DnD.DragFormatArray[i] = (Win32DnD.FORMATETC)Win32DnD.DragFormats[i];
					}
					Win32DnD.Win32SHCreateStdEnumFmtEtc((uint)Win32DnD.DragFormatArray.Length, Win32DnD.DragFormatArray, ref zero);
					Marshal.WriteIntPtr(ppenumFormatEtc, zero);
					return 0U;
				}
				return 2147500033U;
			}

			// Token: 0x0600168A RID: 5770 RVA: 0x00070119 File Offset: 0x0006E319
			internal static uint DAdvise(IntPtr this_, ref Win32DnD.FORMATETC pformatetc, uint advf, IntPtr pAdvSink, ref uint pdwConnection)
			{
				return 2147745795U;
			}

			// Token: 0x0600168B RID: 5771 RVA: 0x00070119 File Offset: 0x0006E319
			internal static uint DUnadvise(IntPtr this_, uint pdwConnection)
			{
				return 2147745795U;
			}

			// Token: 0x0600168C RID: 5772 RVA: 0x00070119 File Offset: 0x0006E319
			internal static uint EnumDAdvise(IntPtr this_, IntPtr ppenumAdvise)
			{
				return 2147745795U;
			}

			// Token: 0x04000DAC RID: 3500
			internal static Win32DnD.STGMEDIUM medium;
		}

		// Token: 0x02000228 RID: 552
		internal class ComIDropSource
		{
			// Token: 0x0600168D RID: 5773 RVA: 0x00070120 File Offset: 0x0006E320
			internal static uint QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				try
				{
					if (Win32DnD.IID_IUnknown.Equals(riid) || Win32DnD.IID_IDropSource.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						return 0U;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Got exception {0}", ex.Message);
				}
				Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
				return 2147500034U;
			}

			// Token: 0x0600168E RID: 5774 RVA: 0x00006F54 File Offset: 0x00005154
			internal static uint AddRef(IntPtr @this)
			{
				return 1U;
			}

			// Token: 0x0600168F RID: 5775 RVA: 0x00002D70 File Offset: 0x00000F70
			internal static uint Release(IntPtr @this)
			{
				return 0U;
			}

			// Token: 0x06001690 RID: 5776 RVA: 0x0007019C File Offset: 0x0006E39C
			internal static uint QueryContinueDrag(IntPtr @this, bool fEscapePressed, uint grfkeyState)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				if (fEscapePressed)
				{
					Win32DnD.DragContinueEventArgs.drag_action = DragAction.Cancel;
				}
				else if ((grfkeyState & 19U) == 0U)
				{
					Win32DnD.DragContinueEventArgs.drag_action = DragAction.Drop;
				}
				else
				{
					Win32DnD.DragContinueEventArgs.drag_action = DragAction.Continue;
				}
				Win32DnD.DragContinueEventArgs.escape_pressed = fEscapePressed;
				Win32DnD.DragContinueEventArgs.key_state = (int)grfkeyState;
				Control.FromHandle(intPtr).DndContinueDrag(Win32DnD.DragContinueEventArgs);
				if (Win32DnD.DragContinueEventArgs.drag_action == DragAction.Cancel)
				{
					return 262401U;
				}
				if (Win32DnD.DragContinueEventArgs.drag_action == DragAction.Drop)
				{
					return 262400U;
				}
				return 0U;
			}

			// Token: 0x06001691 RID: 5777 RVA: 0x00070238 File Offset: 0x0006E438
			internal static uint GiveFeedback(IntPtr @this, uint pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragFeedbackEventArgs.effect = (DragDropEffects)pdwEffect;
				Win32DnD.DragFeedbackEventArgs.use_default_cursors = true;
				Control.FromHandle(intPtr).DndFeedback(Win32DnD.DragFeedbackEventArgs);
				if (Win32DnD.DragFeedbackEventArgs.use_default_cursors)
				{
					return 262402U;
				}
				return 0U;
			}
		}

		// Token: 0x02000229 RID: 553
		internal class ComIDropTarget
		{
			// Token: 0x06001692 RID: 5778 RVA: 0x00070294 File Offset: 0x0006E494
			internal static IntPtr GetUnmanaged(IntPtr Window)
			{
				Win32DnD.ComIDropTarget.IDropTarget dropTarget = new Win32DnD.ComIDropTarget.IDropTarget
				{
					QueryInterface = Win32DnD.DTQueryInterface,
					AddRef = Win32DnD.DTAddRef,
					Release = Win32DnD.DTRelease,
					DragEnter = Win32DnD.DragEnter,
					DragOver = Win32DnD.DragOver,
					DragLeave = Win32DnD.DragLeave,
					Drop = Win32DnD.Drop,
					Window = Window
				};
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Win32DnD.ComIDropTarget.IDropTarget>(dropTarget));
				Marshal.StructureToPtr<Win32DnD.ComIDropTarget.IDropTarget>(dropTarget, intPtr, false);
				long num = intPtr.ToInt64();
				num += (long)(2 * Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(intPtr, new IntPtr(num));
				return intPtr;
			}

			// Token: 0x06001693 RID: 5779 RVA: 0x00070344 File Offset: 0x0006E544
			internal static uint QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				try
				{
					if (Win32DnD.IID_IUnknown.Equals(riid) || Win32DnD.IID_IDropTarget.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						return 0U;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Got exception {0}", ex.Message);
				}
				Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
				return 2147500034U;
			}

			// Token: 0x06001694 RID: 5780 RVA: 0x00006F54 File Offset: 0x00005154
			internal static uint AddRef(IntPtr @this)
			{
				return 1U;
			}

			// Token: 0x06001695 RID: 5781 RVA: 0x00002D70 File Offset: 0x00000F70
			internal static uint Release(IntPtr @this)
			{
				return 0U;
			}

			// Token: 0x06001696 RID: 5782 RVA: 0x000703C0 File Offset: 0x0006E5C0
			internal static uint DragEnter(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragDropEventArgs.x = pt_x.ToInt32();
				Win32DnD.DragDropEventArgs.y = pt_y.ToInt32();
				Win32DnD.DragDropEventArgs.allowed_effect = (DragDropEffects)Marshal.ReadIntPtr(pdwEffect).ToInt32();
				Win32DnD.DragDropEventArgs.current_effect = Win32DnD.DragDropEventArgs.AllowedEffect;
				Win32DnD.DragDropEventArgs.keystate = (int)grfkeyState;
				Control.FromHandle(intPtr).DndEnter(Win32DnD.DragDropEventArgs);
				Marshal.WriteInt32(pdwEffect, (int)Win32DnD.DragDropEventArgs.Effect);
				return 0U;
			}

			// Token: 0x06001697 RID: 5783 RVA: 0x00070460 File Offset: 0x0006E660
			internal static uint DragOver(IntPtr @this, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragDropEventArgs.x = pt_x.ToInt32();
				Win32DnD.DragDropEventArgs.y = pt_y.ToInt32();
				Win32DnD.DragDropEventArgs.allowed_effect = (DragDropEffects)Marshal.ReadIntPtr(pdwEffect).ToInt32();
				Win32DnD.DragDropEventArgs.current_effect = Win32DnD.DragDropEventArgs.AllowedEffect;
				Win32DnD.DragDropEventArgs.keystate = (int)grfkeyState;
				Control.FromHandle(intPtr).DndOver(Win32DnD.DragDropEventArgs);
				Marshal.WriteInt32(pdwEffect, (int)Win32DnD.DragDropEventArgs.Effect);
				return 0U;
			}

			// Token: 0x06001698 RID: 5784 RVA: 0x000704FD File Offset: 0x0006E6FD
			internal static uint DragLeave(IntPtr @this)
			{
				Control.FromHandle(Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)))).DndLeave(EventArgs.Empty);
				return 0U;
			}

			// Token: 0x06001699 RID: 5785 RVA: 0x00070524 File Offset: 0x0006E724
			internal static uint Drop(IntPtr @this, IntPtr pDataObj, uint grfkeyState, IntPtr pt_x, IntPtr pt_y, IntPtr pdwEffect)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(@this, Marshal.SizeOf(typeof(IntPtr)));
				Win32DnD.DragDropEventArgs.x = pt_x.ToInt32();
				Win32DnD.DragDropEventArgs.y = pt_y.ToInt32();
				Win32DnD.DragDropEventArgs.allowed_effect = (DragDropEffects)Marshal.ReadIntPtr(pdwEffect).ToInt32();
				Win32DnD.DragDropEventArgs.current_effect = Win32DnD.DragDropEventArgs.AllowedEffect;
				Win32DnD.DragDropEventArgs.keystate = (int)grfkeyState;
				Control control = Control.FromHandle(intPtr);
				if (control != null)
				{
					control.DndDrop(Win32DnD.DragDropEventArgs);
					return 1U;
				}
				Marshal.WriteInt32(pdwEffect, (int)Win32DnD.DragDropEventArgs.Effect);
				return 0U;
			}

			// Token: 0x0200022A RID: 554
			internal struct IDropTarget
			{
				// Token: 0x04000DAD RID: 3501
				internal IntPtr vtbl;

				// Token: 0x04000DAE RID: 3502
				internal IntPtr Window;

				// Token: 0x04000DAF RID: 3503
				internal Win32DnD.QueryInterfaceDelegate QueryInterface;

				// Token: 0x04000DB0 RID: 3504
				internal Win32DnD.AddRefDelegate AddRef;

				// Token: 0x04000DB1 RID: 3505
				internal Win32DnD.ReleaseDelegate Release;

				// Token: 0x04000DB2 RID: 3506
				internal Win32DnD.DragEnterDelegate DragEnter;

				// Token: 0x04000DB3 RID: 3507
				internal Win32DnD.DragOverDelegate DragOver;

				// Token: 0x04000DB4 RID: 3508
				internal Win32DnD.DragLeaveDelegate DragLeave;

				// Token: 0x04000DB5 RID: 3509
				internal Win32DnD.DropDelegate Drop;
			}
		}
	}
}
