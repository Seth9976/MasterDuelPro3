using System;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000052 RID: 82
	public struct InstantiationParameters
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008D0C File Offset: 0x00006F0C
		public Vector3 Position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00008D14 File Offset: 0x00006F14
		public Quaternion Rotation
		{
			get
			{
				return this.m_Rotation;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008D1C File Offset: 0x00006F1C
		public Transform Parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00008D24 File Offset: 0x00006F24
		public bool InstantiateInWorldPosition
		{
			get
			{
				return this.m_InstantiateInWorldPosition;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008D2C File Offset: 0x00006F2C
		public bool SetPositionRotation
		{
			get
			{
				return this.m_SetPositionRotation;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008D34 File Offset: 0x00006F34
		public InstantiationParameters(Transform parent, bool instantiateInWorldSpace)
		{
			this.m_Position = Vector3.zero;
			this.m_Rotation = Quaternion.identity;
			this.m_Parent = parent;
			this.m_InstantiateInWorldPosition = instantiateInWorldSpace;
			this.m_SetPositionRotation = false;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008D61 File Offset: 0x00006F61
		public InstantiationParameters(Vector3 position, Quaternion rotation, Transform parent)
		{
			this.m_Position = position;
			this.m_Rotation = rotation;
			this.m_Parent = parent;
			this.m_InstantiateInWorldPosition = false;
			this.m_SetPositionRotation = true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008D88 File Offset: 0x00006F88
		public TObject Instantiate<TObject>(TObject source) where TObject : Object
		{
			TObject result;
			if (this.m_Parent == null)
			{
				if (this.m_SetPositionRotation)
				{
					result = Object.Instantiate<TObject>(source, this.m_Position, this.m_Rotation);
				}
				else
				{
					result = Object.Instantiate<TObject>(source);
				}
			}
			else if (this.m_SetPositionRotation)
			{
				result = Object.Instantiate<TObject>(source, this.m_Position, this.m_Rotation, this.m_Parent);
			}
			else
			{
				result = Object.Instantiate<TObject>(source, this.m_Parent, this.m_InstantiateInWorldPosition);
			}
			return result;
		}

		// Token: 0x040000DF RID: 223
		private Vector3 m_Position;

		// Token: 0x040000E0 RID: 224
		private Quaternion m_Rotation;

		// Token: 0x040000E1 RID: 225
		private Transform m_Parent;

		// Token: 0x040000E2 RID: 226
		private bool m_InstantiateInWorldPosition;

		// Token: 0x040000E3 RID: 227
		private bool m_SetPositionRotation;
	}
}
