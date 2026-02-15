using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004F2 RID: 1266
	internal sealed class VisualTreeUpdater : IDisposable
	{
		// Token: 0x06002364 RID: 9060 RVA: 0x0008279D File Offset: 0x0008099D
		public VisualTreeUpdater(BaseVisualElementPanel panel)
		{
			this.m_Panel = panel;
			this.m_UpdaterArray = new VisualTreeUpdater.UpdaterArray();
			this.SetDefaultUpdaters();
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000827C0 File Offset: 0x000809C0
		public void Dispose()
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater updater = this.m_UpdaterArray[i];
				updater.Dispose();
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000827F4 File Offset: 0x000809F4
		public void UpdateVisualTreePhase(VisualTreeUpdatePhase phase)
		{
			IVisualTreeUpdater updater = this.m_UpdaterArray[phase];
			using (updater.profilerMarker.Auto())
			{
				updater.Update();
				IVisualTreeUpdater visualTreeUpdater = updater;
				long num = visualTreeUpdater.FrameCount + 1L;
				visualTreeUpdater.FrameCount = num;
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0008285C File Offset: 0x00080A5C
		public void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater updater = this.m_UpdaterArray[i];
				updater.OnVersionChanged(ve, versionChangeType);
			}
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x00082894 File Offset: 0x00080A94
		public void SetUpdater<T>(VisualTreeUpdatePhase phase) where T : IVisualTreeUpdater, new()
		{
			IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[phase];
			if (visualTreeUpdater != null)
			{
				visualTreeUpdater.Dispose();
			}
			T t = new T();
			t.panel = this.m_Panel;
			T updater = t;
			this.m_UpdaterArray[phase] = updater;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000828EC File Offset: 0x00080AEC
		public IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_UpdaterArray[phase];
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0008290A File Offset: 0x00080B0A
		private void SetDefaultUpdaters()
		{
			this.SetUpdater<VisualTreeBindingsUpdater>(VisualTreeUpdatePhase.Bindings);
			this.SetUpdater<VisualTreeDataBindingsUpdater>(VisualTreeUpdatePhase.DataBinding);
			this.SetUpdater<VisualElementAnimationSystem>(VisualTreeUpdatePhase.Animation);
			this.SetUpdater<VisualTreeStyleUpdater>(VisualTreeUpdatePhase.Styles);
			this.SetUpdater<UIRLayoutUpdater>(VisualTreeUpdatePhase.Layout);
			this.SetUpdater<VisualTreeHierarchyFlagsUpdater>(VisualTreeUpdatePhase.TransformClip);
			this.SetUpdater<UIRRepaintUpdater>(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x04001028 RID: 4136
		private BaseVisualElementPanel m_Panel;

		// Token: 0x04001029 RID: 4137
		private VisualTreeUpdater.UpdaterArray m_UpdaterArray;

		// Token: 0x020004F3 RID: 1267
		private class UpdaterArray
		{
			// Token: 0x0600236B RID: 9067 RVA: 0x00082945 File Offset: 0x00080B45
			public UpdaterArray()
			{
				this.m_VisualTreeUpdaters = new IVisualTreeUpdater[7];
			}

			// Token: 0x1700094C RID: 2380
			public IVisualTreeUpdater this[VisualTreeUpdatePhase phase]
			{
				get
				{
					return this.m_VisualTreeUpdaters[(int)phase];
				}
				set
				{
					this.m_VisualTreeUpdaters[(int)phase] = value;
				}
			}

			// Token: 0x1700094D RID: 2381
			public IVisualTreeUpdater this[int index]
			{
				get
				{
					return this.m_VisualTreeUpdaters[index];
				}
			}

			// Token: 0x0400102A RID: 4138
			private IVisualTreeUpdater[] m_VisualTreeUpdaters;
		}
	}
}
