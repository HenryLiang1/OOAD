using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ImageEditorSpace
{
    public class Layer
    {
        public List<Tool> toolList;

        public bool IsVisible
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Layer()
        {
            this.toolList = new List<Tool>();
        }

        public void Draw(Tool currentTool)
        {
            this.toolList.Add(currentTool);
        }

        public void Erase()
        {
            this.toolList.RemoveAt(toolList.Count-1);
        }

    }
}
