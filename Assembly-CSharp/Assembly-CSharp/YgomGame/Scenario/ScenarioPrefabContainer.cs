using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009DC RID: 2524
	public class ScenarioPrefabContainer : ScenarioContainerBase
	{
		// Token: 0x06004971 RID: 18801 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isBgLabel(string label)
		{
			return false;
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioPrefabContainer(ElementObjectManager backUIEom, ElementObjectManager overUIEom)
			: base(null)
		{
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x0000216D File Offset: 0x0000036D
		public void CreatePrefabObject(string label, string path, int locateSlot, Action<GameObject> onComplete, bool isOverUI = false)
		{
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetCreatedGameObject(string label)
		{
			return null;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject[] FindBGObjects()
		{
			return null;
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool DestroyPrefabObject(string label)
		{
			return false;
		}

		// Token: 0x0400874E RID: 34638
		private readonly int k_LocatorLen;

		// Token: 0x0400874F RID: 34639
		private readonly string k_ELabelLocatorFormat;

		// Token: 0x04008750 RID: 34640
		private readonly string k_PrefLabelBg;

		// Token: 0x04008751 RID: 34641
		private readonly Dictionary<string, GameObject> m_CreatedObjectMap;

		// Token: 0x04008752 RID: 34642
		private readonly Transform[] m_BackUILocators;

		// Token: 0x04008753 RID: 34643
		private readonly Transform[] m_OverUILocators;

		// Token: 0x04008754 RID: 34644
		public readonly ElementObjectManager backUIEom;

		// Token: 0x04008755 RID: 34645
		public readonly ElementObjectManager overUIEom;

		// Token: 0x04008756 RID: 34646
		public Action<string, GameObject> onCreateCallback;
	}
}
