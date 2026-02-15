using System;

namespace AssetStudio
{
	// Token: 0x02000144 RID: 324
	public class Transform : Component
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00014118 File Offset: 0x00012318
		public Transform(ObjectReader reader)
			: base(reader)
		{
			this.m_LocalRotation = reader.ReadQuaternion();
			this.m_LocalPosition = reader.ReadVector3();
			this.m_LocalScale = reader.ReadVector3();
			int m_ChildrenCount = reader.ReadInt32();
			this.m_Children = new PPtr<Transform>[m_ChildrenCount];
			for (int i = 0; i < m_ChildrenCount; i++)
			{
				this.m_Children[i] = new PPtr<Transform>(reader);
			}
			this.m_Father = new PPtr<Transform>(reader);
		}

		// Token: 0x04000910 RID: 2320
		public Quaternion m_LocalRotation;

		// Token: 0x04000911 RID: 2321
		public Vector3 m_LocalPosition;

		// Token: 0x04000912 RID: 2322
		public Vector3 m_LocalScale;

		// Token: 0x04000913 RID: 2323
		public PPtr<Transform>[] m_Children;

		// Token: 0x04000914 RID: 2324
		public PPtr<Transform> m_Father;
	}
}
