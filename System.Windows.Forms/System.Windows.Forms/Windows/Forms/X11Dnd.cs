using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000232 RID: 562
	internal class X11Dnd
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x00070B58 File Offset: 0x0006ED58
		public X11Dnd(IntPtr display, X11Keyboard keyboard)
		{
			this.display = display;
			this.Init();
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00070C10 File Offset: 0x0006EE10
		public void SetAllowDrop(Hwnd hwnd, bool allow)
		{
			if (hwnd.allow_drop == allow)
			{
				return;
			}
			int[] array = new int[X11Dnd.XdndVersion.Length];
			for (int i = 0; i < X11Dnd.XdndVersion.Length; i++)
			{
				array[i] = X11Dnd.XdndVersion[i].ToInt32();
			}
			XplatUIX11.XChangeProperty(this.display, hwnd.whole_window, this.XdndAware, (IntPtr)4, 32, PropertyMode.Replace, array, allow ? 1 : 0);
			hwnd.allow_drop = allow;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00070C8C File Offset: 0x0006EE8C
		public bool HandleClientMessage(ref XEvent xevent)
		{
			if (xevent.ClientMessageEvent.message_type == this.XdndPosition)
			{
				return this.Accepting_HandlePositionEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndEnter)
			{
				return this.Accepting_HandleEnterEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndDrop)
			{
				return this.Accepting_HandleDropEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndLeave)
			{
				return this.Accepting_HandleLeaveEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndStatus)
			{
				return this.HandleStatusEvent(ref xevent);
			}
			return xevent.ClientMessageEvent.message_type == this.XdndFinished && this.HandleFinishedEvent(ref xevent);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00070D5C File Offset: 0x0006EF5C
		public bool HandleSelectionNotifyEvent(ref XEvent xevent)
		{
			X11Dnd.MimeHandler mimeHandler = this.FindHandler(xevent.SelectionEvent.target);
			if (mimeHandler == null)
			{
				return false;
			}
			if (this.data == null)
			{
				this.data = new DataObject();
			}
			mimeHandler.Converter.GetData(this, this.data, ref xevent);
			this.converts_pending--;
			if (this.converts_pending <= 0 && this.position_recieved)
			{
				this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, DragDropEffects.None);
				this.control.DndEnter(this.drag_event);
				this.SendStatus(this.source, this.drag_event.Effect);
				this.status_sent = true;
			}
			return true;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00070E1C File Offset: 0x0006F01C
		public bool HandleSelectionRequestEvent(ref XEvent xevent)
		{
			if (xevent.SelectionRequestEvent.selection != this.XdndSelection)
			{
				return false;
			}
			X11Dnd.MimeHandler mimeHandler = this.FindHandler(xevent.SelectionRequestEvent.target);
			if (mimeHandler == null)
			{
				return false;
			}
			mimeHandler.Converter.SetData(this, this.drag_data.Data, ref xevent);
			return true;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00070E74 File Offset: 0x0006F074
		private bool QueryContinue(bool escape, DragAction action)
		{
			QueryContinueDragEventArgs queryContinueDragEventArgs = new QueryContinueDragEventArgs((int)XplatUI.State.ModifierKeys, escape, action);
			Control control = this.MwfWindow(this.source);
			if (control == null)
			{
				this.tracking = false;
				return false;
			}
			control.DndContinueDrag(queryContinueDragEventArgs);
			switch (queryContinueDragEventArgs.Action)
			{
			case DragAction.Continue:
				return true;
			case DragAction.Drop:
				this.SendDrop(this.drag_data.LastTopLevel, this.source, IntPtr.Zero);
				this.tracking = false;
				return true;
			case DragAction.Cancel:
				this.drag_data.Reset();
				control.InternalCapture = false;
				break;
			}
			this.SendLeave(this.drag_data.LastTopLevel, this.toplevel);
			this.RestoreDefaultCursor();
			this.tracking = false;
			return false;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00070F27 File Offset: 0x0006F127
		private void RestoreDefaultCursor()
		{
			XplatUIX11.XChangeActivePointerGrab(this.display, EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.PointerMotionMask | EventMask.ButtonMotionMask, Cursors.Default.Handle, IntPtr.Zero);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00070F4C File Offset: 0x0006F14C
		private void GiveFeedback(IntPtr action)
		{
			GiveFeedbackEventArgs giveFeedbackEventArgs = new GiveFeedbackEventArgs(this.EffectFromAction(this.drag_data.Action), true);
			this.MwfWindow(this.source).DndFeedback(giveFeedbackEventArgs);
			if (giveFeedbackEventArgs.UseDefaultCursors)
			{
				Cursor cursor = this.CursorNo;
				if (this.drag_data.WillAccept)
				{
					if (action == this.XdndActionCopy)
					{
						cursor = this.CursorCopy;
					}
					else if (action == this.XdndActionLink)
					{
						cursor = this.CursorLink;
					}
					else if (action == this.XdndActionMove)
					{
						cursor = this.CursorMove;
					}
				}
				XplatUIX11.XChangeActivePointerGrab(this.display, EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.PointerMotionMask | EventMask.ButtonMotionMask, cursor.Handle, IntPtr.Zero);
			}
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00071000 File Offset: 0x0006F200
		private void SetProperty(ref XEvent xevent, IntPtr data, int length)
		{
			XEvent xevent2 = default(XEvent);
			xevent2.SelectionEvent.type = XEventName.SelectionNotify;
			xevent2.SelectionEvent.send_event = true;
			xevent2.SelectionEvent.display = this.display;
			xevent2.SelectionEvent.selection = xevent.SelectionRequestEvent.selection;
			xevent2.SelectionEvent.target = xevent.SelectionRequestEvent.target;
			xevent2.SelectionEvent.requestor = xevent.SelectionRequestEvent.requestor;
			xevent2.SelectionEvent.time = xevent.SelectionRequestEvent.time;
			xevent2.SelectionEvent.property = IntPtr.Zero;
			XplatUIX11.XChangeProperty(this.display, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, data, length);
			xevent2.SelectionEvent.property = xevent.SelectionRequestEvent.property;
			XplatUIX11.XSendEvent(this.display, xevent.SelectionRequestEvent.requestor, false, (IntPtr)0, ref xevent2);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00071117 File Offset: 0x0006F317
		private void Reset()
		{
			this.ResetSourceData();
			this.ResetTargetData();
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00071125 File Offset: 0x0006F325
		private void ResetSourceData()
		{
			this.converts_pending = 0;
			this.data = null;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00071135 File Offset: 0x0006F335
		private void ResetTargetData()
		{
			this.position_recieved = false;
			this.status_sent = false;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00071145 File Offset: 0x0006F345
		private bool Accepting_HandleEnterEvent(ref XEvent xevent)
		{
			this.Reset();
			this.source = xevent.ClientMessageEvent.ptr1;
			this.toplevel = xevent.AnyEvent.window;
			this.target = IntPtr.Zero;
			this.ConvertData(ref xevent);
			return true;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00071184 File Offset: 0x0006F384
		private bool Accepting_HandlePositionEvent(ref XEvent xevent)
		{
			this.pos_x = (int)xevent.ClientMessageEvent.ptr3 >> 16;
			this.pos_y = (int)xevent.ClientMessageEvent.ptr3 & 65535;
			if (this.MwfWindow(this.source) == null)
			{
				this.allowed = this.EffectsFromX11Source(this.source, xevent.ClientMessageEvent.ptr5) | DragDropEffects.Copy;
			}
			else
			{
				this.allowed = this.drag_data.AllowedEffects;
			}
			IntPtr intPtr = XplatUIX11.XRootWindow(this.display, 0);
			IntPtr intPtr2 = this.toplevel;
			IntPtr intPtr3 = IntPtr.Zero;
			for (;;)
			{
				IntPtr zero = IntPtr.Zero;
				int num;
				int num2;
				if (!XplatUIX11.XTranslateCoordinates(this.display, intPtr, intPtr2, this.pos_x, this.pos_y, out num, out num2, out zero) || zero == IntPtr.Zero)
				{
					break;
				}
				intPtr2 = zero;
				Hwnd hwnd = Hwnd.ObjectFromHandle(intPtr2);
				if (hwnd != null)
				{
					Control control = Control.FromHandle(hwnd.client_window);
					if (control != null && control.allow_drop)
					{
						intPtr3 = intPtr2;
					}
				}
			}
			if (intPtr3 != IntPtr.Zero)
			{
				intPtr2 = intPtr3;
			}
			if (this.target != intPtr2)
			{
				this.Finish();
			}
			this.target = intPtr2;
			Hwnd hwnd2 = Hwnd.ObjectFromHandle(this.target);
			if (hwnd2 == null)
			{
				return true;
			}
			Control control2 = Control.FromHandle(hwnd2.client_window);
			if (control2 == null)
			{
				return true;
			}
			if (!control2.allow_drop)
			{
				this.SendStatus(this.source, DragDropEffects.None);
				this.Finish();
				return true;
			}
			this.control = control2;
			this.position_recieved = true;
			if (this.converts_pending > 0)
			{
				return true;
			}
			if (!this.status_sent)
			{
				this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, DragDropEffects.None);
				this.control.DndEnter(this.drag_event);
				this.SendStatus(this.source, this.drag_event.Effect);
				this.status_sent = true;
			}
			else
			{
				this.drag_event.x = this.pos_x;
				this.drag_event.y = this.pos_y;
				this.control.DndOver(this.drag_event);
				this.SendStatus(this.source, this.drag_event.Effect);
			}
			return true;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000713B4 File Offset: 0x0006F5B4
		private void Finish()
		{
			if (this.control != null)
			{
				if (this.drag_event == null)
				{
					if (this.data == null)
					{
						this.data = new DataObject();
					}
					this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, DragDropEffects.None);
				}
				this.control.DndLeave(this.drag_event);
				this.control = null;
			}
			this.ResetTargetData();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00071428 File Offset: 0x0006F628
		private bool Accepting_HandleDropEvent(ref XEvent xevent)
		{
			if (this.control != null && this.drag_event != null)
			{
				this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, this.drag_event.Effect);
				this.control.DndDrop(this.drag_event);
			}
			this.SendFinished();
			return true;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0007148C File Offset: 0x0006F68C
		private bool Accepting_HandleLeaveEvent(ref XEvent xevent)
		{
			if (this.control != null && this.drag_event != null)
			{
				this.control.DndLeave(this.drag_event);
			}
			return true;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000714B0 File Offset: 0x0006F6B0
		private bool HandleStatusEvent(ref XEvent xevent)
		{
			if (this.drag_data != null && this.drag_data.State == X11Dnd.DragState.Entered)
			{
				if (!this.QueryContinue(false, DragAction.Continue))
				{
					return true;
				}
				this.drag_data.WillAccept = ((int)xevent.ClientMessageEvent.ptr2 & 1) != 0;
				this.GiveFeedback(xevent.ClientMessageEvent.ptr5);
			}
			return true;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00006F54 File Offset: 0x00005154
		private bool HandleFinishedEvent(ref XEvent xevent)
		{
			return true;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00071514 File Offset: 0x0006F714
		private DragDropEffects EffectsFromX11Source(IntPtr source, IntPtr action_atom)
		{
			DragDropEffects dragDropEffects = DragDropEffects.None;
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(this.display, source, this.XdndActionList, IntPtr.Zero, new IntPtr(32), false, (IntPtr)0, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
			int num2 = Marshal.SizeOf(typeof(IntPtr));
			for (int i = 0; i < intPtr2.ToInt32(); i++)
			{
				IntPtr intPtr4 = Marshal.ReadIntPtr(zero, i * num2);
				dragDropEffects |= this.EffectFromAction(intPtr4);
			}
			if (dragDropEffects == DragDropEffects.None)
			{
				dragDropEffects = this.EffectFromAction(action_atom);
			}
			return dragDropEffects;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000715A6 File Offset: 0x0006F7A6
		private DragDropEffects EffectFromAction(IntPtr action)
		{
			if (action == this.XdndActionCopy)
			{
				return DragDropEffects.Copy;
			}
			if (action == this.XdndActionMove)
			{
				return DragDropEffects.Move;
			}
			if (action == this.XdndActionLink)
			{
				return DragDropEffects.Link;
			}
			return DragDropEffects.None;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000715DC File Offset: 0x0006F7DC
		private IntPtr ActionFromEffect(DragDropEffects effect)
		{
			IntPtr intPtr = IntPtr.Zero;
			if ((effect & DragDropEffects.Copy) != DragDropEffects.None)
			{
				intPtr = this.XdndActionCopy;
			}
			else if ((effect & DragDropEffects.Move) != DragDropEffects.None)
			{
				intPtr = this.XdndActionMove;
			}
			else if ((effect & DragDropEffects.Link) != DragDropEffects.None)
			{
				intPtr = this.XdndActionLink;
			}
			return intPtr;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00071618 File Offset: 0x0006F818
		private bool ConvertData(ref XEvent xevent)
		{
			bool flag = false;
			if (this.MwfWindow(this.source) == null || this.drag_data == null)
			{
				foreach (IntPtr intPtr in this.SourceSupportedList(ref xevent))
				{
					X11Dnd.MimeHandler mimeHandler = this.FindHandler(intPtr);
					if (mimeHandler != null)
					{
						XplatUIX11.XConvertSelection(this.display, this.XdndSelection, mimeHandler.Type, mimeHandler.NonProtocol, this.toplevel, IntPtr.Zero);
						this.converts_pending++;
						flag = true;
					}
				}
				return flag;
			}
			if (!this.tracking)
			{
				return false;
			}
			IDataObject dataObject = this.drag_data.Data as IDataObject;
			if (dataObject != null)
			{
				this.data = dataObject;
			}
			else
			{
				if (this.data == null)
				{
					this.data = new DataObject();
				}
				this.SetDataWithFormats(this.drag_data.Data);
			}
			return true;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000716EF File Offset: 0x0006F8EF
		private void SetDataWithFormats(object value)
		{
			if (value is string)
			{
				this.data.SetData(DataFormats.Text, value);
				this.data.SetData(DataFormats.UnicodeText, value);
			}
			this.data.SetData(value);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00071728 File Offset: 0x0006F928
		private X11Dnd.MimeHandler FindHandler(IntPtr atom)
		{
			if (atom == IntPtr.Zero)
			{
				return null;
			}
			foreach (X11Dnd.MimeHandler mimeHandler in this.MimeHandlers)
			{
				if (mimeHandler.Type == atom)
				{
					return mimeHandler;
				}
			}
			return null;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00071770 File Offset: 0x0006F970
		private void SendStatus(IntPtr source, DragDropEffects effect)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = source;
			xevent.ClientMessageEvent.message_type = this.XdndStatus;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = this.toplevel;
			if (effect != DragDropEffects.None && (effect & this.allowed) != DragDropEffects.None)
			{
				xevent.ClientMessageEvent.ptr2 = (IntPtr)1;
			}
			xevent.ClientMessageEvent.ptr5 = this.ActionFromEffect(effect);
			XplatUIX11.XSendEvent(this.display, source, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0007182C File Offset: 0x0006FA2C
		private void SendDrop(IntPtr handle, IntPtr from, IntPtr time)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = handle;
			xevent.ClientMessageEvent.message_type = this.XdndDrop;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = from;
			xevent.ClientMessageEvent.ptr3 = time;
			XplatUIX11.XSendEvent(this.display, handle, false, IntPtr.Zero, ref xevent);
			this.dropped = true;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000718C4 File Offset: 0x0006FAC4
		private void SendLeave(IntPtr handle, IntPtr from)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = handle;
			xevent.ClientMessageEvent.message_type = this.XdndLeave;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = from;
			XplatUIX11.XSendEvent(this.display, handle, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00071948 File Offset: 0x0006FB48
		private void SendFinished()
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = this.source;
			xevent.ClientMessageEvent.message_type = this.XdndFinished;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = this.toplevel;
			XplatUIX11.XSendEvent(this.display, this.source, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000719DC File Offset: 0x0006FBDC
		private void Init()
		{
			this.XdndAware = XplatUIX11.XInternAtom(this.display, "XdndAware", false);
			this.XdndEnter = XplatUIX11.XInternAtom(this.display, "XdndEnter", false);
			this.XdndLeave = XplatUIX11.XInternAtom(this.display, "XdndLeave", false);
			this.XdndPosition = XplatUIX11.XInternAtom(this.display, "XdndPosition", false);
			this.XdndStatus = XplatUIX11.XInternAtom(this.display, "XdndStatus", false);
			this.XdndDrop = XplatUIX11.XInternAtom(this.display, "XdndDrop", false);
			this.XdndSelection = XplatUIX11.XInternAtom(this.display, "XdndSelection", false);
			this.XdndFinished = XplatUIX11.XInternAtom(this.display, "XdndFinished", false);
			this.XdndTypeList = XplatUIX11.XInternAtom(this.display, "XdndTypeList", false);
			this.XdndActionCopy = XplatUIX11.XInternAtom(this.display, "XdndActionCopy", false);
			this.XdndActionMove = XplatUIX11.XInternAtom(this.display, "XdndActionMove", false);
			this.XdndActionLink = XplatUIX11.XInternAtom(this.display, "XdndActionLink", false);
			this.XdndActionList = XplatUIX11.XInternAtom(this.display, "XdndActionList", false);
			foreach (X11Dnd.MimeHandler mimeHandler in this.MimeHandlers)
			{
				mimeHandler.Type = XplatUIX11.XInternAtom(this.display, mimeHandler.Name, false);
				mimeHandler.NonProtocol = XplatUIX11.XInternAtom(this.display, "MWFNonP+" + mimeHandler.Name, false);
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00071B68 File Offset: 0x0006FD68
		private IntPtr[] SourceSupportedList(ref XEvent xevent)
		{
			IntPtr[] array;
			if (((int)xevent.ClientMessageEvent.ptr2 & 1) == 0)
			{
				array = new IntPtr[]
				{
					xevent.ClientMessageEvent.ptr3,
					xevent.ClientMessageEvent.ptr4,
					xevent.ClientMessageEvent.ptr5
				};
			}
			else
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				int num;
				IntPtr intPtr2;
				IntPtr intPtr3;
				XplatUIX11.XGetWindowProperty(this.display, this.source, this.XdndTypeList, IntPtr.Zero, new IntPtr(32), false, (IntPtr)4, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
				array = new IntPtr[intPtr2.ToInt32()];
				for (int i = 0; i < intPtr2.ToInt32(); i++)
				{
					array[i] = (IntPtr)Marshal.ReadInt32(zero, i * Marshal.SizeOf(typeof(int)));
				}
				XplatUIX11.XFree(zero);
			}
			return array;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00071C4C File Offset: 0x0006FE4C
		private string GetText(ref XEvent xevent, bool unicode)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			IntPtr zero;
			for (;;)
			{
				zero = IntPtr.Zero;
				IntPtr intPtr;
				int num2;
				IntPtr intPtr2;
				IntPtr intPtr3;
				if (XplatUIX11.XGetWindowProperty(this.display, xevent.AnyEvent.window, xevent.SelectionEvent.property, IntPtr.Zero, new IntPtr(16777215), false, (IntPtr)0, out intPtr, out num2, out intPtr2, out intPtr3, ref zero) != 0)
				{
					break;
				}
				if (unicode)
				{
					stringBuilder.Append(Marshal.PtrToStringUni(zero));
				}
				else
				{
					stringBuilder.Append(Marshal.PtrToStringAnsi(zero));
				}
				num += intPtr2.ToInt32();
				XplatUIX11.XFree(zero);
				if (intPtr3.ToInt32() <= 0)
				{
					goto IL_009C;
				}
			}
			XplatUIX11.XFree(zero);
			IL_009C:
			if (num == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00071D00 File Offset: 0x0006FF00
		private MemoryStream GetData(ref XEvent xevent)
		{
			int num = 0;
			MemoryStream memoryStream = new MemoryStream();
			IntPtr zero;
			for (;;)
			{
				zero = IntPtr.Zero;
				IntPtr intPtr;
				int num2;
				IntPtr intPtr2;
				IntPtr intPtr3;
				if (XplatUIX11.XGetWindowProperty(this.display, xevent.AnyEvent.window, xevent.SelectionEvent.property, IntPtr.Zero, new IntPtr(16777215), false, (IntPtr)0, out intPtr, out num2, out intPtr2, out intPtr3, ref zero) != 0)
				{
					break;
				}
				for (int i = 0; i < intPtr2.ToInt32(); i++)
				{
					memoryStream.WriteByte(Marshal.ReadByte(zero, i));
				}
				num += intPtr2.ToInt32();
				XplatUIX11.XFree(zero);
				if (intPtr3.ToInt32() <= 0)
				{
					return memoryStream;
				}
			}
			XplatUIX11.XFree(zero);
			return memoryStream;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00071DB0 File Offset: 0x0006FFB0
		private Control MwfWindow(IntPtr window)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(window);
			if (hwnd == null)
			{
				return null;
			}
			Control control = Control.FromHandle(hwnd.client_window);
			if (control == null)
			{
				control = Control.FromHandle(window);
			}
			return control;
		}

		// Token: 0x04000DD8 RID: 3544
		private X11Dnd.MimeHandler[] MimeHandlers = new X11Dnd.MimeHandler[]
		{
			new X11Dnd.MimeHandler("text/plain", new X11Dnd.TextConverter()),
			new X11Dnd.MimeHandler("text/plain", new X11Dnd.TextConverter(), new string[]
			{
				"System.String",
				DataFormats.Text
			}),
			new X11Dnd.MimeHandler("text/html", new X11Dnd.HtmlConverter(), new string[] { DataFormats.Html }),
			new X11Dnd.MimeHandler("text/uri-list", new X11Dnd.UriListConverter(), new string[] { DataFormats.FileDrop }),
			new X11Dnd.MimeHandler("application/x-mono-serialized-object", new X11Dnd.SerializedObjectConverter())
		};

		// Token: 0x04000DD9 RID: 3545
		private static readonly IntPtr[] XdndVersion = new IntPtr[]
		{
			new IntPtr(4)
		};

		// Token: 0x04000DDA RID: 3546
		private IntPtr display;

		// Token: 0x04000DDB RID: 3547
		private X11Dnd.DragData drag_data;

		// Token: 0x04000DDC RID: 3548
		private IntPtr XdndAware;

		// Token: 0x04000DDD RID: 3549
		private IntPtr XdndSelection;

		// Token: 0x04000DDE RID: 3550
		private IntPtr XdndEnter;

		// Token: 0x04000DDF RID: 3551
		private IntPtr XdndLeave;

		// Token: 0x04000DE0 RID: 3552
		private IntPtr XdndPosition;

		// Token: 0x04000DE1 RID: 3553
		private IntPtr XdndDrop;

		// Token: 0x04000DE2 RID: 3554
		private IntPtr XdndFinished;

		// Token: 0x04000DE3 RID: 3555
		private IntPtr XdndStatus;

		// Token: 0x04000DE4 RID: 3556
		private IntPtr XdndTypeList;

		// Token: 0x04000DE5 RID: 3557
		private IntPtr XdndActionCopy;

		// Token: 0x04000DE6 RID: 3558
		private IntPtr XdndActionMove;

		// Token: 0x04000DE7 RID: 3559
		private IntPtr XdndActionLink;

		// Token: 0x04000DE8 RID: 3560
		private IntPtr XdndActionList;

		// Token: 0x04000DE9 RID: 3561
		private int converts_pending;

		// Token: 0x04000DEA RID: 3562
		private bool position_recieved;

		// Token: 0x04000DEB RID: 3563
		private bool status_sent;

		// Token: 0x04000DEC RID: 3564
		private IntPtr target;

		// Token: 0x04000DED RID: 3565
		private IntPtr source;

		// Token: 0x04000DEE RID: 3566
		private IntPtr toplevel;

		// Token: 0x04000DEF RID: 3567
		private IDataObject data;

		// Token: 0x04000DF0 RID: 3568
		private Control control;

		// Token: 0x04000DF1 RID: 3569
		private int pos_x;

		// Token: 0x04000DF2 RID: 3570
		private int pos_y;

		// Token: 0x04000DF3 RID: 3571
		private DragDropEffects allowed;

		// Token: 0x04000DF4 RID: 3572
		private DragEventArgs drag_event;

		// Token: 0x04000DF5 RID: 3573
		private Cursor CursorNo;

		// Token: 0x04000DF6 RID: 3574
		private Cursor CursorCopy;

		// Token: 0x04000DF7 RID: 3575
		private Cursor CursorMove;

		// Token: 0x04000DF8 RID: 3576
		private Cursor CursorLink;

		// Token: 0x04000DF9 RID: 3577
		private bool tracking;

		// Token: 0x04000DFA RID: 3578
		private bool dropped;

		// Token: 0x02000233 RID: 563
		private enum DragState
		{
			// Token: 0x04000DFC RID: 3580
			None,
			// Token: 0x04000DFD RID: 3581
			Beginning,
			// Token: 0x04000DFE RID: 3582
			Dragging,
			// Token: 0x04000DFF RID: 3583
			Entered
		}

		// Token: 0x02000234 RID: 564
		private interface IDataConverter
		{
			// Token: 0x060016D8 RID: 5848
			void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent);

			// Token: 0x060016D9 RID: 5849
			void SetData(X11Dnd dnd, object data, ref XEvent xevent);
		}

		// Token: 0x02000235 RID: 565
		private class MimeHandler
		{
			// Token: 0x060016DA RID: 5850 RVA: 0x00071DF6 File Offset: 0x0006FFF6
			public MimeHandler(string name, X11Dnd.IDataConverter converter)
				: this(name, converter, new string[] { name })
			{
			}

			// Token: 0x060016DB RID: 5851 RVA: 0x00071E0A File Offset: 0x0007000A
			public MimeHandler(string name, X11Dnd.IDataConverter converter, params string[] aliases)
			{
				this.Name = name;
				this.Converter = converter;
				this.Aliases = aliases;
			}

			// Token: 0x060016DC RID: 5852 RVA: 0x00071E27 File Offset: 0x00070027
			public override string ToString()
			{
				return "MimeHandler {" + this.Name + "}";
			}

			// Token: 0x04000E00 RID: 3584
			public string Name;

			// Token: 0x04000E01 RID: 3585
			public string[] Aliases;

			// Token: 0x04000E02 RID: 3586
			public IntPtr Type;

			// Token: 0x04000E03 RID: 3587
			public IntPtr NonProtocol;

			// Token: 0x04000E04 RID: 3588
			public X11Dnd.IDataConverter Converter;
		}

		// Token: 0x02000236 RID: 566
		private class SerializedObjectConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060016DD RID: 5853 RVA: 0x00071E40 File Offset: 0x00070040
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				MemoryStream data2 = dnd.GetData(ref xevent);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (data2.Length == 0L)
				{
					return;
				}
				data2.Seek(0L, SeekOrigin.Begin);
				object obj = binaryFormatter.Deserialize(data2);
				data.SetData(obj);
			}

			// Token: 0x060016DE RID: 5854 RVA: 0x00071E80 File Offset: 0x00070080
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				if (data == null)
				{
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				new BinaryFormatter().Serialize(memoryStream, data);
				IntPtr intPtr = Marshal.AllocHGlobal((int)memoryStream.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				int num = 0;
				while ((long)num < memoryStream.Length)
				{
					Marshal.WriteByte(intPtr, num, (byte)memoryStream.ReadByte());
					num++;
				}
				dnd.SetProperty(ref xevent, intPtr, (int)memoryStream.Length);
			}
		}

		// Token: 0x02000237 RID: 567
		private class HtmlConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060016E0 RID: 5856 RVA: 0x00071EEC File Offset: 0x000700EC
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				string text = dnd.GetText(ref xevent, false);
				if (text == null)
				{
					return;
				}
				data.SetData(DataFormats.Text, text);
				data.SetData(DataFormats.UnicodeText, text);
			}

			// Token: 0x060016E1 RID: 5857 RVA: 0x00071F20 File Offset: 0x00070120
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				string text = data as string;
				if (text == null)
				{
					return;
				}
				IntPtr intPtr;
				int num;
				if (xevent.SelectionRequestEvent.target == (IntPtr)31)
				{
					byte[] bytes = Encoding.ASCII.GetBytes(text);
					intPtr = Marshal.AllocHGlobal(bytes.Length);
					num = bytes.Length;
					for (int i = 0; i < num; i++)
					{
						Marshal.WriteByte(intPtr, i, bytes[i]);
					}
				}
				else
				{
					intPtr = Marshal.StringToHGlobalAnsi(text);
					num = 0;
					while (Marshal.ReadByte(intPtr, num) != 0)
					{
						num++;
					}
				}
				dnd.SetProperty(ref xevent, intPtr, num);
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x02000238 RID: 568
		private class TextConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060016E3 RID: 5859 RVA: 0x00071FB0 File Offset: 0x000701B0
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				string text = dnd.GetText(ref xevent, true);
				if (text == null)
				{
					return;
				}
				data.SetData(DataFormats.Text, text);
				data.SetData(DataFormats.UnicodeText, text);
			}

			// Token: 0x060016E4 RID: 5860 RVA: 0x00071FE4 File Offset: 0x000701E4
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				string text = data as string;
				if (text == null)
				{
					IDataObject dataObject = data as IDataObject;
					if (dataObject == null)
					{
						return;
					}
					text = (string)dataObject.GetData("System.String", true);
				}
				IntPtr intPtr;
				int num;
				if (xevent.SelectionRequestEvent.target == (IntPtr)31)
				{
					byte[] bytes = Encoding.ASCII.GetBytes(text);
					intPtr = Marshal.AllocHGlobal(bytes.Length);
					num = bytes.Length;
					for (int i = 0; i < num; i++)
					{
						Marshal.WriteByte(intPtr, i, bytes[i]);
					}
				}
				else
				{
					intPtr = Marshal.StringToHGlobalAnsi(text);
					num = 0;
					while (Marshal.ReadByte(intPtr, num) != 0)
					{
						num++;
					}
				}
				dnd.SetProperty(ref xevent, intPtr, num);
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x02000239 RID: 569
		private class UriListConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060016E6 RID: 5862 RVA: 0x00072094 File Offset: 0x00070294
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				string text = dnd.GetText(ref xevent, false);
				if (text == null)
				{
					return;
				}
				ArrayList arrayList = new ArrayList();
				foreach (string text2 in text.Split(new char[] { '\r', '\n' }))
				{
					if (!text2.StartsWith("#"))
					{
						try
						{
							Uri uri = new Uri(text2);
							arrayList.Add(uri.LocalPath);
						}
						catch
						{
						}
					}
				}
				string[] array2 = (string[])arrayList.ToArray(typeof(string));
				if (array2.Length < 1)
				{
					return;
				}
				data.SetData(DataFormats.FileDrop, array2);
				data.SetData("FileName", array2[0]);
				data.SetData("FileNameW", array2[0]);
			}

			// Token: 0x060016E7 RID: 5863 RVA: 0x00072164 File Offset: 0x00070364
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				string[] array = data as string[];
				if (array == null)
				{
					IDataObject dataObject = data as IDataObject;
					if (dataObject == null)
					{
						return;
					}
					array = dataObject.GetData(DataFormats.FileDrop, true) as string[];
				}
				if (array == null)
				{
					return;
				}
				StringBuilder stringBuilder = new StringBuilder();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Uri uri = new Uri(array2[i]);
					stringBuilder.Append(uri.ToString());
					stringBuilder.Append("\r\n");
				}
				IntPtr intPtr = Marshal.StringToHGlobalAnsi(stringBuilder.ToString());
				int num = 0;
				while (Marshal.ReadByte(intPtr, num) != 0)
				{
					num++;
				}
				dnd.SetProperty(ref xevent, intPtr, num);
			}
		}

		// Token: 0x0200023A RID: 570
		private class DragData
		{
			// Token: 0x060016E9 RID: 5865 RVA: 0x00072207 File Offset: 0x00070407
			public void Reset()
			{
				this.State = X11Dnd.DragState.None;
				this.Data = null;
				this.SupportedTypes = null;
				this.WillAccept = false;
			}

			// Token: 0x04000E05 RID: 3589
			public X11Dnd.DragState State;

			// Token: 0x04000E06 RID: 3590
			public object Data;

			// Token: 0x04000E07 RID: 3591
			public IntPtr Action;

			// Token: 0x04000E08 RID: 3592
			public IntPtr[] SupportedTypes;

			// Token: 0x04000E09 RID: 3593
			public DragDropEffects AllowedEffects;

			// Token: 0x04000E0A RID: 3594
			public IntPtr LastTopLevel;

			// Token: 0x04000E0B RID: 3595
			public bool WillAccept;
		}
	}
}
