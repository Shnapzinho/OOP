using System.Windows.Input;

namespace OOP;
public interface HistoryInt
{
	void Execute();
	void Undo();
}
public class History
{
	private readonly Stack<HistoryInt> undoStack = new Stack<HistoryInt>();
	private readonly Stack<HistoryInt> redoStack = new Stack<HistoryInt>();

	public void Execute(HistoryInt command)
	{
		command.Execute();
		undoStack.Push(command);
		redoStack.Clear();
	}

	public void Undo()
	{
		if (CanUndo)
		{
			var command = undoStack.Pop();
			command.Undo();
			redoStack.Push(command);
		}
	}

	public void Redo()
	{
		if (CanRedo)
		{
			var command = redoStack.Pop();
			command.Execute();
			undoStack.Push(command);
		}
	}

	public bool CanUndo => undoStack.Count > 0;
	public bool CanRedo => redoStack.Count > 0;
}