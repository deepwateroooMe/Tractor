using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Drawing;
using System.Drawing.Imaging;
using Kuaff.CardResouces;
namespace Kuaff.Tractor {
	// 【TODO】：当添加两项左右的用户配置，这里也应该有相应的添加
    [Serializable] // 这里，应该就是说，【序列化】配置，到【序列化的文件】，文件的名字，是哪里写定的？
	// 哪里，也没有写，是【序列化】到哪个特定的文件，除非使用Unity 等游戏引擎，在控制面板上、设置配置的
    class GameConfig { // 这里的配置，是会写入某个文件的？
		// 【时间设置】：感觉，都是游戏流程自己管理，用来给用户呈现一下游戏过程的、可调控可缩短的、系统控制时间变量 
#region 时间设置
        // 时间设置
        int finishedOncePauseTime = DefinedConstant.FINISHEDONCEPAUSETIME; // 结算时间
        internal int FinishedOncePauseTime {
            get { return finishedOncePauseTime; }
            set { finishedOncePauseTime = value; }
        }
        int noRankPauseTime = DefinedConstant.NORANKPAUSETIME; // 流局暂停时间
        public int NoRankPauseTime {
            get { return noRankPauseTime; }
            set { noRankPauseTime = value; }
        }
        int get8CardsTime = DefinedConstant.GET8CARDSTIME; // 扣牌时间
        public int Get8CardsTime {
            get { return get8CardsTime; }
            set { get8CardsTime = value; }
        }
        int sortCardsTime = DefinedConstant.SORTCARDSTIME; // 排序时间
        public int SortCardsTime {
            get { return sortCardsTime; }
            set { sortCardsTime = value; }
        }
        int finishedThisTime = DefinedConstant.FINISHEDTHISTIME; // 总结算时间
        public int FinishedThisTime {
            get { return finishedThisTime; }
            set { finishedThisTime = value; }
        }
        int timerDiDa = DefinedConstant.TIMERDIDA; // 游戏滴答
        public int TimerDiDa {
            get { return timerDiDa; }
            set { timerDiDa = value; }
        }
#endregion // 时间设置
// 【规则设置】：用户表单里、自定义的、【游戏规则配置】，从表单向游戏逻辑过度。【TODO】：【2 是常主】配置，与【五主过河】配置：主牌《＝5 张时，与对家组出更能赢的牌
#region 规则设置
        private string mustRank = ""; // 必打的牌
        internal string MustRank {
            get { return mustRank; }
            set { mustRank = value; }
        }
        // 是否在调试
        private bool isDebug = false;
        internal bool IsDebug {
            get { return isDebug; }
            set { isDebug = value; }
        }
        // 扣底算法
        private int bottomAlgorithm = 1;
        internal int BottomAlgorithm {
            get { return bottomAlgorithm; }
            set { bottomAlgorithm = value; }
        }
        // 是否可以流局
        // private bool isPass = true;
        private bool isPass = false;
        internal bool IsPass {
            get { return isPass; }
            set { isPass = value; }
        }
        // 是否可以J到底
        // private bool jToBottom = false;
        private bool jToBottom = true;
        internal bool JToBottom {
            get { return jToBottom; }
            set { jToBottom = value; }
        }
        // 是否可以q到半
        private bool qToHalf = true;
        // private bool qToHalf = false;
        internal bool QToHalf {
            get { return qToHalf; }
            set { qToHalf = value; }
        }
        // 是否可以A到J
        // private bool aToJ = true;
        private bool aToJ = false;
        internal bool AToJ {
            get { return aToJ; }
            set { aToJ = value; }
        }
        // 是否可以自反
        private bool canMyRankAgain = true;
        internal bool CanMyRankAgain {
            get { return canMyRankAgain; }
            set { canMyRankAgain = value; }
        }
        // 是否可以亮无主
        private bool canRankJack = true;
        internal bool CanRankJack {
            get { return canRankJack; }
            set { canRankJack = value; }
        }
        // 是否可以加固
        private bool canMyStrengthen = true;
        internal bool CanMyStrengthen {
            get { return canMyStrengthen; }
            set { canMyStrengthen = value; }
        }
        private int whenFinished = 0;
        internal int WhenFinished {
            get { return whenFinished; }
            set { whenFinished = value; }
        }
#endregion // 规则设置

#region 图案设置
        // 只有在使用内置的牌面资源时才起作用
        [NonSerialized()]
        ResourceManager cardsResourceManager = Kuaff_Cards.ResourceManager; // 当前牌面所使用的资源管理器
        public ResourceManager CardsResourceManager {
            get {
                if (cardsResourceManager != null) {
                    return cardsResourceManager;
                }
                else {
                    cardsResourceManager = Kuaff_Cards.ResourceManager;
                    return cardsResourceManager;
                }
            }
            set { cardsResourceManager = value; }
        }
        [NonSerialized()]
        Bitmap backImage = Kuaff_Cards.back; // 桌面背景
        internal Bitmap BackImage {
            get {
                if (backImage != null) {
                    return backImage;
                }
                else {
                    backImage = Kuaff_Cards.back;
                    return backImage;
                }
            }
            set { backImage = value; }
        }
       
        // 是否采用自定义的牌面
        private string cardImageName = ""; // 牌面图案
        internal string CardImageName {
            get { return cardImageName; }
            set { cardImageName = value; }
        }
#endregion // 图案设置
    }
}