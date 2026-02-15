using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public struct InputActionProperty : IEquatable<InputActionProperty>, IEquatable<InputAction>, IEquatable<InputActionReference>
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000099C2 File Offset: 0x00007BC2
		public InputAction action
		{
			get
			{
				if (!this.m_UseReference)
				{
					return this.m_Action;
				}
				if (!(this.m_Reference != null))
				{
					return null;
				}
				return this.m_Reference.action;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000099EE File Offset: 0x00007BEE
		public InputActionReference reference
		{
			get
			{
				if (!this.m_UseReference)
				{
					return null;
				}
				return this.m_Reference;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00009A00 File Offset: 0x00007C00
		internal InputAction serializedAction
		{
			get
			{
				return this.m_Action;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00009A08 File Offset: 0x00007C08
		internal InputActionReference serializedReference
		{
			get
			{
				return this.m_Reference;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009A10 File Offset: 0x00007C10
		public InputActionProperty(InputAction action)
		{
			this.m_UseReference = false;
			this.m_Action = action;
			this.m_Reference = null;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009A27 File Offset: 0x00007C27
		public InputActionProperty(InputActionReference reference)
		{
			this.m_UseReference = true;
			this.m_Action = null;
			this.m_Reference = reference;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009A3E File Offset: 0x00007C3E
		public bool Equals(InputActionProperty other)
		{
			return this.m_Reference == other.m_Reference && this.m_UseReference == other.m_UseReference && this.m_Action == other.m_Action;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009A71 File Offset: 0x00007C71
		public bool Equals(InputAction other)
		{
			return this.action == other;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009A7C File Offset: 0x00007C7C
		public bool Equals(InputActionReference other)
		{
			return this.m_Reference == other;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009A8A File Offset: 0x00007C8A
		public override bool Equals(object obj)
		{
			if (this.m_UseReference)
			{
				return this.Equals(obj as InputActionReference);
			}
			return this.Equals(obj as InputAction);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009AAD File Offset: 0x00007CAD
		public override int GetHashCode()
		{
			if (this.m_UseReference)
			{
				if (!(this.m_Reference != null))
				{
					return 0;
				}
				return this.m_Reference.GetHashCode();
			}
			else
			{
				if (this.m_Action == null)
				{
					return 0;
				}
				return this.m_Action.GetHashCode();
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009AE8 File Offset: 0x00007CE8
		public static bool operator ==(InputActionProperty left, InputActionProperty right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009AF2 File Offset: 0x00007CF2
		public static bool operator !=(InputActionProperty left, InputActionProperty right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private bool m_UseReference;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		private InputAction m_Action;

		// Token: 0x04000161 RID: 353
		[SerializeField]
		private InputActionReference m_Reference;
	}
}
