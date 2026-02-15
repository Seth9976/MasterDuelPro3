using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000504 RID: 1284
	internal static class CommandManipulator
	{
		// Token: 0x060023CF RID: 9167 RVA: 0x000839C8 File Offset: 0x00081BC8
		private static bool IsParentOrAncestorOf(this VisualElement ve, VisualElement child)
		{
			while (child.hierarchy.parent != null)
			{
				bool flag = child.hierarchy.parent == ve;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00083A20 File Offset: 0x00081C20
		public static void ReplaceCommands(RenderChain renderChain, VisualElement ve, EntryProcessor processor)
		{
			bool flag = processor.firstHeadCommand == null && processor.firstTailCommand == null && ve.renderChainData.firstHeadCommand != null;
			if (flag)
			{
				CommandManipulator.ResetCommands(renderChain, ve);
			}
			else
			{
				bool foundInsertionBounds = false;
				RenderChainCommand prev = null;
				RenderChainCommand next = null;
				bool flag2 = ve.renderChainData.firstHeadCommand != null;
				if (flag2)
				{
					prev = ve.renderChainData.firstHeadCommand.prev;
					next = ve.renderChainData.lastHeadCommand.next;
					CommandManipulator.RemoveChain(renderChain, ve.renderChainData.firstHeadCommand, ve.renderChainData.lastHeadCommand);
					foundInsertionBounds = true;
				}
				bool flag3 = processor.firstHeadCommand != null;
				if (flag3)
				{
					bool flag4 = !foundInsertionBounds;
					if (flag4)
					{
						CommandManipulator.FindHeadCommandInsertionPoint(ve, out prev, out next);
					}
					bool flag5 = prev != null;
					if (flag5)
					{
						processor.firstHeadCommand.prev = prev;
						prev.next = processor.firstHeadCommand;
					}
					bool flag6 = next != null;
					if (flag6)
					{
						processor.lastHeadCommand.next = next;
						next.prev = processor.lastHeadCommand;
					}
					renderChain.OnRenderCommandAdded(processor.firstHeadCommand);
				}
				ve.renderChainData.firstHeadCommand = processor.firstHeadCommand;
				ve.renderChainData.lastHeadCommand = processor.lastHeadCommand;
				bool foundInsertionBounds2 = false;
				RenderChainCommand prev2 = null;
				RenderChainCommand next2 = null;
				bool flag7 = ve.renderChainData.firstTailCommand != null;
				if (flag7)
				{
					prev2 = ve.renderChainData.firstTailCommand.prev;
					next2 = ve.renderChainData.lastTailCommand.next;
					CommandManipulator.RemoveChain(renderChain, ve.renderChainData.firstTailCommand, ve.renderChainData.lastTailCommand);
					foundInsertionBounds2 = true;
				}
				bool flag8 = processor.firstTailCommand != null;
				if (flag8)
				{
					bool flag9 = !foundInsertionBounds2;
					if (flag9)
					{
						CommandManipulator.FindTailCommandInsertionPoint(ve, out prev2, out next2);
					}
					bool flag10 = prev2 != null;
					if (flag10)
					{
						processor.firstTailCommand.prev = prev2;
						prev2.next = processor.firstTailCommand;
					}
					bool flag11 = next2 != null;
					if (flag11)
					{
						processor.lastTailCommand.next = next2;
						next2.prev = processor.lastTailCommand;
					}
					renderChain.OnRenderCommandAdded(processor.firstTailCommand);
				}
				ve.renderChainData.firstTailCommand = processor.firstTailCommand;
				ve.renderChainData.lastTailCommand = processor.lastTailCommand;
			}
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00083C68 File Offset: 0x00081E68
		private static void FindHeadCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement prevDrawingElem = ve.renderChainData.prev;
			while (prevDrawingElem != null && prevDrawingElem.renderChainData.lastHeadCommand == null)
			{
				prevDrawingElem = prevDrawingElem.renderChainData.prev;
			}
			bool flag = prevDrawingElem != null && prevDrawingElem.renderChainData.lastHeadCommand != null;
			if (flag)
			{
				bool flag2 = prevDrawingElem.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					prev = prevDrawingElem.renderChainData.lastTailOrHeadCommand;
				}
				else
				{
					bool flag3 = prevDrawingElem.IsParentOrAncestorOf(ve);
					if (flag3)
					{
						prev = prevDrawingElem.renderChainData.lastHeadCommand;
					}
					else
					{
						RenderChainCommand lastCommand = prevDrawingElem.renderChainData.lastTailOrHeadCommand;
						bool flag5;
						do
						{
							prev = lastCommand;
							lastCommand = lastCommand.next;
							bool flag4 = lastCommand == null || lastCommand.owner == ve || !lastCommand.isTail;
							if (flag4)
							{
								break;
							}
							flag5 = lastCommand.owner.IsParentOrAncestorOf(ve);
						}
						while (!flag5);
					}
				}
				next = prev.next;
			}
			else
			{
				VisualElement nextDrawingElem = ve.renderChainData.next;
				while (nextDrawingElem != null && nextDrawingElem.renderChainData.firstHeadCommand == null)
				{
					nextDrawingElem = nextDrawingElem.renderChainData.next;
				}
				next = ((nextDrawingElem != null) ? nextDrawingElem.renderChainData.firstHeadCommand : null);
				prev = null;
				Debug.Assert(next == null || next.prev == null);
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00083DE4 File Offset: 0x00081FE4
		private static void FindTailCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement nextDrawingElem = ve.renderChainData.next;
			while (nextDrawingElem != null && nextDrawingElem.renderChainData.firstHeadCommand == null)
			{
				nextDrawingElem = nextDrawingElem.renderChainData.next;
			}
			bool flag = nextDrawingElem != null && nextDrawingElem.renderChainData.firstHeadCommand != null;
			if (flag)
			{
				bool flag2 = nextDrawingElem.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					next = nextDrawingElem.renderChainData.firstHeadCommand;
					prev = next.prev;
				}
				else
				{
					bool flag3 = ve.IsParentOrAncestorOf(nextDrawingElem);
					if (flag3)
					{
						bool flag4;
						do
						{
							prev = nextDrawingElem.renderChainData.lastTailOrHeadCommand;
							RenderChainCommand next2 = prev.next;
							nextDrawingElem = ((next2 != null) ? next2.owner : null);
							flag4 = nextDrawingElem == null || !ve.IsParentOrAncestorOf(nextDrawingElem);
						}
						while (!flag4);
						next = prev.next;
					}
					else
					{
						prev = ve.renderChainData.lastHeadCommand;
						next = prev.next;
					}
				}
			}
			else
			{
				prev = ve.renderChainData.lastHeadCommand;
				next = prev.next;
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00083F0C File Offset: 0x0008210C
		private static void RemoveChain(RenderChain renderChain, RenderChainCommand first, RenderChainCommand last)
		{
			Debug.Assert(first != null);
			Debug.Assert(last != null);
			renderChain.OnRenderCommandsRemoved(first, last);
			bool flag = first.prev != null;
			if (flag)
			{
				first.prev.next = last.next;
			}
			bool flag2 = last.next != null;
			if (flag2)
			{
				last.next.prev = first.prev;
			}
			RenderChainCommand current = first;
			RenderChainCommand prev;
			do
			{
				RenderChainCommand next = current.next;
				renderChain.FreeCommand(current);
				prev = current;
				current = next;
			}
			while (prev != last);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00083F9C File Offset: 0x0008219C
		public static void ResetCommands(RenderChain renderChain, VisualElement ve)
		{
			bool flag = ve.renderChainData.firstHeadCommand != null;
			if (flag)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstHeadCommand, ve.renderChainData.lastHeadCommand);
			}
			RenderChainCommand prev = ((ve.renderChainData.firstHeadCommand != null) ? ve.renderChainData.firstHeadCommand.prev : null);
			RenderChainCommand next = ((ve.renderChainData.lastHeadCommand != null) ? ve.renderChainData.lastHeadCommand.next : null);
			Debug.Assert(prev == null || prev.owner != ve);
			Debug.Assert(next == null || next == ve.renderChainData.firstTailCommand || next.owner != ve);
			bool flag2 = prev != null;
			if (flag2)
			{
				prev.next = next;
			}
			bool flag3 = next != null;
			if (flag3)
			{
				next.prev = prev;
			}
			bool flag4 = ve.renderChainData.firstHeadCommand != null;
			if (flag4)
			{
				RenderChainCommand c;
				RenderChainCommand nextC;
				for (c = ve.renderChainData.firstHeadCommand; c != ve.renderChainData.lastHeadCommand; c = nextC)
				{
					nextC = c.next;
					renderChain.FreeCommand(c);
				}
				renderChain.FreeCommand(c);
			}
			ve.renderChainData.firstHeadCommand = (ve.renderChainData.lastHeadCommand = null);
			prev = ((ve.renderChainData.firstTailCommand != null) ? ve.renderChainData.firstTailCommand.prev : null);
			next = ((ve.renderChainData.lastTailCommand != null) ? ve.renderChainData.lastTailCommand.next : null);
			Debug.Assert(prev == null || prev.owner != ve);
			Debug.Assert(next == null || next.owner != ve);
			bool flag5 = prev != null;
			if (flag5)
			{
				prev.next = next;
			}
			bool flag6 = next != null;
			if (flag6)
			{
				next.prev = prev;
			}
			bool flag7 = ve.renderChainData.firstTailCommand != null;
			if (flag7)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstTailCommand, ve.renderChainData.lastTailCommand);
				RenderChainCommand c2;
				RenderChainCommand nextC2;
				for (c2 = ve.renderChainData.firstTailCommand; c2 != ve.renderChainData.lastTailCommand; c2 = nextC2)
				{
					nextC2 = c2.next;
					renderChain.FreeCommand(c2);
				}
				renderChain.FreeCommand(c2);
			}
			ve.renderChainData.firstTailCommand = (ve.renderChainData.lastTailCommand = null);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00084220 File Offset: 0x00082420
		private static void InjectCommandInBetween(RenderChain renderChain, RenderChainCommand cmd, RenderChainCommand prev, RenderChainCommand next)
		{
			bool flag = prev != null;
			if (flag)
			{
				cmd.prev = prev;
				prev.next = cmd;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				cmd.next = next;
				next.prev = cmd;
			}
			VisualElement ve = cmd.owner;
			bool flag3 = !cmd.isTail;
			if (flag3)
			{
				bool flag4 = ve.renderChainData.firstHeadCommand == null || ve.renderChainData.firstHeadCommand == next;
				if (flag4)
				{
					ve.renderChainData.firstHeadCommand = cmd;
				}
				bool flag5 = ve.renderChainData.lastHeadCommand == null || ve.renderChainData.lastHeadCommand == prev;
				if (flag5)
				{
					ve.renderChainData.lastHeadCommand = cmd;
				}
			}
			else
			{
				bool flag6 = ve.renderChainData.firstTailCommand == null || ve.renderChainData.firstTailCommand == next;
				if (flag6)
				{
					ve.renderChainData.firstTailCommand = cmd;
				}
				bool flag7 = ve.renderChainData.lastTailCommand == null || ve.renderChainData.lastTailCommand == prev;
				if (flag7)
				{
					ve.renderChainData.lastTailCommand = cmd;
				}
			}
			renderChain.OnRenderCommandAdded(cmd);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00084340 File Offset: 0x00082540
		public static void DisableElementRendering(RenderChain renderChain, VisualElement ve, bool renderingDisabled)
		{
			bool flag = !ve.renderChainData.isInChain;
			if (!flag)
			{
				if (renderingDisabled)
				{
					bool flag2 = ve.renderChainData.firstHeadCommand == null || ve.renderChainData.firstHeadCommand.type != CommandType.BeginDisable;
					if (flag2)
					{
						RenderChainCommand cmd = renderChain.AllocCommand();
						cmd.type = CommandType.BeginDisable;
						cmd.owner = ve;
						bool flag3 = ve.renderChainData.firstHeadCommand == null;
						if (flag3)
						{
							RenderChainCommand cmdPrev;
							RenderChainCommand cmdNext;
							CommandManipulator.FindHeadCommandInsertionPoint(ve, out cmdPrev, out cmdNext);
							CommandManipulator.InjectCommandInBetween(renderChain, cmd, cmdPrev, cmdNext);
						}
						else
						{
							RenderChainCommand prev = ve.renderChainData.firstHeadCommand.prev;
							RenderChainCommand next = ve.renderChainData.firstHeadCommand;
							RenderChainCommand lastHeadCommand = ve.renderChainData.lastHeadCommand;
							Debug.Assert(lastHeadCommand != null);
							ve.renderChainData.firstHeadCommand = null;
							CommandManipulator.InjectCommandInBetween(renderChain, cmd, prev, next);
							ve.renderChainData.lastHeadCommand = lastHeadCommand;
						}
					}
					bool flag4 = ve.renderChainData.lastTailCommand == null || ve.renderChainData.lastTailCommand.type != CommandType.EndDisable;
					if (flag4)
					{
						RenderChainCommand cmd2 = renderChain.AllocCommand();
						cmd2.type = CommandType.EndDisable;
						cmd2.isTail = true;
						cmd2.owner = ve;
						bool flag5 = ve.renderChainData.lastTailCommand == null;
						if (flag5)
						{
							RenderChainCommand cmdPrev2;
							RenderChainCommand cmdNext2;
							CommandManipulator.FindTailCommandInsertionPoint(ve, out cmdPrev2, out cmdNext2);
							CommandManipulator.InjectCommandInBetween(renderChain, cmd2, cmdPrev2, cmdNext2);
						}
						else
						{
							RenderChainCommand prev2 = ve.renderChainData.lastTailCommand;
							RenderChainCommand next2 = ve.renderChainData.lastTailCommand.next;
							Debug.Assert(ve.renderChainData.firstTailCommand != null);
							CommandManipulator.InjectCommandInBetween(renderChain, cmd2, prev2, next2);
						}
					}
				}
				else
				{
					bool flag6 = ve.renderChainData.firstHeadCommand != null && ve.renderChainData.firstHeadCommand.type == CommandType.BeginDisable;
					if (flag6)
					{
						CommandManipulator.RemoveSingleCommand(renderChain, ve, ve.renderChainData.firstHeadCommand);
					}
					bool flag7 = ve.renderChainData.lastTailCommand != null && ve.renderChainData.lastTailCommand.type == CommandType.EndDisable;
					if (flag7)
					{
						CommandManipulator.RemoveSingleCommand(renderChain, ve, ve.renderChainData.lastTailCommand);
					}
				}
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00084588 File Offset: 0x00082788
		private static void RemoveSingleCommand(RenderChain renderChain, VisualElement ve, RenderChainCommand cmd)
		{
			Debug.Assert(cmd != null);
			Debug.Assert(cmd.owner == ve);
			renderChain.OnRenderCommandsRemoved(cmd, cmd);
			RenderChainCommand prev = cmd.prev;
			RenderChainCommand next = cmd.next;
			bool flag = prev != null;
			if (flag)
			{
				prev.next = next;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				next.prev = prev;
			}
			bool flag3 = ve.renderChainData.firstHeadCommand == cmd;
			if (flag3)
			{
				bool flag4 = ve.renderChainData.firstHeadCommand == ve.renderChainData.lastHeadCommand;
				if (flag4)
				{
					RenderChainCommand prev2 = cmd.prev;
					Debug.Assert(((prev2 != null) ? prev2.owner : null) != ve, "When removing the first head command, the command before this one in the queue should belong to an other parent");
					RenderChainCommand next2 = cmd.next;
					Debug.Assert(((next2 != null) ? next2.owner : null) != ve || cmd.next == ve.renderChainData.firstTailCommand);
					ve.renderChainData.firstHeadCommand = null;
					ve.renderChainData.lastHeadCommand = null;
				}
				else
				{
					Debug.Assert(cmd.next.owner == ve);
					Debug.Assert(ve.renderChainData.lastHeadCommand != null);
					ve.renderChainData.firstHeadCommand = cmd.next;
				}
			}
			else
			{
				bool flag5 = ve.renderChainData.lastHeadCommand == cmd;
				if (flag5)
				{
					Debug.Assert(cmd.prev.owner == ve);
					Debug.Assert(ve.renderChainData.firstHeadCommand != null);
					ve.renderChainData.lastHeadCommand = cmd.prev;
				}
			}
			bool flag6 = ve.renderChainData.firstTailCommand == cmd;
			if (flag6)
			{
				bool flag7 = ve.renderChainData.firstTailCommand == ve.renderChainData.lastTailCommand;
				if (flag7)
				{
					RenderChainCommand prev3 = cmd.prev;
					Debug.Assert(((prev3 != null) ? prev3.owner : null) != ve || cmd.prev == ve.renderChainData.lastHeadCommand);
					RenderChainCommand next3 = cmd.next;
					Debug.Assert(((next3 != null) ? next3.owner : null) != ve);
					ve.renderChainData.firstTailCommand = null;
					ve.renderChainData.lastTailCommand = null;
				}
				else
				{
					Debug.Assert(cmd.next.owner == ve);
					Debug.Assert(ve.renderChainData.lastTailCommand != null);
					ve.renderChainData.firstTailCommand = cmd.next;
				}
			}
			else
			{
				bool flag8 = ve.renderChainData.lastTailCommand == cmd;
				if (flag8)
				{
					Debug.Assert(cmd.prev.owner == ve);
					Debug.Assert(ve.renderChainData.firstTailCommand != null);
					ve.renderChainData.lastTailCommand = cmd.prev;
				}
			}
			renderChain.FreeCommand(cmd);
		}
	}
}
