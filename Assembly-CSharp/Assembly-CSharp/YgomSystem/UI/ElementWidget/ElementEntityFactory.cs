using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x0200068C RID: 1676
	public class ElementEntityFactory : MonoBehaviour
	{
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x0000216A File Offset: 0x0000036A
		public Transform widgetRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x000029CC File Offset: 0x00000BCC
		public int dataCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetEntityByDataIndex(int dataindex)
		{
			return null;
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIndexByEntity(GameObject entity)
		{
			return 0;
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataCount(int dataCount, List<int> templateIdxList = null)
		{
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateItem(int templateIdx)
		{
			return null;
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject AddItem(int templateIdx, int dataIndex)
		{
			return null;
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveItem(int dataIndex)
		{
		}

		// Token: 0x0400300D RID: 12301
		[SerializeField]
		private string m_ELabelTemplate;

		// Token: 0x0400300E RID: 12302
		[SerializeField]
		private string[] m_ELabelAdditionalTemplates;

		// Token: 0x0400300F RID: 12303
		[SerializeField]
		private Transform m_WidgetRoot;

		// Token: 0x04003010 RID: 12304
		[SerializeField]
		private bool m_UseTemplateRoot;

		// Token: 0x04003011 RID: 12305
		private List<GameObject> m_Templates;

		// Token: 0x04003012 RID: 12306
		private Dictionary<GameObject, int> m_EntityToDataIndexTable;

		// Token: 0x04003013 RID: 12307
		private Dictionary<int, GameObject> m_DataIndexToEntityTable;

		// Token: 0x04003014 RID: 12308
		private List<Stack<GameObject>> m_FreeEntityStack;

		// Token: 0x04003015 RID: 12309
		private List<GameObject> m_ActiveEntityList;

		// Token: 0x04003016 RID: 12310
		private int m_DataCount;

		// Token: 0x04003017 RID: 12311
		private List<int> m_TemplateIdxList;

		// Token: 0x04003018 RID: 12312
		public Action<GameObject, int> onCreatedEntityCallback;

		// Token: 0x04003019 RID: 12313
		public Action<GameObject> onActivateEntityCallback;

		// Token: 0x0400301A RID: 12314
		public Action<GameObject, int> onUpdateEntityCallback;

		// Token: 0x0400301B RID: 12315
		public Action<GameObject> onDeactivateEntityCallback;
	}
}
