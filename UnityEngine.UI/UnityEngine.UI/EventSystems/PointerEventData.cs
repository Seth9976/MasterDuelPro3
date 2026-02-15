using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000099 RID: 153
	public class PointerEventData : BaseEventData
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00018B96 File Offset: 0x00016D96
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00018B9E File Offset: 0x00016D9E
		public GameObject pointerEnter { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00018BA7 File Offset: 0x00016DA7
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00018BAF File Offset: 0x00016DAF
		public GameObject lastPress { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00018BB8 File Offset: 0x00016DB8
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00018BC0 File Offset: 0x00016DC0
		public GameObject rawPointerPress { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00018BC9 File Offset: 0x00016DC9
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00018BD1 File Offset: 0x00016DD1
		public GameObject pointerDrag { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00018BDA File Offset: 0x00016DDA
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00018BE2 File Offset: 0x00016DE2
		public GameObject pointerClick { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00018BEB File Offset: 0x00016DEB
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00018BF3 File Offset: 0x00016DF3
		public RaycastResult pointerCurrentRaycast { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00018BFC File Offset: 0x00016DFC
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x00018C04 File Offset: 0x00016E04
		public RaycastResult pointerPressRaycast { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00018C0D File Offset: 0x00016E0D
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00018C15 File Offset: 0x00016E15
		public bool eligibleForClick { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00018C1E File Offset: 0x00016E1E
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00018C26 File Offset: 0x00016E26
		public int displayIndex { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00018C2F File Offset: 0x00016E2F
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00018C37 File Offset: 0x00016E37
		public int pointerId { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00018C40 File Offset: 0x00016E40
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00018C48 File Offset: 0x00016E48
		public Vector2 position { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00018C51 File Offset: 0x00016E51
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00018C59 File Offset: 0x00016E59
		public Vector2 delta { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00018C62 File Offset: 0x00016E62
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00018C6A File Offset: 0x00016E6A
		public Vector2 pressPosition { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00018C73 File Offset: 0x00016E73
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00018C7B File Offset: 0x00016E7B
		[Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public Vector3 worldPosition { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00018C84 File Offset: 0x00016E84
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00018C8C File Offset: 0x00016E8C
		[Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public Vector3 worldNormal { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00018C95 File Offset: 0x00016E95
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00018C9D File Offset: 0x00016E9D
		public float clickTime { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00018CA6 File Offset: 0x00016EA6
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00018CAE File Offset: 0x00016EAE
		public int clickCount { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00018CB7 File Offset: 0x00016EB7
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00018CBF File Offset: 0x00016EBF
		public Vector2 scrollDelta { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00018CC8 File Offset: 0x00016EC8
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00018CD0 File Offset: 0x00016ED0
		public bool useDragThreshold { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00018CD9 File Offset: 0x00016ED9
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00018CE1 File Offset: 0x00016EE1
		public bool dragging { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00018CEA File Offset: 0x00016EEA
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00018CF2 File Offset: 0x00016EF2
		public PointerEventData.InputButton button { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00018CFB File Offset: 0x00016EFB
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00018D03 File Offset: 0x00016F03
		public float pressure { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00018D0C File Offset: 0x00016F0C
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00018D14 File Offset: 0x00016F14
		public float tangentialPressure { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00018D1D File Offset: 0x00016F1D
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00018D25 File Offset: 0x00016F25
		public float altitudeAngle { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00018D2E File Offset: 0x00016F2E
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00018D36 File Offset: 0x00016F36
		public float azimuthAngle { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00018D3F File Offset: 0x00016F3F
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00018D47 File Offset: 0x00016F47
		public float twist { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00018D50 File Offset: 0x00016F50
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00018D58 File Offset: 0x00016F58
		public Vector2 tilt { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00018D61 File Offset: 0x00016F61
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00018D69 File Offset: 0x00016F69
		public PenStatus penStatus { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00018D72 File Offset: 0x00016F72
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00018D7A File Offset: 0x00016F7A
		public Vector2 radius { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00018D83 File Offset: 0x00016F83
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00018D8B File Offset: 0x00016F8B
		public Vector2 radiusVariance { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00018D94 File Offset: 0x00016F94
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00018D9C File Offset: 0x00016F9C
		public bool fullyExited { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00018DA5 File Offset: 0x00016FA5
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00018DAD File Offset: 0x00016FAD
		public bool reentered { get; set; }

		// Token: 0x06000636 RID: 1590 RVA: 0x00018DB8 File Offset: 0x00016FB8
		public PointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.eligibleForClick = false;
			this.displayIndex = 0;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.tilt = new Vector2(0f, 0f);
			this.penStatus = PenStatus.None;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00018EA8 File Offset: 0x000170A8
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00018ECC File Offset: 0x000170CC
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00018EEE File Offset: 0x000170EE
		public Camera enterEventCamera
		{
			get
			{
				if (!(this.pointerCurrentRaycast.module == null))
				{
					return this.pointerCurrentRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00018F15 File Offset: 0x00017115
		public Camera pressEventCamera
		{
			get
			{
				if (!(this.pointerPressRaycast.module == null))
				{
					return this.pointerPressRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00018F3C File Offset: 0x0001713C
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00018F44 File Offset: 0x00017144
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00018F68 File Offset: 0x00017168
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position.ToString());
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta.ToString());
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick.ToString());
			string text = "<b>pointerEnter</b>: ";
			GameObject pointerEnter = this.pointerEnter;
			stringBuilder.AppendLine(text + ((pointerEnter != null) ? pointerEnter.ToString() : null));
			string text2 = "<b>pointerPress</b>: ";
			GameObject pointerPress = this.pointerPress;
			stringBuilder.AppendLine(text2 + ((pointerPress != null) ? pointerPress.ToString() : null));
			string text3 = "<b>lastPointerPress</b>: ";
			GameObject lastPress = this.lastPress;
			stringBuilder.AppendLine(text3 + ((lastPress != null) ? lastPress.ToString() : null));
			string text4 = "<b>pointerDrag</b>: ";
			GameObject pointerDrag = this.pointerDrag;
			stringBuilder.AppendLine(text4 + ((pointerDrag != null) ? pointerDrag.ToString() : null));
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold.ToString());
			stringBuilder.AppendLine("<b>Current Raycast:</b>");
			stringBuilder.AppendLine(this.pointerCurrentRaycast.ToString());
			stringBuilder.AppendLine("<b>Press Raycast:</b>");
			stringBuilder.AppendLine(this.pointerPressRaycast.ToString());
			stringBuilder.AppendLine("<b>Display Index:</b>");
			stringBuilder.AppendLine(this.displayIndex.ToString());
			stringBuilder.AppendLine("<b>pressure</b>: " + this.pressure.ToString());
			stringBuilder.AppendLine("<b>tangentialPressure</b>: " + this.tangentialPressure.ToString());
			stringBuilder.AppendLine("<b>altitudeAngle</b>: " + this.altitudeAngle.ToString());
			stringBuilder.AppendLine("<b>azimuthAngle</b>: " + this.azimuthAngle.ToString());
			stringBuilder.AppendLine("<b>twist</b>: " + this.twist.ToString());
			stringBuilder.AppendLine("<b>tilt</b>: " + this.tilt.ToString());
			stringBuilder.AppendLine("<b>penStatus</b>: " + this.penStatus.ToString());
			stringBuilder.AppendLine("<b>radius</b>: " + this.radius.ToString());
			stringBuilder.AppendLine("<b>radiusVariance</b>: " + this.radiusVariance.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040002AF RID: 687
		private GameObject m_PointerPress;

		// Token: 0x040002B6 RID: 694
		public List<GameObject> hovered = new List<GameObject>();

		// Token: 0x0200009A RID: 154
		public enum InputButton
		{
			// Token: 0x040002D1 RID: 721
			Left,
			// Token: 0x040002D2 RID: 722
			Right,
			// Token: 0x040002D3 RID: 723
			Middle
		}

		// Token: 0x0200009B RID: 155
		public enum FramePressState
		{
			// Token: 0x040002D5 RID: 725
			Pressed,
			// Token: 0x040002D6 RID: 726
			Released,
			// Token: 0x040002D7 RID: 727
			PressedAndReleased,
			// Token: 0x040002D8 RID: 728
			NotChanged
		}
	}
}
