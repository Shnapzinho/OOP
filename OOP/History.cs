using System.Windows.Input;

namespace OOP;
public class History
{
	private readonly Stack<ICommand> undoStack = new Stack<ICommand>();
	private readonly Stack<ICommand> redoStack = new Stack<ICommand>();

	public void Execute(ICommand command)
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

public interface ICommand
{
	void Execute();
	void Undo();
}