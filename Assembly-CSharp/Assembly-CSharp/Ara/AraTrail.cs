using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace Ara
{
	// Token: 0x0200153C RID: 5436
	[ExecuteInEditMode]
	public class AraTrail : MonoBehaviour
	{
		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06009D9D RID: 40349 RVA: 0x0019AF24 File Offset: 0x00199124
		public Vector3 Velocity
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06009D9E RID: 40350 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float DeltaTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06009D9F RID: 40351 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float FixedDeltaTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06009DA0 RID: 40352 RVA: 0x0000216A File Offset: 0x0000036A
		public Mesh mesh
		{
			get
			{
				return null;
			}
		}

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06009DA1 RID: 40353 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06009DA2 RID: 40354 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onUpdatePoints
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06009DA3 RID: 40355 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnValidate()
		{
		}

		// Token: 0x06009DA4 RID: 40356 RVA: 0x0000216D File Offset: 0x0000036D
		public void Awake()
		{
		}

		// Token: 0x06009DA5 RID: 40357 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06009DA6 RID: 40358 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009DA7 RID: 40359 RVA: 0x0000216D File Offset: 0x0000036D
		private void AttachToCameraRendering()
		{
		}

		// Token: 0x06009DA8 RID: 40360 RVA: 0x0000216D File Offset: 0x0000036D
		private void DetachFromCameraRendering()
		{
		}

		// Token: 0x06009DA9 RID: 40361 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x06009DAA RID: 40362 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateVelocity()
		{
		}

		// Token: 0x06009DAB RID: 40363 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06009DAC RID: 40364 RVA: 0x0000216D File Offset: 0x0000036D
		private void EmissionStep(float time)
		{
		}

		// Token: 0x06009DAD RID: 40365 RVA: 0x0000216D File Offset: 0x0000036D
		private void Warmup()
		{
		}

		// Token: 0x06009DAE RID: 40366 RVA: 0x0000216D File Offset: 0x0000036D
		private void PhysicsStep(float timestep)
		{
		}

		// Token: 0x06009DAF RID: 40367 RVA: 0x0000216D File Offset: 0x0000036D
		private void FixedUpdate()
		{
		}

		// Token: 0x06009DB0 RID: 40368 RVA: 0x0000216D File Offset: 0x0000036D
		public void EmitPoint(Vector3 position)
		{
		}

		// Token: 0x06009DB1 RID: 40369 RVA: 0x0000216D File Offset: 0x0000036D
		private void SnapLastPointToTransform()
		{
		}

		// Token: 0x06009DB2 RID: 40370 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePointsLifecycle()
		{
		}

		// Token: 0x06009DB3 RID: 40371 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearMeshData()
		{
		}

		// Token: 0x06009DB4 RID: 40372 RVA: 0x0000216D File Offset: 0x0000036D
		private void CommitMeshData()
		{
		}

		// Token: 0x06009DB5 RID: 40373 RVA: 0x0000216D File Offset: 0x0000036D
		private void RenderMesh(Camera cam)
		{
		}

		// Token: 0x06009DB6 RID: 40374 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetLenght(List<AraTrail.Point> input)
		{
			return 0f;
		}

		// Token: 0x06009DB7 RID: 40375 RVA: 0x0000216A File Offset: 0x0000036A
		private List<AraTrail.Point> GetRenderablePoints(int start, int end)
		{
			return null;
		}

		// Token: 0x06009DB8 RID: 40376 RVA: 0x0019AF3C File Offset: 0x0019913C
		private AraTrail.CurveFrame InitializeCurveFrame(Vector3 point, Vector3 nextPoint)
		{
			return default(AraTrail.CurveFrame);
		}

		// Token: 0x06009DB9 RID: 40377 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTrailMesh(Camera cam)
		{
		}

		// Token: 0x06009DBA RID: 40378 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateSegmentMesh(int start, int end, Vector3 localCamPosition)
		{
		}

		// Token: 0x0400DD33 RID: 56627
		public const float epsilon = 1E-05f;

		// Token: 0x0400DD34 RID: 56628
		public Space space;

		// Token: 0x0400DD35 RID: 56629
		public AraTrail.Timescale timescale;

		// Token: 0x0400DD36 RID: 56630
		public AraTrail.TrailAlignment alignment;

		// Token: 0x0400DD37 RID: 56631
		public AraTrail.TrailSorting sorting;

		// Token: 0x0400DD38 RID: 56632
		public float thickness;

		// Token: 0x0400DD39 RID: 56633
		public int smoothness;

		// Token: 0x0400DD3A RID: 56634
		public bool highQualityCorners;

		// Token: 0x0400DD3B RID: 56635
		public int cornerRoundness;

		// Token: 0x0400DD3C RID: 56636
		public AnimationCurve thicknessOverLenght;

		// Token: 0x0400DD3D RID: 56637
		public Gradient colorOverLenght;

		// Token: 0x0400DD3E RID: 56638
		public AnimationCurve thicknessOverTime;

		// Token: 0x0400DD3F RID: 56639
		public Gradient colorOverTime;

		// Token: 0x0400DD40 RID: 56640
		public bool emit;

		// Token: 0x0400DD41 RID: 56641
		public float initialThickness;

		// Token: 0x0400DD42 RID: 56642
		public Color initialColor;

		// Token: 0x0400DD43 RID: 56643
		public Vector3 initialVelocity;

		// Token: 0x0400DD44 RID: 56644
		public float timeInterval;

		// Token: 0x0400DD45 RID: 56645
		public float minDistance;

		// Token: 0x0400DD46 RID: 56646
		public float time;

		// Token: 0x0400DD47 RID: 56647
		public bool enablePhysics;

		// Token: 0x0400DD48 RID: 56648
		public float warmup;

		// Token: 0x0400DD49 RID: 56649
		public Vector3 gravity;

		// Token: 0x0400DD4A RID: 56650
		public float inertia;

		// Token: 0x0400DD4B RID: 56651
		public float velocitySmoothing;

		// Token: 0x0400DD4C RID: 56652
		public float damping;

		// Token: 0x0400DD4D RID: 56653
		public Material[] materials;

		// Token: 0x0400DD4E RID: 56654
		public ShadowCastingMode castShadows;

		// Token: 0x0400DD4F RID: 56655
		public bool receiveShadows;

		// Token: 0x0400DD50 RID: 56656
		public bool useLightProbes;

		// Token: 0x0400DD51 RID: 56657
		public bool quadMapping;

		// Token: 0x0400DD52 RID: 56658
		public AraTrail.TextureMode textureMode;

		// Token: 0x0400DD53 RID: 56659
		public float uvFactor;

		// Token: 0x0400DD54 RID: 56660
		public float uvWidthFactor;

		// Token: 0x0400DD55 RID: 56661
		public float tileAnchor;

		// Token: 0x0400DD56 RID: 56662
		[HideInInspector]
		public List<AraTrail.Point> points;

		// Token: 0x0400DD57 RID: 56663
		private List<AraTrail.Point> renderablePoints;

		// Token: 0x0400DD58 RID: 56664
		private List<int> discontinuities;

		// Token: 0x0400DD59 RID: 56665
		private Mesh mesh_;

		// Token: 0x0400DD5A RID: 56666
		private Vector3 velocity;

		// Token: 0x0400DD5B RID: 56667
		private Vector3 prevPosition;

		// Token: 0x0400DD5C RID: 56668
		private float accumTime;

		// Token: 0x0400DD5D RID: 56669
		private List<Vector3> vertices;

		// Token: 0x0400DD5E RID: 56670
		private List<Vector3> normals;

		// Token: 0x0400DD5F RID: 56671
		private List<Vector4> tangents;

		// Token: 0x0400DD60 RID: 56672
		private List<Vector4> uvs;

		// Token: 0x0400DD61 RID: 56673
		private List<Color> vertColors;

		// Token: 0x0400DD62 RID: 56674
		private List<int> tris;

		// Token: 0x0400DD63 RID: 56675
		private Action<ScriptableRenderContext, Camera> renderCallback;

		// Token: 0x0200153D RID: 5437
		public enum TrailAlignment
		{
			// Token: 0x0400DD65 RID: 56677
			View,
			// Token: 0x0400DD66 RID: 56678
			Velocity,
			// Token: 0x0400DD67 RID: 56679
			Local
		}

		// Token: 0x0200153E RID: 5438
		public enum TrailSorting
		{
			// Token: 0x0400DD69 RID: 56681
			OlderOnTop,
			// Token: 0x0400DD6A RID: 56682
			NewerOnTop
		}

		// Token: 0x0200153F RID: 5439
		public enum Timescale
		{
			// Token: 0x0400DD6C RID: 56684
			Normal,
			// Token: 0x0400DD6D RID: 56685
			Unscaled
		}

		// Token: 0x02001540 RID: 5440
		public enum TextureMode
		{
			// Token: 0x0400DD6F RID: 56687
			Stretch,
			// Token: 0x0400DD70 RID: 56688
			Tile
		}

		// Token: 0x02001541 RID: 5441
		public struct CurveFrame
		{
			// Token: 0x06009DBC RID: 40380 RVA: 0x0019AF54 File Offset: 0x00199154
			public Vector3 Transport(Vector3 newTangent, Vector3 newPosition)
			{
				return default(Vector3);
			}

			// Token: 0x0400DD71 RID: 56689
			public Vector3 position;

			// Token: 0x0400DD72 RID: 56690
			public Vector3 normal;

			// Token: 0x0400DD73 RID: 56691
			public Vector3 bitangent;

			// Token: 0x0400DD74 RID: 56692
			public Vector3 tangent;
		}

		// Token: 0x02001542 RID: 5442
		public struct Point
		{
			// Token: 0x06009DBD RID: 40381 RVA: 0x000029C5 File Offset: 0x00000BC5
			private static float CatmullRom(float p0, float p1, float p2, float p3, float t)
			{
				return 0f;
			}

			// Token: 0x06009DBE RID: 40382 RVA: 0x0019AF6C File Offset: 0x0019916C
			private static Color CatmullRomColor(Color p0, Color p1, Color p2, Color p3, float t)
			{
				return default(Color);
			}

			// Token: 0x06009DBF RID: 40383 RVA: 0x0019AF84 File Offset: 0x00199184
			private static Vector3 CatmullRom3D(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
			{
				return default(Vector3);
			}

			// Token: 0x06009DC0 RID: 40384 RVA: 0x0019AF9C File Offset: 0x0019919C
			public static AraTrail.Point Interpolate(AraTrail.Point a, AraTrail.Point b, AraTrail.Point c, AraTrail.Point d, float t)
			{
				return default(AraTrail.Point);
			}

			// Token: 0x06009DC1 RID: 40385 RVA: 0x0019AFB4 File Offset: 0x001991B4
			public static AraTrail.Point operator +(AraTrail.Point p1, AraTrail.Point p2)
			{
				return default(AraTrail.Point);
			}

			// Token: 0x06009DC2 RID: 40386 RVA: 0x0019AFCC File Offset: 0x001991CC
			public static AraTrail.Point operator -(AraTrail.Point p1, AraTrail.Point p2)
			{
				return default(AraTrail.Point);
			}

			// Token: 0x0400DD75 RID: 56693
			public Vector3 position;

			// Token: 0x0400DD76 RID: 56694
			public Vector3 velocity;

			// Token: 0x0400DD77 RID: 56695
			public Vector3 tangent;

			// Token: 0x0400DD78 RID: 56696
			public Vector3 normal;

			// Token: 0x0400DD79 RID: 56697
			public Color color;

			// Token: 0x0400DD7A RID: 56698
			public float thickness;

			// Token: 0x0400DD7B RID: 56699
			public float life;

			// Token: 0x0400DD7C RID: 56700
			public bool discontinuous;
		}
	}
}
