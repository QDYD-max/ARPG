using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace CFramework
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactor : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }

        public SplitView()
        {
            
        }
    }
}