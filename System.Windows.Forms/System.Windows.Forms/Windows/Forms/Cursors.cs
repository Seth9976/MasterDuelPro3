using System;

namespace System.Windows.Forms
{
	/// <summary>Provides a collection of <see cref="T:System.Windows.Forms.Cursor" /> objects for use by a Windows Forms application.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000064 RID: 100
	public sealed class Cursors
	{
		/// <summary>Gets the cursor that appears when an application starts.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when an application starts.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000126AC File Offset: 0x000108AC
		public static Cursor AppStarting
		{
			get
			{
				if (Cursors.app_starting == null)
				{
					Cursors.app_starting = new Cursor(StdCursor.AppStarting);
					Cursors.app_starting.name = "AppStarting";
				}
				return Cursors.app_starting;
			}
		}

		/// <summary>Gets the arrow cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the arrow cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000126DA File Offset: 0x000108DA
		public static Cursor Arrow
		{
			get
			{
				if (Cursors.arrow == null)
				{
					Cursors.arrow = new Cursor(StdCursor.Arrow);
					Cursors.arrow.name = "Arrow";
				}
				return Cursors.arrow;
			}
		}

		/// <summary>Gets the crosshair cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the crosshair cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00012708 File Offset: 0x00010908
		public static Cursor Cross
		{
			get
			{
				if (Cursors.cross == null)
				{
					Cursors.cross = new Cursor(StdCursor.Cross);
					Cursors.cross.name = "Cross";
				}
				return Cursors.cross;
			}
		}

		/// <summary>Gets the default cursor, which is usually an arrow cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the default cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00012736 File Offset: 0x00010936
		public static Cursor Default
		{
			get
			{
				if (Cursors.def == null)
				{
					Cursors.def = new Cursor(StdCursor.Default);
					Cursors.def.name = "Default";
				}
				return Cursors.def;
			}
		}

		/// <summary>Gets the hand cursor, typically used when hovering over a Web link.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the hand cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00012764 File Offset: 0x00010964
		public static Cursor Hand
		{
			get
			{
				if (Cursors.hand == null)
				{
					Cursors.hand = new Cursor(StdCursor.Hand);
					Cursors.hand.name = "Hand";
				}
				return Cursors.hand;
			}
		}

		/// <summary>Gets the Help cursor, which is a combination of an arrow and a question mark.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the Help cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00012792 File Offset: 0x00010992
		public static Cursor Help
		{
			get
			{
				if (Cursors.help == null)
				{
					Cursors.help = new Cursor(StdCursor.Help);
					Cursors.help.name = "Help";
				}
				return Cursors.help;
			}
		}

		/// <summary>Gets the cursor that appears when the mouse is positioned over a horizontal splitter bar.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a horizontal splitter bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000127C0 File Offset: 0x000109C0
		public static Cursor HSplit
		{
			get
			{
				if (Cursors.hsplit == null)
				{
					Cursors.hsplit = new Cursor(typeof(Splitter), "SplitterNS.cur");
					Cursors.hsplit.name = "HSplit";
				}
				return Cursors.hsplit;
			}
		}

		/// <summary>Gets the I-beam cursor, which is used to show where the text cursor appears when the mouse is clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the I-beam cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000127FC File Offset: 0x000109FC
		public static Cursor IBeam
		{
			get
			{
				if (Cursors.ibeam == null)
				{
					Cursors.ibeam = new Cursor(StdCursor.IBeam);
					Cursors.ibeam.name = "IBeam";
				}
				return Cursors.ibeam;
			}
		}

		/// <summary>Gets the cursor that indicates that a particular region is invalid for the current operation.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that indicates that a particular region is invalid for the current operation.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001282A File Offset: 0x00010A2A
		public static Cursor No
		{
			get
			{
				if (Cursors.no == null)
				{
					Cursors.no = new Cursor(StdCursor.No);
					Cursors.no.name = "No";
				}
				return Cursors.no;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in both a horizontal and vertical direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00012858 File Offset: 0x00010A58
		public static Cursor NoMove2D
		{
			get
			{
				if (Cursors.no_move_2d == null)
				{
					Cursors.no_move_2d = new Cursor(StdCursor.NoMove2D);
					Cursors.no_move_2d.name = "NoMove2D";
				}
				return Cursors.no_move_2d;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a horizontal direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00012887 File Offset: 0x00010A87
		public static Cursor NoMoveHoriz
		{
			get
			{
				if (Cursors.no_move_horiz == null)
				{
					Cursors.no_move_horiz = new Cursor(StdCursor.NoMoveHoriz);
					Cursors.no_move_horiz.name = "NoMoveHoriz";
				}
				return Cursors.no_move_horiz;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a vertical direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000128B6 File Offset: 0x00010AB6
		public static Cursor NoMoveVert
		{
			get
			{
				if (Cursors.no_move_vert == null)
				{
					Cursors.no_move_vert = new Cursor(StdCursor.NoMoveVert);
					Cursors.no_move_vert.name = "NoMoveVert";
				}
				return Cursors.no_move_vert;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000128E5 File Offset: 0x00010AE5
		public static Cursor PanEast
		{
			get
			{
				if (Cursors.pan_east == null)
				{
					Cursors.pan_east = new Cursor(StdCursor.PanEast);
					Cursors.pan_east.name = "PanEast";
				}
				return Cursors.pan_east;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00012914 File Offset: 0x00010B14
		public static Cursor PanNE
		{
			get
			{
				if (Cursors.pan_ne == null)
				{
					Cursors.pan_ne = new Cursor(StdCursor.PanNE);
					Cursors.pan_ne.name = "PanNE";
				}
				return Cursors.pan_ne;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00012943 File Offset: 0x00010B43
		public static Cursor PanNorth
		{
			get
			{
				if (Cursors.pan_north == null)
				{
					Cursors.pan_north = new Cursor(StdCursor.PanNorth);
					Cursors.pan_north.name = "PanNorth";
				}
				return Cursors.pan_north;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00012972 File Offset: 0x00010B72
		public static Cursor PanNW
		{
			get
			{
				if (Cursors.pan_nw == null)
				{
					Cursors.pan_nw = new Cursor(StdCursor.PanNW);
					Cursors.pan_nw.name = "PanNW";
				}
				return Cursors.pan_nw;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000129A1 File Offset: 0x00010BA1
		public static Cursor PanSE
		{
			get
			{
				if (Cursors.pan_se == null)
				{
					Cursors.pan_se = new Cursor(StdCursor.PanSE);
					Cursors.pan_se.name = "PanSE";
				}
				return Cursors.pan_se;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000129D0 File Offset: 0x00010BD0
		public static Cursor PanSouth
		{
			get
			{
				if (Cursors.pan_south == null)
				{
					Cursors.pan_south = new Cursor(StdCursor.PanSouth);
					Cursors.pan_south.name = "PanSouth";
				}
				return Cursors.pan_south;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x000129FF File Offset: 0x00010BFF
		public static Cursor PanSW
		{
			get
			{
				if (Cursors.pan_sw == null)
				{
					Cursors.pan_sw = new Cursor(StdCursor.PanSW);
					Cursors.pan_sw.name = "PanSW";
				}
				return Cursors.pan_sw;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00012A2E File Offset: 0x00010C2E
		public static Cursor PanWest
		{
			get
			{
				if (Cursors.pan_west == null)
				{
					Cursors.pan_west = new Cursor(StdCursor.PanWest);
					Cursors.pan_west.name = "PanWest";
				}
				return Cursors.pan_west;
			}
		}

		/// <summary>Gets the four-headed sizing cursor, which consists of four joined arrows that point north, south, east, and west.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the four-headed sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00012A5D File Offset: 0x00010C5D
		public static Cursor SizeAll
		{
			get
			{
				if (Cursors.size_all == null)
				{
					Cursors.size_all = new Cursor(StdCursor.SizeAll);
					Cursors.size_all.name = "SizeAll";
				}
				return Cursors.size_all;
			}
		}

		/// <summary>Gets the two-headed diagonal (northeast/southwest) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents two-headed diagonal (northeast/southwest) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00012A8C File Offset: 0x00010C8C
		public static Cursor SizeNESW
		{
			get
			{
				if (Cursors.size_nesw == null)
				{
					if (XplatUI.RunningOnUnix)
					{
						Cursors.size_nesw = new Cursor(typeof(Cursor), "NESW.cur");
						Cursors.size_nesw.name = "SizeNESW";
					}
					else
					{
						Cursors.size_nesw = new Cursor(StdCursor.SizeNESW);
						Cursors.size_nesw.name = "SizeNESW";
					}
				}
				return Cursors.size_nesw;
			}
		}

		/// <summary>Gets the two-headed vertical (north/south) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed vertical (north/south) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00012AF7 File Offset: 0x00010CF7
		public static Cursor SizeNS
		{
			get
			{
				if (Cursors.size_ns == null)
				{
					Cursors.size_ns = new Cursor(StdCursor.SizeNS);
					Cursors.size_ns.name = "SizeNS";
				}
				return Cursors.size_ns;
			}
		}

		/// <summary>Gets the two-headed diagonal (northwest/southeast) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed diagonal (northwest/southeast) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00012B28 File Offset: 0x00010D28
		public static Cursor SizeNWSE
		{
			get
			{
				if (Cursors.size_nwse == null)
				{
					if (XplatUI.RunningOnUnix)
					{
						Cursors.size_nwse = new Cursor(typeof(Cursor), "NWSE.cur");
						Cursors.size_nwse.name = "SizeNWSE";
					}
					else
					{
						Cursors.size_nwse = new Cursor(StdCursor.SizeNWSE);
						Cursors.size_nwse.name = "SizeNWSE";
					}
				}
				return Cursors.size_nwse;
			}
		}

		/// <summary>Gets the two-headed horizontal (west/east) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed horizontal (west/east) sizing cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00012B93 File Offset: 0x00010D93
		public static Cursor SizeWE
		{
			get
			{
				if (Cursors.size_we == null)
				{
					Cursors.size_we = new Cursor(StdCursor.SizeWE);
					Cursors.size_we.name = "SizeWE";
				}
				return Cursors.size_we;
			}
		}

		/// <summary>Gets the up arrow cursor, typically used to identify an insertion point.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the up arrow cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00012BC2 File Offset: 0x00010DC2
		public static Cursor UpArrow
		{
			get
			{
				if (Cursors.up_arrow == null)
				{
					Cursors.up_arrow = new Cursor(StdCursor.UpArrow);
					Cursors.up_arrow.name = "UpArrow";
				}
				return Cursors.up_arrow;
			}
		}

		/// <summary>Gets the cursor that appears when the mouse is positioned over a vertical splitter bar.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a vertical splitter bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00012BF1 File Offset: 0x00010DF1
		public static Cursor VSplit
		{
			get
			{
				if (Cursors.vsplit == null)
				{
					Cursors.vsplit = new Cursor(typeof(Cursor), "SplitterWE.cur");
					Cursors.vsplit.name = "VSplit";
				}
				return Cursors.vsplit;
			}
		}

		/// <summary>Gets the wait cursor, typically an hourglass shape.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the wait cursor.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00012C2D File Offset: 0x00010E2D
		public static Cursor WaitCursor
		{
			get
			{
				if (Cursors.wait_cursor == null)
				{
					Cursors.wait_cursor = new Cursor(StdCursor.WaitCursor);
					Cursors.wait_cursor.name = "WaitCursor";
				}
				return Cursors.wait_cursor;
			}
		}

		// Token: 0x04000282 RID: 642
		internal static Cursor app_starting;

		// Token: 0x04000283 RID: 643
		internal static Cursor arrow;

		// Token: 0x04000284 RID: 644
		internal static Cursor cross;

		// Token: 0x04000285 RID: 645
		internal static Cursor def;

		// Token: 0x04000286 RID: 646
		internal static Cursor hand;

		// Token: 0x04000287 RID: 647
		internal static Cursor help;

		// Token: 0x04000288 RID: 648
		internal static Cursor hsplit;

		// Token: 0x04000289 RID: 649
		internal static Cursor ibeam;

		// Token: 0x0400028A RID: 650
		internal static Cursor no;

		// Token: 0x0400028B RID: 651
		internal static Cursor no_move_2d;

		// Token: 0x0400028C RID: 652
		internal static Cursor no_move_horiz;

		// Token: 0x0400028D RID: 653
		internal static Cursor no_move_vert;

		// Token: 0x0400028E RID: 654
		internal static Cursor pan_east;

		// Token: 0x0400028F RID: 655
		internal static Cursor pan_ne;

		// Token: 0x04000290 RID: 656
		internal static Cursor pan_north;

		// Token: 0x04000291 RID: 657
		internal static Cursor pan_nw;

		// Token: 0x04000292 RID: 658
		internal static Cursor pan_se;

		// Token: 0x04000293 RID: 659
		internal static Cursor pan_south;

		// Token: 0x04000294 RID: 660
		internal static Cursor pan_sw;

		// Token: 0x04000295 RID: 661
		internal static Cursor pan_west;

		// Token: 0x04000296 RID: 662
		internal static Cursor size_all;

		// Token: 0x04000297 RID: 663
		internal static Cursor size_nesw;

		// Token: 0x04000298 RID: 664
		internal static Cursor size_ns;

		// Token: 0x04000299 RID: 665
		internal static Cursor size_nwse;

		// Token: 0x0400029A RID: 666
		internal static Cursor size_we;

		// Token: 0x0400029B RID: 667
		internal static Cursor up_arrow;

		// Token: 0x0400029C RID: 668
		internal static Cursor vsplit;

		// Token: 0x0400029D RID: 669
		internal static Cursor wait_cursor;
	}
}
