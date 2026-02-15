using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200033F RID: 831
	public struct SubMeshDescriptor
	{
		// Token: 0x06001639 RID: 5689 RVA: 0x0002EA60 File Offset: 0x0002CC60
		public SubMeshDescriptor(int indexStart, int indexCount, MeshTopology topology = MeshTopology.Triangles)
		{
			this.indexStart = indexStart;
			this.indexCount = indexCount;
			this.topology = topology;
			this.bounds = default(Bounds);
			this.baseVertex = 0;
			this.firstVertex = 0;
			this.vertexCount = 0;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0002EAAE File Offset: 0x0002CCAE
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0002EAB6 File Offset: 0x0002CCB6
		public Bounds bounds { readonly get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0002EABF File Offset: 0x0002CCBF
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0002EAC7 File Offset: 0x0002CCC7
		public MeshTopology topology { readonly get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
		public int indexStart { readonly get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0002EAE1 File Offset: 0x0002CCE1
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x0002EAE9 File Offset: 0x0002CCE9
		public int indexCount { readonly get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0002EAF2 File Offset: 0x0002CCF2
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x0002EAFA File Offset: 0x0002CCFA
		public int baseVertex { readonly get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0002EB03 File Offset: 0x0002CD03
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x0002EB0B File Offset: 0x0002CD0B
		public int firstVertex { readonly get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0002EB14 File Offset: 0x0002CD14
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x0002EB1C File Offset: 0x0002CD1C
		public int vertexCount { readonly get; set; }

		// Token: 0x06001648 RID: 5704 RVA: 0x0002EB28 File Offset: 0x0002CD28
		public override string ToString()
		{
			return string.Format("(topo={0} indices={1},{2} vertices={3},{4} basevtx={5} bounds={6})", new object[] { this.topology, this.indexStart, this.indexCount, this.firstVertex, this.vertexCount, this.baseVertex, this.bounds });
		}
	}
}
