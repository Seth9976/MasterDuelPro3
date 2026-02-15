using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000531 RID: 1329
	internal class BasicNodePool<T> : LinkedPool<BasicNode<T>>
	{
		// Token: 0x060024BC RID: 9404 RVA: 0x0008BF7B File Offset: 0x0008A17B
		private static void Reset(BasicNode<T> node)
		{
			node.next = null;
			node.data = default(T);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0008BF94 File Offset: 0x0008A194
		private static BasicNode<T> Create()
		{
			return new BasicNode<T>();
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0008BFAB File Offset: 0x0008A1AB
		public BasicNodePool()
			: base(new Func<BasicNode<T>>(BasicNodePool<T>.Create), new Action<BasicNode<T>>(BasicNodePool<T>.Reset), 10000)
		{
		}
	}
}
