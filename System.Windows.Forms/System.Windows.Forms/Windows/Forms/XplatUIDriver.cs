using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200029A RID: 666
	internal abstract class XplatUIDriver
	{
		// Token: 0x06001897 RID: 6295
		internal abstract IntPtr InitializeDriver();

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00078DB4 File Offset: 0x00076FB4
		internal virtual Size Border3DSize
		{
			get
			{
				return new Size(2, 2);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00050905 File Offset: 0x0004EB05
		internal virtual Size BorderSize
		{
			get
			{
				return new Size(1, 1);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x00078E5B File Offset: 0x0007705B
		internal virtual Size CaptionButtonSize
		{
			get
			{
				return new Size(18, 18);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00078E66 File Offset: 0x00077066
		internal virtual int DoubleClickTime
		{
			get
			{
				return 500;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x00078E5B File Offset: 0x0007705B
		public virtual Size MenuButtonSize
		{
			get
			{
				return new Size(18, 18);
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00002D70 File Offset: 0x00000F70
		internal virtual Keys ModifierKeys
		{
			get
			{
				return Keys.None;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00078E6D File Offset: 0x0007706D
		internal virtual Point MousePosition
		{
			get
			{
				return Point.Empty;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00078DB0 File Offset: 0x00076FB0
		internal virtual int MenuHeight
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0005093A File Offset: 0x0004EB3A
		internal virtual int HorizontalScrollBarHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00006F54 File Offset: 0x00005154
		internal virtual bool UserClipWontExposeParent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0005093A File Offset: 0x0004EB3A
		internal virtual int VerticalScrollBarWidth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060018A3 RID: 6307
		internal abstract int CaptionHeight { get; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060018A4 RID: 6308
		internal abstract Size DragSize { get; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060018A5 RID: 6309
		internal abstract Size FrameBorderSize { get; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060018A6 RID: 6310
		internal abstract bool MenuAccessKeysUnderlined { get; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00078E74 File Offset: 0x00077074
		internal virtual Size MinimizedWindowSize
		{
			get
			{
				return new Size(160, SystemInformation.CaptionHeight + 6 - 1);
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060018A8 RID: 6312
		internal abstract Size MinimumWindowSize { get; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00078E89 File Offset: 0x00077089
		internal virtual Size MinimumFixedToolWindowSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x00078E89 File Offset: 0x00077089
		internal virtual Size MinimumSizeableToolWindowSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00078E89 File Offset: 0x00077089
		internal virtual Size MinimumNoBorderWindowSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x00078E90 File Offset: 0x00077090
		internal virtual Size MinWindowTrackSize
		{
			get
			{
				return new Size(112, 27);
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060018AD RID: 6317
		internal abstract Rectangle VirtualScreen { get; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060018AE RID: 6318
		internal abstract Rectangle WorkingArea { get; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060018AF RID: 6319
		internal abstract Screen[] AllScreens { get; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060018B0 RID: 6320
		internal abstract bool ThemesEnabled { get; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00006F54 File Offset: 0x00005154
		internal virtual bool RequiresPositiveClientAreaSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005093A File Offset: 0x0004EB3A
		public virtual int ToolWindowCaptionHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00078E9B File Offset: 0x0007709B
		public virtual Size ToolWindowCaptionButtonSize
		{
			get
			{
				return new Size(15, 15);
			}
		}

		// Token: 0x060018B4 RID: 6324
		internal abstract void AudibleAlert(AlertType alert);

		// Token: 0x060018B5 RID: 6325
		internal abstract void GetDisplaySize(out Size size);

		// Token: 0x060018B6 RID: 6326
		internal abstract IntPtr CreateWindow(CreateParams cp);

		// Token: 0x060018B7 RID: 6327
		internal abstract void DestroyWindow(IntPtr handle);

		// Token: 0x060018B8 RID: 6328
		internal abstract FormWindowState GetWindowState(IntPtr handle);

		// Token: 0x060018B9 RID: 6329
		internal abstract void SetWindowState(IntPtr handle, FormWindowState state);

		// Token: 0x060018BA RID: 6330
		internal abstract void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max);

		// Token: 0x060018BB RID: 6331
		internal abstract void SetWindowStyle(IntPtr handle, CreateParams cp);

		// Token: 0x060018BC RID: 6332
		internal abstract void SetWindowTransparency(IntPtr handle, double transparency, Color key);

		// Token: 0x060018BD RID: 6333
		internal abstract TransparencySupport SupportsTransparency();

		// Token: 0x060018BE RID: 6334 RVA: 0x00078EA6 File Offset: 0x000770A6
		internal virtual void SetAllowDrop(IntPtr handle, bool value)
		{
			Console.Error.WriteLine("Drag and Drop is currently not supported on this platform");
		}

		// Token: 0x060018BF RID: 6335
		internal abstract void SetBorderStyle(IntPtr handle, FormBorderStyle border_style);

		// Token: 0x060018C0 RID: 6336
		internal abstract void SetMenu(IntPtr handle, Menu menu);

		// Token: 0x060018C1 RID: 6337
		internal abstract bool Text(IntPtr handle, string text);

		// Token: 0x060018C2 RID: 6338
		internal abstract bool SetVisible(IntPtr handle, bool visible, bool activate);

		// Token: 0x060018C3 RID: 6339
		internal abstract bool IsEnabled(IntPtr handle);

		// Token: 0x060018C4 RID: 6340
		internal abstract IntPtr SetParent(IntPtr handle, IntPtr parent);

		// Token: 0x060018C5 RID: 6341
		internal abstract IntPtr GetParent(IntPtr handle);

		// Token: 0x060018C6 RID: 6342
		internal abstract void UpdateWindow(IntPtr handle);

		// Token: 0x060018C7 RID: 6343
		internal abstract PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client);

		// Token: 0x060018C8 RID: 6344
		internal abstract void PaintEventEnd(ref Message msg, IntPtr handle, bool client);

		// Token: 0x060018C9 RID: 6345
		internal abstract void SetWindowPos(IntPtr handle, int x, int y, int width, int height);

		// Token: 0x060018CA RID: 6346
		internal abstract void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height);

		// Token: 0x060018CB RID: 6347
		internal abstract void Activate(IntPtr handle);

		// Token: 0x060018CC RID: 6348
		internal abstract void EnableWindow(IntPtr handle, bool Enable);

		// Token: 0x060018CD RID: 6349
		internal abstract void SetModal(IntPtr handle, bool Modal);

		// Token: 0x060018CE RID: 6350
		internal abstract void Invalidate(IntPtr handle, Rectangle rc, bool clear);

		// Token: 0x060018CF RID: 6351
		internal abstract void InvalidateNC(IntPtr handle);

		// Token: 0x060018D0 RID: 6352
		internal abstract IntPtr DefWndProc(ref Message msg);

		// Token: 0x060018D1 RID: 6353
		internal abstract void DoEvents();

		// Token: 0x060018D2 RID: 6354
		internal abstract bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags);

		// Token: 0x060018D3 RID: 6355
		internal abstract void PostQuitMessage(int exitCode);

		// Token: 0x060018D4 RID: 6356
		internal abstract bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax);

		// Token: 0x060018D5 RID: 6357
		internal abstract bool TranslateMessage(ref MSG msg);

		// Token: 0x060018D6 RID: 6358
		internal abstract IntPtr DispatchMessage(ref MSG msg);

		// Token: 0x060018D7 RID: 6359
		internal abstract bool SetZOrder(IntPtr hWnd, IntPtr AfterhWnd, bool Top, bool Bottom);

		// Token: 0x060018D8 RID: 6360
		internal abstract bool SetTopmost(IntPtr hWnd, bool Enabled);

		// Token: 0x060018D9 RID: 6361
		internal abstract bool SetOwner(IntPtr hWnd, IntPtr hWndOwner);

		// Token: 0x060018DA RID: 6362
		internal abstract bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect);

		// Token: 0x060018DB RID: 6363
		internal abstract void SetClipRegion(IntPtr hwnd, Region region);

		// Token: 0x060018DC RID: 6364
		internal abstract void SetCursor(IntPtr hwnd, IntPtr cursor);

		// Token: 0x060018DD RID: 6365
		internal abstract IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot);

		// Token: 0x060018DE RID: 6366
		internal abstract IntPtr DefineStdCursor(StdCursor id);

		// Token: 0x060018DF RID: 6367
		internal abstract void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y);

		// Token: 0x060018E0 RID: 6368
		internal abstract void GetCursorPos(IntPtr hwnd, out int x, out int y);

		// Token: 0x060018E1 RID: 6369
		internal abstract void SetCursorPos(IntPtr hwnd, int x, int y);

		// Token: 0x060018E2 RID: 6370
		internal abstract void ScreenToClient(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x060018E3 RID: 6371
		internal abstract void ClientToScreen(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x060018E4 RID: 6372
		internal abstract void GrabWindow(IntPtr hwnd, IntPtr ConfineToHwnd);

		// Token: 0x060018E5 RID: 6373
		internal abstract void GrabInfo(out IntPtr hwnd, out bool GrabConfined, out Rectangle GrabArea);

		// Token: 0x060018E6 RID: 6374
		internal abstract void UngrabWindow(IntPtr hwnd);

		// Token: 0x060018E7 RID: 6375
		internal abstract void SendAsyncMethod(AsyncMethodData method);

		// Token: 0x060018E8 RID: 6376
		internal abstract void SetTimer(Timer timer);

		// Token: 0x060018E9 RID: 6377
		internal abstract void KillTimer(Timer timer);

		// Token: 0x060018EA RID: 6378
		internal abstract void CreateCaret(IntPtr hwnd, int width, int height);

		// Token: 0x060018EB RID: 6379
		internal abstract void DestroyCaret(IntPtr hwnd);

		// Token: 0x060018EC RID: 6380
		internal abstract void SetCaretPos(IntPtr hwnd, int x, int y);

		// Token: 0x060018ED RID: 6381
		internal abstract void CaretVisible(IntPtr hwnd, bool visible);

		// Token: 0x060018EE RID: 6382
		internal abstract IntPtr GetFocus();

		// Token: 0x060018EF RID: 6383
		internal abstract void SetFocus(IntPtr hwnd);

		// Token: 0x060018F0 RID: 6384
		internal abstract IntPtr GetActive();

		// Token: 0x060018F1 RID: 6385
		internal abstract IntPtr GetPreviousWindow(IntPtr hwnd);

		// Token: 0x060018F2 RID: 6386
		internal abstract void ScrollWindow(IntPtr hwnd, Rectangle rectangle, int XAmount, int YAmount, bool with_children);

		// Token: 0x060018F3 RID: 6387
		internal abstract void ScrollWindow(IntPtr hwnd, int XAmount, int YAmount, bool with_children);

		// Token: 0x060018F4 RID: 6388
		internal abstract bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent);

		// Token: 0x060018F5 RID: 6389
		internal abstract Point GetMenuOrigin(IntPtr hwnd);

		// Token: 0x060018F6 RID: 6390
		internal abstract void MenuToScreen(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x060018F7 RID: 6391
		internal abstract void ScreenToMenu(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x060018F8 RID: 6392
		internal abstract void SetIcon(IntPtr handle, Icon icon);

		// Token: 0x060018F9 RID: 6393
		internal abstract void ClipboardClose(IntPtr handle);

		// Token: 0x060018FA RID: 6394
		internal abstract IntPtr ClipboardOpen(bool primary_selection);

		// Token: 0x060018FB RID: 6395
		internal abstract int ClipboardGetID(IntPtr handle, string format);

		// Token: 0x060018FC RID: 6396
		internal abstract void ClipboardStore(IntPtr handle, object obj, int id, XplatUI.ObjectToClipboard converter, bool copy);

		// Token: 0x060018FD RID: 6397
		internal abstract int[] ClipboardAvailableFormats(IntPtr handle);

		// Token: 0x060018FE RID: 6398
		internal abstract object ClipboardRetrieve(IntPtr handle, int id, XplatUI.ClipboardToObject converter);

		// Token: 0x060018FF RID: 6399
		internal abstract void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width);

		// Token: 0x06001900 RID: 6400
		internal abstract SizeF GetAutoScaleSize(Font font);

		// Token: 0x06001901 RID: 6401
		internal abstract IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam);

		// Token: 0x06001902 RID: 6402
		internal abstract bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam);

		// Token: 0x06001903 RID: 6403
		internal abstract object StartLoop(Thread thread);

		// Token: 0x06001904 RID: 6404
		internal abstract void EndLoop(Thread thread);

		// Token: 0x06001905 RID: 6405
		internal abstract void RequestNCRecalc(IntPtr hwnd);

		// Token: 0x06001906 RID: 6406
		internal abstract void ResetMouseHover(IntPtr hwnd);

		// Token: 0x06001907 RID: 6407
		internal abstract void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave);

		// Token: 0x06001908 RID: 6408 RVA: 0x00078EB8 File Offset: 0x000770B8
		internal virtual void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			offscreen_drawable = bitmap;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00078ED6 File Offset: 0x000770D6
		internal virtual void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			((Bitmap)offscreen_drawable).Dispose();
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00078EE3 File Offset: 0x000770E3
		internal virtual Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			return Graphics.FromImage((Bitmap)offscreen_drawable);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00078EF0 File Offset: 0x000770F0
		internal virtual void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			dest_dc.DrawImage((Bitmap)offscreen_drawable, r, r, GraphicsUnit.Pixel);
		}

		// Token: 0x0200029B RID: 667
		// (Invoke) Token: 0x0600190E RID: 6414
		internal delegate IntPtr WndProc(IntPtr hwnd, Msg msg, IntPtr wParam, IntPtr lParam);
	}
}
