using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditorSpace
{
    class UseStraightLineCommand : ICommand
    {
        StraightLine line;
        Layer layer;

        public UseStraightLineCommand(Layer layer, StraightLine line)
        {
            this.line = line;
            this.layer = layer;
        }

        public void Execute()
        {
            layer.Draw(line);
        }

        public void UnExecute()
        {
            layer.Erase();
        }
    }
}
