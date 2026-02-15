using System;

namespace AssetStudio
{
	// Token: 0x020000F6 RID: 246
	public class BlendShapeData
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public BlendShapeData(ObjectReader reader)
		{
			int[] version = reader.version;
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				int numVerts = reader.ReadInt32();
				this.vertices = new BlendShapeVertex[numVerts];
				for (int i = 0; i < numVerts; i++)
				{
					this.vertices[i] = new BlendShapeVertex(reader);
				}
				int numShapes = reader.ReadInt32();
				this.shapes = new MeshBlendShape[numShapes];
				for (int j = 0; j < numShapes; j++)
				{
					this.shapes[j] = new MeshBlendShape(reader);
				}
				int numChannels = reader.ReadInt32();
				this.channels = new MeshBlendShapeChannel[numChannels];
				for (int k = 0; k < numChannels; k++)
				{
					this.channels[k] = new MeshBlendShapeChannel(reader);
				}
				this.fullWeights = reader.ReadSingleArray();
				return;
			}
			int m_ShapesSize = reader.ReadInt32();
			MeshBlendShape[] m_Shapes = new MeshBlendShape[m_ShapesSize];
			for (int l = 0; l < m_ShapesSize; l++)
			{
				m_Shapes[l] = new MeshBlendShape(reader);
			}
			reader.AlignStream();
			int m_ShapeVerticesSize = reader.ReadInt32();
			BlendShapeVertex[] m_ShapeVertices = new BlendShapeVertex[m_ShapeVerticesSize];
			for (int m = 0; m < m_ShapeVerticesSize; m++)
			{
				m_ShapeVertices[m] = new BlendShapeVertex(reader);
			}
		}

		// Token: 0x0400070C RID: 1804
		public BlendShapeVertex[] vertices;

		// Token: 0x0400070D RID: 1805
		public MeshBlendShape[] shapes;

		// Token: 0x0400070E RID: 1806
		public MeshBlendShapeChannel[] channels;

		// Token: 0x0400070F RID: 1807
		public float[] fullWeights;
	}
}
