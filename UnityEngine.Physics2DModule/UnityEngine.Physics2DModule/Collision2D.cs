using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Collision2D
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000280C File Offset: 0x00000A0C
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002830 File Offset: 0x00000A30
		public Rigidbody2D rigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Rigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002854 File Offset: 0x00000A54
		public GameObject gameObject
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.gameObject : this.collider.gameObject;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000288C File Offset: 0x00000A8C
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x04000023 RID: 35
		internal int m_Collider;

		// Token: 0x04000024 RID: 36
		internal int m_OtherCollider;

		// Token: 0x04000025 RID: 37
		internal int m_Rigidbody;

		// Token: 0x04000026 RID: 38
		internal int m_OtherRigidbody;

		// Token: 0x04000027 RID: 39
		internal Vector2 m_RelativeVelocity;

		// Token: 0x04000028 RID: 40
		internal int m_Enabled;

		// Token: 0x04000029 RID: 41
		internal int m_ContactCount;

		// Token: 0x0400002A RID: 42
		internal ContactPoint2D[] m_ReusedContacts;

		// Token: 0x0400002B RID: 43
		internal ContactPoint2D[] m_LegacyContacts;
	}
}
