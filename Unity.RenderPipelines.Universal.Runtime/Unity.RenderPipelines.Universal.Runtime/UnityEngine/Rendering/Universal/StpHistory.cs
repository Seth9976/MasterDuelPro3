using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CE RID: 206
	internal sealed class StpHistory : CameraHistoryItem
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x000139E4 File Offset: 0x00011BE4
		public override void OnCreate(BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
			{
				this.m_historyContexts[eyeIndex] = new STP.HistoryContext();
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00013A14 File Offset: 0x00011C14
		public override void Reset()
		{
			for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
			{
				this.m_historyContexts[eyeIndex].Dispose();
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00013A3A File Offset: 0x00011C3A
		internal STP.HistoryContext GetHistoryContext(int eyeIndex)
		{
			return this.m_historyContexts[eyeIndex];
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00013A44 File Offset: 0x00011C44
		internal bool Update(UniversalCameraData cameraData)
		{
			STP.HistoryUpdateInfo info;
			info.preUpscaleSize = new Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
			info.postUpscaleSize = new Vector2Int(cameraData.pixelWidth, cameraData.pixelHeight);
			info.useHwDrs = false;
			info.useTexArray = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			int eyeIndex = ((cameraData.xr.enabled && !cameraData.xr.singlePassEnabled) ? cameraData.xr.multipassId : 0);
			return !this.GetHistoryContext(eyeIndex).Update(ref info);
		}

		// Token: 0x04000496 RID: 1174
		private STP.HistoryContext[] m_historyContexts = new STP.HistoryContext[2];
	}
}
