using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009B RID: 155
	public class DebugShapes
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public static DebugShapes instance
		{
			get
			{
				if (DebugShapes.s_Instance == null)
				{
					DebugShapes.s_Instance = new DebugShapes();
				}
				return DebugShapes.s_Instance;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		private void BuildSphere(ref Mesh outputMesh, float radius, uint longSubdiv, uint latSubdiv)
		{
			outputMesh.Clear();
			Vector3[] vertices = new Vector3[(longSubdiv + 1U) * latSubdiv + 2U];
			float _pi = 3.1415927f;
			float _2pi = _pi * 2f;
			vertices[0] = Vector3.up * radius;
			int lat = 0;
			while ((long)lat < (long)((ulong)latSubdiv))
			{
				float num = _pi * (float)(lat + 1) / (latSubdiv + 1U);
				float sin = Mathf.Sin(num);
				float cos = Mathf.Cos(num);
				int lon = 0;
				while ((long)lon <= (long)((ulong)longSubdiv))
				{
					float num2 = _2pi * (float)(((long)lon == (long)((ulong)longSubdiv)) ? 0 : lon) / longSubdiv;
					float sin2 = Mathf.Sin(num2);
					float cos2 = Mathf.Cos(num2);
					vertices[(int)(checked((IntPtr)(unchecked((long)lon + (long)lat * (long)((ulong)(longSubdiv + 1U)) + 1L))))] = new Vector3(sin * cos2, cos, sin * sin2) * radius;
					lon++;
				}
				lat++;
			}
			vertices[vertices.Length - 1] = Vector3.up * -radius;
			Vector3[] normals = new Vector3[vertices.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				normals[i] = vertices[i].normalized;
			}
			Vector2[] uvs = new Vector2[vertices.Length];
			uvs[0] = Vector2.up;
			uvs[uvs.Length - 1] = Vector2.zero;
			int lat2 = 0;
			while ((long)lat2 < (long)((ulong)latSubdiv))
			{
				int lon2 = 0;
				while ((long)lon2 <= (long)((ulong)longSubdiv))
				{
					uvs[(int)(checked((IntPtr)(unchecked((long)lon2 + (long)lat2 * (long)((ulong)(longSubdiv + 1U)) + 1L))))] = new Vector2((float)lon2 / longSubdiv, 1f - (float)(lat2 + 1) / (latSubdiv + 1U));
					lon2++;
				}
				lat2++;
			}
			int[] triangles = new int[(longSubdiv * 2U + (latSubdiv - 1U) * longSubdiv * 2U) * 3U];
			int j = 0;
			int lon3 = 0;
			while ((long)lon3 < (long)((ulong)longSubdiv))
			{
				triangles[j++] = lon3 + 2;
				triangles[j++] = lon3 + 1;
				triangles[j++] = 0;
				lon3++;
			}
			for (uint lat3 = 0U; lat3 < latSubdiv - 1U; lat3 += 1U)
			{
				for (uint lon4 = 0U; lon4 < longSubdiv; lon4 += 1U)
				{
					uint current = lon4 + lat3 * (longSubdiv + 1U) + 1U;
					uint next = current + longSubdiv + 1U;
					triangles[j++] = (int)current;
					triangles[j++] = (int)(current + 1U);
					triangles[j++] = (int)(next + 1U);
					triangles[j++] = (int)current;
					triangles[j++] = (int)(next + 1U);
					triangles[j++] = (int)next;
				}
			}
			int lon5 = 0;
			while ((long)lon5 < (long)((ulong)longSubdiv))
			{
				triangles[j++] = vertices.Length - 1;
				triangles[j++] = vertices.Length - (lon5 + 2) - 1;
				triangles[j++] = vertices.Length - (lon5 + 1) - 1;
				lon5++;
			}
			outputMesh.vertices = vertices;
			outputMesh.normals = normals;
			outputMesh.uv = uvs;
			outputMesh.triangles = triangles;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		private void BuildBox(ref Mesh outputMesh, float length, float width, float height)
		{
			outputMesh.Clear();
			Vector3 p0 = new Vector3(-length * 0.5f, -width * 0.5f, height * 0.5f);
			Vector3 p = new Vector3(length * 0.5f, -width * 0.5f, height * 0.5f);
			Vector3 p2 = new Vector3(length * 0.5f, -width * 0.5f, -height * 0.5f);
			Vector3 p3 = new Vector3(-length * 0.5f, -width * 0.5f, -height * 0.5f);
			Vector3 p4 = new Vector3(-length * 0.5f, width * 0.5f, height * 0.5f);
			Vector3 p5 = new Vector3(length * 0.5f, width * 0.5f, height * 0.5f);
			Vector3 p6 = new Vector3(length * 0.5f, width * 0.5f, -height * 0.5f);
			Vector3 p7 = new Vector3(-length * 0.5f, width * 0.5f, -height * 0.5f);
			Vector3[] vertices = new Vector3[]
			{
				p0, p, p2, p3, p7, p4, p0, p3, p4, p5,
				p, p0, p6, p7, p3, p2, p5, p6, p2, p,
				p7, p6, p5, p4
			};
			Vector3 up = Vector3.up;
			Vector3 down = Vector3.down;
			Vector3 front = Vector3.forward;
			Vector3 back = Vector3.back;
			Vector3 left = Vector3.left;
			Vector3 right = Vector3.right;
			Vector3[] normales = new Vector3[]
			{
				down, down, down, down, left, left, left, left, front, front,
				front, front, back, back, back, back, right, right, right, right,
				up, up, up, up
			};
			Vector2 _0 = new Vector2(0f, 0f);
			Vector2 _ = new Vector2(1f, 0f);
			Vector2 _2 = new Vector2(0f, 1f);
			Vector2 _3 = new Vector2(1f, 1f);
			Vector2[] uvs = new Vector2[]
			{
				_3, _2, _0, _, _3, _2, _0, _, _3, _2,
				_0, _, _3, _2, _0, _, _3, _2, _0, _,
				_3, _2, _0, _
			};
			int[] triangles = new int[]
			{
				3, 1, 0, 3, 2, 1, 7, 5, 4, 7,
				6, 5, 11, 9, 8, 11, 10, 9, 15, 13,
				12, 15, 14, 13, 19, 17, 16, 19, 18, 17,
				23, 21, 20, 23, 22, 21
			};
			outputMesh.vertices = vertices;
			outputMesh.normals = normales;
			outputMesh.uv = uvs;
			outputMesh.triangles = triangles;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000E03C File Offset: 0x0000C23C
		private void BuildCone(ref Mesh outputMesh, float height, float topRadius, float bottomRadius, int nbSides)
		{
			outputMesh.Clear();
			int nbVerticesCap = nbSides + 1;
			Vector3[] vertices = new Vector3[nbVerticesCap + nbVerticesCap + nbSides * 2 + 2];
			int vert = 0;
			float _2pi = 6.2831855f;
			vertices[vert++] = new Vector3(0f, 0f, 0f);
			while (vert <= nbSides)
			{
				float rad = (float)vert / (float)nbSides * _2pi;
				vertices[vert] = new Vector3(Mathf.Sin(rad) * bottomRadius, Mathf.Cos(rad) * bottomRadius, 0f);
				vert++;
			}
			vertices[vert++] = new Vector3(0f, 0f, height);
			while (vert <= nbSides * 2 + 1)
			{
				float rad2 = (float)(vert - nbSides - 1) / (float)nbSides * _2pi;
				vertices[vert] = new Vector3(Mathf.Sin(rad2) * topRadius, Mathf.Cos(rad2) * topRadius, height);
				vert++;
			}
			int v = 0;
			while (vert <= vertices.Length - 4)
			{
				float rad3 = (float)v / (float)nbSides * _2pi;
				vertices[vert] = new Vector3(Mathf.Sin(rad3) * topRadius, Mathf.Cos(rad3) * topRadius, height);
				vertices[vert + 1] = new Vector3(Mathf.Sin(rad3) * bottomRadius, Mathf.Cos(rad3) * bottomRadius, 0f);
				vert += 2;
				v++;
			}
			vertices[vert] = vertices[nbSides * 2 + 2];
			vertices[vert + 1] = vertices[nbSides * 2 + 3];
			Vector3[] normales = new Vector3[vertices.Length];
			vert = 0;
			while (vert <= nbSides)
			{
				normales[vert++] = new Vector3(0f, 0f, -1f);
			}
			while (vert <= nbSides * 2 + 1)
			{
				normales[vert++] = new Vector3(0f, 0f, 1f);
			}
			v = 0;
			while (vert <= vertices.Length - 4)
			{
				float num = (float)v / (float)nbSides * _2pi;
				float cos = Mathf.Cos(num);
				float sin = Mathf.Sin(num);
				normales[vert] = new Vector3(sin, cos, 0f);
				normales[vert + 1] = normales[vert];
				vert += 2;
				v++;
			}
			normales[vert] = normales[nbSides * 2 + 2];
			normales[vert + 1] = normales[nbSides * 2 + 3];
			Vector2[] uvs = new Vector2[vertices.Length];
			int u = 0;
			uvs[u++] = new Vector2(0.5f, 0.5f);
			while (u <= nbSides)
			{
				float rad4 = (float)u / (float)nbSides * _2pi;
				uvs[u] = new Vector2(Mathf.Cos(rad4) * 0.5f + 0.5f, Mathf.Sin(rad4) * 0.5f + 0.5f);
				u++;
			}
			uvs[u++] = new Vector2(0.5f, 0.5f);
			while (u <= nbSides * 2 + 1)
			{
				float rad5 = (float)u / (float)nbSides * _2pi;
				uvs[u] = new Vector2(Mathf.Cos(rad5) * 0.5f + 0.5f, Mathf.Sin(rad5) * 0.5f + 0.5f);
				u++;
			}
			int u_sides = 0;
			while (u <= uvs.Length - 4)
			{
				float t = (float)u_sides / (float)nbSides;
				uvs[u] = new Vector3(t, 1f);
				uvs[u + 1] = new Vector3(t, 0f);
				u += 2;
				u_sides++;
			}
			uvs[u] = new Vector2(1f, 1f);
			uvs[u + 1] = new Vector2(1f, 0f);
			int nbTriangles = nbSides + nbSides + nbSides * 2;
			int[] triangles = new int[nbTriangles * 3 + 3];
			int tri = 0;
			int i = 0;
			while (tri < nbSides - 1)
			{
				triangles[i] = 0;
				triangles[i + 1] = tri + 1;
				triangles[i + 2] = tri + 2;
				tri++;
				i += 3;
			}
			triangles[i] = 0;
			triangles[i + 1] = tri + 1;
			triangles[i + 2] = 1;
			tri++;
			i += 3;
			while (tri < nbSides * 2)
			{
				triangles[i] = tri + 2;
				triangles[i + 1] = tri + 1;
				triangles[i + 2] = nbVerticesCap;
				tri++;
				i += 3;
			}
			triangles[i] = nbVerticesCap + 1;
			triangles[i + 1] = tri + 1;
			triangles[i + 2] = nbVerticesCap;
			tri++;
			i += 3;
			tri++;
			while (tri <= nbTriangles)
			{
				triangles[i] = tri + 2;
				triangles[i + 1] = tri + 1;
				triangles[i + 2] = tri;
				tri++;
				i += 3;
				triangles[i] = tri + 1;
				triangles[i + 1] = tri + 2;
				triangles[i + 2] = tri;
				tri++;
				i += 3;
			}
			outputMesh.vertices = vertices;
			outputMesh.normals = normales;
			outputMesh.uv = uvs;
			outputMesh.triangles = triangles;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000E55C File Offset: 0x0000C75C
		private void BuildPyramid(ref Mesh outputMesh, float width, float height, float depth)
		{
			outputMesh.Clear();
			Vector3[] vertices = new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(-width / 2f, height / 2f, depth),
				new Vector3(width / 2f, height / 2f, depth),
				new Vector3(0f, 0f, 0f),
				new Vector3(width / 2f, height / 2f, depth),
				new Vector3(width / 2f, -height / 2f, depth),
				new Vector3(0f, 0f, 0f),
				new Vector3(width / 2f, -height / 2f, depth),
				new Vector3(-width / 2f, -height / 2f, depth),
				new Vector3(0f, 0f, 0f),
				new Vector3(-width / 2f, -height / 2f, depth),
				new Vector3(-width / 2f, height / 2f, depth),
				new Vector3(-width / 2f, height / 2f, depth),
				new Vector3(-width / 2f, -height / 2f, depth),
				new Vector3(width / 2f, -height / 2f, depth),
				new Vector3(width / 2f, height / 2f, depth)
			};
			Vector3[] normals = new Vector3[vertices.Length];
			Vector2[] uvs = new Vector2[vertices.Length];
			int[] triangles = new int[18];
			for (int idx = 0; idx < 12; idx++)
			{
				triangles[idx] = idx;
			}
			triangles[12] = 12;
			triangles[13] = 13;
			triangles[14] = 14;
			triangles[15] = 12;
			triangles[16] = 14;
			triangles[17] = 15;
			outputMesh.vertices = vertices;
			outputMesh.normals = normals;
			outputMesh.uv = uvs;
			outputMesh.triangles = triangles;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		private void BuildShapes()
		{
			this.m_sphereMesh = new Mesh();
			this.BuildSphere(ref this.m_sphereMesh, 1f, 24U, 16U);
			this.m_boxMesh = new Mesh();
			this.BuildBox(ref this.m_boxMesh, 1f, 1f, 1f);
			this.m_coneMesh = new Mesh();
			this.BuildCone(ref this.m_coneMesh, 1f, 1f, 0f, 16);
			this.m_pyramidMesh = new Mesh();
			this.BuildPyramid(ref this.m_pyramidMesh, 1f, 1f, 1f);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000E865 File Offset: 0x0000CA65
		private void RebuildResources()
		{
			if (this.m_sphereMesh == null || this.m_boxMesh == null || this.m_coneMesh == null || this.m_pyramidMesh == null)
			{
				this.BuildShapes();
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000E8A5 File Offset: 0x0000CAA5
		public Mesh RequestSphereMesh()
		{
			this.RebuildResources();
			return this.m_sphereMesh;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public Mesh BuildCustomSphereMesh(float radius, uint longSubdiv, uint latSubdiv)
		{
			Mesh sphereMesh = new Mesh();
			this.BuildSphere(ref sphereMesh, radius, longSubdiv, latSubdiv);
			return sphereMesh;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000E8D3 File Offset: 0x0000CAD3
		public Mesh RequestBoxMesh()
		{
			this.RebuildResources();
			return this.m_boxMesh;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000E8E1 File Offset: 0x0000CAE1
		public Mesh RequestConeMesh()
		{
			this.RebuildResources();
			return this.m_coneMesh;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000E8EF File Offset: 0x0000CAEF
		public Mesh RequestPyramidMesh()
		{
			this.RebuildResources();
			return this.m_pyramidMesh;
		}

		// Token: 0x04000204 RID: 516
		private static DebugShapes s_Instance;

		// Token: 0x04000205 RID: 517
		private Mesh m_sphereMesh;

		// Token: 0x04000206 RID: 518
		private Mesh m_boxMesh;

		// Token: 0x04000207 RID: 519
		private Mesh m_coneMesh;

		// Token: 0x04000208 RID: 520
		private Mesh m_pyramidMesh;
	}
}
