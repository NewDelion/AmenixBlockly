using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmenixBlocklyCore.block
{
    /// <summary>
    /// 移動はできるけど、DataArgSlotにしか連結できない
    /// </summary>
    public partial class DataArgControl : UserControl, IBlockly
    {
        public string BlockId { get { return "DataArgControl"; } }

        public BLOCK_TYPE BlockType { get { return BLOCK_TYPE.BLOCK_ARG_4; } }

        public bool CanRemove { get { return true; } }

        public DataArgControl()
        {
            InitializeComponent();
        }

        public bool IsChain(UIElement el) { return false; }
        public bool SetChain(UIElement el) { return false; }
        public void ReleaseChain(UIElement el) { }

        public event BlockDropEventHandler OnBlockDrop;
        public event BlockMoveEventHandler OnBlockMove;
        public event BlockPickupEventHandler OnBlockPickup;

        public Canvas GetCanvas()
        {
            Func<IBlockly, Canvas> saiki = null;
            saiki = (c) =>
            {
                if (c.OwnerBlock == null)
                    return (c as Control).Parent as Canvas;
                return saiki(c.OwnerBlock);
            };
            return saiki(this);
        }

        public double GetHeight() { return 25.0; }

        public virtual Point GetLinkBackwardPoint() { return new Point(); }
        public virtual Point GetLinkForwardPoint() { return new Point(RootPanel.ActualWidth + (double)GetValue(Canvas.LeftProperty) - 2.5, (double)GetValue(Canvas.TopProperty) + 15); }
        public virtual Point GetPointFromLinkBackward(Point link_point) { return new Point(); }
        public virtual Point GetPointFromLinkForward(Point link_point) { return new Point(-RootPanel.ActualWidth - link_point.X + 2.5, link_point.Y - 15); }

        public IBlockly OwnerBlock { get; set; }
    }
}
