using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000C3 RID: 195
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[SaveDuringPlay]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineImpulseSourceOverview.html")]
	public class CinemachineImpulseSource : MonoBehaviour
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x000189B0 File Offset: 0x00016BB0
		private void OnValidate()
		{
			this.m_ImpulseDefinition.OnValidate();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000189C0 File Offset: 0x00016BC0
		private void Reset()
		{
			this.m_ImpulseDefinition = new CinemachineImpulseDefinition
			{
				m_ImpulseChannel = 1,
				m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Bump,
				m_CustomImpulseShape = new AnimationCurve(),
				m_ImpulseDuration = 0.2f,
				m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Uniform,
				m_DissipationDistance = 100f,
				m_DissipationRate = 0.25f,
				m_PropagationSpeed = 343f
			};
			this.m_DefaultVelocity = Vector3.down;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00018A2F File Offset: 0x00016C2F
		public void GenerateImpulseAtPositionWithVelocity(Vector3 position, Vector3 velocity)
		{
			if (this.m_ImpulseDefinition != null)
			{
				this.m_ImpulseDefinition.CreateEvent(position, velocity);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00018A46 File Offset: 0x00016C46
		public void GenerateImpulseWithVelocity(Vector3 velocity)
		{
			this.GenerateImpulseAtPositionWithVelocity(base.transform.position, velocity);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00018A5A File Offset: 0x00016C5A
		public void GenerateImpulseWithForce(float force)
		{
			this.GenerateImpulseAtPositionWithVelocity(base.transform.position, this.m_DefaultVelocity * force);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00018A79 File Offset: 0x00016C79
		public void GenerateImpulse()
		{
			this.GenerateImpulseWithVelocity(this.m_DefaultVelocity);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00018A87 File Offset: 0x00016C87
		public void GenerateImpulseAt(Vector3 position, Vector3 velocity)
		{
			this.GenerateImpulseAtPositionWithVelocity(position, velocity);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00018A91 File Offset: 0x00016C91
		public void GenerateImpulse(Vector3 velocity)
		{
			this.GenerateImpulseWithVelocity(velocity);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00018A9A File Offset: 0x00016C9A
		public void GenerateImpulse(float force)
		{
			this.GenerateImpulseWithForce(force);
		}

		// Token: 0x040003FF RID: 1023
		public CinemachineImpulseDefinition m_ImpulseDefinition = new CinemachineImpulseDefinition();

		// Token: 0x04000400 RID: 1024
		[Header("Default Invocation")]
		[Tooltip("The default direction and force of the Impulse Signal in the absense of any specified overrides.  Overrides can be specified by calling the appropriate GenerateImpulse method in the API.")]
		public Vector3 m_DefaultVelocity = Vector3.down;
	}
}
