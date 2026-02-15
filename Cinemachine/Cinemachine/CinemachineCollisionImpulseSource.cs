using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000B1 RID: 177
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[SaveDuringPlay]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineCollisionImpulseSource.html")]
	public class CinemachineCollisionImpulseSource : CinemachineImpulseSource
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00017167 File Offset: 0x00015367
		private void Start()
		{
			this.mRigidBody = base.GetComponent<Rigidbody>();
			this.mRigidBody2D = base.GetComponent<Rigidbody2D>();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000429A File Offset: 0x0000249A
		private void OnEnable()
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00017181 File Offset: 0x00015381
		private void OnCollisionEnter(Collision c)
		{
			this.GenerateImpactEvent(c.collider, c.relativeVelocity);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00017195 File Offset: 0x00015395
		private void OnTriggerEnter(Collider c)
		{
			this.GenerateImpactEvent(c, Vector3.zero);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000171A4 File Offset: 0x000153A4
		private float GetMassAndVelocity(Collider other, ref Vector3 vel)
		{
			bool getVelocity = vel == Vector3.zero;
			float mass = 1f;
			if (this.m_ScaleImpactWithMass || this.m_ScaleImpactWithSpeed || this.m_UseImpactDirection)
			{
				if (this.mRigidBody != null)
				{
					if (this.m_ScaleImpactWithMass)
					{
						mass *= this.mRigidBody.mass;
					}
					if (getVelocity)
					{
						vel = -this.mRigidBody.linearVelocity;
					}
				}
				Rigidbody rb = ((other != null) ? other.attachedRigidbody : null);
				if (rb != null)
				{
					if (this.m_ScaleImpactWithMass)
					{
						mass *= rb.mass;
					}
					if (getVelocity)
					{
						vel += rb.linearVelocity;
					}
				}
			}
			return mass;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00017268 File Offset: 0x00015468
		private void GenerateImpactEvent(Collider other, Vector3 vel)
		{
			if (!base.enabled)
			{
				return;
			}
			if (other != null)
			{
				int layer = other.gameObject.layer;
				if (((1 << layer) & this.m_LayerMask) == 0)
				{
					return;
				}
				if (this.m_IgnoreTag.Length != 0 && other.CompareTag(this.m_IgnoreTag))
				{
					return;
				}
			}
			float mass = this.GetMassAndVelocity(other, ref vel);
			if (this.m_ScaleImpactWithSpeed)
			{
				mass *= Mathf.Sqrt(vel.magnitude);
			}
			Vector3 dir = this.m_DefaultVelocity;
			if (this.m_UseImpactDirection && !vel.AlmostZero())
			{
				dir = -vel.normalized * dir.magnitude;
			}
			base.GenerateImpulseWithVelocity(dir * mass);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00017322 File Offset: 0x00015522
		private void OnCollisionEnter2D(Collision2D c)
		{
			this.GenerateImpactEvent2D(c.collider, c.relativeVelocity);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001733B File Offset: 0x0001553B
		private void OnTriggerEnter2D(Collider2D c)
		{
			this.GenerateImpactEvent2D(c, Vector3.zero);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001734C File Offset: 0x0001554C
		private float GetMassAndVelocity2D(Collider2D other2d, ref Vector3 vel)
		{
			bool getVelocity = vel == Vector3.zero;
			float mass = 1f;
			if (this.m_ScaleImpactWithMass || this.m_ScaleImpactWithSpeed || this.m_UseImpactDirection)
			{
				if (this.mRigidBody2D != null)
				{
					if (this.m_ScaleImpactWithMass)
					{
						mass *= this.mRigidBody2D.mass;
					}
					if (getVelocity)
					{
						vel = -this.mRigidBody2D.linearVelocity;
					}
				}
				Rigidbody2D rb2d = ((other2d != null) ? other2d.attachedRigidbody : null);
				if (rb2d != null)
				{
					if (this.m_ScaleImpactWithMass)
					{
						mass *= rb2d.mass;
					}
					if (getVelocity)
					{
						Vector3 v = rb2d.linearVelocity;
						vel += v;
					}
				}
			}
			return mass;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001741C File Offset: 0x0001561C
		private void GenerateImpactEvent2D(Collider2D other2d, Vector3 vel)
		{
			if (!base.enabled)
			{
				return;
			}
			if (other2d != null)
			{
				int layer = other2d.gameObject.layer;
				if (((1 << layer) & this.m_LayerMask) == 0)
				{
					return;
				}
				if (this.m_IgnoreTag.Length != 0 && other2d.CompareTag(this.m_IgnoreTag))
				{
					return;
				}
			}
			float mass = this.GetMassAndVelocity2D(other2d, ref vel);
			if (this.m_ScaleImpactWithSpeed)
			{
				mass *= Mathf.Sqrt(vel.magnitude);
			}
			Vector3 dir = this.m_DefaultVelocity;
			if (this.m_UseImpactDirection && !vel.AlmostZero())
			{
				dir = -vel.normalized * dir.magnitude;
			}
			base.GenerateImpulseWithVelocity(dir * mass);
		}

		// Token: 0x040003A3 RID: 931
		[Header("Trigger Object Filter")]
		[Tooltip("Only collisions with objects on these layers will generate Impulse events")]
		public LayerMask m_LayerMask = 1;

		// Token: 0x040003A4 RID: 932
		[TagField]
		[Tooltip("No Impulse evemts will be generated for collisions with objects having these tags")]
		public string m_IgnoreTag = string.Empty;

		// Token: 0x040003A5 RID: 933
		[Header("How To Generate The Impulse")]
		[Tooltip("If checked, signal direction will be affected by the direction of impact")]
		public bool m_UseImpactDirection;

		// Token: 0x040003A6 RID: 934
		[Tooltip("If checked, signal amplitude will be multiplied by the mass of the impacting object")]
		public bool m_ScaleImpactWithMass;

		// Token: 0x040003A7 RID: 935
		[Tooltip("If checked, signal amplitude will be multiplied by the speed of the impacting object")]
		public bool m_ScaleImpactWithSpeed;

		// Token: 0x040003A8 RID: 936
		private Rigidbody mRigidBody;

		// Token: 0x040003A9 RID: 937
		private Rigidbody2D mRigidBody2D;
	}
}
