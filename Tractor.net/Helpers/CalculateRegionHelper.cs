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
    class CalculateRegionHelper {
        MainForm mainForm;

        internal CalculateRegionHelper(MainForm mainForm) {
            this.mainForm = mainForm;
        }
// 计算是否点击到牌面: 所以这里就被设置成为了，必须鼠标点击到，才重画，画得稍微高一点儿。
// 想要设置的点击辅助至少是：如果出对或是甩牌拖拉机，需要自动对成对成甩牌成拖拉机的牌自动改更。而不需要用户点2 3 等多次。同理再点击时同步降低，比如换个对或是拖拉机出时
        internal bool CalculateClickedRegion(MouseEventArgs e,int clicks) {
            // 采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count];
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++) {
                if ((bool)mainForm.myCardIsReady[i]) {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 355, 71, 96));
                } else {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 375, 71, 96));
                }
            }
            // 计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个
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
                    } else if (clicks == 1) {
                        mainForm.myCardIsReady[i] = !(bool)mainForm.myCardIsReady[i];
                    }
                    return true;
                }
            }
            return false;
        }
// 计算是否点击到牌面
        internal bool CalculateDoubleClickedRegion(MouseEventArgs e) {
            // 采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count];
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
            // 这里，为什么要对最后 5 张牌，另行处理？
            // 与上面同理，这里算的是：当前一张牌6, 可能会被后面的多张（最多4 张？）牌盖过。可是问题时，为什么只对最后5 张牌如此处理，而不是每张牌这么处理？
            for (int i = 0; i < k - 1; i++) 
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
        internal int CalculateRightClickedRegion(MouseEventArgs e) {
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