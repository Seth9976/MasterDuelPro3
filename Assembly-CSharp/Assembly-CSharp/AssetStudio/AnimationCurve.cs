using System;

namespace AssetStudio
{
	// Token: 0x0200009B RID: 155
	public class AnimationCurve<T>
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000C614 File Offset: 0x0000A814
		public AnimationCurve(ObjectReader reader, Func<T> readerFunc)
		{
			int[] version = reader.version;
			int numCurves = reader.ReadInt32();
			this.m_Curve = new Keyframe<T>[numCurves];
			for (int i = 0; i < numCurves; i++)
			{
				this.m_Curve[i] = new Keyframe<T>(reader, readerFunc);
			}
			this.m_PreInfinity = reader.ReadInt32();
			this.m_PostInfinity = reader.ReadInt32();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 3))
			{
				this.m_RotationOrder = reader.ReadInt32();
			}
		}

		// Token: 0x04000541 RID: 1345
		public Keyframe<T>[] m_Curve;

		// Token: 0x04000542 RID: 1346
		public int m_PreInfinity;

		// Token: 0x04000543 RID: 1347
		public int m_PostInfinity;

		// Token: 0x04000544 RID: 1348
		public int m_RotationOrder;
	}
}
