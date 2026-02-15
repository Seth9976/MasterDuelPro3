using System;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public class Collision
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_Flipped ? this.m_Header.m_RelativeVelocity : (-this.m_Header.m_RelativeVelocity);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002077 File Offset: 0x00000277
		public Component body
		{
			get
			{
				return this.m_Flipped ? this.m_Header.body : this.m_Header.otherBody;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002099 File Offset: 0x00000299
		public Collider collider
		{
			get
			{
				return this.m_Flipped ? this.m_Pair.collider : this.m_Pair.otherCollider;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020BC File Offset: 0x000002BC
		public GameObject gameObject
		{
			get
			{
				return (this.body != null) ? this.body.gameObject : this.collider.gameObject;
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		internal bool Flipped
		{
			set
			{
				this.m_Flipped = value;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FE File Offset: 0x000002FE
		public Collision()
		{
			this.m_Header = default(ContactPairHeader);
			this.m_Pair = default(ContactPair);
			this.m_Flipped = false;
			this.m_LegacyContacts = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000338
		internal Collision(in ContactPairHeader header, in ContactPair pair, bool flipped)
		{
			this.m_LegacyContacts = new ContactPoint[pair.m_NbPoints];
			pair.ExtractContactsArray(this.m_LegacyContacts, flipped);
			this.m_Header = header;
			this.m_Pair = pair;
			this.m_Flipped = flipped;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002192 File Offset: 0x00000392
		internal void Reuse(in ContactPairHeader header, in ContactPair pair)
		{
			this.m_Header = header;
			this.m_Pair = pair;
			this.m_LegacyContacts = null;
			this.m_Flipped = false;
		}

		// Token: 0x04000007 RID: 7
		private ContactPairHeader m_Header;

		// Token: 0x04000008 RID: 8
		private ContactPair m_Pair;

		// Token: 0x04000009 RID: 9
		private bool m_Flipped;

		// Token: 0x0400000A RID: 10
		private ContactPoint[] m_LegacyContacts = null;
	}
}
