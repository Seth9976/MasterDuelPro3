using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020001B2 RID: 434
	public static class CameraCaptureBridge
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002D553 File Offset: 0x0002B753
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0002D55A File Offset: 0x0002B75A
		public static bool enabled
		{
			get
			{
				return CameraCaptureBridge._enabled;
			}
			set
			{
				CameraCaptureBridge._enabled = value;
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002D564 File Offset: 0x0002B764
		public static IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> GetCaptureActions(Camera camera)
		{
			CameraCaptureBridge.CameraEntry entry;
			if (!CameraCaptureBridge.actionDict.TryGetValue(camera, out entry) || entry.actions.Count == 0)
			{
				return null;
			}
			return entry.actions.GetEnumerator();
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002D5A0 File Offset: 0x0002B7A0
		internal static IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> GetCachedCaptureActionsEnumerator(Camera camera)
		{
			CameraCaptureBridge.CameraEntry entry;
			if (!CameraCaptureBridge.actionDict.TryGetValue(camera, out entry) || entry.actions.Count == 0)
			{
				return null;
			}
			entry.cachedEnumerator.Reset();
			return entry.cachedEnumerator;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002D5DC File Offset: 0x0002B7DC
		public static void AddCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			CameraCaptureBridge.CameraEntry entry;
			CameraCaptureBridge.actionDict.TryGetValue(camera, out entry);
			if (entry == null)
			{
				entry = new CameraCaptureBridge.CameraEntry
				{
					actions = new HashSet<Action<RenderTargetIdentifier, CommandBuffer>>()
				};
				CameraCaptureBridge.actionDict.Add(camera, entry);
			}
			entry.actions.Add(action);
			entry.cachedEnumerator = entry.actions.GetEnumerator();
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002D63C File Offset: 0x0002B83C
		public static void RemoveCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			if (camera == null)
			{
				return;
			}
			CameraCaptureBridge.CameraEntry entry;
			if (CameraCaptureBridge.actionDict.TryGetValue(camera, out entry))
			{
				entry.actions.Remove(action);
				entry.cachedEnumerator = entry.actions.GetEnumerator();
			}
		}

		// Token: 0x04000865 RID: 2149
		private static Dictionary<Camera, CameraCaptureBridge.CameraEntry> actionDict = new Dictionary<Camera, CameraCaptureBridge.CameraEntry>();

		// Token: 0x04000866 RID: 2150
		private static bool _enabled;

		// Token: 0x020001B3 RID: 435
		private class CameraEntry
		{
			// Token: 0x04000867 RID: 2151
			internal HashSet<Action<RenderTargetIdentifier, CommandBuffer>> actions;

			// Token: 0x04000868 RID: 2152
			internal IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> cachedEnumerator;
		}
	}
}
