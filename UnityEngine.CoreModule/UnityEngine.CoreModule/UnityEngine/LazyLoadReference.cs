using System;

namespace UnityEngine
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	public struct LazyLoadReference<T> where T : Object
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00023990 File Offset: 0x00021B90
		public bool isSet
		{
			get
			{
				return this.m_InstanceID != 0;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0002399C File Offset: 0x00021B9C
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x000239DC File Offset: 0x00021BDC
		public T asset
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				T t;
				if (flag)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)Object.ForceLoadFromInstanceID(this.m_InstanceID));
				}
				return t;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_InstanceID = 0;
				}
				else
				{
					bool flag2 = !Object.IsPersistent(value);
					if (flag2)
					{
						throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
					}
					this.m_InstanceID = value.GetInstanceID();
				}
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00023A38 File Offset: 0x00021C38
		public static implicit operator LazyLoadReference<T>(T asset)
		{
			return new LazyLoadReference<T>
			{
				asset = asset
			};
		}

		// Token: 0x04000673 RID: 1651
		private const int kInstanceID_None = 0;

		// Token: 0x04000674 RID: 1652
		[SerializeField]
		private int m_InstanceID;
	}
}
