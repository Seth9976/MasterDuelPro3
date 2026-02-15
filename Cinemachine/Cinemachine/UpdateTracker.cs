using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000A6 RID: 166
	[DocumentationSorting(DocumentationSortingAttribute.Level.Undoc)]
	internal class UpdateTracker
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x000166DC File Offset: 0x000148DC
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeModule()
		{
			UpdateTracker.mUpdateStatus.Clear();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000166E8 File Offset: 0x000148E8
		private static void UpdateTargets(UpdateTracker.UpdateClock currentClock)
		{
			int now = Time.frameCount;
			foreach (KeyValuePair<Transform, UpdateTracker.UpdateStatus> current in UpdateTracker.mUpdateStatus)
			{
				if (current.Key == null)
				{
					UpdateTracker.sToDelete.Add(current.Key);
				}
				else
				{
					current.Value.OnUpdate(now, currentClock, current.Key.localToWorldMatrix);
				}
			}
			for (int i = UpdateTracker.sToDelete.Count - 1; i >= 0; i--)
			{
				UpdateTracker.mUpdateStatus.Remove(UpdateTracker.sToDelete[i]);
			}
			UpdateTracker.sToDelete.Clear();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001678C File Offset: 0x0001498C
		public static UpdateTracker.UpdateClock GetPreferredUpdate(Transform target)
		{
			if (Application.isPlaying && target != null)
			{
				UpdateTracker.UpdateStatus status;
				if (UpdateTracker.mUpdateStatus.TryGetValue(target, out status))
				{
					return status.PreferredUpdate;
				}
				status = new UpdateTracker.UpdateStatus(Time.frameCount, target.localToWorldMatrix);
				UpdateTracker.mUpdateStatus.Add(target, status);
			}
			return UpdateTracker.UpdateClock.Late;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000167E0 File Offset: 0x000149E0
		public static void OnUpdate(UpdateTracker.UpdateClock currentClock)
		{
			float now = CinemachineCore.CurrentTime;
			if (now != UpdateTracker.mLastUpdateTime)
			{
				UpdateTracker.mLastUpdateTime = now;
				UpdateTracker.UpdateTargets(currentClock);
			}
		}

		// Token: 0x04000369 RID: 873
		private static Dictionary<Transform, UpdateTracker.UpdateStatus> mUpdateStatus = new Dictionary<Transform, UpdateTracker.UpdateStatus>();

		// Token: 0x0400036A RID: 874
		private static List<Transform> sToDelete = new List<Transform>();

		// Token: 0x0400036B RID: 875
		private static float mLastUpdateTime;

		// Token: 0x020000A7 RID: 167
		public enum UpdateClock
		{
			// Token: 0x0400036D RID: 877
			Fixed,
			// Token: 0x0400036E RID: 878
			Late
		}

		// Token: 0x020000A8 RID: 168
		private class UpdateStatus
		{
			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0001681D File Offset: 0x00014A1D
			// (set) Token: 0x060003DF RID: 991 RVA: 0x00016825 File Offset: 0x00014A25
			public UpdateTracker.UpdateClock PreferredUpdate { get; private set; }

			// Token: 0x060003E0 RID: 992 RVA: 0x0001682E File Offset: 0x00014A2E
			public UpdateStatus(int currentFrame, Matrix4x4 pos)
			{
				this.windowStart = currentFrame;
				this.lastFrameUpdated = Time.frameCount;
				this.PreferredUpdate = UpdateTracker.UpdateClock.Late;
				this.lastPos = pos;
			}

			// Token: 0x060003E1 RID: 993 RVA: 0x00016858 File Offset: 0x00014A58
			public void OnUpdate(int currentFrame, UpdateTracker.UpdateClock currentClock, Matrix4x4 pos)
			{
				if (this.lastPos == pos)
				{
					return;
				}
				if (currentClock == UpdateTracker.UpdateClock.Late)
				{
					this.numWindowLateUpdateMoves++;
				}
				else if (this.lastFrameUpdated != currentFrame)
				{
					this.numWindowFixedUpdateMoves++;
				}
				this.lastPos = pos;
				UpdateTracker.UpdateClock choice;
				if (this.numWindowFixedUpdateMoves > 3 && this.numWindowLateUpdateMoves < this.numWindowFixedUpdateMoves / 3)
				{
					choice = UpdateTracker.UpdateClock.Fixed;
				}
				else
				{
					choice = UpdateTracker.UpdateClock.Late;
				}
				if (this.numWindows == 0)
				{
					this.PreferredUpdate = choice;
				}
				if (this.windowStart + 30 <= currentFrame)
				{
					this.PreferredUpdate = choice;
					this.numWindows++;
					this.windowStart = currentFrame;
					this.numWindowLateUpdateMoves = ((this.PreferredUpdate == UpdateTracker.UpdateClock.Late) ? 1 : 0);
					this.numWindowFixedUpdateMoves = ((this.PreferredUpdate == UpdateTracker.UpdateClock.Fixed) ? 1 : 0);
				}
			}

			// Token: 0x0400036F RID: 879
			private const int kWindowSize = 30;

			// Token: 0x04000370 RID: 880
			private int windowStart;

			// Token: 0x04000371 RID: 881
			private int numWindowLateUpdateMoves;

			// Token: 0x04000372 RID: 882
			private int numWindowFixedUpdateMoves;

			// Token: 0x04000373 RID: 883
			private int numWindows;

			// Token: 0x04000374 RID: 884
			private int lastFrameUpdated;

			// Token: 0x04000375 RID: 885
			private Matrix4x4 lastPos;
		}
	}
}
