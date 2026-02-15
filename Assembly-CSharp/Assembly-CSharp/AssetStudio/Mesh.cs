using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AssetStudio
{
	// Token: 0x020000F9 RID: 249
	public sealed class Mesh : NamedObject
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000FF88 File Offset: 0x0000E188
		public Mesh(ObjectReader reader)
			: base(reader)
		{
			if (this.version[0] < 3 || (this.version[0] == 3 && this.version[1] < 5))
			{
				this.m_Use16BitIndices = reader.ReadInt32() > 0;
			}
			if (this.version[0] == 2 && this.version[1] <= 5)
			{
				int m_IndexBuffer_size = reader.ReadInt32();
				if (this.m_Use16BitIndices)
				{
					this.m_IndexBuffer = new uint[m_IndexBuffer_size / 2];
					for (int i = 0; i < m_IndexBuffer_size / 2; i++)
					{
						this.m_IndexBuffer[i] = (uint)reader.ReadUInt16();
					}
					reader.AlignStream();
				}
				else
				{
					this.m_IndexBuffer = reader.ReadUInt32Array(m_IndexBuffer_size / 4);
				}
			}
			int m_SubMeshesSize = reader.ReadInt32();
			this.m_SubMeshes = new SubMesh[m_SubMeshesSize];
			for (int j = 0; j < m_SubMeshesSize; j++)
			{
				this.m_SubMeshes[j] = new SubMesh(reader);
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 1))
			{
				this.m_Shapes = new BlendShapeData(reader);
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				this.m_BindPose = reader.ReadMatrixArray();
				this.m_BoneNameHashes = reader.ReadUInt32Array();
				reader.ReadUInt32();
			}
			if (this.version[0] > 2 || (this.version[0] == 2 && this.version[1] >= 6))
			{
				if (this.version[0] >= 2019)
				{
					int m_BonesAABBSize = reader.ReadInt32();
					MinMaxAABB[] m_BonesAABB = new MinMaxAABB[m_BonesAABBSize];
					for (int k = 0; k < m_BonesAABBSize; k++)
					{
						m_BonesAABB[k] = new MinMaxAABB(reader);
					}
					reader.ReadUInt32Array();
				}
				byte m_MeshCompression = reader.ReadByte();
				if (this.version[0] >= 4)
				{
					if (this.version[0] < 5)
					{
						reader.ReadByte();
					}
					reader.ReadBoolean();
					reader.ReadBoolean();
					reader.ReadBoolean();
				}
				reader.AlignStream();
				if (this.version[0] > 2017 || (this.version[0] == 2017 && this.version[1] >= 4) || (this.version[0] == 2017 && this.version[1] == 3 && this.version[2] == 1 && this.buildType.IsPatch) || (this.version[0] == 2017 && this.version[1] == 3 && m_MeshCompression == 0))
				{
					int m_IndexFormat = reader.ReadInt32();
					this.m_Use16BitIndices = m_IndexFormat == 0;
				}
				int m_IndexBuffer_size2 = reader.ReadInt32();
				if (this.m_Use16BitIndices)
				{
					this.m_IndexBuffer = new uint[m_IndexBuffer_size2 / 2];
					for (int l = 0; l < m_IndexBuffer_size2 / 2; l++)
					{
						this.m_IndexBuffer[l] = (uint)reader.ReadUInt16();
					}
					reader.AlignStream();
				}
				else
				{
					this.m_IndexBuffer = reader.ReadUInt32Array(m_IndexBuffer_size2 / 4);
				}
			}
			if (this.version[0] < 3 || (this.version[0] == 3 && this.version[1] < 5))
			{
				this.m_VertexCount = reader.ReadInt32();
				this.m_Vertices = reader.ReadSingleArray(this.m_VertexCount * 3);
				this.m_Skin = new BoneWeights4[reader.ReadInt32()];
				for (int s = 0; s < this.m_Skin.Length; s++)
				{
					this.m_Skin[s] = new BoneWeights4(reader);
				}
				this.m_BindPose = reader.ReadMatrixArray();
				this.m_UV0 = reader.ReadSingleArray(reader.ReadInt32() * 2);
				this.m_UV1 = reader.ReadSingleArray(reader.ReadInt32() * 2);
				if (this.version[0] == 2 && this.version[1] <= 5)
				{
					int m_TangentSpace_size = reader.ReadInt32();
					this.m_Normals = new float[m_TangentSpace_size * 3];
					this.m_Tangents = new float[m_TangentSpace_size * 4];
					for (int v = 0; v < m_TangentSpace_size; v++)
					{
						this.m_Normals[v * 3] = reader.ReadSingle();
						this.m_Normals[v * 3 + 1] = reader.ReadSingle();
						this.m_Normals[v * 3 + 2] = reader.ReadSingle();
						this.m_Tangents[v * 3] = reader.ReadSingle();
						this.m_Tangents[v * 3 + 1] = reader.ReadSingle();
						this.m_Tangents[v * 3 + 2] = reader.ReadSingle();
						this.m_Tangents[v * 3 + 3] = reader.ReadSingle();
					}
				}
				else
				{
					this.m_Tangents = reader.ReadSingleArray(reader.ReadInt32() * 4);
					this.m_Normals = reader.ReadSingleArray(reader.ReadInt32() * 3);
				}
			}
			else
			{
				if (this.version[0] < 2018 || (this.version[0] == 2018 && this.version[1] < 2))
				{
					this.m_Skin = new BoneWeights4[reader.ReadInt32()];
					for (int s2 = 0; s2 < this.m_Skin.Length; s2++)
					{
						this.m_Skin[s2] = new BoneWeights4(reader);
					}
				}
				if (this.version[0] == 3 || (this.version[0] == 4 && this.version[1] <= 2))
				{
					this.m_BindPose = reader.ReadMatrixArray();
				}
				this.m_VertexData = new VertexData(reader);
			}
			if (this.version[0] > 2 || (this.version[0] == 2 && this.version[1] >= 6))
			{
				this.m_CompressedMesh = new CompressedMesh(reader);
			}
			reader.Position += 24L;
			if (this.version[0] < 3 || (this.version[0] == 3 && this.version[1] <= 4))
			{
				int m_Colors_size = reader.ReadInt32();
				this.m_Colors = new float[m_Colors_size * 4];
				for (int v2 = 0; v2 < m_Colors_size * 4; v2++)
				{
					this.m_Colors[v2] = (float)reader.ReadByte() / 255f;
				}
				int m_CollisionTriangles_size = reader.ReadInt32();
				reader.Position += (long)(m_CollisionTriangles_size * 4);
				reader.ReadInt32();
			}
			reader.ReadInt32();
			if (this.version[0] > 2022 || (this.version[0] == 2022 && this.version[1] >= 1))
			{
				reader.ReadInt32();
			}
			if (this.version[0] >= 5)
			{
				reader.ReadUInt8Array();
				reader.AlignStream();
				reader.ReadUInt8Array();
				reader.AlignStream();
			}
			if (this.version[0] > 2018 || (this.version[0] == 2018 && this.version[1] >= 2))
			{
				float[] array = new float[]
				{
					reader.ReadSingle(),
					reader.ReadSingle()
				};
			}
			if (this.version[0] > 2018 || (this.version[0] == 2018 && this.version[1] >= 3))
			{
				reader.AlignStream();
				this.m_StreamData = new StreamingInfo(reader);
			}
			this.ProcessData();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010660 File Offset: 0x0000E860
		private void ProcessData()
		{
			StreamingInfo streamData = this.m_StreamData;
			if (!string.IsNullOrEmpty((streamData != null) ? streamData.path : null) && this.m_VertexData.m_VertexCount > 0U)
			{
				ResourceReader resourceReader = new ResourceReader(this.m_StreamData.path, this.assetsFile, this.m_StreamData.offset, (long)((ulong)this.m_StreamData.size));
				this.m_VertexData.m_DataSize = resourceReader.GetData();
			}
			if (this.version[0] > 3 || (this.version[0] == 3 && this.version[1] >= 5))
			{
				this.ReadVertexData();
			}
			if (this.version[0] > 2 || (this.version[0] == 2 && this.version[1] >= 6))
			{
				this.DecompressCompressedMesh();
			}
			this.GetTriangles();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010728 File Offset: 0x0000E928
		private void ReadVertexData()
		{
			this.m_VertexCount = (int)this.m_VertexData.m_VertexCount;
			for (int chn = 0; chn < this.m_VertexData.m_Channels.Length; chn++)
			{
				ChannelInfo m_Channel = this.m_VertexData.m_Channels[chn];
				if (m_Channel.dimension > 0)
				{
					StreamInfo m_Stream = this.m_VertexData.m_Streams[(int)m_Channel.stream];
					if (new BitArray(new int[] { (int)m_Stream.channelMask }).Get(chn))
					{
						if (this.version[0] < 2018 && chn == 2 && m_Channel.format == 2)
						{
							m_Channel.dimension = 4;
						}
						MeshHelper.VertexFormat vertexFormat = MeshHelper.ToVertexFormat((int)m_Channel.format, this.version);
						int componentByteSize = (int)MeshHelper.GetFormatSize(vertexFormat);
						byte[] componentBytes = new byte[this.m_VertexCount * (int)m_Channel.dimension * componentByteSize];
						for (int v = 0; v < this.m_VertexCount; v++)
						{
							int vertexOffset = (int)(m_Stream.offset + (uint)m_Channel.offset + m_Stream.stride * (uint)v);
							for (int d = 0; d < (int)m_Channel.dimension; d++)
							{
								int componentOffset = vertexOffset + componentByteSize * d;
								Buffer.BlockCopy(this.m_VertexData.m_DataSize, componentOffset, componentBytes, componentByteSize * (v * (int)m_Channel.dimension + d), componentByteSize);
							}
						}
						if (this.reader.Endian == EndianType.BigEndian && componentByteSize > 1)
						{
							for (int i = 0; i < componentBytes.Length / componentByteSize; i++)
							{
								byte[] buff = new byte[componentByteSize];
								Buffer.BlockCopy(componentBytes, i * componentByteSize, buff, 0, componentByteSize);
								buff = buff.Reverse<byte>().ToArray<byte>();
								Buffer.BlockCopy(buff, 0, componentBytes, i * componentByteSize, componentByteSize);
							}
						}
						int[] componentsIntArray = null;
						float[] componentsFloatArray = null;
						if (MeshHelper.IsIntFormat(vertexFormat))
						{
							componentsIntArray = MeshHelper.BytesToIntArray(componentBytes, vertexFormat);
						}
						else
						{
							componentsFloatArray = MeshHelper.BytesToFloatArray(componentBytes, vertexFormat);
						}
						if (this.version[0] >= 2018)
						{
							switch (chn)
							{
							case 0:
								this.m_Vertices = componentsFloatArray;
								break;
							case 1:
								this.m_Normals = componentsFloatArray;
								break;
							case 2:
								this.m_Tangents = componentsFloatArray;
								break;
							case 3:
								this.m_Colors = componentsFloatArray;
								break;
							case 4:
								this.m_UV0 = componentsFloatArray;
								break;
							case 5:
								this.m_UV1 = componentsFloatArray;
								break;
							case 6:
								this.m_UV2 = componentsFloatArray;
								break;
							case 7:
								this.m_UV3 = componentsFloatArray;
								break;
							case 8:
								this.m_UV4 = componentsFloatArray;
								break;
							case 9:
								this.m_UV5 = componentsFloatArray;
								break;
							case 10:
								this.m_UV6 = componentsFloatArray;
								break;
							case 11:
								this.m_UV7 = componentsFloatArray;
								break;
							case 12:
							{
								if (this.m_Skin == null)
								{
									this.InitMSkin();
								}
								for (int j = 0; j < this.m_VertexCount; j++)
								{
									for (int k = 0; k < (int)m_Channel.dimension; k++)
									{
										this.m_Skin[j].weight[k] = componentsFloatArray[j * (int)m_Channel.dimension + k];
									}
								}
								break;
							}
							case 13:
							{
								if (this.m_Skin == null)
								{
									this.InitMSkin();
								}
								for (int l = 0; l < this.m_VertexCount; l++)
								{
									for (int m = 0; m < (int)m_Channel.dimension; m++)
									{
										this.m_Skin[l].boneIndex[m] = componentsIntArray[l * (int)m_Channel.dimension + m];
									}
								}
								break;
							}
							}
						}
						else
						{
							switch (chn)
							{
							case 0:
								this.m_Vertices = componentsFloatArray;
								break;
							case 1:
								this.m_Normals = componentsFloatArray;
								break;
							case 2:
								this.m_Colors = componentsFloatArray;
								break;
							case 3:
								this.m_UV0 = componentsFloatArray;
								break;
							case 4:
								this.m_UV1 = componentsFloatArray;
								break;
							case 5:
								if (this.version[0] >= 5)
								{
									this.m_UV2 = componentsFloatArray;
								}
								else
								{
									this.m_Tangents = componentsFloatArray;
								}
								break;
							case 6:
								this.m_UV3 = componentsFloatArray;
								break;
							case 7:
								this.m_Tangents = componentsFloatArray;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010B34 File Offset: 0x0000ED34
		private void DecompressCompressedMesh()
		{
			if (this.m_CompressedMesh.m_Vertices.m_NumItems > 0U)
			{
				this.m_VertexCount = (int)(this.m_CompressedMesh.m_Vertices.m_NumItems / 3U);
				this.m_Vertices = this.m_CompressedMesh.m_Vertices.UnpackFloats(3, 12, 0, -1);
			}
			if (this.m_CompressedMesh.m_UV.m_NumItems > 0U)
			{
				uint m_UVInfo = this.m_CompressedMesh.m_UVInfo;
				if (m_UVInfo != 0U)
				{
					int uvSrcOffset = 0;
					for (int uv = 0; uv < 8; uv++)
					{
						uint texCoordBits = m_UVInfo >> uv * 4;
						texCoordBits &= 15U;
						if ((texCoordBits & 4U) != 0U)
						{
							int uvDim = (int)(1U + (texCoordBits & 3U));
							float[] m_UV = this.m_CompressedMesh.m_UV.UnpackFloats(uvDim, uvDim * 4, uvSrcOffset, this.m_VertexCount);
							this.SetUV(uv, m_UV);
							uvSrcOffset += uvDim * this.m_VertexCount;
						}
					}
				}
				else
				{
					this.m_UV0 = this.m_CompressedMesh.m_UV.UnpackFloats(2, 8, 0, this.m_VertexCount);
					if ((ulong)this.m_CompressedMesh.m_UV.m_NumItems >= (ulong)((long)(this.m_VertexCount * 4)))
					{
						this.m_UV1 = this.m_CompressedMesh.m_UV.UnpackFloats(2, 8, this.m_VertexCount * 2, this.m_VertexCount);
					}
				}
			}
			if (this.version[0] < 5 && this.m_CompressedMesh.m_BindPoses.m_NumItems > 0U)
			{
				this.m_BindPose = new Matrix4x4[this.m_CompressedMesh.m_BindPoses.m_NumItems / 16U];
				float[] m_BindPoses_Unpacked = this.m_CompressedMesh.m_BindPoses.UnpackFloats(16, 64, 0, -1);
				float[] buffer = new float[16];
				for (int i = 0; i < this.m_BindPose.Length; i++)
				{
					Array.Copy(m_BindPoses_Unpacked, i * 16, buffer, 0, 16);
					this.m_BindPose[i] = new Matrix4x4(buffer);
				}
			}
			if (this.m_CompressedMesh.m_Normals.m_NumItems > 0U)
			{
				float[] normalData = this.m_CompressedMesh.m_Normals.UnpackFloats(2, 8, 0, -1);
				int[] signs = this.m_CompressedMesh.m_NormalSigns.UnpackInts();
				this.m_Normals = new float[this.m_CompressedMesh.m_Normals.m_NumItems / 2U * 3U];
				int j = 0;
				while ((long)j < (long)((ulong)(this.m_CompressedMesh.m_Normals.m_NumItems / 2U)))
				{
					float x3 = normalData[j * 2];
					float y = normalData[j * 2 + 1];
					float zsqr = 1f - x3 * x3 - y * y;
					float z;
					if (zsqr >= 0f)
					{
						z = (float)Math.Sqrt((double)zsqr);
					}
					else
					{
						z = 0f;
						Vector3 normal = new Vector3(x3, y, z);
						normal.Normalize();
						x3 = normal.X;
						y = normal.Y;
						z = normal.Z;
					}
					if (signs[j] == 0)
					{
						z = -z;
					}
					this.m_Normals[j * 3] = x3;
					this.m_Normals[j * 3 + 1] = y;
					this.m_Normals[j * 3 + 2] = z;
					j++;
				}
			}
			if (this.m_CompressedMesh.m_Tangents.m_NumItems > 0U)
			{
				float[] tangentData = this.m_CompressedMesh.m_Tangents.UnpackFloats(2, 8, 0, -1);
				int[] signs2 = this.m_CompressedMesh.m_TangentSigns.UnpackInts();
				this.m_Tangents = new float[this.m_CompressedMesh.m_Tangents.m_NumItems / 2U * 4U];
				int k = 0;
				while ((long)k < (long)((ulong)(this.m_CompressedMesh.m_Tangents.m_NumItems / 2U)))
				{
					float x2 = tangentData[k * 2];
					float y2 = tangentData[k * 2 + 1];
					float zsqr2 = 1f - x2 * x2 - y2 * y2;
					float z2;
					if (zsqr2 >= 0f)
					{
						z2 = (float)Math.Sqrt((double)zsqr2);
					}
					else
					{
						z2 = 0f;
						Vector3 vector3f = new Vector3(x2, y2, z2);
						vector3f.Normalize();
						x2 = vector3f.X;
						y2 = vector3f.Y;
						z2 = vector3f.Z;
					}
					if (signs2[k * 2] == 0)
					{
						z2 = -z2;
					}
					float w = ((signs2[k * 2 + 1] > 0) ? 1f : (-1f));
					this.m_Tangents[k * 4] = x2;
					this.m_Tangents[k * 4 + 1] = y2;
					this.m_Tangents[k * 4 + 2] = z2;
					this.m_Tangents[k * 4 + 3] = w;
					k++;
				}
			}
			if (this.version[0] >= 5 && this.m_CompressedMesh.m_FloatColors.m_NumItems > 0U)
			{
				this.m_Colors = this.m_CompressedMesh.m_FloatColors.UnpackFloats(1, 4, 0, -1);
			}
			if (this.m_CompressedMesh.m_Weights.m_NumItems > 0U)
			{
				int[] weights = this.m_CompressedMesh.m_Weights.UnpackInts();
				int[] boneIndices = this.m_CompressedMesh.m_BoneIndices.UnpackInts();
				this.InitMSkin();
				int bonePos = 0;
				int boneIndexPos = 0;
				int l = 0;
				int sum = 0;
				int m = 0;
				while ((long)m < (long)((ulong)this.m_CompressedMesh.m_Weights.m_NumItems))
				{
					this.m_Skin[bonePos].weight[l] = (float)weights[m] / 31f;
					this.m_Skin[bonePos].boneIndex[l] = boneIndices[boneIndexPos++];
					l++;
					sum += weights[m];
					if (sum >= 31)
					{
						while (l < 4)
						{
							this.m_Skin[bonePos].weight[l] = 0f;
							this.m_Skin[bonePos].boneIndex[l] = 0;
							l++;
						}
						bonePos++;
						l = 0;
						sum = 0;
					}
					else if (l == 3)
					{
						this.m_Skin[bonePos].weight[l] = (float)(31 - sum) / 31f;
						this.m_Skin[bonePos].boneIndex[l] = boneIndices[boneIndexPos++];
						bonePos++;
						l = 0;
						sum = 0;
					}
					m++;
				}
			}
			if (this.m_CompressedMesh.m_Triangles.m_NumItems > 0U)
			{
				this.m_IndexBuffer = Array.ConvertAll<int, uint>(this.m_CompressedMesh.m_Triangles.UnpackInts(), (int x) => (uint)x);
			}
			PackedIntVector colors = this.m_CompressedMesh.m_Colors;
			if (colors != null && colors.m_NumItems > 0U)
			{
				this.m_CompressedMesh.m_Colors.m_NumItems *= 4U;
				PackedIntVector colors2 = this.m_CompressedMesh.m_Colors;
				colors2.m_BitSize /= 4;
				int[] tempColors = this.m_CompressedMesh.m_Colors.UnpackInts();
				this.m_Colors = new float[this.m_CompressedMesh.m_Colors.m_NumItems];
				int v = 0;
				while ((long)v < (long)((ulong)this.m_CompressedMesh.m_Colors.m_NumItems))
				{
					this.m_Colors[v] = (float)tempColors[v] / 255f;
					v++;
				}
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00011218 File Offset: 0x0000F418
		private void GetTriangles()
		{
			foreach (SubMesh m_SubMesh in this.m_SubMeshes)
			{
				uint firstIndex = m_SubMesh.firstByte / 2U;
				if (!this.m_Use16BitIndices)
				{
					firstIndex /= 2U;
				}
				uint indexCount = m_SubMesh.indexCount;
				GfxPrimitiveType topology = m_SubMesh.topology;
				if (topology == GfxPrimitiveType.Triangles)
				{
					int i = 0;
					while ((long)i < (long)((ulong)indexCount))
					{
						checked
						{
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)i))))]);
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)i) + 1UL)))]);
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)i) + 2UL)))]);
						}
						i += 3;
					}
				}
				else if (this.version[0] < 4 || topology == GfxPrimitiveType.TriangleStrip)
				{
					uint triIndex = 0U;
					int j = 0;
					while ((long)j < (long)((ulong)(indexCount - 2U)))
					{
						uint a;
						uint b;
						uint c;
						checked
						{
							a = this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)j))))];
							b = this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)j) + 1UL)))];
							c = this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)j) + 2UL)))];
						}
						if (a != b && a != c && b != c)
						{
							if ((j & 1) == 1)
							{
								this.m_Indices.Add(b);
								this.m_Indices.Add(a);
							}
							else
							{
								this.m_Indices.Add(a);
								this.m_Indices.Add(b);
							}
							this.m_Indices.Add(c);
							triIndex += 3U;
						}
						j++;
					}
					m_SubMesh.indexCount = triIndex;
				}
				else
				{
					if (topology != GfxPrimitiveType.Quads)
					{
						throw new NotSupportedException("Failed getting triangles. Submesh topology is lines or points.");
					}
					int q = 0;
					while ((long)q < (long)((ulong)indexCount))
					{
						checked
						{
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)q))))]);
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)q) + 1UL)))]);
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)q) + 2UL)))]);
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)q))))]);
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)q) + 2UL)))]);
							this.m_Indices.Add(this.m_IndexBuffer[(int)((IntPtr)(unchecked((ulong)firstIndex + (ulong)((long)q) + 3UL)))]);
						}
						q += 4;
					}
					m_SubMesh.indexCount = indexCount / 2U * 3U;
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011488 File Offset: 0x0000F688
		private void InitMSkin()
		{
			this.m_Skin = new BoneWeights4[this.m_VertexCount];
			for (int i = 0; i < this.m_VertexCount; i++)
			{
				this.m_Skin[i] = new BoneWeights4();
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000114C4 File Offset: 0x0000F6C4
		private void SetUV(int uv, float[] m_UV)
		{
			switch (uv)
			{
			case 0:
				this.m_UV0 = m_UV;
				return;
			case 1:
				this.m_UV1 = m_UV;
				return;
			case 2:
				this.m_UV2 = m_UV;
				return;
			case 3:
				this.m_UV3 = m_UV;
				return;
			case 4:
				this.m_UV4 = m_UV;
				return;
			case 5:
				this.m_UV5 = m_UV;
				return;
			case 6:
				this.m_UV6 = m_UV;
				return;
			case 7:
				this.m_UV7 = m_UV;
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00011540 File Offset: 0x0000F740
		public float[] GetUV(int uv)
		{
			switch (uv)
			{
			case 0:
				return this.m_UV0;
			case 1:
				return this.m_UV1;
			case 2:
				return this.m_UV2;
			case 3:
				return this.m_UV3;
			case 4:
				return this.m_UV4;
			case 5:
				return this.m_UV5;
			case 6:
				return this.m_UV6;
			case 7:
				return this.m_UV7;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0400071F RID: 1823
		private bool m_Use16BitIndices = true;

		// Token: 0x04000720 RID: 1824
		public SubMesh[] m_SubMeshes;

		// Token: 0x04000721 RID: 1825
		private uint[] m_IndexBuffer;

		// Token: 0x04000722 RID: 1826
		public BlendShapeData m_Shapes;

		// Token: 0x04000723 RID: 1827
		public Matrix4x4[] m_BindPose;

		// Token: 0x04000724 RID: 1828
		public uint[] m_BoneNameHashes;

		// Token: 0x04000725 RID: 1829
		public int m_VertexCount;

		// Token: 0x04000726 RID: 1830
		public float[] m_Vertices;

		// Token: 0x04000727 RID: 1831
		public BoneWeights4[] m_Skin;

		// Token: 0x04000728 RID: 1832
		public float[] m_Normals;

		// Token: 0x04000729 RID: 1833
		public float[] m_Colors;

		// Token: 0x0400072A RID: 1834
		public float[] m_UV0;

		// Token: 0x0400072B RID: 1835
		public float[] m_UV1;

		// Token: 0x0400072C RID: 1836
		public float[] m_UV2;

		// Token: 0x0400072D RID: 1837
		public float[] m_UV3;

		// Token: 0x0400072E RID: 1838
		public float[] m_UV4;

		// Token: 0x0400072F RID: 1839
		public float[] m_UV5;

		// Token: 0x04000730 RID: 1840
		public float[] m_UV6;

		// Token: 0x04000731 RID: 1841
		public float[] m_UV7;

		// Token: 0x04000732 RID: 1842
		public float[] m_Tangents;

		// Token: 0x04000733 RID: 1843
		private VertexData m_VertexData;

		// Token: 0x04000734 RID: 1844
		private CompressedMesh m_CompressedMesh;

		// Token: 0x04000735 RID: 1845
		private StreamingInfo m_StreamData;

		// Token: 0x04000736 RID: 1846
		public List<uint> m_Indices = new List<uint>();
	}
}
