using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditorSpace
{
    public class CommandManager
    {
        Stack<ICommand> undo = new Stack<ICommand>();
        Stack<ICommand> redo = new Stack<ICommand>();

        //處理繪圖的正運算
        public void Execute(ICommand command)
        {
            command.Execute();
            undo.Push(command);
            redo.Clear();
        }

        //處理Undo
        public void Undo()
        {
            ICommand command = undo.Pop();
            redo.Push(command);
            command.UnExecute();
        }

        //處理Redo
        public void Redo()
        {
            ICommand command = redo.Pop();
            undo.Push(command);
            command.Execute();
        }

        //清除所有Command紀錄
        public void ClearCommand()
        {
            redo.Clear();
            undo.Clear();
        }

        //判斷Redo按鈕是否Enable
        public bool IsRedoEnabled
        {
            get
            {
                return redo.Count != 0;
            }
        }

        //判斷Undo按鈕是否Enable
        public bool IsUndoEnabled
        {
            get
            {
                return undo.Count != 0;
            }
        }
    }
}
