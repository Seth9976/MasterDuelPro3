using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CD RID: 973
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct StyleDataRef<T> : IEquatable<StyleDataRef<T>> where T : struct, IEquatable<T>, IStyleDataGroup<T>
	{
		// Token: 0x06001CC5 RID: 7365 RVA: 0x0006B0D8 File Offset: 0x000692D8
		public StyleDataRef<T> Acquire()
		{
			this.m_Ref.Acquire();
			return this;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006B0FC File Offset: 0x000692FC
		public void Release()
		{
			this.m_Ref.Release();
			this.m_Ref = null;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0006B114 File Offset: 0x00069314
		public void CopyFrom(StyleDataRef<T> other)
		{
			bool flag = this.m_Ref.refCount == 1;
			if (flag)
			{
				this.m_Ref.value.CopyFrom(ref other.m_Ref.value);
			}
			else
			{
				this.m_Ref.Release();
				this.m_Ref = other.m_Ref;
				this.m_Ref.Acquire();
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0006B180 File Offset: 0x00069380
		public readonly ref T Read()
		{
			return ref this.m_Ref.value;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0006B190 File Offset: 0x00069390
		public ref T Write()
		{
			bool flag = this.m_Ref.refCount == 1;
			ref T ptr;
			if (flag)
			{
				ptr = ref this.m_Ref.value;
			}
			else
			{
				StyleDataRef<T>.RefCounted oldRef = this.m_Ref;
				this.m_Ref = this.m_Ref.Copy();
				oldRef.Release();
				ptr = ref this.m_Ref.value;
			}
			return ref ptr;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0006B1EC File Offset: 0x000693EC
		public static StyleDataRef<T> Create()
		{
			return new StyleDataRef<T>
			{
				m_Ref = new StyleDataRef<T>.RefCounted()
			};
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0006B214 File Offset: 0x00069414
		public override int GetHashCode()
		{
			return (this.m_Ref != null) ? this.m_Ref.value.GetHashCode() : 0;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0006B248 File Offset: 0x00069448
		public static bool operator ==(StyleDataRef<T> lhs, StyleDataRef<T> rhs)
		{
			return lhs.m_Ref == rhs.m_Ref || lhs.m_Ref.value.Equals(rhs.m_Ref.value);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0006B28C File Offset: 0x0006948C
		public bool Equals(StyleDataRef<T> other)
		{
			return other == this;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0006B2AC File Offset: 0x000694AC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleDataRef<T>)
			{
				StyleDataRef<T> other = (StyleDataRef<T>)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0006B2D8 File Offset: 0x000694D8
		public bool ReferenceEquals(StyleDataRef<T> other)
		{
			return this.m_Ref == other.m_Ref;
		}

		// Token: 0x04000C8C RID: 3212
		private StyleDataRef<T>.RefCounted m_Ref;

		// Token: 0x020003CE RID: 974
		private class RefCounted
		{
			// Token: 0x17000810 RID: 2064
			// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0006B2F8 File Offset: 0x000694F8
			public int refCount
			{
				get
				{
					return this.m_RefCount;
				}
			}

			// Token: 0x06001CD1 RID: 7377 RVA: 0x0006B300 File Offset: 0x00069500
			public RefCounted()
			{
				this.m_RefCount = 1;
				this.m_Id = (StyleDataRef<T>.RefCounted.m_NextId += 1U);
			}

			// Token: 0x06001CD2 RID: 7378 RVA: 0x0006B324 File Offset: 0x00069524
			public void Acquire()
			{
				this.m_RefCount++;
			}

			// Token: 0x06001CD3 RID: 7379 RVA: 0x0006B334 File Offset: 0x00069534
			public void Release()
			{
				this.m_RefCount--;
			}

			// Token: 0x06001CD4 RID: 7380 RVA: 0x0006B348 File Offset: 0x00069548
			public StyleDataRef<T>.RefCounted Copy()
			{
				return new StyleDataRef<T>.RefCounted
				{
					value = this.value.Copy()
				};
			}

			// Token: 0x04000C8D RID: 3213
			private static uint m_NextId = 1U;

			// Token: 0x04000C8E RID: 3214
			private int m_RefCount;

			// Token: 0x04000C8F RID: 3215
			private readonly uint m_Id;

			// Token: 0x04000C90 RID: 3216
			public T value;
		}
	}
}
