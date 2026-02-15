using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Identifies a control or user interface (UI) element that is drawn with visual styles.</summary>
	// Token: 0x0200030C RID: 780
	public class VisualStyleElement
	{
		// Token: 0x06001BEA RID: 7146 RVA: 0x0008646F File Offset: 0x0008466F
		internal VisualStyleElement(string className, int part, int state)
		{
			this.class_name = className;
			this.part = part;
			this.state = state;
		}

		/// <summary>Gets the class name of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A string that represents the class name of a visual style element.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0008648C File Offset: 0x0008468C
		public string ClassName
		{
			get
			{
				return this.class_name;
			}
		}

		/// <summary>Gets a value indicating the part of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A value that represents the part of a visual style element.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00086494 File Offset: 0x00084694
		public int Part
		{
			get
			{
				return this.part;
			}
		}

		/// <summary>Gets a value indicating the state of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A value that represents the state of a visual style element.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0008649C File Offset: 0x0008469C
		public int State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Creates a new visual style element from the specified class, part, and state values.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> with the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" />, <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.Part" />, and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.State" /> properties initialized to the <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> parameters.</returns>
		/// <param name="className">A string that represents the class name of the visual style element to be created.</param>
		/// <param name="part">A value that represents the part of the visual style element to be created.</param>
		/// <param name="state">A value that represents the state of the visual style element to be created.</param>
		// Token: 0x06001BEE RID: 7150 RVA: 0x000864A4 File Offset: 0x000846A4
		public static VisualStyleElement CreateElement(string className, int part, int state)
		{
			return new VisualStyleElement(className, part, state);
		}

		// Token: 0x0400174E RID: 5966
		private string class_name;

		// Token: 0x0400174F RID: 5967
		private int part;

		// Token: 0x04001750 RID: 5968
		private int state;

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for button-related controls. This class cannot be inherited. </summary>
		// Token: 0x0200030D RID: 781
		public static class Button
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the check box control. This class cannot be inherited. </summary>
			// Token: 0x0200030E RID: 782
			public static class CheckBox
			{
				/// <summary>Gets a visual style element that represents a disabled check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the checked state.</returns>
				// Token: 0x17000672 RID: 1650
				// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000864AE File Offset: 0x000846AE
				public static VisualStyleElement CheckedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 8);
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the checked state.</returns>
				// Token: 0x17000673 RID: 1651
				// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000864BC File Offset: 0x000846BC
				public static VisualStyleElement CheckedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the checked state.</returns>
				// Token: 0x17000674 RID: 1652
				// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x000864CA File Offset: 0x000846CA
				public static VisualStyleElement CheckedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the checked state.</returns>
				// Token: 0x17000675 RID: 1653
				// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000864D8 File Offset: 0x000846D8
				public static VisualStyleElement CheckedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 7);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the indeterminate state.</returns>
				// Token: 0x17000676 RID: 1654
				// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000864E6 File Offset: 0x000846E6
				public static VisualStyleElement MixedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 12);
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the indeterminate state.</returns>
				// Token: 0x17000677 RID: 1655
				// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000864F5 File Offset: 0x000846F5
				public static VisualStyleElement MixedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 10);
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the indeterminate state.</returns>
				// Token: 0x17000678 RID: 1656
				// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00086504 File Offset: 0x00084704
				public static VisualStyleElement MixedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 9);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the indeterminate state.</returns>
				// Token: 0x17000679 RID: 1657
				// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00086513 File Offset: 0x00084713
				public static VisualStyleElement MixedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 11);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the unchecked state.</returns>
				// Token: 0x1700067A RID: 1658
				// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00086522 File Offset: 0x00084722
				public static VisualStyleElement UncheckedDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the unchecked state.</returns>
				// Token: 0x1700067B RID: 1659
				// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x00086530 File Offset: 0x00084730
				public static VisualStyleElement UncheckedHot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the unchecked state.</returns>
				// Token: 0x1700067C RID: 1660
				// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x0008653E File Offset: 0x0008473E
				public static VisualStyleElement UncheckedNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the unchecked state. </returns>
				// Token: 0x1700067D RID: 1661
				// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0008654C File Offset: 0x0008474C
				public static VisualStyleElement UncheckedPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the group box control. This class cannot be inherited. </summary>
			// Token: 0x0200030F RID: 783
			public static class GroupBox
			{
				/// <summary>Gets a visual style element that represents a disabled group box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled group box.</returns>
				// Token: 0x1700067E RID: 1662
				// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0008655A File Offset: 0x0008475A
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal group box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal group box.</returns>
				// Token: 0x1700067F RID: 1663
				// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00086568 File Offset: 0x00084768
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 4, 1);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the button control. This class cannot be inherited. </summary>
			// Token: 0x02000310 RID: 784
			public static class PushButton
			{
				/// <summary>Gets a visual style element that represents a default button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a default button.</returns>
				// Token: 0x17000680 RID: 1664
				// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00086576 File Offset: 0x00084776
				public static VisualStyleElement Default
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a disabled button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled button.</returns>
				// Token: 0x17000681 RID: 1665
				// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00086584 File Offset: 0x00084784
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a hot button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot button. </returns>
				// Token: 0x17000682 RID: 1666
				// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00086592 File Offset: 0x00084792
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal button.</returns>
				// Token: 0x17000683 RID: 1667
				// (get) Token: 0x06001C00 RID: 7168 RVA: 0x000865A0 File Offset: 0x000847A0
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed button.</returns>
				// Token: 0x17000684 RID: 1668
				// (get) Token: 0x06001C01 RID: 7169 RVA: 0x000865AE File Offset: 0x000847AE
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("BUTTON", 1, 3);
					}
				}
			}
		}

		/// <summary>Contains a class that provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the drop-down arrow of the combo box control. This class cannot be inherited.</summary>
		// Token: 0x02000311 RID: 785
		public static class ComboBox
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the drop-down arrow of the combo box control. This class cannot be inherited. </summary>
			// Token: 0x02000312 RID: 786
			public static class DropDownButton
			{
				/// <summary>Gets a visual style element that represents a drop-down arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the disabled state.</returns>
				// Token: 0x17000685 RID: 1669
				// (get) Token: 0x06001C02 RID: 7170 RVA: 0x000865BC File Offset: 0x000847BC
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the hot state.</returns>
				// Token: 0x17000686 RID: 1670
				// (get) Token: 0x06001C03 RID: 7171 RVA: 0x000865CA File Offset: 0x000847CA
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the normal state. </returns>
				// Token: 0x17000687 RID: 1671
				// (get) Token: 0x06001C04 RID: 7172 RVA: 0x000865D8 File Offset: 0x000847D8
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the pressed state.</returns>
				// Token: 0x17000688 RID: 1672
				// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000865E6 File Offset: 0x000847E6
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("COMBOBOX", 1, 3);
					}
				}
			}

			// Token: 0x02000313 RID: 787
			internal static class Border
			{
				// Token: 0x17000689 RID: 1673
				// (get) Token: 0x06001C06 RID: 7174 RVA: 0x000865F4 File Offset: 0x000847F4
				public static VisualStyleElement Normal
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 1);
					}
				}

				// Token: 0x1700068A RID: 1674
				// (get) Token: 0x06001C07 RID: 7175 RVA: 0x00086602 File Offset: 0x00084802
				public static VisualStyleElement Hot
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 2);
					}
				}

				// Token: 0x1700068B RID: 1675
				// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00086610 File Offset: 0x00084810
				public static VisualStyleElement Focused
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 3);
					}
				}

				// Token: 0x1700068C RID: 1676
				// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0008661E File Offset: 0x0008481E
				public static VisualStyleElement Disabled
				{
					get
					{
						return new VisualStyleElement("COMBOBOX", 4, 4);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each part of the header control. This class cannot be inherited.</summary>
		// Token: 0x02000314 RID: 788
		public static class Header
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of an item of the header control. This class cannot be inherited. </summary>
			// Token: 0x02000315 RID: 789
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a hot header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot header item.</returns>
				// Token: 0x1700068D RID: 1677
				// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0008662C File Offset: 0x0008482C
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal header item.</returns>
				// Token: 0x1700068E RID: 1678
				// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0008663A File Offset: 0x0008483A
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a pressed header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed header item. </returns>
				// Token: 0x1700068F RID: 1679
				// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00086648 File Offset: 0x00084848
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("HEADER", 1, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the scroll bar control. This class cannot be inherited.</summary>
		// Token: 0x02000316 RID: 790
		public static class ScrollBar
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state and direction of a scroll arrow. This class cannot be inherited. </summary>
			// Token: 0x02000317 RID: 791
			public static class ArrowButton
			{
				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17000690 RID: 1680
				// (get) Token: 0x06001C0D RID: 7181 RVA: 0x00086656 File Offset: 0x00084856
				public static VisualStyleElement DownDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 8);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17000691 RID: 1681
				// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00086664 File Offset: 0x00084864
				public static VisualStyleElement DownHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17000692 RID: 1682
				// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00086672 File Offset: 0x00084872
				public static VisualStyleElement DownNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x17000693 RID: 1683
				// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00086680 File Offset: 0x00084880
				public static VisualStyleElement DownPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 7);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17000694 RID: 1684
				// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0008668E File Offset: 0x0008488E
				public static VisualStyleElement LeftDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 12);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17000695 RID: 1685
				// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0008669D File Offset: 0x0008489D
				public static VisualStyleElement LeftHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 10);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17000696 RID: 1686
				// (get) Token: 0x06001C13 RID: 7187 RVA: 0x000866AC File Offset: 0x000848AC
				public static VisualStyleElement LeftNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 9);
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x17000697 RID: 1687
				// (get) Token: 0x06001C14 RID: 7188 RVA: 0x000866BB File Offset: 0x000848BB
				public static VisualStyleElement LeftPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 11);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17000698 RID: 1688
				// (get) Token: 0x06001C15 RID: 7189 RVA: 0x000866CA File Offset: 0x000848CA
				public static VisualStyleElement RightDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 16);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17000699 RID: 1689
				// (get) Token: 0x06001C16 RID: 7190 RVA: 0x000866D9 File Offset: 0x000848D9
				public static VisualStyleElement RightHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 14);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the normal state.</returns>
				// Token: 0x1700069A RID: 1690
				// (get) Token: 0x06001C17 RID: 7191 RVA: 0x000866E8 File Offset: 0x000848E8
				public static VisualStyleElement RightNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 13);
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x1700069B RID: 1691
				// (get) Token: 0x06001C18 RID: 7192 RVA: 0x000866F7 File Offset: 0x000848F7
				public static VisualStyleElement RightPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 15);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x1700069C RID: 1692
				// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00086706 File Offset: 0x00084906
				public static VisualStyleElement UpDisabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the hot state.</returns>
				// Token: 0x1700069D RID: 1693
				// (get) Token: 0x06001C1A RID: 7194 RVA: 0x00086714 File Offset: 0x00084914
				public static VisualStyleElement UpHot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the normal state.</returns>
				// Token: 0x1700069E RID: 1694
				// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00086722 File Offset: 0x00084922
				public static VisualStyleElement UpNormal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the pressed state. </returns>
				// Token: 0x1700069F RID: 1695
				// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00086730 File Offset: 0x00084930
				public static VisualStyleElement UpPressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 1, 3);
					}
				}

				// Token: 0x170006A0 RID: 1696
				// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0008673E File Offset: 0x0008493E
				internal static VisualStyleElement DownHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 18);
					}
				}

				// Token: 0x170006A1 RID: 1697
				// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0008674D File Offset: 0x0008494D
				internal static VisualStyleElement LeftHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 19);
					}
				}

				// Token: 0x170006A2 RID: 1698
				// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0008675C File Offset: 0x0008495C
				internal static VisualStyleElement RightHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 20);
					}
				}

				// Token: 0x170006A3 RID: 1699
				// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0008676B File Offset: 0x0008496B
				internal static VisualStyleElement UpHover
				{
					get
					{
						return new VisualStyleElement("SCROLLBAR", 1, 17);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a horizontal scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x02000318 RID: 792
			public static class GripperHorizontal
			{
				/// <summary>Gets a visual style element that represents a grip for a horizontal scroll box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a horizontal scroll box. </returns>
				// Token: 0x170006A4 RID: 1700
				// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0008677A File Offset: 0x0008497A
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 8, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x02000319 RID: 793
			public static class GripperVertical
			{
				/// <summary>Gets a visual style element that represents a grip for a vertical scroll box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a vertical scroll box. </returns>
				// Token: 0x170006A5 RID: 1701
				// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00086788 File Offset: 0x00084988
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 9, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left part of a horizontal scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x0200031A RID: 794
			public static class LeftTrackHorizontal
			{
				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the disabled state.</returns>
				// Token: 0x170006A6 RID: 1702
				// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00086797 File Offset: 0x00084997
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 5, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the normal state.</returns>
				// Token: 0x170006A7 RID: 1703
				// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000867A5 File Offset: 0x000849A5
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the pressed state.</returns>
				// Token: 0x170006A8 RID: 1704
				// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000867B3 File Offset: 0x000849B3
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 5, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the lower part of a vertical scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x0200031B RID: 795
			public static class LowerTrackVertical
			{
				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the disabled state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the disabled state.</returns>
				// Token: 0x170006A9 RID: 1705
				// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000867C1 File Offset: 0x000849C1
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 6, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the normal state.</returns>
				// Token: 0x170006AA RID: 1706
				// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000867CF File Offset: 0x000849CF
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 6, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the pressed state. </returns>
				// Token: 0x170006AB RID: 1707
				// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000867DD File Offset: 0x000849DD
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 6, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right part of a horizontal scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x0200031C RID: 796
			public static class RightTrackHorizontal
			{
				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the disabled state.</returns>
				// Token: 0x170006AC RID: 1708
				// (get) Token: 0x06001C29 RID: 7209 RVA: 0x000867EB File Offset: 0x000849EB
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 4, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the normal state.</returns>
				// Token: 0x170006AD RID: 1709
				// (get) Token: 0x06001C2A RID: 7210 RVA: 0x000867F9 File Offset: 0x000849F9
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the pressed state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the pressed state.</returns>
				// Token: 0x170006AE RID: 1710
				// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00086807 File Offset: 0x00084A07
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 4, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a horizontal scroll box (also known as the thumb). This class cannot be inherited. </summary>
			// Token: 0x0200031D RID: 797
			public static class ThumbButtonHorizontal
			{
				/// <summary>Gets a visual style element that represents a horizontal scroll box in the disabled state. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the disabled state.</returns>
				// Token: 0x170006AF RID: 1711
				// (get) Token: 0x06001C2C RID: 7212 RVA: 0x00086815 File Offset: 0x00084A15
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the hot state.</returns>
				// Token: 0x170006B0 RID: 1712
				// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00086823 File Offset: 0x00084A23
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the normal state.</returns>
				// Token: 0x170006B1 RID: 1713
				// (get) Token: 0x06001C2E RID: 7214 RVA: 0x00086831 File Offset: 0x00084A31
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the pressed state.</returns>
				// Token: 0x170006B2 RID: 1714
				// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0008683F File Offset: 0x00084A3F
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 2, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x0200031E RID: 798
			public static class ThumbButtonVertical
			{
				/// <summary>Gets a visual style element that represents a vertical scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the disabled state.</returns>
				// Token: 0x170006B3 RID: 1715
				// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0008684D File Offset: 0x00084A4D
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the hot state.</returns>
				// Token: 0x170006B4 RID: 1716
				// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0008685B File Offset: 0x00084A5B
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the normal state.</returns>
				// Token: 0x170006B5 RID: 1717
				// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00086869 File Offset: 0x00084A69
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the pressed state. </returns>
				// Token: 0x170006B6 RID: 1718
				// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00086877 File Offset: 0x00084A77
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upper part of a vertical scroll bar track. This class cannot be inherited. </summary>
			// Token: 0x0200031F RID: 799
			public static class UpperTrackVertical
			{
				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the disabled state.</returns>
				// Token: 0x170006B7 RID: 1719
				// (get) Token: 0x06001C34 RID: 7220 RVA: 0x00086885 File Offset: 0x00084A85
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("SCROLLBAR", 7, 4);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a text box. This class cannot be inherited.</summary>
		// Token: 0x02000320 RID: 800
		public static class TextBox
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a text box. This class cannot be inherited. </summary>
			// Token: 0x02000321 RID: 801
			public static class TextEdit
			{
				/// <summary>Gets a visual style element that represents a disabled text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled text box.</returns>
				// Token: 0x170006B8 RID: 1720
				// (get) Token: 0x06001C35 RID: 7221 RVA: 0x00086893 File Offset: 0x00084A93
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a text box that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a text box that has focus.</returns>
				// Token: 0x170006B9 RID: 1721
				// (get) Token: 0x06001C36 RID: 7222 RVA: 0x000868A1 File Offset: 0x00084AA1
				public static VisualStyleElement Focused
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a hot text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot text box.</returns>
				// Token: 0x170006BA RID: 1722
				// (get) Token: 0x06001C37 RID: 7223 RVA: 0x000868AF File Offset: 0x00084AAF
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a normal text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal text box.</returns>
				// Token: 0x170006BB RID: 1723
				// (get) Token: 0x06001C38 RID: 7224 RVA: 0x000868BD File Offset: 0x00084ABD
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a read-only text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a read-only text box.</returns>
				// Token: 0x170006BC RID: 1724
				// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000868CB File Offset: 0x00084ACB
				public static VisualStyleElement ReadOnly
				{
					get
					{
						return VisualStyleElement.CreateElement("EDIT", 1, 6);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a toolbar. This class cannot be inherited.</summary>
		// Token: 0x02000322 RID: 802
		public static class ToolBar
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a toolbar button. This class cannot be inherited. </summary>
			// Token: 0x02000323 RID: 803
			public static class Button
			{
				/// <summary>Gets a visual style element that represents a toolbar button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the checked state.</returns>
				// Token: 0x170006BD RID: 1725
				// (get) Token: 0x06001C3A RID: 7226 RVA: 0x000868D9 File Offset: 0x00084AD9
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the disabled state.</returns>
				// Token: 0x170006BE RID: 1726
				// (get) Token: 0x06001C3B RID: 7227 RVA: 0x000868E7 File Offset: 0x00084AE7
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot state.</returns>
				// Token: 0x170006BF RID: 1727
				// (get) Token: 0x06001C3C RID: 7228 RVA: 0x000868F5 File Offset: 0x00084AF5
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot and checked states.</returns>
				// Token: 0x170006C0 RID: 1728
				// (get) Token: 0x06001C3D RID: 7229 RVA: 0x00086903 File Offset: 0x00084B03
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the normal state.</returns>
				// Token: 0x170006C1 RID: 1729
				// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00086911 File Offset: 0x00084B11
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the pressed state.</returns>
				// Token: 0x170006C2 RID: 1730
				// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0008691F File Offset: 0x00084B1F
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 1, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a drop-down toolbar button. This class cannot be inherited. </summary>
			// Token: 0x02000324 RID: 804
			public static class DropDownButton
			{
				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the checked state.</returns>
				// Token: 0x170006C3 RID: 1731
				// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0008692D File Offset: 0x00084B2D
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the disabled state.</returns>
				// Token: 0x170006C4 RID: 1732
				// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0008693B File Offset: 0x00084B3B
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot state.</returns>
				// Token: 0x170006C5 RID: 1733
				// (get) Token: 0x06001C42 RID: 7234 RVA: 0x00086949 File Offset: 0x00084B49
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot and checked states.</returns>
				// Token: 0x170006C6 RID: 1734
				// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00086957 File Offset: 0x00084B57
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the normal state.</returns>
				// Token: 0x170006C7 RID: 1735
				// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00086965 File Offset: 0x00084B65
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the pressed state.</returns>
				// Token: 0x170006C8 RID: 1736
				// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00086973 File Offset: 0x00084B73
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 2, 3);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a horizontal separator of the toolbar. This class cannot be inherited. </summary>
			// Token: 0x02000325 RID: 805
			public static class SeparatorHorizontal
			{
				/// <summary>Gets a visual style element that represents a horizontal separator of the toolbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal separator of the toolbar.</returns>
				// Token: 0x170006C9 RID: 1737
				// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00086981 File Offset: 0x00084B81
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 5, 0);
					}
				}
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a vertical separator of the toolbar. This class cannot be inherited. </summary>
			// Token: 0x02000326 RID: 806
			public static class SeparatorVertical
			{
				/// <summary>Gets a visual style element that represents a vertical separator of the toolbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical separator of the toolbar.</returns>
				// Token: 0x170006CA RID: 1738
				// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0008698F File Offset: 0x00084B8F
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 6, 0);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the regular button portion of a combined regular button and drop-down button. This class cannot be inherited.</summary>
			// Token: 0x02000327 RID: 807
			public static class SplitButton
			{
				/// <summary>Gets a visual style element that represents a split button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the checked state.</returns>
				// Token: 0x170006CB RID: 1739
				// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0008699D File Offset: 0x00084B9D
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the disabled state.</returns>
				// Token: 0x170006CC RID: 1740
				// (get) Token: 0x06001C49 RID: 7241 RVA: 0x000869AB File Offset: 0x00084BAB
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot state.</returns>
				// Token: 0x170006CD RID: 1741
				// (get) Token: 0x06001C4A RID: 7242 RVA: 0x000869B9 File Offset: 0x00084BB9
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot and checked states.</returns>
				// Token: 0x170006CE RID: 1742
				// (get) Token: 0x06001C4B RID: 7243 RVA: 0x000869C7 File Offset: 0x00084BC7
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the normal state.</returns>
				// Token: 0x170006CF RID: 1743
				// (get) Token: 0x06001C4C RID: 7244 RVA: 0x000869D5 File Offset: 0x00084BD5
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the pressed state. </returns>
				// Token: 0x170006D0 RID: 1744
				// (get) Token: 0x06001C4D RID: 7245 RVA: 0x000869E3 File Offset: 0x00084BE3
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 3, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the drop-down portion of a combined regular button and drop-down button. This class cannot be inherited. </summary>
			// Token: 0x02000328 RID: 808
			public static class SplitButtonDropDown
			{
				/// <summary>Gets a visual style element that represents a split drop-down button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the checked state.</returns>
				// Token: 0x170006D1 RID: 1745
				// (get) Token: 0x06001C4E RID: 7246 RVA: 0x000869F1 File Offset: 0x00084BF1
				public static VisualStyleElement Checked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 5);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the disabled state.</returns>
				// Token: 0x170006D2 RID: 1746
				// (get) Token: 0x06001C4F RID: 7247 RVA: 0x000869FF File Offset: 0x00084BFF
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot state.</returns>
				// Token: 0x170006D3 RID: 1747
				// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00086A0D File Offset: 0x00084C0D
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot and checked states.</returns>
				// Token: 0x170006D4 RID: 1748
				// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00086A1B File Offset: 0x00084C1B
				public static VisualStyleElement HotChecked
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 6);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the normal state.</returns>
				// Token: 0x170006D5 RID: 1749
				// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00086A29 File Offset: 0x00084C29
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the pressed state.</returns>
				// Token: 0x170006D6 RID: 1750
				// (get) Token: 0x06001C53 RID: 7251 RVA: 0x00086A37 File Offset: 0x00084C37
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLBAR", 4, 3);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a ToolTip. This class cannot be inherited.</summary>
		// Token: 0x02000329 RID: 809
		public static class ToolTip
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for a standard ToolTip. This class cannot be inherited. </summary>
			// Token: 0x0200032A RID: 810
			public static class Standard
			{
				/// <summary>Gets a visual style element that represents a standard ToolTip that contains text.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a standard ToolTip that contains text.</returns>
				// Token: 0x170006D7 RID: 1751
				// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00086A45 File Offset: 0x00084C45
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("TOOLTIP", 1, 1);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the tree view control. This class cannot be inherited.  </summary>
		// Token: 0x0200032B RID: 811
		public static class TreeView
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the plus sign (+) and minus sign (-) buttons of a tree view control. This class cannot be inherited. </summary>
			// Token: 0x0200032C RID: 812
			public static class Glyph
			{
				/// <summary>Gets a visual style element that represents a minus sign (-) button of a tree view node.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a minus sign button of a tree view node.</returns>
				// Token: 0x170006D8 RID: 1752
				// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00086A53 File Offset: 0x00084C53
				public static VisualStyleElement Closed
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a plus sign (+) button of a tree view node.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a plus sign button of a tree view node.</returns>
				// Token: 0x170006D9 RID: 1753
				// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00086A61 File Offset: 0x00084C61
				public static VisualStyleElement Opened
				{
					get
					{
						return VisualStyleElement.CreateElement("TREEVIEW", 2, 2);
					}
				}
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a window. This class cannot be inherited.</summary>
		// Token: 0x0200032D RID: 813
		public static class Window
		{
			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a window. This class cannot be inherited. </summary>
			// Token: 0x0200032E RID: 814
			public static class Caption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active window.</returns>
				// Token: 0x170006DA RID: 1754
				// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00086A6F File Offset: 0x00084C6F
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 1, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled window.</returns>
				// Token: 0x170006DB RID: 1755
				// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00086A7D File Offset: 0x00084C7D
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 1, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive window.</returns>
				// Token: 0x170006DC RID: 1756
				// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00086A8B File Offset: 0x00084C8B
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 1, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a window. This class cannot be inherited. </summary>
			// Token: 0x0200032F RID: 815
			public static class CloseButton
			{
				/// <summary>Gets a visual style element that represents a Close button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the disabled state.</returns>
				// Token: 0x170006DD RID: 1757
				// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00086A99 File Offset: 0x00084C99
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the hot state.</returns>
				// Token: 0x170006DE RID: 1758
				// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00086AA8 File Offset: 0x00084CA8
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the normal state.</returns>
				// Token: 0x170006DF RID: 1759
				// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00086AB7 File Offset: 0x00084CB7
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the pressed state.</returns>
				// Token: 0x170006E0 RID: 1760
				// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00086AC6 File Offset: 0x00084CC6
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 18, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a window. This class cannot be inherited. </summary>
			// Token: 0x02000330 RID: 816
			public static class FrameBottom
			{
				/// <summary>Gets a visual style element that represents the bottom border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active window.</returns>
				// Token: 0x170006E1 RID: 1761
				// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00086AD5 File Offset: 0x00084CD5
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 9, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the bottom border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive window.</returns>
				// Token: 0x170006E2 RID: 1762
				// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00086AE4 File Offset: 0x00084CE4
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 9, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a window. This class cannot be inherited. </summary>
			// Token: 0x02000331 RID: 817
			public static class FrameLeft
			{
				/// <summary>Gets a visual style element that represents the left border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active window.</returns>
				// Token: 0x170006E3 RID: 1763
				// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00086AF3 File Offset: 0x00084CF3
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 7, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the left border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive window.</returns>
				// Token: 0x170006E4 RID: 1764
				// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00086B01 File Offset: 0x00084D01
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 7, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a window. This class cannot be inherited. </summary>
			// Token: 0x02000332 RID: 818
			public static class FrameRight
			{
				/// <summary>Gets a visual style element that represents the right border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active window.</returns>
				// Token: 0x170006E5 RID: 1765
				// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00086B0F File Offset: 0x00084D0F
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 8, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the right border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive window.</returns>
				// Token: 0x170006E6 RID: 1766
				// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00086B1D File Offset: 0x00084D1D
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 8, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a window or dialog box. This class cannot be inherited. </summary>
			// Token: 0x02000333 RID: 819
			public static class HelpButton
			{
				/// <summary>Gets a visual style element that represents a Help button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the disabled state.</returns>
				// Token: 0x170006E7 RID: 1767
				// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00086B2B File Offset: 0x00084D2B
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the hot state.</returns>
				// Token: 0x170006E8 RID: 1768
				// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00086B3A File Offset: 0x00084D3A
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the normal state.</returns>
				// Token: 0x170006E9 RID: 1769
				// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00086B49 File Offset: 0x00084D49
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the pressed state.</returns>
				// Token: 0x170006EA RID: 1770
				// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00086B58 File Offset: 0x00084D58
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 23, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Maximize button of a window. This class cannot be inherited. </summary>
			// Token: 0x02000334 RID: 820
			public static class MaxButton
			{
				/// <summary>Gets a visual style element that represents a Maximize button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the disabled state.</returns>
				// Token: 0x170006EB RID: 1771
				// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00086B67 File Offset: 0x00084D67
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the hot state.</returns>
				// Token: 0x170006EC RID: 1772
				// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00086B76 File Offset: 0x00084D76
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the normal state.</returns>
				// Token: 0x170006ED RID: 1773
				// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00086B85 File Offset: 0x00084D85
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the pressed state.</returns>
				// Token: 0x170006EE RID: 1774
				// (get) Token: 0x06001C6B RID: 7275 RVA: 0x00086B94 File Offset: 0x00084D94
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 17, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized window. This class cannot be inherited. </summary>
			// Token: 0x02000335 RID: 821
			public static class MaxCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of a maximized active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized active window.</returns>
				// Token: 0x170006EF RID: 1775
				// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00086BA3 File Offset: 0x00084DA3
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 5, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a maximized disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized disabled window.</returns>
				// Token: 0x170006F0 RID: 1776
				// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00086BB1 File Offset: 0x00084DB1
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 5, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a maximized inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized inactive window. </returns>
				// Token: 0x170006F1 RID: 1777
				// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00086BBF File Offset: 0x00084DBF
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 5, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x02000336 RID: 822
			public static class MdiCloseButton
			{
				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the disabled state.</returns>
				// Token: 0x170006F2 RID: 1778
				// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00086BCD File Offset: 0x00084DCD
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the hot state.</returns>
				// Token: 0x170006F3 RID: 1779
				// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00086BDC File Offset: 0x00084DDC
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the normal state.</returns>
				// Token: 0x170006F4 RID: 1780
				// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00086BEB File Offset: 0x00084DEB
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the pressed state.</returns>
				// Token: 0x170006F5 RID: 1781
				// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00086BFA File Offset: 0x00084DFA
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 20, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x02000337 RID: 823
			public static class MdiHelpButton
			{
				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the disabled state.</returns>
				// Token: 0x170006F6 RID: 1782
				// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00086C09 File Offset: 0x00084E09
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the hot state.</returns>
				// Token: 0x170006F7 RID: 1783
				// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00086C18 File Offset: 0x00084E18
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the normal state.</returns>
				// Token: 0x170006F8 RID: 1784
				// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00086C27 File Offset: 0x00084E27
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the pressed state.</returns>
				// Token: 0x170006F9 RID: 1785
				// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00086C36 File Offset: 0x00084E36
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 24, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x02000338 RID: 824
			public static class MdiMinButton
			{
				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the disabled state.</returns>
				// Token: 0x170006FA RID: 1786
				// (get) Token: 0x06001C77 RID: 7287 RVA: 0x00086C45 File Offset: 0x00084E45
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the hot state.</returns>
				// Token: 0x170006FB RID: 1787
				// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00086C54 File Offset: 0x00084E54
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the normal state.</returns>
				// Token: 0x170006FC RID: 1788
				// (get) Token: 0x06001C79 RID: 7289 RVA: 0x00086C63 File Offset: 0x00084E63
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the pressed state.</returns>
				// Token: 0x170006FD RID: 1789
				// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00086C72 File Offset: 0x00084E72
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 16, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
			// Token: 0x02000339 RID: 825
			public static class MdiRestoreButton
			{
				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the disabled state.</returns>
				// Token: 0x170006FE RID: 1790
				// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00086C81 File Offset: 0x00084E81
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the hot state.</returns>
				// Token: 0x170006FF RID: 1791
				// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00086C90 File Offset: 0x00084E90
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the normal state.</returns>
				// Token: 0x17000700 RID: 1792
				// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00086C9F File Offset: 0x00084E9F
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the pressed state.</returns>
				// Token: 0x17000701 RID: 1793
				// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00086CAE File Offset: 0x00084EAE
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 22, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a window. This class cannot be inherited. </summary>
			// Token: 0x0200033A RID: 826
			public static class MinButton
			{
				/// <summary>Gets a visual style element that represents a Minimize button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the disabled state.</returns>
				// Token: 0x17000702 RID: 1794
				// (get) Token: 0x06001C7F RID: 7295 RVA: 0x00086CBD File Offset: 0x00084EBD
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the hot state.</returns>
				// Token: 0x17000703 RID: 1795
				// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00086CCC File Offset: 0x00084ECC
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the normal state.</returns>
				// Token: 0x17000704 RID: 1796
				// (get) Token: 0x06001C81 RID: 7297 RVA: 0x00086CDB File Offset: 0x00084EDB
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the pressed state.</returns>
				// Token: 0x17000705 RID: 1797
				// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00086CEA File Offset: 0x00084EEA
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 15, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized window. This class cannot be inherited. </summary>
			// Token: 0x0200033B RID: 827
			public static class MinCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of a minimized active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized active window.</returns>
				// Token: 0x17000706 RID: 1798
				// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00086CF9 File Offset: 0x00084EF9
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 3, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a minimized disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized disabled window.</returns>
				// Token: 0x17000707 RID: 1799
				// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00086D07 File Offset: 0x00084F07
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 3, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a minimized inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized inactive window.</returns>
				// Token: 0x17000708 RID: 1800
				// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00086D15 File Offset: 0x00084F15
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 3, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a window. This class cannot be inherited. </summary>
			// Token: 0x0200033C RID: 828
			public static class RestoreButton
			{
				/// <summary>Gets a visual style element that represents a Restore button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the disabled state.</returns>
				// Token: 0x17000709 RID: 1801
				// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00086D23 File Offset: 0x00084F23
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 4);
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the hot state.</returns>
				// Token: 0x1700070A RID: 1802
				// (get) Token: 0x06001C87 RID: 7303 RVA: 0x00086D32 File Offset: 0x00084F32
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 2);
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the normal state. </returns>
				// Token: 0x1700070B RID: 1803
				// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00086D41 File Offset: 0x00084F41
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 1);
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the pressed state. </returns>
				// Token: 0x1700070C RID: 1804
				// (get) Token: 0x06001C89 RID: 7305 RVA: 0x00086D50 File Offset: 0x00084F50
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 21, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a small window. This class cannot be inherited. </summary>
			// Token: 0x0200033D RID: 829
			public static class SmallCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window.</returns>
				// Token: 0x1700070D RID: 1805
				// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00086D5F File Offset: 0x00084F5F
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 2, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window.</returns>
				// Token: 0x1700070E RID: 1806
				// (get) Token: 0x06001C8B RID: 7307 RVA: 0x00086D6D File Offset: 0x00084F6D
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 2, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window.</returns>
				// Token: 0x1700070F RID: 1807
				// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00086D7B File Offset: 0x00084F7B
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 2, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a small window. This class cannot be inherited. </summary>
			// Token: 0x0200033E RID: 830
			public static class SmallCloseButton
			{
				/// <summary>Gets a visual style element that represents the small Close button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the disabled state.</returns>
				// Token: 0x17000710 RID: 1808
				// (get) Token: 0x06001C8D RID: 7309 RVA: 0x00086D89 File Offset: 0x00084F89
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 4);
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the hot state.</returns>
				// Token: 0x17000711 RID: 1809
				// (get) Token: 0x06001C8E RID: 7310 RVA: 0x00086D98 File Offset: 0x00084F98
				public static VisualStyleElement Hot
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 2);
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the normal state.</returns>
				// Token: 0x17000712 RID: 1810
				// (get) Token: 0x06001C8F RID: 7311 RVA: 0x00086DA7 File Offset: 0x00084FA7
				public static VisualStyleElement Normal
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the pressed state.</returns>
				// Token: 0x17000713 RID: 1811
				// (get) Token: 0x06001C90 RID: 7312 RVA: 0x00086DB6 File Offset: 0x00084FB6
				public static VisualStyleElement Pressed
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 19, 3);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a small window. This class cannot be inherited. </summary>
			// Token: 0x0200033F RID: 831
			public static class SmallFrameBottom
			{
				/// <summary>Gets a visual style element that represents the bottom border of an active small window. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active small window.</returns>
				// Token: 0x17000714 RID: 1812
				// (get) Token: 0x06001C91 RID: 7313 RVA: 0x00086DC5 File Offset: 0x00084FC5
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 12, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the bottom border of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive small window. </returns>
				// Token: 0x17000715 RID: 1813
				// (get) Token: 0x06001C92 RID: 7314 RVA: 0x00086DD4 File Offset: 0x00084FD4
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 12, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a small window. This class cannot be inherited. </summary>
			// Token: 0x02000340 RID: 832
			public static class SmallFrameLeft
			{
				/// <summary>Gets a visual style element that represents the left border of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active small window.</returns>
				// Token: 0x17000716 RID: 1814
				// (get) Token: 0x06001C93 RID: 7315 RVA: 0x00086DE3 File Offset: 0x00084FE3
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 10, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the left border of an inactive small window. </summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive small window. </returns>
				// Token: 0x17000717 RID: 1815
				// (get) Token: 0x06001C94 RID: 7316 RVA: 0x00086DF2 File Offset: 0x00084FF2
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 10, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a small window. This class cannot be inherited. </summary>
			// Token: 0x02000341 RID: 833
			public static class SmallFrameRight
			{
				/// <summary>Gets a visual style element that represents the right border of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active small window.</returns>
				// Token: 0x17000718 RID: 1816
				// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00086E01 File Offset: 0x00085001
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 11, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the right border of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive small window.</returns>
				// Token: 0x17000719 RID: 1817
				// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00086E10 File Offset: 0x00085010
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 11, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized small window. This class cannot be inherited. </summary>
			// Token: 0x02000342 RID: 834
			public static class SmallMaxCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is maximized.</returns>
				// Token: 0x1700071A RID: 1818
				// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00086E1F File Offset: 0x0008501F
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 6, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is maximized.</returns>
				// Token: 0x1700071B RID: 1819
				// (get) Token: 0x06001C98 RID: 7320 RVA: 0x00086E2D File Offset: 0x0008502D
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 6, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is maximized.</returns>
				// Token: 0x1700071C RID: 1820
				// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00086E3B File Offset: 0x0008503B
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 6, 2);
					}
				}
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized small window. This class cannot be inherited. </summary>
			// Token: 0x02000343 RID: 835
			public static class SmallMinCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is minimized.</returns>
				// Token: 0x1700071D RID: 1821
				// (get) Token: 0x06001C9A RID: 7322 RVA: 0x00086E49 File Offset: 0x00085049
				public static VisualStyleElement Active
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 4, 1);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is minimized.</returns>
				// Token: 0x1700071E RID: 1822
				// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00086E57 File Offset: 0x00085057
				public static VisualStyleElement Disabled
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 4, 3);
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is minimized.</returns>
				// Token: 0x1700071F RID: 1823
				// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00086E65 File Offset: 0x00085065
				public static VisualStyleElement Inactive
				{
					get
					{
						return VisualStyleElement.CreateElement("WINDOW", 4, 2);
					}
				}
			}
		}
	}
}
