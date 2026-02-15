using System;

namespace Spine.Unity
{
	// Token: 0x02000064 RID: 100
	public class SkeletonRendererInstruction
	{
		// Token: 0x06000323 RID: 803 RVA: 0x00012683 File Offset: 0x00010883
		public void Clear()
		{
			this.attachments.Clear(false);
			this.rawVertexCount = -1;
			this.hasActiveClipping = false;
			this.submeshInstructions.Clear(false);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000126AB File Offset: 0x000108AB
		public void Dispose()
		{
			this.attachments.Clear(true);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000126BC File Offset: 0x000108BC
		public void SetWithSubset(ExposedList<SubmeshInstruction> instructions, int startSubmesh, int endSubmesh)
		{
			int runningVertexCount = 0;
			ExposedList<SubmeshInstruction> exposedList = this.submeshInstructions;
			exposedList.Clear(false);
			int submeshCount = endSubmesh - startSubmesh;
			exposedList.Resize(submeshCount);
			SubmeshInstruction[] submeshesItems = exposedList.Items;
			SubmeshInstruction[] instructionsItems = instructions.Items;
			for (int i = 0; i < submeshCount; i++)
			{
				SubmeshInstruction instruction = instructionsItems[startSubmesh + i];
				submeshesItems[i] = instruction;
				this.hasActiveClipping |= instruction.hasClipping;
				submeshesItems[i].rawFirstVertexIndex = runningVertexCount;
				runningVertexCount += instruction.rawVertexCount;
			}
			this.rawVertexCount = runningVertexCount;
			int startSlot = instructionsItems[startSubmesh].startSlot;
			int endSlot = instructionsItems[endSubmesh - 1].endSlot;
			this.attachments.Clear(false);
			int attachmentCount = endSlot - startSlot;
			this.attachments.Resize(attachmentCount);
			Attachment[] attachmentsItems = this.attachments.Items;
			Slot[] drawOrderItems = instructionsItems[0].skeleton.DrawOrder.Items;
			for (int j = 0; j < attachmentCount; j++)
			{
				Slot slot = drawOrderItems[startSlot + j];
				if (!slot.Bone.Active || slot.A == 0f)
				{
					attachmentsItems[j] = null;
				}
				else
				{
					attachmentsItems[j] = slot.Attachment;
				}
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000127F8 File Offset: 0x000109F8
		public void Set(SkeletonRendererInstruction other)
		{
			this.immutableTriangles = other.immutableTriangles;
			this.hasActiveClipping = other.hasActiveClipping;
			this.rawVertexCount = other.rawVertexCount;
			this.attachments.Clear(false);
			this.attachments.EnsureCapacity(other.attachments.Capacity);
			this.attachments.Count = other.attachments.Count;
			other.attachments.CopyTo(this.attachments.Items);
			this.submeshInstructions.Clear(false);
			this.submeshInstructions.EnsureCapacity(other.submeshInstructions.Capacity);
			this.submeshInstructions.Count = other.submeshInstructions.Count;
			other.submeshInstructions.CopyTo(this.submeshInstructions.Items);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000128C8 File Offset: 0x00010AC8
		public static bool GeometryNotEqual(SkeletonRendererInstruction a, SkeletonRendererInstruction b)
		{
			if (a.hasActiveClipping || b.hasActiveClipping)
			{
				return true;
			}
			if (a.rawVertexCount != b.rawVertexCount)
			{
				return true;
			}
			if (a.immutableTriangles != b.immutableTriangles)
			{
				return true;
			}
			int attachmentCountB = b.attachments.Count;
			if (a.attachments.Count != attachmentCountB)
			{
				return true;
			}
			int count = a.submeshInstructions.Count;
			int submeshCountB = b.submeshInstructions.Count;
			if (count != submeshCountB)
			{
				return true;
			}
			SubmeshInstruction[] submeshInstructionsItemsA = a.submeshInstructions.Items;
			SubmeshInstruction[] submeshInstructionsItemsB = b.submeshInstructions.Items;
			Attachment[] attachmentsA = a.attachments.Items;
			Attachment[] attachmentsB = b.attachments.Items;
			for (int i = 0; i < attachmentCountB; i++)
			{
				if (attachmentsA[i] != attachmentsB[i])
				{
					return true;
				}
			}
			for (int j = 0; j < submeshCountB; j++)
			{
				SubmeshInstruction submeshA = submeshInstructionsItemsA[j];
				SubmeshInstruction submeshB = submeshInstructionsItemsB[j];
				if (submeshA.rawVertexCount != submeshB.rawVertexCount || submeshA.startSlot != submeshB.startSlot || submeshA.endSlot != submeshB.endSlot || submeshA.rawTriangleCount != submeshB.rawTriangleCount || submeshA.rawFirstVertexIndex != submeshB.rawFirstVertexIndex)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040001FF RID: 511
		public readonly ExposedList<SubmeshInstruction> submeshInstructions = new ExposedList<SubmeshInstruction>();

		// Token: 0x04000200 RID: 512
		public bool immutableTriangles;

		// Token: 0x04000201 RID: 513
		public bool hasActiveClipping;

		// Token: 0x04000202 RID: 514
		public int rawVertexCount = -1;

		// Token: 0x04000203 RID: 515
		public readonly ExposedList<Attachment> attachments = new ExposedList<Attachment>();
	}
}
