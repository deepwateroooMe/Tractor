using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
namespace Kuaff.Tractor {
// 【TODO】：完全随机发牌 ==> 【有偏发牌】更有挑战、也更好玩儿！！
    class DistributePokerHelper {
        // 【0,107】：每个数字占3 位，总共 108 张牌
        StringBuilder sb = new StringBuilder("000001002003004005006007008009"); // [0,1,2,....9]=10*3
        Queue[] queues = new Queue[4] { new Queue(), new Queue(), new Queue(), new Queue()};
        ArrayList[] list = new ArrayList[4] { new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList() };

        public DistributePokerHelper() { // 构建了 108 张牌的字符串
            for (int i = 10; i<100; i++) // 两位数的 index 【10,99】
                sb.Append("0" + i);
            sb.Append("100101102103104105106107"); // 【100,107】
        }
        public ArrayList[] Distribute() {
            Random random = new Random(); // 完全随机，不好玩。【有偏发牌】更有挑战、也更好玩儿！！
            int j = 0;
            int pokerNumber = 0;
            for (int i = 107; i >= 0; i --) { // 字符串长度也变短了
                int select = random.Next(i) * 3; // 用生成随机数的方法，完全随机地分配 108 张牌
                pokerNumber = int.Parse(sb.ToString().Substring(select, 3));
                if (pokerNumber >= 54) // 因为是两副牌
                    pokerNumber -= 54;
                list[j % 4].Add(pokerNumber);
                sb.Remove(select, 3); // 真正移除了
                j++;
            }
            return list;
        }
    }
}
