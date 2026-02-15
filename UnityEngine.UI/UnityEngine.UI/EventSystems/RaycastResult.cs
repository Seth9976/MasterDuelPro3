using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000C3 RID: 195
	public struct RaycastResult
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001C12C File Offset: 0x0001A32C
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001C134 File Offset: 0x0001A334
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001C13D File Offset: 0x0001A33D
		public bool isValid
		{
			get
			{
				return this.module != null && this.gameObject != null;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001C15C File Offset: 0x0001A35C
		public void Clear()
		{
			this.gameObject = null;
			this.module = null;
			this.distance = 0f;
			this.index = 0f;
			this.depth = 0;
			this.sortingLayer = 0;
			this.sortingOrder = 0;
			this.worldNormal = Vector3.up;
			this.worldPosition = Vector3.zero;
			this.screenPosition = Vector3.zero;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		public override string ToString()
		{
			if (!this.isValid)
			{
				return "";
			}
			string[] array = new string[24];
			array[0] = "Name: ";
			int num = 1;
			GameObject gameObject = this.gameObject;
			array[num] = ((gameObject != null) ? gameObject.ToString() : null);
			array[2] = "\nmodule: ";
			int num2 = 3;
			BaseRaycaster baseRaycaster = this.module;
			array[num2] = ((baseRaycaster != null) ? baseRaycaster.ToString() : null);
			array[4] = "\ndistance: ";
			array[5] = this.distance.ToString();
			array[6] = "\nindex: ";
			array[7] = this.index.ToString();
			array[8] = "\ndepth: ";
			array[9] = this.depth.ToString();
			array[10] = "\nworldNormal: ";
			int num3 = 11;
			Vector3 vector = this.worldNormal;
			array[num3] = vector.ToString();
			array[12] = "\nworldPosition: ";
			int num4 = 13;
			vector = this.worldPosition;
			array[num4] = vector.ToString();
			array[14] = "\nscreenPosition: ";
			int num5 = 15;
			Vector2 vector2 = this.screenPosition;
			array[num5] = vector2.ToString();
			array[16] = "\nmodule.sortOrderPriority: ";
			array[17] = this.module.sortOrderPriority.ToString();
			array[18] = "\nmodule.renderOrderPriority: ";
			array[19] = this.module.renderOrderPriority.ToString();
			array[20] = "\nsortingLayer: ";
			array[21] = this.sortingLayer.ToString();
			array[22] = "\nsortingOrder: ";
			array[23] = this.sortingOrder.ToString();
			return string.Concat(array);
		}

		// Token: 0x04000344 RID: 836
		private GameObject m_GameObject;

		// Token: 0x04000345 RID: 837
		public BaseRaycaster module;

		// Token: 0x04000346 RID: 838
		public float distance;

		// Token: 0x04000347 RID: 839
		public float index;

		// Token: 0x04000348 RID: 840
		public int depth;

		// Token: 0x04000349 RID: 841
		public int sortingGroupID;

		// Token: 0x0400034A RID: 842
		public int sortingGroupOrder;

		// Token: 0x0400034B RID: 843
		public int sortingLayer;

		// Token: 0x0400034C RID: 844
		public int sortingOrder;

		// Token: 0x0400034D RID: 845
		public Vector3 worldPosition;

		// Token: 0x0400034E RID: 846
		public Vector3 worldNormal;

		// Token: 0x0400034F RID: 847
		public Vector2 screenPosition;

		// Token: 0x04000350 RID: 848
		public int displayIndex;
	}
}
