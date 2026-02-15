using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x0200039C RID: 924
	internal class Dnd
	{
		// Token: 0x06001DCF RID: 7631 RVA: 0x00094224 File Offset: 0x00092424
		static Dnd()
		{
			try
			{
				Dnd.InstallTrackingHandler(Dnd.DragTrackingHandler, IntPtr.Zero, IntPtr.Zero);
			}
			catch (EntryPointNotFoundException)
			{
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00002A07 File Offset: 0x00000C07
		internal Dnd()
		{
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00094274 File Offset: 0x00092474
		internal static void TrackingCallback(short message, IntPtr window, IntPtr data, IntPtr dragref)
		{
			XplatUICarbon.GetInstance().FlushQueue();
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00094280 File Offset: 0x00092480
		internal static DragDropEffects DragActionsToEffects(uint actions)
		{
			DragDropEffects dragDropEffects = DragDropEffects.None;
			if ((actions & 1U) != 0U)
			{
				dragDropEffects |= DragDropEffects.Copy;
			}
			if ((actions & 16U) != 0U)
			{
				dragDropEffects |= DragDropEffects.Move;
			}
			if ((actions & 4294967295U) != 0U)
			{
				dragDropEffects |= DragDropEffects.All;
			}
			return dragDropEffects;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000942B0 File Offset: 0x000924B0
		internal static DataObject DragToDataObject(IntPtr dragref)
		{
			uint num = 0U;
			ArrayList arrayList = new ArrayList();
			Dnd.CountDragItems(dragref, ref num);
			for (uint num2 = 1U; num2 <= num; num2 += 1U)
			{
				IntPtr zero = IntPtr.Zero;
				uint num3 = 0U;
				Dnd.GetDragItemReferenceNumber(dragref, num2, ref zero);
				Dnd.CountDragItemFlavors(dragref, zero, ref num3);
				for (uint num4 = 1U; num4 <= num3; num4 += 1U)
				{
					FlavorHandler flavorHandler = new FlavorHandler(dragref, zero, num4);
					if (flavorHandler.Supported)
					{
						arrayList.Add(flavorHandler);
					}
				}
			}
			if (arrayList.Count > 0)
			{
				return ((FlavorHandler)arrayList[0]).Convert(arrayList);
			}
			return new DataObject();
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00094348 File Offset: 0x00092548
		internal static bool HandleEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			QDPoint qdpoint = default(QDPoint);
			uint num = 0U;
			IntPtr zero = IntPtr.Zero;
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null || hwnd.Handle != handle)
			{
				return false;
			}
			Dnd.GetEventParameter(eventref, 1685217639U, 1685217639U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero, ref zero);
			XplatUICarbon.GetGlobalMouse(ref qdpoint);
			Dnd.GetDragAllowableActions(zero, ref num);
			Control control = Control.FromHandle(hwnd.Handle);
			DragDropEffects dragDropEffects = Dnd.DragActionsToEffects(num);
			DragEventArgs dragEventArgs = new DragEventArgs(Dnd.DragToDataObject(zero), 0, (int)qdpoint.x, (int)qdpoint.y, dragDropEffects, DragDropEffects.None);
			switch (kind)
			{
			case 18U:
			{
				bool allowDrop = control.AllowDrop;
				Dnd.SetEventParameter(eventref, 1668047975U, 1651470188U, (uint)Marshal.SizeOf(typeof(bool)), ref allowDrop);
				control.DndEnter(dragEventArgs);
				Dnd.effects = dragEventArgs.Effect;
				return false;
			}
			case 19U:
				control.DndOver(dragEventArgs);
				Dnd.effects = dragEventArgs.Effect;
				break;
			case 20U:
				control.DndLeave(dragEventArgs);
				break;
			case 21U:
				control.DndDrop(dragEventArgs);
				break;
			}
			return true;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00094471 File Offset: 0x00092671
		public void SetAllowDrop(Hwnd hwnd, bool allow)
		{
			if (hwnd.allow_drop == allow)
			{
				return;
			}
			hwnd.allow_drop = allow;
			Dnd.SetControlDragTrackingEnabled(hwnd.whole_window, true);
			Dnd.SetControlDragTrackingEnabled(hwnd.client_window, true);
		}

		// Token: 0x06001DD6 RID: 7638
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int InstallTrackingHandler(DragTrackingDelegate callback, IntPtr window, IntPtr data);

		// Token: 0x06001DD7 RID: 7639
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref IntPtr data);

		// Token: 0x06001DD8 RID: 7640
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetEventParameter(IntPtr eventref, uint name, uint type, uint size, ref bool data);

		// Token: 0x06001DD9 RID: 7641
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetControlDragTrackingEnabled(IntPtr view, bool enabled);

		// Token: 0x06001DDA RID: 7642
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CountDragItems(IntPtr dragref, ref uint count);

		// Token: 0x06001DDB RID: 7643
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CountDragItemFlavors(IntPtr dragref, IntPtr itemref, ref uint count);

		// Token: 0x06001DDC RID: 7644
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetDragItemReferenceNumber(IntPtr dragref, uint index, ref IntPtr itemref);

		// Token: 0x06001DDD RID: 7645
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetDragAllowableActions(IntPtr dragref, ref uint actions);

		// Token: 0x04001CEB RID: 7403
		private static DragDropEffects effects = DragDropEffects.None;

		// Token: 0x04001CEC RID: 7404
		private static DragTrackingDelegate DragTrackingHandler = new DragTrackingDelegate(Dnd.TrackingCallback);
	}
}
