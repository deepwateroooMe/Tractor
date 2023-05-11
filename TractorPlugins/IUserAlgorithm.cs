using System;
using System.Collections;
using System.Text;
namespace Kuaff.Tractor.Plugins {

    public interface IUserAlgorithm {
        // 算法作者
        string Author {
            get;
        }
        // 算法作者的email地址
        string Email {
            get;
        }
        // 算法名称
        string Name {
            get;
        }
        // 算法介绍
        string Description {
            get;
        }
        // 首先出牌的算法。
        // <param name="who">当前用户是谁，1为南家，2为北家，3为西家，4为东家</param>
        // <param name="suit">当前主牌的花色，1为红心，2为黑桃，3为方片，4为梅花,5为王（无主）</param>
        // <param name="rank">当前打几,0为打2，1为打3,2为打4........11为打K，12为打A,53为打王</param>
        // <param name="master">当前谁为庄家,1为南家，2为北家，3为西家，4为东家</param>
        // <param name="sendCards">当前一局各家已经出掉的牌，sendCards[0]为南家，sendCards[1]为北家，sendCards[2]为西家，sendCards[3]为东家</param>
        // <param name="myCards">此用户手中的牌</param>
        ArrayList ShouldSendCards(int who, int suit, int rank, int master, string[] sendCards, string myCards);
        
        // 改自己出的牌时的算法（自己不是首家）
        // <param name="who">当前用户是谁，1为南家，2为北家，3为西家，4为东家</param>
        // <param name="suit">当前主牌的花色，1为红心，2为黑桃，3为方片，4为梅花,5为王（无主）</param>
        // <param name="rank">当前打几,0为打2，1为打3,2为打4........11为打K，12为打A,53为打王</param>
        // <param name="master">当前谁为庄家,1为南家，2为北家，3为西家，4为东家</param>
        // <param name="whoIsFirst">谁首先出的牌，1为南家，2为北家，3为西家，4为东家</param>
        // <param name="sendCards">当前一局各家已经出掉的牌，sendCards[0]为南家，sendCards[1]为北家，sendCards[2]为西家，sendCards[3]为东家</param>
        // <param name="currentSendCards">首家以及自己的上家出的牌</param>
        // <param name="myCards">此用户手中的牌</param>
        ArrayList MustSendCards(int who, int suit, int rank, int master, int whoIsFirst, string[] sendCards, ArrayList[] currentSendCards, string myCards);
    }
}