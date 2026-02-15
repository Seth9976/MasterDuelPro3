using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E7 RID: 1255
	internal class VisualElementAnimationSystem : BaseVisualTreeUpdater
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x00081302 File Offset: 0x0007F502
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_ProfilerMarker;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x00081309 File Offset: 0x0007F509
		private static ProfilerMarker stylePropertyAnimationProfilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_StylePropertyAnimationProfilerMarker;
			}
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00081310 File Offset: 0x0007F510
		public void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Remove(anim);
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00081328 File Offset: 0x0007F528
		public void UnregisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate a in anims)
			{
				this.m_Animations.Remove(a);
			}
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00081388 File Offset: 0x0007F588
		public void RegisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Add(anim);
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000813A8 File Offset: 0x0007F5A8
		public void RegisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate a in anims)
			{
				this.m_Animations.Add(a);
			}
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00081410 File Offset: 0x0007F610
		public override void Update()
		{
			long now = Panel.TimeSinceStartupMs();
			bool iterationListDirty = this.m_IterationListDirty;
			if (iterationListDirty)
			{
				this.m_IterationList = this.m_Animations.ToList<IValueAnimationUpdate>();
				this.m_IterationListDirty = false;
			}
			bool flag = this.m_HasNewAnimations || this.lastUpdate != now;
			if (flag)
			{
				foreach (IValueAnimationUpdate anim in this.m_IterationList)
				{
					anim.Tick(now);
				}
				this.m_HasNewAnimations = false;
				this.lastUpdate = now;
			}
			IStylePropertyAnimationSystem styleAnim = base.panel.styleAnimationSystem;
			using (VisualElementAnimationSystem.stylePropertyAnimationProfilerMarker.Auto())
			{
				styleAnim.Update();
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000020EA File Offset: 0x000002EA
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
		}

		// Token: 0x04000FEB RID: 4075
		private HashSet<IValueAnimationUpdate> m_Animations = new HashSet<IValueAnimationUpdate>();

		// Token: 0x04000FEC RID: 4076
		private List<IValueAnimationUpdate> m_IterationList = new List<IValueAnimationUpdate>();

		// Token: 0x04000FED RID: 4077
		private bool m_HasNewAnimations = false;

		// Token: 0x04000FEE RID: 4078
		private bool m_IterationListDirty = false;

		// Token: 0x04000FEF RID: 4079
		private static readonly string s_Description = "Animation Update";

		// Token: 0x04000FF0 RID: 4080
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_Description);

		// Token: 0x04000FF1 RID: 4081
		private static readonly string s_StylePropertyAnimationDescription = "StylePropertyAnimation Update";

		// Token: 0x04000FF2 RID: 4082
		private static readonly ProfilerMarker s_StylePropertyAnimationProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_StylePropertyAnimationDescription);

		// Token: 0x04000FF3 RID: 4083
		private long lastUpdate;
	}
}
