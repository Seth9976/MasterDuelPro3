using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000B0 RID: 176
	[RequireComponent(typeof(CinemachineTargetGroup))]
	[ExecuteAlways]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/api/Cinemachine.GroupWeightManipulator.html")]
	public class GroupWeightManipulator : MonoBehaviour
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00016F41 File Offset: 0x00015141
		private void Start()
		{
			this.m_group = base.GetComponent<CinemachineTargetGroup>();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00016F50 File Offset: 0x00015150
		private void OnValidate()
		{
			this.m_Weight0 = Mathf.Max(0f, this.m_Weight0);
			this.m_Weight1 = Mathf.Max(0f, this.m_Weight1);
			this.m_Weight2 = Mathf.Max(0f, this.m_Weight2);
			this.m_Weight3 = Mathf.Max(0f, this.m_Weight3);
			this.m_Weight4 = Mathf.Max(0f, this.m_Weight4);
			this.m_Weight5 = Mathf.Max(0f, this.m_Weight5);
			this.m_Weight6 = Mathf.Max(0f, this.m_Weight6);
			this.m_Weight7 = Mathf.Max(0f, this.m_Weight7);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001700D File Offset: 0x0001520D
		private void Update()
		{
			if (this.m_group != null)
			{
				this.UpdateWeights();
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00017024 File Offset: 0x00015224
		private void UpdateWeights()
		{
			CinemachineTargetGroup.Target[] targets = this.m_group.m_Targets;
			int last = targets.Length - 1;
			if (last < 0)
			{
				return;
			}
			targets[0].weight = this.m_Weight0;
			if (last < 1)
			{
				return;
			}
			targets[1].weight = this.m_Weight1;
			if (last < 2)
			{
				return;
			}
			targets[2].weight = this.m_Weight2;
			if (last < 3)
			{
				return;
			}
			targets[3].weight = this.m_Weight3;
			if (last < 4)
			{
				return;
			}
			targets[4].weight = this.m_Weight4;
			if (last < 5)
			{
				return;
			}
			targets[5].weight = this.m_Weight5;
			if (last < 6)
			{
				return;
			}
			targets[6].weight = this.m_Weight6;
			if (last < 7)
			{
				return;
			}
			targets[7].weight = this.m_Weight7;
		}

		// Token: 0x0400039A RID: 922
		[Tooltip("The weight of the group member at index 0")]
		public float m_Weight0 = 1f;

		// Token: 0x0400039B RID: 923
		[Tooltip("The weight of the group member at index 1")]
		public float m_Weight1 = 1f;

		// Token: 0x0400039C RID: 924
		[Tooltip("The weight of the group member at index 2")]
		public float m_Weight2 = 1f;

		// Token: 0x0400039D RID: 925
		[Tooltip("The weight of the group member at index 3")]
		public float m_Weight3 = 1f;

		// Token: 0x0400039E RID: 926
		[Tooltip("The weight of the group member at index 4")]
		public float m_Weight4 = 1f;

		// Token: 0x0400039F RID: 927
		[Tooltip("The weight of the group member at index 5")]
		public float m_Weight5 = 1f;

		// Token: 0x040003A0 RID: 928
		[Tooltip("The weight of the group member at index 6")]
		public float m_Weight6 = 1f;

		// Token: 0x040003A1 RID: 929
		[Tooltip("The weight of the group member at index 7")]
		public float m_Weight7 = 1f;

		// Token: 0x040003A2 RID: 930
		private CinemachineTargetGroup m_group;
	}
}
