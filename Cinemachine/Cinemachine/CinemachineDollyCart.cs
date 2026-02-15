using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000023 RID: 35
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineDollyCart.html")]
	public class CinemachineDollyCart : MonoBehaviour
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00006C10 File Offset: 0x00004E10
		private void FixedUpdate()
		{
			if (this.m_UpdateMethod == CinemachineDollyCart.UpdateMethod.FixedUpdate)
			{
				this.SetCartPosition(this.m_Position + this.m_Speed * Time.deltaTime);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006C34 File Offset: 0x00004E34
		private void Update()
		{
			float speed = (Application.isPlaying ? this.m_Speed : 0f);
			if (this.m_UpdateMethod == CinemachineDollyCart.UpdateMethod.Update)
			{
				this.SetCartPosition(this.m_Position + speed * Time.deltaTime);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006C72 File Offset: 0x00004E72
		private void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				this.SetCartPosition(this.m_Position);
				return;
			}
			if (this.m_UpdateMethod == CinemachineDollyCart.UpdateMethod.LateUpdate)
			{
				this.SetCartPosition(this.m_Position + this.m_Speed * Time.deltaTime);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006CAC File Offset: 0x00004EAC
		private void SetCartPosition(float distanceAlongPath)
		{
			if (this.m_Path != null)
			{
				this.m_Position = this.m_Path.StandardizeUnit(distanceAlongPath, this.m_PositionUnits);
				Vector3 pos = this.m_Path.EvaluatePositionAtUnit(this.m_Position, this.m_PositionUnits);
				Quaternion rot = this.m_Path.EvaluateOrientationAtUnit(this.m_Position, this.m_PositionUnits);
				base.transform.ConservativeSetPositionAndRotation(pos, rot);
			}
		}

		// Token: 0x040000AE RID: 174
		[Tooltip("The path to follow")]
		public CinemachinePathBase m_Path;

		// Token: 0x040000AF RID: 175
		[Tooltip("When to move the cart, if Velocity is non-zero")]
		public CinemachineDollyCart.UpdateMethod m_UpdateMethod;

		// Token: 0x040000B0 RID: 176
		[Tooltip("How to interpret the Path Position.  If set to Path Units, values are as follows: 0 represents the first waypoint on the path, 1 is the second, and so on.  Values in-between are points on the path in between the waypoints.  If set to Distance, then Path Position represents distance along the path.")]
		public CinemachinePathBase.PositionUnits m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;

		// Token: 0x040000B1 RID: 177
		[Tooltip("Move the cart with this speed along the path.  The value is interpreted according to the Position Units setting.")]
		[FormerlySerializedAs("m_Velocity")]
		public float m_Speed;

		// Token: 0x040000B2 RID: 178
		[Tooltip("The position along the path at which the cart will be placed.  This can be animated directly or, if the velocity is non-zero, will be updated automatically.  The value is interpreted according to the Position Units setting.")]
		[FormerlySerializedAs("m_CurrentDistance")]
		public float m_Position;

		// Token: 0x02000024 RID: 36
		public enum UpdateMethod
		{
			// Token: 0x040000B4 RID: 180
			Update,
			// Token: 0x040000B5 RID: 181
			FixedUpdate,
			// Token: 0x040000B6 RID: 182
			LateUpdate
		}
	}
}
