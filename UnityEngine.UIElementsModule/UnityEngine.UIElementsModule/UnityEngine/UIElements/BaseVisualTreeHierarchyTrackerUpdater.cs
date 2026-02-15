using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004EA RID: 1258
	internal abstract class BaseVisualTreeHierarchyTrackerUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x06002333 RID: 9011
		protected abstract void OnHierarchyChange(VisualElement ve, HierarchyChangeType type);

		// Token: 0x06002334 RID: 9012 RVA: 0x00081754 File Offset: 0x0007F954
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag)
			{
				switch (this.m_State)
				{
				case BaseVisualTreeHierarchyTrackerUpdater.State.Waiting:
					this.ProcessNewChange(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove:
					this.ProcessAddOrMove(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove:
					this.ProcessRemove(ve);
					break;
				}
			}
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000817AC File Offset: 0x0007F9AC
		public override void Update()
		{
			Debug.Assert(this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove || this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.Waiting);
			bool flag = this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			this.m_CurrentChangeElement = null;
			this.m_CurrentChangeParent = null;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00081808 File Offset: 0x0007FA08
		private void ProcessNewChange(VisualElement ve)
		{
			this.m_CurrentChangeElement = ve;
			this.m_CurrentChangeParent = ve.parent;
			bool flag = this.m_CurrentChangeParent == null && ve.panel != null;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_State = ((this.m_CurrentChangeParent == null) ? BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove : BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove);
			}
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00081870 File Offset: 0x0007FA70
		private void ProcessAddOrMove(VisualElement ve)
		{
			Debug.Assert(this.m_CurrentChangeParent != null);
			bool flag = this.m_CurrentChangeParent == ve;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Add);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.ProcessNewChange(ve);
			}
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000818CC File Offset: 0x0007FACC
		private void ProcessRemove(VisualElement ve)
		{
			this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Remove);
			bool flag = ve.panel != null;
			if (flag)
			{
				this.m_CurrentChangeParent = null;
				this.m_CurrentChangeElement = null;
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_CurrentChangeElement = ve;
			}
		}

		// Token: 0x04000FFC RID: 4092
		private BaseVisualTreeHierarchyTrackerUpdater.State m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;

		// Token: 0x04000FFD RID: 4093
		private VisualElement m_CurrentChangeElement;

		// Token: 0x04000FFE RID: 4094
		private VisualElement m_CurrentChangeParent;

		// Token: 0x020004EB RID: 1259
		private enum State
		{
			// Token: 0x04001000 RID: 4096
			Waiting,
			// Token: 0x04001001 RID: 4097
			TrackingAddOrMove,
			// Token: 0x04001002 RID: 4098
			TrackingRemove
		}
	}
}
