using System;
using System.Runtime.CompilerServices;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B2 RID: 690
	internal static class UIRUtility
	{
		// Token: 0x060012A5 RID: 4773 RVA: 0x0004D86C File Offset: 0x0004BA6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ShapeWindingIsClockwise(int maskDepth, int stencilRef)
		{
			Debug.Assert(maskDepth == stencilRef || maskDepth == stencilRef + 1);
			return maskDepth == stencilRef;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004D898 File Offset: 0x0004BA98
		public static void GetVerticesTransformInfo(VisualElement ve, out Matrix4x4 transform)
		{
			bool flag = RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || ve.renderChainData.isGroupTransform;
			if (flag)
			{
				transform = Matrix4x4.identity;
			}
			else
			{
				bool flag2 = ve.renderChainData.boneTransformAncestor != null;
				if (flag2)
				{
					bool worldTransformScaleZero = ve.renderChainData.boneTransformAncestor.renderChainData.worldTransformScaleZero;
					if (worldTransformScaleZero)
					{
						UIRUtility.ComputeTransformMatrix(ve, ve.renderChainData.boneTransformAncestor, out transform);
					}
					else
					{
						VisualElement.MultiplyMatrix34(ve.renderChainData.boneTransformAncestor.worldTransformInverse, ve.worldTransformRef, out transform);
					}
				}
				else
				{
					bool flag3 = ve.renderChainData.groupTransformAncestor != null;
					if (flag3)
					{
						bool worldTransformScaleZero2 = ve.renderChainData.groupTransformAncestor.renderChainData.worldTransformScaleZero;
						if (worldTransformScaleZero2)
						{
							UIRUtility.ComputeTransformMatrix(ve, ve.renderChainData.groupTransformAncestor, out transform);
						}
						else
						{
							VisualElement.MultiplyMatrix34(ve.renderChainData.groupTransformAncestor.worldTransformInverse, ve.worldTransformRef, out transform);
						}
					}
					else
					{
						transform = ve.worldTransform;
					}
				}
			}
			BaseVisualElementPanel elementPanel = ve.elementPanel;
			bool flag4 = elementPanel != null && elementPanel.isFlat;
			if (flag4)
			{
				transform.m22 = 1f;
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0004D9D8 File Offset: 0x0004BBD8
		internal static void ComputeTransformMatrix(VisualElement ve, VisualElement ancestor, out Matrix4x4 result)
		{
			ve.GetPivotedMatrixWithLayout(out result);
			VisualElement currentAncestor = ve.hierarchy.parent;
			bool flag = currentAncestor == null || ancestor == currentAncestor;
			if (!flag)
			{
				Matrix4x4 temp = default(Matrix4x4);
				bool destIsTemp = true;
				do
				{
					Matrix4x4 ancestorMatrix;
					currentAncestor.GetPivotedMatrixWithLayout(out ancestorMatrix);
					bool flag2 = destIsTemp;
					if (flag2)
					{
						VisualElement.MultiplyMatrix34(ref ancestorMatrix, ref result, out temp);
					}
					else
					{
						VisualElement.MultiplyMatrix34(ref ancestorMatrix, ref temp, out result);
					}
					currentAncestor = currentAncestor.hierarchy.parent;
					destIsTemp = !destIsTemp;
				}
				while (currentAncestor != null && ancestor != currentAncestor);
				bool flag3 = !destIsTemp;
				if (flag3)
				{
					result = temp;
				}
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		public static bool IsRoundRect(VisualElement ve)
		{
			IResolvedStyle style = ve.resolvedStyle;
			return style.borderTopLeftRadius >= 1E-30f || style.borderTopRightRadius >= 1E-30f || style.borderBottomLeftRadius >= 1E-30f || style.borderBottomRightRadius >= 1E-30f;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0004DAD0 File Offset: 0x0004BCD0
		public static bool IsVectorImageBackground(VisualElement ve)
		{
			return ve.computedStyle.backgroundImage.vectorImage != null;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0004DAFC File Offset: 0x0004BCFC
		public static void Destroy(Object obj)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool isPlaying = Application.isPlaying;
				if (isPlaying)
				{
					Object.Destroy(obj);
				}
				else
				{
					Object.DestroyImmediate(obj);
				}
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0004DB30 File Offset: 0x0004BD30
		public static int GetPrevPow2(int n)
		{
			int bits = 0;
			while (n > 1)
			{
				n >>= 1;
				bits++;
			}
			return 1 << bits;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0004DB60 File Offset: 0x0004BD60
		public static int GetNextPow2(int n)
		{
			int test;
			for (test = 1; test < n; test <<= 1)
			{
			}
			return test;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0004DB84 File Offset: 0x0004BD84
		public static int GetNextPow2Exp(int n)
		{
			int test = 1;
			int exp = 0;
			while (test < n)
			{
				test <<= 1;
				exp++;
			}
			return exp;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetThreadIndex()
		{
			int? threadIndex = UIRUtility.s_ThreadIndex;
			bool flag = threadIndex != null;
			int num;
			if (flag)
			{
				num = threadIndex.Value;
			}
			else
			{
				int initValue = JobsUtility.ThreadIndex;
				UIRUtility.s_ThreadIndex = new int?(initValue);
				num = initValue;
			}
			return num;
		}

		// Token: 0x04000ACD RID: 2765
		private static readonly ProfilerMarker k_ComputeTransformMatrixMarker = new ProfilerMarker("UIR.ComputeTransformMatrix");

		// Token: 0x04000ACE RID: 2766
		public static readonly string k_DefaultShaderName = Shaders.k_Runtime;

		// Token: 0x04000ACF RID: 2767
		public static readonly string k_DefaultWorldSpaceShaderName = Shaders.k_RuntimeWorld;

		// Token: 0x04000AD0 RID: 2768
		[ThreadStatic]
		private static int? s_ThreadIndex;
	}
}
