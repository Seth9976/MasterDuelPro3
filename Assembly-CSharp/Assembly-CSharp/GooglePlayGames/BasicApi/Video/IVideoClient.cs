using System;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020011C0 RID: 4544
	public interface IVideoClient
	{
		// Token: 0x0600877A RID: 34682
		void GetCaptureCapabilities(Action<ResponseStatus, VideoCapabilities> callback);

		// Token: 0x0600877B RID: 34683
		void ShowCaptureOverlay();

		// Token: 0x0600877C RID: 34684
		void GetCaptureState(Action<ResponseStatus, VideoCaptureState> callback);

		// Token: 0x0600877D RID: 34685
		void IsCaptureAvailable(VideoCaptureMode captureMode, Action<ResponseStatus, bool> callback);

		// Token: 0x0600877E RID: 34686
		bool IsCaptureSupported();

		// Token: 0x0600877F RID: 34687
		void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListener listener);

		// Token: 0x06008780 RID: 34688
		void UnregisterCaptureOverlayStateChangedListener();
	}
}
