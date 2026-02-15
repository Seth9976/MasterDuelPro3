using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x0200039A RID: 922
	internal struct CarbonCursor
	{
		// Token: 0x06001DC7 RID: 7623 RVA: 0x00093FF1 File Offset: 0x000921F1
		public CarbonCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			this.id = StdCursor.Default;
			this.bmp = bitmap;
			this.mask = mask;
			this.cursor_color = cursor_pixel;
			this.mask_color = mask_pixel;
			this.hot_x = xHotSpot;
			this.hot_y = yHotSpot;
			this.standard = true;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00094030 File Offset: 0x00092230
		public CarbonCursor(StdCursor id)
		{
			this.id = id;
			this.bmp = null;
			this.mask = null;
			this.cursor_color = Color.Black;
			this.mask_color = Color.Black;
			this.hot_x = 0;
			this.hot_y = 0;
			this.standard = true;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0009407D File Offset: 0x0009227D
		public void SetCursor()
		{
			if (this.standard)
			{
				this.SetStandardCursor();
				return;
			}
			this.SetCustomCursor();
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00094094 File Offset: 0x00092294
		public void SetCustomCursor()
		{
			throw new NotImplementedException("We dont support custom cursors yet");
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000940A0 File Offset: 0x000922A0
		public void SetStandardCursor()
		{
			switch (this.id)
			{
			case StdCursor.Default:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.AppStarting:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeSpinningCursor);
				return;
			case StdCursor.Arrow:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.Cross:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeCrossCursor);
				return;
			case StdCursor.Hand:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeOpenHandCursor);
				return;
			case StdCursor.Help:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.HSplit:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeLeftRightCursor);
				return;
			case StdCursor.IBeam:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeIBeamCursor);
				return;
			case StdCursor.No:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				return;
			case StdCursor.NoMove2D:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				return;
			case StdCursor.NoMoveHoriz:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				return;
			case StdCursor.NoMoveVert:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeNotAllowedCursor);
				return;
			case StdCursor.PanEast:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeRightCursor);
				return;
			case StdCursor.PanNE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.PanNorth:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.PanNW:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.PanSE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.PanSouth:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.PanSW:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.PanWest:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeLeftCursor);
				return;
			case StdCursor.SizeAll:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeResizeLeftRightCursor);
				return;
			case StdCursor.SizeNESW:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.SizeNS:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.SizeNWSE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.SizeWE:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.UpArrow:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.VSplit:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			case StdCursor.WaitCursor:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeSpinningCursor);
				return;
			default:
				CarbonCursor.SetThemeCursor(ThemeCursor.kThemeArrowCursor);
				return;
			}
		}

		// Token: 0x06001DCC RID: 7628
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetThemeCursor(ThemeCursor cursor);

		// Token: 0x04001CE3 RID: 7395
		private Bitmap bmp;

		// Token: 0x04001CE4 RID: 7396
		private Bitmap mask;

		// Token: 0x04001CE5 RID: 7397
		private Color cursor_color;

		// Token: 0x04001CE6 RID: 7398
		private Color mask_color;

		// Token: 0x04001CE7 RID: 7399
		private int hot_x;

		// Token: 0x04001CE8 RID: 7400
		private int hot_y;

		// Token: 0x04001CE9 RID: 7401
		private StdCursor id;

		// Token: 0x04001CEA RID: 7402
		private bool standard;
	}
}
