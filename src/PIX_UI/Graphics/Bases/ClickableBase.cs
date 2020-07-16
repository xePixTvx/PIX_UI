using PIX_UI.Graphics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIX_UI.Graphics.Bases
{
    public class ClickableBase : RenderableBase, IClickable
    {
        private bool _IsSelected = false;
        private Action _ExecAction = null;

        public virtual bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; }
        }

        public virtual Action ExecAction
        {
            get { return _ExecAction; }
            set { _ExecAction = value; }
        }

        public virtual void UpdateSelection()
        {
            App.Log.Print("UpdateSelection() not defined for " + Name, Logger.LoggerType.ERROR);
        }

        public virtual void ExecuteAction()
        {
            if (ExecAction == null)
            {
                App.Log.Print("ExecAction not defined for " + Name, Logger.LoggerType.ERROR);
            }
            else
            {
                ExecAction();
            }
        }
    }
}
