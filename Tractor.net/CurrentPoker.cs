using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace Kuaff.Tractor {
// 好大一个类：    
    class CurrentPoker {
// 当前的Rank: 当前打几 
        internal int Rank = 0;
// 当前的suit: 什么花色、无主？
        internal int Suit = 0;
// 当前牌的数量：
        private int count = 0;
        internal int Count { // 这不是，只算了一副牌的张数？
            get {
                count = 0;
                for (int i = 0; i < 13; i++) {
                    count += diamonds[i];
                    count += clubs[i];
                    count += hearts[i];
                    count += peachs[i];
                }
                count += smallJack;
                count += bigJack;
                return count;
            }
        }
        internal int GetMasterCardsTotal() {
            int tmp = bigJack + smallJack + masterRank + SubRank;
            if (Suit == 1) {
                tmp += HeartsNoRankTotal;
            } else if (Suit == 2) {
                tmp += PeachsNoRankTotal;
            } else if (Suit == 3) {
                tmp += DiamondsNoRankTotal;
            } else if (Suit == 4) {
                tmp += ClubsNoRankTotal;
            }
            return tmp;
        }
        internal int GetMaxCards(int asuit) {
            int rt = -1;
            if (asuit == 1) {
                for(int i=12;i> -1;i--) {
                    if (HeartsNoRank[i]>0) {
                        return i;
                    }
                }
            } else if (asuit == 2) {
                for (int i = 12; i > -1; i--) {
                    if (peachsNoRank[i] > 0) {
                        return i+13;
                    }
                }
            } else if (asuit == 3) {
                for (int i = 12; i > -1; i--) {
                    if (diamondsNoRank[i] > 0) {
                        return i+26;
                    }
                }
            } else if (asuit == 4) {
                for (int i = 12; i > -1; i--) {
                    if (clubsNoRank[i] > 0) {
                        return i+39;
                    }
                }
            }
            return rt;
        }
        internal int GetMaxCard(int asuit) {
            if (asuit == Suit) {
                return GetMaxMasterCards();
            } else {
                return GetMaxCards(asuit);
            }
        }
        internal int GetMaxMasterCards() {
            int rt = -1;
            if (bigJack > 0) {
                rt = 53;
                return rt;
            }
            if (smallJack > 0) {
                rt = 52;
                return rt;
            }
            if (masterRank > 0) {
                rt = (Suit - 1) * 13 + Rank;
                return rt;
            }
            if (Suit != 1) {
                if (HeartsRankTotal > 0) {
                    rt = Rank;
                    return rt;
                }
            }
            if (Suit != 2) {
                if (PeachsRankTotal > 0) {
                    rt = Rank + 13;
                    return rt;
                }
            }
            if (Suit != 3) {
                if (DiamondsRankTotal > 0) {
                    rt = Rank + 26;
                    return rt;
                }
            }
            if (Suit != 4) {
                if (ClubsRankTotal > 0) {
                    rt = Rank + 39;
                    return rt;
                }
            }
            if (Suit == 1) {
                for (int i = 12; i > -1; i--) {
                    if (heartsNoRank[i] > 0) {
                        return i;
                    }
                }
            } else if (Suit == 2) {
                for (int i = 12; i > -1; i--) {
                    if (peachsNoRank[i] > 0) {
                        return i + 13;
                    }
                }
            } else if (Suit == 3) {
                for (int i = 12; i > -1; i--) {
                    if (diamondsNoRank[i] > 0) {
                        return i + 26;
                    }
                }
            } else if (Suit == 4) {
                for (int i = 12; i > -1; i--) {
                    if (clubsNoRank[i] > 0) {
                        return i + 39;
                    }
                }
            }
            return rt;
        }
        internal int GetMinCardsOrScores(int asuit) {
            int rt = -1;
            if (asuit == 1) {
                if (heartsNoRank[8] == 1) {
                    return 8;
                }
                if (heartsNoRank[11] == 1) {
                    return 11;
                }
                if (heartsNoRank[3] == 1) {
                    return 3;
                }
                for (int i = 0; i < 13; i++) {
                    if (heartsNoRank[i] > 0) {
                        return i;
                    }
                }
            } else if (asuit == 2) {
                if (peachsNoRank[8] == 1) {
                    return 21;
                }
                if (peachsNoRank[11] == 1) {
                    return 24;
                }
                if (peachsNoRank[3] == 1) {
                    return 16;
                }
                for (int i = 0; i < 13; i++) {
                    if (peachsNoRank[i] > 0) {
                        return i + 13;
                    }
                }
            } else if (asuit == 3) {
                if (diamondsNoRank[8] == 1) {
                    return 34;
                }
                if (diamondsNoRank[11] == 1) {
                    return 37;
                }
                if (diamondsNoRank[3] == 1) {
                    return 29;
                }
                for (int i = 0; i < 13; i++) {
                    if (diamondsNoRank[i] > 0) {
                        return i + 26;
                    }
                }
            } else if (asuit == 4) {
                if (clubsNoRank[8] == 1) {
                    return 47;
                }
                if (clubsNoRank[11] == 1) {
                    return 50;
                }
                if (clubsNoRank[3] == 1) {
                    return 42;
                }
                for (int i = 0; i < 13; i++) {
                    if (clubsNoRank[i] > 0) {
                        return i + 39;
                    }
                }
            }
            return rt;
        }
        internal int GetMinCardsNoScores(int asuit) {
            int rt = -1;
            if (asuit == 1) {
                for (int i = 0; i < 13; i++) {
                    if ((i == 8) || (i == 11) || (i == 3)) {
                        continue;
                    }
                    if (heartsNoRank[i] > 0) {
                        return i;
                    }
                }
            } else if (asuit == 2) {
                for (int i = 0; i < 13; i++) {
                    if ((i == 21) || (i == 24) || (i == 16)) {
                        continue;
                    }
                    if (peachsNoRank[i] > 0) {
                        return i + 13;
                    }
                }
            } else if (asuit == 3) {
                for (int i = 0; i < 13; i++) {
                    if ((i == 34) || (i == 37) || (i == 29)) {
                        continue;
                    }
                    if (diamondsNoRank[i] > 0) {
                        return i + 26;
                    }
                }
            } else if (asuit == 4) {
                for (int i = 0; i < 13; i++) {
                    if ((i == 47) || (i == 50) || (i == 42)) {
                        continue;
                    }
                    if (clubsNoRank[i] > 0) {
                        return i + 39;
                    }
                }
            }
            return rt;
        }
        internal int GetMinCards(int asuit) {
            int rt = -1;
            if (asuit == 1) {
                for (int i = 0; i < 13; i++) {
                    if (heartsNoRank[i] > 0) {
                        return i;
                    }
                }
            } else if (asuit == 2) {
                for (int i = 0; i < 13; i++) {
                    if (peachsNoRank[i] > 0) {
                        return i + 13;
                    }
                }
            } else if (asuit == 3) {
                for (int i = 0; i < 13; i++) {
                    if (diamondsNoRank[i] > 0) {
                        return i + 26;
                    }
                }
            } else if (asuit == 4) {
                for (int i = 0; i < 13; i++) {
                    if (clubsNoRank[i] > 0) {
                        return i + 39;
                    }
                }
            }
            return rt;
        }
        internal int GetMinMasterCards(int asuit) {
            int rt = -1;
            if (asuit == 1) {
                for (int i = 0; i < 13; i++) {
                    if (heartsNoRank[i] > 0) {
                        return i;
                    }
                }
            } else if (asuit == 2) {
                for (int i = 0; i < 13; i++) {
                    if (peachsNoRank[i] > 0) {
                        return i + 13;
                    }
                }
            } else if (asuit == 3) {
                for (int i = 0; i < 13; i++) {
                    if (diamondsNoRank[i] > 0) {
                        return i + 26;
                    }
                }
            } else if (asuit == 4) {
                for (int i = 0; i < 13; i++) {
                    if (clubsNoRank[i] > 0) {
                        return i + 39;
                    }
                }
            }
            if (Suit != 1) {
                if (HeartsRankTotal > 0) {
                    rt = Rank;
                    return rt;
                }
            }
            if (Suit != 2) {
                if (PeachsRankTotal > 0) {
                    rt = Rank + 13;
                    return rt;
                }
            }
            if (Suit != 3) {
                if (DiamondsRankTotal > 0) {
                    rt = Rank + 26;
                    return rt;
                }
            }
            if (Suit != 4) {
                if (ClubsRankTotal > 0) {
                    rt = Rank + 39;
                    return rt;
                }
            }
            if (masterRank > 0) {
                rt = (Suit - 1) * 13 + Rank;
                return rt;
            }
            if (smallJack > 0) {
                rt = 52;
                return rt;
            }
            if (bigJack > 0) {
                rt = 53;
                return rt;
            }
            return rt;
        }
		// 不知道，这写得什么狗屎一堆破烂代码，连个注释也没有
        internal int[] GetSuitCards(int asuit) { 
            ArrayList list = new ArrayList();
            if (asuit == 5) {
                if (Rank != 53) {
                    if (PeachsRankTotal == 1) {
                        list.Add(13 + Rank);
                    } else if (PeachsRankTotal == 2) {
                        list.Add(13 + Rank);
                        list.Add(13 + Rank);
                    }
                    if (DiamondsRankTotal == 1) {
                        list.Add(26 + Rank);
                    } else if (DiamondsRankTotal == 2) {
                        list.Add(26 + Rank);
                        list.Add(26 + Rank);
                    }
                    if (ClubsRankTotal == 1) {
                        list.Add(39 + Rank);
                    } else if (ClubsRankTotal == 2) {
                        list.Add(39 + Rank);
                        list.Add(39 + Rank);
                    }
                    // 
                    if (HeartsRankTotal == 1) {
                        list.Add(Rank);
                    } else if (HeartsRankTotal == 2) {
                        list.Add(Rank);
                        list.Add(Rank);
                    }
                }
                if (smallJack == 1) {
                    list.Add(52);
                } else if (smallJack == 2) {
                    list.Add(52);
                    list.Add(52);
                }
                if (bigJack == 1) {
                    list.Add(53);
                } else if (bigJack == 2) {
                    list.Add(53);
                    list.Add(53);
                }
            } else if (asuit == Suit) {
                if (asuit == 1) {
                    for (int i = 0; i < 13; i++) {
                        if (heartsNoRank[i] == 1) {
                            list.Add(i);
                        }
                        else if (heartsNoRank[i] == 2) {
                            list.Add(i);
                            list.Add(i);
                        }
                    }
                    // 
                    if (PeachsRankTotal == 1) {
                        list.Add(13+ Rank);
                    } else if (PeachsRankTotal == 2) {
                        list.Add(13 + Rank);
                        list.Add(13 + Rank);
                    }
                    if (DiamondsRankTotal == 1) {
                        list.Add(26 + Rank);
                    } else if (DiamondsRankTotal == 2) {
                        list.Add(26 + Rank);
                        list.Add(26 + Rank);
                    }
                    if (ClubsRankTotal == 1) {
                        list.Add(39 + Rank);
                    } else if (ClubsRankTotal == 2) {
                        list.Add(39 + Rank);
                        list.Add(39 + Rank);
                    }
                    // 
                    if (HeartsRankTotal == 1) {
                        list.Add(Rank);
                    } else if (HeartsRankTotal == 2) {
                        list.Add(Rank);
                        list.Add(Rank);
                    }
                    // 
                    if (smallJack == 1) {
                        list.Add(52);
                    } else if (smallJack == 2) {
                        list.Add(52);
                        list.Add(52);
                    }
                    if (bigJack == 1) {
                        list.Add(53);
                    } else if (bigJack == 2) {
                        list.Add(53);
                        list.Add(53);
                    }
                } else if (asuit == 2) {
                    for (int i = 0; i < 13; i++) {
                        if (peachsNoRank[i] == 1) {
                            list.Add(i+13);
                        }
                        else if (peachsNoRank[i] == 2) {
                            list.Add(i+13);
                            list.Add(i+13);
                        }
                    }
                    // 
                    if (HeartsRankTotal == 1) {
                        list.Add(Rank);
                    } else if (HeartsRankTotal == 2) {
                        list.Add(Rank);
                        list.Add(Rank);
                    }
                    if (DiamondsRankTotal == 1) {
                        list.Add(26 + Rank);
                    } else if (DiamondsRankTotal == 2) {
                        list.Add(26 + Rank);
                        list.Add(26 + Rank);
                    }
                    if (ClubsRankTotal == 1) {
                        list.Add(39 + Rank);
                    } else if (ClubsRankTotal == 2) {
                        list.Add(39 + Rank);
                        list.Add(39 + Rank);
                    }
                    // 
                    if (PeachsRankTotal == 1) {
                        list.Add(13 + Rank);
                    } else if (PeachsRankTotal == 2) {
                        list.Add(13 + Rank);
                        list.Add(13 + Rank);
                    }
                    // 
                    if (smallJack == 1) {
                        list.Add(52);
                    } else if (smallJack == 2) {
                        list.Add(52);
                        list.Add(52);
                    }
                    if (bigJack == 1) {
                        list.Add(53);
                    } else if (bigJack == 2) {
                        list.Add(53);
                        list.Add(53);
                    }
                } else if (asuit == 3) {
                    for (int i = 0; i < 13; i++) {
                        if (diamondsNoRank[i] == 1) {
                            list.Add(i+26);
                        }
                        else if (diamondsNoRank[i] == 2) {
                            list.Add(i+26);
                            list.Add(i+26);
                        }
                    }
                    // 
                    if (PeachsRankTotal == 1) {
                        list.Add(13 + Rank);
                    } else if (PeachsRankTotal == 2) {
                        list.Add(13 + Rank);
                        list.Add(13 + Rank);
                    }
                    if (HeartsRankTotal == 1) {
                        list.Add(Rank);
                    } else if (HeartsRankTotal == 2) {
                        list.Add(Rank);
                        list.Add(Rank);
                    }
                    // 
                    if (DiamondsRankTotal == 1) {
                        list.Add(26 + Rank);
                    } else if (DiamondsRankTotal == 2) {
                        list.Add(26 + Rank);
                        list.Add(26 + Rank);
                    }
                    if (ClubsRankTotal == 1) {
                        list.Add(39 + Rank);
                    } else if (ClubsRankTotal == 2) {
                        list.Add(39 + Rank);
                        list.Add(39 + Rank);
                    }
                    // 
                    if (smallJack == 1) {
                        list.Add(52);
                    } else if (smallJack == 2) {
                        list.Add(52);
                        list.Add(52);
                    }
                    if (bigJack == 1) {
                        list.Add(53);
                    } else if (bigJack == 2) {
                        list.Add(53);
                        list.Add(53);
                    }
                } else if (asuit == 4) {
                    for (int i = 0; i < 13; i++) {
                        if (clubsNoRank[i] == 1) {
                            list.Add(i+39);
                        }
                        else if (clubsNoRank[i] == 2) {
                            list.Add(i+39);
                            list.Add(i+39);
                        }
                    }
                    // 
                    if (HeartsRankTotal == 1) {
                        list.Add(Rank);
                    } else if (HeartsRankTotal == 2) {
                        list.Add(Rank);
                        list.Add(Rank);
                    }
                    if (PeachsRankTotal == 1) {
                        list.Add(13 + Rank);
                    } else if (PeachsRankTotal == 2) {
                        list.Add(13 + Rank);
                        list.Add(13 + Rank);
                    }
                    if (DiamondsRankTotal == 1) {
                        list.Add(26 + Rank);
                    } else if (DiamondsRankTotal == 2) {
                        list.Add(26 + Rank);
                        list.Add(26 + Rank);
                    }
                    // 
                    if (ClubsRankTotal == 1) {
                        list.Add(39 + Rank);
                    } else if (ClubsRankTotal == 2) {
                        list.Add(39 + Rank);
                        list.Add(39 + Rank);
                    }
                    // 
                    if (smallJack == 1) {
                        list.Add(52);
                    } else if (smallJack == 2) {
                        list.Add(52);
                        list.Add(52);
                    }
                    if (bigJack == 1) {
                        list.Add(53);
                    } else if (bigJack == 2) {
                        list.Add(53);
                        list.Add(53);
                    }
                }
            } else {
                if (asuit == 1) {
                    for (int i = 0; i < 13; i++) {
                        if (heartsNoRank[i] == 1) {
                            list.Add(i);
                        }
                        else if (heartsNoRank[i] == 2) {
                            list.Add(i);
                            list.Add(i);
                        }
                    }
                } else if (asuit == 2) {
                    for (int i = 0; i < 13; i++) {
                        if (peachsNoRank[i] == 1) {
                            list.Add(i+13);
                        }
                        else if (peachsNoRank[i] == 2) {
                            list.Add(i + 13);
                            list.Add(i + 13);
                        }
                    }
                } else if (asuit == 3) {
                    for (int i = 0; i < 13; i++) {
                        if (diamondsNoRank[i] == 1) {
                            list.Add(i+26);
                        }
                        else if (diamondsNoRank[i] == 2) {
                            list.Add(i + 26);
                            list.Add(i + 26);
                        }
                    }
                } else if (asuit == 4) {
                    for (int i = 0; i < 13; i++) {
                        if (clubsNoRank[i] == 1) {
                            list.Add(i+39);
                        }
                        else if (clubsNoRank[i] == 2) {
                            list.Add(i + 39);
                            list.Add(i + 39);
                        }
                    }
                }
            }
            return (int[])list.ToArray(typeof(int));
        }
        internal int[] GetSuitAllCards(int asuit) {
            if (asuit == 1) {
                return heartsNoRank;
            } else if (asuit == 2) {
                return peachsNoRank;
            } else if (asuit == 3) {
                return diamondsNoRank;
            } else if (asuit == 4) {
                return clubsNoRank;
            }
            return new int[13];
        }
// 破烂不学OOD/OOP 的主程：私有数组，用来表示，一个花色的【缺省张数】：数组初始化为 0
#region 方块
// 方块(2,3,4,5,6,7,8,9,10,J,Q,K,A)
        private int[] diamonds = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // 13 个元素都有
        internal int[] Diamonds {
            get { return diamonds; }
            set { diamonds = value; }
        }
// 不带主的方块
        private int[] diamondsNoRank = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // 13 个元素都有
        internal int[] DiamondsNoRank {
            get { return diamondsNoRank; }
            set { diamondsNoRank = value; }
        }
// 方块Rank数
        internal int DiamondsRankTotal = 0;
// 方块非Rank数
        internal int DiamondsNoRankTotal = 0;
// 排序的牌型：怎么就变成了是 56 个元素呢？
        internal int[] SortCards = new int[56];
#endregion // 方块
#region 梅花
// 梅花(2,3,4,5,6,7,8,9,10,J,Q,K,A)
        private int[] clubs = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] Clubs {
            get { return clubs; }
            set { clubs = value; }
        }
// 不带主的梅花
        private int[] clubsNoRank = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] ClubsNoRank {
            get { return clubsNoRank; }
            set { clubsNoRank = value; }
        }
// 梅花Rank数
        internal int ClubsRankTotal = 0;
// 梅花非Rank数
        internal int ClubsNoRankTotal = 0;
#endregion // 梅花
#region 红桃
// 红桃(2,3,4,5,6,7,8,9,10,J,Q,K,A)
        private int[] hearts = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] Hearts {
            get { return hearts; }
            set { hearts = value; }
        }
// 不带主的红桃
        private int[] heartsNoRank = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] HeartsNoRank {
            get { return heartsNoRank; }
            set { heartsNoRank = value; }
        }
// 红桃Rank数
        internal int HeartsRankTotal = 0;
// 红桃非Rank数
        internal int HeartsNoRankTotal = 0;
#endregion // 红桃
#region 黑桃
// 黑桃(2,3,4,5,6,7,8,9,10,J,Q,K,A)
        private int[] peachs = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] Peachs {
            get { return peachs; }
            set { peachs = value; }
        }
// 不带主的黑桃
        private int[] peachsNoRank = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] PeachsNoRank {
            get { return peachsNoRank; }
            set { peachsNoRank = value; }
        }
// 黑桃Rank数
        internal int PeachsRankTotal = 0;
// 黑桃非Rank数
        internal int PeachsNoRankTotal = 0;
#endregion // 黑桃
#region 大小王
// 大王
        private int bigJack = 0;
        internal int BigJack {
            get { return bigJack; }
            set { bigJack = value; }
        }
// 小王
        private int smallJack = 0;
        internal int SmallJack {
            get { return smallJack; }
            set { smallJack = value; }
        }
#endregion // 大小王
// 感觉，这里，没明白说的是什么意思？
#region Rank记录 
// 主Rank
        private int masterRank = 0;
        internal int MasterRank {
            get { return masterRank; }
            set { masterRank = value; }
        }
// 副牌Rank
        private int subRank = 0;
        internal int SubRank {
            get  {
                return HeartsRankTotal + PeachsRankTotal + DiamondsRankTotal + ClubsRankTotal - masterRank;
            }
            set { subRank = value; }
        }
#endregion // Rank记录
// 牌的花色排序，索引，是按照【大王53、小王52、黑桃0-12, 红桃13-25, 方块26-38, 梅花 39-51】的索引来的
// 增加一张牌
        internal void AddCard(int number) {
            if (number == 53) { // 大王
                bigJack++;
            } else if (number == 52) { // 小王
                smallJack++;
            } else if (number < 52) {
                if (number >= 0 && number < 13) { // 黑桃
                    hearts[number]++;
                    if (number == Rank) {
                        HeartsRankTotal++;
                    } else {
                        heartsNoRank[number]++;
                        HeartsNoRankTotal++;
                    }
                } else if (number >= 13 && number < 26) { // 红桃
                    peachs[number - 13]++;
                    if ((number - 13) == Rank) {
                        PeachsRankTotal++;
                    } else {
                        peachsNoRank[number - 13]++;
                        PeachsNoRankTotal++;
                    }
                } else if (number >= 26 && number < 39) { // 方块
                    diamonds[number - 26]++;
                    if ((number - 26) == Rank) {
                        DiamondsRankTotal++;
                    } else {
                        DiamondsNoRank[number - 26]++;
                        DiamondsNoRankTotal++;
                    }
                } else if (number >= 39 && number < 52) { // 梅花
                    clubs[number - 39]++;
                    if ((number - 39) == Rank) {
                        ClubsRankTotal++;
                    } else {
                        ClubsNoRank[number - 39]++;
                        ClubsNoRankTotal++;
                    }
                }
                if (Suit > 0 && Suit < 5) { // 这里，说什么意思 
                    if (number == ((Suit - 1) * 13 + Rank)) {
                        masterRank++;
                    }
                }
            }
        }
// 删除一张牌
        internal void RemoveCard(int number) {
            if (number == 53) { // 大王
                bigJack--;
                return;
            } else if (number == 52) {
                smallJack--;
                return;
            } else if (number < 52) {
                if (number >= 0 && number < 13) {
                    hearts[number]--;
                    if (number == Rank) {
                        HeartsRankTotal--;
                    } else {
                        heartsNoRank[number]--;
                        HeartsNoRankTotal--;
                    }
                } else if (number >= 13 && number < 26) {
                    peachs[number - 13]--;
                    if ((number - 13) == Rank) {
                        PeachsRankTotal--;
                    } else {
                        peachsNoRank[number - 13]--;
                        PeachsNoRankTotal--;
                    }
                } else if (number >= 26 && number < 39) {
                    diamonds[number - 26]--;
                    if ((number - 26) == Rank) {
                        DiamondsRankTotal--;
                    } else {
                        DiamondsNoRank[number - 26]--;
                        DiamondsNoRankTotal--;
                    }
                } else if (number >= 39 && number < 52) {
                    clubs[number - 39]--;
                    if ((number - 39) == Rank) {
                        ClubsRankTotal--;
                    } else {
                        ClubsNoRank[number - 39]--;
                        ClubsNoRankTotal--;
                    }
                }
                if (Suit > 0) {
                    if (number == ((Suit - 1) * 13 + Rank)) {
                        masterRank--;
                    }
                }
            }
        }
// 全部清空
        internal void Clear() {
            diamonds = new int[13];
            clubs = new int[13];
            hearts = new int[13];
            peachs = new int[13];
            diamondsNoRank = new int[13];
            clubsNoRank = new int[13];
            heartsNoRank = new int[13];
            peachsNoRank = new int[13];
            DiamondsRankTotal = 0;
            ClubsRankTotal = 0;
            HeartsRankTotal = 0;
            PeachsRankTotal = 0;
            DiamondsNoRankTotal = 0;
            ClubsNoRankTotal = 0;
            HeartsNoRankTotal = 0;
            PeachsNoRankTotal = 0;
            bigJack = 0;
            smallJack = 0;
            masterRank = 0;
            subRank = 0;
            Rank = 0;
            count = 0;
        }
// 排序
        internal void Sort() {
            for (int i = 0; i < 56; i++) // 多少张牌呢 
                SortCards[i] = 0;
            SortCards[0] = bigJack;
            SortCards[1] = smallJack;
            SortCards[2] = masterRank;
            SortCards[3] = SubRank;
            if (Suit == 1) {
                SetSortValues(heartsNoRank, peachsNoRank, diamondsNoRank, clubsNoRank);
            } else if (Suit == 2) {
                SetSortValues(peachsNoRank, heartsNoRank, diamondsNoRank, clubsNoRank);
            } else if (Suit == 3) {
                SetSortValues(diamondsNoRank, heartsNoRank, peachsNoRank, clubsNoRank);
            } else if (Suit == 4) {
                SetSortValues(clubsNoRank, heartsNoRank, peachsNoRank,diamondsNoRank);
            } else if (Suit == 5) {
                SetSortValues(heartsNoRank, peachsNoRank, diamondsNoRank, clubsNoRank);
            }
        }
        private void SetSortValues(int[] nr0, int[] nr1, int[] nr2, int[] nr3) {
            int j = 4;
            for (int i = 12; i > -1; i--) {
                if (i != Rank) {
                    SortCards[j] = nr0[i];
                    j++;
                } else {
                    SortCards[j] = 0;
                    j++;
                }
            }
            for (int i = 12; i > -1; i--) {
                if (i != Rank) {
                    SortCards[j] = nr1[i];
                    j++;
                } else {
                    SortCards[j] = 0;
                    j++;
                }
            }
            for (int i = 12; i > -1; i--) {
                if (i != Rank) {
                    SortCards[j] = nr2[i];
                    j++;
                } else {
                    SortCards[j] = 0;
                    j++;
                }
            }
            for (int i = 12; i > -1; i--) {
                if (i != Rank) {
                    SortCards[j] = nr3[i];
                    j++;
                } else {
                    SortCards[j] = 0;
                    j++;
                }
            }
        }
// 是否是混合出牌
        internal bool IsMixed() {
            if (Suit ==5) {
                int a2 = 0;
                for (int i = 4; i < 56; i++) 
                    if (SortCards[i]>0) 
                        a2++;
                if (a2 == 0) 
                    return false;
            }
            int[] c = {0,0,0,0};
            for (int i=0;i<17;i++) 
                if (SortCards[i]>0)
                    c[0]++;
            for (int i=17;i<30;i++) 
                if (SortCards[i] > 0) 
                    c[1]++;
            for (int i=31;i<43;i++) 
                if (SortCards[i] > 0) 
                    c[2]++;
            for (int i = 44; i < 56; i++) 
                if (SortCards[i] > 0) 
                    c[3]++;
            int total = 0;
            for (int i = 0; i < 4; i++) 
                if (c[i]>0) 
                    total++;
            if (total>1) {
                return true;
            } else {
                if ((Suit ==5) && (total ==1)) {
                    int tmp = SortCards[0] + SortCards[1] + SortCards[2] + SortCards[3];
                    if (tmp > 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
                return false;
            }
        }
// 是否有对
        internal ArrayList GetPairs() { // 算的是：两副或是更多副牌里，【0,53】所有单副牌索引里，同 rank 同花色，不止一张的下标，链条
            ArrayList list = new ArrayList();
            for (int i = 0; i < 13; i++) {
                if (hearts[i] > 1) 
                    list.Add(i);
                if (peachs[i] > 1) 
                    list.Add(i+13);
                if (diamonds[i] > 1) 
                    list.Add(i+26);
                if (clubs[i] > 1) 
                    list.Add(i+39);
            }
            if (smallJack > 1) 
                list.Add(52);
            if (bigJack > 1) 
                list.Add(53);
            return list;
        }
        internal ArrayList GetPairs(int asuit) { // 内部方法：什么地方调用的？
            if (asuit == Suit) { // 主：有花色
                return GetMasterPairs();
            } else { // 无花色，常主
                return GetNoRankPairs(asuit);
            }
        }
        internal ArrayList GetMasterPairs() {
            ArrayList list = new ArrayList();
            for (int i = 0; i < 13; i++) {
                if (Suit == 1) {
                    if (heartsNoRank[i] > 1) {
                        list.Add(i);
                    }
                }
                if (Suit == 2) {
                    if (peachsNoRank[i] > 1) {
                        list.Add(i + 13);
                    }
                }
                if (Suit == 3) {
                    if (diamondsNoRank[i] > 1) {
                        list.Add(i + 26);
                    }
                }
                if (Suit == 4) {
                    if (clubsNoRank[i] > 1) {
                        list.Add(i + 39);
                    }
                }
            }
            if (Suit != 1) {
                if (HeartsRankTotal > 1) {
                    list.Add(Rank);
                }
            }
            if (Suit != 2) {
                if (PeachsRankTotal > 1) {
                    list.Add(Rank + 13);
                }
            }
            if (Suit != 3) {
                if (DiamondsRankTotal > 1) {
                    list.Add(Rank + 26);
                }
            }
            if (Suit != 4) {
                if (ClubsRankTotal > 1) {
                    list.Add(Rank + 39);
                }
            }
            if (masterRank ==2) {
                list.Add((Suit-1)* 13+ Rank);
            }
            if (smallJack > 1) {
                list.Add(52);
            }
            if (bigJack > 1) {
                list.Add(53);
            }
            return list;
        }
        internal ArrayList GetSubRankPairs() {
            ArrayList list = new ArrayList();
            if (Suit != 1) {
                if (HeartsRankTotal == 2) {
                    list.Add(Rank);
                }
            }
            if (Suit != 2) {
                if (PeachsRankTotal == 2) {
                    list.Add(13 + Rank);
                }
            }
            if (Suit != 3) {
                if (DiamondsRankTotal == 2) {
                    list.Add(26 + Rank);
                }
            }
            if (Suit != 4) {
                if (ClubsRankTotal == 2) {
                    list.Add(39 + Rank);
                }
            }
            return list;
        }
        internal ArrayList GetNoRankPairs(int asuit) { 
            ArrayList list = new ArrayList();
            if ((asuit == 1)) {
                for (int i = 0; i < 13; i++) 
                    if (heartsNoRank[i] > 1) 
                        list.Add(i);
            } else if ((asuit == 2)) {
                for (int i = 0; i < 13; i++) 
                    if (peachsNoRank[i] > 1) 
                        list.Add(i + 13);
            } else if ((asuit == 3)) {
                for (int i = 0; i < 13; i++) 
                    if (diamondsNoRank[i] > 1) 
                        list.Add(i + 26);
            } else if ((asuit == 4)) {
                for (int i = 0; i < 13; i++)
                    if (clubsNoRank[i] > 1) 
                        list.Add(i + 39);
            } else if ((asuit == 5)) {
                if (smallJack>1)  
                    list.Add(52);
                if (bigJack > 1) 
                    list.Add(53);
            }
            return list;
        }
        internal ArrayList GetNoRankPairs() {
            ArrayList list = new ArrayList();
            for (int i = 0; i < 13; i++) {
                if (heartsNoRank[i] > 1) 
                    list.Add(i);
                if (peachsNoRank[i] > 1) 
                    list.Add(i+13);
                if (diamondsNoRank[i] > 1) 
                    list.Add(i+26);
                if (clubsNoRank[i] > 1) 
                    list.Add(i+39);
            }
            return list;
        }
        internal ArrayList GetNoRankNoSuitPairs() { // 感觉，这里的几个方法，说的意思没弄明白，要再看一下
            ArrayList list = new ArrayList();
            for (int i = 0; i < 13; i++) {
                if (Suit != 1) {
                    if (heartsNoRank[i] > 1) 
                        list.Add(i);
                }
                if (Suit != 2) {
                    if (peachsNoRank[i] > 1) 
                        list.Add(i+13);
                }
                if (Suit != 3) {
                    if (diamondsNoRank[i] > 1) 
                        list.Add(i+26);
                }
                if (Suit != 4) {
                    if (clubsNoRank[i] > 1) 
                        list.Add(i+39);
                }
            }
            return list;
        }
        internal bool HasMasterRankPairs() {
            if (Rank > 12) 
                return false;
            if (masterRank > 1) {
                return true; 
            } else {
                return false;
            }
        }
        internal bool HasSubRankPairs() {
            if (Rank > 12) 
                return false;
            int count = 0;
            if (hearts[Rank]>1) 
                count++;
            if (peachs[Rank] > 1) 
                count++;
            if (diamonds[Rank] > 1) 
                count++;
            if (clubs[Rank] > 1) 
                count++;
            if (HasMasterRankPairs()) 
                count --;
            if (count > 0) { 
                return true;
            } else {
                return false;
            }
        }
// 是否有拖拉机
        internal bool HasTractors() {
            ArrayList list = GetPairs();
            if (list.Count == 0) 
                return false;
            if (GetTractor() == -1) {
                return false;
            } else {
                return true;
            }
        }
        internal int GetTractor() { // 这个方法，也再读遍
            // 大小王
            if ((smallJack == 2) && (bigJack == 2)) {
                return 53;
            }
            // 小王主花色
            if ((smallJack == 2) && (masterRank == 2)) {
                return 52;
            }
            // 主花色副花色
            if ((masterRank == 2) && HasSubRankPairs()) {
                return ((Suit-1) * 13 + Rank);
            }
            // 副花色A时
            if (HasSubRankPairs()) {
                ArrayList a = GetSubRankPairs();
                int m = 12;
                if (Rank == 12) {
                    m = 11;
                }
                if ((Suit == 1) && (hearts[m] > 1)) {
                    return (int)a[0];
                }
                if ((Suit == 2) && (peachs[m] > 1)) {
                    return (int)a[0];
                }
                if ((Suit == 3) && (diamonds[m] > 1)) {
                    return (int)a[0];
                }
                if ((Suit == 4) && (clubs[m] > 1)) {
                    return (int)a[0];
                }
            }
            // 顺序比较
            for (int i = 12; i > 0; i--) {
                if (i == Rank) {
                    continue;
                }
                int m = i - 1;
                if (m == Rank) {
                    m--;
                }
                if (m < 0) {
                    break;
                }
                if ((heartsNoRank[i] > 1) && (heartsNoRank[m] > 1)) {
                    return i;
                }
                if ((peachsNoRank[i] > 1) && (peachsNoRank[m] > 1)) {
                    return (i+13);
                }
                if ((diamondsNoRank[i] > 1) && (diamondsNoRank[m] > 1)) {
                    return (i + 26);
                }
                if ((clubsNoRank[i] > 1) && (clubsNoRank[m] > 1)) {
                    return (i + 39);
                }
            }
            return -1;
        }
        internal int GetTractor(int asuit) {
            if (asuit == Suit) {
                return GetMasterTractor();
            } else {
                // 顺序比较
                for (int i = 12; i > 0; i--) {
                    if (i == Rank) 
                        continue;
                    int m = i - 1;
                    if (m == Rank) 
                        m--;
                    if (m < 0) 
                        break;
                    if (asuit == 1) {
                        if ((heartsNoRank[i] > 1) && (heartsNoRank[m] > 1)) 
                            return i;
                    }
                    if (asuit == 2) {
                        if ((peachsNoRank[i] > 1) && (peachsNoRank[m] > 1)) 
                            return (i + 13);
                    }
                    if (asuit == 3) {
                        if ((diamondsNoRank[i] > 1) && (diamondsNoRank[m] > 1)) 
                            return (i + 26);
                    }
                    if (asuit == 4) {
                        if ((clubsNoRank[i] > 1) && (clubsNoRank[m] > 1)) 
                            return (i + 39);
                    }
                }
                return -1;
            }
        }
        internal int GetMasterTractor() { // 这里，捡最大的，返回 
            // 大小王
            if ((smallJack == 2) && (bigJack == 2)) 
                return 53;
            // 小王主花色
            if ((smallJack == 2) && (masterRank == 2)) 
                return 52;
            // 主花色副花色
            if ((masterRank == 2) && HasSubRankPairs()) 
                return ((Suit - 1) * 13 + Rank);
            // 副花色A时
            if (HasSubRankPairs()) {
                ArrayList a = GetSubRankPairs();
                int m = Rank; 
                if (Rank == 12) 
                    m = 11;
                if ((Suit == 1) && (hearts[m] > 1)) 
                    return (int)a[0];
                if ((Suit == 2) && (peachs[m] > 1)) 
                    return (int)a[0];
                if ((Suit == 3) && (diamonds[m] > 1)) 
                    return (int)a[0];
                if ((Suit == 4) && (clubs[m] > 1)) 
                    return (int)a[0];
            }
            // 顺序比较
            for (int i = 12; i > 0; i--) {
                if (i == Rank) 
                    continue;
                int m = i - 1;
                if (m == Rank) 
                    m--;
                if (m < 0) 
                    break;
                if (Suit == 1) {
                    if ((heartsNoRank[i] > 1) && (heartsNoRank[m] > 1)) 
                        return i;
                }
                if (Suit == 2) {
                    if ((peachsNoRank[i] > 1) && (peachsNoRank[m] > 1)) 
                        return (i + 13);
                }
                if (Suit == 3) {
                    if ((diamondsNoRank[i] > 1) && (diamondsNoRank[m] > 1)) 
                        return (i + 26);
                }
                if (Suit == 4) {
                    if ((clubsNoRank[i] > 1) && (clubsNoRank[m] > 1)) 
                        return (i + 39);
                }
            }
            return -1;
        }
        internal int[] GetTractorOtherCards(int max) {
            // 大小王
            if (max == 53) 
                return new int[] {53,52,52};
            // 小王主花色
            if (max == 52) 
                return new int[] { 52, (Suit - 1) * 13 + Rank, (Suit - 1) * 13 + Rank };
            // 主花色副花色
            if (max == ((Suit - 1) * 13 + Rank)) {
                ArrayList a = GetSubRankPairs();
                return new int[] { (Suit - 1) * 13 + Rank, (int)a[0],(int)a[0]};
            }
            // 副花色A时
            if (HasSubRankPairs()) {
                ArrayList a = GetSubRankPairs();
                if ((int)a[0] == max) {
                    int m = 12;
                    if (Rank == 12) 
                        m = 11;
                    if ((Suit == 1) && (hearts[m] > 1)) 
                        return new int[] { (int)a[0], m, m };
                    if ((Suit == 2) && (peachs[m] > 1)) 
                        return new int[] { (int)a[0], m + 13, m + 13 };
                    if ((Suit == 3) && (diamonds[m] > 1)) 
                        return new int[] { (int)a[0], m + 26, m + 26 };
                    if ((Suit == 4) && (clubs[m] > 1)) 
                        return new int[] { (int)a[0], m + 39, m + 39 };
                }
            }
            // 顺序比较
            for (int i = 12; i > 0; i--) {
                if (Suit == 1) {
                    int m = i - 1;
                    if (m == Rank) 
                        m--;
                    if (m < 0) 
                        break;
                    if (max == i) {
                        if ((heartsNoRank[i] > 1) && (heartsNoRank[m] > 1)) 
                            return new int[] { i, m, m };
                    }
                }
                if (Suit == 2) {
                    if ((max - 13) == i) {
                        int m = i - 1;
                        if (m == Rank) 
                            m--;
                        if (m < 0) 
                            break;
                        if ((peachsNoRank[i] > 1) && (peachsNoRank[m] > 1)) 
                            return new int[] { i + 13, m + 13, m + 13 };
                    }
                }
                if (Suit == 3) {
                    if ((max - 26) == i) {
                        int m = i - 1;
                        if (m == Rank) 
                            m--;
                        if (m < 0) 
                            break;
                        if ((diamondsNoRank[i] > 1) && (diamondsNoRank[m] > 1)) 
                            return new int[] { i + 26, m + 26, m + 26 };
                    }
                }
                if (Suit == 4) {
                    if ((max - 39) == i) {
                        int m = i - 1;
                        if (m == Rank) 
                            m--;
                        if (m < 0) 
                            break;
                        if ((clubsNoRank[i] > 1) && (clubsNoRank[m] > 1)) 
                            return new int[] { i + 39, m+ 39, m + 39 };
                    }
                }
            }
            // 顺序比较
            for (int i = 12; i > 0; i--) {
                if (Suit != 1) {
                    if (max == i) {
                        int m = i - 1;
                        if (m == Rank) 
                            m--;
                        if (m < 0) 
                            break;
                        if ((heartsNoRank[i] > 1) && (heartsNoRank[m] > 1)) 
                            return new int[] { i, m, m };
                    }
                }
                if (Suit != 2) {
                    if ((max-13) == i) {
                        int m = i - 1;
                        if (m == Rank) 
                            m--;
                        if (m < 0) 
                            break;
                        if ((peachsNoRank[i] > 1) && (peachsNoRank[m] > 1)) 
                            return new int[] { i + 13, m + 13, m + 13 };
                    }
                }
                if (Suit != 3) {
                    if ((max-26) == i) {
                        int m = i - 1;
                        if (m == Rank) 
                            m--;
                        if (m < 0) 
                            break;
                        if ((diamondsNoRank[i] > 1) && (diamondsNoRank[m] > 1)) 
                            return new int[] { i + 26, m +26, m +26 };
                    }
                }
                if (Suit != 4) {
                    if ((max-39) == i) {
                        int m = i - 1;
                        if (m == Rank) 
                            m--;
                        if (m < 0) 
                            break;
                        if ((clubsNoRank[i] > 1) && (clubsNoRank[m] > 1)) 
                            return new int[] { i + 39, m + 39 , m + 39};
                    }
                }
            }
            return null;
        }
        internal int GetNoRankNoSuitTractor() {
            // 顺序比较
            for (int i = 12; i > 0; i--) {
                if (Suit != 1) {
                    if ((heartsNoRank[i] > 1) && (heartsNoRank[i - 1] > 1)) 
                        return i;
                }
                if (Suit != 2) {
                    if ((peachsNoRank[i] > 1) && (peachsNoRank[i - 1] > 1)) 
                        return (i + 13);
                }
                if (Suit != 3) {
                    if ((diamondsNoRank[i] > 1) && (diamondsNoRank[i - 1] > 1)) 
                        return (i + 26);
                }
                if (Suit != 4) {
                    if ((clubsNoRank[i] > 1) && (clubsNoRank[i - 1] > 1)) 
                        return (i + 39);
                }
            }
            return -1;
        }
// 比较单张副牌
        internal bool CompareTo(int number) { // 返回的是：是否，比被比较的数 number 小？
            int masterCards = GetMasterCardsTotal();
            if (number >= 0 && number < 13) {
                for (int i = 12; i >-1; i--) { // 降序：从大往小遍历，如果【同花色】里，还有大牌【张数 > 0】，就不大
                    if (heartsNoRank[i]>0) {
                        if (number >= i)
                            return false;
                        else // 是否，比被比较的数 number 小？
                            return true;
                    }
                }
                if (masterCards > 0) {
                    return true;
                } else {
                    return false;
                }
            } else if (number >= 13 && number < 26) {
                for (int i = 12; i > -1; i--) {
                    if (peachsNoRank[i] > 0) {
                        if ((number-13) >= i)
                            return false;
                        else
                            return true;
                    }
                }
                if (masterCards > 0) {
                    return true;
                } else {
                    return false;
                }
            } else if (number >= 26 && number < 39) {
                for (int i = 12; i > -1; i--) {
                    if (diamondsNoRank[i] > 0) {
                        if ((number-26) >= i)
                            return false;
                        else
                            return true;
                    }
                }
                if (masterCards > 0) {
                    return true;
                } else {
                    return false;
                }
            } else if (number >= 39 && number < 52) {
                for (int i = 12; i > -1; i--) {
                    if (clubsNoRank[i] > 0) {
                        if ((number-39) >= i)
                            return false;
                        else
                            return true;
                    }
                }
                if (masterCards > 0) {
                    return true;
                } else {
                    return false;
                }
            }
            return false;
        }
// 比较对
        internal bool CompareTo(int[] numbers) { // 这里没看明白，要再看一下
			// 最多可能连5 拖，可是概率上应该不至于，真的连续多少拖
// 【连三拖】；只有一种可能？不止一种可能。【AA22 副副22 正正77 副副 77 正正小王小王大王大王】不是这样的，尤其添加【2 是常主】用户可配置功能后
            if (numbers.Length >= 6) {  // 这里不明白
                return false;
            }
            ArrayList al = new ArrayList();
            if (numbers[0] >= 0 && numbers[0] < 13) {
                al = GetNoRankPairs(1);
            } else if (numbers[0] >= 13 && numbers[0] < 26) {
                al = GetNoRankPairs(2);
            } else if (numbers[0] >= 26 && numbers[0] < 39) {
                al = GetNoRankPairs(3);
            } else if (numbers[0] >= 39 && numbers[0] < 52) {
                al = GetNoRankPairs(4);
            }
            if (al.Count == 0) {
                return false;
            } else if (al.Count >= 0) {
                if ((int)al[0] - numbers[0]>=0) {
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        }
        internal bool HasSomeCards(int suit) {
            if (suit == Suit) {
                int count = HeartsRankTotal + PeachsRankTotal + DiamondsRankTotal + ClubsRankTotal;
                count = count + masterRank + subRank + bigJack + smallJack;
                if (suit == 1) {
                    count += HeartsNoRankTotal;
                } else if (suit == 2) {
                    count += PeachsNoRankTotal;
                } else if (suit == 3) {
                    count += DiamondsNoRankTotal;
                } else if (suit == 4) {
                    count += ClubsNoRankTotal;
                }
                if (count > 0)
                    return true;
                else
                    return false;
            } else {
                if (suit == 1)  {
                    if (HeartsNoRankTotal>0) {
                        return true;
                    } else {
                        return false;
                    }
                }
                if (suit == 2) {
                    if (PeachsNoRankTotal > 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
                if (suit == 3) {
                    if (DiamondsNoRankTotal > 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
                if (suit == 4) {
                    if (ClubsNoRankTotal > 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
                if (suit == 5) {
                    if ((smallJack+bigJack) > 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
            return false;
        }
        internal int GetCardCount(int number) {
            if (number ==53) {
                return bigJack;
            } else if (number == 52) {
                return smallJack;
            } else if (number>=0 && number< 13) {
                return hearts[number];
            } else if (number >= 13 && number < 26) {
                return peachs[number-13];
            } else if (number >= 26 && number < 39) {
                return diamonds[number-26];
            } else if (number >= 39 && number < 52) {
                return clubs[number-39];
            }
            return 0;
        }
        internal int[] GetCards() {
            int[] cards = new int[54];
            for (int i = 0; i < 13; i++) {
                cards[i] = hearts[i];
            }
            for (int i = 0; i < 13; i++) {
                cards[i + 13] = peachs[i];
            }
            for (int i = 0; i < 13; i++) {
                cards[i + 26] = diamonds[i];
            }
            for (int i = 0; i < 13; i++) {
                cards[i + 39] = clubs[i];
            }
            cards[52] = smallJack;
            cards[53] = bigJack;
            return cards;
        }
        internal string getAllCards() {
            string pokers = "";
            for (int i = 0; i < 13; i++) {
                if (hearts[i] == 1) {
                    pokers = pokers + i + ",";
                } else if (hearts[i] == 2) {
                    pokers = pokers + i + "," + i + ",";
                }
            }
            for (int i = 0; i < 13; i++) {
                if (peachs[i] == 1) {
                    pokers = pokers + (i + 13) + ",";
                } else if (peachs[i] == 2) {
                    pokers = pokers + (i + 13) + "," + (i + 13) + ",";
                }
            }
            for (int i = 0; i < 13; i++) {
                if (diamonds[i] == 1) {
                    pokers = pokers + (i + 26) + ",";
                } else if (diamonds[i] == 2) {
                    pokers = pokers + (i + 26) + "," + (i + 26) + ",";
                }
            }
            for (int i = 0; i < 13; i++) {
                if (clubs[i] == 1) {
                    pokers = pokers + (i + 39) + ",";
                } else if (clubs[i] == 2) {
                    pokers = pokers + (i + 39) + "," + (i + 39) + ",";
                }
            }
            if (smallJack == 1) {
                pokers = pokers + 52 + ",";
            } else if (smallJack == 2) {
                pokers = pokers + 52 + "," + 52 + ",";
            }
            if (bigJack == 1) {
                pokers = pokers + 53 + ",";
            } else if (bigJack == 2) {
                pokers = pokers + 53 + "," + 53 + ",";
            }
            if (pokers.EndsWith(",")) {
                pokers = pokers.Substring(0, pokers.Length - 1);
            }
            return pokers;
        }
    }
} // 【亲爱的表哥的活宝妹，任何时候，亲爱的表哥的活宝妹就是一定要、一定会嫁给活宝妹的亲爱的表哥！！！爱表哥，爱生活！！！】