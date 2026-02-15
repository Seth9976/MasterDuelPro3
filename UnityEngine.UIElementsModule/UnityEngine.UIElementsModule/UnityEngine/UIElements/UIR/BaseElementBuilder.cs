using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000506 RID: 1286
	internal abstract class BaseElementBuilder
	{
		// Token: 0x060023DA RID: 9178
		public abstract bool RequiresStencilMask(VisualElement ve);

		// Token: 0x060023DB RID: 9179 RVA: 0x000848A4 File Offset: 0x00082AA4
		public void Build(MeshGenerationContext mgc)
		{
			VisualElement ve = mgc.visualElement;
			Debug.Assert(ve.areAncestorsAndSelfDisplayed);
			bool shouldCutRenderChain = ve.shouldCutRenderChain;
			if (shouldCutRenderChain)
			{
				mgc.entryRecorder.CutRenderChain(mgc.parentEntry);
			}
			bool isGroupTransform = ve.renderChainData.isGroupTransform;
			bool flag = isGroupTransform;
			if (flag)
			{
				mgc.entryRecorder.PushGroupMatrix(mgc.parentEntry);
			}
			bool usesSubRenderTargetMode = ve.subRenderTargetMode > VisualElement.RenderTargetMode.None;
			bool flag2 = usesSubRenderTargetMode;
			if (flag2)
			{
				mgc.entryRecorder.PushRenderTexture(mgc.parentEntry);
			}
			bool changesDefaultMaterial = ve.defaultMaterial != null;
			bool flag3 = changesDefaultMaterial;
			if (flag3)
			{
				mgc.entryRecorder.PushDefaultMaterial(mgc.parentEntry, ve.defaultMaterial);
			}
			bool mustPopClipping = false;
			bool visible = ve.visible;
			if (visible)
			{
				this.DrawVisualElementBackground(mgc);
				this.DrawVisualElementBorder(mgc);
				this.PushVisualElementClipping(mgc);
				mustPopClipping = true;
				BaseElementBuilder.InvokeGenerateVisualContent(mgc);
			}
			else
			{
				bool isClippingWithStencil = ve.renderChainData.clipMethod == ClipMethod.Stencil;
				bool isClippingWithScissors = ve.renderChainData.clipMethod == ClipMethod.Scissor;
				bool flag4 = isClippingWithScissors || isClippingWithStencil;
				if (flag4)
				{
					mustPopClipping = true;
					this.PushVisualElementClipping(mgc);
				}
			}
			mgc.entryRecorder.DrawChildren(mgc.parentEntry);
			bool flag5 = mustPopClipping;
			if (flag5)
			{
				BaseElementBuilder.PopVisualElementClipping(mgc);
			}
			bool flag6 = changesDefaultMaterial;
			if (flag6)
			{
				mgc.entryRecorder.PopDefaultMaterial(mgc.parentEntry);
			}
			bool flag7 = isGroupTransform;
			if (flag7)
			{
				mgc.entryRecorder.PopGroupMatrix(mgc.parentEntry);
			}
			bool flag8 = usesSubRenderTargetMode;
			if (flag8)
			{
				mgc.entryRecorder.BlitAndPopRenderTexture(mgc.parentEntry);
			}
		}

		// Token: 0x060023DC RID: 9180
		protected abstract void DrawVisualElementBackground(MeshGenerationContext mgc);

		// Token: 0x060023DD RID: 9181
		protected abstract void DrawVisualElementBorder(MeshGenerationContext mgc);

		// Token: 0x060023DE RID: 9182
		protected abstract void DrawVisualElementStencilMask(MeshGenerationContext mgc);

		// Token: 0x060023DF RID: 9183
		public abstract void ScheduleMeshGenerationJobs(MeshGenerationContext mgc);

		// Token: 0x060023E0 RID: 9184 RVA: 0x00084A34 File Offset: 0x00082C34
		private void PushVisualElementClipping(MeshGenerationContext mgc)
		{
			VisualElement ve = mgc.visualElement;
			bool flag = ve.renderChainData.clipMethod == ClipMethod.Scissor;
			if (flag)
			{
				mgc.entryRecorder.PushScissors(mgc.parentEntry);
			}
			else
			{
				bool flag2 = ve.renderChainData.clipMethod == ClipMethod.Stencil;
				if (flag2)
				{
					mgc.entryRecorder.BeginStencilMask(mgc.parentEntry);
					this.DrawVisualElementStencilMask(mgc);
					mgc.entryRecorder.EndStencilMask(mgc.parentEntry);
				}
			}
			mgc.entryRecorder.PushClippingRect(mgc.parentEntry);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00084AC4 File Offset: 0x00082CC4
		private static void PopVisualElementClipping(MeshGenerationContext mgc)
		{
			VisualElement ve = mgc.visualElement;
			mgc.entryRecorder.PopClippingRect(mgc.parentEntry);
			bool flag = ve.renderChainData.clipMethod == ClipMethod.Scissor;
			if (flag)
			{
				mgc.entryRecorder.PopScissors(mgc.parentEntry);
			}
			else
			{
				bool flag2 = ve.renderChainData.clipMethod == ClipMethod.Stencil;
				if (flag2)
				{
					mgc.entryRecorder.PopStencilMask(mgc.parentEntry);
				}
			}
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00084B38 File Offset: 0x00082D38
		private static void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			VisualElement ve = mgc.visualElement;
			Painter2D.isPainterActive = true;
			ve.InvokeGenerateVisualContent(mgc);
			Painter2D.isPainterActive = false;
		}
	}
}
