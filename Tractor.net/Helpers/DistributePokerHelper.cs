using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Kuaff.Tractor
{
    /// <summary>
    /// ����ַ��˿���.
    /// �ɳ����������108���Ƶ����У��ַ����ĸ��û�.
    /// 
    /// </summary>
    class DistributePokerHelper
    {
        //δ���ҵļ���
        StringBuilder sb = new StringBuilder("000001002003004005006007008009");
        //���Һ�Ķ���
        Queue[] queues = new Queue[4] { new Queue(), new Queue(), new Queue(), new Queue()};
        ArrayList[] list = new ArrayList[4] { new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList() };

        public DistributePokerHelper()
        {
            for (int i = 10; i<100; i++)
            {
                sb.Append("0" + i);
            }
            sb.Append("100101102103104105106107");
        }

        public ArrayList[] Distribute()
        {
            Random random = new Random();
            int j = 0;
            int pokerNumber = 0;

            for (int i = 107; i>-1; i --)
            {
                int select = random.Next(i) * 3;
                pokerNumber = int.Parse(sb.ToString().Substring(select, 3));
                if (pokerNumber>=54)
                {
                    pokerNumber -= 54;
                }

                list[j % 4].Add(pokerNumber);
                sb.Remove(select, 3);
                j++;
            }
            
            return list;

        }
    }
}
