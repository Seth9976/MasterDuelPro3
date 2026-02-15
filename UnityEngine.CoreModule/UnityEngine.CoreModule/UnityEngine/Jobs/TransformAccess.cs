using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x020001F9 RID: 505
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformAccess.bindings.h")]
	public struct TransformAccess
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00029718 File Offset: 0x00027918
		public Vector3 position
		{
			get
			{
				Vector3 p;
				TransformAccess.GetPosition(ref this, out p);
				return p;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00029734 File Offset: 0x00027934
		public Quaternion rotation
		{
			get
			{
				Quaternion r;
				TransformAccess.GetRotation(ref this, out r);
				return r;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00029750 File Offset: 0x00027950
		public Vector3 localScale
		{
			get
			{
				Vector3 s;
				TransformAccess.GetLocalScale(ref this, out s);
				return s;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0002976C File Offset: 0x0002796C
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 i;
				TransformAccess.GetLocalToWorldMatrix(ref this, out i);
				return i;
			}
		}

		// Token: 0x060013A8 RID: 5032
		[NativeMethod(Name = "TransformAccessBindings::GetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPosition(ref TransformAccess access, out Vector3 p);

		// Token: 0x060013A9 RID: 5033
		[NativeMethod(Name = "TransformAccessBindings::GetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotation(ref TransformAccess access, out Quaternion r);

		// Token: 0x060013AA RID: 5034
		[NativeMethod(Name = "TransformAccessBindings::GetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScale(ref TransformAccess access, out Vector3 r);

		// Token: 0x060013AB RID: 5035
		[NativeMethod(Name = "TransformAccessBindings::GetLocalToWorldMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrix(ref TransformAccess access, out Matrix4x4 m);

		// Token: 0x04000724 RID: 1828
		private IntPtr hierarchy;

		// Token: 0x04000725 RID: 1829
		private int index;
	}
}
