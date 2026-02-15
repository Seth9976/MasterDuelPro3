using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200024D RID: 589
	[NativeHeader("Runtime/Export/SceneManager/Scene.bindings.h")]
	[Serializable]
	public struct Scene
	{
		// Token: 0x060014AF RID: 5295
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidInternal(int sceneHandle);

		// Token: 0x060014B0 RID: 5296 RVA: 0x0002BB9C File Offset: 0x00029D9C
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static string GetGUIDInternal(int sceneHandle)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Scene.GetGUIDInternal_Injected(sceneHandle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060014B1 RID: 5297
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsLoadedInternal(int sceneHandle);

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0002BBCC File Offset: 0x00029DCC
		public int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0002BBE4 File Offset: 0x00029DE4
		internal string guid
		{
			get
			{
				return Scene.GetGUIDInternal(this.handle);
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0002BC04 File Offset: 0x00029E04
		public bool IsValid()
		{
			return Scene.IsValidInternal(this.handle);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0002BC24 File Offset: 0x00029E24
		public bool isLoaded
		{
			get
			{
				return Scene.GetIsLoadedInternal(this.handle);
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0002BC44 File Offset: 0x00029E44
		public static bool operator ==(Scene lhs, Scene rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0002BC68 File Offset: 0x00029E68
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0002BC80 File Offset: 0x00029E80
		public override bool Equals(object other)
		{
			bool flag = !(other is Scene);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Scene rhs = (Scene)other;
				flag2 = this.handle == rhs.handle;
			}
			return flag2;
		}

		// Token: 0x060014B9 RID: 5305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGUIDInternal_Injected(int sceneHandle, out ManagedSpanWrapper ret);

		// Token: 0x040007B3 RID: 1971
		[HideInInspector]
		[SerializeField]
		private int m_Handle;
	}
}
