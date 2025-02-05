using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Kuaff.CardResouces;
namespace Kuaff.Tractor {
	// 【功能逻辑】：
    class CalculateRegionHelper {
        MainForm mainForm;
        internal CalculateRegionHelper(MainForm mainForm) {
            this.mainForm = mainForm;
        }
// 计算是否点击到牌面: 所以这里就被设置成为了，必须鼠标点击到，才重画，画得稍微高一点儿。
// 想要设置的点击辅助至少是：如果出对或是甩牌拖拉机，需要自动对成对成甩牌成拖拉机的牌自动改更。而不需要用户点2 3 等多次。同理再点击时同步降低，
// 【TODO】：亲用户便利小功能：手牌【AAKK】拖拉机，用户我点四张任意一个，四张全涨高；再点任意一张，至少三张全降低，代表玩家我先出A 帮我的对家带牌, 再甩牌 AKK, 对家可以出更多的牌。K==>AAK A==>AKK
        internal bool CalculateClickedRegion(MouseEventArgs e, int clicks) {
            // 采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count]; // 手上，现有所有牌的张数
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++) {
                if ((bool)mainForm.myCardIsReady[i]) { // 当前牌，是先前被点中的，就是二点，把它降低，【TODO】：
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 355, 71, 96));
                } else { // 以前没选过，现选中，涨高
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 375, 71, 96));
                }
            }
			// 【精确定位】：点中的是哪张牌？因为同一个鼠标点击点，可能是几张牌都占据这一块儿，现把鼠标定位到，一张特定的牌上
            // 计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个：
			// 这个是根据，这个表单的画法里，每张牌的宽度、与每相邻两张牌的宽度间隔来的，不细看
            int k = mainForm.myCardsLocation.Count;
            int m = 0;
            if (mainForm.myCardsLocation.Count > 5) {
                for (int i = 0; i < mainForm.myCardsLocation.Count - 5; i++) {
                    regions[i].Exclude(regions[i + 1]);
                    regions[i].Exclude(regions[i + 2]);
                    regions[i].Exclude(regions[i + 3]);
                    regions[i].Exclude(regions[i + 4]);
                    regions[i].Exclude(regions[i + 5]);
                }
                m = mainForm.myCardsLocation.Count - 5;
                k = 5;
            }
            for (int i = 0; i < k - 1; i++) 
                for (int j = 1; j < (k - i); j++) 
                    regions[i + m].Exclude(regions[m + i + j]);
            // 计算鼠标点是否落入区域中
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++) {
                if (regions[i].IsVisible(e.X, e.Y)) {
                    if (clicks == 2) {
                        mainForm.myCardIsReady[i] = true;
                    } else if (clicks == 1) { // 鼠标左銉，传1, 被点击的牌取反
                        mainForm.myCardIsReady[i] = !(bool)mainForm.myCardIsReady[i];
                    }
                    // 【判断条件＋优化思路】：【TODO】：
                    // whoseOrder == 1 该自己出牌，并且自己不是首发出牌，而是跟牌
                    // whoseOrder == 1 该自己出牌，自己是首发出牌，自己点中了，任何拖拉机，或是能甩牌【AAKK】【AKK-AAK】中的一张，自动化。。
                    // 出牌的它家出了对，甩牌或是拖拉机，【当要求出对，有对必出】时
                    // 自己有对，并点中了对儿中的一张，【自己有对，且选中了其中一张，另一张自动化、同涨高同降低】
                    // ＝＝》同步对儿中的另一张为被点中状态。我先判断这些条件，因为取的时候，同样要这步优化
                    // if (mainForm.firstSend != 1 && )
                    // if (mainForm.myCardIsReady[i]) { // 【添加】：Model 或是控制层对数据的进一步优化控制
                    // } else {
                    // }
                    return true;
                }
            }
            return false;
        }
// 计算是否点击到牌面：鼠标双击事件
        internal bool CalculateDoubleClickedRegion(MouseEventArgs e) {
            // 采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count]; // 同步到：牌的最新状态
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++) {
                if ((bool)mainForm.myCardIsReady[i]) {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 355, 71, 96));
                } else {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 375, 71, 96));
                }
            }
            // 桌游摆牌时，牌是连续摆放有重叠的。这里的意思是说，eg 【67】两张牌，7 会盖住6 的右边部分。为判断右键是否点在 6 上，要把被 7 覆盖的部分不算，才是点在6 上
            // 计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个
            int k = mainForm.myCardsLocation.Count;
            int m = 0;
            if (mainForm.myCardsLocation.Count > 5) {
                for (int i = 0; i < mainForm.myCardsLocation.Count - 5; i++) {
                    regions[i].Exclude(regions[i + 1]); // //求区域A中不含区域B的区域（B的补集）
                    regions[i].Exclude(regions[i + 2]);
                    regions[i].Exclude(regions[i + 3]);
                    regions[i].Exclude(regions[i + 4]);
                    regions[i].Exclude(regions[i + 5]);
                }
                m = mainForm.myCardsLocation.Count - 5;
                k = 5;
            }
            // 与上面同理，这里算的是：当前一张牌6, 可能会被后面的多张（最多4 张？）牌盖过。
			// 为什么只对最后5 张牌如此处理，而不是每张牌这么处理？前面的、所有一样可以把紧跟着的5 张尾巴排除的，不是上面 XX.Count 》 5 已经全部自动化排除了吗？！！
            for (int i = 0; i < k - 1; i++)  // 处理，最后面，紧跟着尾巴 < 5 张的特殊情况
                for (int j = 1; j < (k - i); j++) 
                    regions[i + m].Exclude(regions[m + i + j]);
            // 计算鼠标点是否落入区域中
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++) {
                if (regions[i].IsVisible(e.X, e.Y)) {
                    mainForm.myCardIsReady[i] = true;
                    return true;
                }
            }
            return false;
        }
        internal int CalculateRightClickedRegion(MouseEventArgs e) { // 【点击逻辑】定义：当【右键点击】【左键选中过、涨高了】的牌，自动降低，变为【没选中】状态
            // 采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count]; // 遍历自已的手牌，25 张或者更少
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++) {
                if ((bool)mainForm.myCardIsReady[i]) { // 这张牌被左键过，升高了
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 355, 71, 96));
                } else { // 这里取反了：右键，没选中的会被选中。【逻辑狠过分】：不应该把手中的牌全部取反了吗？
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 375, 71, 96));
                }
            }
            int k = mainForm.myCardsLocation.Count;
            int m = 0;
            if (mainForm.myCardsLocation.Count > 5) {
                // 计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个。下面的循环过程中，会把倒数前 5 个前的处理好，处理完毕
                for (int i = 0; i < mainForm.myCardsLocation.Count - 5; i++) {
                    regions[i].Exclude(regions[i + 1]);
                    regions[i].Exclude(regions[i + 2]);
                    regions[i].Exclude(regions[i + 3]);
                    regions[i].Exclude(regions[i + 4]);
                    regions[i].Exclude(regions[i + 5]);
                }
                m = mainForm.myCardsLocation.Count - 5;
                k = 5;
            }
            // 计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个，【最后5 张牌】：不能完全嵌入前面循环，才需要特殊处理一下【活宝妹就是一定要嫁给亲爱的表哥！！】
            for (int i = 0; i < k - 1; i++) 
                for (int j = 1; j < (k - i); j++) 
                    regions[i + m].Exclude(regions[m + i + j]);
            // 计算鼠标点是否落入区域中：【Region】对于判断点是否落入，前面所有，只是对属于每张牌的干净 Region 作净化处理【属于每张牌的，只有一个小竖条！】
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++) 
                if (regions[i].IsVisible(e.X, e.Y)) { // 测试指定点是否包含在此 Region 中。如果能够简单测试，搞不明白前面一大堆是在做什么 ?
                    mainForm.myCardIsReady[i] = !(bool)mainForm.myCardIsReady[i]; // 这里取反才更新：【右銉】：先前选中的会被取消，先前没选中的会选中升高
                    return i;
                }
            return -1;
        }
    }
}