using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000C5 RID: 197
	public abstract class BaseRaycaster : UIBehaviour
	{
		// Token: 0x06000746 RID: 1862
		public abstract void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList);

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000747 RID: 1863
		public abstract Camera eventCamera { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x000093DE File Offset: 0x000075DE
		[Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001C389 File Offset: 0x0001A589
		public virtual int sortOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001C389 File Offset: 0x0001A589
		public virtual int renderOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001C390 File Offset: 0x0001A590
		public BaseRaycaster rootRaycaster
		{
			get
			{
				if (this.m_RootRaycaster == null)
				{
					BaseRaycaster[] baseRaycasters = base.GetComponentsInParent<BaseRaycaster>();
					if (baseRaycasters.Length != 0)
					{
						this.m_RootRaycaster = baseRaycasters[baseRaycasters.Length - 1];
					}
				}
				return this.m_RootRaycaster;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		public override string ToString()
		{
			string[] array = new string[8];
			array[0] = "Name: ";
			int num = 1;
			GameObject gameObject = base.gameObject;
			array[num] = ((gameObject != null) ? gameObject.ToString() : null);
			array[2] = "\neventCamera: ";
			int num2 = 3;
			Camera eventCamera = this.eventCamera;
			array[num2] = ((eventCamera != null) ? eventCamera.ToString() : null);
			array[4] = "\nsortOrderPriority: ";
			array[5] = this.sortOrderPriority.ToString();
			array[6] = "\nrenderOrderPriority: ";
			array[7] = this.renderOrderPriority.ToString();
			return string.Concat(array);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001C450 File Offset: 0x0001A650
		protected override void OnEnable()
		{
			base.OnEnable();
			RaycasterManager.AddRaycaster(this);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001C45E File Offset: 0x0001A65E
		protected override void OnDisable()
		{
			RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001C46C File Offset: 0x0001A66C
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001C47B File Offset: 0x0001A67B
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x04000352 RID: 850
		private BaseRaycaster m_RootRaycaster;
	}
}
