using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditorSpace
{
    class UsePenCommand : ICommand
    {
        private Pen pen;
        private Layer layer;

        public UsePenCommand(Layer layer, Pen pen)
        {
            this.pen = pen;
            this.layer = layer;
        }

        public void Execute()
        {
            layer.Draw(pen);
        }

        public void UnExecute()
        {
            layer.Erase();
        }
    }
}
