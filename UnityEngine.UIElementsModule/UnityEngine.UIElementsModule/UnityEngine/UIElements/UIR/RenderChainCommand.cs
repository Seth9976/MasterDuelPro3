using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200051B RID: 1307
	internal class RenderChainCommand : LinkedPoolItem<RenderChainCommand>
	{
		// Token: 0x06002446 RID: 9286 RVA: 0x00088B84 File Offset: 0x00086D84
		internal void Reset()
		{
			this.owner = null;
			this.prev = (this.next = null);
			this.isTail = false;
			this.type = CommandType.Draw;
			this.state = default(State);
			this.mesh = null;
			this.indexOffset = (this.indexCount = 0);
			this.callback = null;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00088BE4 File Offset: 0x00086DE4
		internal void ExecuteNonDrawMesh(DrawParams drawParams, float pixelsPerPoint, ref Exception immediateException)
		{
			switch (this.type)
			{
			case CommandType.ImmediateCull:
			{
				bool flag = !RenderChainCommand.RectPointsToPixelsAndFlipYAxis(this.owner.worldBound, pixelsPerPoint).Overlaps(Utility.GetActiveViewport());
				if (flag)
				{
					return;
				}
				break;
			}
			case CommandType.Immediate:
				break;
			case CommandType.PushView:
			{
				drawParams.view.Push(this.owner.worldTransform);
				GL.modelview = this.owner.worldTransform;
				VisualElement parent = this.owner.hierarchy.parent;
				bool flag2 = parent != null;
				Rect clipRect;
				if (flag2)
				{
					clipRect = parent.worldClip;
				}
				else
				{
					clipRect = DrawParams.k_FullNormalizedRect;
				}
				drawParams.scissor.Push(clipRect);
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(clipRect, pixelsPerPoint));
				return;
			}
			case CommandType.PopView:
			{
				drawParams.view.Pop();
				GL.modelview = drawParams.view.Peek();
				drawParams.scissor.Pop();
				Rect prevRect = drawParams.scissor.Peek();
				bool flag3 = prevRect.x == DrawParams.k_UnlimitedRect.x;
				if (flag3)
				{
					Utility.DisableScissor();
				}
				else
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(prevRect, pixelsPerPoint));
				}
				return;
			}
			case CommandType.PushScissor:
			{
				Rect elemRect = RenderChainCommand.CombineScissorRects(this.owner.worldClip, drawParams.scissor.Peek());
				drawParams.scissor.Push(elemRect);
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(elemRect, pixelsPerPoint));
				return;
			}
			case CommandType.PopScissor:
			{
				drawParams.scissor.Pop();
				Rect prevRect2 = drawParams.scissor.Peek();
				bool flag4 = prevRect2.x == DrawParams.k_UnlimitedRect.x;
				if (flag4)
				{
					Utility.DisableScissor();
				}
				else
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(prevRect2, pixelsPerPoint));
				}
				return;
			}
			case CommandType.PushRenderTexture:
			{
				RectInt viewport = Utility.GetActiveViewport();
				RenderTexture rt = RenderTexture.GetTemporary(viewport.width, viewport.height, 24, RenderTextureFormat.ARGBHalf);
				RenderTexture.active = rt;
				GL.Clear(true, true, new Color(0f, 0f, 0f, 0f), 0.99f);
				drawParams.renderTexture.Add(RenderTexture.active);
				return;
			}
			case CommandType.PopRenderTexture:
			{
				int index = drawParams.renderTexture.Count - 1;
				Debug.Assert(index > 0);
				Debug.Assert(drawParams.renderTexture[index - 1] == RenderTexture.active, "Content of previous render texture was probably not blitted");
				RenderTexture rt2 = drawParams.renderTexture[index];
				bool flag5 = rt2 != null;
				if (flag5)
				{
					RenderTexture.ReleaseTemporary(rt2);
				}
				drawParams.renderTexture.RemoveAt(index);
				return;
			}
			case CommandType.BlitToPreviousRT:
			{
				RenderTexture source = drawParams.renderTexture[drawParams.renderTexture.Count - 1];
				RenderTexture destination = drawParams.renderTexture[drawParams.renderTexture.Count - 2];
				Debug.Assert(source == RenderTexture.active, "Unexpected render target change: Current renderTarget is not the one on the top of the stack");
				this.Blit(source, destination, 0f);
				return;
			}
			case CommandType.PushDefaultMaterial:
				return;
			case CommandType.PopDefaultMaterial:
				return;
			default:
				return;
			}
			bool flag6 = immediateException != null;
			if (!flag6)
			{
				Matrix4x4 oldProjection = Utility.GetUnityProjectionMatrix();
				Camera oldCamera = Camera.current;
				RenderTexture oldRT = RenderTexture.active;
				bool hasScissor = drawParams.scissor.Count > 1;
				bool flag7 = hasScissor;
				if (flag7)
				{
					Utility.DisableScissor();
				}
				using (new GUIClip.ParentClipScope(this.owner.worldTransform, this.owner.worldClip))
				{
					try
					{
						this.callback();
					}
					catch (Exception e)
					{
						immediateException = e;
					}
				}
				Camera.SetupCurrent(oldCamera);
				RenderTexture.active = oldRT;
				GL.modelview = drawParams.view.Peek();
				GL.LoadProjectionMatrix(oldProjection);
				bool flag8 = hasScissor;
				if (flag8)
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(drawParams.scissor.Peek(), pixelsPerPoint));
				}
			}
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00089014 File Offset: 0x00087214
		private void Blit(Texture source, RenderTexture destination, float depth)
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			RenderTexture.active = destination;
			this.state.material.SetTexture(RenderChainCommand.k_ID_MainTex, source);
			this.state.material.SetPass(0);
			GL.Begin(7);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, depth);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, depth);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, depth);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, depth);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000890F8 File Offset: 0x000872F8
		private static Rect CombineScissorRects(Rect r0, Rect r1)
		{
			Rect r2 = new Rect(0f, 0f, 0f, 0f);
			r2.x = Math.Max(r0.x, r1.x);
			r2.y = Math.Max(r0.y, r1.y);
			r2.xMax = Math.Max(r2.x, Math.Min(r0.xMax, r1.xMax));
			r2.yMax = Math.Max(r2.y, Math.Min(r0.yMax, r1.yMax));
			return r2;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000891AC File Offset: 0x000873AC
		private static RectInt RectPointsToPixelsAndFlipYAxis(Rect rect, float pixelsPerPoint)
		{
			float viewportHeight = (float)Utility.GetActiveViewport().height;
			return new RectInt(0, 0, 0, 0)
			{
				x = Mathf.RoundToInt(rect.x * pixelsPerPoint),
				y = Mathf.RoundToInt(viewportHeight - rect.yMax * pixelsPerPoint),
				width = Mathf.RoundToInt(rect.width * pixelsPerPoint),
				height = Mathf.RoundToInt(rect.height * pixelsPerPoint)
			};
		}

		// Token: 0x04001103 RID: 4355
		internal VisualElement owner;

		// Token: 0x04001104 RID: 4356
		internal RenderChainCommand prev;

		// Token: 0x04001105 RID: 4357
		internal RenderChainCommand next;

		// Token: 0x04001106 RID: 4358
		internal bool isTail;

		// Token: 0x04001107 RID: 4359
		internal CommandType type;

		// Token: 0x04001108 RID: 4360
		internal State state;

		// Token: 0x04001109 RID: 4361
		internal MeshHandle mesh;

		// Token: 0x0400110A RID: 4362
		internal int indexOffset;

		// Token: 0x0400110B RID: 4363
		internal int indexCount;

		// Token: 0x0400110C RID: 4364
		internal Action callback;

		// Token: 0x0400110D RID: 4365
		private static readonly int k_ID_MainTex = Shader.PropertyToID("_MainTex");

		// Token: 0x0400110E RID: 4366
		private static ProfilerMarker s_ImmediateOverheadMarker = new ProfilerMarker("UIR.ImmediateOverhead");
	}
}
