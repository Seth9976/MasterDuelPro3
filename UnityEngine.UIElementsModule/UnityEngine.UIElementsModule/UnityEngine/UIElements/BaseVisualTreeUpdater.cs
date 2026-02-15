using System;
using System.Diagnostics;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020004F5 RID: 1269
	internal abstract class BaseVisualTreeUpdater : IVisualTreeUpdater, IDisposable
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000829A0 File Offset: 0x00080BA0
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x000829B8 File Offset: 0x00080BB8
		long IVisualTreeUpdater.FrameCount
		{
			get
			{
				return this.frameCount;
			}
			set
			{
				this.frameCount = value;
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002377 RID: 9079 RVA: 0x000829C4 File Offset: 0x00080BC4
		// (remove) Token: 0x06002378 RID: 9080 RVA: 0x000829FC File Offset: 0x00080BFC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<BaseVisualElementPanel> panelChanged;

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x00082A34 File Offset: 0x00080C34
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x00082A4C File Offset: 0x00080C4C
		public BaseVisualElementPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				this.m_Panel = value;
				bool flag = this.panelChanged != null;
				if (flag)
				{
					this.panelChanged(value);
				}
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00082A7C File Offset: 0x00080C7C
		public VisualElement visualTree
		{
			get
			{
				return this.panel.visualTree;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600237C RID: 9084
		public abstract ProfilerMarker profilerMarker { get; }

		// Token: 0x0600237D RID: 9085 RVA: 0x00082A99 File Offset: 0x00080C99
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000020EA File Offset: 0x000002EA
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600237F RID: 9087
		public abstract void Update();

		// Token: 0x06002380 RID: 9088
		public abstract void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);

		// Token: 0x0400102B RID: 4139
		private long frameCount;

		// Token: 0x0400102D RID: 4141
		private BaseVisualElementPanel m_Panel;
	}
}
