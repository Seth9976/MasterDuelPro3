using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200008E RID: 142
	[InputControlLayout(stateType = typeof(KeyboardState), isGenericTypeOfDevice = true)]
	public class Keyboard : InputDevice, ITextInputReceiver
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060006B1 RID: 1713 RVA: 0x0001AB76 File Offset: 0x00018D76
		// (remove) Token: 0x060006B2 RID: 1714 RVA: 0x0001ABA1 File Offset: 0x00018DA1
		public event Action<char> onTextInput
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.m_TextInputListeners.Contains(value))
				{
					this.m_TextInputListeners.Append(value);
				}
			}
			remove
			{
				this.m_TextInputListeners.Remove(value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060006B3 RID: 1715 RVA: 0x0001ABAF File Offset: 0x00018DAF
		// (remove) Token: 0x060006B4 RID: 1716 RVA: 0x0001ABDA File Offset: 0x00018DDA
		public event Action<IMECompositionString> onIMECompositionChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.m_ImeCompositionListeners.Contains(value))
				{
					this.m_ImeCompositionListeners.Append(value);
				}
			}
			remove
			{
				this.m_ImeCompositionListeners.Remove(value);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		public void SetIMEEnabled(bool enabled)
		{
			EnableIMECompositionCommand command = EnableIMECompositionCommand.Create(enabled);
			base.ExecuteCommand<EnableIMECompositionCommand>(ref command);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001AC08 File Offset: 0x00018E08
		public void SetIMECursorPosition(Vector2 position)
		{
			SetIMECursorPositionCommand command = SetIMECursorPositionCommand.Create(position);
			base.ExecuteCommand<SetIMECursorPositionCommand>(ref command);
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001AC25 File Offset: 0x00018E25
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001AC33 File Offset: 0x00018E33
		public string keyboardLayout
		{
			get
			{
				base.RefreshConfigurationIfNeeded();
				return this.m_KeyboardLayoutName;
			}
			protected set
			{
				this.m_KeyboardLayoutName = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001AC3C File Offset: 0x00018E3C
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001AC44 File Offset: 0x00018E44
		public AnyKeyControl anyKey { get; protected set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001AC4D File Offset: 0x00018E4D
		public KeyControl spaceKey
		{
			get
			{
				return this[Key.Space];
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001AC56 File Offset: 0x00018E56
		public KeyControl enterKey
		{
			get
			{
				return this[Key.Enter];
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001AC5F File Offset: 0x00018E5F
		public KeyControl tabKey
		{
			get
			{
				return this[Key.Tab];
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001AC68 File Offset: 0x00018E68
		public KeyControl backquoteKey
		{
			get
			{
				return this[Key.Backquote];
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001AC71 File Offset: 0x00018E71
		public KeyControl quoteKey
		{
			get
			{
				return this[Key.Quote];
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001AC7A File Offset: 0x00018E7A
		public KeyControl semicolonKey
		{
			get
			{
				return this[Key.Semicolon];
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001AC83 File Offset: 0x00018E83
		public KeyControl commaKey
		{
			get
			{
				return this[Key.Comma];
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001AC8C File Offset: 0x00018E8C
		public KeyControl periodKey
		{
			get
			{
				return this[Key.Period];
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001AC95 File Offset: 0x00018E95
		public KeyControl slashKey
		{
			get
			{
				return this[Key.Slash];
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001AC9F File Offset: 0x00018E9F
		public KeyControl backslashKey
		{
			get
			{
				return this[Key.Backslash];
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001ACA9 File Offset: 0x00018EA9
		public KeyControl leftBracketKey
		{
			get
			{
				return this[Key.LeftBracket];
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001ACB3 File Offset: 0x00018EB3
		public KeyControl rightBracketKey
		{
			get
			{
				return this[Key.RightBracket];
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001ACBD File Offset: 0x00018EBD
		public KeyControl minusKey
		{
			get
			{
				return this[Key.Minus];
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001ACC7 File Offset: 0x00018EC7
		public KeyControl equalsKey
		{
			get
			{
				return this[Key.Equals];
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001ACD1 File Offset: 0x00018ED1
		public KeyControl aKey
		{
			get
			{
				return this[Key.A];
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001ACDB File Offset: 0x00018EDB
		public KeyControl bKey
		{
			get
			{
				return this[Key.B];
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001ACE5 File Offset: 0x00018EE5
		public KeyControl cKey
		{
			get
			{
				return this[Key.C];
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001ACEF File Offset: 0x00018EEF
		public KeyControl dKey
		{
			get
			{
				return this[Key.D];
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001ACF9 File Offset: 0x00018EF9
		public KeyControl eKey
		{
			get
			{
				return this[Key.E];
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001AD03 File Offset: 0x00018F03
		public KeyControl fKey
		{
			get
			{
				return this[Key.F];
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001AD0D File Offset: 0x00018F0D
		public KeyControl gKey
		{
			get
			{
				return this[Key.G];
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001AD17 File Offset: 0x00018F17
		public KeyControl hKey
		{
			get
			{
				return this[Key.H];
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001AD21 File Offset: 0x00018F21
		public KeyControl iKey
		{
			get
			{
				return this[Key.I];
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001AD2B File Offset: 0x00018F2B
		public KeyControl jKey
		{
			get
			{
				return this[Key.J];
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001AD35 File Offset: 0x00018F35
		public KeyControl kKey
		{
			get
			{
				return this[Key.K];
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001AD3F File Offset: 0x00018F3F
		public KeyControl lKey
		{
			get
			{
				return this[Key.L];
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001AD49 File Offset: 0x00018F49
		public KeyControl mKey
		{
			get
			{
				return this[Key.M];
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001AD53 File Offset: 0x00018F53
		public KeyControl nKey
		{
			get
			{
				return this[Key.N];
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001AD5D File Offset: 0x00018F5D
		public KeyControl oKey
		{
			get
			{
				return this[Key.O];
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001AD67 File Offset: 0x00018F67
		public KeyControl pKey
		{
			get
			{
				return this[Key.P];
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001AD71 File Offset: 0x00018F71
		public KeyControl qKey
		{
			get
			{
				return this[Key.Q];
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001AD7B File Offset: 0x00018F7B
		public KeyControl rKey
		{
			get
			{
				return this[Key.R];
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001AD85 File Offset: 0x00018F85
		public KeyControl sKey
		{
			get
			{
				return this[Key.S];
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001AD8F File Offset: 0x00018F8F
		public KeyControl tKey
		{
			get
			{
				return this[Key.T];
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001AD99 File Offset: 0x00018F99
		public KeyControl uKey
		{
			get
			{
				return this[Key.U];
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001ADA3 File Offset: 0x00018FA3
		public KeyControl vKey
		{
			get
			{
				return this[Key.V];
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001ADAD File Offset: 0x00018FAD
		public KeyControl wKey
		{
			get
			{
				return this[Key.W];
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001ADB7 File Offset: 0x00018FB7
		public KeyControl xKey
		{
			get
			{
				return this[Key.X];
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001ADC1 File Offset: 0x00018FC1
		public KeyControl yKey
		{
			get
			{
				return this[Key.Y];
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001ADCB File Offset: 0x00018FCB
		public KeyControl zKey
		{
			get
			{
				return this[Key.Z];
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001ADD5 File Offset: 0x00018FD5
		public KeyControl digit1Key
		{
			get
			{
				return this[Key.Digit1];
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001ADDF File Offset: 0x00018FDF
		public KeyControl digit2Key
		{
			get
			{
				return this[Key.Digit2];
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001ADE9 File Offset: 0x00018FE9
		public KeyControl digit3Key
		{
			get
			{
				return this[Key.Digit3];
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001ADF3 File Offset: 0x00018FF3
		public KeyControl digit4Key
		{
			get
			{
				return this[Key.Digit4];
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001ADFD File Offset: 0x00018FFD
		public KeyControl digit5Key
		{
			get
			{
				return this[Key.Digit5];
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001AE07 File Offset: 0x00019007
		public KeyControl digit6Key
		{
			get
			{
				return this[Key.Digit6];
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001AE11 File Offset: 0x00019011
		public KeyControl digit7Key
		{
			get
			{
				return this[Key.Digit7];
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001AE1B File Offset: 0x0001901B
		public KeyControl digit8Key
		{
			get
			{
				return this[Key.Digit8];
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001AE25 File Offset: 0x00019025
		public KeyControl digit9Key
		{
			get
			{
				return this[Key.Digit9];
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001AE2F File Offset: 0x0001902F
		public KeyControl digit0Key
		{
			get
			{
				return this[Key.Digit0];
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001AE39 File Offset: 0x00019039
		public KeyControl leftShiftKey
		{
			get
			{
				return this[Key.LeftShift];
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001AE43 File Offset: 0x00019043
		public KeyControl rightShiftKey
		{
			get
			{
				return this[Key.RightShift];
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001AE4D File Offset: 0x0001904D
		public KeyControl leftAltKey
		{
			get
			{
				return this[Key.LeftAlt];
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001AE57 File Offset: 0x00019057
		public KeyControl rightAltKey
		{
			get
			{
				return this[Key.RightAlt];
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001AE61 File Offset: 0x00019061
		public KeyControl leftCtrlKey
		{
			get
			{
				return this[Key.LeftCtrl];
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001AE6B File Offset: 0x0001906B
		public KeyControl rightCtrlKey
		{
			get
			{
				return this[Key.RightCtrl];
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001AE75 File Offset: 0x00019075
		public KeyControl leftMetaKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001AE7F File Offset: 0x0001907F
		public KeyControl rightMetaKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001AE75 File Offset: 0x00019075
		public KeyControl leftWindowsKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001AE7F File Offset: 0x0001907F
		public KeyControl rightWindowsKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001AE75 File Offset: 0x00019075
		public KeyControl leftAppleKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001AE7F File Offset: 0x0001907F
		public KeyControl rightAppleKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001AE75 File Offset: 0x00019075
		public KeyControl leftCommandKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001AE7F File Offset: 0x0001907F
		public KeyControl rightCommandKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001AE89 File Offset: 0x00019089
		public KeyControl contextMenuKey
		{
			get
			{
				return this[Key.ContextMenu];
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001AE93 File Offset: 0x00019093
		public KeyControl escapeKey
		{
			get
			{
				return this[Key.Escape];
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001AE9D File Offset: 0x0001909D
		public KeyControl leftArrowKey
		{
			get
			{
				return this[Key.LeftArrow];
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001AEA7 File Offset: 0x000190A7
		public KeyControl rightArrowKey
		{
			get
			{
				return this[Key.RightArrow];
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001AEB1 File Offset: 0x000190B1
		public KeyControl upArrowKey
		{
			get
			{
				return this[Key.UpArrow];
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001AEBB File Offset: 0x000190BB
		public KeyControl downArrowKey
		{
			get
			{
				return this[Key.DownArrow];
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001AEC5 File Offset: 0x000190C5
		public KeyControl backspaceKey
		{
			get
			{
				return this[Key.Backspace];
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001AECF File Offset: 0x000190CF
		public KeyControl pageDownKey
		{
			get
			{
				return this[Key.PageDown];
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001AED9 File Offset: 0x000190D9
		public KeyControl pageUpKey
		{
			get
			{
				return this[Key.PageUp];
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001AEE3 File Offset: 0x000190E3
		public KeyControl homeKey
		{
			get
			{
				return this[Key.Home];
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001AEED File Offset: 0x000190ED
		public KeyControl endKey
		{
			get
			{
				return this[Key.End];
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001AEF7 File Offset: 0x000190F7
		public KeyControl insertKey
		{
			get
			{
				return this[Key.Insert];
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001AF01 File Offset: 0x00019101
		public KeyControl deleteKey
		{
			get
			{
				return this[Key.Delete];
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001AF0B File Offset: 0x0001910B
		public KeyControl capsLockKey
		{
			get
			{
				return this[Key.CapsLock];
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001AF15 File Offset: 0x00019115
		public KeyControl scrollLockKey
		{
			get
			{
				return this[Key.ScrollLock];
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001AF1F File Offset: 0x0001911F
		public KeyControl numLockKey
		{
			get
			{
				return this[Key.NumLock];
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001AF29 File Offset: 0x00019129
		public KeyControl printScreenKey
		{
			get
			{
				return this[Key.PrintScreen];
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001AF33 File Offset: 0x00019133
		public KeyControl pauseKey
		{
			get
			{
				return this[Key.Pause];
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001AF3D File Offset: 0x0001913D
		public KeyControl numpadEnterKey
		{
			get
			{
				return this[Key.NumpadEnter];
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001AF47 File Offset: 0x00019147
		public KeyControl numpadDivideKey
		{
			get
			{
				return this[Key.NumpadDivide];
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001AF51 File Offset: 0x00019151
		public KeyControl numpadMultiplyKey
		{
			get
			{
				return this[Key.NumpadMultiply];
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001AF5B File Offset: 0x0001915B
		public KeyControl numpadMinusKey
		{
			get
			{
				return this[Key.NumpadMinus];
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001AF65 File Offset: 0x00019165
		public KeyControl numpadPlusKey
		{
			get
			{
				return this[Key.NumpadPlus];
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001AF6F File Offset: 0x0001916F
		public KeyControl numpadPeriodKey
		{
			get
			{
				return this[Key.NumpadPeriod];
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001AF79 File Offset: 0x00019179
		public KeyControl numpadEqualsKey
		{
			get
			{
				return this[Key.NumpadEquals];
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001AF83 File Offset: 0x00019183
		public KeyControl numpad0Key
		{
			get
			{
				return this[Key.Numpad0];
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001AF8D File Offset: 0x0001918D
		public KeyControl numpad1Key
		{
			get
			{
				return this[Key.Numpad1];
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001AF97 File Offset: 0x00019197
		public KeyControl numpad2Key
		{
			get
			{
				return this[Key.Numpad2];
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001AFA1 File Offset: 0x000191A1
		public KeyControl numpad3Key
		{
			get
			{
				return this[Key.Numpad3];
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001AFAB File Offset: 0x000191AB
		public KeyControl numpad4Key
		{
			get
			{
				return this[Key.Numpad4];
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001AFB5 File Offset: 0x000191B5
		public KeyControl numpad5Key
		{
			get
			{
				return this[Key.Numpad5];
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001AFBF File Offset: 0x000191BF
		public KeyControl numpad6Key
		{
			get
			{
				return this[Key.Numpad6];
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001AFC9 File Offset: 0x000191C9
		public KeyControl numpad7Key
		{
			get
			{
				return this[Key.Numpad7];
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001AFD3 File Offset: 0x000191D3
		public KeyControl numpad8Key
		{
			get
			{
				return this[Key.Numpad8];
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001AFDD File Offset: 0x000191DD
		public KeyControl numpad9Key
		{
			get
			{
				return this[Key.Numpad9];
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001AFE7 File Offset: 0x000191E7
		public KeyControl f1Key
		{
			get
			{
				return this[Key.F1];
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001AFF1 File Offset: 0x000191F1
		public KeyControl f2Key
		{
			get
			{
				return this[Key.F2];
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001AFFB File Offset: 0x000191FB
		public KeyControl f3Key
		{
			get
			{
				return this[Key.F3];
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001B005 File Offset: 0x00019205
		public KeyControl f4Key
		{
			get
			{
				return this[Key.F4];
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001B00F File Offset: 0x0001920F
		public KeyControl f5Key
		{
			get
			{
				return this[Key.F5];
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001B019 File Offset: 0x00019219
		public KeyControl f6Key
		{
			get
			{
				return this[Key.F6];
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001B023 File Offset: 0x00019223
		public KeyControl f7Key
		{
			get
			{
				return this[Key.F7];
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001B02D File Offset: 0x0001922D
		public KeyControl f8Key
		{
			get
			{
				return this[Key.F8];
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001B037 File Offset: 0x00019237
		public KeyControl f9Key
		{
			get
			{
				return this[Key.F9];
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001B041 File Offset: 0x00019241
		public KeyControl f10Key
		{
			get
			{
				return this[Key.F10];
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001B04B File Offset: 0x0001924B
		public KeyControl f11Key
		{
			get
			{
				return this[Key.F11];
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001B055 File Offset: 0x00019255
		public KeyControl f12Key
		{
			get
			{
				return this[Key.F12];
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001B05F File Offset: 0x0001925F
		public KeyControl oem1Key
		{
			get
			{
				return this[Key.OEM1];
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001B069 File Offset: 0x00019269
		public KeyControl oem2Key
		{
			get
			{
				return this[Key.OEM2];
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001B073 File Offset: 0x00019273
		public KeyControl oem3Key
		{
			get
			{
				return this[Key.OEM3];
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001B07D File Offset: 0x0001927D
		public KeyControl oem4Key
		{
			get
			{
				return this[Key.OEM4];
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001B087 File Offset: 0x00019287
		public KeyControl oem5Key
		{
			get
			{
				return this[Key.OEM5];
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001B091 File Offset: 0x00019291
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001B099 File Offset: 0x00019299
		public ButtonControl shiftKey { get; protected set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001B0A2 File Offset: 0x000192A2
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001B0AA File Offset: 0x000192AA
		public ButtonControl ctrlKey { get; protected set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001B0B3 File Offset: 0x000192B3
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001B0BB File Offset: 0x000192BB
		public ButtonControl altKey { get; protected set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001B0C4 File Offset: 0x000192C4
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001B0CC File Offset: 0x000192CC
		public ButtonControl imeSelected { get; protected set; }

		// Token: 0x1700025D RID: 605
		public KeyControl this[Key key]
		{
			get
			{
				int index = key - Key.Space;
				if (index < 0 || index >= this.m_Keys.Length)
				{
					throw new ArgumentOutOfRangeException("key");
				}
				return this.m_Keys[index];
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001B10B File Offset: 0x0001930B
		public ReadOnlyArray<KeyControl> allKeys
		{
			get
			{
				return new ReadOnlyArray<KeyControl>(this.m_Keys);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001B118 File Offset: 0x00019318
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0001B11F File Offset: 0x0001931F
		public static Keyboard current { get; private set; }

		// Token: 0x0600073B RID: 1851 RVA: 0x0001B127 File Offset: 0x00019327
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Keyboard.current = this;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001B135 File Offset: 0x00019335
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Keyboard.current == this)
			{
				Keyboard.current = null;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001B14C File Offset: 0x0001934C
		protected override void FinishSetup()
		{
			string[] keyStrings = new string[]
			{
				"space", "enter", "tab", "backquote", "quote", "semicolon", "comma", "period", "slash", "backslash",
				"leftbracket", "rightbracket", "minus", "equals", "a", "b", "c", "d", "e", "f",
				"g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
				"q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
				"1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
				"leftshift", "rightshift", "leftalt", "rightalt", "leftctrl", "rightctrl", "leftmeta", "rightmeta", "contextmenu", "escape",
				"leftarrow", "rightarrow", "uparrow", "downarrow", "backspace", "pagedown", "pageup", "home", "end", "insert",
				"delete", "capslock", "numlock", "printscreen", "scrolllock", "pause", "numpadenter", "numpaddivide", "numpadmultiply", "numpadplus",
				"numpadminus", "numpadperiod", "numpadequals", "numpad0", "numpad1", "numpad2", "numpad3", "numpad4", "numpad5", "numpad6",
				"numpad7", "numpad8", "numpad9", "f1", "f2", "f3", "f4", "f5", "f6", "f7",
				"f8", "f9", "f10", "f11", "f12", "oem1", "oem2", "oem3", "oem4", "oem5"
			};
			this.m_Keys = new KeyControl[keyStrings.Length];
			for (int i = 0; i < keyStrings.Length; i++)
			{
				this.m_Keys[i] = base.GetChildControl<KeyControl>(keyStrings[i]);
				this.m_Keys[i].keyCode = i + Key.Space;
			}
			this.anyKey = base.GetChildControl<AnyKeyControl>("anyKey");
			this.shiftKey = base.GetChildControl<ButtonControl>("shift");
			this.ctrlKey = base.GetChildControl<ButtonControl>("ctrl");
			this.altKey = base.GetChildControl<ButtonControl>("alt");
			this.imeSelected = base.GetChildControl<ButtonControl>("IMESelected");
			base.FinishSetup();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001B5D0 File Offset: 0x000197D0
		protected override void RefreshConfiguration()
		{
			this.keyboardLayout = null;
			QueryKeyboardLayoutCommand command = QueryKeyboardLayoutCommand.Create();
			if (base.ExecuteCommand<QueryKeyboardLayoutCommand>(ref command) >= 0L)
			{
				this.keyboardLayout = command.ReadLayoutName();
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001B604 File Offset: 0x00019804
		public void OnTextInput(char character)
		{
			for (int i = 0; i < this.m_TextInputListeners.length; i++)
			{
				this.m_TextInputListeners[i](character);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001B63C File Offset: 0x0001983C
		public KeyControl FindKeyOnCurrentKeyboardLayout(string displayName)
		{
			ReadOnlyArray<KeyControl> keys = this.allKeys;
			for (int i = 0; i < keys.Count; i++)
			{
				if (string.Equals(keys[i].displayName, displayName, StringComparison.CurrentCultureIgnoreCase))
				{
					return keys[i];
				}
			}
			return null;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001B684 File Offset: 0x00019884
		public void OnIMECompositionChanged(IMECompositionString compositionString)
		{
			if (this.m_ImeCompositionListeners.length > 0)
			{
				for (int i = 0; i < this.m_ImeCompositionListeners.length; i++)
				{
					this.m_ImeCompositionListeners[i](compositionString);
				}
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001B6C7 File Offset: 0x000198C7
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001B6CF File Offset: 0x000198CF
		protected KeyControl[] keys
		{
			get
			{
				return this.m_Keys;
			}
			set
			{
				this.m_Keys = value;
			}
		}

		// Token: 0x040003B3 RID: 947
		public const int KeyCount = 110;

		// Token: 0x040003BA RID: 954
		private InlinedArray<Action<char>> m_TextInputListeners;

		// Token: 0x040003BB RID: 955
		private string m_KeyboardLayoutName;

		// Token: 0x040003BC RID: 956
		private KeyControl[] m_Keys;

		// Token: 0x040003BD RID: 957
		private InlinedArray<Action<IMECompositionString>> m_ImeCompositionListeners;
	}
}
