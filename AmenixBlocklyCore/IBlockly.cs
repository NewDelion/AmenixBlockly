using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AmenixBlocklyCore
{
    public delegate void BlockPickupEventHandler(UIElement el);
    public delegate void BlockMoveEventHandler(UIElement el);
    public delegate void BlockDropEventHandler(UIElement el);
    

    public enum BLOCK_TYPE
    {
        BLOCK_FUNC,//アクションブロック(関数など)
        BLOCK_ARG_1,//文字列ブロック
        BLOCK_ARG_2,//数値ブロック
        BLOCK_ARG_3,//論理ブロック
        BLOCK_ARG_4//データブロック(アロー演算子で連結されてる文や、関数の戻り値を別の関数の引数に渡す文を別ウィンドウで構築できるブロック)
    }

    public interface IBlockly
    {
        string BlockId { get; }

        BLOCK_TYPE BlockType { get; }

        bool CanRemove { get; }

        event BlockPickupEventHandler OnBlockPickup;
        event BlockMoveEventHandler OnBlockMove;
        event BlockDropEventHandler OnBlockDrop;

        double GetHeight();
        Point GetLinkForwardPoint();
        Point GetLinkBackwardPoint();
        Point GetPointFromLinkForward(Point link_point);
        Point GetPointFromLinkBackward(Point link_point);
        IBlockly OwnerBlock { get; set; }
        Canvas GetCanvas();
        bool IsChain(UIElement el);
        bool SetChain(UIElement el);
        void ReleaseChain(UIElement el);
    }
}
