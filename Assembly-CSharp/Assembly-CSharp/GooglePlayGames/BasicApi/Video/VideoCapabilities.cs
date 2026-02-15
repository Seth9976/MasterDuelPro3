using System;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020011C1 RID: 4545
	public class VideoCapabilities
	{
		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06008781 RID: 34689 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCameraSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06008782 RID: 34690 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMicSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06008783 RID: 34691 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsWriteStorageSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008784 RID: 34692 RVA: 0x00002739 File Offset: 0x00000939
		internal VideoCapabilities(bool isCameraSupported, bool isMicSupported, bool isWriteStorageSupported, bool[] captureModesSupported, bool[] qualityLevelsSupported)
		{
		}

		// Token: 0x06008785 RID: 34693 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SupportsCaptureMode(VideoCaptureMode captureMode)
		{
			return false;
		}

		// Token: 0x06008786 RID: 34694 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SupportsQualityLevel(VideoQualityLevel qualityLevel)
		{
			return false;
		}

		// Token: 0x06008787 RID: 34695 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x0400C1FD RID: 49661
		private bool mIsCameraSupported;

		// Token: 0x0400C1FE RID: 49662
		private bool mIsMicSupported;

		// Token: 0x0400C1FF RID: 49663
		private bool mIsWriteStorageSupported;

		// Token: 0x0400C200 RID: 49664
		private bool[] mCaptureModesSupported;

		// Token: 0x0400C201 RID: 49665
		private bool[] mQualityLevelsSupported;
	}
}
