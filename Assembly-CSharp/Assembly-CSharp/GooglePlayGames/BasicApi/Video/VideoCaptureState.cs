using System;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020011C2 RID: 4546
	public class VideoCaptureState
	{
		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06008788 RID: 34696 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCapturing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06008789 RID: 34697 RVA: 0x000029CC File Offset: 0x00000BCC
		public VideoCaptureMode CaptureMode
		{
			get
			{
				return VideoCaptureMode.File;
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x0600878A RID: 34698 RVA: 0x000029CC File Offset: 0x00000BCC
		public VideoQualityLevel QualityLevel
		{
			get
			{
				return VideoQualityLevel.SD;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x0600878B RID: 34699 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsOverlayVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x0600878C RID: 34700 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPaused
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600878D RID: 34701 RVA: 0x00002739 File Offset: 0x00000939
		internal VideoCaptureState(bool isCapturing, VideoCaptureMode captureMode, VideoQualityLevel qualityLevel, bool isOverlayVisible, bool isPaused)
		{
		}

		// Token: 0x0600878E RID: 34702 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x0400C202 RID: 49666
		private bool mIsCapturing;

		// Token: 0x0400C203 RID: 49667
		private VideoCaptureMode mCaptureMode;

		// Token: 0x0400C204 RID: 49668
		private VideoQualityLevel mQualityLevel;

		// Token: 0x0400C205 RID: 49669
		private bool mIsOverlayVisible;

		// Token: 0x0400C206 RID: 49670
		private bool mIsPaused;
	}
}
