using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x0200000F RID: 15
	public readonly struct HierarchyPropertyUnmanaged<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IEquatable<HierarchyPropertyUnmanaged<T>>, IHierarchyProperty<T> where T : struct, ValueType
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000028E8 File Offset: 0x00000AE8
		internal HierarchyPropertyUnmanaged(Hierarchy hierarchy, in HierarchyPropertyId property)
		{
			bool flag = hierarchy == null;
			if (flag)
			{
				throw new ArgumentNullException("hierarchy");
			}
			bool flag2 = (in property) == HierarchyPropertyId.Null;
			if (flag2)
			{
				throw new ArgumentException("property");
			}
			this.m_Hierarchy = hierarchy;
			this.m_Property = property;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002938 File Offset: 0x00000B38
		public unsafe void SetValue(in HierarchyNode node, T value)
		{
			bool flag = this.m_Hierarchy == null;
			if (flag)
			{
				throw new NullReferenceException("Hierarchy reference has not been set.");
			}
			bool flag2 = !this.m_Hierarchy.IsCreated;
			if (flag2)
			{
				throw new InvalidOperationException("Hierarchy has been disposed.");
			}
			this.m_Hierarchy.SetPropertyRaw(in this.m_Property, in node, (void*)(&value), sizeof(T));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000299C File Offset: 0x00000B9C
		public unsafe T GetValue(in HierarchyNode node)
		{
			bool flag = this.m_Hierarchy == null;
			if (flag)
			{
				throw new NullReferenceException("Hierarchy reference has not been set.");
			}
			bool flag2 = !this.m_Hierarchy.IsCreated;
			if (flag2)
			{
				throw new InvalidOperationException("Hierarchy has been disposed.");
			}
			int size;
			void* ptr = this.m_Hierarchy.GetPropertyRaw(in this.m_Property, in node, out size);
			bool flag3 = ptr == null || size != sizeof(T);
			T t;
			if (flag3)
			{
				t = default(T);
			}
			else
			{
				t = *UnsafeUtility.AsRef<T>(ptr);
			}
			return t;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A2E File Offset: 0x00000C2E
		[ExcludeFromDocs]
		public bool Equals(HierarchyPropertyUnmanaged<T> other)
		{
			return this.m_Hierarchy == other.m_Hierarchy && (in this.m_Property) == (in other.m_Property);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A53 File Offset: 0x00000C53
		[ExcludeFromDocs]
		public override string ToString()
		{
			return this.m_Property.ToString();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A68 File Offset: 0x00000C68
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyPropertyUnmanaged<T>)
			{
				HierarchyPropertyUnmanaged<T> property = (HierarchyPropertyUnmanaged<T>)obj;
				flag = this.Equals(property);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A8E File Offset: 0x00000C8E
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return this.m_Property.GetHashCode();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AA1 File Offset: 0x00000CA1
		T IHierarchyProperty<T>.GetValue(in HierarchyNode node)
		{
			return this.GetValue(in node);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AAA File Offset: 0x00000CAA
		void IHierarchyProperty<T>.SetValue(in HierarchyNode node, T value)
		{
			this.SetValue(in node, value);
		}

		// Token: 0x04000022 RID: 34
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x04000023 RID: 35
		internal readonly HierarchyPropertyId m_Property;
	}
}
