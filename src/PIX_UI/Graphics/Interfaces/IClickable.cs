using System;

namespace PIX_UI.Graphics.Interfaces
{
    public interface IClickable
    {
        bool IsSelected { get; set; }
        Action ExecAction { get; set; }
        void UpdateSelection();
        void ExecuteAction();
    }
}
