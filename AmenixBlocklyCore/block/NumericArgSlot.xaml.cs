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
    /// NumericArgSlot.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericArgSlot : UserControl, IBlockly
    {
        public string BlockId { get { return "NumericArgSlot"; } }

        public BLOCK_TYPE BlockType { get { return BLOCK_TYPE.BLOCK_ARG_2; } }
        public bool CanRemove { get { return false; } }

        public event BlockPickupEventHandler OnBlockPickup;
        public event BlockMoveEventHandler OnBlockMove;
        public event BlockDropEventHandler OnBlockDrop;

        public NumericArgSlot()
        {
            InitializeComponent();
        }

        public double GetHeight()
        {
            return 25.0;
        }

        public Point GetLinkForwardPoint()
        {
            Point pt = TranslatePoint(new Point(0, 0), GetCanvas());
            return new Point(pt.X + 2.5, pt.Y + 15);
        }

        public Point GetLinkBackwardPoint()
        {
            return GetLinkForwardPoint();
        }

        public Point GetPointFromLinkForward(Point link_point)
        {
            return new Point(link_point.X - 2.5, link_point.Y - 15);
        }

        public Point GetPointFromLinkBackward(Point link_point)
        {
            return GetPointFromLinkForward(link_point);
        }

        public IBlockly OwnerBlock { get; set; }

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

        public bool IsChain(UIElement el)
        {
            if (ContentPanel.Children.Count == 1)
                return (ContentPanel.Children[0] as IBlockly).IsChain(el);
            return false;
        }

        public bool SetChain(UIElement el)
        {
            //後ろへチェインするだけ
            if (el.Equals(this))
                return false;
            if (BlockType == (el as IBlockly).BlockType && ContentPanel.Children.Count == 0)
            {
                Point pt = (el as IBlockly).GetLinkForwardPoint();
                Point element_pt = (this as IBlockly).GetLinkBackwardPoint();
                if (element_pt.X - 20 < pt.X && pt.X < element_pt.X + 20 && element_pt.Y - 20 < pt.Y && pt.Y < element_pt.Y + 20)
                {
                    GetCanvas().Children.Remove(el);
                    ContentPanel.Children.Add(el);
                    (el as IBlockly).OwnerBlock = this;
                    return true;
                }
            }
            if (ContentPanel.Children.Count == 1)
                return (ContentPanel.Children[0] as IBlockly).SetChain(el);

            return false;
        }

        public void ReleaseChain(UIElement el)
        {
            if (((Control)el).Parent.Equals(this))
                return;
            ContentPanel.Children.Remove(el);
            (el as IBlockly).OwnerBlock = null;
            GetCanvas().Children.Add(el);
        }

        protected void SetForefront(UIElement element)
        {
            int target_z = Canvas.GetZIndex(element);
            if (target_z + 1 == GetCanvas().Children.Count)
                return;
            Canvas.SetZIndex(element, GetCanvas().Children.Count);
            foreach (UIElement ele in GetCanvas().Children)
                if (target_z < Canvas.GetZIndex(ele))
                    Canvas.SetZIndex(ele, Canvas.GetZIndex(ele) - 1);
        }
    }
}
