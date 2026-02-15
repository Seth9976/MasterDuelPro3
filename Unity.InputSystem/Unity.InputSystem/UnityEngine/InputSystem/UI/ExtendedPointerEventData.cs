using System;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000115 RID: 277
	public class ExtendedPointerEventData : PointerEventData
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x00041FDF File Offset: 0x000401DF
		public ExtendedPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00041FE8 File Offset: 0x000401E8
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00041FF0 File Offset: 0x000401F0
		public InputControl control { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00041FF9 File Offset: 0x000401F9
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00042001 File Offset: 0x00040201
		public InputDevice device { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0004200A File Offset: 0x0004020A
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00042012 File Offset: 0x00040212
		public int touchId { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0004201B File Offset: 0x0004021B
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00042023 File Offset: 0x00040223
		public UIPointerType pointerType { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0004202C File Offset: 0x0004022C
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00042034 File Offset: 0x00040234
		public int uiToolkitPointerId { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0004203D File Offset: 0x0004023D
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00042045 File Offset: 0x00040245
		public Vector3 trackedDevicePosition { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0004204E File Offset: 0x0004024E
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00042056 File Offset: 0x00040256
		public Quaternion trackedDeviceOrientation { get; set; }

		// Token: 0x06000D20 RID: 3360 RVA: 0x00042060 File Offset: 0x00040260
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendLine("button: " + base.button.ToString());
			stringBuilder.AppendLine("clickTime: " + base.clickTime.ToString());
			stringBuilder.AppendLine("clickCount: " + base.clickCount.ToString());
			string text = "device: ";
			InputDevice device = this.device;
			stringBuilder.AppendLine(text + ((device != null) ? device.ToString() : null));
			stringBuilder.AppendLine("pointerType: " + this.pointerType.ToString());
			stringBuilder.AppendLine("touchId: " + this.touchId.ToString());
			stringBuilder.AppendLine("pressPosition: " + base.pressPosition.ToString());
			stringBuilder.AppendLine("trackedDevicePosition: " + this.trackedDevicePosition.ToString());
			stringBuilder.AppendLine("trackedDeviceOrientation: " + this.trackedDeviceOrientation.ToString());
			stringBuilder.AppendLine("pressure" + base.pressure.ToString());
			stringBuilder.AppendLine("radius: " + base.radius.ToString());
			stringBuilder.AppendLine("azimuthAngle: " + base.azimuthAngle.ToString());
			stringBuilder.AppendLine("altitudeAngle: " + base.altitudeAngle.ToString());
			stringBuilder.AppendLine("twist: " + base.twist.ToString());
			stringBuilder.AppendLine("displayIndex: " + base.displayIndex.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00042281 File Offset: 0x00040481
		internal static int MakePointerIdForTouch(int deviceId, int touchId)
		{
			return (deviceId << 24) + touchId;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00042289 File Offset: 0x00040489
		internal static int TouchIdFromPointerId(int pointerId)
		{
			return pointerId & 255;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00042294 File Offset: 0x00040494
		internal unsafe void ReadDeviceState()
		{
			Pen pen = this.control.parent as Pen;
			if (pen != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetPenPointerId(pen);
				base.pressure = pen.pressure.magnitude;
				base.azimuthAngle = (pen.tilt.value.x + 1f) * 3.1415927f / 2f;
				base.altitudeAngle = (pen.tilt.value.y + 1f) * 3.1415927f / 2f;
				base.twist = *pen.twist.value * 3.1415927f * 2f;
				base.displayIndex = pen.displayIndex.ReadValue();
				return;
			}
			TouchControl touchControl = this.control.parent as TouchControl;
			if (touchControl != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetTouchPointerId(touchControl);
				base.pressure = touchControl.pressure.magnitude;
				base.radius = *touchControl.radius.value;
				base.displayIndex = touchControl.displayIndex.ReadValue();
				return;
			}
			Touchscreen touchscreen = this.control.parent as Touchscreen;
			if (touchscreen != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetTouchPointerId(touchscreen.primaryTouch);
				base.pressure = touchscreen.pressure.magnitude;
				base.radius = *touchscreen.radius.value;
				base.displayIndex = touchscreen.displayIndex.ReadValue();
				return;
			}
			this.uiToolkitPointerId = PointerId.mousePointerId;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00042418 File Offset: 0x00040618
		private static int GetPenPointerId(Pen pen)
		{
			int i = 0;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				Pen otherPen = inputDevice as Pen;
				if (otherPen != null)
				{
					if (pen == otherPen)
					{
						return PointerId.penPointerIdBase + Mathf.Min(i, PointerId.penPointerCount - 1);
					}
					i++;
				}
			}
			return PointerId.penPointerIdBase;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00042498 File Offset: 0x00040698
		private static int GetTouchPointerId(TouchControl touchControl)
		{
			int i = ((Touchscreen)touchControl.device).touches.IndexOfReference(touchControl);
			return PointerId.touchPointerIdBase + Mathf.Clamp(i, 0, PointerId.touchPointerCount - 1);
		}
	}
}
